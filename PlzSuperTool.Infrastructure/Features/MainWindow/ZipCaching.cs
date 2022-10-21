namespace PlzSuperTool.Infrastructure.Features.MainWindow
{
    public class ZipCaching : IZipRepository
    {
        private readonly Dictionary<string, string[]> _cache = new();
        private readonly IZipRepository _zipRepository;

        public ZipCaching(IZipRepository zipRepository)
        {
            _zipRepository = zipRepository;
        }

        public string[] GetZipsFrom(string cityname)
        {
            using var logger = File.AppendText("log.txt");
            logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname);


            if (_cache.ContainsKey(cityname))
            {
                return _cache[cityname];
            }

            var zips = _zipRepository.GetZipsFrom(cityname);


            _cache.Add(cityname, zips.ToArray());
            logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname + " - " + zips.Length + " results");

            return zips.ToArray();
        }
    }
}
