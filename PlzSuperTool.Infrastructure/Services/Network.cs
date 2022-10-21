using System.Net.NetworkInformation;

namespace PlzSuperTool.Infrastructure.Services
{
    public class Network
    {
        public static bool IsOnline()
        {
            string host = "www.github.com";
            bool isOnline = false;
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                {
                    isOnline = true;
                }
            }
            catch
            {}

            return isOnline;
        }
    }
}
