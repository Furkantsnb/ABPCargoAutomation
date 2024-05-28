using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyProject.Entities
{
    public class Station : AuditedAggregateRoot<Guid>
    {
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public Guid? UnitId { get; set; }
        public Unit Unit { get; set; }
        public Guid LineId { get; set; }
        public Line Line { get; set; }
    }
}
