using PlzSuperTool.Infrastructure.Features.MainWindow;

namespace PlzSuperTool.Infrastructure.Repositories;

public class ZipCaching : IZipRepository
{
    private readonly Dictionary<string, string[]> _cache = new();
    private readonly IZipRepository _zipRepository;

    public ZipCaching(IZipRepository zipRepository)
    {
        _zipRepository = zipRepository;
    }

    public string[] LoadZipsFrom(string cityName)
    {
        if (_cache.ContainsKey(cityName))
        {
            return _cache[cityName];
        }

        var zips = _zipRepository.LoadZipsFrom(cityName);
        _cache.Add(cityName, zips);

        return zips;
    }
}