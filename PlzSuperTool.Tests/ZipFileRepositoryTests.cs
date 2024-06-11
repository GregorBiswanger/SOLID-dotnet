using FluentAssertions;
using Moq;
using PlzSuperTool.Infrastructure.Repositories;

namespace PlzSuperTool.Tests;

public class ZipFileRepositoryTests
{
    [Theory]
    [InlineData("Nürnberg", "90455")]
    [InlineData("Ingolstadt", "85055")]
    public void Should_give_postcode_when_given_cityName_file_based(string cityName, string zip)
    {
        // Arrange
        IZipsFileReader zipsFileReader = new ZipsFileReader();
        IZipRepository zipRepository = new ZipFileRepository(zipsFileReader);

        // Act
        var zips = zipRepository.LoadZipsFrom(cityName);

        // Assert
        zips.Should().Contain(zip);
    }

    [Theory]
    [InlineData("Nürnberg", "90455")]
    [InlineData("Ingolstadt", "85055")]
    public void Should_give_postcode_when_given_cityName_based_MOCKED(string cityName, string zip)
    {
        // Arrange
        var mock = new Mock<IZipsFileReader>();
        mock.Setup(zipsFileReader => zipsFileReader.Lines)
            .Returns([
                "DE\t90455\tNürnberg\tBayern\tBY\tMittelfranken\t\tKreisfreie Stadt Nürnberg\t49.4478\t11.0683\t",
                "DE\t85055\tIngolstadt\tBayern\tBY\tOberbayern\t\tKreisfreie Stadt Ingolstadt\t48.7645\t11.4227\t"
            ]);
        var zipsFileReader = mock.Object;
        IZipRepository zipRepository = new ZipFileRepository(zipsFileReader);

        // Act
        var zips = zipRepository.LoadZipsFrom(cityName);

        // Assert
        zips.Should().Contain(zip);
    }
}