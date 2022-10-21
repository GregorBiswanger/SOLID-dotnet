using System;
using PlzSuperTool.Contracts;
using PlzSuperTool.Implementations;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows;
using PlzSuperTool.ViewModels;

namespace PlzSuperTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ILogger logger = new Logger();
            IZipSource localzipRepository = new LocalZipRepository(logger);
            IZipSource onlinezipRepository = new OnlineZipRepository(logger);
            IPingService githubPingService = new GithubPingService();
            DataContext = new MainViewModel(localzipRepository, onlinezipRepository, githubPingService);
        }
    }
}
