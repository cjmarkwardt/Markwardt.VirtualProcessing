using System.Collections.Generic;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class ManagementInstructionFamily : InstructionFamily
    {
        public override InstructionId Id => InstructionId.ManagementFamily;

        public override int FunctionCodeStart => 5;
        public override int FunctionCodeLength => 3;

        protected override IEnumerable<InstructionFunctionReader> Functions => new List<InstructionFunctionReader>()
        {
            new StopInstruction.Reader()
        };

        public enum FunctionId
        {
            Stop = 0b100
        }
    }
}