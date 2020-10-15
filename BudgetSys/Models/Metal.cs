using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Metal : RawMaterial, INotifyPropertyChanged
    {

        private double _length;

        /// <summary>
        /// 长
        /// </summary>
        public double m_length
        {
            get { return _length; }
            set
            {
                _length = value;
                NotifyPropertyChanged();
            }
        }

        private double _m_weigth;

        /// <summary>
        /// 重量
        /// </summary>
        public double m_weigth
        {
            get { return _m_weigth; }
            set
            {
                _m_weigth = value;
                NotifyPropertyChanged();
            }
        }


        private double _width;

        /// <summary>
        /// 宽
        /// </summary>
        public double m_width
        {
            get { return _width; }
            set
            {
                _width = value;
                NotifyPropertyChanged();
            }
        }

        private double _thick;

        /// <summary>
        /// 厚度
        /// </summary>
		public double m_thick
        {
            get
            {
                return _thick;
            }
            set
            {
                _thick = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 体积
        /// </summary>
        private double _volume;
        public double m_volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                NotifyPropertyChanged();
            }
        }
        public int m_workStation { get; set; }

    }
}
