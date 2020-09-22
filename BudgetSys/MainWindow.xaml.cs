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
        List<MetalMaterial> metalMaterials;
        List<MetalTonnage> metalTonnages;
        private readonly string metalBasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/Metal/";
        private readonly string configPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Config/";

        public MainWindow()
        {

            //File.WriteAllText("tonnages.json", Newtonsoft.Json.JsonConvert.SerializeObject(metalTonnages), Encoding.UTF8);
            //File.WriteAllText("Materials.json", Newtonsoft.Json.JsonConvert.SerializeObject(metalMaterials), Encoding.UTF8);

            this.Loaded += MainWindow_Loaded;
            FileIniDataParser parser = new FileIniDataParser();
            data = parser.ReadFile("Config.ini", Encoding.UTF8);
            InitializeComponent();
            this.DataContext = new MetalViewModel<Metal>
            {
                //Details = Moq(),
                Batches = MoqBatch()
            };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //meterials
            metalMaterials = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MetalMaterial>>(File.ReadAllText(configPath + "Materials.json"));

            //Tonnages
            metalTonnages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MetalTonnage>>(File.ReadAllText(configPath + "Tonnages.json"));
        }

        private ObservableCollection<RawMaterial> Moq()
        {
            var metals = new ObservableCollection<RawMaterial>();
            var metal = new Metal
            {
                id = 1,
                materialId = 0,
                name = "main-frame",
                m_weigth = 1.728f,
                loss = 1.728f * 0.03f,
                qty = 1
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

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "batchNo")
                e.Cancel = true;
            else
            {
                if (e.PropertyName == "materialId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = metalMaterials;
                    templateColumn.DisplayMemberPath = "name";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("materialId");
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    templateColumn.SelectedValueBinding = binding;
                    e.Column = templateColumn;
                    e.Column.Header = data["Metal"]["m_materialId"];
                }
                else if (e.PropertyName == "tonnageId")
                {
                    var templateColumn = new DataGridComboBoxColumn();
                    templateColumn.ItemsSource = metalTonnages;
                    templateColumn.DisplayMemberPath = "tonnage";
                    templateColumn.SelectedValuePath = "id";
                    var binding = new Binding("tonnageId");
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
            //editMetal.EndEdit();
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
            var viewModel = this.DataContext as MetalViewModel<Metal>;
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
            this.DataContext = new MetalViewModel<Metal>
            {
                Batches = MoqBatch(),
                Details = new ObservableCollection<Metal>()
            };

            currentBatch = batch;
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            var metal = editMetal;
            if (metal != null)
            {
                var cost = metalMaterials.Find(x => x.id == metal.materialId)?.cost ?? 0;
                var t_cost = metalTonnages.Find(x => x.id == metal.tonnageId)?.cost ?? 0;
                //volume
                metal.m_volume = (metal.m_length + 10) * (metal.m_width + 10) * metal.m_thick;
                //loss
                metal.loss = metal.m_volume * 0.00000785f;

                // 加工成本
                metal.finishedCost1 = metal.m_workStation * metal.qty * t_cost;

                //材料成本
                metal.costOfMeterial1 = (metal.m_weigth + metal.loss) * cost * metal.qty;

                //total（材料成本 + 加工成本 + CMF）/ 良率
                metal.total1 = (metal.costOfMeterial1 + metal.finishedCost1 + metal.CMF1) / (metal.yield/100) ;

                if (float.IsNaN(metal.total1))
                    metal.total1 = 0;

            }
        }

        private void DgDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
           
            //this.dgDetail.CommitEdit(DataGridEditingUnit.Row, true);
        }

        

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MetalViewModel<Metal>
            {
                Batches = MoqBatch(),
                Details = new ObservableCollection<Metal>()
            };
            currentBatch = null;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            (this.dgDetail.DataContext as MetalViewModel<Metal>).Details.Remove(this.dgDetail.SelectedItem as   Metal);
        }

        private void BtnBatchDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
