using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        public string PublicId { get; set; }

        public User User { get; set; }

        public Tip Tip { get; set; }

        public Company Company { get; set; }
    }

}
