using cepix1234.WgetTracker.Core.Wrappers.Models;

namespace cepix1234.WgetTracker.Core.Wrappers;

public class FileReader : IFileReader
{
    public IEnumerable<string> readFileFiles(string filePath)
    {
        return File.ReadLines(filePath);
    }
}