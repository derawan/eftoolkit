/*
 **************************************************************
 * Author: Irfansjah
 * Email: irfansjah@gmail.com
 * Original Author:Mahmud A Hakim
 * Personal Notes: This was  developed as part of my previous teams named Netra
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
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// Class for encryption
    /// </summary>
    /// <remarks>Note : To Netra Developer Team, Add Encryption methods here</remarks>
    public static class SimpleEncryptor
    {
        /// <summary>
        /// default byte array of Key for Encryption
        /// </summary>
        /// <remarks>
        /// <br />created by : mahmud a hakim, st
        /// <br />created at : Sunday, August 06, 2006 12:49:43 AM
        /// <br />updated at : Sunday, August 06, 2006 12:49:46 AM
        /// </remarks>        
        internal static byte[] DefaultByteKeyEncryption = { 242, 58, 7, 227, 105, 122, 192, 168, 238, 129, 36, 96, 108, 23, 247, 249, 18, 42, 241, 87, 135, 70, 17, 178, 15, 31, 2, 0, 91, 7, 85, 22 };
        /// <summary>
        /// default byte array of Initial Vector for Encryption
        /// </summary>
        /// <remarks>
        /// <br />created by : mahmud a hakim, st
        /// <br />created at : Sunday, August 06, 2006 12:49:50 AM
        /// <br />updated at : Sunday, August 06, 2006 12:49:52 AM
        /// </remarks>        
        internal static byte[] DefaultByteIVEncryption = { 42, 127, 130, 78, 94, 245, 237, 28, 251, 191, 96, 164, 7, 183, 253, 164 };
        /// <summary>
        /// convert a string to a password string
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>password string</returns>
        public static string ToPassword(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text is empty");
            }
            var bytePass = Encoding.ASCII.GetBytes(text);
            var sha1 = new SHA1Managed();
            var bytehash = sha1.ComputeHash(bytePass);
            return Convert.ToBase64String(bytehash);
        }
        /// <summary>
        /// Method to encrypt a string with Rijndael 
        /// </summary>
        /// <param name="textToEncypt">string to encrypt</param>
        /// <returns>string : encrypted string</returns>
        /// <remarks>
        /// <br />created by : mahmud a hakim, st
        /// <br />created at : Sunday, August 06, 2006 12:50:08 AM
        /// <br />updated at : Sunday, August 06, 2006 12:50:11 AM
        /// </remarks>
        public static string EncryptString(string textToEncypt)
        {
            if (textToEncypt == null)
            {
                throw new ArgumentNullException("textToEncypt");
            }
            var myRijndael = new RijndaelManaged();
            var encryptor = myRijndael.CreateEncryptor(DefaultByteKeyEncryption, DefaultByteIVEncryption);
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            byte[] byteString = Encoding.ASCII.GetBytes(textToEncypt);
            csEncrypt.Write(byteString, 0, byteString.Length);
            csEncrypt.FlushFinalBlock();
            byte[] byteEncryptedString = msEncrypt.ToArray();
            return Convert.ToBase64String(byteEncryptedString);
        }
        /// <summary>
        /// method to decrypt an Rijndael encrypted string
        /// </summary>
        /// <param name="encryptedText">Encrypted String to decrypt</param>
        /// <returns>decrypted string</returns>
        /// <remarks>
        /// <br />created by : mahmud a hakim, st
        /// <br />created at : Sunday, August 06, 2006 12:50:24 AM
        /// <br />updated at : Sunday, August 06, 2006 12:50:27 AM
        /// </remarks>
        public static string DecryptString(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                throw new ArgumentNullException("encryptedText");
            }
            var byteEncryptedString = Convert.FromBase64String(encryptedText);
            var myRijndael = new RijndaelManaged();
            var decryptor = myRijndael.CreateDecryptor(DefaultByteKeyEncryption, DefaultByteIVEncryption);
            var msDecrypt = new MemoryStream(byteEncryptedString);
            var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            var byteDecryptedString = new byte[byteEncryptedString.Length - 1];
            csDecrypt.Read(byteDecryptedString, 0, byteDecryptedString.Length);
            var decryptedString = Encoding.ASCII.GetString(byteDecryptedString);

            const string charToTrim = "\0";
            decryptedString = decryptedString.TrimEnd(charToTrim.ToCharArray());
            csDecrypt.Dispose();
            msDecrypt.Dispose();
            decryptor.Dispose();
            myRijndael.Dispose();
            return decryptedString;
        }
    }


}
