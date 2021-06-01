using API.DTO;
using API.Helpers;
using API.Models;
using API.Persistance;
using API.Services.Interfaces;
using API.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITipService _tipService;
        private readonly ITipCalculator _tipCalculator;


        public AdminController(IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IMapper mapper,
            ITipService tipService,
            ITipCalculator tipCalculator)
        {
            _uow = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _tipService = tipService;
            _tipCalculator = tipCalculator;
        }

        [HttpGet("roles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        [Authorize(Roles = "Admin, Enterpreneur")]
        [HttpGet("users")]
        public IActionResult ListUsers(string email)
        {
            var users = _userManager.Users;

            if (!string.IsNullOrEmpty(email)) 
            {
                users = users
                    .Where(p => p.Email.Contains(email));
            }

            var model = _mapper.Map<User[], IList<UserQuickInfoDto>>(users.ToArray());

            return Ok(model);
        }

        [HttpGet("roles/{name}")]
        public async Task<IActionResult> GetUsersInRole(string name)
        {

            var role = await _roleManager.FindByNameAsync(name);

            if (role == null)
            {
                return BadRequest("No such role");
            }
            IEnumerable<string> emailsOfUsers = (await _userManager.GetUsersInRoleAsync(name)).Select(u => u.Email);

            return Ok(emailsOfUsers);
        }

        [HttpPost("roles/{roleName}")]
        public async Task<IActionResult> EditUsersInRole(string roleName, IEnumerable<string> model)
        {

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return BadRequest("No such role");
            }

            foreach (var email in model)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var userRole = (await _userManager.GetRolesAsync(user))[0];

                if (userRole != roleName)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, userRole);
                    if (!result.Succeeded)
                    {
                        return BadRequest("Error while removeing from role");
                    }


                    result = await _userManager.AddToRoleAsync(user, roleName);
                    if (!result.Succeeded)
                    {
                        return BadRequest("Error while adding to role");
                    }
                }
                continue;
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("factors")]
        public async Task<IActionResult> GetFactors([FromQuery] FactorParams factorParams) 
        {
            var factors = await _uow.FactorRepository.GetFactorsAsync(factorParams);
            if (factors.Any())
            {
                Response.AddPaginationHeader(factors.CurrentPage, factors.PageSize,
                    factors.TotalCount, factors.TotalPages);

                return Ok(factors);
            }
            else 
            {
                return NotFound("No such factors");
            }
        }

        [HttpPost("factors")]
        public async Task<IActionResult> AddFactor(CreateFactorDto model) 
        {
            var validator = new CreateFactorDtoValidator();

            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var factor = _mapper.Map<Factor>(model);

            _uow.FactorRepository.Add(factor);

            if (_uow.Complete()) 
            {
                if(_tipService.AddBaseTip(factor)) return Ok("Added");

                return BadRequest("Error while adding factor");
            }

            return NotFound();
        }


        [HttpDelete("factors/{id}/{typeId}")]
        public IActionResult RemoveFactor(int id, int typeId) 
        {
            var factor = _uow.FactorRepository.GetById(id);

            if (factor != null) 
            {
                if (!_tipService.RemoveBaseTip(factor)) return BadRequest("Error while removing factor");

                var relientTips = new List<Tip>();
                switch (typeId) 
                {
                    case 0:
                        relientTips = _uow.TipRepository
                        .Find(p => p.HealthFactorId == id)
                        .ToList();

                        foreach (var tip in relientTips)
                        {
                            tip.HealthFactorId = null;
                            tip.CoefficientSum = _tipCalculator.CalculateTipCoefficient(tip).Value;
                            tip.FactorHash = _tipService.GetTipHash(tip);
                        }
                        break;
                    case 1:
                        relientTips = _uow.TipRepository
                        .Find(p => p.MentalFactorId == id)
                        .ToList();
                        foreach (var tip in relientTips)
                        {
                            tip.MentalFactorId = null;
                            tip.CoefficientSum = _tipCalculator.CalculateTipCoefficient(tip).Value;
                            tip.FactorHash = _tipService.GetTipHash(tip);
                        }
                        break;

                    case 2:
                        relientTips = _uow.TipRepository
                        .Find(p => p.SleepFactorId == id)
                        .ToList();
                        foreach (var tip in relientTips)
                        {
                            tip.SleepFactorId = null;
                            tip.CoefficientSum = _tipCalculator.CalculateTipCoefficient(tip).Value;
                            tip.FactorHash = _tipService.GetTipHash(tip);
                        }
                        break;
                    case 3:
                        relientTips = _uow.TipRepository
                        .Find(p => p.LaborFactorId == id)
                        .ToList();
                        foreach (var tip in relientTips)
                        {
                            tip.LaborFactorId = null;
                            tip.CoefficientSum = _tipCalculator.CalculateTipCoefficient(tip).Value;
                            tip.FactorHash = _tipService.GetTipHash(tip);
                        }
                        break;
                }

                _uow.Complete();


                _uow.FactorRepository.Remove(factor);

                if(_uow.Complete()) return Ok();
            }

            return NotFound("No such factor");

        }

    }
}
