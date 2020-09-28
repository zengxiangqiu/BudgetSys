using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class CostAdjustment : MaterialBase
    {
        //材料成本调整区	  (RMB/kg)	  (US$/kg)
        public string costAdjustment { get; set; }
        public double RMB { get; set; }
        private double _us;
        public double US
        {
            get { return _us; }
            set
            {
                _us = value;
                NotifyPropertyChanged();
            }
        }
    }
}

