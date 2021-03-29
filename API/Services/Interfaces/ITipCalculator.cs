using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ITipCalculator
    {
        public bool CanCalcualate(Tip tip);
        public int? CalculateTipCoefficient(Tip tip);
        public int CalculateFaultChance(int coefficient, int heartbeat, double temperature);
    }
}
