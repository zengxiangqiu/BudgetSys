using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BudgetSys.Models;

namespace BudgetSys.Repositories
{
    public class AccessoriesRepository : MatetiralRepository<Accessories>, IMatetrialRepository
    {
        public AccessoriesRepository() : base(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Accessories/")
        {
        }

        public override IEnumerable<object> Materials => throw new NotImplementedException();

        public override IEnumerable<object> Tonnages => throw new NotImplementedException();

        public void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.AutoGenColumns(e);
        }

        public override void Calculate(MaterialBase material)
        {
            var parts = material as Accessories;

            if (parts != null)
            {
                parts.sum = Math.Round(parts.qty * parts.unitPrice, 2);
                parts.remark = Math.Round(parts.sum / Sys.ExchangeRate[0].Current, 2);
            }
        }

        public void DeleteRecord(object dataContext, object material)
        {
            base.DeleteRecord(dataContext, material as Accessories);
        }

        public override BatchType GetBatchType()
        {
            return BatchType.Accessories;
        }

        public void RenameColumn(DataGridColumn column)
        {
            var config = Sys.Columns.Accessories.Where(x => x.Key == column.Header.ToString()).First();
            column.DisplayIndex = config.Value.order;
            column.Header = config.Value.description;
        }

        object IMatetrialRepository.AddNewItem(object dataContext)
        {
            return base.AddNewItem(dataContext);
        }

        object IMatetrialRepository.CreateViewModel(MetalBatch batch)
        {
            return base.CreateViewModel(batch);
        }

        void IMatetrialRepository.DeleteBatch(object dataContext, MetalBatch batch)
        {
            base.DeleteBatch(dataContext, batch);
        }

        ObservableCollection<MetalBatch> IMatetrialRepository.GetBatches()
        {
            return base.GetBatches();
        }
    }
}
