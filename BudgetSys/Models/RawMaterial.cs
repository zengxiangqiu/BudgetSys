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

        private float _loss;
        public float loss
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

        private float _costOfMetetial1;
        public float costOfMeterial1
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

        private float _finishedCost1;
        public float finishedCost1
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

        public float CMF1 { get; set; }


        private float _total1;

        public float total1
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
        private float _costOfMetetial2;
        public float costOfMeterial2
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

        private float _finishedCost2;
        public float finishedCost2
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

        public float CMF2 { get; set; }


        private float _total2;

        public float total2
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
