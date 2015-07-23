using System;
using System.Security.Cryptography;

namespace Hashing.Crc
{
    /// <summary>
    /// Based on http://sanity-free.org/12/crc32_implementation_in_csharp.html
    /// </summary>
    public class Crc32 : HashAlgorithm
    {
        private const uint DefaultPoly = 0xedb88320;
        private const uint DefaultSeed = 0xffffffff;
        private readonly uint[] _table;

        private uint _hash;

        private static uint[] InitializeTable(uint poly)
        {
            var table = new uint[256];

            for (uint i = 0; i < table.Length; ++i)
            {
                var temp = i;
                for (var j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ poly;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }

            return table;
        }

        public Crc32(uint poly)
        {
            _table = InitializeTable(poly);
        }

        public Crc32()
            : this(DefaultPoly)
        {
        }


        public override void Initialize()
        {
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            _hash = ComputeChecksum(array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            HashValue = BitConverter.GetBytes(_hash);
            return HashValue;
        }

        public override int HashSize
        {
            get { return 32; }
        }

        private uint ComputeChecksum(byte[] bytes, int index, int count)
        {
            var crc = DefaultSeed;
            for (var i = index; i < (index+count); ++i)
            {
                var idx = (byte)(((crc) & 0xff) ^ bytes[i]);
                crc = (crc >> 8) ^ _table[idx];
            }
            return ~crc;
        }

        public new static Crc32 Create()
        {
            return new Crc32();
        }
    }
}
