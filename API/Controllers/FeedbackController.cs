using API.DTO;
using API.Models;
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
    [Authorize(Roles = "Enterpreneur")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public FeedbackController(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Roles = "Enterpreneur, Worker")]
        [HttpGet("userId")]
        public async Task<IActionResult> GetUserFeedbacks(string userId) 
        {
            var user = await  _userManager.FindByIdAsync(userId);

            if (user == null) 
            {
                return BadRequest("No such user");
            }

            var feedbacks = _uow.FeedbackRepository
                .Find(p => p.UserId == userId);


            var model = _mapper.Map<FeedbackInfoDto>(feedbacks);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(AddFeedbackDto model) 
        {
            var user = await _userManager.FindByIdAsync(model.WorkerId);

            if (user == null)
            {
                return BadRequest("No such user");
            }

            var enterpreneur = await _userManager.FindByNameAsync(User.Identity.Name);

            if (enterpreneur.CompanyId != user.CompanyId) 
            {
                return BadRequest("You have no right to add feedback to that user");
            }

            var feedback = new Feedback
            {
                Text = model.Text,
                Watched = false,
                UserId = model.WorkerId,
                EnterpreneurId = enterpreneur.Id
            };

            _uow.FeedbackRepository.Add(feedback);

            if (_uow.Complete()) 
            {
                return Ok("Added feddback");
            }

            return BadRequest("Error while adding feedback");
        }
        
    }
}
