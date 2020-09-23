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
        void Save(object dataContext, MetalBatch currentBatch);
        void Calculate(RawMaterial material);
        object AddNewItem(int id, string batchNo);
        void Delete(object dataContext, object material);
        void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e);
        void RenameColumn(DataGridColumn column);
    }
}
