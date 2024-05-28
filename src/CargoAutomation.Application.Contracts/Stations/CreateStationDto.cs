
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Stations
{
    public class CreateStationDto 
    {
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public Guid UnitId { get; set; }
        public Guid LineId { get; set; }
    }
}
