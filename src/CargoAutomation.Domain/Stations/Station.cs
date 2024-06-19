using CargoAutomation.Lines;
using CargoAutomation.Units;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace CargoAutomation.Stations
{
    public class Station : AuditedAggregateRoot<Guid>
    {
  
        public int OrderNumber { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public Guid LineId { get; set; }
        public Line Line { get; set; }


    }
   
}
