
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Stations
{
    public class UpdateStationDto 
    {
       
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public int UnitId { get; set; }
        public int LineId { get; set; }
    }
}
