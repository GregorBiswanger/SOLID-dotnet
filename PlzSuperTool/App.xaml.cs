using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PlzSuperTool.Infrastructure.Features.MainWindow;

namespace PlzSuperTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serivceCollection = new ServiceCollection();
            serivceCollection.AddTransient<MainWindowViewModel>();

            _serviceProvider = serivceCollection.BuildServiceProvider(true);

            Current.MainWindow = new MainWindow();
            Current.MainWindow.DataContext = _serviceProvider.GetService<MainWindowViewModel>();
            Current.MainWindow.Show();
        }
    }
}
