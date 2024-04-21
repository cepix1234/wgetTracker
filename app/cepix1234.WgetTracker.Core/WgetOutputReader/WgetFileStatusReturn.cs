using cepix1234.WgetTracker.Core.WgetOutputReader.Models;

namespace cepix1234.WgetTracker.Core.WgetOutputReader;

public class WgetFileStatusReturn: IWgetFileStatusReturn
{
    public int LineRead { get; }
    
    public Int64 FileStatus { get; }

    public WgetFileStatusReturn(int lineRead, Int64 status)
    {
        LineRead = lineRead;
        FileStatus = status;
    }
}