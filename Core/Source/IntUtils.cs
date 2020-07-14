using System.Collections;

namespace Markwardt.VirtualProcessing
{
    public static class IntUtils
    {
        public static BitArray ToBits(this int value)
            => new BitArray(new int[] { value });
    }
}