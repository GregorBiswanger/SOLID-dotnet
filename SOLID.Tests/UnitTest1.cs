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
        IZipSource zipService = new ZipRepository();
        IPingService githubPingService = new GithubPingService();
        MainViewModel vm = new(zipService, githubPingService);

        vm.Cityname = "Berlin";
        vm.LoadZipsCommand.Execute(null);
        
        Assert.IsTrue(vm.ZipSource.Contains("14109"));
    }
}