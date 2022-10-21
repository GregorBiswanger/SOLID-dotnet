using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using PlzSuperTool.Contracts;
using PlzSuperTool.Core;

namespace PlzSuperTool.Implementations
{
    public sealed class OnlineZipRepository : IZipSource
    {
        private readonly ILogger logger;
        public OnlineZipRepository(ILogger logger)
        {
            this.logger = logger;
        }
        
        public string[] GetZipsFrom(string cityname)
        {
            logger?.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname);
            
            string citynameUrlEncoded = HttpUtility.UrlEncode(cityname);
            const string baseUrl = "https://public.opendatasoft.com/api/records/1.0/search/";
            string query = $"?dataset=georef-germany-postleitzahl&q={citynameUrlEncoded}&facet=plz_name&facet=lan_name&facet=lan_code&rows=9999";

            var httpClient = new HttpClient();
            var stream = httpClient.GetStreamAsync(baseUrl + query).Result;

            var plzResponse = JsonSerializer.DeserializeAsync<PlzResponse>(stream).Result;

            var zips = plzResponse.records.Select(r => r.fields.plz_code).ToArray();
            logger?.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname + " - " + zips.Count() + " results");
            return zips;
        }
    }
}