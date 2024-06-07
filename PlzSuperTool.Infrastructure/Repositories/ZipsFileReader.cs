namespace PlzSuperTool.Infrastructure.Repositories;

public class ZipsFileReader : IZipsFileReader
{
    public string[] Lines => File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "DE.txt");
}