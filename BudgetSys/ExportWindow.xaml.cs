using BudgetSys.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BudgetSys.Repositories;
using Npoi.Mapper;
using System.IO;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace BudgetSys
{
    /// <summary>
    /// ExportExcel.xaml 的交互逻辑
    /// </summary>
    public partial class ExportExcel : Window
    {
        public ExportExcel()
        {
            InitializeComponent();
            this.Loaded += ExportExcel_Loaded;
        }

        private void ExportExcel_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ExportViewModel
            {
                Metals = new System.Collections.ObjectModel.ObservableCollection<ExportFile>(new MetalRepository().GetBatches().Select(x => new ExportFile { fileName = x.batchNo, Include = false })),
                Platics = new System.Collections.ObjectModel.ObservableCollection<ExportFile>(new PlasticRepository().GetBatches().Select(x => new ExportFile { fileName = x.batchNo, Include = false })),
                CostAdjustment = new System.Collections.ObjectModel.ObservableCollection<ExportFile>(new CostAdjustmentRepository().GetBatches().Select(x => new ExportFile { fileName = x.batchNo, Include = false })),
                ProjectCost = new System.Collections.ObjectModel.ObservableCollection<ExportFile>(new ProjectCostRepository().GetBatches().Select(x => new ExportFile { fileName = x.batchNo, Include = false })),
                PurchasedParts = new System.Collections.ObjectModel.ObservableCollection<ExportFile>(new PurchasedPartsRepository().GetBatches().Select(x => new ExportFile { fileName = x.batchNo, Include = false })),
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel 文件| *.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                var evm = this.DataContext as ExportViewModel;

                var mapper = new Mapper();

                mapper.Put<Metal>(evm.Metals, BatchType.Metal.ToString(), Sys.Columns.Metal);
                mapper.Put<Plastic>(evm.Platics, BatchType.Plastic.ToString(), Sys.Columns.Plastic);
                mapper.Put<ProjectCost>(evm.ProjectCost, BatchType.ProjectCost.ToString(), Sys.Columns.ProjectCost);
                mapper.Put<CostAdjustment>(evm.CostAdjustment, BatchType.CostAdjustment.ToString(), Sys.Columns.CostAdjustment);
                mapper.Put<PurchasedParts>(evm.PurchasedParts, BatchType.PurchasedParts.ToString(), Sys.Columns.PurchasedParts);

                try
                {
                    mapper.Save(saveFileDialog.FileName);
                    MessageBox.Show("保存成功 文件路径:" + saveFileDialog.FileName, "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败，原因：" + ex.Message, "提示");
                }

            }
        }

        private void btnColse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public static class MapExtensions
    {
        public static void Put<T>(this Mapper mapper, ObservableCollection<ExportFile> files, string materialType, Dictionary<string, ColumnProp> colsJson)
        {
            foreach (var col in colsJson)
            {
                mapper.Map<T>(col.Value.description, col.Key);
            }

            foreach (ExportFile item in files.Where(x => x.Include == true))
            {
                var batch = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Batches/" + materialType + "/" + item.fileName + ".json";
                var metals = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(batch));
                mapper.Put<T>(metals, materialType + "-" + item.fileName, true, columns =>
                {
                    foreach (var col in colsJson)
                    {
                        try
                        {
                            var kp = columns.Where(x => x.Key.Name == col.Key && x.Key.ReflectedType == typeof(T)).First();
                            if (col.Value.saveOrder < 0)
                            {
                                var ignoreAttribute = new Npoi.Mapper.Attributes.ColumnAttribute
                                {
                                    Ignored = true
                                };
                                ignoreAttribute.Property = kp.Key;
                                ignoreAttribute.MergeTo(columns);
                                kp.Value.Index = -1;
                            }
                            else
                            {
                                kp.Value.Index = col.Value.saveOrder;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }
                });
            }
        }
    }
}
