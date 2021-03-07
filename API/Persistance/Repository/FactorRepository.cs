using API.DTO;
using API.Helpers;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistance.Repository
{
    public class FactorRepository : Repository<Factor>, IFactorRepository
    {
        public FactorRepository(DbContext context): base(context)
        {
                
        }

        public async Task<PagedList<Factor>> GetFactorsAsync(FactorParams query)
        {
            var factors = dbSet.AsEnumerable();

            if (!string.IsNullOrEmpty(query.Name))
                factors = factors
                    .Where(p => p.Name
                        .ToLower()
                        .Contains(
                            query.Name
                            .ToLower()));

            if (isCoefficientCorrect(query.LowerCoefficient)
                && isCoefficientCorrect(query.HigherCoefficient)
                && query.HigherCoefficient > query.LowerCoefficient)
                factors = factors
                    .Where(p => p.Coefficient >= query.LowerCoefficient
                        && p.Coefficient <= query.HigherCoefficient);

            factors = query.AscendingName switch
            {
                true => factors.OrderBy(p => p.Name),
                false => factors.OrderByDescending(p => p.Name)
            };

            if (query.FactorType > 0
                && query.FactorType < 5)
                factors = factors.Where(p => p.FactorTypeId == query.FactorType);

            return await PagedList<Factor>.CreateAsync(factors,
                    query.PageNumber, query.PageSize);
        }

        public bool isCoefficientCorrect(int coefficient) 
        {
            return coefficient != 0
                && coefficient > -100
                && coefficient < 100;
        }
    }
}
