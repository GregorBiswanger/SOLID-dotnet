namespace PlzSuperTool.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        public void Write(string message)
        {
            File.AppendAllText("log.txt", DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + " - " + message);
        }
    }
}
