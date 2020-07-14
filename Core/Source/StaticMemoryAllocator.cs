namespace Markwardt.VirtualProcessing
{
    public interface IStaticMemoryAllocator
    {
        IMemoryBlock Allocate();
    }

    public class StaticMemoryAllocator : IStaticMemoryAllocator
    {
        public StaticMemoryAllocator(IMemoryAllocator allocator, long size)
        {
            Allocator = allocator;
            Size = size;
        }

        public IMemoryAllocator Allocator { get; }
        public long Size { get; }

        public IMemoryBlock Allocate()
            => Allocator.Allocate(Size);
    }
}