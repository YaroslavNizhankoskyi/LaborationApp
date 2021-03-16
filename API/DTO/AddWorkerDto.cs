using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AddWorkerDto
    {
        public string Email { get; set; }

        public string Position { get; set; }

        public int CompanyId { get; set; }
    }
}
