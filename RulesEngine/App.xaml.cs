using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RulesEngine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

        public App()
        { }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _bootstrapper = this.Resources["Locator"] as Bootstrapper;

            MainWindow = new MainWindow();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrapper.Dispose();

            base.OnExit(e);
        }
    }
}
