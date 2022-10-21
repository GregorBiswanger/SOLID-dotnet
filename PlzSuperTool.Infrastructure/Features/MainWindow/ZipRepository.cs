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


            string citynameUrlEncoded = HttpUtility.UrlEncode(cityname);
            const string baseUrl = "https://public.opendatasoft.com/api/records/1.0/search/";
            string query = $"?dataset=georef-germany-postleitzahl&q={citynameUrlEncoded}&facet=plz_name&facet=lan_name&facet=lan_code&rows=9999";

            var httpClient = new HttpClient();
            var stream = httpClient.GetStreamAsync(baseUrl + query).Result;

            var plzResponse = JsonSerializer.DeserializeAsync<PlzResponse>(stream).Result;

            plzResponse.records.ToList().ForEach(record =>
            {
                zips.Add(record.fields.plz_code);
            });

            cache.Add(cityname, zips.ToArray());
            logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname + " - " + zips.Count + " results");

            return zips.ToArray();
        }

        public string[] GetStreetsFromZip(string zip)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllCities()
        {
            throw new NotImplementedException();
        }

        public string[] GetAllStates()
        {
            throw new NotImplementedException();
        }
    }

    public class PlzResponse
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public Record[] records { get; set; }
        public Facet_Groups[] facet_groups { get; set; }
    }

    public class Parameters
    {
        public string dataset { get; set; }
        public string q { get; set; }
        public int rows { get; set; }
        public int start { get; set; }
        public string[] facet { get; set; }
        public string format { get; set; }
        public string timezone { get; set; }
    }

    public class Record
    {
        public string datasetid { get; set; }
        public string recordid { get; set; }
        public Fields fields { get; set; }
        public Geometry1 geometry { get; set; }
        public DateTime record_timestamp { get; set; }
    }

    public class Fields
    {
        public string krs_code { get; set; }
        public string lan_code { get; set; }
        public float[] geo_point_2d { get; set; }
        public string plz_name { get; set; }
        public string plz_name_long { get; set; }
        public string lan_name { get; set; }
        public string name { get; set; }
        public string plz_code { get; set; }
        public string krs_name { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public float[][][] coordinates { get; set; }
        public string type { get; set; }
    }

    public class Geometry1
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }

    public class Facet_Groups
    {
        public string name { get; set; }
        public Facet[] facets { get; set; }
    }

    public class Facet
    {
        public string name { get; set; }
        public int count { get; set; }
        public string state { get; set; }
        public string path { get; set; }
    }
}
