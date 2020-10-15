using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class MetalTonnage:MasterBase
    {
        public string tonnage { get; set; }
        public string size { get; set; }
        public float cost { get; set; }
    }
}
