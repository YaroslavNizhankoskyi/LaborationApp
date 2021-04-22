using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class TipDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public int CoefficientSum { get; set; }

        public string Text { get; set; }

        public FactorDto HealthFactor { get; set; }

        public FactorDto LaborFactor { get; set; }

        public FactorDto MentalFactor { get; set; }

        public FactorDto SleepFactor { get; set; }

    }
}
