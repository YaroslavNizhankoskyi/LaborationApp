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
        private SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;

        public AccountController(IUnitOfWork uow,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IPhotoService photoService,
            ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _photoService = photoService;
            _roleManager = roleManager;
            _uow = uow;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NoContent();
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var photo = _uow.PhotoRepository
                .Find(u => u.Id == user.PhotoId)
                .FirstOrDefault();

            var photoUrl = "";

            if (photo != null) photoUrl = photo.Url;

            var userInfo = new UserDto
            {
                FirstName = user.FirstName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                Id = user.Id,
                PhotoUrl = photoUrl
            };        

            if ((await _userManager.IsInRoleAsync(user, "Enterpreneur"))) 
            {

                var company = _uow.CompanyRepository
                    .Find(u => u.EnterpreneurId == user.Id)
                    .FirstOrDefault();

                if (company != null) 
                {
                    userInfo.CompanyId = company.Id;
                    userInfo.CompanyName = company.Name;
                }
            }

            return Ok(userInfo);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            User user = _mapper.Map<User>(model);

            if ((await _userManager.FindByEmailAsync(user.Email) != null))
                return BadRequest("User with such email already exists");

            if (!(await _roleManager.RoleExistsAsync(model.Role)))
            {
                return BadRequest("No such role");
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                
                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, model.Role);

                if (!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
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

                var userInfo = new UserDto
                {
                    FirstName = user.FirstName,
                    Token = await _tokenService.CreateToken(user),
                    Email = user.Email,
                    Id = user.Id
                };

                return Ok(userInfo);

            }
            return BadRequest("Аn error occured");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpPost("photo")]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

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
        [HttpDelete("photo")]
        public async Task<IActionResult> RemovePhoto(string publicId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _photoService.DeletePhotoAsync(publicId);

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

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("No such user");
            }

            var model = _mapper.Map<UserDetailsDto>(user);

            var userCompanyName = _uow.CompanyRepository
                .Find(p => p.Id == user.CompanyId)
                .FirstOrDefault()
                .Name;

            var userPhotoUrl = _uow.PhotoRepository
                .Find(p => p.Id == user.PhotoId)
                .FirstOrDefault()
                .Url;

            model.CompanyName = userCompanyName;
            model.PhotoUrl = userPhotoUrl;

            return Ok(model);

        }

        [HttpPut]
        public async Task<IActionResult> EditUser(EditUserDto model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return BadRequest("No user with such id");
            }

            user = _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User updated");
        }
    }
}
