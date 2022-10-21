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
        private readonly IZipSource localZipRepository;
        private readonly IZipSource onlineZipRepository;
        private readonly IPingService pingService;

        public MainViewModel(IZipSource localZipRepository, IZipSource onlineZipRepository, IPingService pingService)
        {
            this.localZipRepository = localZipRepository ?? throw new ArgumentNullException(nameof(localZipRepository));
            this.onlineZipRepository = onlineZipRepository ?? throw new ArgumentNullException(nameof(onlineZipRepository));
            this.pingService = pingService ?? throw new ArgumentNullException(nameof(pingService));
            LoadZipsCommand = new RelayCommand(() => LoadZips());
        }

        private void LoadZips()
        {
            string host = "www.github.com";
            bool result = pingService.Ping(host, 3000);

            string[] zips = null;

            if (!string.IsNullOrEmpty(Cityname))
            {
                zips = result ? onlineZipRepository.GetZipsFrom(Cityname) : localZipRepository.GetZipsFrom(Cityname);
            }

            if (zips?.Length > 0)
            {
                ZipSource = zips;
                return;
            }

            ZipSource = new[] { "No results found" };
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