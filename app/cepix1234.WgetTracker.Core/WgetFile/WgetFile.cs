using cepix1234.WgetTracker.Core.WgetFile.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;

namespace cepix1234.WgetTracker.Core.WgetFile;

public class WgetFile : IWgetFile
{
    private readonly IWgetOutputFileReader _wgetOutputFileReader;
    private int _lastLineReadStatus;
    private readonly String _fileName;

    public string Direcory { get; }
    public string FilePath { get; }

    public WgetFile(string filePath, IWgetOutputFileReader wgetOutputFileReader, string directory)
    {
        FilePath = filePath;
        _wgetOutputFileReader = wgetOutputFileReader;
        _lastLineReadStatus = 6;
        _fileName = _wgetOutputFileReader.FileName(FilePath);
        Direcory = directory;
    }
    
    public string Size()
    {
        return _wgetOutputFileReader.FileSize(FilePath);
    }

    public string Status()
    {
        IWgetFileStatusReturn result = _wgetOutputFileReader.FileStatus(FilePath, _lastLineReadStatus);
        _lastLineReadStatus = result.LineRead;
        return result.FileStatus;
    }

    public String FileName()
    {
        return _fileName;
    }

    public override String ToString()
    {
        string size = this.Size();
        float percentage = 0;
        if (UInt128.TryParse(size, out UInt128 intSize))
        {
            percentage = (float)(UInt128.Parse(this.Status()) / intSize) * 100;
        }
        
        if (DownloadFinished())
        {
            return String.Format("{0} : |DONE| {1}%, {2}B -> {3}B", this._fileName
                , 100, this.Status(), size );
        }
        var percentageDone = (int)percentage / 2;
        var percentageTodo = 50 - percentageDone;
        var percentageDisplay = "";
        for (int i = 0; i < percentageDone; i++)
        {
            percentageDisplay = String.Format("{0}{1}", percentageDisplay,":");
        }
        
        for (int i = 0; i < percentageTodo; i++)
        {
            percentageDisplay = String.Format("{0}{1}", percentageDisplay," ");
        }
        
        return String.Format("{0} : |{1}| {2}%, {3}B -> {4}B", this._fileName,
            percentageDisplay, (int)percentage, this.Status(), size );
    }

    public Boolean Exists()
    {
        return File.Exists(this.FilePath);
    }

    private Boolean DownloadFinished()
    {
        return _wgetOutputFileReader.FileSaved(FilePath);
    }
}