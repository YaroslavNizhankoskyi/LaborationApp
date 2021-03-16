using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class WorkersInCompanyDto
    {
        public IEnumerable<int> Workers { get; set; }

        public int CompanyId { get; set; }
    }
}
