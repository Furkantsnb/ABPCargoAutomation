using CargoAutomation.Agentas;
using CargoAutomation.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoAutomation.TransferCenters;

    public class TransferCenter:Unit
    {
        public IList<Agenta> Agentas { get; set; }
    }

