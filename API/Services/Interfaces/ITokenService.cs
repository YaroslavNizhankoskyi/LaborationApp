using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
