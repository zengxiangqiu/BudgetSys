using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
   public class MetalMaterial:MasterBase
    {
        public string name { get; set; }
        public string description { get; set; }
        public float cost { get; set; }
    }
}
