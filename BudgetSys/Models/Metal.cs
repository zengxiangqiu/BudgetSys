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

        private float _length;

        public float m_length
        {
            get { return _length; }
            set {
                _length = value;
            }
        }

        public float m_weigth { get; set; }


        private float _width;

        public float m_width
        {
            get { return _width; }
            set
            {
                _width = value;
            }
        }

		public float m_thick { get; set; }
        private float _volume;
		public float m_volume
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
