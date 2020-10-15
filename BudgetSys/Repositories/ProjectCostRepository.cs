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
    public class ProjectCostRepository : MatetiralRepository<ProjectCost>, IMatetrialRepository
    {
        private MetalViewModel<ProjectCost> viewModel;

        public ProjectCostRepository() : base(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/ProjectCost/")
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
            var projectCost = material as ProjectCost;

            var sum = viewModel.Details.Where(x => x.id != 18).Aggregate(0d, (totoal, x) => totoal + Convert.ToDouble(x.cost));

            viewModel.Details.Where(x => x.id == 18).First().cost = sum.ToString();
        }

        public void DeleteRecord(object dataContext, object material)
        {
            base.DeleteRecord(dataContext, material as ProjectCost);
        }

        public override BatchType GetBatchType()
        {
            return BatchType.ProjectCost;
        }

        public void RenameColumn(ObservableCollection<DataGridColumn> columns)
        {
            Sys.Columns.ProjectCost.OrderBy(x => x.Value.order).ToList().ForEach(x =>
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
             var vm = base.CreateViewModel(batch);
            viewModel = (MetalViewModel<ProjectCost>)vm;
            return vm;
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
            base.Calculate(materialBase, column, value, Sys.Columns.ProjectCost);
        }
    }
}
