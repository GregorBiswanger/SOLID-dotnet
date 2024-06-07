using System.Collections.ObjectModel;
using PlzSuperTool.Infrastructure.Repositories;

namespace PlzSuperTool.Infrastructure.Features.MainWindow;

public class MainWindowViewModel
{
    public string CityName { get; set; } = "";
    public ObservableCollection<string> Zips { get; set; } = new();

    private readonly IZipRepository _zipRepository;

    public MainWindowViewModel(IZipRepository zipRepository)
    {
        _zipRepository = zipRepository;
    }

    public void SearchZips()
    {
        var zips = _zipRepository.LoadZipsFrom(CityName);
        Zips.Clear();

        if (zips.Length > 0)
        {

            foreach (var zip in zips)
            {
                Zips.Add(zip);
            }
        }
        else
        {
            Zips.Add("No results found");
        }
    }
}