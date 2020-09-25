using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class MetalBatch: INotifyPropertyChanged
    {
        //public int id { get; set; }
        public MetalBatch()
        {
            batchNo = "";
        }

        public string batchNo
        {
            get { return _batchNo; }
            set {
                _batchNo = value;
                NotifyPropertyChanged();
            }
        }
        private string _batchNo;
        public BatchType batchType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public enum BatchType
    {
        Metal,
        Plastic
    }
}
