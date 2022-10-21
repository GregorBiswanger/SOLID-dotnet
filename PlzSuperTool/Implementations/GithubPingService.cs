using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PlzSuperTool.Contracts;

namespace PlzSuperTool.Implementations
{
    internal class GithubPingService : IPingService
    {
        public bool Ping(string host, int timeout)
        {
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, timeout);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
