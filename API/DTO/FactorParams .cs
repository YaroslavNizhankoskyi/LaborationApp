using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class FactorParams
    {
        public byte FactorType{ get; set; }

        public bool AscendingName { get; set; }
    }
}
