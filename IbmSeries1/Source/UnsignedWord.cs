using System;
using System.Collections;
using System.Collections.Specialized;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    /*public struct UnsignedWord : IEquatable<UnsignedWord>, IEquatable<ushort>, IEquatable<byte>
    {
        public static UnsignedWord Zero => new UnsignedWord();
        public static UnsignedWord Min => Zero;
        public static UnsignedWord Max => (UnsignedWord)ushort.MaxValue;

        public static implicit operator UnsignedWord(byte value)
            => new UnsignedWord(new Word(value));
        
        public static implicit operator UnsignedWord(ushort value)
            => new UnsignedWord(new Word(value));
        
        public static implicit operator ushort(UnsignedWord value)
            => value.Value;

        public static implicit operator uint(UnsignedWord value)
            => value.Value;

        public static explicit operator UnsignedWord(SignedWord value)
            => new UnsignedWord(value);
        
        public static explicit operator UnsignedWord(Word value)
            => new UnsignedWord(value);

        public static bool operator ==(UnsignedWord obj1, UnsignedWord obj2)
            => obj1.Equals(obj2);

        public static bool operator !=(UnsignedWord obj1, UnsignedWord obj2)
            => !(obj1 == obj2);

        private UnsignedWord(Word word)
            => this.word = word;

        private Word word;

        public ushort Value => word.UnsignedValue;

        public SignedWord Unsigned => (SignedWord)this;

        public bool this[int bit] => word[bit];

        public UnsignedWord Mask(int start = 0, int? length = null)
            => (UnsignedWord)word.Mask(start, length);

        public UnsignedWord Extract(int start = 0, int? length = null)
            => (UnsignedWord)word.Mask(start, length);

        public bool GetBit(int bit)
            => word.GetBit(bit);

        public UnsignedWord SetBit(int bit, bool value)
            => (UnsignedWord)word.SetBit(bit, value);

        public bool Equals(UnsignedWord other)
            => Value == other.Value;

        public bool Equals(ushort other)
            => Value == other;

        public bool Equals(byte other)
            => Value == other;

        public override bool Equals(object obj)
        {
            if (obj is UnsignedWord word)
            {
                return Equals(word);
            }
            else if (obj is ushort ushortValue)
            {
                return Equals(ushortValue);
            }
            else if (obj is byte byteValue)
            {
                return Equals(byteValue);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Value.ToString();

        public string ToBinaryString()
            => word.ToString();
    }*/
}