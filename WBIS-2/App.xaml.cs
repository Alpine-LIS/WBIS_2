using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core;

namespace WBIS_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationThemeHelper.ApplicationThemeName = "Office2019Colorful";
            //ApplicationThemeHelper.ApplicationThemeName = "Office2019Black";
            ApplicationThemeHelper.UpdateApplicationThemeName();

            base.OnStartup(e);
            new Bootstrapper().Run();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //   ExceptionWindow window = new ExceptionWindow(e);
            //   window.ShowDialog();
            //   e.Handled = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
