using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Tip
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string FactorHash { get; set; }

        [ForeignKey("HealthFactor")]
        public int? HealthFactorId { get; set; }

        [ForeignKey("MentalFactor")]
        public int? MentalFactorId { get; set; }
        
        [ForeignKey("SleepFactor")]
        public int? SleepFactorId { get; set; }

        [ForeignKey("LaborFactor")]
        public int? LaborFactorId { get; set; }

        public int CoefficientSum { get; set; }

        public Factor HealthFactor { get; set; }

        public Factor MentalFactor { get; set; }

        public Factor SleepFactor { get; set; }

        public Factor LaborFactor { get; set; }

        public IEnumerable<UserTip> UserTips{ get; set; }

        public Tip(Tip tip)
        {
            HealthFactorId = tip.HealthFactorId;
            LaborFactorId = tip.MentalFactorId;
            SleepFactorId = tip.SleepFactorId;
            MentalFactorId = tip.MentalFactorId;
            Name = tip.Name;
            Text = tip.Text;
            FactorHash = tip.FactorHash;
            CoefficientSum = tip.CoefficientSum;
        }

        public Tip()
        {

        }

    }
}
