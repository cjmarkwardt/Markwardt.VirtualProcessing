using System;
using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class MoveByteImmediateInstruction : Instruction
    {
        public static int Size => 1;

        public MoveByteImmediateInstruction(IEnumerable<Word> words)
        {
            Word word = words.First();
            ResultRegister = word.Extract(5, 3);
            Immediate = word.Extract(8, 8);
        }

        public MoveByteImmediateInstruction(int resultRegister, byte immediate)
        {
            if (resultRegister < 0 || resultRegister >= IbmSeries1Processor.LevelBlock.GeneralCount)
            {
                throw new ArgumentException(nameof(resultRegister));
            }

            ResultRegister = (ushort)resultRegister;
            Immediate = immediate;
        }

        public Word ResultRegister { get; }
        public Word Immediate { get; }

        public override byte[] Export()
            => new byte[] { (byte)(((int)InstructionId.MoveByteImmediate << 3) | (int)ResultRegister.Unsigned), (byte)Immediate.Unsigned };

        protected override void ExecuteInstruction(IbmSeries1Processor processor)
        {
            processor.Level[ResultRegister] = Immediate;
            processor.Move(Size);
        }

        public class Reader : InstructionReader
        {
            public override InstructionId Id => InstructionId.MoveByteImmediate;

            public override (IInstruction Instruction, int AdditionalWords) ReadWords(IEnumerable<Word> words)
                => ReadStaticSize(words, Size, w => new MoveByteImmediateInstruction(w));
        }
    }
}