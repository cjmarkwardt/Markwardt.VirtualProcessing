using System;

namespace Markwardt.VirtualProcessing
{
    public interface IMemoryReader
    {
        byte ReadUnsignedByte(long address);
        sbyte ReadSignedByte(long address);

        ushort ReadUnsignedShort(long address);
        short ReadSignedShort(long address);

        uint ReadUnsignedInteger(long address);
        int ReadSignedInteger(long address);

        ulong ReadUnsignedLong(long address);
        long ReadSignedLong(long address);
    }

    public class MemoryReader : IMemoryReader
    {
        public MemoryReader(IMemoryBlock block)
        {
            Block = block;
        }

        public IMemoryBlock Block { get; }

        public byte ReadUnsignedByte(long address)
            => Block[address];

        public sbyte ReadSignedByte(long address)
            => (sbyte)Block[address];

        public ushort ReadUnsignedShort(long address)
            => BitConverter.ToUInt16(Block.Read(address, 2), 0);

        public short ReadSignedShort(long address)
            => BitConverter.ToInt16(Block.Read(address, 2), 0);

        public uint ReadUnsignedInteger(long address)
            => BitConverter.ToUInt32(Block.Read(address, 4), 0);

        public int ReadSignedInteger(long address)
            => BitConverter.ToInt32(Block.Read(address, 4), 0);

        public ulong ReadUnsignedLong(long address)
            => BitConverter.ToUInt64(Block.Read(address, 8), 0);

        public long ReadSignedLong(long address)
            => BitConverter.ToInt64(Block.Read(address, 8), 0);
    }
}