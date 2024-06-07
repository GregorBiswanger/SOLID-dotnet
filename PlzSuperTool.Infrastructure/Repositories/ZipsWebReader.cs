using PlzSuperTool.Infrastructure.Entities;
using System.Text.Json;
using System.Web;

namespace PlzSuperTool.Infrastructure.Repositories
{
    public class ZipsWebReader : IZipsWebReader
    {
        public PlzResponse? LoadZipsFrom(string cityName)
        {
            string citynameUrlEncoded = HttpUtility.UrlEncode(cityName);
            const string baseUrl = "https://public.opendatasoft.com/api/records/1.0/search/";
            string query = $"?dataset=georef-germany-postleitzahl&q={citynameUrlEncoded}&facet=plz_name&facet=lan_name&facet=lan_code&rows=9999";

            var httpClient = new HttpClient();
            var stream = httpClient.GetStreamAsync(baseUrl + query).Result;

            var plzResponse = JsonSerializer.DeserializeAsync<PlzResponse>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).Result;

            return plzResponse;
        }
    }
}
