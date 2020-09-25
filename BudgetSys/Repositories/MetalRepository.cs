using BudgetSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BudgetSys.Repositories
{
    public class MetalRepository : MatetiralRepository<Metal>,IMatetrialRepository
    {
        public MetalRepository():base(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Metal/")
        {
            
        }

        public new object AddNewItem(object dataContext)
        {
            return base.AddNewItem(dataContext);
        }

        public void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.AutoGenColumns(e);
        }

        public override void Calculate(MaterialBase material)
        {
            var metal = material as Metal;

            if (metal != null)
            {
                var cost = Sys.metalMaterials.Find(x => x.id == metal.materialId)?.cost ?? 0;
                var t_cost = Sys.metalTonnages.Find(x => x.id == metal.tonnageId)?.cost ?? 0;
                //volume
                metal.m_volume = Math.Round((metal.m_length + 10) * (metal.m_width + 10) * metal.m_thick,2);
                //m_weigth
                metal.m_weigth = Math.Round(metal.m_volume * 0.00000785,2);
                //loss
                metal.loss = Math.Round(metal.m_weigth * 0.03, 2);

                // 加工成本
                metal.finishedCost1 = Math.Round(metal.m_workStation * metal.qty * t_cost,2);

                //材料成本
                metal.costOfMeterial1 =Math.Round( (metal.m_weigth + metal.loss) * cost * metal.qty,2);

                //total（材料成本 + 加工成本 + CMF）/ 良率
                metal.total1 = Math.Round((metal.costOfMeterial1 + metal.finishedCost1 + metal.CMF1) / (metal.yield / 100),2);

                if (double.IsNaN(metal.total1) || double.IsInfinity(metal.total1))
                    metal.total1 = 0;

                // 加工成本2
                metal.finishedCost2 = Math.Round(metal.finishedCost1/ Sys.ExchangeRate.First().Current,2);

                //材料成本2
                metal.costOfMeterial2 = Math.Round(metal.costOfMeterial1 / Sys.ExchangeRate.First().Current,2);

                //total2
                metal.total2= Math.Round(metal.total1 / Sys.ExchangeRate.First().Current,2);

                if (double.IsNaN(metal.total2) || double.IsInfinity(metal.total2))
                    metal.total2 = 0;
            }
        }

        public new object CreateViewModel(MetalBatch batch  = null)
        {
            return base.CreateViewModel(batch);
        }

        public void DeleteRecord(object dataContext, object material)
        {
            base.DeleteRecord(dataContext, material as Metal);
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
            var config = Sys.Columns.Metal.Where(x => x.Key == column.Header.ToString()).First();
            column.DisplayIndex = config.Value.order;
            column.Header = config.Value.description;
        }

        public new void Save(object dataContext)
        {
            base.Save(dataContext);
        }
    }
}
