using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string PhotoUrl { get; set; }
        public string Position { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
}
