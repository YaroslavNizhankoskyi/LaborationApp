using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserCharacteristic
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [MaxLength(50)]
        public string Labor { get; set; }

        [Range(-80, 100)]
        public int FaultChance { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

    }
}
