// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: April 2013
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2013 Steffen Ploetz
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
using System.Runtime.InteropServices;

namespace X11
{
	public static class X11Utils
	{
		
		#region SByte (TChar)
		
		/// <summary>Convert a C# string (2 byte (0...65.536) character array) into a byte (1 byte (0...255) character) array.</summary>
		/// <param name="text">The C# string to convert.<see cref="System.String"/></param>
		/// <returns>The byte (1 byte (0...255) character) array.<see cref="X11.TChar[]"/></returns>
		/// <remarks>Used for X11lib.XDrawString() and X11lib.XQueryTextExtents().</remarks>
		public static X11.TChar[] StringToSByteArray (string text)
		{
			if (string.IsNullOrEmpty (text))
				return new X11.TChar[0];
			
			X11.TChar[] result = new X11.TChar[text.Length];
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
				result[charIndex] = (X11.TChar)text[charIndex];
			
			return result;
		}
		
		/// <summary>Convert a byte (1 byte (0...255) character) array into a C# string (2 byte (0...65.536) character array).</summary>
		/// <param name="text">The byte (1 byte (0...255) character) array to convert.<see cref="X11.TChar[]"/></param>
		/// <returns>The C# string (2 byte (0...65.536) character array).<see cref="System.String"/></returns>
		public static string SByteArrayToString (X11.TChar[] text)
		{
			if (text.Length == 0)
				return "";
			
			System.Text.StringBuilder result = new System.Text.StringBuilder ();
			for (int charIndex = 0; charIndex < text.Length; charIndex++)
				// A 'char' is a 2-byte unicode character (0...65.536).
				result.Append ((char)text[charIndex]);
			
			return result.ToString ();
		}
		
		/// <summary>Extract a sub-array from a byte (1 byte (0...255) character) array.</summary>
		/// <param name="text">The byte (1 byte (0...255) character) array to extract a sub-array from.<see cref="X11.TChar[]"/></param>
		/// <param name="start">The index of the first character to extract.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters to extract, or -1 for all characters up to the end.<see cref="System.Int32"/></param>
		/// <returns>The extracted sub-array on success, or an empty array otherwise.<see cref="X11.TChar[]"/></returns>
		public static X11.TChar[] SubSByteArray (X11.TChar[] text, int start, int length)
		{
			if (start >= text.Length)
				return new X11.TChar[0];
			if (length <= 0)
				length = text.Length;
			
			int realLength = (length > 0 && start + length < text.Length ? length : text.Length - start);
			X11.TChar[] result = new X11.TChar[realLength];
			for (int charIndex = 0;  charIndex < realLength; charIndex++)
				result[charIndex] = text[start + charIndex];
			
			return result;
		}
		
		/// <summary>Determine the index of indicated character within a byte (1 byte (0...255) character) array.</summary>
		/// <param name="text">The byte (1 byte (0...255) character) array to find the index in.<see cref="X11.TChar[]"/></param>
		/// <param name="character">The character to find the index for.<see cref="X11.TChar"/></param>
		/// <returns>The zero-based index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOfSByteArray (X11.TChar[] text, X11.TChar character)
		{
			if (text.Length == 0)
				return -1;
			
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
			{
				if (text[charIndex] == character)
					return charIndex;
			}
			
			return -1;
		}
		
		/// <summary>Determine the index of indicated characters within a byte (1 byte (0...255) character) array.</summary>
		/// <param name="text">The byte (1 byte (0...255) character) array to find the index in.<see cref="X11.TChar[]"/></param>
		/// <param name="character">The character array to find the index for.<see cref="X11.TChar[]"/></param>
		/// <returns>The zero-based index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOfSByteArray (X11.TChar[] text, X11.TChar[] characters)
		{
			if (text.Length == 0)
				return -1;
			
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
			{
				bool match = true;
				for (int testIndex = 0; testIndex < characters.Length; testIndex++)
				{	
					if (charIndex + testIndex >= text.Length)
					{
						match = false;
						break;
					}
					if (text[charIndex + testIndex] != characters[testIndex])
					{
						match = false;
						break;
					}
				}
				
				if (match == true)
					return charIndex;
			}
			
			return -1;
		}
		
		#endregion SByte (TChar)
		
		#region Short (XChar2b)
		
		/// <summary>Convert a C# character (2 byte (0...65.536) character) into a 2 byte (0...65.536) character.</summary>
		/// <param name="text">The C# character to convert.<see cref="System.Char"/></param>
		/// <returns>The 2 byte (0...65.536) character.<see cref="X11lib.XChar2b"/></returns>
		/// <remarks>Used for X11lib.XDrawString16() and X11lib.XQueryTextExtents16().</remarks>
		public static X11lib.XChar2b CharToXChar2b (char c)
		{
			return new X11lib.XChar2b (c);
		}
		
		/// <summary>Convert a C# string (2 byte (0...65.536) character array) into a 2 byte (0...65.536) character array.</summary>
		/// <param name="text">The C# string to convert.<see cref="System.String"/></param>
		/// <returns>The 2 byte (0...65.536) character array.<see cref="X11lib.XChar2b[]"/></returns>
		/// <remarks>Used for X11lib.XDrawString16() and X11lib.XQueryTextExtents16().</remarks>
		public static X11lib.XChar2b[] StringToXChar2bArray (string text)
		{
			if (string.IsNullOrEmpty (text))
				return new X11lib.XChar2b[0];
			
			char[]  buffer = text.ToCharArray();
			X11lib.XChar2b[] result = new X11lib.XChar2b[text.Length];
			for (int charIndex = 0;  charIndex < buffer.Length; charIndex++)
			{
				result[charIndex].byte2 = (X11.TUchar)(buffer[charIndex] & 255);
				result[charIndex].byte1 = (X11.TUchar)(buffer[charIndex] >> 8);
			}
			return result;
		}
		
		/// <summary>Convert a 2 byte (0...65.536) character into a C# character (2 byte (0...65.536) character).</summary>
		/// <param name="text">The 2 byte (0...65.536) character to convert.<see cref="X11lib.XChar2b[]"/></param>
		/// <returns>The C# character (2 byte (0...65.536) character).<see cref="System.Char"/></returns>
		public static char XChar2bToChar (X11lib.XChar2b c)
		{
			return (char)((((int)c.byte1) << 8) + (int)(c.byte2));
		}
		
		/// <summary>Convert a 2 byte (0...65.536) character array into a C# string (2 byte (0...65.536) character array).</summary>
		/// <param name="text">The 2 byte (0...65.536) character array to convert.<see cref="X11lib.XChar2b[]"/></param>
		/// <returns>The C# string (2 byte (0...65.536) character array).<see cref="System.String"/></returns>
		public static string XChar2bArrayToString (X11lib.XChar2b[] text)
		{
			if (text.Length == 0)
				return "";
			
			System.Text.StringBuilder result = new System.Text.StringBuilder ();
			for (int charIndex = 0; charIndex < text.Length; charIndex++)
			{
				// A 'char' is a 2-byte unicode character (0...65.536).
				result.Append ((char)((((int)text[charIndex].byte1) << 8) + ((int)text[charIndex].byte2)));
			}
			
			return result.ToString ();
		}
		
		/// <summary>Extract a sub-array from a 2 byte (0...65.536) character array.</summary>
		/// <param name="text">The 2 byte (0...65.536) character array to extract a sub-array from.<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="start">The index of the first character to extract.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters to extract, or -1 for all characters up to the end.<see cref="System.Int32"/></param>
		/// <returns>The extracted sub-array on success, or an empty array otherwise.<see cref="X11lib.XChar2b[]"/></returns>
		public static X11lib.XChar2b[] SubXChar2bArray (X11lib.XChar2b[] text, int start, int length)
		{
			if (start >= text.Length)
				return new X11lib.XChar2b[0];
			if (length <= 0)
				length = text.Length;
			
			int realLength = (length > 0 && start + length < text.Length ? length : text.Length - start);
			X11lib.XChar2b[] result = new X11lib.XChar2b[realLength];
			for (int charIndex = 0;  charIndex < realLength; charIndex++)
				result[charIndex] = text[start + charIndex];
			
			return result;
		}
		
		/// <summary>Concatenate two 4 byte (0...4.294.967.296) character array.</summary>
		/// <param name="text1">The first 4 byte (0...4.294.967.296) character array to concatenate.<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="text2">The second 4 byte (0...4.294.967.296) character array to concatenate.<see cref="X11lib.XChar2b[]"/></param>
		/// <returns>The concatenated array on success, or an empty array otherwise.<see cref="X11lib.XChar2b[]"/></returns>
		/// <remarks>A tailing null character of text1 will be overwritten by the forst character of text2.</remarks>
		/// <remarks>Not required character of the result will be set to null characters.</remarks>
		public static X11lib.XChar2b[] AddXChar2bArray (X11lib.XChar2b[] text1, X11lib.XChar2b[] text2)
		{
			if (text1.Length == 0)
				return text2;
			if (text2.Length == 0)
				return text1;
			
			X11lib.XChar2b[] result = new X11lib.XChar2b[text1.Length + text2.Length];
			int charIndex1;
			for (charIndex1 = 0; charIndex1 < text1.Length; charIndex1++)
			{
				if (charIndex1 == text1.Length && text1[charIndex1].byte1 == 0 && text1[charIndex1].byte2 == 0)
				{
					charIndex1++;
					break;
				}
				else
					result[charIndex1] = text1[charIndex1];
			}
			for (int charIndex2 = 0; charIndex1 + charIndex2 < result.Length; charIndex2++)
			{
				if (charIndex2 < text2.Length)
					result[charIndex1 + charIndex2] = text2[charIndex2];
				else
				{
					result[charIndex1 + charIndex2].byte1 = 0;
					result[charIndex1 + charIndex2].byte2 = 0;
				}
			}
			
			return result;
		}
		
		/// <summary>Determine the index of indicated character within a 2 byte (0...65.536) character array.</summary>
		/// <param name="text">The 2 byte (0...65.536) character array to find the index in.<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="character">The character to find the index for.<see cref="X11lib.XChar2b"/></param>
		/// <returns>The zero-based index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOfXChar2bArray (X11lib.XChar2b[] text, X11lib.XChar2b character)
		{
			if (text.Length == 0)
				return -1;
			
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
			{
				if (text[charIndex] == character)
					return charIndex;
			}
			
			return -1;
		}
		
		/// <summary>Determine the index of indicated characters within a 2 byte (0...65.536) character array.</summary>
		/// <param name="text">The 2 byte (0...65.536) character array to find the index in.<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="character">The character array to find the index for.<see cref="X11lib.XChar2b[]"/></param>
		/// <returns>The zero-based index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOfXChar2bArray (X11lib.XChar2b[] text, X11lib.XChar2b[] characters)
		{
			if (text.Length == 0)
				return -1;
			
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
			{
				bool match = true;
				for (int testIndex = 0; testIndex < characters.Length; testIndex++)
				{	
					if (charIndex + testIndex >= text.Length)
					{
						match = false;
						break;
					}
					if (text[charIndex + testIndex] != characters[testIndex])
					{
						match = false;
						break;
					}
				}
				
				if (match == true)
					return charIndex;
			}
			
			return -1;
		}
		
		/// <summary>Test whether 'text', starting at 'startIndex', is equal to 'predicate'.</summary>
		/// <param name="text">The string to test.<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="startIndex">The index to start the test.<see cref="System.Int32"/></param>
		/// <param name="predicate">The string to compare to.<see cref="X11lib.XChar2b[]"/></param>
		/// <returns>True if substring is equal to predicate, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool SubstringEqual (X11lib.XChar2b[] text, int startIndex, X11lib.XChar2b[] predicate)
		{
			if (text == null)
				return false;
			if (predicate == null)
				return false;
			
			if (text.Length - startIndex < predicate.Length)
				return false;
			
			for (int index = 0; index < predicate.Length; index++)
				if (text[startIndex + index] != predicate[index])
					return false;
			
			return true;
		}
		
		/// <summary>Test whether 'text', starting at 'startIndex', matches the 'predicate'.</summary>
		//// <param name="text">The string to test.<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="startIndex">The index to start the test.<see cref="System.Int32"/></param>
		/// <param name="predicate">The mask string to compare to. Accepts '?' as single-char joker and '*' as multi-char joker.<see cref="X11lib.XChar2b[]"/></param>
		/// <returns>The matching string if substring matches predicate, or string.Empty otherwise.<see cref="X11lib.XChar2b[]"/></returns>
		public static X11lib.XChar2b[] SubstringMatch (X11lib.XChar2b[] text, int startIndex, X11lib.XChar2b[] predicate)
		{
			if (text == null)
				return new X11lib.XChar2b[0];
			if (predicate == null)
				return new X11lib.XChar2b[0];
			
			
			if (text.Length - startIndex < predicate.Length)
				return new X11lib.XChar2b[0];
			
			List<X11lib.XChar2b> result = new List<X11lib.XChar2b> ();
			X11lib.XChar2b asteric = new X11lib.XChar2b ('*');
			X11lib.XChar2b qmark   = new X11lib.XChar2b ('?');
			
			for (int index = 0; index < predicate.Length; index++)
			{
				// [1] Either current char matches the predicate's current mask char.
				if (text[startIndex + index] == predicate[index] ||
				    predicate[index] == asteric /* '*' = Multi-char joker! */ ||
				    predicate[index] == qmark   /* '?' = Single-char joker! */)
					result.Add (text[startIndex + index]);
				// [2] Or the predicate's current mask char is a multi-char joker.
				else if (index > 0 && predicate[index - 1] == asteric /* '*' = Multi-char joker! */)
				{
					// [A] Either multi-char joker represents a sub-string length of 0 characters.
					if (text[startIndex + index - 1] == predicate[index])
						index--;
					// [B] Or the multi-char joker represents a sub-string length of >= 1 character(s).
					else
					{
						do
						{
							// [a] Either current char matches the predicate's current mask char.
							if (text[startIndex + index] == predicate[index])
							{
								result.Add (text[startIndex + index]);
								break;
							}
							// [b] Or the current char isn't the last char and matches the predicate's last joker.
							if (startIndex + index < text.Length - 1)
							{
								result.Add (text[startIndex + index]);
								startIndex++;
							}
							// [c] Or the current char don't match at all.
							else
								return new X11lib.XChar2b[0];
						} while (true);
					}
				}
				// [3] Or the current char don't match at all.
				else
					return new X11lib.XChar2b[0];
			}
			return result.ToArray();
		}
		
		#endregion Short (XChar2b)
		
		#region Int32 (TWchar)
		
		/// <summary>Convert a C# character (2 byte (0...65.536) character) into a 4 byte (0...4.294.967.296) character.</summary>
		/// <param name="c">The C# character to convert.<see cref="System.Char"/></param>
		/// <returns>The 4 byte (0...4.294.967.296) character.<see cref="X11.TWchar"/></returns>
		/// <remarks>Used for X11lib.XwcDrawString6() and X11lib.XwcTextExtents().</remarks>
		public static X11.TWchar CharToWchar (char c)
		{
			try
			{
				byte[]		unicode = System.Text.Encoding.Unicode.GetBytes (new char[] {c});
				X11.TWchar	result  = (X11.TWchar)(unicode[0] + unicode[1] * 256);
				
				return result;
			}
			catch
			{
				return (X11.TWchar)0;
			}
		}
		
		/// <summary>Convert a C# string (2 byte (0...65.536) character array) into a 4 byte (0...4.294.967.296) character array.</summary>
		/// <param name="text">The C# string to convert.<see cref="System.String"/></param>
		/// <returns>The 4 byte (0...4.294.967.296) character array.<see cref="X11.TWchar[]"/></returns>
		/// <remarks>Used for X11lib.XwcDrawString6() and X11lib.XwcTextExtents().</remarks>
		public static X11.TWchar[] StringToWcharArray (string text)
		{
			if (string.IsNullOrEmpty (text))
				return new X11.TWchar[0];
			
			try
			{
				byte[]			unicode = System.Text.Encoding.Unicode.GetBytes (text);
				X11.TWchar[]	result	= new X11.TWchar[unicode.Length / 2];
				
				for (int charIndex = 0; charIndex < unicode.Length; charIndex++)
				{
					// A 'byte' is a 1-byte unsigned integer (0...255).
					result[charIndex / 2] = (X11.TWchar)(unicode[charIndex] + unicode[++charIndex] * 256);
				}
				
				return result;
			}
			catch
			{
				return new X11.TWchar[0];
			}
		}
		
		/// <summary>Convert a 4 byte (0...4.294.967.296) character into a C# char (2 byte (0...65.536) character).</summary>
		/// <param name="c">Then 4 byte (0...4.294.967.296) character to convert.<see cref="X11.TWchar"/></param>
		/// <returns>The C# char (2 byte (0...65.536) character).<see cref="System.Char"/></returns>
		/// <remarks>This conversion might be lossy.</remarks>
		public static char WcharToChar (X11.TWchar c)
		{
			return System.Text.Encoding.Unicode.GetChars (new byte[] {(byte)(((int)c) & 0x00FF), (byte)((((int)c) & 0xFF00) / 256)})[0];
		}
		
		/// <summary>Convert a 4 byte (0...4.294.967.296) character into a C# char (2 byte (0...65.536) character).</summary>
		/// <param name="c">Then 4 byte (0...4.294.967.296) character to convert.<see cref="X11.TWchar"/></param>
		/// <returns>The C# char (2 byte (0...65.536) character).<see cref="System.Char"/></returns>
		/// <remarks>This conversion might be lossy.</remarks>
		public static X11lib.XChar2b WcharToXChar2b (X11.TWchar c)
		{	return new X11lib.XChar2b ((X11.TUchar)(((uint)c) & 255), (X11.TUchar)(((uint)c) >> 8));	}
		
		/// <summary>Convert a 4 byte (0...4.294.967.296) character array into a C# string (2 byte (0...65.536) character array).</summary>
		/// <param name="text">The 4 byte (0...4.294.967.296) character array to convert.<see cref="X11lib.TWchar[]"/></param>
		/// <returns>The C# string (2 byte (0...65.536) character array).<see cref="System.String"/></returns>
		/// <remarks>This conversion might be lossy.</remarks>
		public static string WcharArrayToString (X11.TWchar[] text)
		{
			if (text.Length == 0)
				return "";
			
			System.Text.StringBuilder result = new System.Text.StringBuilder ();
			for (int charIndex = 0; charIndex < text.Length; charIndex++)
			{
				// A 'char' is a 2-byte unicode character (0...65.536).
				result.Append ((char)text[charIndex]);
			}
			
			return result.ToString ();
		}
		
		/// <summary>Extract a sub-array from a 4 byte (0...4.294.967.296) character array.</summary>
		/// <param name="text">The 4 byte (0...4.294.967.296) character array to extract a sub-array from.<see cref="X11.TWchar[]"/></param>
		/// <param name="start">The index of the first character to extract.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters to extract, or -1 for all characters up to the end.<see cref="System.Int32"/></param>
		/// <returns>The extracted sub-array on success, or an empty array otherwise.<see cref="X11.TWchar[]"/></returns>
		public static X11.TWchar[] SubWcharArray (X11.TWchar[] text, int start, int length)
		{
			if (start >= text.Length)
				return new X11.TWchar[0];
			if (length <= 0)
				length = text.Length;
			
			int realLength = (length > 0 && start + length < text.Length ? length : text.Length - start);
			X11.TWchar[] result = new X11.TWchar[realLength];
			for (int charIndex = 0;  charIndex < realLength; charIndex++)
				result[charIndex] = text[start + charIndex];
			
			return result;
		}
		
		/// <summary>Concatenate two 4 byte (0...4.294.967.296) character array.</summary>
		/// <param name="text1">The first 4 byte (0...4.294.967.296) character array to concatenate.<see cref="X11.TWchar[]"/></param>
		/// <param name="text2">The second 4 byte (0...4.294.967.296) character array to concatenate.<see cref="X11.TWchar[]"/></param>
		/// <returns>The concatenated array on success, or an empty array otherwise.<see cref="X11.TWchar[]"/></returns>
		/// <remarks>A tailing null character of text1 will be overwritten by the forst character of text2.</remarks>
		/// <remarks>Not required character of the result will be set to null characters.</remarks>
		public static X11.TWchar[] AddWcharArray (X11.TWchar[] text1, X11.TWchar[] text2)
		{
			if (text1.Length == 0)
				return text2;
			if (text2.Length == 0)
				return text1;
			
			X11.TWchar[] result = new X11.TWchar[text1.Length + text2.Length];
			int charIndex1;
			for (charIndex1 = 0; charIndex1 < text1.Length; charIndex1++)
			{
				if (charIndex1 == text1.Length && text1[charIndex1] == (TWchar)0)
				{
					charIndex1++;
					break;
				}
				else
					result[charIndex1] = text1[charIndex1];
			}
			for (int charIndex2 = 0; charIndex1 + charIndex2 < result.Length; charIndex2++)
			{
				if (charIndex2 < text2.Length)
					result[charIndex1 + charIndex2] = text2[charIndex2];
				else
					result[charIndex1 + charIndex2] = (TWchar)0;
			}
			
			return result;
		}
		
		/// <summary>Determine the index of indicated character within a 4 byte (0...4.294.967.296) character array.</summary>
		/// <param name="text">The 4 byte (0...4.294.967.296) character array to find the index in.<see cref="X11.TWchar[]"/></param>
		/// <param name="character">The character to find the index for.<see cref="X11.TWchar"/></param>
		/// <returns>The zero-based index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOfWcharArray (X11.TWchar[] text, X11.TWchar character)
		{
			if (text.Length == 0)
				return -1;
			
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
			{
				if (text[charIndex] == character)
					return charIndex;
			}
			
			return -1;
		}
		
		/// <summary>Determine the index of indicated characters within a 4 byte (0...4.294.967.296) character array.</summary>
		/// <param name="text">The 4 byte (0...4.294.967.296) character array to find the index in.<see cref="X11.TWchar[]"/></param>
		/// <param name="character">The character array to find the index for.<see cref="X11.TWchar[]"/></param>
		/// <returns>The zero-based index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public static int IndexOfWcharArray (X11.TWchar[] text, X11.TWchar[] characters)
		{
			if (text.Length == 0)
				return -1;
			
			for (int charIndex = 0;  charIndex < text.Length; charIndex++)
			{
				bool match = true;
				for (int testIndex = 0; testIndex < characters.Length; testIndex++)
				{	
					if (charIndex + testIndex >= text.Length)
					{
						match = false;
						break;
					}
					if (text[charIndex + testIndex] != characters[testIndex])
					{
						match = false;
						break;
					}
				}
				
				if (match == true)
					return charIndex;
			}
			
			return -1;
		}
		
		/// <summary>Test whether 'text', starting at 'startIndex', is equal to 'predicate'.</summary>
		/// <param name="text">The string to test.<see cref="X11.TWchar[]"/></param>
		/// <param name="startIndex">The index to start the test.<see cref="System.Int32"/></param>
		/// <param name="predicate">The string to compare to.<see cref="X11.TWchar[]"/></param>
		/// <returns>True if substring is equal to predicate, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool SubstringEqual (X11.TWchar[] text, int startIndex, X11.TWchar[] predicate)
		{
			if (text == null)
				return false;
			if (predicate == null)
				return false;
			
			if (text.Length - startIndex < predicate.Length)
				return false;
			
			for (int index = 0; index < predicate.Length; index++)
				if (text[startIndex + index] != predicate[index])
					return false;
			
			return true;
		}
		
		/// <summary>Test whether 'text', starting at 'startIndex', matches the 'predicate'.</summary>
		//// <param name="text">The string to test.<see cref="X11.TWchar[]"/></param>
		/// <param name="startIndex">The index to start the test.<see cref="System.Int32"/></param>
		/// <param name="predicate">The mask string to compare to. Accepts '?' as single-char joker and '*' as multi-char joker.<see cref="X11.TWchar[]"/></param>
		/// <returns>The matching string if substring matches predicate, or string.Empty otherwise.<see cref="X11.TWchar[]"/></returns>
		public static X11.TWchar[] SubstringMatch (X11.TWchar[] text, int startIndex, X11.TWchar[] predicate)
		{
			if (text == null)
				return new X11.TWchar[0];
			if (predicate == null)
				return new X11.TWchar[0];
			
			
			if (text.Length - startIndex < predicate.Length)
				return new X11.TWchar[0];
			
			List<X11.TWchar> result = new List<X11.TWchar> ();
			X11.TWchar asteric = (X11.TWchar) '*';
			X11.TWchar qmark   = (X11.TWchar) '?';
			
			for (int index = 0; index < predicate.Length; index++)
			{
				// [1] Either current char matches the predicate's current mask char.
				if (text[startIndex + index] == predicate[index] ||
				    predicate[index] == asteric /* '*' = Multi-char joker! */ ||
				    predicate[index] == qmark   /* '?' = Single-char joker! */)
					result.Add (text[startIndex + index]);
				// [2] Or the predicate's current mask char is a multi-char joker.
				else if (index > 0 && predicate[index - 1] == asteric /* '*' = Multi-char joker! */)
				{
					// [A] Either multi-char joker represents a sub-string length of 0 characters.
					if (text[startIndex + index - 1] == predicate[index])
						index--;
					// [B] Or the multi-char joker represents a sub-string length of >= 1 character(s).
					else
					{
						do
						{
							// [a] Either current char matches the predicate's current mask char.
							if (text[startIndex + index] == predicate[index])
							{
								result.Add (text[startIndex + index]);
								break;
							}
							// [b] Or the current char isn't the last char and matches the predicate's last joker.
							if (startIndex + index < text.Length - 1)
							{
								result.Add (text[startIndex + index]);
								startIndex++;
							}
							// [c] Or the current char don't match at all.
							else
								return new X11.TWchar[0];
						} while (true);
					}
				}
				// [3] Or the current char don't match at all.
				else
					return new X11.TWchar[0];
			}
			return result.ToArray();
		}
		
		#endregion Int32 (TWchar)

		#region String
		
		/// <summary> Convert a string by capitalizing the start letter(s). </summary>
		/// <param name="text"> The string to convert. <see cref="System.String"/> </param>
		/// <param name="allWords"> Define whether to capitalize the first letter of the string or all first letters of every word. <see cref="System.Boolean"/> </param>
		/// <returns> The string with capitalized start letter(s). <see cref="System.String"/> </returns>
		public static string CapitalStartLetter (string text, bool allWords)
		{
			if (allWords)
			{
				string result = "";
				string[] lines = text.Split (new char[] {' ', '	', '\t', '-'});
				
				for (int count = 0; count < lines.Length; count++)
				{
					result += (string.IsNullOrEmpty (lines[count]) || lines[count].Length < 1 ? text : lines[count].Substring(0, 1).ToUpper() + lines[count].Substring (1));
					result += (text.Length > result.Length ? text.Substring (result.Length, 1) : "");
				}
				return result;
			}
			else
				return (string.IsNullOrEmpty (text) || text.Length < 1 ? text : text.Substring(0, 1).ToUpper() + text.Substring (1));
		}
		
		#endregion
		
	}
}