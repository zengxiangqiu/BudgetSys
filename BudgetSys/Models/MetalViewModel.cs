using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetSys.Models
{
    public class MetalViewModel<T> where T:RawMaterial
    {
        public ObservableCollection<T> Details { get; set; }

        public ObservableCollection<MetalBatch> Batches { get; set; }

        public MetalBatch CurrentBatch { get; set; }
    }
}
