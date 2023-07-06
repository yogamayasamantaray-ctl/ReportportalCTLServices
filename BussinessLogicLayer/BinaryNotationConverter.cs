using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SjclHelpers.Codec
{
    public static class BinaryNotationConverter
    {
        #region bitArray related...

        public static string ToHex(this int[] bitArray)
        {
            return bitArray.Aggregate(
                new StringBuilder(8),
                (s, n) => s.Append(Convert.ToString(n, 16).PadLeft(8, '0'))
            ).ToString();
        }


        public static byte[] ToBytes(this int[] bitArray)
        {
            var hex = bitArray.ToHex();
            return Enumerable
                .Range(0, 32)
                .Select(i => Convert.ToByte(hex.Substring(i * 2, 2), 16))
                .ToArray();
        }

        #endregion

        #region String related...

        public static byte[] ToBytes(this string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException("hex");
            }
            var outOfRange = hex.Length % 2 == 1 ||
                !Regex.IsMatch(hex.ToLower(), "^[0-9a-f]*$");
            if (outOfRange)
            {
                throw new ArgumentOutOfRangeException(
                    "hex", "Invalid hexadecimal notation."
                );
            }
            return Enumerable
                .Range(0, hex.Length / 2)
                .Select(i => Convert.ToByte(hex.Substring(i * 2, 2), 16))
                .ToArray();
        }


        public static string ToBase64(this string hex)
        {
            return Convert.ToBase64String(hex.ToBytes());
        }

        #endregion

        #region Byte related


        public static string ToHex(this byte[] bytes)
        {
            return bytes.Aggregate(
                new StringBuilder(32),
                (s, b) => s.Append(Convert.ToString(b, 16).PadLeft(2, '0'))
            ).ToString();
        }

        #endregion
    }
}