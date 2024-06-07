namespace PlzSuperTool.Infrastructure.Repositories;

public class ZipFileRepository : IZipRepository
{
    private readonly IZipsFileReader _zipsFileReader;

    public ZipFileRepository(IZipsFileReader zipsFileReader)
    {
        _zipsFileReader = zipsFileReader;
    }

    public string[] LoadZipsFrom(string cityName)
    {
        var zips = new List<string>();

        foreach (var line in _zipsFileReader.Lines)
        {
            var words = line.Split('\t');

            if (words[2].StartsWith(cityName, StringComparison.InvariantCultureIgnoreCase))
            {
                zips.Add(words[1]);
            }
        }

        return zips.ToArray();
    }
}