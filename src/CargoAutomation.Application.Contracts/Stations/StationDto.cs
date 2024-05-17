
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Entities.Dtos.Stations
{
    public class StationDto : AuditedEntityDto<Guid>
    {
 
        public string StationName { get; set; }
        public int OrderNumber { get; set; }
        public int UnitId { get; set; }
        public int LineId { get; set; }
        public bool IsActive { get; set; }
    }
}
