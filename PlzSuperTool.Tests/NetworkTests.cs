using PlzSuperTool.Infrastructure.Services;
using Shouldly;

namespace PlzSuperTool.Tests
{
    public class NetworkTests
    {
        [Fact]
        public void Should_return_true_if_is_online_aviable()
        {
            bool isOnline = Network.IsOnline();

            isOnline.ShouldBeTrue();
        }
    }
}
