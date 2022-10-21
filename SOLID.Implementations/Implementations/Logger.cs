using System.IO;
using PlzSuperTool.Contracts;

namespace PlzSuperTool.Implementations
{
    public class Logger : ILogger
    {
        private readonly StreamWriter logger;
        public Logger()
        {
            logger = File.AppendText("log.txt");
        }
        
        public void Dispose()
        {
            logger.Dispose();
        }

        public void WriteLine(string line)
        {
            logger.WriteLine(line);
        }
    }
}