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
        public double p_netWeigth { get; set; }

        /// <summary>
        ///口水公式Id 
        /// </summary>

        private double _salivaId;
        public double p_salivaId
        {
            get { return _salivaId; }
            set
            {
                if (_salivaId != value)
                {
                    _salivaId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 毛重
        /// </summary>

        private double _grossWeight;

        public double p_grossWeight
        {
            get { return _grossWeight; }
            set
            {
                if (_grossWeight != value)
                {
                    _grossWeight = value;
                    NotifyPropertyChanged();
                }
            }
        }



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

        private double _p_ME1;
        public double p_ME1
        {
            get { return _p_ME1; }
            set
            {
                if (_p_ME1 != value)
                {
                    _p_ME1 = value;
                    NotifyPropertyChanged();
                }
            }
        }


        /// <summary>
        /// ME合计2
        /// </summary>

        private double _p_ME2;

        public double p_ME2
        {
            get { return _p_ME2; }
            set
            {
                if (_p_ME2 != value)
                {
                    _p_ME2 = value;
                    NotifyPropertyChanged();
                }
            }
        }


    }
}
