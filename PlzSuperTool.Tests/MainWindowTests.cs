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
            var zipRepository = new ZipFileRepository();
            var mainWindowViewModel = new MainWindowViewModel(zipRepository);

            // Action
            mainWindowViewModel.Cityname = "Nürnberg";
            mainWindowViewModel.SearchZips();

            // Assert
            mainWindowViewModel.Zips.ShouldContain("90455");
        }
    }
}