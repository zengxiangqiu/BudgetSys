using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BudgetSys.Models;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;

namespace BudgetSys
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IniData data;

        MetalBatch currentBatch;

        private readonly string metalBasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Metal/";

        public MainWindow()
        {

            //File.WriteAllText("tonnages.json", Newtonsoft.Json.JsonConvert.SerializeObject(metalTonnages), Encoding.UTF8);
            //File.WriteAllText("Materials.json", Newtonsoft.Json.JsonConvert.SerializeObject(metalMaterials), Encoding.UTF8);

            FileIniDataParser parser = new FileIniDataParser();
            data = parser.ReadFile("Config.ini", Encoding.UTF8);
            InitializeComponent();
            this.DataContext = new MetalViewModel
            {
                //Details = Moq(),
                Batches = MoqBatch()
            };
        }

        private ObservableCollection<Metal> Moq()
        {
            var metals = new ObservableCollection<Metal>();
            var metal = new Metal
            {
                id = 1,
                m_materialId = 0,
                m_name = "main-frame",
                m_weigth = 1.728f,
                m_loss = 1.728f * 0.03f,
                m_qty = 1
            };
            metals.Add(metal);
            return metals;
        }

        private ObservableCollection<MetalBatch> MoqBatch()
        {
            /*
            var batches = new ObservableCollection<MetalBatch>();
            var batch = new MetalBatch
            {
                batchNo = 1,
                batchType = BatchType.Metal,
                id = 0
                 
             
            };
            batches.Add(batch);
            */

            //Read from files 

            var batches = new DirectoryInfo(metalBasePath).GetFiles("*.json");
            var batchesList = new ObservableCollection<MetalBatch>(batches.Select(x => new MetalBatch
            {
                batchNo =x.Name.Replace(".json", ""),
                batchType = BatchType.Metal,
                //id = Convert.ToInt32(x.Name.Replace(".json", ""))
            }));

            return batchesList;
        }

        List<MetalMaterial> metalMaterials = new List<MetalMaterial> {
                        new MetalMaterial { id = 0, name = "SECC", cost = 6.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 1, name = "SUS", cost = 31.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 2, name = "SPTE", cost = 9.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 3, name = "NETTING", cost = 21.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 4, name = "Al5052", cost = 30.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 5, name = "Al1050", cost = 25.0f, description = "（GI/SGCC)" }
                    };

        List<MetalTonnage> metalTonnages = new List<MetalTonnage>
        {
            new MetalTonnage{  id =0, tonnage = "16T", cost = 0.1f,  size = "280*450"} ,
            new MetalTonnage{  id =0, tonnage ="63T",cost=  0.25f,size="450*700"},
            new MetalTonnage{  id =1, tonnage ="80T",cost=  0.30f,size="490*760"},
            new MetalTonnage{  id =2, tonnage ="100T",cost=0.35f,size="550*830"},
            new MetalTonnage{  id =3, tonnage ="125T",cost=0.40f,size="620*940"},
            new MetalTonnage{  id =4, tonnage ="160T",cost=0.45f,size="720*1150"},
            new MetalTonnage{  id =5, tonnage ="200T",cost=0.50f,size="780*1250"},
            new MetalTonnage{  id =6, tonnage ="250T",cost=0.55f,size="820*1220"},
            new MetalTonnage{  id =7, tonnage ="315T",cost=1.15f,size="880*1280"},
            new MetalTonnage{  id =8, tonnage ="400T",cost=1.45f,size="900*1400"},
            new MetalTonnage{  id =9, tonnage ="500T",cost=1.85f,size="1000*1500"},
            new MetalTonnage{  id =10, tonnage ="630T",cost=2.30f,size="1100*1450"},
            new MetalTonnage{  id =11, tonnage ="800T",cost=2.80f,size="1600*1600"},
            new MetalTonnage{  id =12, tonnage ="1000T",cost=3.35f,size="1600*1800"},
            new MetalTonnage{  id =13, tonnage ="1250T",cost=3.95f,size="1600*1800"},
            new MetalTonnage{  id =14, tonnage ="1600T",cost=4.50f,size="1750*1900"}
        };

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "batchNo")
                e.Cancel = true;
            else
            {
                if (e.PropertyName == "m_materialId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = metalMaterials;
                    templateColumn.DisplayMemberPath = "name";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("m_materialId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                    e.Column.Header = data["Metal"]["m_materialId"];
                }
                else if (e.PropertyName == "m_tonnageId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = metalTonnages;
                    templateColumn.DisplayMemberPath = "tonnage";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("m_tonnageId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                    e.Column.Header = data["Metal"]["m_tonnageId"];
                }
                else
                {
                    e.Column.Header = data["Metal"][e.Column.Header.ToString()];
                }

            }
        }
        private Metal editMetal;

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            editMetal = e.Row.Item as Metal;
            editMetal.EndEdit();
        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var details = (sender as DataGrid).ItemsSource as ObservableCollection<Metal>;
            var batch = details.FirstOrDefault()?.batchNo ;
            var metal = new Metal
            {
                id = details.Count + 1,
                batchNo= batch
            };
            e.NewItem = metal;
            //metal.id = ((sender as DataGrid).ItemsSource as ObservableCollection<Metal>).Count + 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MetalViewModel;
            var metals = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel.Details);
            if (currentBatch == null)
            {
                currentBatch = new MetalBatch
                {
                    batchNo = string.Empty
                };
                var saveWindow = new SaveWindow(currentBatch);
                saveWindow.ShowDialog();
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
               lvBatch.ItemsSource = MoqBatch();
            }
            else
            {
                File.WriteAllText(metalBasePath + currentBatch.batchNo + ".json", metals);
                MessageBox.Show("保存成功","提示");
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var batch = (sender as ListView).SelectedItem as MetalBatch;

            var details = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Metal>>(File.ReadAllText(metalBasePath+batch.batchNo + ".json"));

            this.DataContext = null;
            this.DataContext = new MetalViewModel
            {
                Batches = MoqBatch(),
                Details = details
            };

            currentBatch = batch;
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            var metal = editMetal;
            if (metal != null)
            {
                var cost = metalMaterials.Find(x => x.id == metal.m_materialId)?.cost ?? 0;
                var t_cost = metalTonnages.Find(x => x.id == metal.m_tonnageId)?.cost ?? 0;
                //volume
                metal.m_volume = (metal.m_length + 10) * (metal.m_width + 10) * metal.m_thick;
                //loss
                metal.m_loss = metal.m_volume * 0.00000785f;

                // 加工成本
                metal.m_finishedCost1 = metal.m_workStation * metal.m_qty * t_cost;

                //材料成本
                metal.m_costOfMeterial1 = (metal.m_weigth + metal.m_loss) * cost * metal.m_qty;

                //total（材料成本 + 加工成本 + CMF）/ 良率
                metal.m_total1 = (metal.m_costOfMeterial1 + metal.m_finishedCost1 + metal.m_CMF1) / (metal.m_yield/100) ;

                if (float.IsNaN(metal.m_total1))
                    metal.m_total1 = 0;

            }
        }

        private void DgDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
           
            //this.dgDetail.CommitEdit(DataGridEditingUnit.Row, true);
        }

        

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MetalViewModel
            {
                Batches = MoqBatch(),
                Details = new ObservableCollection<Metal>()
            };
            currentBatch = null;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            (this.dgDetail.DataContext as MetalViewModel).Details.Remove(this.dgDetail.SelectedItem as   Metal);
        }

        private void BtnBatchDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
