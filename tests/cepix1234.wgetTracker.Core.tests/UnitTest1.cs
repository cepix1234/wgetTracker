using cepix1234.wgetTracker.Core.Logging;

namespace cepix1234.wgetTracker.Core.tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ConsoleLoggerWritesToConsole()
    {
        TextWriter writer = new StringWriter();
        Console.SetOut(writer);
        ConsoleLogger logger = new ConsoleLogger();
        logger.Log("test");
        Assert.AreEqual("test\r\n", writer.ToString());
    }
}