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
        fileProviderStub.Setup(fps => fps.GetCurrentDirectory())
            .Returns(String.Format("c:{0}Git{0}", Path.DirectorySeparatorChar));
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) =>
                [String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, searchPattern)]);
        var wgetOutputFileReader = new Mock<IWgetOutputFileReader>();

        fileCollector = new FileCollectorService(appSettingsStub, fileProviderStub.Object, wgetOutputFileReader.Object);
    }

    [Test]
    public void AllAppSettingsDirectoriesAndPatternsAreUsedToGetFiles()
    {
        fileCollector.GetFiles();
        string[] result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        string[] expectedResult = new[]
        {
            "c:{0}Git{0}Dir1{0}*.out", "c:{0}Git{0}Dir1{0}wget*", "c:{0}Git{0}Dir2{0}*.out", "c:{0}Git{0}Dir2{0}wget*"
        };
        Assert.That(result, Is.EquivalentTo(expectedResult.Select(i => String.Format(i, Path.DirectorySeparatorChar))));
    }

    [Test]
    public void NewFilesAreLoaded()
    {
        var appSettingsStub = Options.Create(new AppSettings()
        {
            FileCollectionTaskTimout = 1, WgetDirectories = ["Dir1", "Dir2"], WgetOutputPattern = ["*.out", "wget*"]
        });

        var fileProviderStub = new Mock<IFileProvider>();
        fileProviderStub.Setup(fps => fps.GetCurrentDirectory())
            .Returns(String.Format("c:{0}Git{0}", Path.DirectorySeparatorChar));
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) =>
                [String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, searchPattern)]);
        var wgetOutputFileReader = new Mock<IWgetOutputFileReader>();

        fileCollector = new FileCollectorService(appSettingsStub, fileProviderStub.Object, wgetOutputFileReader.Object);
        fileCollector.GetFiles();
        string[] result = fileCollector.Files().Select(file => file.FilePath).ToArray();

        string[] expectedResult = new[]
        {
            "c:{0}Git{0}Dir1{0}*.out", "c:{0}Git{0}Dir1{0}wget*", "c:{0}Git{0}Dir2{0}*.out", "c:{0}Git{0}Dir2{0}wget*"
        };
        Assert.That(result, Is.EquivalentTo(expectedResult.Select(i => String.Format(i, Path.DirectorySeparatorChar))));

        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) =>
            [
                String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, searchPattern),
                String.Format("{0}{1}test{1}{2}", path, Path.DirectorySeparatorChar, searchPattern)
            ]);

        fileCollector.GetFiles();
        result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        string[] secondExpectedResult = new[]
        {
            "c:{0}Git{0}Dir1{0}*.out", "c:{0}Git{0}Dir1{0}wget*", "c:{0}Git{0}Dir2{0}*.out", "c:{0}Git{0}Dir2{0}wget*",
            "c:{0}Git{0}Dir1{0}test{0}*.out", "c:{0}Git{0}Dir1{0}test{0}wget*", "c:{0}Git{0}Dir2{0}test{0}*.out",
            "c:{0}Git{0}Dir2{0}test{0}wget*"
        };
        Assert.That(result,
            Is.EquivalentTo(secondExpectedResult.Select(i => String.Format(i, Path.DirectorySeparatorChar))));
    }

    [Test]
    public void DeletedFilesAreRemoved()
    {
        var appSettingsStub = Options.Create(new AppSettings()
        {
            FileCollectionTaskTimout = 1, WgetDirectories = ["Dir1", "Dir2"], WgetOutputPattern = ["*.out", "wget*"]
        });

        var fileProviderStub = new Mock<IFileProvider>();
        fileProviderStub.Setup(fps => fps.GetCurrentDirectory())
            .Returns(String.Format("c:{0}Git{0}", Path.DirectorySeparatorChar));
        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) =>
            [
                String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, searchPattern),
                String.Format("{0}{1}test{1}{2}", path, Path.DirectorySeparatorChar, searchPattern)
            ]);
        var wgetOutputFileReader = new Mock<IWgetOutputFileReader>();

        fileCollector = new FileCollectorService(appSettingsStub, fileProviderStub.Object, wgetOutputFileReader.Object);
        fileCollector.GetFiles();
        string[] result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        string[] expectedResult = new[]
        {
            "c:{0}Git{0}Dir1{0}*.out", "c:{0}Git{0}Dir1{0}wget*", "c:{0}Git{0}Dir2{0}*.out", "c:{0}Git{0}Dir2{0}wget*",
            "c:{0}Git{0}Dir1{0}test{0}*.out", "c:{0}Git{0}Dir1{0}test{0}wget*", "c:{0}Git{0}Dir2{0}test{0}*.out",
            "c:{0}Git{0}Dir2{0}test{0}wget*"
        };
        Assert.That(result, Is.EquivalentTo(expectedResult.Select(i => String.Format(i, Path.DirectorySeparatorChar))));

        fileProviderStub.Setup(fps => fps.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(
            (string path, string searchPattern) =>
                [String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, searchPattern)]);

        fileCollector.GetFiles();
        result = fileCollector.Files().Select(file => file.FilePath).ToArray();
        string[] secondExpectedResult = new[]
        {
            "c:{0}Git{0}Dir1{0}*.out", "c:{0}Git{0}Dir1{0}wget*", "c:{0}Git{0}Dir2{0}*.out", "c:{0}Git{0}Dir2{0}wget*"
        };
        Assert.That(result,
            Is.EquivalentTo(secondExpectedResult.Select(i => String.Format(i, Path.DirectorySeparatorChar))));
    }
}