using CargoAutomation.Lines;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace MyProject.Entities
{
    public class Line : Entity<int>
    {
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; }
        public int? TransferCenterId { get; set; }
        public List<Station> Stations { get; set; }
    }
}
