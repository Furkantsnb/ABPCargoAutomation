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

      public Guid Guid { get; set; }
    }
}
