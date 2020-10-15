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
using BudgetSys.Repositories;

namespace BudgetSys
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IniData data;

        private MaterialBase editMetal;

        private IMatetrialRepository matetrialRepository;

        public MainWindow()
        {
            this.Loaded += MainWindow_Loaded;
            FileIniDataParser parser = new FileIniDataParser();
            data = parser.ReadFile("Config.ini", Encoding.UTF8);
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.materialType.ItemsSource = Enum.GetValues(typeof(BatchType)).Cast<BatchType>();
            this.materialType.SelectedIndex = 0;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            matetrialRepository.AutoGenColumn(e);
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            editMetal = e.Row.Item as MaterialBase;
            //editMetal.EndEdit();
            try
            {
                var content = (e.EditingElement as TextBox).Text;
                matetrialRepository.Calculate(editMetal, e.Column, content);
            }
            catch (Exception ex )
            {
                 //log
            }
        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = matetrialRepository.AddNewItem(this.DataContext);
        }

        /// <summary>
        /// 保存批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                matetrialRepository.Save(this.DataContext);
                MessageBox.Show("保存成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败，原因：" + ex.Message, "提示");
            }
        }

        /// <summary>
        /// 加载批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var batch = (sender as ListView).SelectedItem as MetalBatch;

            this.DataContext = matetrialRepository.CreateViewModel(batch);
        }

        /// <summary>
        /// 重计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //var metal = editMetal;
            //matetrialRepository.Calculate(metal);
        }

        private void DgDetail_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            editMetal?.EndEdit();

            //this.dgDetail.CommitEdit(DataGridEditingUnit.Row, true);
        }

        /// <summary>
        /// 新建批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = matetrialRepository.CreateViewModel(new MetalBatch {  batchNo = "default"});
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            matetrialRepository.DeleteRecord(this.dgDetail.DataContext, this.dgDetail.SelectedItem);
        }

        /// <summary>
        /// 删除批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBatchDelete_Click(object sender, RoutedEventArgs e)
        {
            var batch = lvBatch.SelectedItem as MetalBatch;
            try
            {
                matetrialRepository.DeleteBatch(this.DataContext, batch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败，原因：" + ex.Message, "提示");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((BatchType)((e.Source as ComboBox).SelectedItem))
            {
                case BatchType.Metal:
                    matetrialRepository = new MetalRepository();
                    break;
                case BatchType.Plastic:
                    matetrialRepository = new PlasticRepository();
                    break;
                case BatchType.PurchasedParts:
                    matetrialRepository = new PurchasedPartsRepository();
                    break;
                case BatchType.Accessories:
                    matetrialRepository = new AccessoriesRepository();
                    break;
                case BatchType.ProjectCost:
                    matetrialRepository = new ProjectCostRepository();
                    break;
                case BatchType.CostAdjustment:
                    matetrialRepository = new CostAdjustmentRepository();
                    break;
                default:
                    break;
            }
            this.DataContext = matetrialRepository.CreateViewModel();
            editMetal = null;
        }

        private void DgDetail_AutoGeneratedColumns(object sender, EventArgs e)
        {
            try
            {
                matetrialRepository.RenameColumn(dgDetail.Columns);
            }
            catch (Exception)
            {
               
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = (e.Source as TextBox).Text;
            foreach (MetalBatch item in lvBatch.Items)
            {
                if (item.batchNo.Contains(search))
                {
                    lvBatch.SelectedItem = item;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MetalViewModel<Metal>).CurrentBatch = new MetalBatch { batchNo = "11", batchType = BatchType.Metal };
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var masterForm = new SaveWindow();

            switch ((e.Source as MenuItem).Header.ToString())
            {
                case "Metal-Meterials":
                    masterForm.LoadSource<MetalMaterial>();
                    break;
                case "Metal-Tonnages":
                    masterForm.LoadSource<MetalTonnage>();
                    break;
                case "Plastic-Meterials":
                    masterForm.LoadSource<PlasticMaterial>();
                    break;
                case "Plastic-Tonnages":
                    masterForm.LoadSource<PlasticTonnage>();
                    break;
                case "Plastic-SalivaId":
                    masterForm.LoadSource<PlasticSalivaId>();
                    break;
                case "ExchangeRate":
                    masterForm.LoadSource<ExchangeRate>();
                    break;
                default:
                    break;
            }
            masterForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            masterForm.ShowDialog();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var eForm = new ExportExcel();
            eForm.ShowDialog();
        }

        private void DgDetail_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
    }
}
