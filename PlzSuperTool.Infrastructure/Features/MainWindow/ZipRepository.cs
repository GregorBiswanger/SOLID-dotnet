using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace PlzSuperTool.Infrastructure.Features.MainWindow;

public class ZipRepository : IZipRepository
{
    private readonly Dictionary<string, string[]> cache = new Dictionary<string, string[]>();
        
    public string[] GetZipsFrom(bool online, string cityname)
    {
        using var logger = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "log.txt");
        logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname);

        var zips = new List<string>();

        if (cache.ContainsKey(cityname))
        {
            return cache[cityname];
        }
            
        if (online != true)
        {
            foreach (var line in File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "DE.txt"))
            {
                var words = line.Split('\t');

                if (words[2].StartsWith(cityname, StringComparison.InvariantCultureIgnoreCase))
                {
                    zips.Add(words[1]);
                }
            }
        }
        else
        {
            string citynameUrlEncoded = HttpUtility.UrlEncode(cityname);
            const string baseUrl = "https://public.opendatasoft.com/api/records/1.0/search/";
            string query = $"?dataset=georef-germany-postleitzahl&q={citynameUrlEncoded}&facet=plz_name&facet=lan_name&facet=lan_code&rows=9999";

            var httpClient = new HttpClient();
            var stream = httpClient.GetStreamAsync(baseUrl + query).Result;

            var plzResponse = JsonSerializer.DeserializeAsync<PlzResponse>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).Result;

            plzResponse.Records.ToList().ForEach(record =>
            {
                zips.Add(record.Fields.PlzCode);
            });
        }

        cache.Add(cityname, zips.ToArray());
        logger.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname + " - " + zips.Count + " results");

        return zips.ToArray();
    }
}

public class PlzResponse
{
    public int Nhits { get; set; }
    public Record[]? Records { get; set; }
}

public class Record
{
    public Fields? Fields { get; set; }
}

public class Fields
{
    [JsonPropertyName("plz_code")]
    public string? PlzCode { get; set; }
}