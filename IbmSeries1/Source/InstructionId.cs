namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public enum InstructionId
    {
        AddByteImmediate = 0b00000,
        MoveByteImmediate = 0b00001,
        JumpOnCondition = 0b00010,
        JumpOnNotCondition = 0b00011,

        ComparisonFamily = 0b00101,
        ManagementFamily = 0b01100,
        ByteFamily = 0b11000
    }
}