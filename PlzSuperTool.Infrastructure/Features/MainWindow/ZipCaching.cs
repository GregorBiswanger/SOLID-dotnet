using PlzSuperTool.Infrastructure.Logging;

namespace PlzSuperTool.Infrastructure.Features.MainWindow
{
    public class ZipCaching : IZipRepository
    {
        private readonly Dictionary<string, string[]> _cache = new();
        private readonly IZipRepository _zipRepository;
        private readonly ILogger _logger;

        public ZipCaching(IZipRepository zipRepository, ILogger logger)
        {
            _zipRepository = zipRepository;
            _logger = logger;
        }

        public string[] GetZipsFrom(string cityname)
        {
            _logger.Write("GetZipsFrom: " + cityname);

            if (_cache.ContainsKey(cityname))
            {
                return _cache[cityname];
            }

            var zips = _zipRepository.GetZipsFrom(cityname);
            _cache.Add(cityname, zips.ToArray());

            _logger.Write("GetZipsFrom: " + cityname + " - " + zips.Length + " results");

            return zips.ToArray();
        }
    }
}
