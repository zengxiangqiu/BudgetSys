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

        public void RenameColumn(DataGridColumn column)
        {
            var config = Sys.Columns.PurchasedParts.Where(x => x.Key == column.Header.ToString()).First();
            column.DisplayIndex = config.Value.order;
            column.Header = config.Value.description;
        }

        public new void Save(object dataContext)
        {
            base.Save(dataContext);
        }

        public new object AddNewItem(object dataContext)
        {
            return base.AddNewItem(dataContext);
        }
    }
}
