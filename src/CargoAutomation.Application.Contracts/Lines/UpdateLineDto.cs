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
    public class UpdateLineDto :CreateLineDto
    {

     
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; }
        public Guid? TransferCenterId { get; set; }
        public List<Guid> Stations { get; set; }
    }
}
