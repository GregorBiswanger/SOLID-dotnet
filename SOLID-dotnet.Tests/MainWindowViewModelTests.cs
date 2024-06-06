using PlzSuperTool.Infrastructure.Features.MainWindow;

namespace SOLID_dotnet.Tests;

public class MainWindowViewModelTests
{
    [Fact]
    public void Should_return_85055_from_Ingolstadt()
    {
        // Arrange
        var viewModel = new MainWindowViewModel
        {
            CityName = "Ingolstadt"
        };

        // Act
        viewModel.SearchZips();

        // Assert
        Assert.Contains("85055", viewModel.Zips);
    }
}