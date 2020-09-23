using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class PlasticMaterial: MetalMaterial
    {
        /// <summary>
        /// 比重
        /// </summary>
        public float SG { get; set; }
    }
}
