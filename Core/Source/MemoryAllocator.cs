namespace Markwardt.VirtualProcessing
{
    public interface IMemoryAllocator
    {
        IMemoryBlock Allocate(long size);
    }

    public static class MemoryAllocatorUtils
    {
        public static StaticMemoryAllocator ToStatic(this IMemoryAllocator allocator, long size)
            => new StaticMemoryAllocator(allocator, size);
    }
}