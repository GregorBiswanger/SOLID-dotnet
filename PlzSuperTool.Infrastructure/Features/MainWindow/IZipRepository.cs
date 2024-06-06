namespace PlzSuperTool.Infrastructure.Features.MainWindow;

public interface IZipRepository
{
    string[] GetZipsFrom(bool online, string cityname);
}