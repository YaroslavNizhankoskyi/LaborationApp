using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class TipDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public int CoefficientSum { get; set; }
    }
}
