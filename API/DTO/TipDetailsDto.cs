using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class TipDetailsDto : TipDto
    {
        public string Text { get; set; }

        public FactorDto HealthFactor { get; set; }

        public FactorDto LaborFactor { get; set; }

        public FactorDto MentalFactor { get; set; }

        public FactorDto SleepFactor { get; set; }

    }
}
