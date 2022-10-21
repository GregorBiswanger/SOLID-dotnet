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
}
