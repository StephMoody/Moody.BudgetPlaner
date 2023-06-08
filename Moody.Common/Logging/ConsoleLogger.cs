using Moody.Common.Contracts;

namespace Moody.Common.Logging;

public class ConsoleLogger : ILogger
{
    public void Error(Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}