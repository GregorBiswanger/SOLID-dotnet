using FluentAssertions;
using PlzSuperTool.Infrastructure.Services;

namespace SOLID_dotnet.Tests;

public class NetworkTests
{
    [Fact]
    public void Should_return_true_if_is_online_aviable()
    {
        var networkService = new NetworkService();
        
        var isOnline = networkService.IsOnline();

        isOnline.Should().BeTrue();
    }
}