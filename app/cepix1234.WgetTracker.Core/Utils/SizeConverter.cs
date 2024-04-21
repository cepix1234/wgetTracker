namespace cepix1234.WgetTracker.Core.Utils;

public static class SizeConverter
{
    public static Int64 CovertToB(int kibibyte)
    {
        return kibibyte * 1024;
    }
}