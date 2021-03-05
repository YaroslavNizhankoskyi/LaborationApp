﻿using API.DTO;
using API.Models;
using API.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(IUnitOfWork uow,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration config,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NoContent();
            }

            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, userRole)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["TokenKey"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var userInfo = new UserDto
                {
                    FirstName = user.FirstName,
                    Role = userRole,
                    Token = tokenHandler.WriteToken(token),
                    Id = user.Id,
                    Email = user.Email
                };

                return Ok(userInfo);
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            User user = _mapper.Map<User>(model);

            if ((await _userManager.FindByEmailAsync(user.Email) != null))
                return BadRequest("UserName email exists");

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!(await _roleManager.RoleExistsAsync(model.Role)))
                {
                    _userManager.DeleteAsync(user);
                    return BadRequest("No such role");
                }

                IdentityResult autorize_result = await _userManager.AddToRoleAsync(user, model.Role);

                if (!autorize_result.Succeeded)
                {
                    return BadRequest(autorize_result.Errors);
                }
                return Ok();
            }
            return BadRequest("Аn error occured");
        }


        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
