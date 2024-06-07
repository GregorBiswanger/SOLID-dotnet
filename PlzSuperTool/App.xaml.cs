using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PlzSuperTool.Infrastructure.Features.MainWindow;
using PlzSuperTool.Infrastructure.Repositories;
using PlzSuperTool.Infrastructure.Services;

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
            .AddTransient<IZipsFileReader, ZipsFileReader>()
            .AddTransient<IZipsWebReader, ZipsWebReader>()
            .AddTransient<ZipFileRepository>()
            .AddTransient<ZipWebRepository>()
            .AddTransient<NetworkService>()
            .AddSingleton<IZipRepository>(serviceProvider =>
            {
                var networkService = serviceProvider.GetRequiredService<NetworkService>();

                if (networkService.IsOnline())
                {
                    return new ZipCaching(serviceProvider.GetRequiredService<ZipWebRepository>());
                }

                return new ZipCaching(serviceProvider.GetRequiredService<ZipFileRepository>());
            });

        _serviceProvider = serviceCollection.BuildServiceProvider(true);

        Current.MainWindow = new MainWindow();
        Current.MainWindow.DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        Current.MainWindow.Show();
    }
}