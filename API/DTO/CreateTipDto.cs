using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CreateTipDto
    {
        public string Name { get; set; }

        public string Text { get; set; }
        public int HealthFactorId { get; set; }

        public int MentalFactorId { get; set; }

        public int SleepFactorId { get; set; }

        public int LaborFactorId { get; set; }
    }
}
