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
            IZipSource zipRepository = new ZipRepository();
            IPingService githubPingService = new GithubPingService();
            DataContext = new MainViewModel(zipRepository, githubPingService);
        }
    }
}
