using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

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

        var zips = _zipRepository.GetZipsFrom(result, CityName);
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