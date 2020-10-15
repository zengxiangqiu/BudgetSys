using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class ProjectCost: MaterialBase
    {
        /// <summary>
        ///项目  
        /// </summary>
        public string project { get; set; }
        /// <summary>
        ///明细   
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 费用（圆）	
        /// </summary>
        private string _cost;
        public string cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
