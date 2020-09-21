using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BudgetSys
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IniData data;

        public MainWindow()
        {
            FileIniDataParser parser = new FileIniDataParser();
            data= parser.ReadFile("Config.ini",Encoding.UTF8);
            InitializeComponent();
            this.DataContext = new MetalViewModel {
                Details = Moq(),
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
                 m_loss = 1.728f*0.03f,
                 m_qty =1
            };
            metals.Add(metal);
            return metals;
        }

        private ObservableCollection<MetalBatch> MoqBatch()
        {
            var batches = new ObservableCollection<MetalBatch>();
            var batch = new MetalBatch
            {
                batchNo = 1,
                batchType = BatchType.Metal,
                id = 0
                 
             
            };
            batches.Add(batch);
            return batches;
        }

        List<MetalMaterial> metalMaterials =   new List<MetalMaterial> {
                        new MetalMaterial { id = 0, name = "SECC", cost = 6.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 1, name = "SUS", cost = 31.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 2, name = "SPTE", cost = 9.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 3, name = "NETTING", cost = 21.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 4, name = "Al5052", cost = 30.0f, description = "（GI/SGCC)" },
                        new MetalMaterial { id = 5, name = "Al1050", cost = 25.0f, description = "（GI/SGCC)" }
                    };


private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "batchId")
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
                    e.Column.Header = "材料";
                }
                else
                {
                    e.Column.Header = data["Metal"][e.Column.Header.ToString()];
                }

            }
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //reculcate
            
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var metal = e.Row.Item as Metal;
            var cost = metalMaterials.Find(x => x.id == metal.m_materialId)?.cost??0;
            metal.m_costOfMeterial1 = (metal.m_weigth+metal.m_loss)*cost* metal.m_qty;

        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var metal = e.NewItem as Metal;

            metal.id = ((sender as DataGrid).ItemsSource as ObservableCollection<Metal>).Count + 1;
                 
        }
    }
}
