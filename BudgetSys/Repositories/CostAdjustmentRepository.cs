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
    public class CostAdjustmentRepository : MatetiralRepository<CostAdjustment>, IMatetrialRepository
    {
        public CostAdjustmentRepository() : base(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/CostAdjustment/")
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
            var parts = material as CostAdjustment;

            if (parts != null)
            {
                parts.US = Math.Round(parts.RMB/Sys.ExchangeRate[0].Current, 2);
            }
        }

        public void DeleteRecord(object dataContext, object material)
        {
            base.DeleteRecord(dataContext, material as CostAdjustment);
        }

        public override BatchType GetBatchType()
        {
            return BatchType.CostAdjustment;
        }

        public void RenameColumn(ObservableCollection<DataGridColumn> columns)
        {
            Sys.Columns.CostAdjustment.OrderBy(x => x.Value.order).ToList().ForEach(x =>
            {
                var col = columns.Where(c => c.Header.ToString() == x.Key).First();
                col.DisplayIndex = x.Value.order;
                col.Header = x.Value.description;
            });
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

        public void Calculate(MaterialBase materialBase, DataGridColumn column, object value)
        {
            base.Calculate(materialBase, column, value, Sys.Columns.CostAdjustment);
        }
    }
}
