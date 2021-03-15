using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string SecondName { get; set; }
        
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }

        [MaxLength(50)]
        public string? Position { get; set; }

        public int Age { get; set; }

        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }

        public IEnumerable<UserTip> UserTips{ get; set; }

        public IEnumerable<Feedback> Feedbacks { get; set; }

        public IEnumerable<Feedback> EnterpreneurFeedbacks { get; set; }

        public IEnumerable<UserCharacteristic> UserCharacteristics { get; set; }

        public Company WorkerCompany { get; set; }

        public Company EnterpreneurCompany { get; set; }

        public Photo Photo { get; set; }

    }
}
