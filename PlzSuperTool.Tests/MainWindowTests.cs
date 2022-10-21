using PlzSuperTool.Infrastructure.Features.MainWindow;
using Shouldly;

namespace PlzSuperTool.Tests
{
    public class MainWindowTests
    {
        [Fact]
        public void Should_return_90455_from_Nuremberg()
        {
            // AAA
            // Arrange
            var mainWindowViewModel = new MainWindowViewModel();

            // Action
            mainWindowViewModel.Cityname = "N�rnberg";
            mainWindowViewModel.SearchZips();

            // Assert
            mainWindowViewModel.Zips.ShouldContain("90455");
        }
    }
}