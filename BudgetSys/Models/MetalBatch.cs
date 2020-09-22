using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class MetalBatch
    {
        //public int id { get; set; }

        public string batchNo { get; set; }

        public BatchType batchType { get; set; }
    }

    public enum BatchType
    {
        Metal
    }
}
