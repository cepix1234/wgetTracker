using cepix1234.WgetTracker.Core.WgetFile.Models;

namespace cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker.Models;

public interface IFileCollectorService
{
    /// <summary>
    /// Get Wget files to process
    /// </summary>
    /// <returns>List of gotten wget files</returns>
    public List<IWgetFile> Files();

    /// <summary>
    /// Get all wget output files.
    /// </summary>
    void GetFiles();
}