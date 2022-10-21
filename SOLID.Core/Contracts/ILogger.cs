using System;

namespace PlzSuperTool.Contracts
{
    public interface ILogger : IDisposable
    {
        void WriteLine(string line);
    }
}