namespace PlzSuperTool.Infrastructure.Repositories;

public class ZipWebRepository : IZipRepository
{
    private readonly IZipsWebReader _zipsWebReader;

    public ZipWebRepository(IZipsWebReader zipsWebReader)
    {
        _zipsWebReader = zipsWebReader;
    }

    public string[] LoadZipsFrom(string cityName)
    {
        var zips = new List<string>();

        var plzResponse = _zipsWebReader.LoadZipsFrom(cityName);

        plzResponse.Records.ToList().ForEach(record =>
        {
            zips.Add(record.Fields.PlzCode);
        });

        return zips.ToArray();
    }
}