// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: March 2011
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2011 Steffen Ploetz
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// This copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// //////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using X11;

namespace X11
{
    /// <summary> Alternative Base64 implementation to avoid problems with
    /// ToBase64Transform.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset) on Mono. </summary>
    public class AlternativeBase64
    {
        /// <summary> Endode one byte to Base64 character code. </summary>
        /// <param name="c"> The byte to encode to Base64 character code. </param>
        /// <returns> The equivalent Base64 character code. </returns>
        private static byte Encode(byte c)
        {
            if (c < 26)
                return (byte)('A' + c);
            if (c < 52)
                return (byte)('a' + (c - 26));
            if (c < 62)
                return (byte)('0' + (c - 52));
            if (c == 62)
                return (byte)'+';

            return (byte)'/';
        }

        /// <summary> Decode a character code from Base64 to byte. </summary>
        /// <param name="c"> The character code to decode from Base64. </param>
        /// <returns> The equivalent byte. </returns>
        private static byte Decode(byte c)
        {
            if (c >= 'A' && c <= 'Z')
                return (byte)(c - 'A');
            if (c >= 'a' && c <= 'z')
                return (byte)(c - 'a' + 26);
            if (c >= '0' && c <= '9')
                return (byte)(c - '0' + 52);
            if (c == '+')
                return 62;

            return 63;
        }

        /// <summary> Endode a byte array to Base64 character code array. </summary>
        /// <param name="bya"> The byte array to encode. </param>
        /// <returns> The encoded Base64 character code array. </returns>
        public static string Encode(byte[] bya)
        {
            string retval = "";

            if (bya.Length == 0)
                return retval;

            for (int i = 0; i < bya.Length; i += 3)
            {
                byte by1 = 0;
                byte by2 = 0;
                byte by3 = 0;
            
                by1 = bya[i];
                if (i + 1 < bya.Length)
                    by2 = bya[i + 1];

                if (i + 2 < bya.Length)
                    by3 = bya[i + 2];

                byte by4 = 0;
                byte by5 = 0;
                byte by6 = 0;
                byte by7 = 0;

                by4 = (byte)(by1 >> 2);
                by5 = (byte)(((by1 & 0x3) << 4) | (by2 >> 4));
                by6 = (byte)(((by2 & 0xf) << 2) | (by3 >> 6));
                by7 = (byte)(by3 & 0x3f);

                retval += (char)Encode(by4);
                retval += (char)Encode(by5);
                if (i + 1 < bya.Length)
                    retval += (char)Encode(by6);
                else
                    retval += "=";

                if (i + 2 < bya.Length)
                    retval += (char)Encode(by7);
                else
                    retval += "=";
            }

            return retval;
        }

        /// <summary> Decode a Base64 character code array to a byte array. </summary>
        /// <param name="str"> The Base64 character code array to decode. </param>
        /// <returns> The decoded byte array. </returns>
        public static byte[] Decode(string str)
        {
            List<byte> retval = new List<byte>();
            if (str.Length == 0)
                return retval.ToArray();

            for (int i = 0; i < str.Length; i += 4)
            {
                byte c1 = (byte)'A';
                byte c2 = (byte)'A';
                byte c3 = (byte)'A';
                byte c4 = (byte)'A';

                c1 = (byte)str[i];
                if (i + 1 < str.Length)
                    c2 = (byte)str[i + 1];
                if (i + 2 < str.Length)
                    c3 = (byte)str[i + 2];
                if (i + 3 < str.Length)
                    c4 = (byte)str[i + 3];

                byte by1 = 0;
                byte by2 = 0;
                byte by3 = 0;
                byte by4 = 0;
                by1 = Decode(c1);
                by2 = Decode(c2);
                by3 = Decode(c3);
                by4 = Decode(c4);

                retval.Add((byte)((by1<<2)|(by2>>4)));
                if (c3 != '=')
                {
                    retval.Add((byte)(((by2 & 0xf) << 4) | (by3 >> 2)));
                }
                if (c4 != '=')
                {
                    retval.Add((byte)(((by3 & 0x3) << 6) | by4));
                }
            }
            return retval.ToArray();
        }

    }
}
