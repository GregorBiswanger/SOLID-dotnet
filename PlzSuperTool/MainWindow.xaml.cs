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
        private readonly ILogger logger;
        public MainWindow()
        {
            InitializeComponent();
            logger = new Logger();
            IZipSource localzipRepository = new LocalZipRepository(logger);
            IZipSource onlinezipRepository = new OnlineZipRepository(logger);
            IPingService githubPingService = new GithubPingService();
            DataContext = new MainViewModel(localzipRepository, onlinezipRepository, githubPingService);
        }

        protected override void OnClosed(EventArgs e)
        {
            logger.Dispose();
            base.OnClosed(e);
        }
    }
}
