using PlzSuperTool.Infrastructure.Features.MainWindow;
using PlzSuperTool.Infrastructure.Repositories;
using Snapshooter;
using Snapshooter.Xunit;

namespace SOLID_dotnet.Tests;

public class ZipWebRepositoryTests
{
    [Theory]
    [InlineData("Nürnberg")]
    [InlineData("Ingolstadt")]
    public async Task Should_give_postcode_when_given_cityName_web_based(string cityName)
    {
        // Arrange
        IZipsWebReader zipsWebReader = new ZipsWebReader();
        IZipRepository zipRepository = new ZipWebRepository(zipsWebReader);

        // Act
        var zips = zipRepository.LoadZipsFrom(cityName).OrderBy(zip => zip);

        var result = await Verify(zips).UseParameters(cityName).AutoVerify();
        result.Text.Contains(cityName);
    }

    [Theory]
    [InlineData("Nürnberg")]
    [InlineData("Ingolstadt")]
    public void Should_give_postcode_when_given_cityName_web_based_2(string cityName)
    {
        // Arrange
        IZipsWebReader zipsWebReader = new ZipsWebReader();
        IZipRepository zipRepository = new ZipWebRepository(zipsWebReader);

        // Act
        var zips = zipRepository.LoadZipsFrom(cityName);
        var sortedZips = zips.OrderBy(zip => zip);

        Snapshot.Match(sortedZips, SnapshotNameExtension.Create(cityName));
    }
}
