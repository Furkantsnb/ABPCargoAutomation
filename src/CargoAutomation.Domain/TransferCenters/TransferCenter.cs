using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyProject.Entities
{
    public class TransferCenter : AuditedAggregateRoot<Guid>
    {
        public IList<Agenta> Agentas { get; set; }
    }
}
