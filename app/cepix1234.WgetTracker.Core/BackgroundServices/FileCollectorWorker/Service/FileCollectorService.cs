using cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker.Models;
using cepix1234.WgetTracker.Core.Models.Application;
using cepix1234.WgetTracker.Core.WgetFile.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using cepix1234.WgetTracker.Core.Wrappers.Models;
using Microsoft.Extensions.Options;

namespace cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker.Service;

public class FileCollectorService(
    IOptions<AppSettings> appSettings,
    IFileProvider fileProvider,
    IWgetOutputFileReader wgetOutputFileReader) : IFileCollectorService
{
    private readonly AppSettings _appSettings = appSettings.Value;
    private List<IWgetFile> _wgetFiles = new List<IWgetFile>();

    public List<IWgetFile> Files()
    {
        return _wgetFiles;
    }

    public void GetFiles()
    {
        string cwd = fileProvider.GetCurrentDirectory();
        List<string> wgetFiles = new List<string>();
        foreach (string directory in _appSettings.WgetDirectories)
        {
            string currentDir = Path.Combine(cwd, directory);
            foreach (string pattern in _appSettings.WgetOutputPattern)
            {
                wgetFiles.AddRange(fileProvider.GetFiles(currentDir, pattern));
            }
        }

        // Filter out already loaded files.
        foreach (var wgetFilePath in wgetFiles)
        {
            if (!_fileAlreadyLoaded(wgetFilePath))
            {
                _wgetFiles.Add(new WgetFile.WgetFile(wgetFilePath, wgetOutputFileReader,
                    wgetFilePath.Split(Path.DirectorySeparatorChar)[^2]));
            }
        }

        // Remove deleted files
        string[] filesNoLongerExisting = _wgetFiles.FindAll(file => !wgetFiles.Contains(file.FilePath))
            .Select(file => file.FilePath).ToArray();
        _wgetFiles = _wgetFiles.FindAll(file => !filesNoLongerExisting.Contains(file.FilePath));
    }

    private bool _fileAlreadyLoaded(string filePath)
    {
        bool found = false;
        foreach (var wgetFile in _wgetFiles)
        {
            if (wgetFile.FilePath == filePath)
            {
                found = true;
            }
        }

        return found;
    }
}