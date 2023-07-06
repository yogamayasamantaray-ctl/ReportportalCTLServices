using System;
using System.Security.Cryptography;
using System.Text;
using SjclHelpers.Codec;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SjclHelpers.Misc
{

    public class PBKDF2HMACSHA256 : DeriveBytes
    {

        public PBKDF2HMACSHA256(byte[] password, byte[] salt, int iterations)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (password.Length < 1)
            {
                throw new ArgumentOutOfRangeException("password");
            }
            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }
            if (salt.Length < 8)
            {
                throw new ArgumentOutOfRangeException("salt");
            }
            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("iterations");
            }
            _pseudorandomFunction = new HMACSHA256(password);
            _salt = salt;
            _iterations = iterations;
        }

        private readonly HMACSHA256 _pseudorandomFunction;

        internal virtual HMACSHA256 PseudorandomFunction
        {
            get { return _pseudorandomFunction; }
        }

        private readonly byte[] _salt;

        internal virtual byte[] Salt { get { return _salt; } }

        private readonly int _iterations;

        internal virtual int Iterations { get { return _iterations; } }

        internal virtual uint IndexOfNextBitOfKeyToFetch { get; set; }

        internal virtual byte[] LastComputedBlock { get; set; }

        public override byte[] GetBytes(int keyLength)
        {
            if (keyLength < 1)
            {
                throw new ArgumentOutOfRangeException("keyLength");
            }
            var currentIndexOfNextBitOfKeyToFetch = IndexOfNextBitOfKeyToFetch;
            if ((keyLength + currentIndexOfNextBitOfKeyToFetch - 1) > 255)
            {
                throw new NotImplementedException(
                    "Support for a total key length of more" +
                    " than 256 bits is not implemented."
                );
            }
            if (currentIndexOfNextBitOfKeyToFetch > 0)
            {
                if (LastComputedBlock == null)
                {
                    throw new InvalidOperationException(
                        "LastComputedBlock may not be null when " +
                        "IndexOfNextBitOfKeyToFetch greater than zero."
                    );
                }
                var bytesToReturn = LastComputedBlock
                    .Skip((int)currentIndexOfNextBitOfKeyToFetch)
                    .Take(keyLength)
                    .ToArray();
                IndexOfNextBitOfKeyToFetch =
                    currentIndexOfNextBitOfKeyToFetch + (uint)keyLength;
                return bytesToReturn;
            }
            IndexOfNextBitOfKeyToFetch = (uint)keyLength;
            var blockToCompute = ConcatSaltAndBlockIndex(1);
            var computedBlock = ComputeBlock(blockToCompute);
            LastComputedBlock = computedBlock;
            return computedBlock.Take(keyLength).ToArray();
        }

        internal virtual byte[] ConcatSaltAndBlockIndex(uint blockIndex)
        {
            if (blockIndex < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "blockIndex",
                    "Parameter blockIndex must be larger than zero."
                );
            }
            var indexBytes = Convert
                .ToString(blockIndex, 16)
                .PadLeft(8, '0')
                .ToBytes();
            var bytes = new byte[Salt.Length + indexBytes.Length];
            Array.Copy(Salt, bytes, Salt.Length);
            Array.Copy(indexBytes, 0, bytes, Salt.Length, indexBytes.Length);
            return bytes;
        }

        internal virtual byte[] ComputeBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException("block");
            }
            var randomizedBlock = Randomize(block);
            var computedBlock = randomizedBlock;
            for (var i = 0; i < Iterations - 1; i++)
            {
                randomizedBlock = Randomize(randomizedBlock);
                computedBlock = computedBlock
                    .Select((b, j) => (byte)(b ^ randomizedBlock[j]))
                    .ToArray();
            }
            return computedBlock;
        }

        internal virtual byte[] Randomize(byte[] block)
        {
            return PseudorandomFunction.ComputeHash(block);
        }

        public override void Reset()
        {
            IndexOfNextBitOfKeyToFetch = 0;
            LastComputedBlock = null;
        }
    }
}