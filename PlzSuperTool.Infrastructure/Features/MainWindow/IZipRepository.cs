namespace PlzSuperTool.Infrastructure.Features.MainWindow;

public interface IZipRepository
{
    string[] GetZipsFrom(bool online, string cityname);
    string[] GetStreetsFromZip(string zip);
    string[] GetAllCities();
    string[] GetAllStates();
}