using API.Helpers;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class FactorParams : PaginationParams
    {
        public byte FactorType{ get; set; }

        public bool AscendingName { get; set; }

        public int LowerCoefficient { get; set; }

        public int HigherCoefficient { get; set; }

        public string Name { get; set; }

    }
}
