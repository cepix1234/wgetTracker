namespace cepix1234.WgetTracker.Core.WgetFile.Models;

public interface IWgetFile
{
    /// <summary>
    /// File path to wget file.
    /// </summary>
    string FilePath { get; }

    /// <summary>
    /// Directory under which the wget file is.
    /// </summary>
    string Direcory { get; }

    /// <summary>
    /// Get size of the file being downloaded.
    /// </summary>
    /// <returns>Size in Bytes</returns>
    string Size();

    /// <summary>
    /// Get current file download status.
    /// </summary>
    /// <returns>Return download status in Bytes</returns>
    string Status();

    /// <summary>
    /// Name of file being download.
    /// </summary>
    /// <returns>String of file name being downloaded</returns>
    String FileName();

    /// <summary>
    /// Wget file status to string to output to console.
    /// </summary>
    /// <returns>Status of wget file.</returns>
    String ToString();

    /// <summary>
    /// File exists.
    /// </summary>
    /// <returns>True if file exists</returns>
    Boolean Exists();
}