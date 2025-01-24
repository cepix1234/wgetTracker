using cepix1234.WgetTracker.Core.WgetFile.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;

namespace cepix1234.WgetTracker.Core.WgetFile;

public class WgetFile : IWgetFile
{
    private readonly IWgetOutputFileReader _wgetOutputFileReader;
    private int _lastLineReadStatus;
    private readonly String _fileName;

    private readonly string _wgetFileSize;

    public string Direcory { get; }
    public string FilePath { get; }

    public WgetFile(string filePath, IWgetOutputFileReader wgetOutputFileReader, string directory)
    {
        FilePath = filePath;
        _wgetOutputFileReader = wgetOutputFileReader;
        _lastLineReadStatus = 6;
        _fileName = _wgetOutputFileReader.FileName(FilePath);
        Direcory = directory;
        _wgetFileSize = _wgetOutputFileReader.FileSize(FilePath);
    }


    public string? Status()
    {
        var result = _wgetOutputFileReader.FileStatus(FilePath, _lastLineReadStatus);
        if (result == null)
        {
            return null;
        }
        _lastLineReadStatus = result.LineRead;
        return result.FileStatus;
    }

    public String FileName()
    {
        return _fileName;
    }

    public override String ToString()
    {
        if (!Exists())
        {
            return "";
        }
        string? wgetStatus = Status();
        bool wgetFinished = DownloadFinished();
        float percentage = 0;
        if (UInt128.TryParse(_wgetFileSize, out UInt128 intSize))
        {
            percentage = (float)(UInt128.Parse(wgetStatus) / intSize) * 100;
        }

        if (wgetFinished)
        {
            return String.Format("{0} : |DONE| {1}%, {2}B -> {3}B", _fileName
                , 100, wgetStatus, _wgetFileSize);
        }

        var percentageDone = (int)percentage / 2;
        var percentageTodo = 50 - percentageDone;
        var percentageDisplay = "";
        for (int i = 0; i < percentageDone; i++)
        {
            percentageDisplay = String.Format("{0}{1}", percentageDisplay, ":");
        }

        for (int i = 0; i < percentageTodo; i++)
        {
            percentageDisplay = String.Format("{0}{1}", percentageDisplay, " ");
        }

        return String.Format("{0} : |{1}| {2}%, {3}B -> {4}B", _fileName,
            percentageDisplay, (int)percentage, wgetStatus, _wgetFileSize);
    }

    public Boolean Exists()
    {
        return _wgetOutputFileReader.FileExists(FilePath);
    }

    private Boolean DownloadFinished()
    {
        return _wgetOutputFileReader.FileSaved(FilePath) || _wgetOutputFileReader.FileComplete(FilePath);
    }
}
