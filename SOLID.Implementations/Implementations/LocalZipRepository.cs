using System;
using System.Collections.Generic;
using System.IO;
using PlzSuperTool.Contracts;

namespace PlzSuperTool.Implementations
{
    public sealed class LocalZipRepository : IZipSource
    {
        private readonly ILogger logger;
        public LocalZipRepository(ILogger logger)
        {
            this.logger = logger;
        }
        
        public string[] GetZipsFrom(string cityname)
        {
            logger?.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname);
            
            var zips = new List<string>();
            foreach (var line in File.ReadAllLines("DE.txt"))
            {
                var words = line.Split('\t');

                if (words[2].StartsWith(cityname, StringComparison.InvariantCultureIgnoreCase))
                {
                    zips.Add(words[1]);
                }
            }
            
            logger?.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - GetZipsFrom: " + cityname + " - " + zips.Count + " results");
            return zips.ToArray();
        }
    }
}