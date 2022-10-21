using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlzSuperTool.Contracts
{
    public interface IPingService
    {
        bool Ping(string address, int timeout);
    }
}
