using CargoAutomation.Lines;
using CargoAutomation.Stations;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace CargoAutomation.Lines
{
    public class Line : AuditedAggregateRoot<Guid>
    {
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; }
        public Guid? TransferCenterId { get; set; }
        public List<Station> Stations { get; set; }
    }
}
