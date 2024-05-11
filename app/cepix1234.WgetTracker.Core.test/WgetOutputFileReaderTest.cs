using cepix1234.WgetTracker.Core.WgetOutputReader;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using cepix1234.WgetTracker.Core.Wrappers.Models;
using Moq;

namespace cepix1234.WgetTracker.Core.test;

public class WgetOutputFileReaderTest
{
    private WgetOutputFileReader wgetReader;
    
    [SetUp]
    public void Setup()
    {
        
        var fileReaderStub = new Mock<IFileReader>();
        fileReaderStub.Setup(fps => fps.readFileFiles(It.IsAny<string>())).Returns(new[]
        {
            "--2024-03-31 22:09:35--  https://cepix21.cz3.quickconnect.to/fbdownload/P1010002.JPG?dlink=%222f686f6d652f416972736f66742f424c52372f736c696b652f50313031303030322e4a5047%22&noCache=1711915736471&mode=download&stdhtml=false&SynoToken=74CiOhC4cuFxM&SynoHash=K6eIXlNOaLtuelvb30XDRD6_7fKV2w.ODM",
            "Resolving cepix21.cz3.quickconnect.to (cepix21.cz3.quickconnect.to)... 2a02:6ea0:c222:1::91, 143.244.58.91",
            "Connecting to cepix21.cz3.quickconnect.to (cepix21.cz3.quickconnect.to)|2a02:6ea0:c222:1::91|:443... connected.",
            "HTTP request sent, awaiting response... 200 OK",
            "Length: 111111111111 (1,7M) [application/octet-stream]",
            "Saving to: ‘P1010002.JPG’",
            "     0K .......... .......... .......... .......... ..........  2%  345K 5s",
            "    50K .......... .......... .......... .......... ..........  5%  319K 5s",
            "   100K .......... .......... .......... .......... ..........  8%  553K 4s"
        });
        wgetReader = new WgetOutputFileReader(fileReaderStub.Object);
    }

    [Test]
    public void WgetOutputFileReaderGetsFileName()
    {
        String fileName = wgetReader.FileName("WgetExample.out");
        Assert.That(fileName, Is.EqualTo("P1010002.JPG"));
    }
    
    [Test]
    public void WgetOutputFileReaderGetsFileSize()
    {
        var size = wgetReader.FileSize("WgetExample.out");
        Assert.That(size, Is.EqualTo("111111111111"));
    }

    [Test]
    public void WgetOutputFileReaderGetsCurrentDownload()
    {
        IWgetFileStatusReturn size = wgetReader.FileStatus("WgetExample.out");
        Assert.That(size.FileStatus, Is.EqualTo("153600"));
        Assert.That(size.LineRead, Is.EqualTo(8));
    }
    
    [Test]
    public void WgetOutputFileReaderGetsCurrentDownloadNotAllDots()
    {
        var fileReaderStub = new Mock<IFileReader>();
        fileReaderStub.Setup(fps => fps.readFileFiles(It.IsAny<string>())).Returns(new[]
        {
            "--2024-03-31 22:09:35--  https://cepix21.cz3.quickconnect.to/fbdownload/P1010002.JPG?dlink=%222f686f6d652f416972736f66742f424c52372f736c696b652f50313031303030322e4a5047%22&noCache=1711915736471&mode=download&stdhtml=false&SynoToken=74CiOhC4cuFxM&SynoHash=K6eIXlNOaLtuelvb30XDRD6_7fKV2w.ODM",
            "Resolving cepix21.cz3.quickconnect.to (cepix21.cz3.quickconnect.to)... 2a02:6ea0:c222:1::91, 143.244.58.91",
            "Connecting to cepix21.cz3.quickconnect.to (cepix21.cz3.quickconnect.to)|2a02:6ea0:c222:1::91|:443... connected.",
            "HTTP request sent, awaiting response... 200 OK",
            "Length: 111111111111 (1,7M) [application/octet-stream]",
            "Saving to: ‘P1010002.JPG’",
            "     0K .......... .......... .......... .......... ..........  2%  345K 5s",
            "    50K .......... .......... .......... .......... ..........  5%  319K 5s",
            "   100K .......... ..........                                   8%  553K 4s"
        });
        wgetReader = new WgetOutputFileReader(fileReaderStub.Object);
        IWgetFileStatusReturn size = wgetReader.FileStatus("WgetExample.out", 7);
        Assert.That(size.FileStatus, Is.EqualTo("122880"));
        Assert.That(size.LineRead, Is.EqualTo(8));
    }
}