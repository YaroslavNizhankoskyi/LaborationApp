using API.DTO;
using API.Helpers;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistance.Repository
{
    public interface IFactorRepository
    {
        Task<PagedList<Factor>> GetFactorsAsync(FactorParams factorParams);

    }
}
