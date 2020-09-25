using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class PurchasedParts : MaterialBase
    {
        public int qty { get; set; }

        public string name { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double unitPrice { get; set; }
        /// <summary>
        /// 合计
        /// </summary>
        private double _sum;
        public double sum
        {
            get { return _sum; }
            set { _sum = value; NotifyPropertyChanged(); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        private double _remark;
        public double remark
        {
            get { return _remark; }
            set { _remark = value; NotifyPropertyChanged(); }
        }
    }
}
