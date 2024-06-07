namespace PlzSuperTool.Infrastructure.Features.MainWindow;

public interface IZipRepository
{
    string[] LoadZipsFrom(string cityName);
}