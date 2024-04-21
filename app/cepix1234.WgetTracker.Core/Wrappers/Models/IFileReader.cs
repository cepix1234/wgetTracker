namespace cepix1234.WgetTracker.Core.Wrappers.Models;

public interface IFileReader
{
    /// <summary>
    /// Read file files
    /// </summary>
    /// <param name="filePath">Path to files to read</param>
    /// <returns></returns>
    IEnumerable<string> readFileFiles(string filePath);
}