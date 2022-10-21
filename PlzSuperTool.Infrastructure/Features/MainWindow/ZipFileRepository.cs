namespace PlzSuperTool.Infrastructure.Features.MainWindow
{
    public class ZipFileRepository : IZipRepository
    {
        public string[] GetZipsFrom(string cityname)
        {
            var zips = new List<string>();
            
            foreach (var line in File.ReadAllLines("DE.txt"))
            {
                var words = line.Split('\t');

                if (words[2].StartsWith(cityname, StringComparison.InvariantCultureIgnoreCase))
                {
                    zips.Add(words[1]);
                }
            }

            return zips.ToArray();
        }
    }
}
