using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class WorkerInfoDto
    {
        public string WorkerName { get; set; }

        public IEnumerable<UserCharacteristicsDto> UserCharacteristics{ get; set; }
    }
}
