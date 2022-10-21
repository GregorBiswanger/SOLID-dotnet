namespace PlzSuperTool.Infrastructure.Features.MainWindow;

public interface IZipRepository
{
    string[] GetZipsFrom(string cityname);
}