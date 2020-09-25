using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public abstract class RawMaterial : INotifyPropertyChanged, IEditableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
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
        public string name { get; set; }

        public string picture { get; set; }

        public int materialId { get; set; }

        public int  qty { get; set; }

        private double _loss;
        public double loss
        {
            get { return _loss; }
            set
            {
                _loss = value;
                NotifyPropertyChanged();
            }
        }

        public int tonnageId { get; set; }

        public float yield { get; set; }

        private double _costOfMetetial1;
        public double costOfMeterial1
        {
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

        private double _finishedCost1;
        public double finishedCost1
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

        public double CMF1 { get; set; }


        private double _total1;

        public double total1
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
        private double _costOfMetetial2;
        public double costOfMeterial2
        {
            get
            {
                return _costOfMetetial2;
            }
            set
            {
                if (_costOfMetetial2 != value)
                {
                    _costOfMetetial2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _finishedCost2;
        public double finishedCost2
        {
            get
            {
                return _finishedCost2;
            }
            set
            {
                if (_finishedCost2 != value)
                {
                    _finishedCost2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double CMF2 { get; set; }


        private double _total2;

        public double total2
        {
            get
            {
                return _total2;
            }
            set
            {
                if (_total2 != value)
                {
                    _total2 = value;
                    NotifyPropertyChanged();
                }
            }
        }


    }
}
