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
        IZipRepository zipRepository = new ZipRepository();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string host = "www.github.com";
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                {
                    result = true;
                }
            }
            catch { }

            var zips = zipRepository.GetZipsFrom(result, CityNameTextBox.Text);
            
            if (zips.Length > 0)
            {
                ZipsResultListBox.ItemsSource = new List<string>(zips);
            }
            else
            {
                ZipsResultListBox.ItemsSource = new []{"No results found"};
            }
        }
    }
}
