using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Plastic : RawMaterial, INotifyPropertyChanged
    {
        /// <summary>
        /// 净重
        /// </summary>
        public float p_netWeigth { get; set; }

        /// <summary>
        ///口水公式Id 
        /// </summary>
        public int p_salivaId { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public float p_grossWeight { get; set; }


        /// <summary>
        /// 模穴
        /// </summary>
        public int p_cavity { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        public int p_cycle { get; set; }


        /// <summary>
        /// ME合计1
        /// </summary>
        public float p_ME1 { get; set; }

        /// <summary>
        /// ME合计2
        /// </summary>
        public float p_ME2 { get; set; }

    }
}
