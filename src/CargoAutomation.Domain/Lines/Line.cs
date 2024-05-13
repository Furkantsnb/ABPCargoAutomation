
using CargoAutomation.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Entities.Concrete
{

    public class Line : AuditedAggregateRoot<Guid>
    {

        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; } // Enum tipi olarak tanımlanmış LineType
        public int? TransferCenterId { get; set; } //hattın ana hat olması durumunda başlangıç duragı.

        public List<Station>? Stations { get; set; }
    }
}
