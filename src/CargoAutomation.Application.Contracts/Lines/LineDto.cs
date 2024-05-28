using CargoAutomation.Lines;

using Entities.Dtos.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Entities.Dtos.Lines
{
    public class LineDto : AuditedEntityDto<Guid>
    {
       
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; }
        public Guid TransferCenterId { get; set; }
        public List<StationDto> Stations { get; set; }
    }
}
