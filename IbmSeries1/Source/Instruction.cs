using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public abstract class Instruction : IInstruction
    {
        public void Execute(IProcessor processor)
            => ExecuteInstruction((IbmSeries1Processor)processor);

        public abstract byte[] Export();

        protected abstract void ExecuteInstruction(IbmSeries1Processor processor);
    }

    public static class InstructionUtils
    {
        public static IEnumerable<byte> BuildExecutable(this IEnumerable<Instruction> instructions)
            => instructions.SelectMany(i => i.Export());
    }
}