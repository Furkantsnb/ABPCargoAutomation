using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Entities.Auditing;

namespace Entities.Concrete
{
    public class Agenta : Unit
    {
       
        public int TransferCenterId { get; set; }
        public TransferCenter TransferCenter { get; set; }
    }
}
