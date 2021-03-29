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
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;
        private readonly ITipService _tipService;

        public TipController(
            UserManager<User> userManager,
            IPhotoService photoService,
            ITipService tipService)
        {
            _userManager = userManager;
            _photoService = photoService;
            _tipService = tipService;
        }   

        [HttpGet]
        public IActionResult GetTips()
        {
            var tips = _tipService.GetAll();

            if (tips.Any()) return Ok(tips);

            return BadRequest("No tips has been found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTip(CreateTipDto model) 
        {

            if (_tipService.CreateTip(model) != null) return Ok("Tip has been created");
            
            return BadRequest("Error while creating tip");

        }
            
        [HttpPut]
        public async Task<IActionResult> EditTip(EditTipDto model) 
        {

            if (_tipService.EditTip(model) != null) return Ok("Tip has been edited");

            return BadRequest("Error while editing tip");
        }

        [HttpPost("{tipId}")]
        public async Task<IActionResult> AddTipPhoto(int tipId, IFormFile newPhoto) 
        {
            var result = await _tipService.AddTipPhoto(tipId, newPhoto);

            if (string.IsNullOrEmpty(result)) return Ok("Tip photo has been added");

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveTip(int id) 
        {
            if (_tipService.DeleteTip(id)) return Ok("Tip has been removed");

            return BadRequest("Error while removing tip");
        }

        [Authorize(Roles = "Worker")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTips(string userId) 
        {

            var user = await _userManager.FindByIdAsync(userId);

            var userTips = _tipService.GetUserTips(user);

            if (userTips.Any()) return Ok(userTips);

            return BadRequest("No user tips yet");
        }

        [Authorize(Roles = "Enterpreneur")]
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> CreateUserTip(UserConditionDto model, string userId) 
        {

            if (_tipService.CreateUserTip(model, userId)) return Ok("Created");

            return BadRequest("Operation of adding your tip has been failed");
        }

        [Authorize(Roles = "Worker")]
        [HttpPost("watch")]
        public async Task<IActionResult> WatchUserTips(IEnumerable<int> tipIds) 
        {

            if (_tipService.WatchUserTips(tipIds)) return Ok();

            return BadRequest();

        }
    }
}
