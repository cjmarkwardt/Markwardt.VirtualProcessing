namespace Markwardt.VirtualProcessing
{
    public class OffsetMemoryBlock : IMemoryBlock
    {
        public OffsetMemoryBlock(IMemoryBlock block, int offset)
        {
            Block = block;
            Offset = offset;
        }

        public IMemoryBlock Block { get; }
        public int Offset { get; }

        public byte this[long address]
        {
            get => Block[Offset + address];
            set => Block[Offset + address] = value;
        }

        byte IReadOnlyMemoryBlock.this[long address] => ((IReadOnlyMemoryBlock)Block)[Offset + address];

        public long Size => Block.Size - Offset;

        public void Read(long address, byte[] output, int start = 0, int? length = null)
            => Block.Read(Offset + address, output, start, length);

        public void Write(long address, byte[] input, int start = 0, int? length = null)
            => Block.Write(Offset + address, input, start, length);

        public void Clear()
            => Block.Clear();
    }
}