using System.Collections.ObjectModel;

namespace PlzSuperTool.Infrastructure.Features.MainWindow
{
    public class MainWindowViewModel
    {
        public string Cityname { get; set; } = string.Empty;

        public ObservableCollection<string> Zips { get; set; } = new();
        
        private readonly IZipRepository _zipRepository;

        public MainWindowViewModel(IZipRepository zipRepository)
        {
            _zipRepository = zipRepository;
        }

        public void SearchZips()
        {
            var zips = _zipRepository.GetZipsFrom(Cityname);
            
            Zips.Clear();
            
            if (zips.Length > 0)
            {
                zips.ToList().ForEach(Zips.Add);
            }
            else
            {
                Zips.Add("No results found");
            }
        }
    }
}
