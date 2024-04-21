namespace cepix1234.WgetTracker.Core.Wrappers.Models;

public interface IFileProvider
{
    /// <summary>
    /// Get current working directory.
    /// </summary>
    /// <returns></returns>
    string GetCurrentDirectory();
    
    /// <summary>
    /// Get files within path directory, filter them by string pattern.
    /// </summary>
    /// <param name="path">Path to directory.</param>
    /// <param name="searchPattern">Pattern to filter files by</param>
    /// <returns></returns>
    string[] GetFiles(string path, string searchPattern);
}