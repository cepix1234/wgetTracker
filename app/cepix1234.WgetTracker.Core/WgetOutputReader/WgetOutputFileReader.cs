using cepix1234.WgetTracker.Core.Utils;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using cepix1234.WgetTracker.Core.Wrappers.Models;

namespace cepix1234.WgetTracker.Core.WgetOutputReader;

public class WgetOutputFileReader(IFileReader fileReader): IWgetOutputFileReader
{
    public Int64 FileSize(string filePath)
    {
        string lengthLine = fileReader.readFileFiles(filePath).Skip(4).Take(1).First();
        string[] lengthLiesSplit = lengthLine.Split(" ");
        return Int64.Parse(lengthLiesSplit[1].Trim());
    }

    public IWgetFileStatusReturn FileStatus(string filePath, int skipLines = 6)
    {
        string[] lengthLine = fileReader.readFileFiles(filePath).Skip(skipLines).Where((line)=> line.Contains("%") && line.Contains("0K")).ToArray();
        string lastStatusLine = lengthLine[lengthLine.Length - 1];
        int dotCount = lastStatusLine.Count(f => f == '.');
        int kibibyteBase = int.Parse(lastStatusLine.Split("K")[0].Trim());
        return new WgetFileStatusReturn(skipLines + lengthLine.Length - 1, SizeConverter.CovertToB(kibibyteBase + dotCount));
    }
}
