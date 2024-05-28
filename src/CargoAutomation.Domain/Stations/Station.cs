using Entities.Concrete;
using System;
using Volo.Abp.Domain.Entities;

namespace MyProject.Entities
{
    public class Station : Entity<int>
    {
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }
        public int LineId { get; set; }
        public Line Line { get; set; }
    }
}
