

using System;
using Volo.Abp.Application.Dtos;

namespace Entities.Dtos.Agentas
{
    public class AgentaDto : EntityDto<Guid>
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
        public bool IsDeleted { get; set; } = false;
        public Guid TransferCenterId { get; set; }
    }
}
