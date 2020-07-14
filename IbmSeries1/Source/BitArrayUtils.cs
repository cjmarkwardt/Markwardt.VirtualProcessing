using System.Collections;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public static class BitArrayUtils
    {
        public static int GetValue(this BitArray bits)
        {
            var output = new int[1];
            bits.CopyTo(output, 0);
            return output[0];
        }
    }
}