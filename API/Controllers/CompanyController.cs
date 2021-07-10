using API.DTO;
using API.Models;
using API.Persistance;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "Enterpreneur")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public CompanyController(IUnitOfWork unitOfWork,
               RoleManager<IdentityRole> roleManager,
               UserManager<User> userManager,
               IMapper mapper,
               IPhotoService phototService)
        {
            _uow = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _photoService = phototService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var company = new Company
            {
                Name = model.Name,
                Description = model.Description,
                EnterpreneurId = user.Id
            };

            if (model.File != null)
            {

                var result = await _photoService.AddMediumPhotoAsync(model.File);

                if (result.Error != null)
                {
                    return BadRequest("Error while adding photo");
                }

                var photo = new Photo
                {
                    PublicId = result.PublicId,
                    Url = result.SecureUrl.AbsoluteUri
                };

                _uow.PhotoRepository.Add(photo);

                _uow.Complete();

                company.PhotoId = photo.Id;
            }

            _uow.CompanyRepository.Add(company);

            if (_uow.Complete())
            {
                return Ok("Company has been added");
            }

            return BadRequest("Error while adding company");
        }
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> RemoveCompany(int companyId) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var company = _uow.CompanyRepository
                .Find(p => p.Id == companyId)
                .FirstOrDefault();

            if (company == null)
            {
                return BadRequest("No such company");
            }

            if (user.Id != company.EnterpreneurId) 
            {
                return BadRequest("You are not owner of this company");
            }



            _uow.CompanyRepository.Remove(company);

            if (_uow.Complete()) 
            {
                return Ok("Company has been removed");
            }

            return BadRequest("Error while removing company");

        }

        [HttpPost("worker")]
        public async Task<IActionResult> AddWorker(AddWorkerDto model) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var company = _uow.CompanyRepository
                .Find(p => p.Id == model.CompanyId)
                .FirstOrDefault();

            if (company == null)
            {
                return BadRequest("No such company");
            }

            if (company.EnterpreneurId != user.Id) 
            {
                return BadRequest("You are not owner of this company");
            }

            var worker = await _userManager.FindByEmailAsync(model.Email);


            if (worker.Position != null) 
            {
                return BadRequest("This user is related to other company");
            }

            worker.CompanyId = model.CompanyId;
            worker.Position = model.Position;

            if (_uow.Complete()) 
            {
                return Ok($"Worker has been added to company {company.Name}");
            }

            return BadRequest("Error while add worker to company");


        }

        [HttpDelete("{companyId}/worker/{email}")]
        public async Task<IActionResult> RemoveWorker(int companyId, string email) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var company = _uow.CompanyRepository
                .Find(p => p.Id == companyId)
                .FirstOrDefault();
            
            if (company == null)
            {
                return BadRequest("No such company");
            }


            if (company.EnterpreneurId != user.Id)
            {
                return BadRequest("You are not owner of this company");
            }

            var worker = await _userManager.FindByEmailAsync(email);

            worker.Position = null;
            worker.CompanyId = null;

            if (_uow.Complete()) 
            {
                return Ok("User has been removed");
            }

            return BadRequest("Error while removing user");
        }

        [HttpGet("{companyId}/worker")]
        public async Task<IActionResult> GetWorkers(int companyId, string email) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var company = _uow.CompanyRepository
                .Find(p => p.Id == companyId)
                .FirstOrDefault();

            if (company == null) 
            {
                return BadRequest("No such company");
            }

            if (company.EnterpreneurId != user.Id)
            {
                return BadRequest("You are not owner of this company");
            }

            var workers = _userManager.Users
                .Where(p => p.CompanyId == companyId);

            if(!string.IsNullOrEmpty(email)) 
            {
                workers = workers
                    .Where(p => p.Email.Contains(email));
            }
            var model = _mapper.Map<IEnumerable<WorkerDto>>(workers);
            //var model = _mapper.Map<User[], IList<WorkerDto>>(workers.ToArray());

            return Ok(model);

        }

        
        [AllowAnonymous]
        [HttpGet("info/{workerId}")]
        public async Task<IActionResult> GetUserInfo(string workerId) 
        {
            var user = await _userManager.FindByIdAsync(workerId);

            var characteristics = _uow.UserCharacteristicRepository
                .Find(c => c.UserId == workerId);

            var model = new WorkerInfoDto
            {
                WorkerName = user.UserName,
                UserCharacteristics = _mapper.Map<IEnumerable<UserCharacteristicsDto>>(characteristics)
            };

            return Ok(model);
        }
    }
}
