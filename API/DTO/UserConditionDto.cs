using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class UserConditionDto
    {
        public int HealthFactorId { get; set; }

        public int MentalFactorId { get; set; }

        public int SleepFactorId { get; set; }

        public int LaborFactorId { get; set; }
    }
}
