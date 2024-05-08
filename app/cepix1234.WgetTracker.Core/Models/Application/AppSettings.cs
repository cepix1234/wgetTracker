namespace cepix1234.WgetTracker.Core.Models.Application;

public class AppSettings
{
    /// <summary>
    /// How many seconds should you wait for to collect wget output files.
    /// </summary>
    public required double FileCollectionTaskTimout { get; set; }

    /// <summary>
    /// Directories to get wget files from.
    /// </summary>
    public required string[] WgetDirectories { get; init; } = ["."];

    /// <summary>
    /// Wget output file name format to capture.
    /// </summary>
    public required string[] WgetOutputPattern { get; init; } = ["*.out"];
}