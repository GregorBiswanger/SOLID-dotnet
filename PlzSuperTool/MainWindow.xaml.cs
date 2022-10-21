using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows;
using PlzSuperTool.Infrastructure.Features.MainWindow;

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
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.SearchZips();
        }
    }
}
