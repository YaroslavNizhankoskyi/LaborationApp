using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AddFeedbackDto
    {
        public string WorkerId { get; set; }
        public string Text { get; set; }
    }
}
