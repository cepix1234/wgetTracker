using cepix1234.WgetTracker.Core.WgetFile.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using Microsoft.VisualBasic;

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
    
    public Int64? Size()
    {
        return _wgetOutputFileReader.FileSize(FilePath);
    }

    public Int64 Status()
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
        var size = this.Size();
        var percentage = (float)0;
        if (size != null)
        {
            percentage = ((float)this.Status() / (float)size) * 100;
        }
        
        if (downloadFinished())
        {
            return String.Format("{0,20} : |DONE| {1,4}%, {2}B -> {3}B", this._fileName
                , (int)percentage, this.Status(), size == null? "?": size );
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
        
        return String.Format("{0,20} : |{1,55}| {2,4}%, {3}B -> {4}B", this._fileName,
            percentageDisplay, (int)percentage, this.Status(), size == null? "?": size );
    }

    public Boolean Exists()
    {
        return File.Exists(this.FilePath);
    }

    private Boolean downloadFinished()
    {
        return _wgetOutputFileReader.FileSaved(FilePath);
    }
}