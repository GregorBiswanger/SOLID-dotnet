using PlzSuperTool.Infrastructure.Features.MainWindow;
using Shouldly;

namespace PlzSuperTool.Tests
{
    public class ZipRepositoryTests
    {
        [Fact]
        public void Should_return_90455_from_Nuremberg_file_based()
        {
            IZipRepository zipRepository = new ZipFileRepository();

            var zips = zipRepository.GetZipsFrom("Nürnberg");

            zips.ShouldContain("90455");
        }
    }


}
