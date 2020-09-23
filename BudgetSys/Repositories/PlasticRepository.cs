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
using BudgetSys.Models;

namespace BudgetSys.Repositories
{
    class PlasticRepository : IMatetrialRepository
    {
        private readonly string metalBasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Plastic/";

        public object AddNewItem(int id, string batchNo)
        {
            var metal = new Plastic
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
                    templateColumn.ItemsSource = Sys.plasticMaterials;
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
                    templateColumn.ItemsSource = Sys.plasticTonnages;
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
            var plastic = material as Plastic;

            if (plastic != null)
            {
                var cost = Sys.plasticMaterials.Find(x => x.id == plastic.materialId)?.cost ?? 0;
                var t_cost = Sys.plasticTonnages.Find(x => x.id == plastic.tonnageId)?.cost ?? 0;

                PlasticSalivaId salivaId= null;

                foreach (PlasticSalivaId item in Sys.plasticSalivaId)
                {
                    if (item.scope > plastic.p_netWeigth)
                    {
                        salivaId = item;
                        break;
                    }
                }

                //口水
                plastic.p_salivaId = plastic.p_netWeigth * salivaId.percent;
                //毛重量
                plastic.p_grossWeight = plastic.qty *( plastic.p_netWeigth+plastic.p_salivaId);

                //损耗
                plastic.loss = plastic.p_grossWeight * 0.03f;

                // 加工成本 成本公式*周期s/60/模穴*数量
                plastic.finishedCost1 = t_cost*plastic.p_cycle/60 /plastic.p_cavity* plastic.qty ;

                if (float.IsNaN(plastic.finishedCost1) || float.IsInfinity(plastic.finishedCost1))
                    plastic.finishedCost1 = 0;

                //材料成本 单价公式*（毛重量+损耗）/1000*数量
                plastic.costOfMeterial1 = (plastic.p_grossWeight + plastic.loss) /1000 * cost * plastic.qty;

                //ME
                plastic.p_ME1 = plastic.finishedCost1 + plastic.costOfMeterial1;

                //total（ME + CMF）/ 良率
                plastic.total1 = (plastic.p_ME1 + plastic.CMF1) / plastic.yield;

                if (float.IsNaN(plastic.total1) || float.IsInfinity(plastic.total1))
                    plastic.total1 = 0;

                // 加工成本2
                plastic.finishedCost2 = plastic.finishedCost1 * Sys.ExchangeRate.Current;

                //材料成本2
                plastic.costOfMeterial2 = plastic.costOfMeterial1 * Sys.ExchangeRate.Current;

                plastic.p_ME2 = plastic.p_ME1 * Sys.ExchangeRate.Current;

                //total2
                plastic.total2 = plastic.total1 * Sys.ExchangeRate.Current;

                if (float.IsNaN(plastic.total2) || float.IsInfinity(plastic.total2))
                    plastic.total2 = 0;
            }
        }


        public object CreateViewModel(MetalBatch batch = null)
        {
            if (batch == null)
            {
                return new MetalViewModel<Plastic>
                {
                    Batches = GetBatches(),
                    Details = new ObservableCollection<Plastic>(),
                    CurrentBatch = new MetalBatch { batchType = BatchType.Plastic }
                };
            }
            else
            {
                var details = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Plastic>>(File.ReadAllText(metalBasePath + batch.batchNo + ".json"));
                return new MetalViewModel<Plastic>
                {
                    Batches = GetBatches(),
                    Details = details,
                    CurrentBatch = batch
                };
            }
        }

        public void Delete(object dataContext, object material)
        {
            (dataContext as MetalViewModel<Plastic>).Details.Remove(material as Plastic);
        }

        public ObservableCollection<MetalBatch> GetBatches()
        {
            var batches = new DirectoryInfo(metalBasePath).GetFiles("*.json");
            var batchesList = new ObservableCollection<MetalBatch>(batches.Select(x => new MetalBatch
            {
                batchNo = x.Name.Replace(".json", ""),
                batchType = BatchType.Plastic,
            }));

            return batchesList;
        }

        public void RenameColumn(DataGridColumn column)
        {
            var config = Sys.Columns.Plastic.Where(x => x.Key == column.Header.ToString()).First();
            column.DisplayIndex = config.Value.order;
            column.Header = config.Value.description;
        }

        public void Save(object dataContext, MetalBatch currentBatch)
        {
            var viewModel = dataContext as MetalViewModel<Plastic>;
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
                if (currentBatch == null)
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
            viewModel.Batches.Add(currentBatch);
        }
    }
}
