using cepix1234.WgetTracker.Core.WgetFile.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;

namespace cepix1234.WgetTracker.Core.WgetFile;

public class WgetFile : IWgetFile
{
    private readonly IWgetOutputFileReader _wgetOutputFileReader;
    private int _lastLineReadStatus;
    
    public string FilePath { get; }

    public WgetFile(string filePath, IWgetOutputFileReader wgetOutputFileReader)
    {
        FilePath = filePath;
        _wgetOutputFileReader = wgetOutputFileReader;
        _lastLineReadStatus = 6;
    }
    
    public Int64 Size()
    {
        return _wgetOutputFileReader.FileSize(FilePath);
    }

    public Int64 Status()
    {
        IWgetFileStatusReturn result = _wgetOutputFileReader.FileStatus(FilePath, _lastLineReadStatus);
        _lastLineReadStatus = result.LineRead;
        return result.FileStatus;
    }
}