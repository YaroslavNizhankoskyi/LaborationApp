using API.DTO;
using API.Models;
using API.Persistance;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class TipService : ITipService
    {
        private readonly IDataProtector _protector;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITipCalculator _tipCalculator;
        private readonly IPhotoService _photoService;

        public TipService(
            IMapper mapper,
            IUnitOfWork uow, 
            IDataProtectionProvider provider,
            ITipCalculator tipCalculator,
            IPhotoService photoService)
        {
            _protector = provider.CreateProtector("API.Services.TipService");
            _uow = uow;
            _mapper = mapper;
            _tipCalculator = tipCalculator;
            _photoService = photoService;
        }
        public async Task<string> AddTipPhoto(int tipId, IFormFile photo) 
        {
            var tip = _uow.TipRepository
               .Find(p => p.Id == tipId)
               .FirstOrDefault();

            if (tip == null) return "No such tip";

            var result = await _photoService.AddMediumPhotoAsync(photo);

            if (result.Error != null) return result.Error.Message;

            var newPhoto = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            _uow.PhotoRepository.Add(newPhoto);

            if (_uow.Complete())
            {
                tip.PhotoId = newPhoto.Id;

                if (_uow.Complete())
                {
                    return null;
                }
            }

            return "Error while adding photo";
        }

        public Tip CreateTip(CreateTipDto model)
        {
            var tip = _mapper.Map<Tip>(model);

            var hash = GetTipHash(tip);

            if (TipExists(hash)) return null;

            tip.FactorHash = hash;



            var tipCoefficient = _tipCalculator.CalculateTipCoefficient(tip);

            if (tipCoefficient.HasValue) tip.CoefficientSum = tipCoefficient.Value;

            tip.CanDelete = true;



            _uow.TipRepository.Add(tip);

            if (_uow.Complete()) return tip;

            return null;
        }

        public bool CreateUserTip(UserConditionDto model, string userId)
        {
            var laborName = _uow.FactorRepository
               .Find(p => p.Id == model.LaborFactorId)
               .FirstOrDefault()
               .Name;

            var tip = _mapper.Map<Tip>(model);

            var tipHash = GetTipHash(tip);

            if (TipExists(tipHash)) 
            {
                tip = _uow.TipRepository
                    .Find(u => u.FactorHash == tipHash)
                    .FirstOrDefault();
            }
            else 
            {
                if (!_tipCalculator.CanCalcualate(tip)) return false;
                tip = GetTipBySingleFactor(tip);
                if (tip == null) return false;
            }

            var temperature = 36.6;

            var heartbeat = 75;

            var faultChance = _tipCalculator.CalculateFaultChance(tip.CoefficientSum, heartbeat, temperature);

            var userCharacteristic = new UserCharacteristic
            {
                UserId = userId,
                Labor = laborName,
                FaultChance = faultChance,
                Date = DateTime.Now
            };

            _uow.UserCharacteristicRepository.Add(userCharacteristic);

            _uow.Complete();

            UserTip userTip = new UserTip
            {
                TipId = tip.Id,
                UserId = userId,
                Date = DateTime.Now,
                Watched = false
            };

            _uow.UserTipRepository.Add(userTip);

            if (_uow.Complete()) return true;

            return false;
        }

        public bool DeleteTip(int id)
        {

            var tip = _uow.TipRepository
                .Find(u => u.Id == id)
                .FirstOrDefault();
            _uow.TipRepository.Remove(tip);

            return _uow.Complete();
        }

        public bool RemoveBaseTip(Factor factor) 
        {
            var hash = GetBaseTipHash(factor.Id);

            var tip = _uow.TipRepository
                .Find(u => u.FactorHash == hash)
                .FirstOrDefault();
            if (tip == null) return false;

            _uow.TipRepository
                .Remove(tip);

            return _uow.Complete();
        }

        public bool AddBaseTip(Factor factor)
        {
            switch (factor.FactorTypeId)
            {
                case 0:
                    var tip = new Tip
                    {
                        Name = factor.Name,
                        Text = "Сделайте перерыв. Make a break",
                        MentalFactorId = factor.Id,
                        FactorHash = GetBaseTipHash(factor.Id),
                        CoefficientSum = factor.Coefficient,
                        CanDelete = false
                    };

                    _uow.TipRepository
                        .Add(tip);
                    break;
                case 1:
                    tip = new Tip
                    {
                        Name = factor.Name,
                        Text = "Возможно пришло время поесть. Maybe it's time to eat",
                        MentalFactorId = factor.Id,
                        FactorHash = GetBaseTipHash(factor.Id),
                        CoefficientSum = factor.Coefficient,
                        CanDelete = false
                    };

                    _uow.TipRepository
                        .Add(tip);
                    break;
                case 2:
                    tip = new Tip
                    {
                        Name = factor.Name,
                        Text = "Утро вечера мудренее. An hour in the morning is worth two in the evening.",
                        MentalFactorId = factor.Id,
                        FactorHash = GetBaseTipHash(factor.Id),
                        CoefficientSum = factor.Coefficient,
                        CanDelete = false
                    };

                    _uow.TipRepository
                        .Add(tip);
                    break;
                case 3:
                    tip = new Tip
                    {
                        Name = factor.Name,
                        Text = "Создайте четкий план работы у себя в голове. Make up a clear plan in your head",
                        MentalFactorId = factor.Id,
                        FactorHash = GetBaseTipHash(factor.Id),
                        CoefficientSum = factor.Coefficient,
                        CanDelete = false
                    };

                    _uow.TipRepository
                        .Add(tip);
                    break;
                default:
                    return false;
            }

            if (_uow.Complete()) return true;

            return false;

        }

        public Tip EditTip(EditTipDto model)
        {
            if (!TipExists(model.Id)) return null;

            var tip = _mapper.Map<Tip>(model);

            var tipHash = GetTipHash(tip);

            if (TipExists(tipHash)) return null;

            var tipFromDb = _uow.TipRepository
                .Find(p => p.Id == tip.Id)
                .FirstOrDefault();

            var updatedTip = _mapper.Map(tipHash, tipFromDb);

            _uow.TipRepository
                .Add(updatedTip);

            if (_uow.Complete()) return updatedTip;

            return null;
        }

        public IEnumerable<TipDto> GetAll()
        {
            var tips = _uow.TipRepository
                 .GetAll()
                 .ToArray();
                 

            var model = _mapper.Map<Tip[], IEnumerable<TipDto>>(tips);

            return model;
        }

        public TipDetailsDto GetTip(int id)
        {
            var tip = _uow.TipRepository
                .GetById(id);

            if (tip == null) 
            {
                return null;
            }

            var hfac = _uow.FactorRepository
                .Find(u => u.Id == tip.HealthFactorId)
                .FirstOrDefault();
            var lfac = _uow.FactorRepository
                .Find(u => u.Id == tip.LaborFactorId)
                .FirstOrDefault();
            var mfac = _uow.FactorRepository
                .Find(u => u.Id == tip.MentalFactorId)
                .FirstOrDefault();
            var sfac = _uow.FactorRepository
                .Find(u => u.Id == tip.SleepFactorId)
                .FirstOrDefault();

            var model = new TipDetailsDto
            {
                Name = tip.Name,
                Text = tip.Text,
                CoefficientSum = tip.CoefficientSum
            };
            if (hfac != null)
            {
                model.HealthFactor = new FactorDto
                {
                    Id = hfac.Id,
                    Coefficient = hfac.Coefficient,
                    Name = hfac.Name,
                    FactorType = hfac.FactorTypeId
                };
            }
            if (mfac != null)
            {
                model.MentalFactor = new FactorDto
                {
                    Id = mfac.Id,
                    Coefficient = mfac.Coefficient,
                    Name = mfac.Name,
                    FactorType = mfac.FactorTypeId
                };
            }
            if (lfac != null)
            {
                model.LaborFactor = new FactorDto
                {
                    Id = lfac.Id,
                    Coefficient = lfac.Coefficient,
                    Name = lfac.Name,
                    FactorType = lfac.FactorTypeId
                };
            }

            if (sfac != null)
            {
                model.SleepFactor = new FactorDto
                {
                    Id = sfac.Id,
                    Coefficient = sfac.Coefficient,
                    Name = sfac.Name,
                    FactorType = sfac.FactorTypeId
                };
            }
            return model;
        }

        public IEnumerable<UserTipDto> GetUserTips(User user)
        {
            var userTipIds = _uow.UserTipRepository.Find(p => p.UserId == user.Id)
                .ToList();

            ArrayList userTips = new ArrayList();

            foreach (var tipId in userTipIds) 
            {
                var tip = _uow.TipRepository
                    .Find(u => u.Id == tipId.TipId)
                    .FirstOrDefault();

                var photo = _uow.PhotoRepository
                    .Find(u => u.Id == tip.PhotoId)
                    .FirstOrDefault();

                var photoUrl = "";

                if (photo != null) photoUrl = photo.Url;

                var userTipDto = new UserTipDto
                {
                    Id = tipId.Id,
                    Name = tip.Name,
                    Text = tip.Text,
                    Watched = tipId.Watched,
                    Date = tipId.Date,
                    PhotoUrl = photoUrl
                };

                userTips.Add(userTipDto);
            }

            return userTips.Cast<UserTipDto>();
        }

        public int GetNumberOfUnreadTips(string userId) 
        {

            var numberOfUnreadTips = _uow.UserTipRepository
                .Find(u => u.UserId == userId)
                .Where(t => t.Watched == false)
                .Count();

            return numberOfUnreadTips;

        }

        public bool WatchUserTips(IEnumerable<int> ids)
        {
            foreach (var tipId in ids)
            {
                var tip = _uow.UserTipRepository
                    .Find(p => p.Id == tipId)
                    .FirstOrDefault();

                if (tip != null)
                {
                    tip.Watched = true;
                }
            }

            return _uow.Complete();
        }

        private string GetTipHash(Tip tip)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tip.HealthFactorId);
            sb.Append("-");
            sb.Append(tip.LaborFactorId);
            sb.Append("-");
            sb.Append(tip.MentalFactorId);
            sb.Append("-");
            sb.Append(tip.SleepFactorId);

            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))
                ).Replace("-", String.Empty);
            }

            return hash;
        }

        private string GetBaseTipHash(int id) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(id);
            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))
                ).Replace("-", String.Empty);
            }

            return hash;
        }

        private bool TipExists(string hash)
        {
            var tip = _uow.TipRepository
                .Find(t => t.FactorHash == hash)
                .FirstOrDefault();
            return tip != null;
        }

        private bool TipExists(int id) 
        {
            var tip = _uow.TipRepository
                .Find(u => u.Id == id);

            return tip == null;
        }

        private Tip GetTipBySingleFactor(Tip tip)
        {
            if (tip.HealthFactorId.HasValue)
            {
                var hash = GetBaseTipHash(tip.HealthFactorId.Value);
                return _uow.TipRepository
                    .Find(u => u.FactorHash == hash)
                    .FirstOrDefault();
            }

            if (tip.MentalFactorId.HasValue)
            {
                var hash = GetBaseTipHash(tip.MentalFactorId.Value);
                return _uow.TipRepository
                    .Find(u => u.FactorHash == hash)
                    .FirstOrDefault();
            }

            if (tip.SleepFactorId.HasValue)
            {
                var hash = GetBaseTipHash(tip.SleepFactorId.Value);
                return _uow.TipRepository
                    .Find(u => u.FactorHash == hash)
                    .FirstOrDefault();
            }

            if (tip.LaborFactorId.HasValue)
            {
                var hash = GetBaseTipHash(tip.LaborFactorId.Value);
                return _uow.TipRepository
                    .Find(u => u.FactorHash == hash)
                    .FirstOrDefault();
            }

            return null;

        }
    }
}
