﻿using cepix1234.WgetTracker.Core.Utils;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using cepix1234.WgetTracker.Core.Wrappers.Models;

namespace cepix1234.WgetTracker.Core.WgetOutputReader;

public class WgetOutputFileReader(IFileReader fileReader): IWgetOutputFileReader
{
    public string FileSize(string filePath)
    {
        string lengthLine = fileReader.readFileFiles(filePath).Skip(4).Take(1).First();
        string[] lengthLinesSplit = lengthLine.Split(" ");
        string sizeInString = lengthLinesSplit[1].Trim();
        return sizeInString;
    }

    public IWgetFileStatusReturn FileStatus(string filePath, int skipLines = 6)
    {
        string[] lengthLine = fileReader.readFileFiles(filePath).Skip(skipLines).Where((line)=> line.Contains("0K")).ToArray();
        string lastStatusLine = lengthLine[lengthLine.Length - 1];
        int dotCount = lastStatusLine.Count(f => f == '.');
        Int64 kibibyteBase = Int64.Parse(lastStatusLine.Split("K")[0].Trim());
        return new WgetFileStatusReturn(skipLines + lengthLine.Length - 1, SizeConverter.CovertToB((UInt128)(kibibyteBase + dotCount)).ToString());
    }

    public String FileName(string filePath)
    {
        string savingToLine = fileReader.readFileFiles(filePath).Skip(5).Take(1).First();
        string[] savingToLineSplit = savingToLine.Split(":");
        return savingToLineSplit[1].Trim().Trim((char)8217, (char)8216);
    }

    public Boolean FileSaved(string filePath)
    {
        string[] savedLines = fileReader.readFileFiles(filePath).Skip(6).Where(line => line.Contains(" saved [")).ToArray();
        return savedLines.Length > 0;
    }
}
