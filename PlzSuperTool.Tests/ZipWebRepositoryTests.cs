using FluentAssertions;
using Moq;
using PlzSuperTool.Infrastructure.Entities;
using PlzSuperTool.Infrastructure.Repositories;
using Snapshooter;
using Snapshooter.Xunit;
using Record = PlzSuperTool.Infrastructure.Entities.Record;

namespace PlzSuperTool.Tests;

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

    [Theory]
    [InlineData("Nürnberg")]
    [InlineData("Ingolstadt")]
    public void Should_give_postcode_when_given_cityName_web_based_MOCKED(string cityName)
    {
        // Arrange
        var mock = new Mock<IZipsWebReader>();
        mock.Setup(zipsWebReader => zipsWebReader.LoadZipsFrom(It.IsAny<string>()))
            .Returns((string cityName) =>
            {
                if (cityName == "Nürnberg")
                {
                    return new PlzResponse
                    {
                        Records = new[]
                        {
                            new Record { Fields = new() { PlzCode = "90455" } }
                        }
                    };
                }

                if (cityName == "Ingolstadt")
                {
                    return new PlzResponse
                    {
                        Records = new[]
                        {
                            new Record { Fields = new() { PlzCode = "85049" } },
                            new Record { Fields = new() { PlzCode = "85051" } },
                            new Record { Fields = new() { PlzCode = "85053" } },
                            new Record { Fields = new() { PlzCode = "85055" } },
                            new Record { Fields = new() { PlzCode = "85057" } }
                        }
                    };
                }

                return new PlzResponse();
            });
        var zipsWebReader = mock.Object;
        IZipRepository zipRepository = new ZipWebRepository(zipsWebReader);

        // Act
        var zips = zipRepository.LoadZipsFrom(cityName);
        var sortedZips = zips.OrderBy(x => x);

        Snapshot.Match(sortedZips, SnapshotNameExtension.Create(cityName));
    }
}
