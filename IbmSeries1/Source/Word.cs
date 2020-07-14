using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public struct Word : IEquatable<Word>
    {
        public static int ByteCount => 2;
        public static int BitCount => 16;

        public static Word Zero => new Word();

        public static ushort UnsignedMin => ushort.MinValue;
        public static ushort UnsignedMax => ushort.MaxValue;

        public static short SignedMin => short.MinValue;
        public static short SignedMax => short.MaxValue;

        public static implicit operator Word(byte value)
            => new Word(new BitVector32(value));
        
        public static implicit operator Word(sbyte value)
            => new Word(new BitVector32(value));

        public static implicit operator Word(ushort value)
            => new Word(new BitVector32(value));
        
        public static implicit operator Word(short value)
            => new Word(new BitVector32(value));

        /*public static Word operator +(Word x, Word y)
            => new Word(new BitVector32(x.Unsigned + y.Unsigned));

        public static Word operator -(Word x, Word y)
            => new Word(new BitVector32(x.Unsigned - y.Unsigned));

        public static Word operator *(Word x, Word y)
            => new Word(new BitVector32(x.Unsigned * y.Unsigned));

        public static Word operator /(Word x, Word y)
            => new Word(new BitVector32(x.Unsigned / y.Unsigned));*/

        private Word(BitVector32 bits)
            => this.bits = bits;

        private BitVector32 bits;

        public ushort Unsigned => (ushort)bits.Data;
        public short Signed => (short)bits.Data;

        public bool this[int bit] => GetBit(bit);

        public Word Mask(int start = 0, int? length = null)
        {
            length = RangeUtils.Calculate(BitCount, start, length);
            int mask = ((1 << length.Value) - 1) << (BitCount - start - length.Value);
            return new Word(new BitVector32(Unsigned & mask));
        }

        public Word Extract(int start = 0, int? length = null)
        {
            int value = Mask(start, length).Unsigned >> (BitCount - start - length.Value);
            return new Word(new BitVector32(value));
        }

        public bool GetBit(int bit)
            => bits[GetBitIndex(bit)];

        public Word SetBit(int bit, bool value)
        {
            BitVector32 newBits = bits;
            newBits[GetBitIndex(bit)] = value;
            return new Word(newBits);
        }

        private int GetBitIndex(int bit)
            => BitVector32Utils.BitKeys[BitCount - 1 - bit];

        public bool Equals(Word other)
            => bits.Equals(other.bits);

        public override bool Equals(object obj)
        {
            if (obj is Word word)
            {
                return Equals(word);
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
            => $"{(titled ? "IBM Series 1 Word { " : string.Empty)}(U){Unsigned}, (S){Signed}, {Convert.ToString(Unsigned, 2).PadLeft(BitCount, '0')}{(titled ? " }" : string.Empty)}";
    }

    /*public struct Word : IEquatable<Word>
    {
        public static int ByteCount => 2;
        public static int BitCount => 16;

        public static Word Zero => new Word();

        public static explicit operator int(Word value)
            => value.bits.Data;

        public static implicit operator Word(SignedWord value)
            => new Word(new BitVector32(value.Value));

        public static implicit operator Word(UnsignedWord value)
            => new Word(new BitVector32(value.Value));

        public static explicit operator byte(Word value)
            => (byte)value.UnsignedValue;

        public static explicit operator sbyte(Word value)
            => (sbyte)value.SignedValue;

        public static explicit operator ushort(Word value)
            => (ushort)value.UnsignedValue;

        public static explicit operator short(Word value)
            => (short)value.SignedValue;

        public static explicit operator Word(uint value)
            => new Word((ushort)value);

        public static explicit operator Word(int value)
            => new Word((short)value);

        public static bool operator ==(Word obj1, Word obj2)
            => obj1.Equals(obj2);

        public static bool operator !=(Word obj1, Word obj2)
            => !(obj1 == obj2);

        public Word(ushort value)
            : this(new BitVector32(value)) { }

        public Word(short value)
            : this(new BitVector32(value)) { }

        private Word(BitVector32 bits)
            => this.bits = bits;

        private BitVector32 bits;

        public ushort UnsignedValue => (ushort)bits.Data;
        public short SignedValue => (short)bits.Data;

        public UnsignedWord Unsigned => (UnsignedWord)this;
        public SignedWord Signed => (SignedWord)this;

        public bool this[int bit] => GetBit(bit);

        public Word Mask(int start = 0, int? length = null)
        {
            length = RangeUtils.Calculate(16, start, length);
            int mask = ((1 << length.Value) - 1) << (16 - start - length.Value);
            return new Word(new BitVector32(UnsignedValue & mask));
        }

        public Word Extract(int start = 0, int? length = null)
            => new Word(new BitVector32(Mask(start, length).UnsignedValue >> (16 - start - length.Value)));

        public bool GetBit(int bit)
            => bits[GetBitIndex(bit)];

        public Word SetBit(int bit, bool value)
        {
            BitVector32 newBits = bits;
            newBits[GetBitIndex(bit)] = value;
            return new Word(newBits);
        }

        private int GetBitIndex(int bit)
            => BitVector32Utils.BitKeys[BitCount - 1 - bit];

        public bool Equals(Word other)
            => bits.Equals(other.bits);

        public override bool Equals(object obj)
        {
            if (obj is Word word)
            {
                return Equals(word);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
            => UnsignedValue.GetHashCode();

        public override string ToString()
            => Convert.ToString(UnsignedValue, 2).PadLeft(BitCount, '0');
    }*/
}