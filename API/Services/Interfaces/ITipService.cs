using API.DTO;
using API.Models;
using API.Persistance;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ITipService
    {
        public Tip CreateTip(CreateTipDto model);
        public Tip EditTip(EditTipDto model);
        public bool DeleteTip(int id);
        public Task<string> AddTipPhoto(int tipId, IFormFile photo);
        public TipDetailsDto GetTip(int id);
        public IEnumerable<TipDto> GetAll();
        public IEnumerable<UserTipDto> GetUserTips(User user);
        public bool CreateUserTip(UserConditionDto model, string userId);
        public bool WatchUserTips(IEnumerable<int> ids);


    }
}
