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
    public class MetalRepository : IMatetrialRepository
    {
        private readonly string metalBasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Metal/";

        public object AddNewItem(int id,string batchNo)
        {
            var metal = new Metal
            {
                id = id,
                batchNo = batchNo
            };
            return metal;
        }

        public void AutoGenColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "batchNo")
            {
                e.Cancel = true;
                return;
            }
            else
            {
                if (e.PropertyName == "materialId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = Sys.metalMaterials;
                    templateColumn.DisplayMemberPath = "name";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("materialId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                }
                else if (e.PropertyName == "tonnageId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = Sys.metalTonnages;
                    templateColumn.DisplayMemberPath = "tonnage";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("tonnageId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                }
                e.Column.Header = e.PropertyName;
                //e.Column.Header = data["Metal"][e.PropertyName];
            }
        }

        public void Calculate(RawMaterial material)
        {
            var metal = material as Metal;

            if (metal != null)
            {
                var cost = Sys.metalMaterials.Find(x => x.id == metal.materialId)?.cost ?? 0;
                var t_cost = Sys.metalTonnages.Find(x => x.id == metal.tonnageId)?.cost ?? 0;
                //volume
                metal.m_volume = (metal.m_length + 10) * (metal.m_width + 10) * metal.m_thick;
                //loss
                metal.loss = metal.m_volume * 0.00000785f;

                // 加工成本
                metal.finishedCost1 = metal.m_workStation * metal.qty * t_cost;

                //材料成本
                metal.costOfMeterial1 = (metal.m_weigth + metal.loss) * cost * metal.qty;

                //total（材料成本 + 加工成本 + CMF）/ 良率
                metal.total1 = (metal.costOfMeterial1 + metal.finishedCost1 + metal.CMF1) / (metal.yield / 100);

                if (float.IsNaN(metal.total1) || float.IsInfinity(metal.total1))
                    metal.total1 = 0;

                // 加工成本2
                metal.finishedCost2 = metal.finishedCost1* Sys.ExchangeRate.Current;

                //材料成本2
                metal.costOfMeterial2 = metal.costOfMeterial1 * Sys.ExchangeRate.Current;

                //total2
                metal.total2= metal.total1 * Sys.ExchangeRate.Current;

                if (float.IsNaN(metal.total2) || float.IsInfinity(metal.total2))
                    metal.total2 = 0;
            }
        }

        public object CreateViewModel(MetalBatch batch  = null)
        {
            if (batch == null)
            {
                return new MetalViewModel<Metal>
                {
                    Batches = GetBatches(),
                    Details = new ObservableCollection<Metal>(),
                    CurrentBatch = new MetalBatch { batchType = BatchType.Metal }
                };
            }
            else
            {
                var details = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Metal>>(File.ReadAllText(metalBasePath + batch.batchNo + ".json"));
                return new MetalViewModel<Metal>
                {
                    Batches = GetBatches(),
                    Details = details,
                    CurrentBatch = batch
                };
            }

        }

        public void Delete(object dataContext, object material)
        {
            (dataContext as MetalViewModel<Metal>).Details.Remove(material as Metal);
        }

        public ObservableCollection<MetalBatch> GetBatches()
        {
            var batches = new DirectoryInfo(metalBasePath).GetFiles("*.json");
            var batchesList = new ObservableCollection<MetalBatch>(batches.Select(x => new MetalBatch
            {
                batchNo = x.Name.Replace(".json", ""),
                batchType = BatchType.Metal,
            }));

            return batchesList;
        }

        public void RenameColumn(DataGridColumn column)
        {
            var config = Sys.Columns.Metal.Where(x => x.Key == column.Header.ToString()).First();
            column.DisplayIndex = config.Value.order;
            column.Header = config.Value.description;
        }

        public void Save(object dataContext, MetalBatch currentBatch)
        {
            var viewModel = dataContext as MetalViewModel<Metal>;
            var metals = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel.Details);
            if (currentBatch == null)
            {
                currentBatch = new MetalBatch
                {
                    batchNo = string.Empty
                };
                var saveWindow = new SaveWindow(currentBatch);
                saveWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                saveWindow.ShowDialog();
                if (currentBatch.batchNo == "Cancel")
                {
                    return;
                }
                if (currentBatch.batchNo.Equals(string.Empty))
                {
                    MessageBox.Show("保存失败，请输入文件名", "提示");
                    return;
                }
                if (File.Exists(metalBasePath + currentBatch.batchNo + ".json"))
                {
                    if (MessageBox.Show($"批次 {currentBatch.batchNo} 已存在，是否覆盖？", "保存", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        File.WriteAllText(metalBasePath + currentBatch.batchNo + ".json", metals);
                        MessageBox.Show("保存成功", "提示");
                    }
                }
                else
                {
                    File.WriteAllText(metalBasePath + currentBatch.batchNo + ".json", metals);
                    MessageBox.Show("保存成功", "提示");

                }
            }
            else
            {
                File.WriteAllText(metalBasePath + currentBatch.batchNo + ".json", metals);
                MessageBox.Show("保存成功", "提示");
            }
        }
    }
}
