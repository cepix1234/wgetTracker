namespace cepix1234.WgetTracker.Core.Logging.Models;

public interface IConsoleLogger
{
    /// <summary>
    /// Log string to console.
    /// </summary>
    /// <param name="str">String to print to console</param>
    void Log(String str);
}