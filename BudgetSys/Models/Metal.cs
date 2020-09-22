using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class Metal : INotifyPropertyChanged,IEditableObject
    {

		public event PropertyChangedEventHandler PropertyChanged;

		// This method is called by the Set accessor of each property.  
		// The CallerMemberName attribute that is applied to the optional propertyName  
		// parameter causes the property name of the caller to be substituted as an argument.  
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        public void BeginEdit()
        {
        }

        public void EndEdit()
        {
        }

        public void CancelEdit()
        {
        }

        public int id { get; set; }
		public string batchNo { get; set; }
		public string m_name { get; set; }
		public string m_picture { get; set; }
		public int m_materialId { get; set; }
        private int _qty;
		public int m_qty
        {
            get { return _qty; }
            set {
                _qty = value;
                this.EndEdit();
            }
        }
        private float _length;

        public float m_length
        {
            get { return _length; }
            set {
                _length = value;
                this.EndEdit();
            }
        }


        private float _width;

        public float m_width
        {
            get { return _width; }
            set
            {
                _width = value;
                this.EndEdit();
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
		public float m_weigth { get; set; }
        private float _loss;
        public float m_loss
        {
            get { return _loss; }
            set
            {
                _loss = value;
                NotifyPropertyChanged();
            }
        }

		public int m_tonnageId { get; set; }
		public int m_workStation { get; set; }
		public float m_yield { get; set; }
		private float _costOfMetetial1;
		public float m_costOfMeterial1 { 
			get
			{
				return _costOfMetetial1;
			}
			set
			{
                if (_costOfMetetial1 != value)
                {
                    _costOfMetetial1 = value;
                    NotifyPropertyChanged();
                }
			} 
		}
        private float _finishedCost1;

        public float m_finishedCost1
        {
            get
            {
                return _finishedCost1;
            }
            set
            {
                if (_finishedCost1 != value)
                {
                    _finishedCost1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

		public float m_CMF1 { get; set; }


        private float _total1;

        public float m_total1
        {
            get
            {
                return _total1;
            }
            set
            {
                if (_total1 != value)
                {
                    _total1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string m_costOfMeterial2 { get; set; }
		public string m_finishedCost2 { get; set; }
		public string m_CMF2 { get; set; }
		public string m_total2 { get; set; }
	}
}
