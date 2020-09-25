using BudgetSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BudgetSys.Repositories
{
    public interface IMatetrialRepository
    {
        object CreateViewModel(MetalBatch batch=null);
        ObservableCollection<MetalBatch> GetBatches();
        void Save(object dataContext);
        void Calculate(RawMaterial material);
        object AddNewItem(object dataContext);
        void DeleteRecord(object dataContext, object material);
        void DeleteBatch(object dataContext, MetalBatch batch);
        void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e);
        void RenameColumn(DataGridColumn column);
    }
}
