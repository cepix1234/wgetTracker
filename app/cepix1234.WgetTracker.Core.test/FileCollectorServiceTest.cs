using cepix1234.WgetTracker.Core.Wrappers.Models;
using cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker.Service;
using cepix1234.WgetTracker.Core.Models.Application;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace cepix1234.WgetTracker.Core.test;

public class FileCollectorServiceTest
{
    private FileCollectorService fileCollector;
    
    [SetUp]
    public void Setup()
    {
        var appSettingsStub = Options.Create(new AppSettings()
        {
            FileCollectionTaskTimout = 1, WgetDirectories = ["Dir1", "Dir2"], WgetOutputPattern = ["*.out", "wget*"]
        });
        
        var fileProviderStub = new Mock<IFileProvider>();
        fileProviderStub.Setup(fps => fps.GetCurrentDirectory()).Returns("c://Git/");
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) => [String.Format("{0}/{1}", path, searchPattern)]);
        var wgetOutputFileReader = new Mock<IWgetOutputFileReader>();
        
        fileCollector = new FileCollectorService(appSettingsStub, fileProviderStub.Object ,wgetOutputFileReader.Object);
    }
    
    [Test]
    public void AllAppSettingsDirectoriesAndPatternsAreUsedToGetFiles()
    {
        fileCollector.GetFiles();
        string[] result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        Assert.That(result, Is.EquivalentTo(new []{"c://Git/Dir1/*.out","c://Git/Dir1/wget*","c://Git/Dir2/*.out","c://Git/Dir2/wget*" }));
    }
    
    [Test]
    public void NewFilesAreLoaded()
    {
        var appSettingsStub = Options.Create(new AppSettings()
        {
            FileCollectionTaskTimout = 1, WgetDirectories = ["Dir1", "Dir2"], WgetOutputPattern = ["*.out", "wget*"]
        });
        
        var fileProviderStub = new Mock<IFileProvider>();
        fileProviderStub.Setup(fps => fps.GetCurrentDirectory()).Returns("c://Git/");
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) => [String.Format("{0}/{1}", path, searchPattern)]);
        var wgetOutputFileReader = new Mock<IWgetOutputFileReader>();
        
        fileCollector = new FileCollectorService(appSettingsStub, fileProviderStub.Object ,wgetOutputFileReader.Object);
        fileCollector.GetFiles();
        string[] result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        Assert.That(result, Is.EquivalentTo(new []{"c://Git/Dir1/*.out","c://Git/Dir1/wget*","c://Git/Dir2/*.out","c://Git/Dir2/wget*" }));
        
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) => [String.Format("{0}/{1}", path, searchPattern), String.Format("{0}/test/{1}", path, searchPattern)]);
        
        fileCollector.GetFiles();
        result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        Assert.That(result, Is.EquivalentTo(new []{"c://Git/Dir1/*.out","c://Git/Dir1/wget*","c://Git/Dir2/*.out","c://Git/Dir2/wget*", "c://Git/Dir1/test/*.out","c://Git/Dir1/test/wget*","c://Git/Dir2/test/*.out","c://Git/Dir2/test/wget*" }));
    }
    
    [Test]
    public void DeletedFilesAreRemoved()
    {
        var appSettingsStub = Options.Create(new AppSettings()
        {
            FileCollectionTaskTimout = 1, WgetDirectories = ["Dir1", "Dir2"], WgetOutputPattern = ["*.out", "wget*"]
        });
        
        var fileProviderStub = new Mock<IFileProvider>();
        fileProviderStub.Setup(fps => fps.GetCurrentDirectory()).Returns("c://Git/");
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) => [String.Format("{0}/{1}", path, searchPattern), String.Format("{0}/test/{1}", path, searchPattern)]);
        var wgetOutputFileReader = new Mock<IWgetOutputFileReader>();
        
        fileCollector = new FileCollectorService(appSettingsStub, fileProviderStub.Object ,wgetOutputFileReader.Object);
        fileCollector.GetFiles();
        string[] result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        Assert.That(result, Is.EquivalentTo(new []{"c://Git/Dir1/*.out","c://Git/Dir1/wget*","c://Git/Dir2/*.out","c://Git/Dir2/wget*", "c://Git/Dir1/test/*.out","c://Git/Dir1/test/wget*","c://Git/Dir2/test/*.out","c://Git/Dir2/test/wget*"  }));
        
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) => [String.Format("{0}/{1}", path, searchPattern)]);
        
        fileCollector.GetFiles();
        result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        Assert.That(result, Is.EquivalentTo(new []{"c://Git/Dir1/*.out","c://Git/Dir1/wget*","c://Git/Dir2/*.out","c://Git/Dir2/wget*"}));
    }
}