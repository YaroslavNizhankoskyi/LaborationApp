﻿using API.Models;
using API.Persistance;
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

        [HttpGet("Roles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        [HttpGet("Users")]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet("Roles/{name}")]
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

        [HttpPost("Roles/{roleName}")]
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

        [HttpGet("Factors")]
        public IActionResult GetFactors([FromQuery] FactorParams factorParam) 
        {

        }

    }
}
