using System;
using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing
{
    public interface IReadOnlyMemoryBlock
    {
        long Size { get; }

        byte this[long address] { get; }

        void Read(long address, byte[] output, int start = 0, int? length = null);
    }

    public interface IMemoryBlock : IReadOnlyMemoryBlock
    {
        new byte this[long address] { get; set; }

        void Write(long address, byte[] input, int start = 0, int? length = null);
        void Clear();
    }

    public class MemoryBlock : IMemoryBlock
    {
        public MemoryBlock(long size)
        {
            data = new byte[size];
        }

        public long Size => data.Length;

        public byte this[long address]
        {
            get => data[address];
            set => data[address] = value;
        }

        byte IReadOnlyMemoryBlock.this[long address] => this[address];

        private byte[] data;

        public void Read(long address, byte[] output, int start = 0, int? length = null)
            => Array.Copy(data, address, output, start, RangeUtils.Calculate(output.Length, start, length));

        public void Write(long address, byte[] input, int start = 0, int? length = null)
            => Array.Copy(input, start, data, address, RangeUtils.Calculate(input.Length, start, length));

        public void Clear()
            => data = new byte[Size];

        public class Allocator : IMemoryAllocator
        {
            public IMemoryBlock Allocate(long size)
                => new MemoryBlock(size);
        }
    }

    public static class MemoryBlockUtils
    {
        public static byte[] Read(this IMemoryBlock memory, long address, int length)
        {
            var block = new byte[length];
            memory.Read(address, block);
            return block;
        }

        public static void Write(this IMemoryBlock memory, long address, IEnumerable<byte> block, int start = 0, int? length = null)
        {
            length = RangeUtils.Calculate(block.Count(), start, length);

            if (block is byte[] array)
            {
                memory.Write(address, array, start, length);
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    memory[address + i] = block.Skip(start + i).First();
                }
            }
        }
    }
}