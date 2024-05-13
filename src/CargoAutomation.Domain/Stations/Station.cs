
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Entities.Concrete
{
    public class Station: AuditedAggregateRoot<Guid>
    {
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }
        public int LineId { get; set; }
        public Line Line { get; set; }
        


    }
}
