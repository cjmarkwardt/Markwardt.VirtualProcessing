using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class StopInstruction : Instruction
    {
        public static int Size => 1;

        public StopInstruction(IEnumerable<Word> words)
        {
            Word word = words.First();
            Parameter = word.Extract(8, 8);
        }

        public StopInstruction(byte parameter = 0)
        {
            Parameter = parameter;
        }

        public Word Parameter { get; }

        public override byte[] Export()
            => new byte[] { ((int)InstructionId.ManagementFamily << 3) | ((int)ManagementInstructionFamily.FunctionId.Stop), (byte)Parameter.Unsigned };

        protected override void ExecuteInstruction(IbmSeries1Processor processor)
            => processor.State = ProcessorState.Stop;

        public class Reader : InstructionFunctionReader
        {
            public override InstructionId Id => InstructionId.ManagementFamily;
            public override Word FunctionCode => (ushort)ManagementInstructionFamily.FunctionId.Stop;

            public override (IInstruction Instruction, int AdditionalWords) ReadWords(IEnumerable<Word> words)
                => ReadStaticSize(words, Size, w => new StopInstruction(w));
        }
    }
}