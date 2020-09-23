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
using BudgetSys.Models;

namespace BudgetSys
{
    /// <summary>
    /// SaveWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SaveWindow : Window
    {
        private MetalBatch _batch;
        public SaveWindow(MetalBatch batch)
        {
            InitializeComponent();
            this.DataContext = batch;
            _batch = batch;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _batch.batchNo = "Cancel";
            this.Close();
        }
    }
}
