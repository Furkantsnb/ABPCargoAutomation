
using CargoAutomation.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Entities.Dtos.Lines
{
    public class CreateLineDto 
    {
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; } // Enum tipi olarak tanımlanmış LineType
        public Guid? TransferCenterId { get; set; } //hattın ana hat olması durumunda başlangıç duragı.

        public List<Guid> Stations { get; set; }
    }
}
