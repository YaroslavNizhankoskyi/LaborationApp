using API.DTO;
using API.Models;
using API.Persistance;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class TipCalculator : Tip
    {
        private const int MinNormHBeat = 65;
        private const int MaxNormHBeat = 85;
        private const int HBeatCoeff = 5;

        private const double NormTemperature = 36.6;
        private readonly IUnitOfWork _uow;
        private int coefficient;

        public TipCalculator(Tip tip, IUnitOfWork uow) : base(tip)
        {
            _uow = uow;
            coefficient = 0;
        }


        public bool CanCalcualate() 
        {
            if (LaborFactorId == 0
               && HealthFactorId == 0
               && MentalFactorId == 0
               && SleepFactorId == 0) 
            {
                return false;
            }

            return true;
        }


        /*
             Every Factor in Tip is estimated between -20 and 20 points
             human heartbeat and temperature both estimated from 0 to 10
             together they can show Productivity Coefficient for a human
             at this particular moment.
        */
        public void CalculateTipCoefficient() 
        {
            if (!CanCalcualate()) 
            {
                throw new TipCalculationException("No properties to calculate tip fault coefficient");
            }

            var coefficient = 0;

            if (HealthFactorId != 0) 
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == HealthFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            if (MentalFactorId != 0)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == MentalFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            if (SleepFactorId != 0)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == SleepFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            if (LaborFactorId != 0)
            {
                coefficient += _uow.FactorRepository
                    .Find(p => p.Id == LaborFactorId)
                    .FirstOrDefault()
                    .Coefficient;
            }

            CoefficientSum = coefficient;

        }

        public int CalculateFaultChance(int heartbeat, double temperature ) 
        {
            if (CoefficientSum == 0)
            {
                CalculateTipCoefficient();
            }

            int faultChance = CoefficientSum;

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

        private class TipCalculationException : Exception 
        {
            public TipCalculationException(string message) : base(message)
            {
                    
            }
        } 
    }

}
