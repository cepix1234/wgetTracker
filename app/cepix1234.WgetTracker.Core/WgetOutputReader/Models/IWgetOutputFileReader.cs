using System.Runtime.InteropServices;

namespace cepix1234.WgetTracker.Core.WgetOutputReader.Models;

public interface IWgetOutputFileReader
{
    /// <summary>
    /// Get file size from wget output, Header length.
    /// </summary>
    /// <param name="filePath">Path to file to read</param>
    /// <returns>Length of file being downloaded in Bytes</returns>
    string FileSize(string filePath);

    /// <summary>
    /// Get current file downloaded size from wget output.
    /// </summary>
    /// <param name="filePath">Path to file to read</param>
    /// <param name="skipLines">How meany lines to skip default 6</param>
    /// <returns>Length of file downloaded in Bytes</returns>
    IWgetFileStatusReturn? FileStatus(string filePath, int skipLines = 6);

    /// <summary>
    /// Get file name that is being downloaded.
    /// </summary>
    /// <param name="filePath">Path to wget output file.</param>
    /// <returns>Name of file being returned.</returns>
    String FileName(string filePath);

    /// <summary>
    /// Is file saved/finished downloading.
    /// </summary>
    /// <param name="filePath">Path to wget output file.</param>
    /// <returns>Is file saved.</returns>
    Boolean FileSaved(string filePath);

    /// <summary>
    /// Does file exist.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>If file exists.</returns>
    Boolean FileExists(string filePath);
}