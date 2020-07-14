namespace Markwardt.VirtualProcessing
{
    public enum ConditionCode
    {
        Zero = 0b000,
        Positive = 0b001,
        Negative = 0b010,
        Even = 0b011,
        ArithmeticallyLess = 0b100,
        ArithmeticallyLessOrEqual = 0b101,
        LogicallyLessOrEqual = 0b110,
        Carry = 0b111
    }
}