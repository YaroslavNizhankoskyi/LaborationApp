﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
