using cepix1234.WgetTracker.Core.WgetOutputReader.Models;

namespace cepix1234.WgetTracker.Core.WgetOutputReader;

public class WgetFileStatusReturn: IWgetFileStatusReturn
{
    public int LineRead { get; }
    
    public string FileStatus { get; }

    public WgetFileStatusReturn(int lineRead, string status)
    {
        LineRead = lineRead;
        FileStatus = status;
    }
}