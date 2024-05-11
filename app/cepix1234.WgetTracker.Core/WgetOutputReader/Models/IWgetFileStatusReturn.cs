namespace cepix1234.WgetTracker.Core.WgetOutputReader.Models;

public interface IWgetFileStatusReturn
{
    /// <summary>
    /// Line number read of last status.
    /// </summary>
    int LineRead { get; }

    /// <summary>
    /// Current file size status of file in Bytes.
    /// </summary>
    string FileStatus { get; }
}