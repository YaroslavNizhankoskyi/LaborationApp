using API.Models;
using API.Persistance;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TipCalculator : ITipCalculator
    {
        private const int MinNormHBeat = 65;
        private const int MaxNormHBeat = 85;
        private const int HBeatCoeff = 5;

        private const double NormTemperature = 36.6;
        private readonly IUnitOfWork _uow;
        private int coefficient;

        public TipCalculator(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public int CalculateFaultChance(int coefficient, int heartbeat, double temperature)
        {
            int faultChance = coefficient;

            if (heartbeat < MinNormHBeat)
            {
                faultChance += (MinNormHBeat - heartbeat) / HBeatCoeff;
            }

            if (heartbeat > MaxNormHBeat)
            {
                faultChance += (faultChance - heartbeat) / HBeatCoeff;
            }

            var tempDiff = Math.Abs(temperature - NormTemperature);

            faultChance += (int)(tempDiff * 2);
            return faultChance;
        }

        public int? CalculateTipCoefficient(Tip tip)
        {
            if (!CanCalcualate(tip)) return null;

            var coefficient = 0;

            if (tip.HealthFactorId.HasValue)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == tip.HealthFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            if (tip.MentalFactorId.HasValue)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == tip.MentalFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            if (tip.SleepFactorId.HasValue)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == tip.SleepFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            if (tip.LaborFactorId.HasValue)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == tip.LaborFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            return coefficient;
        }

        public bool CanCalcualate(Tip tip)
        {
           if (tip.LaborFactorId.HasValue
               || tip.HealthFactorId.HasValue
               || tip.MentalFactorId.HasValue
               || tip.SleepFactorId.HasValue)
            {
                return true;
            }

            return false;
        }
    }
}
