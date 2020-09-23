using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BudgetSys.Models;

namespace BudgetSys.Converter
{
    [ValueConversion(typeof(MetalBatch), typeof(String))]
    public class BatchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MetalBatch batch = (MetalBatch)value;
            if (batch != null)
                return $"表格录入  {batch.batchType}   批次-{batch.batchNo}  汇率-{Sys.ExchangeRate.Current}";
            else
                return "表格录入";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
