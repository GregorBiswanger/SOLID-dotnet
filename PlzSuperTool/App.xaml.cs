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

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<MainWindowViewModel>()
                .AddTransient<ZipRepositoryFactory>()
                .AddTransient<ZipFileRepository>()
                .AddTransient<ZipWebRepository>()
                .AddSingleton<IZipRepository>(serviceProvider =>
                {
                    var zipRepositoryFactory = serviceProvider.GetService(typeof(ZipRepositoryFactory)) as ZipRepositoryFactory;
                    var zipRepository = zipRepositoryFactory.Create();

                    return new ZipCaching(zipRepository);
                });

            _serviceProvider = serviceCollection.BuildServiceProvider(true);

            Current.MainWindow = new MainWindow();
            Current.MainWindow.DataContext = _serviceProvider.GetService<MainWindowViewModel>();
            Current.MainWindow.Show();
        }
    }
}
