namespace cepix1234.wgetTracker.Core.Logging.Models;

public interface IConsoleLogger
{
    /// <summary>
    /// Log string to console.
    /// </summary>
    /// <param name="str">String to print to console</param>
    void Log(String str);
}