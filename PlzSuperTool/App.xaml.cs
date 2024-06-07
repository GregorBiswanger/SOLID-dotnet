using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PlzSuperTool.Infrastructure.Features.MainWindow;
using PlzSuperTool.Infrastructure.Repositories;

namespace PlzSuperTool;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<MainWindowViewModel>()
            .AddTransient<IZipRepository, ZipWebRepository>()
            .AddTransient<IZipsFileReader, ZipsFileReader>()
            .AddTransient<IZipsWebReader, ZipsWebReader>();
            
        _serviceProvider = serviceCollection.BuildServiceProvider(true);

        Current.MainWindow = new MainWindow();
        Current.MainWindow.DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        Current.MainWindow.Show();
    }
}