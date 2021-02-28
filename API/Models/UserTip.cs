using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserTip
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Tip")]
        public int TipId { get; set; }

        public DateTime Date { get; set; }

        public bool Watched { get; set; }

        public virtual Tip Tip { get; set; }

        public virtual User User { get; set; }
    }

}
