namespace PlzSuperTool;

internal interface IZipRepository
{
    string[] GetZipsFrom(bool online, string cityname);
}