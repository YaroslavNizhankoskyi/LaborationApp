using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class UserTipDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime Date { get; set; }

        public bool Watched { get; set; }
    }
}
