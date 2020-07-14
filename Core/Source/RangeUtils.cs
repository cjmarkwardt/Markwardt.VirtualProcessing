namespace Markwardt.VirtualProcessing
{
    public static class RangeUtils
    {
        public static int Calculate(int sourceLength, int start = 0, int? length = null)
        {
            if (length != null)
            {
                return length.Value;
            }
            else
            {
                return sourceLength - start;
            }
        }
    }
}