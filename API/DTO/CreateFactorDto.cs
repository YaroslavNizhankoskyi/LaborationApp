using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CreateFactorDto
    {
        public string Name { get; set; }

        public int FactorType{ get; set; }

        public int Coefficient { get; set; }
    }
}
