using System.Text.Json;
using System.Web;

namespace PlzSuperTool.Infrastructure.Features.MainWindow
{
    public class ZipWebRepository : IZipRepository
    {
        public string[] GetZipsFrom(string cityname)
        {
            var zips = new List<string>();
            
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

            return zips.ToArray();
        }
    }

    internal class PlzResponse
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public Record[] records { get; set; }
        public Facet_Groups[] facet_groups { get; set; }
    }

    internal class Parameters
    {
        public string dataset { get; set; }
        public string q { get; set; }
        public int rows { get; set; }
        public int start { get; set; }
        public string[] facet { get; set; }
        public string format { get; set; }
        public string timezone { get; set; }
    }

    internal class Record
    {
        public string datasetid { get; set; }
        public string recordid { get; set; }
        public Fields fields { get; set; }
        public Geometry1 geometry { get; set; }
        public DateTime record_timestamp { get; set; }
    }

    internal class Fields
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

    internal class Geometry
    {
        public float[][][] coordinates { get; set; }
        public string type { get; set; }
    }

    internal class Geometry1
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }

    internal class Facet_Groups
    {
        public string name { get; set; }
        public Facet[] facets { get; set; }
    }

    internal class Facet
    {
        public string name { get; set; }
        public int count { get; set; }
        public string state { get; set; }
        public string path { get; set; }
    }
}
