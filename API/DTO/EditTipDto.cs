using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class EditTipDto : CreateTipDto
    {
        public int Id { get; set; }
    }
}
