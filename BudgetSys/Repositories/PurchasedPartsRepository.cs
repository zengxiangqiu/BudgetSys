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
    public class PurchasedPartsRepository : MatetiralRepository<PurchasedParts>, IMatetrialRepository
    {
        public PurchasedPartsRepository() : base(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/PurchasedParts/")
        {
        }

        public void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.AutoGenColumns(e);
        }

        public override void Calculate(MaterialBase material)
        {
            var parts = material as PurchasedParts;

            if (parts != null)
            {
                parts.sum = Math.Round(parts.qty * parts.unitPrice,2);
                parts.remark = Math.Round(parts.sum /Sys.ExchangeRate[0].Current,2);
            }
        }
        public new object CreateViewModel(MetalBatch batch = null)
        {
            return base.CreateViewModel(batch);
        }

        public void DeleteRecord(object dataContext, object material)
        {
            base.DeleteRecord(dataContext, material as PurchasedParts);
        }

        public new void DeleteBatch(object dataContext, MetalBatch batch)
        {
            base.DeleteBatch(dataContext, batch);
        }

        public new ObservableCollection<MetalBatch> GetBatches()
        {
            return base.GetBatches();
        }

        public void RenameColumn(ObservableCollection<DataGridColumn> columns)
        {
            Sys.Columns.PurchasedParts.OrderBy(x => x.Value.order).ToList().ForEach(x =>
            {
                var col = columns.Where(c => c.Header.ToString() == x.Key).First();
                col.DisplayIndex = x.Value.order;
                col.Header = x.Value.description;
            });
        }

        public new void Save(object dataContext)
        {
            base.Save(dataContext);
        }

        public new object AddNewItem(object dataContext)
        {
            return base.AddNewItem(dataContext);
        }

        public override IEnumerable<object> Materials => throw new NotImplementedException();

        public override IEnumerable<object> Tonnages => throw new NotImplementedException();

        public override BatchType GetBatchType()
        {
            return BatchType.PurchasedParts;
        }

        public void Calculate(MaterialBase materialBase, DataGridColumn column, object value)
        {
            base.Calculate(materialBase, column, value, Sys.Columns.PurchasedParts);
        }
    }
}
