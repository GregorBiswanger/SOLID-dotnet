namespace PlzSuperTool;

internal interface IZipRepository
{
    string[] GetZipsFrom(bool online, string cityname);
    string[] GetStreetsFromZip(string zip);
    string[] GetAllCities();
    string[] GetAllStates();
}