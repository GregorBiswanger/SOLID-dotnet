using System.Net.NetworkInformation;

namespace PlzSuperTool.Infrastructure.Services;

public class NetworkService
{
    public bool IsOnline()
    {
        string host = "www.github.com";
        bool result = false;
        Ping ping = new Ping();
        try
        {
            PingReply reply = ping.Send(host, 3000);
            if (reply.Status == IPStatus.Success)
            {
                result = true;
            }
        }
        catch { }

        return result;
    }
}