using System;

namespace Markwardt.VirtualProcessing
{
    public interface IInstructionSet : IInstructionReader
    {
        int MinInstructionSize { get; }
        int MaxInstructionSize { get; }
    }

    public static class InstructionSetUtils
    {
        public static IInstruction Read(this IInstructionSet set, IReadOnlyMemoryBlock memory, long address, out int size)
        {
            byte[] data = new byte[set.MaxInstructionSize];

            size = set.MinInstructionSize;
            int i = 0;
            while (true)
            {
                memory.Read(address + i, data, i, size - i);
                i = size;

                (IInstruction instruction, int additional) = set.Read(data, 0, size);
                if (instruction != null)
                {
                    return instruction;
                }

                size += additional;
            }
        }
    }
}