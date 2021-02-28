using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }
        
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public string Position { get; set; }

        public int Age { get; set; }

        public IEnumerable<UserTip> UserTips{ get; set; }

        public IEnumerable<Feedback> Feedbacks { get; set; }

        public IEnumerable<Feedback> EnterpreneurFeedbacks { get; set; }

        public IEnumerable<UserCharacteristic> UserCharacteristics { get; set; }

        public Company WorkerCompany { get; set; }

        public Company EnterpreneurCompany { get; set; }
    }
}
