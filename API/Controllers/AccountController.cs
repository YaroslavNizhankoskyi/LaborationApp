using API.DTO;
using API.Models;
using API.Persistance;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _uow;

        public AccountController(IUnitOfWork uow,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration config,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IPhotoService photoService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _photoService = photoService;
            _roleManager = roleManager;
            _uow = uow;
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
                return BadRequest("User with such email already exists");

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

                if (model.Role == "Enterpreneur") 
                {
                    user.Position = "Enterpreneur";
                    
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                    {
                        return BadRequest("Error while registering user");    
                    }
                   
                }

                return Ok("Registered");

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

        [Authorize]
        [HttpPost("AddPhoto")]
        public async Task<IActionResult> AddPhoto(IFormFile file) 
        {
            var user = await  _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _photoService.AddMediumPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            _uow.PhotoRepository.Add(photo);

            if (_uow.Complete()) 
            {
                user.PhotoId = photo.Id;
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return Ok("User Photo added successfully");
                }
                else 
                {
                    return BadRequest(updateResult.Errors);
                }

                

            }

            return BadRequest("Photo hasn't been added to database");
        }


        [Authorize]
        [HttpDelete("DeletePhoto")]
        public async Task<IActionResult> RemovePhoto(string publicId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await  _photoService.DeletePhotoAsync(publicId);

            if (result.Error != null) 
            {
                return BadRequest("Error while removing photo");
            }

            user.PhotoId = null;

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded) 
            {
                return Ok("Photo removed");
            }

            return BadRequest("Error while removing photo reference");
        }

    }
}
