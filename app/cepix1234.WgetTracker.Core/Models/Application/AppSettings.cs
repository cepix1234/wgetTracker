namespace cepix1234.WgetTracker.Core.Models.Application;

public class AppSettings
{
    /// <summary>
    /// How many seconds should you wait for to collect wget output files.
    /// </summary>
    public required double FileCollectionTaskTimout { get; set; }

    public required string[] WgetDirectories { get; init; } = ["."];

    public required string[] WgetOutputPattern { get; init; } = ["*.out"];
}