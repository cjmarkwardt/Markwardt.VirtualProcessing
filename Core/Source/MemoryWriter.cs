using System;

namespace Markwardt.VirtualProcessing
{
    public interface IMemoryWriter
    {
        void WriteUnsignedByte(long address, byte value);
        void WriteSignedByte(long address, sbyte value);

        void WriteUnsignedShort(long address, ushort value);
        void WriteSignedShort(long address, short value);

        void WriteUnsignedInteger(long address, uint value);
        void WriteSignedInteger(long address, int value);

        void WriteUnsignedLong(long address, ulong value);
        void WriteSignedLong(long address, long value);
    }

    public class MemoryWriter : IMemoryWriter
    {
        public MemoryWriter(IMemoryBlock block)
        {
            Block = block;
        }

        public IMemoryBlock Block { get; }

        public void WriteUnsignedByte(long address, byte value)
            => Block[address] = value;

        public void WriteSignedByte(long address, sbyte value)
            => Block[address] = (byte)value;

        public void WriteUnsignedShort(long address, ushort value)
            => Block.Write(address, BitConverter.GetBytes(value));

        public void WriteSignedShort(long address, short value)
            => Block.Write(address, BitConverter.GetBytes(value));

        public void WriteUnsignedInteger(long address, uint value)
            => Block.Write(address, BitConverter.GetBytes(value));

        public void WriteSignedInteger(long address, int value)
            => Block.Write(address, BitConverter.GetBytes(value));

        public void WriteUnsignedLong(long address, ulong value)
            => Block.Write(address, BitConverter.GetBytes(value));

        public void WriteSignedLong(long address, long value)
            => Block.Write(address, BitConverter.GetBytes(value));
    }
}