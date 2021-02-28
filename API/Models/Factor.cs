﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Factor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual int FactorTypeId 
        {
            get 
            {
                return (int)this.FactorType;
            }
            set
            {
                FactorType = (FactorTypes)value;
            }
        }
        
        [EnumDataType(typeof(FactorTypes))]
        public FactorTypes FactorType { get; set; }

        public string Name { get; set; }

        public int Coefficient { get; set; }

        public IEnumerable<Tip> HealthTips{ get; set; }

        public IEnumerable<Tip> MentalTips { get; set; }

        public IEnumerable<Tip> SleepTips { get; set; }

        public IEnumerable<Tip> LaborTips { get; set; }
    }

    public enum FactorTypes
    {
        Mental = 0,
        Health = 1,
        Sleep = 4,
        Labor = 5
    }
}
