using cepix1234.WgetTracker.Core.Logging.Models;

namespace cepix1234.WgetTracker.Core.Logging;

public class ConsoleLogger: IConsoleLogger
{
    public void Log(String str)
    {
        Console.WriteLine(str);
    }

    public void ClearConsole()
    {
        Console.Clear();
    }
    
    public void ResetCursor()
    {
        Console.SetCursorPosition(0,0);
    }
}