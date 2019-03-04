/*
 **************************************************************
 * Author: Irfansjah
 * Email: irfansjah@gmail.com
 * Created: 07/14/2018
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 **************************************************************  
*/
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Westwind.Utilities;

namespace System
{
    public enum HashStringMode
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        RNG,
        AES,
        DES,
        DSA,
        RC2,
        RSA,
        TripleDES
    }

    public enum UniqueGeneratorType
    {
        Guid, Guid2, Guid3, Guid4, Guid5, Guid6
    }


    public static class GTX_String_Utility
    {
        public const string Alphanumeric1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string Alphanumeric2 = "abcdefghijklmnopqrstuvwxyz1234567890";
        public const string Numeric1 = "1234567890.,+-";
        /// <summary>
        /// The chars
        /// </summary>
        private static readonly Char[] Chars = Alphanumeric1.ToCharArray();

        /// <summary>
        /// Get Hash String From CryptoServiceProvider
        /// </summary>
        /// <typeparam name="TCryptoServiceProvider"></typeparam>
        /// <param name="pContent"></param>
        /// <returns></returns>
        /// SHA384CryptoServiceProvider, RNGCryptoServiceProvider, MD5CryptoServiceProvider,
        /// SHA1CryptoServiceProvider, SHA256CryptoServiceProvider
        public static string GetHashString<TCryptoServiceProvider>(byte[] pContent)
        {
            var s = new StringBuilder();
            dynamic x = Activator.CreateInstance<TCryptoServiceProvider>();
            try
            {
                byte[] bs = x.ComputeHash(pContent);
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }
            } 
            catch {}
            return s.ToString();
        }

        public static string GetHashString<TCryptoServiceProvider>(string pContent)
        {
            byte[] bs = Encoding.UTF8.GetBytes(pContent);
            return GetHashString<TCryptoServiceProvider>(bs);
        }

        public static string GetHashString(HashStringMode mode, string pContent)
        {
            string result = string.Empty;
            byte[] bs = Encoding.UTF8.GetBytes(pContent);
            switch (mode)
            {
                case HashStringMode.MD5: result = GetHashString<MD5CryptoServiceProvider>(bs); break;
                case HashStringMode.SHA1: result = GetHashString<SHA1CryptoServiceProvider>(bs); break;
                case HashStringMode.SHA256: result = GetHashString<SHA256CryptoServiceProvider>(bs); break;
                case HashStringMode.SHA384: result = GetHashString<SHA384CryptoServiceProvider>(bs); break;
                case HashStringMode.SHA512: result = GetHashString<SHA512CryptoServiceProvider>(bs); break;
                /* Not Applicable */
                //case HashStringMode.RNG: result = GetHashString<RNGCryptoServiceProvider>(bs); break;
                //case HashStringMode.AES: result = GetHashString<AesCryptoServiceProvider>(bs); break;
                //case HashStringMode.DES: result = GetHashString<DESCryptoServiceProvider>(bs); break;
                //case HashStringMode.DSA: result = GetHashString<DSACryptoServiceProvider>(bs); break;
                //case HashStringMode.RC2: result = GetHashString<RC2CryptoServiceProvider>(bs); break;
                //case HashStringMode.RSA: result = GetHashString<RSACryptoServiceProvider>(bs); break;
                //case HashStringMode.TripleDES: result = GetHashString<TripleDESCryptoServiceProvider>(bs); break;

            }
            return result;
        }

        /// <summary>
        /// RngCharacterMask
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static string RngCharacterMask(int maxSize = 8)
        {
            var chars = Alphanumeric1.ToCharArray();
            byte[] data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            var size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// Removes everything that is not in the filter text from the input.
        /// </summary>
        /// <param name="input">Input text</param>
        /// <param name="filter">Regex expression of text to keep</param>
        /// <returns>The input text minus everything not in the filter text.</returns>
        public static string KeepFilterText(string input, string filter)
        {
            var tempRegex = new Regex(filter);
            var collection = tempRegex.Matches(input);
            var builder = new StringBuilder();
            foreach (Match match in collection)
            {
                builder.Append(match.Value);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Reverses a string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>The reverse of the input string</returns>
        public static string ReverseString(string input)
        {
            var arrayValues = input.ToCharArray();
            Array.Reverse(arrayValues);
            return new string(arrayValues);
        }



        /// <summary>
        /// Gets the string random.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        private static string GetStringRandom(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                builder.Append(Chars[random.Next(Chars.Length)]);
            }

            return builder.ToString();
        }

        
        /// <summary>
        /// Create Combination of Guid and DateTime for uniqueness
        /// </summary>
        public static string CreateUniqueId
        {
            get
            {
                return string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddhhmmssfffffff"), Guid.NewGuid().ToString().Replace("-", ""));
            }
        }
        
        public static string GenerateUniqueId(UniqueGeneratorType type)
        {
            string result = string.Empty;
            switch(type)
            {
                /* Standard Guid String */
                case UniqueGeneratorType.Guid:
                    result = Guid.NewGuid().ToString();
                    break;
                /* Guid No Hyphen */
                case UniqueGeneratorType.Guid2:
                    result = Guid.NewGuid().ToString().Replace("-", "");
                    break;
                case UniqueGeneratorType.Guid3:
                    result = CreateUniqueId;
                    break;
                case UniqueGeneratorType.Guid4:
                    result = DataUtils.GenerateUniqueId();
                    break;
                case UniqueGeneratorType.Guid5:
                    result = DataUtils.GenerateUniqueId(3);
                    break;
                case UniqueGeneratorType.Guid6:
                    result = DataUtils.GenerateUniqueId(16);
                    break;
            }
            return result;
        }


        /// <summary>
        /// make number inf this format 000001, 0000002, 00010
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string MakeStrFormatedNumber(int number, int length, string format = "{0}{1}")
        {
            return MakeStrFormatedNumber(number, length, format, "0");
        }

        public static string MakeStrFormatedNumber(int number, int length, string format = "{0}{1}", string repeatedChar = "0")
        {
            return string.Format(format, StringUtils.Replicate(repeatedChar, length - number.ToString().Trim().Length), number);
        }

        /***************************************************************************************************/
        /*
        Copyright (c) 2010 <a href="http://www.gutgames.com">James Craig</a>

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in
        all copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
        THE SOFTWARE.*/

        /// <summary>
        /// Converts base 64 string to normal string
        /// </summary>
        /// <param name="Input">Input string</param>
        /// <returns>normal string (non base 64)</returns>
        public static string Base64ToString(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            byte[] TempArray = Convert.FromBase64String(Input);
            return Encoding.UTF8.GetString(TempArray);
        }

        /// <summary>
        /// Converts a normal string to base 64 string
        /// </summary>
        /// <param name="Input">Input string</param>
        /// <returns>A base 64 string</returns>
        public static string StringToBase64(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            byte[] TempArray = Encoding.UTF8.GetBytes(Input);
            return Convert.ToBase64String(TempArray);
        }
        /***************************************************************************************************/

    }
}
