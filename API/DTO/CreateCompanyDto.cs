﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CreateCompanyDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile File { get; set; }
    }
}
