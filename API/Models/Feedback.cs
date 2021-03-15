using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Enterpreneur")]
        public string EnterpreneurId{ get; set; }

        public User User { get; set; }

        public User Enterpreneur { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }

        public bool Watched { get; set; }

    }
}
