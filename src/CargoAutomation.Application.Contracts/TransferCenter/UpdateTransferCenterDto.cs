
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.TransferCenter
{
    public class UpdateTransferCenterDto :CreateTransferCenterDto
    {
      
        [ConcurrencyCheck]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
       
    }
}
