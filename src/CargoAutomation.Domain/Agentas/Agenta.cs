﻿


using CargoAutomation.TransferCenters;
using CargoAutomation.Units;
using System;
using Volo.Abp.Domain.Entities;

namespace CargoAutomation.Agentas
{
    public class Agenta : Unit
    {
        public Guid TransferCenterId { get; set; }
        public TransferCenter TransferCenter { get; set; }
    }
}
