using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace MyProject.Entities
{
    public class TransferCenter : Unit
    {
        public IList<Agenta> Agentas { get; set; }
    }
}
