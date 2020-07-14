using System;
using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public abstract class InstructionFamily : InstructionReader
    {
        public InstructionFamily()
        {
            FunctionLookup = Functions.ToDictionary(f => f.FunctionCode);
        }

        public abstract int FunctionCodeStart { get; }
        public abstract int FunctionCodeLength { get; }

        public IReadOnlyDictionary<Word, InstructionFunctionReader> FunctionLookup { get; }

        protected abstract IEnumerable<InstructionFunctionReader> Functions { get; }

        public override (IInstruction Instruction, int AdditionalWords) ReadWords(IEnumerable<Word> words)
        {
            Word functionCode = words.First().Extract(FunctionCodeStart, FunctionCodeLength);
            if (FunctionLookup.TryGetValue(functionCode, out InstructionFunctionReader reader))
            {
                return reader.ReadWords(words);
            }

            throw new InvalidOperationException($"Unknown function code {functionCode} for instruction family {Id}.");
        }
    }
}