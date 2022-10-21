using System.Text.Json;
using System.Web;

namespace PlzSuperTool.Infrastructure.Features.MainWindow
{
    public class ZipRepository : IZipRepository
    {
        private readonly Dictionary<string, string[]> cache = new Dictionary<string, string[]>();

        public string[] GetZipsFrom(string cityname)
        {
            using var logger = File.AppendText("log.txt");
            logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname);

            var zips = new List<string>();

            if (cache.ContainsKey(cityname))
            {
                return cache[cityname];
            }

            cache.Add(cityname, zips.ToArray());
            logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname + " - " + zips.Count + " results");

            return zips.ToArray();
        }
    }

    
}
