using API.DTO;
using API.Helpers;
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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public AdminController(IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("roles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        [HttpGet("users")]
        public IActionResult ListUsers(string email)
        {
            var users = _userManager.Users;

            if (!string.IsNullOrEmpty(email)) 
            {
                users = users
                    .Where(p => p.Email.Contains(email));
            }

            var model = _mapper.Map<UserQuickInfoDto>(users);

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
            var factor = _mapper.Map<Factor>(model);

            _uow.FactorRepository.Add(factor);

            if (_uow.Complete()) 
            {
                return Ok(factor);
            }

            return NotFound();
        }


        [HttpDelete("factors/{id}")]
        public IActionResult RemoveFactor(int id) 
        {
            var factor = _uow.FactorRepository.GetById(id);

            if (factor != null) 
            {
                _uow.FactorRepository.Remove(factor);
                return Ok("Removed");
            }

            return NotFound("No such factor");

        }

    }
}
