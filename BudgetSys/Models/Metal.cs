using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Metal :RawMaterial, INotifyPropertyChanged
    {

        private double _length;

        public double m_length
        {
            get { return _length; }
            set {
                _length = value;
            }
        }

        private double _m_weigth;

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

        public double m_width
        {
            get { return _width; }
            set
            {
                _width = value;
            }
        }

		public double m_thick { get; set; }
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
