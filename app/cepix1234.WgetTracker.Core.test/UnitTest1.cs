using cepix1234.WgetTracker.Core.Logging;

namespace cepix1234.WgetTracker.Core.test;

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
        Assert.That("test\r\n", Is.EqualTo(writer.ToString()));
    }
}