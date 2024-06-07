namespace PlzSuperTool.Infrastructure.Repositories;

public interface IZipRepository
{
    string[] LoadZipsFrom(string cityName);
}