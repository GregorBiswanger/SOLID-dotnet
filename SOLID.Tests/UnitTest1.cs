using PlzSuperTool.Contracts;
using PlzSuperTool.Implementations;
using PlzSuperTool.ViewModels;

namespace SOLID.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ContainsZipFromBerlinTest()
    {
        IZipSource localzipService = new LocalZipRepository(null);
        IZipSource onlinezipService = new OnlineZipRepository(null);
        IPingService githubPingService = new GithubPingService();
        MainViewModel vm = new(localzipService, onlinezipService, githubPingService);

        vm.Cityname = "Berlin";
        vm.LoadZipsCommand.Execute(null);
        
        Assert.IsTrue(vm.ZipSource.Contains("14109"));
    }
}