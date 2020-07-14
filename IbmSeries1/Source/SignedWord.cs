using System;
using System.Collections;
using System.Collections.Specialized;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    /*public struct SignedWord : IEquatable<SignedWord>, IEquatable<short>, IEquatable<sbyte>
    {
        public static SignedWord Zero => new SignedWord();
        public static SignedWord Min => (SignedWord)short.MinValue;
        public static SignedWord Max => (SignedWord)short.MaxValue;
        
        public static implicit operator SignedWord(sbyte value)
            => new SignedWord(new Word(value));
        
        public static implicit operator SignedWord(short value)
            => new SignedWord(new Word(value));
        
        public static implicit operator short(SignedWord value)
            => value.Value;

        public static implicit operator int(SignedWord value)
            => value.Value;

        public static explicit operator SignedWord(UnsignedWord value)
            => new SignedWord(value);

        public static explicit operator SignedWord(Word value)
            => new SignedWord(value);

        public static bool operator ==(SignedWord obj1, SignedWord obj2)
            => obj1.Equals(obj2);

        public static bool operator !=(SignedWord obj1, SignedWord obj2)
            => !(obj1 == obj2);

        private SignedWord(Word word)
            => this.word = word;

        private Word word;

        public short Value => word.SignedValue;

        public UnsignedWord Unsigned => (UnsignedWord)this;

        public bool this[int bit] => word[bit];

        public SignedWord Mask(int start = 0, int? length = null)
            => (SignedWord)word.Mask(start, length);

        public SignedWord Extract(int start = 0, int? length = null)
            => (SignedWord)word.Extract(start, length);

        public bool GetBit(int bit)
            => word.GetBit(bit);

        public SignedWord SetBit(int bit, bool value)
            => (SignedWord)word.SetBit(bit, value);

        public bool Equals(SignedWord other)
            => Value == other.Value;

        public bool Equals(short other)
            => Value == other;

        public bool Equals(sbyte other)
            => Value == other;

        public override bool Equals(object obj)
        {
            if (obj is SignedWord word)
            {
                return Equals(word);
            }
            else if (obj is short shortValue)
            {
                return Equals(shortValue);
            }
            else if (obj is sbyte sbyteValue)
            {
                return Equals(sbyteValue);
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