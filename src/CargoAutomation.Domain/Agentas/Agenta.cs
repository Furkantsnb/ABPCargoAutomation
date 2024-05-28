using Entities.Concrete;
using System;
using Volo.Abp.Domain.Entities;

namespace MyProject.Entities
{
    public class Agenta : Unit
    {
        public int TransferCenterId { get; set; }
        public TransferCenter TransferCenter { get; set; }
    }
}
