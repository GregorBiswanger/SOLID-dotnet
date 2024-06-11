using System.Diagnostics.CodeAnalysis;
using System.Windows;
using PlzSuperTool.Infrastructure.Features.MainWindow;

namespace PlzSuperTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SuppressMessage("NDepend", "ND2014:AvoidNamingTypesAndNamespacesWithTheSameIdentifier", Justification="TODO")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           var mainWindowViewModel = (MainWindowViewModel) DataContext;
           mainWindowViewModel.SearchZips();
        }
    }
}
