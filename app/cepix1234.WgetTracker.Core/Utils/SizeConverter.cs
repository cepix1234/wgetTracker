namespace cepix1234.WgetTracker.Core.Utils;

public static class SizeConverter
{
    public static UInt128 CovertToB(UInt128 kibibyte)
    {
        return kibibyte * 1024;
    }
}