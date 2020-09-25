using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Columns
    {
        public Dictionary<string, ColumnProp> Metal { get; set; }
        public Dictionary<string, ColumnProp> Plastic { get; set; }
        public Dictionary<string, ColumnProp> PurchasedParts { get; set; }
    }

    public class ColumnProp
    {
        public string description { get; set; }
        public int  order { get; set; }
    }
}
