using cepix1234.WgetTracker.Core.Wrappers.Models;

namespace cepix1234.WgetTracker.Core.Wrappers;

public class FileProvider: IFileProvider
{
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public string[] GetFiles(string path, string searchPattern)
    {
        return Directory.GetFiles(path, searchPattern);
    }
}