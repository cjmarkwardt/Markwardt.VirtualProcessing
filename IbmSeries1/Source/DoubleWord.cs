using System;
using System.Collections.Specialized;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public struct DoubleWord : IEquatable<DoubleWord>
    {
        public static int ByteCount => 4;
        public static int BitCount => 32;

        public static DoubleWord Zero => new DoubleWord();

        public static uint UnsignedMin => ushort.MinValue;
        public static uint UnsignedMax => ushort.MaxValue;

        public static int SignedMin => short.MinValue;
        public static int SignedMax => short.MaxValue;

        public static implicit operator DoubleWord(byte value)
            => new DoubleWord(new BitVector32(value));
        
        public static implicit operator DoubleWord(sbyte value)
            => new DoubleWord(new BitVector32(value));

        public static implicit operator DoubleWord(ushort value)
            => new DoubleWord(new BitVector32(value));
        
        public static implicit operator DoubleWord(short value)
            => new DoubleWord(new BitVector32(value));
        
        public static implicit operator DoubleWord(uint value)
            => new DoubleWord(new BitVector32((int)value));

        public static implicit operator DoubleWord(int value)
            => new DoubleWord(new BitVector32(value));

        /*public static DoubleWord operator +(DoubleWord x, DoubleWord y)
            => new DoubleWord(new BitVector32(x.Unsigned + y.Unsigned));

        public static DoubleWord operator -(DoubleWord x, DoubleWord y)
            => new DoubleWord(new BitVector32(x.Unsigned - y.Unsigned));

        public static DoubleWord operator *(DoubleWord x, DoubleWord y)
            => new DoubleWord(new BitVector32(x.Unsigned * y.Unsigned));

        public static DoubleWord operator /(DoubleWord x, DoubleWord y)
            => new DoubleWord(new BitVector32(x.Unsigned / y.Unsigned));*/

        private DoubleWord(BitVector32 bits)
            => this.bits = bits;

        private BitVector32 bits;

        public uint Unsigned => (uint)bits.Data;
        public int Signed => (int)bits.Data;

        public bool this[int bit] => GetBit(bit);

        public DoubleWord Mask(int start = 0, int? length = null)
        {
            length = RangeUtils.Calculate(BitCount, start, length);
            int mask = ((1 << length.Value) - 1) << (BitCount - start - length.Value);
            return new DoubleWord(new BitVector32(Signed & mask));
        }

        public DoubleWord Extract(int start = 0, int? length = null)
            => new DoubleWord(new BitVector32(Mask(start, length).Signed >> (BitCount - start - length.Value)));

        public bool GetBit(int bit)
            => bits[GetBitIndex(bit)];

        public DoubleWord SetBit(int bit, bool value)
        {
            BitVector32 newBits = bits;
            newBits[GetBitIndex(bit)] = value;
            return new DoubleWord(newBits);
        }

        private int GetBitIndex(int bit)
            => BitVector32Utils.BitKeys[BitCount - 1 - bit];

        public bool Equals(DoubleWord other)
            => bits.Equals(other.bits);

        public override bool Equals(object obj)
        {
            if (obj is DoubleWord DoubleWord)
            {
                return Equals(DoubleWord);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
            => Unsigned.GetHashCode();

        public override string ToString()
            => ToString(true);

        public string ToString(bool titled)
            => $"{(titled ? "IBM Series 1 Double Word { " : string.Empty)}(U){Unsigned}, (S){Signed}, {Convert.ToString(Unsigned, 2).PadLeft(BitCount, '0')}{(titled ? " }" : string.Empty)}";
    }
}