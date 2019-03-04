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
using System.Text;

namespace System
{
    public static class GTX_Compression_Utility
    {
        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string CompressString(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        GTX_Stream_Utility.CopyTo(msi, gs);
                    }
                    return Convert.ToBase64String(mso.ToArray());
                }
            }

                

        }

        /// <summary>
        /// Decompress string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string DecompressString(this string str)
        {
            var bytes = Convert.FromBase64String(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    GTX_Stream_Utility.CopyTo(gs, mso);
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
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
        #region Deflate Compression


        /// <summary>
        /// Compresses data
        /// </summary>
        /// <param name="Bytes">The byte array to be compressed</param>
        /// <returns>A byte array of compressed data</returns>
        public static byte[] DeflateCompress(byte[] Bytes)
        {
            MemoryStream Stream = new MemoryStream();
            DeflateStream ZipStream = new DeflateStream(Stream, CompressionMode.Compress, true);
            ZipStream.Write(Bytes, 0, Bytes.Length);
            ZipStream.Close();
            byte[] Values = Stream.ToArray();
            ZipStream.Dispose();
            Stream.Dispose();
            return Values;
        }

        /// <summary>
        /// Decompresses data
        /// </summary>
        /// <param name="Bytes">The byte array to be decompressed</param>
        /// <returns>A byte array of uncompressed data</returns>
        public static byte[] DeflateDecompress(byte[] Bytes)
        {
            MemoryStream Stream = new MemoryStream();
            DeflateStream ZipStream = new DeflateStream(new MemoryStream(Bytes), CompressionMode.Decompress, true);
            byte[] Buffer = new byte[4096];
            int Size;
            while (true)
            {
                Size = ZipStream.Read(Buffer, 0, Buffer.Length);
                if (Size > 0) Stream.Write(Buffer, 0, Size);
                else break;
            }
            ZipStream.Close();
            byte[] Values = Stream.ToArray();
            ZipStream.Dispose();
            Stream.Dispose();
            return Values;
        }
        #endregion

        #region GZip Compression
        /// <summary>
        /// Compresses byte data
        /// </summary>
        /// <param name="Bytes">Byte array to be compressed</param>
        /// <returns>A byte array of compressed data</returns>
        public static byte[] GZipCompress(byte[] Bytes)
        {
            MemoryStream Stream = new MemoryStream();
            GZipStream ZipStream = new GZipStream(Stream, CompressionMode.Compress, true);
            ZipStream.Write(Bytes, 0, Bytes.Length);
            ZipStream.Close();
            byte[] Values = Stream.ToArray();
            Stream.Dispose();
            ZipStream.Dispose();
            return Values;
        }

        /// <summary>
        /// Decompresses byte data
        /// </summary>
        /// <param name="Bytes">Byte array to be decompressed</param>
        /// <returns>A byte array of decompressed data</returns>
        public static byte[] GZipDecompress(byte[] Bytes)
        {
            MemoryStream Stream = new MemoryStream();
            GZipStream ZipStream = new GZipStream(new MemoryStream(Bytes), CompressionMode.Decompress, true);
            byte[] Buffer = new byte[4096];
            int Size;
            while (true)
            {
                Size = ZipStream.Read(Buffer, 0, Buffer.Length);
                if (Size > 0)
                {
                    Stream.Write(Buffer, 0, Size);
                }
                else
                {
                    break;
                }
            }
            ZipStream.Close();
            byte[] Values = Stream.ToArray();
            Stream.Dispose();
            ZipStream.Dispose();
            return Values;
        }

        #endregion
        /***************************************************************************************************/
        public enum GTX_CompressionMode
        {
            Deflate,
            GZip
        }

        public static byte[] Compress(GTX_CompressionMode mode, byte[] Bytes)
        {
            byte[] result = null;
            switch(mode)
            {
                case GTX_CompressionMode.Deflate: result = DeflateCompress(Bytes); break;
                case GTX_CompressionMode.GZip: result = GZipCompress(Bytes); break;
            }
            return result;
        }
    }
}
