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
    public class EnterpreneurController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public EnterpreneurController(IUnitOfWork unitOfWork,
                RoleManager<IdentityRole> roleManager,
                UserManager<User> userManager,
                IMapper mapper)
        {
            _uow = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

/*        [HttpPost("{userId}")]
        public IActionResult CreateCompany(CreateCompanyDto model, string userId) 
        {

        }*/
    }
}
