using System.Collections.Generic;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    internal static class BitVector32Utils
    {
        static BitVector32Utils()
        {
            var bitKeys = new Dictionary<int, int>();
            for (int i = 0; i < 32; i++)
            {
                int value = 1;
                for (int j = 0; j < i; j++)
                {
                    value *= 2;
                }

                bitKeys.Add(i, value);
            }

            BitKeys = bitKeys;
        }

        public static IReadOnlyDictionary<int, int> BitKeys { get; }
    }
}