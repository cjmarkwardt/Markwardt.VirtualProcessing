using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public abstract class InstructionReader : IInstructionReader
    {
        public static (IInstruction Instruction, int AdditionalWords) ReadStaticSize(IEnumerable<Word> words, int size, Func<IEnumerable<Word>, IInstruction> builder)
        {
            int count = words.Count();
            if (count < size)
            {
                return (null, size - count);
            }

            return (builder(words), 0);
        }

        public abstract InstructionId Id { get; }

        public (IInstruction Instruction, int AdditionalBytes) Read(byte[] data, int start = 0, int? length = null)
        {
            length = RangeUtils.Calculate(data.Length, start, length);

            if (length < 0)
            {
                throw new ArgumentException("Length cannot be negative.");
            }
            else if (length < Word.ByteCount)
            {
                return (null, Word.ByteCount - length.Value);
            }
            else if (length > InstructionSet.MaxInstructionSize)
            {
                throw new ArgumentException($"Length cannot be greater than the max size of {InstructionSet.MaxInstructionSize}.");
            }
            else if (length.Value % Word.ByteCount != 0)
            {
                throw new ArgumentException($"Length must be in words (2 bytes).");
            }
            else
            {
                IEnumerable<Word> GetWords()
                {
                    for (int i = 0; i < length.Value; i += Word.ByteCount)
                    {
                        yield return (ushort)new BitArray(new byte[] { data[start + i + 1], data[start + i] }).GetValue();
                    }
                }

                (IInstruction instruction, int additionalWords) = ReadWords(GetWords());
                if (instruction != null)
                {
                    return (instruction, 0);
                }
                else
                {
                    return (null, additionalWords * Word.ByteCount);
                }
            }
        }

        public abstract (IInstruction Instruction, int AdditionalWords) ReadWords(IEnumerable<Word> words);
    }
}