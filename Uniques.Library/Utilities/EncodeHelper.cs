using System;
using System.Text;

namespace Uniques.Library.Utilities
{
    public static class EncodeHelper
    {
        /// <summary>
        /// Encodes a string to Base 64.
        /// </summary>
        /// <param name="toEncode">String value to encode.</param>
        /// <returns>Base64 encoded string.</returns>
        public static string EncodeTo64(string toEncode)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));
        }

        /// <summary>
        /// Decodes a string from Base64.
        /// </summary>
        /// <param name="encodedData">Encoded Data.</param>
        /// <returns>Decoded string.</returns>
        public static string DecodeFrom64(string encodedData)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(encodedData));
        }
    }
}
