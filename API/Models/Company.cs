using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("Enterpreneur")]
        public string EnterpreneurId { get; set; }

        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }

        public User Enterpreneur { get; set; }

        public IEnumerable<User> Workers { get; set; }

        public Photo Photo { get; set; }
    }
}
