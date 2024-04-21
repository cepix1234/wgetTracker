namespace cepix1234.WgetTracker.Core.WgetFile.Models;

public interface IWgetFile
{
    /// <summary>
    /// File path to wget file.
    /// </summary>
    string FilePath { get; }

    /// <summary>
    /// Get size of the file being downloaded.
    /// </summary>
    /// <returns>Size in Bytes</returns>
    Int64 Size();

    /// <summary>
    /// Get current file download status.
    /// </summary>
    /// <returns>Return download status in Bytes</returns>
    Int64 Status();
}