using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetSys.Models
{
    public class MetalViewModel<T>: INotifyPropertyChanged where T : RawMaterial
    {
        public MetalViewModel()
       {
            BatchInput = "";
        }

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<T> Details { get; set; }

        public ObservableCollection<MetalBatch> Batches { get; set; }

        private MetalBatch _currentBatch;
        public MetalBatch CurrentBatch
        {
            get { return _currentBatch; }
            set {
                _currentBatch = value;
                NotifyPropertyChanged();
            }
        }

        public string BatchInput { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
