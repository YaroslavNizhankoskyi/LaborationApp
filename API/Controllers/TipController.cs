using API.DTO;
using API.Helpers;
using API.Models;
using API.Persistance;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IDataProtectionProvider _provider;
        private readonly IPhotoService _photoService;

        public TipController(IUnitOfWork uow,
            UserManager<User> userManager,
            IMapper mapper,
            IDataProtectionProvider provider,
            IPhotoService photoService)
        {
            _uow = uow;
            _userManager = userManager;
            _mapper = mapper;
            _provider = provider;
            _photoService = photoService;
        }   

        [HttpGet("{name}")]
        public IActionResult GetTips(string name)
        {
            var tips = _uow.TipRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
            {
                tips = tips.Where(p => p.Name == name);
            }

            if (tips.Any()) return Ok(tips);

            return BadRequest("No tips has been found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTip(CreateTipDto model) 
        {
            var tip = _mapper.Map<Tip>(model);

            var tipHash = new TipHash(_provider, tip);

            var hash = tipHash.Protect();

            var search = new TipSearch(_uow, hash, tipHash);

            if (search.TipExists()) 
            {
                return BadRequest("Tip with such factors already exists, consider changing pre-existing tip");
            }

            var result = await _photoService.AddSmallPhotoAsync(model.Photo);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            _uow.PhotoRepository.Add(photo);

            _uow.Complete();
            
            tipHash.PhotoId = photo.Id;

            _uow.TipRepository.Add(tipHash);

            if (_uow.Complete()) 
            {
                return Ok("Created new tip");
            }

            return BadRequest("Error while creating tip");


        }
            
        [HttpPut]
        public async Task<IActionResult> EditTip(EditTipDto model) 
        {
            var tip = _mapper.Map<Tip>(model);

            if (!_uow.TipRepository.Contains(c => c.Id == tip.Id)) 
            {
                return BadRequest("No such tip");
            }

            var tipHash = new TipHash(_provider, tip);

            var hash = tipHash.Protect();

            var search = new TipSearch(_uow, hash, tipHash);

            if (search.TipExists())
            {
                return BadRequest("Tip with such factors already exists, consider changing pre-existing tip");
            }

            var tipFromDb = _uow.TipRepository
                .Find(p => p.Id == tip.Id)
                .FirstOrDefault();

            var updatedTip = _mapper.Map(tipHash, tipFromDb);

            if (_uow.Complete()) 
            {
                return Ok("Tip edited successfully");
            }

            return BadRequest("Error while editing tip");


        }

        [HttpPost("{tipId}")]
        public async Task<IActionResult> AddTipPhoto(int tipId, IFormFile newPhoto) 
        {
            var tip = _uow.TipRepository
                .Find(p => p.Id == tipId)
                .FirstOrDefault();

            if (tip == null) 
            {
                return BadRequest("No tip with such Id");
            }

            var result = await _photoService.AddMediumPhotoAsync(newPhoto);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            _uow.PhotoRepository.Add(photo);

            if (_uow.Complete())
            {
                tip.PhotoId = photo.Id;

                if (_uow.Complete()) 
                {
                    return Ok("Added photo");
                }
            }

            return BadRequest("Error while adding photo");
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveTip(int id) 
        {
            var tip = _uow.TipRepository
                .Find(p => p.Id == id)
                .FirstOrDefault();
            if (tip == null) 
            {
                return BadRequest("No tip with such id");
            }

            _uow.TipRepository.Remove(tip);

            if (_uow.Complete()) 
            {
                return Ok("Tip has been removed");
            }

            return BadRequest("Error while removing tip");
        }

        [Authorize(Roles = "Worker, Enterprener")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTips(string userId) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null) 
            {
                return BadRequest("No such user");
            }

            var userTips = _uow.UserTipRepository.Find(p => p.UserId == userId);

            if(userTips.Any()) return Ok(userTips);

            return BadRequest("No user tips yet");
        }

        [Authorize(Roles = "Enterpreneur")]
        [HttpPost("user")]
        public async Task<IActionResult> CreateUserTip(UserConditionDto model) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user  == null) 
            {
                return BadRequest("No such user");
            }

            var laborName = _uow.TipRepository
                .Find(p => p.LaborFactorId == model.LaborFactorId)
                .FirstOrDefault()
                .Name;


            var tip = _mapper.Map<Tip>(model);

            if (!_uow.TipRepository.Contains(c => c.Id == tip.Id))
            {
                return BadRequest("No such tip");
            }

            var tipHash = new TipHash(_provider, tip);

            var hash = tipHash.Protect();

            var tipCalculator = new TipCalculator(tipHash, _uow);

            var temperature = 36.6;

            var heartbeat = 75;

            var faultChance = tipCalculator.CalculateFaultChance(heartbeat, temperature);

            var userCharacteristic = new UserCharacteristic
            {
                UserId = userId,
                Labor = laborName,
                FaultChance = faultChance,
                Date = DateTime.Now
            };

            _uow.UserCharacteristicRepository.Add(userCharacteristic);

            _uow.Complete();

            var tipSearch = new TipSearch(_uow, hash, tipHash);

            var tipFromDb = tipSearch.FindTip();

            if (tipFromDb == null && _uow.Complete()) 
            {
                return Ok("Your data has been reported, but we cannot guve you recommendation yet");
            }

            if (tipFromDb != null)
            {
                UserTip userTip = new UserTip
                {
                    TipId = tipFromDb.Id,
                    UserId = userId,
                    Date = DateTime.Now,
                    Watched = false,
                };

                _uow.UserTipRepository.Add(userTip);

                if (_uow.Complete()) 
                {
                    return Ok("Your tip has been added");
                }
            }


            return BadRequest("Operation of adding your tip has been failed");
        }

        [Authorize(Roles = "Worker")]
        [HttpPost("watch")]
        public async Task<IActionResult> WatchUserTips(IEnumerable<int> tipIds) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null) 
            {
                return BadRequest("No such user");
            }

            foreach (var tipId in tipIds) 
            {
                var tip = _uow.UserTipRepository
                    .Find(p => p.Id == tipId)
                    .FirstOrDefault();

                if (tip != null)
                {
                    tip.Watched = true;
                }
            }

            if (_uow.Complete()) 
            {
                return Ok();
            }

            return BadRequest();

        }
    }
}
