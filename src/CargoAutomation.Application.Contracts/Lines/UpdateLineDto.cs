using CargoAutomation.Lines;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Entities.Dtos.Lines
{
    public class UpdateLineDto :EntityDto
    {
        public Guid Guid { get; set; }
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid? TransferCenterId { get; set; } //hattın ana hat olması durumunda başlangıç duragı.
        public LineType LineType { get; set; } // Enum tipi olarak tanımlanmış LineType bu burada olmayacak
        public bool IsDeleted { get; set; } = false;
        public List<Guid> Stations { get; set; }
    }
}
