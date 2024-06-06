using PlzSuperTool.Infrastructure.Features.MainWindow;

namespace SOLID_dotnet.Tests;

public class MainWindowViewModelTests
{
    [Fact]
    public void Should_return_85055_from_Ingolstadt()
    {
        // Arrange
        var zipRepository = new ZipRepository();
        var viewModel = new MainWindowViewModel(zipRepository)
        {
            CityName = "Ingolstadt"
        };

        // Act
        viewModel.SearchZips();

        // Assert
        Assert.Contains("85055", viewModel.Zips);
    }
}