using System.Collections.Generic;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class ByteInstructionFamily : InstructionFamily
    {
        public override InstructionId Id => InstructionId.ManagementFamily;

        public override int FunctionCodeStart => 13;
        public override int FunctionCodeLength => 3;

        protected override IEnumerable<InstructionFunctionReader> Functions => new List<InstructionFunctionReader>()
        {
            new MoveByteInstruction.Reader()
        };

        public enum FunctionId
        {
            MoveByte = 0b000,
            OrByte = 0b001,
            ResetBitsByte = 0b010,
            ExclusiveOrByte = 0b011,
            CompareByte = 0b100,
            MoveByteAndZero = 0b101,
            AddByte = 0b110,
            SubtractByte = 0b111
        }
    }
}