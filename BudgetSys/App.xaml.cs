using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetSys
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen s = new SplashScreen("/Images/Startup.png");
            s.Show(false);
            System.Threading.Thread.Sleep(2000);
            s.Close(new TimeSpan(0, 0, 3));
            base.OnStartup(e);
        }
    }
}
