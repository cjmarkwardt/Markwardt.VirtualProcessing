namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public enum LevelFlag
    {
        Even = 0,
        Carry = 1,
        Overflow = 2,
        Negative = 3,
        Zero = 4,

        Supervisor = 8,
        InProcess = 9,
        Trace = 10,
        InterruptsEnabled = 11
    }
}