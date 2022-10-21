using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlzSuperTool.Contracts;

namespace PlzSuperTool.ViewModels
{
    public sealed class MainViewModel : ObservableObject
    {
        private readonly IZipSource zipRepository;
        private readonly IPingService githubPingService;

        public MainViewModel(IZipSource zipRepository, IPingService githubPingService)
        {
            this.zipRepository = zipRepository ?? throw new ArgumentNullException(nameof(zipRepository));
            this.githubPingService = githubPingService ?? throw new ArgumentNullException(nameof(githubPingService));
            LoadZipsCommand = new RelayCommand(() => LoadZips());
        }

        private void LoadZips()
        {
            string host = "www.github.com";
            bool result = githubPingService.Ping(host, 3000);

            string[] zips = null;

            if (!string.IsNullOrEmpty(Cityname))
                zips = zipRepository.GetZipsFrom(result, Cityname);

            if (zips?.Length > 0)
            {
                ZipSource = zips;
            }
            else
            {
                ZipSource = new[] { "No results found" };
            }
        }

        public string Cityname { get; set; } = string.Empty;

        public IRelayCommand LoadZipsCommand { get; }

        public IEnumerable<string> ZipSource
        {
            get => zipSource;
            set => SetProperty(ref zipSource, value);
        }

        private IEnumerable<string> zipSource = Array.Empty<string>();
    }
}
