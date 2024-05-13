
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using static System.Collections.Specialized.BitVector32;

namespace Entities.Concrete
{
    public class Unit : AuditedAggregateRoot<Guid>
    {
      
        public string UnitName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Gsm { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string NeighbourHood { get; set; }
        public string Street { get; set; }
        public string AddressDetail { get; set; }
        public bool IsDeleted { get; set; }
        [ConcurrencyCheck]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    }
}
