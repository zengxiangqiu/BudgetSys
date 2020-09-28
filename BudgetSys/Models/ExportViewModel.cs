using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSys.Models
{
    public class ExportViewModel
    {
        public ObservableCollection<ExportFile> Metals { get; set; }
        public ObservableCollection<ExportFile> Platics { get; set; }
        public ObservableCollection<ExportFile> ProjectCost { get; set; }
        public ObservableCollection<ExportFile> PurchasedParts { get; set; }
        public ObservableCollection<ExportFile> CostAdjustment { get; set; }
    }
}
