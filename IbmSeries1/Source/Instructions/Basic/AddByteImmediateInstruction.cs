using System;
using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class AddByteImmediateInstruction : Instruction
    {
        public static int Size => 1;

        public AddByteImmediateInstruction(IEnumerable<Word> words)
        {
            Word word = words.First();
            ResultRegister = word.Extract(5, 3).Unsigned;
            Immediate = word.Extract(8, 8).Unsigned;
        }

        public AddByteImmediateInstruction(int resultRegister, byte immediate)
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
            => new byte[] { (byte)(((int)InstructionId.AddByteImmediate << 3) | (int)ResultRegister.Unsigned), (byte)Immediate.Unsigned };

        protected override void ExecuteInstruction(IbmSeries1Processor processor)
        {
            Word original = processor.Level[ResultRegister];
            int fullResult = original.Signed + Immediate.Unsigned;
            Word result = (short)fullResult;
            
            processor.Level.SetStatusFlag(LevelFlag.Zero, result.Unsigned == 0);
            processor.Level.SetStatusFlag(LevelFlag.Negative, result.Signed < 0);
            processor.Level.SetStatusFlag(LevelFlag.Even, result.Unsigned % 2 == 0);
            processor.Level.SetStatusFlag(LevelFlag.Overflow, fullResult > Word.SignedMax);
            processor.Level.SetStatusFlag(LevelFlag.Carry, result.Unsigned < original.Unsigned);

            processor.Level[ResultRegister] = result;
            processor.Move(Size);
        }

        public class Reader : InstructionReader
        {
            public override InstructionId Id => InstructionId.AddByteImmediate;

            public override (IInstruction Instruction, int AdditionalWords) ReadWords(IEnumerable<Word> words)
                => ReadStaticSize(words, Size, w => new AddByteImmediateInstruction(w));
        }
    }
}