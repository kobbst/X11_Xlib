// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: November 2014
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 * In case of problems with .NEt see: .NET Reference Source, http://referencesource-beta.microsoft.com/
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2014 Steffen Ploetz
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
using System.Diagnostics;

namespace X11
{
	// For additional functionality see:
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System/String.cs
	
	/// <summary>Provide System.String functionality to an array fo TWchar.</summary>
	/// <remarks>In some languages, such as C and C++, a null character indicates the end of a string.
	/// In the .NET Framework, a null character can be embedded in a string. When a string includes one or
	/// more null characters, they are included in the length of the total string.</remarks>
	public class TWstring
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string				CLASS_NAME = "TWstring";
		
		/// <summary>Represents the null character, that indicates the end of a string.</summary>
		public static readonly TWchar	NULL_CHAR	= (TWchar)'\x0';
		
		/// <summary>Represents the empty string.</summary>
		public static readonly TWstring	Empty		= new TWstring ();
		
		/// <summary>The array of white space characters.</summary>
		public static readonly TWchar[]	WhiteChars	= {
														(TWchar) 0x9, // horizontal tab
														(TWchar) 0xA, // new line
														(TWchar) 0xB, // vertical tab
														(TWchar) 0xC, // form feed
														(TWchar) 0xD, // carriage return
														(TWchar) 0x85, // next line
														(TWchar) 0x20, // space
														(TWchar) 0xA0, // space separator / no-break space
														(TWchar) 0x1680, // space separator / ogham space mark 
														(TWchar) 0x2000, // space separator / en quad
														(TWchar) 0x2001, // space separator / em quad
														(TWchar) 0x2002, // space separator / en space
														(TWchar) 0x2003, // space separator / em space
														(TWchar) 0x2004, // space separator / three-per-em space
														(TWchar) 0x2005, // space separator / four-per-em space
														(TWchar) 0x2006, // space separator / six-per-em space
														(TWchar) 0x2007, // space separator / figure space
														(TWchar) 0x2008, // space separator / punctuation space 
														(TWchar) 0x2009, // space separator / thin space
														(TWchar) 0x200A, // space separator / hair space
														(TWchar) 0x200B, // space separator / zero width space
														(TWchar) 0x2028, // line separator
														(TWchar) 0x2029, // paragraph separator
														(TWchar) 0x3000, // space separator / ideographic space
														(TWchar) 0xFEFF 
													};
		
        #endregion Constants
			
		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The array of chars, representing the string.</summary>
		/// <remarks>NULL_CHARs are always adequate characters and can occur at any position.</remarks>
		protected TWchar[]	_string;
		
		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>The default constructor.</summary>
		public TWstring ()
		{	_string = new TWchar[0];
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the TWstring class to the indicated single character.</summary>
		/// <param name="chr">The initial single char.<see cref="X11.TWchar"/></param>
		public TWstring (TWchar chr)
		{
			_string = new TWchar[] {	chr	};
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the TWstring class to the specified character repeated a specified number of times.</summary>
		/// <param name="chr">The initial char to repeat multiple times.<see cref="X11.TWchar"/></param>
		/// <param name="count">The number of times to repeat chr.<see cref="System.Int32"/></param>
		public TWstring (TWchar chr, int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count");
			
			_string = new TWchar[count];
			
			for (int index = 0; index < count; index++)
			{
				_string[index] = chr;
			}
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the TWstring class to the indicated array of characters.</summary>
		/// <param name="chrs">The array of initial array of chars.<see cref="X11.TWchar[]"/></param>
		public TWstring (TWchar[] chrs)
		{
			if (chrs == null || chrs.Length == 0)
			{	_string = new TWchar[0];
				return;
			}
			
			int length = chrs.Length;
			_string = new TWchar[length];
			CharCopy (_string, 0, chrs, 0, length);
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the TWstring class to a substring of the indicated array of characters.</summary>
		/// <param name="chrs">The initial array of chars.<see cref="X11.TWchar[]"/></param>
		/// <param name="startIndex">The starting index within the initial array of chars.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in initial array of chars to append.<see cref="System.Int32"/></param>
		public TWstring (TWchar[] chrs, int startIndex, int length)
		{
			if (chrs == null || chrs.Length == 0)
			{	_string = new TWchar[0];
				return;
			}
			
			if (startIndex < 0 || startIndex > chrs.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || chrs.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");
			
			_string = new TWchar[length];
			CharCopy (_string, 0, chrs, startIndex, length);
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the TWstring class to the indicated length.</summary>
		/// <param name="length">The requested length (capacity).<see cref="System.Int32"/></param>
		public TWstring (int length)
		{
			if (length < 0)
				throw new System.ArgumentOutOfRangeException ("length", length, "Length cannot be less than zero.");
			
			_string = new TWchar[length];
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the TWstring class to the indicated string.</summary>
		/// <param name="str">The initial string.<see cref="System.String"/></param>
		public TWstring (string str)
		{
			if (string.IsNullOrEmpty (str))
			{	_string = new TWchar[0];
				return;
			}
			
			_string = X11Utils.StringToWcharArray (str);
		}
		
		/// <summary>The copy constructor.</summary>
		/// <param name="str">The initial string.<see cref="X11.TWstring"/></param>
		public TWstring (TWstring str)
		{
			if (object.Equals(str, null)) // Prevent cyclic calls.
			{
				_string = new TWchar[0];
				return;
			}
			
			lock (str._string)
			{
				if (str._string.Length == 0)
					_string = new TWchar[0];
				else
				{	int length = str.Length;
					
					_string = new TWchar[length];
					CharCopy (_string, 0, str._string, 0, length);
				}
			}			
		}
		
        #endregion Construction

        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary>Get the number of characters in the current string.</summary>
		/// <remarks>NULL_CHARs are always adequate characters and can occur at any position.</remarks>
		public int Length
		{	get	{	return _string.Length;	}	}
		
		/// <summary>Get the character at a specified position in the current string.</summary>
		/// <param name="index">The zero-based position of the requested character in the current string.<see cref="System.Int32"/></param>
		/// <remarks>Throws IndexOutOfRangeException if index is smaller than 0 or greater than Length - 1.</remarks>
		public TWchar this[int index]
		{	get
			{
				bool   exception = false;
				TWchar result    = NULL_CHAR;
				
				lock (_string)
				{
					if (index < 0 || index >= _string.Length)
						exception = true;
					else
						result = _string[index];
				}
				if (exception)
					throw new IndexOutOfRangeException ();
				
				return result;
			}
		}

		#endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Static methods
		
		/// <summary>Set one character of indicated string at indicated index to indicated value.</summary>
		/// <param name="target">The string to set a character.<see cref="X11.TWstring"/></param>
		/// <param name="index">The position to set a character.<see cref="System.Int32"/></param>
		/// <param name="chr">The character to set.<see cref="X11.TWchar"/></param>
		internal static void SetChar (TWstring target, int index, TWchar chr)
		{
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (target.Length - index <= 0)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::SetChar () The targetIndex exceeds target's length.");
				return;
			}
			
			lock (target._string)
			{
				target._string[index] = chr;
			}
		}
			
		/// <summary>Copy an amount of characters from a source character array to a target character array.</summary>
		/// <param name="target">The character array to copy to.<see cref="X11.TWchar[]"/></param>
		/// <param name="targetIndex">The character position index to start writing character to.<see cref="System.Int32"/></param>
		/// <param name="source">The character array to copy from.<see cref="X11.TWchar[]"/></param>
		/// <param name="sourceIndex">The character position index to start reading character from.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to copy.<see cref="System.Int32"/></param>
		private static void CharCopy (TWchar[] target, int targetIndex, TWchar[] source, int sourceIndex, int count)
		{
			for (int index = 0; index < count; index++)
				target[targetIndex + index] = source[sourceIndex + index];
		}
		
		/// <summary>Copy the characters from source to target.</summary>
		/// <param name="target">The target to copy the characters to.<see cref="X11.TWstring"/></param>
		/// <param name="source">The source to copy the characters from.<see cref="X11.TWchar[]"/></param>
		internal static void CharCopy (TWstring target, TWchar[] source)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () Argument null: source");
				return;
			}
			
			lock (target._string)
			{
				if (target._string.Length < source.Length)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The source has more characters than the target.");
				else
					CharCopy (target._string, 0, source, 0, source.Length);
			}
		}
		
		/// <summary>Copy the characters from source to target.</summary>
		/// <param name="target">The target to copy the characters to.<see cref="X11.TWstring"/></param>
		/// <param name="source">The source to copy the characters from.<see cref="X11.TWstring"/></param>
		internal static void CharCopy (TWstring target, TWstring source)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () Argument null: source");
				return;
			}
			
			lock (target._string)
			{
				if (target._string != source._string) // Prevent a dead-lock (performs an adress comparison)!
				{
					lock (source._string)
					{
						if (target._string.Length < source._string.Length)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The source has more characters than the target.");
						else
							CharCopy (target._string, 0, source._string, 0, source.Length);
					}
				}
				else
					CharCopy (target._string, 0, source._string, 0, source.Length);
			}
		}
		
		/// <summary>Copy an amount of characters from a source string to a target string.</summary>
		/// <param name="target">The string to copy to.<see cref="X11.TWstring"/></param>
		/// <param name="targetIndex">The character position index to start writing character to.<see cref="System.Int32"/></param>
		/// <param name="source">The string to copy from.<see cref="X11.TWchar[]"/></param>
		/// <param name="sourceIndex">The character array to copy from.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to copy.<see cref="System.Int32"/></param>
		internal static void CharCopy (TWstring target, int targetIndex, TWchar[] source, int sourceIndex, int count)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () Argument null: source");
				return;
			}

			lock (target._string)
			{
				if (target._string.Length - targetIndex <= 0)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The targetIndex exceeds target's length.");
				else if (source.Length - sourceIndex <= 0)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The sourceIndex exceeds source's length.");
				else if (target._string.Length - targetIndex < count)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The target has insufficient length to copy count characters.");
				else if (source.Length - sourceIndex < count)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The source, starting at sourceIndex, hasn't count characters.");
				else
					CharCopy (target._string, targetIndex, source, sourceIndex, count);
			}
		}
		
		/// <summary>Copy an amount of characters from a source string to a target string.</summary>
		/// <param name="target">The string to copy to.<see cref="X11.TWstring"/></param>
		/// <param name="targetIndex">The character position index to start writing character to.<see cref="System.Int32"/></param>
		/// <param name="source">The string to copy from.<see cref="X11.TWstring"/></param>
		/// <param name="sourceIndex">The character array to copy from.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to copy.<see cref="System.Int32"/></param>
		internal static void CharCopy (TWstring target, int targetIndex, TWstring source, int sourceIndex, int count)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () Argument null: source");
				return;
			}

			lock (target._string)
			{
				if (target._string != source._string) // Prevent a dead-lock (performs an adress comparison)!
				{
					lock (source._string)
					{
						if (target._string.Length - targetIndex <= 0)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The targetIndex exceeds target's length.");
						else if (source._string.Length - sourceIndex <= 0)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The sourceIndex exceeds source's length.");
						else if (target._string.Length - targetIndex < count)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The target has insufficient length to copy count characters.");
						else if (source._string.Length - sourceIndex < count)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The source, starting at sourceIndex, hasn't count characters.");
						else
							CharCopy (target._string, targetIndex, source._string, sourceIndex, count);
					}
				}
				else
				{
					if (target._string.Length - targetIndex <= 0)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The targetIndex exceeds target's length.");
					else if (source._string.Length - sourceIndex <= 0)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The sourceIndex exceeds source's length.");
					else if (target._string.Length - targetIndex < count)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The target has insufficient length to copy count characters.");
					else if (source._string.Length - sourceIndex < count)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopy () The source, starting at sourceIndex, hasn't count characters.");
					else
						CharCopy (target._string, targetIndex, source._string, sourceIndex, count);
				}
			}
		}
			
		/// <summary>Copy an amount of characters from a source character array to a target character array. The copy loop is counting down, not up as usual.</summary>
		/// <param name="target">The character array to copy to.<see cref="X11.TWchar[]"/></param>
		/// <param name="targetIndex">The character position index to start writing character to.<see cref="System.Int32"/></param>
		/// <param name="source">The character array to copy from.<see cref="X11.TWchar[]"/></param>
		/// <param name="sourceIndex">The character position index to start reading character from.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to copy.<see cref="System.Int32"/></param>
		/// <remarks>This approach is useful, if target and source share the same string (and target is longer than source).</remarks>
		private static void CharCopyReverse (TWchar[] target, int targetIndex, TWchar[] source, int sourceIndex, int count)
		{
			for (int index = count -1; index >= 0; index--)
				target[targetIndex + index] = source[sourceIndex + index];
		}
		
		/// <summary>Copy the characters from source to target. The copy loop is counting down, not up as usual.</summary>
		/// <param name="target">The target to copy the characters to.<see cref="X11.TWstring"/></param>
		/// <param name="source">The source to copy the characters from.<see cref="X11.TWchar[]"/></param>
		/// <remarks>This approach is useful, if target and source share the same character array (and target is longer than source).</remarks>
		internal static void CharCopyReverse (TWstring target, TWchar[] source)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () Argument null: source");
				return;
			}
			
			lock (target._string)
			{
				if (target._string.Length < source.Length)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The source has more characters than the target.");
				else
					CharCopyReverse (target._string, 0, source, 0, source.Length);
			}
		}
		
		/// <summary>Copy the characters from source to target. The copy loop is counting down, not up as usual.</summary>
		/// <param name="target">The target to copy the characters to.<see cref="X11.TWstring"/></param>
		/// <param name="source">The source to copy the characters from.<see cref="X11.TWstring"/></param>
		/// <remarks>This approach is useful, if target and source share the same character array (and target is longer than source).</remarks>
		internal static void CharCopyReverse (TWstring target, TWstring source)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () Argument null: source");
				return;
			}
			
			lock (target._string)
			{
				if (target._string != source._string) // Prevent a dead-lock (performs an adress comparison)!
				{
					lock (source._string)
					{
						if (target._string.Length < source._string.Length)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The source has more characters than the target.");
						else
							CharCopyReverse (target._string, 0, source._string, 0, source.Length);
					}
				}
				else
					CharCopyReverse (target._string, 0, source._string, 0, source.Length);
			}
		}
		
		/// <summary>Copy an amount of characters from a source string to a target string. The copy loop is counting down, not up as usual.</summary>
		/// <param name="target">The string to copy to.<see cref="X11.TWstring"/></param>
		/// <param name="targetIndex">The character position index to start writing character to.<see cref="System.Int32"/></param>
		/// <param name="source">The string to copy from.<see cref="X11.TWchar[]"/></param>
		/// <param name="sourceIndex">The character array to copy from.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to copy.<see cref="System.Int32"/></param>
		/// <remarks>This approach is useful, if target and source share the same character array (and target is longer than source).</remarks>
		internal static void CharCopyReverse (TWstring target, int targetIndex, TWchar[] source, int sourceIndex, int count)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () Argument null: source");
				return;
			}

			lock (target._string)
			{
				if (target._string.Length - targetIndex <= 0)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The targetIndex exceeds target's length.");
				else if (source.Length - sourceIndex <= 0)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The sourceIndex exceeds source's length.");
				else if (target._string.Length - targetIndex < count)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The target has insufficient length to copy count characters.");
				else if (source.Length - sourceIndex < count)
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The source, starting at sourceIndex, hasn't count characters.");
				else
					CharCopyReverse (target._string, targetIndex, source, sourceIndex, count);
			}
		}
		
		/// <summary>Copy an amount of characters from a source string to a target string. The copy loop is counting down, not up as usual.</summary>
		/// <param name="target">The string to copy to.<see cref="X11.TWstring"/></param>
		/// <param name="targetIndex">The character position index to start writing character to.<see cref="System.Int32"/></param>
		/// <param name="source">The string to copy from.<see cref="X11.TWstring"/></param>
		/// <param name="sourceIndex">The character array to copy from.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to copy.<see cref="System.Int32"/></param>
		/// <remarks>This approach is useful, if target and source share the same character array (and target is longer than source).</remarks>
		internal static void CharCopyReverse (TWstring target, int targetIndex, TWstring source, int sourceIndex, int count)
		{
			if (object.Equals(target, null) && object.Equals(source, null)) // Prevent cyclic calls.
				return;
			
			if (object.Equals(target, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("target");
			
			if (object.Equals(source, null)) // Prevent cyclic calls.
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () Argument null: source");
				return;
			}

			lock (target._string)
			{
				if (target._string != source._string) // Prevent a dead-lock (performs an adress comparison)!
				{
					lock (source._string)
					{
						if (target._string.Length - targetIndex <= 0)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The targetIndex exceeds target's length.");
						else if (source._string.Length - sourceIndex <= 0)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The sourceIndex exceeds source's length.");
						else if (target._string.Length - targetIndex < count)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The target has insufficient length to copy count characters.");
						else if (source._string.Length - sourceIndex < count)
							SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The source, starting at sourceIndex, hasn't count characters.");
						else
							CharCopyReverse (target._string, targetIndex, source._string, sourceIndex, count);
					}
				}
				else
				{
					if (target._string.Length - targetIndex <= 0)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The targetIndex exceeds target's length.");
					else if (source._string.Length - sourceIndex <= 0)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The sourceIndex exceeds source's length.");
					else if (target._string.Length - targetIndex < count)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The target has insufficient length to copy count characters.");
					else if (source._string.Length - sourceIndex < count)
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CharCopyReverse () The source, starting at sourceIndex, hasn't count characters.");
					else
						CharCopyReverse (target._string, targetIndex, source._string, sourceIndex, count);
				}
			}
		}
		
		/// <summary>Indicates whether the specified string is null or an Empty string.</summary>
		/// <param name="str">The string to test.<see cref="X11.TWstring"/></param>
		/// <returns>True if the str parameter is null or an empty string, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool IsNullOrEmpty (TWstring str)
		{
			if (object.Equals(str, null)) // Prevent cyclic calls.
				return true;
			
			bool empty = false;
			lock (str._string)
			{
				if (str._string.Length == 0)
					empty = true;
			}
			return empty;
		}
		
		/// <summary>Compare two specified strings and indicate whether the first string precedes, follows, or appears in the same position in the sort order as the second string.</summary>
		/// <param name="strA">The first string to compare.<see cref="X11.TWstring"/></param>
		/// <param name="strB">The second string to compare.<see cref="X11.TWstring"/></param>
		/// <returns>-1 if strA is is less than strB, 1 if strA is greater than strB or 0 if strA is equal to strB.<see cref="System.Int32"/></returns>
		public static int Compare (TWstring strA, TWstring strB)
		{	return Compare (strA, 0, strB);		}
		
		/// <summary>Compare two specified strings and indicate whether the first string precedes, follows, or appears in the same position in the sort order as the second string.</summary>
		/// <param name="strA">The first string to compare.<see cref="X11.TWstring"/></param>
		/// <param name="startIndex">The compare starting zero-based index in charsA.<see cref="System.Int32"/></param>
		/// <param name="strB">The second string to compare.<see cref="X11.TWstring"/></param>
		/// <returns>-1 if strA is is less than strB, 1 if strA is greater than strB or 0 if strA is equal to strB.<see cref="System.Int32"/></returns>
		public static int Compare (TWstring strA, int startIndex, TWstring strB)
		{
			if ( object.Equals(strA, null) && !object.Equals(strB, null)) // Prevent cyclic calls.
				return -1;
			if ( object.Equals(strA, null) &&  object.Equals(strB, null)) // Prevent cyclic calls.
				return 0;
			if (!object.Equals(strA, null) &&  object.Equals(strB, null)) // Prevent cyclic calls.
				return 1;
			
			int result = 0;
			
			lock (strA._string)
			{
				if (strA._string != strB._string) // Prevent a dead-lock (performs an adress comparison)!
				{
					lock (strB._string)
					{
						result = TWstring.Compare (strA._string, startIndex, strB._string);
					}
				}
				else
				{
					if (startIndex == 0)
						result = 0;
					else
						result = TWstring.Compare (strA._string, startIndex, strB._string);
				}
			}
			
			return result;
		}
		
		/// <summary>Compare two specified arrays of chars and indicate whether the first string precedes, follows, or appears in the same position in the sort order as the second string.</summary>
		/// <param name="charsA">The first character array to compare.<see cref="X11.TWchar[]"/></param>
		/// <param name="charsB">The second character array to compare.<see cref="X11.TWchar[]"/></param>
		/// <returns>-1 if strA is is less than strB, 1 if strA is greater than strB or 0 if strA is equal to strB.<see cref="System.Int32"/></returns>
		public static int Compare (TWchar[] charsA, TWchar[] charsB)
		{	return TWstring.Compare (charsA, 0, charsB);	}
		
		/// <summary>Compare two specified arrays of chars and indicate whether the first string precedes, follows, or appears in the same position in the sort order as the second string.</summary>
		/// <param name="charsA">The first character array to compare.<see cref="X11.TWchar[]"/></param>
		/// <param name="startIndex">The compare starting zero-based index in charsA.<see cref="System.Int32"/></param>
		/// <param name="charsB">The second character array to compare.<see cref="X11.TWchar[]"/></param>
		/// <returns>-1 if strA is is less than strB, 1 if strA is greater than strB or 0 if strA is equal to strB.<see cref="System.Int32"/></returns>
		public static int Compare (TWchar[] charsA, int startIndex, TWchar[] charsB)
		{
			if (charsA == null && charsB != null)
				return -1;
			if (charsA == null && charsB == null)
				return 0;
			if (charsA != null && charsB == null)
				return 1;
			
			if (charsA.Length == 0 || startIndex < 0 || startIndex >= charsA.Length)
				return -1;
			
			for (int index = 0; index < charsA.Length - startIndex && index < charsB.Length; index++)
			{
				if (charsA[startIndex + index] < charsB[index])	// Works with NULL_CHAR as well.
					return -1;
				if (charsA[startIndex + index] > charsB[index])	// Works with NULL_CHAR as well.
					return 1;
			}
			
			if (charsA.Length - startIndex < charsB.Length)
				return -1;
			if (charsA.Length - startIndex > charsB.Length)
				return 1;

			return 0;			
		}
		
		/// <summary>Test whether indicated character is any of the known white space characters.</summary>
		/// <param name="chr">The character to test.<see cref="X11.TWchar"/></param>
		/// <returns>True, if indicated character is any of the known white space characters, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool IsWhiteSpace (TWchar chr)
		{
			for (int index = 0;  index < WhiteChars.Length; index++)
				if (WhiteChars[index] == chr)
					return true;
			return false;
		}
		
		/// <summary>Replaces the format items in a specified string with the string representations of corresponding objects in a specified array.
		/// The provider parameter supplies culture-specific formatting information.</summary>
		/// <param name="result">A prepared string builder instance to use as return value, or null to create a new string builder instance to use as return value.<see cref="X11.TWstringBuilder"/></param>
		/// <param name="provider">The provider to supply culture-specific formatting information.<see cref="IFormatProvider"/></param>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="args">An object array that contains zero or more objects to format. <see cref="System.Object[]"/></param>
		/// <returns>A string builder containing a copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.<see cref="X11.TWstringBuilder"/></returns>
		internal static TWstringBuilder FormatHelper (TWstringBuilder result, IFormatProvider provider, TWstring format, params object[] args)
		{
			if (format == null)
				throw new ArgumentNullException ("format");
			
			if (args == null)
				throw new ArgumentNullException ("args");
			
			// Try to approximate the size of result to avoid reallocations.
			if (result == null)
			{
				int i = 0;
				int len = 0;

				for (i = 0; i < args.Length; ++i)
				{
					string s = args [i] as string;
					if (s != null)
						len += s.Length;
					else
						break;
				}
				if (i == args.Length)
					result = new TWstringBuilder (len + format.Length);
				else
					result = new TWstringBuilder ();
			}
			
			int currentCharIndex		= 0;
			int lastProcessedCharIndex	= currentCharIndex;
			
			// Get the object that provides formatting services for ICustomFormatter. Otherwise the build-in formatter is used.
			var formatter = provider != null ? provider.GetFormat (typeof (ICustomFormatter)) as ICustomFormatter : null;

			while (currentCharIndex < format.Length)
			{
				TWchar c = format[currentCharIndex ++];

				if (c == (TWchar)'{')
				{
					// Append processes (leading) format substring to reault.
					result.Append (format, lastProcessedCharIndex, currentCharIndex - lastProcessedCharIndex - 1);

					// Check for escaped open bracket (now currentCharIndex points to the second '{' of "{{").
					if (format[currentCharIndex] == (TWchar)'{')
					{
						// The escaped '{' (but not the escaping '{') shall become part of the result.
						lastProcessedCharIndex = currentCharIndex ++;
						continue;
					}

					// Parse format specifier (argument-number[,[+|-]display-width[:argument-format]]).				
					int			argNumber;
					int			dispWidth;
					bool		leftAlign;
					TWstring	argFormat;

					ParseFormatSpecifier (format, ref currentCharIndex, out argNumber, out dispWidth, out leftAlign, out argFormat);
					if (argNumber < 0 || argNumber >= args.Length)
						throw new FormatException ("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");

					// Format the argument.
					object arg = args[argNumber];

					TWstring str;
					if (arg == null)
						str = TWstring.Empty;
					else if (formatter != null)
						str = new TWstring (formatter.Format (argFormat.ToString (), arg, provider));
					else
						str = null;

					if (str == null)
					{
						if (arg is IFormattable)
							str = new TWstring(((IFormattable)arg).ToString (argFormat.ToString (), provider));
						else
							str = new TWstring (arg.ToString () ?? "");
					}

					// Pad formatted string and append to result.
					if (dispWidth > str.Length)
					{
						const TWchar padchar = (TWchar)' ';
						int padlen = dispWidth - str.Length;

						if (leftAlign)
						{
							result.Append (str);
							result.Append (padchar, padlen);
						}
						else
						{
							result.Append (padchar, padlen);
							result.Append (str);
						}
					}
					else
						result.Append (str);

					lastProcessedCharIndex = currentCharIndex;
				}
				// Check for escaped close bracket (now currentCharIndex points to the second '}' of "}}").
				else if (c == (TWchar)'}' && currentCharIndex < format.Length && format[currentCharIndex] == (TWchar)'}')
				{
					// The escaped '}' (but not the escaping '}') shall become part of the result.
					result.Append (format, lastProcessedCharIndex, currentCharIndex - lastProcessedCharIndex - 1);
					lastProcessedCharIndex = currentCharIndex ++;
				}
				// A close bracket, that belongs to a format specifier, has already been processed by ParseFormatSpecifier().
				else if (c == (TWchar)'}')
				{
					throw new FormatException ("The format string was not in a correct format.");
				}
			}
			
			// Append remaining (tailing) format substring to reault.
			if (lastProcessedCharIndex < format.Length)
				result.Append (format, lastProcessedCharIndex, format.Length - lastProcessedCharIndex);

			return result;
		}
		
		/// <summary>Parse one single format specifier, starting after the leading '{' up to (including) the tailing '}'.</summary>
		/// <param name="format">The string, containing the format specifier to parse at zero-based currentCharIndex.<see cref="X11.TWstring"/></param>
		/// <param name="currentCharIndex">The zero-based position index in format.<see cref="System.Int32"/></param>
		/// <param name="argNumber">The pased zero-based index of the argument to format.<see cref="System.Int32"/></param>
		/// <param name="dispWidth">The parsed display width (length) of the formatted argument.<see cref="System.Int32"/></param>
		/// <param name="leftAlign">The parsed alignment of the formatted argument, if display width has to be padded with spaces.<see cref="System.Boolean"/></param>
		/// <param name="argFormat">The parsed format to apply to the argument.<see cref="System.String"/></param>
		private static void ParseFormatSpecifier (TWstring format, ref int currentCharIndex, out int argNumber,
		                                          out int dispWidth, out bool leftAlign, out TWstring argFormat)
		{
			int max = format.Length;
			
			// Parse format specifier of form: N,[\ +[-]M][:F]}
			// Where:
			
			// N = argument number (non-negative integer)
			argNumber = ParseDecimal (format, ref currentCharIndex);
			if (argNumber < 0)
				throw new FormatException ("Input string was not in a correct format.");
			
			// M = formatted field width (non-negative integer for right-aligned or negative integer for left-aligned)
			if (currentCharIndex < max && format[currentCharIndex] == (TWchar)',')
			{
				// White space between ',' and number or sign.
				currentCharIndex++;
				while (currentCharIndex < max && IsWhiteSpace (format [currentCharIndex]))
					currentCharIndex++;
				
				int start = currentCharIndex;
				
				argFormat = format.Substring (start, currentCharIndex - start);
				
				leftAlign = (currentCharIndex < max && format [currentCharIndex] == (TWchar)'-');
				if (leftAlign)
					++ currentCharIndex;
				
				dispWidth = ParseDecimal (format, ref currentCharIndex);
				if (dispWidth < 0)
					throw new FormatException ("Input string was not in a correct format.");
			}
			else
			{
				dispWidth = 0;
				leftAlign = false;
				argFormat = TWstring.Empty;
			}
			
			// F = argument format (string)
			if (currentCharIndex < max && format[currentCharIndex] == (TWchar)':')
			{
				int start = ++ currentCharIndex;
				while (currentCharIndex < max)
				{
					if (format [currentCharIndex] == (TWchar)'}')
					{
						if (currentCharIndex + 1 < max && format [currentCharIndex + 1] == (TWchar)'}')
						{
							++currentCharIndex;
							argFormat += format.Substring (start, currentCharIndex - start);
							++currentCharIndex;
							start = currentCharIndex;
							continue;
						}

						break;
					}

					++currentCharIndex;
				}

				argFormat += format.Substring (start, currentCharIndex - start);
			}
			else
				argFormat = null;
			
			if ((currentCharIndex >= max) || format[currentCharIndex ++] != (TWchar)'}')
				throw new FormatException ("Input string was not in a correct format.");
		}
		
		/// <summary>Parse a (positive) decimal value from the indicated string, starting at indicated posiotion index.</summary>
		/// <param name="str">The string to parse an decimal value from.<see cref="X11.TWstring"/></param>
		/// <param name="index">The zero-based posiotion index to start parsing from [in] and
		/// the next zero-based posiotion index after the last parsed numerical charachter [out].<see cref="System.Int32"/></param>
		/// <returns>The parsed decimal value on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		/// <remarks>Parsing continues until the forst non-numerical (!= '0' ... '9') charachter is reached.</remarks>
		private static int ParseDecimal (TWstring str, ref int index)
		{
			int currentIndex	= index;
			int result			= 0;
			int maxLength		= str.Length;
			
			while (currentIndex < maxLength)
			{
				TWchar c = str[currentIndex];
				if (c < (TWchar)'0' || (TWchar)'9' < c)
					break;

				result = result * 10 + (int)(c - (TWchar)'0');
				currentIndex++;
			}

			if (currentIndex == index || currentIndex == maxLength)
				return -1;

			index = currentIndex;
			return result;
		}
		
		#endregion Static methods
		
		#region Methods (compare)
		
		/// <summary>Determine whether the current instance of string and a specified object, which must also be a string, have the same value. (Overrides Object.Equals(Object).)</summary>
		/// <param name="other">The string to compare with the current instance of string.<see cref="System.Object"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool Equals (object other)
		{	return (CompareTo (other) == 0);	}
		
		/// <summary>Determine whether the current instance of string and a specified string have the same value.</summary>
		/// <param name="other">The string to compare with the current instance of string.<see cref="X11.TWstring"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals (TWstring other)
		{	return (CompareTo (other) == 0);	}
		
		/// <summary>Determine whether the current instance of string and a specified array of chars have the same value.</summary>
		/// <param name="str">The array of chars to compare with the current instance of string.<see cref="X11.TWchar[]"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals (TWchar[] chars)
		{	return (CompareTo (chars) == 0);	}
		
		/// <summary>Compares the current instance of string with a specified object, which must also be a string, and indicate whether this instance of string
		/// precedes, follows, or appears in the same position in the sort order as the specified string. </summary>
		/// <param name="other">The string to compare with the current instance of string.<see cref="System.Object"/></param>
		/// <returns>-1 if the current instance of string is is less than the string to compare to,
		/// 1 if the current instance of string is greater than the string to compare to or
		/// 0 if the current instance of string is equal to the string to compare to.<see cref="System.Int32"/></returns>
		public int CompareTo (object other)
		{	return this.CompareTo (other as TWstring);	}
		
		/// <summary>Compares the current instance of string with a specified string and indicate whether this instance of string
		/// precedes, follows, or appears in the same position in the sort order as the specified string.</summary>
		/// <param name="other">The string to compare with the current instance of string.<see cref="X11.TWstring"/></param>
		/// <returns>-1 if the current instance of string is is less than the string to compare to,
		/// 1 if the current instance of string is greater than the string to compare to or
		/// 0 if the current instance of string is equal to the string to compare to.<see cref="System.Int32"/></returns>
		public int CompareTo (TWstring other)
		{
			// Prevent infinite loop and use object.ReferenceEquals()!
			if (object.ReferenceEquals(other, null))
				return 1;
			
			int result = 0;
			
			lock (_string)
			{
				if (_string != other._string) // Prevent a double-(dead-)lock (performs an adress compare)!
				{
					lock (other._string)
					{
						result = TWstring.Compare (_string, 0, other._string);
					}
				}
				else
					result = TWstring.Compare (_string, 0, other._string);
			}
			
			return result;
		}
		
		/// <summary>Compares the current instance of string with a specified array of chars and indicate whether this instance of string
		/// precedes, follows, or appears in the same position in the sort order as the specified string.</summary>
		/// <param name="chars">The arrays of chars to compare with the current instance of string.<see cref="X11.TWchar[]"/></param>
		/// <returns>-1 if the current instance of string is is less than the string to compare to,
		/// 1 if the current instance of string is greater than the string to compare to or
		/// 0 if the current instance of string is equal to the string to compare to.<see cref="System.Int32"/></returns>
		public int CompareTo (TWchar[] chars)
		{
			if (chars == null)
				return 1;
			
			int result = 0;
			
			lock (_string)
			{
				result = TWstring.Compare (_string, 0, chars);
			}
			
			return result;
		}
		
		#endregion Methods (compare)
		
		#region Methods (concat)
		
		/// <summary>Concatenates two specified strings.</summary>
		/// <param name="charsA">The first character array to concatenate.<see cref="X11.TWchar[]"/></param>
		/// <param name="charsB">The second character array to concatenate.<see cref="X11.TWchar[]"/></param>
		/// <returns>A new string containing the concatenation of strA and strB.<see cref="X11.TWchar[]"/></returns>
		public static TWchar[] Concat (TWchar[] charsA, TWchar[] charsB)
		{
			int rawLengthA = (charsA != null ? charsA.Length : 0);
			int rawLengthB = (charsB != null ? charsB.Length : 0);
			
			TWchar[] result = new TWchar[rawLengthA + rawLengthB];
			
			for (int index = 0; index < rawLengthA; index++)
				result[index] = charsA[index];

			for (int index = 0; index < rawLengthB; index++)
				result[rawLengthA + index] = charsB[index];
			
			return result;
		}
		
		/// <summary>Concatenates two specified strings.</summary>
		/// <param name="strA">The first string to concatenate.<see cref="X11.TWstring"/></param>
		/// <param name="strB">The second string to concatenate.<see cref="X11.TWstring"/></param>
		/// <returns>A new string containing the concatenation of strA and strB.<see cref="X11.TWstring"/></returns>
		public static TWstring Concat (TWstring strA, TWstring strB)
		{
			TWchar[] result = null;
			
			if (!object.Equals(strA, null)) // Prevent cyclic calls.
			{
				lock (strA._string)
				{
					if (!object.Equals(strB, null)) // Prevent cyclic calls.
					{
						if (strA._string != strB._string) // Prevent a double-(dead-)lock (performs an adress compare)!
						{
							lock (strB._string)
							{
								result = TWstring.Concat(strA._string, strB._string);
							}
						}
						else
							result = TWstring.Concat(strA._string, strB._string);
					}
					else
						result = TWstring.Concat(strA._string, null);
				}
			}
			else
			{
				if (!object.Equals(strB, null)) // Prevent cyclic calls.
				{
					lock (strB._string)
					{
						result = TWstring.Concat(null, strB._string);
					}
				}
			}
			
			return new TWstring (result);
		}
		
		#endregion Methods (concat)
		
		#region Methods (indexing)
		
		/// <summary>Determine the zero-based index of the first occurrence of the specified character in this string.</summary>
		/// <param name="character">The character to seek.<see cref="X11.TWchar"/></param>
		/// <returns>The zero-based index position of character to seek on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (TWchar character)
		{	return IndexOf (0, character);	}
		
		/// <summary>Determine the zero-based index of the first occurrence of the specified character in this string.</summary>
		/// <param name="startIndex">The seek starting zero-based index.<see cref="System.Int32"/></param>
		/// <param name="character">The character to seek.<see cref="X11.TWchar"/></param>
		/// <returns>The zero-based index position of character to seek on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (int startIndex, TWchar character)
		{
			int result = -1;
			
			lock (_string)
			{
				if (_string.Length == 0 || startIndex < 0 || startIndex >= _string.Length)
					result = -1;
				else
				{
					for (int index = startIndex; index < _string.Length; index++)
					{	if (_string[index] == character)
						{	result = index;
							break;
						}
					}
				}
			}
			return result;
		}
		
		/// <summary>Determine the zero-based index of the first occurrence of the specified string in this string.</summary>
		/// <param name="chars">The character array to seek.<see cref="X11.TWchar[]"/></param>
		/// <returns>The zero-based index position of character to seek on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (TWchar[] chars)
		{	return this.IndexOf (0, chars);	}
		
		/// <summary>Determine the zero-based index of the first occurrence of the specified string in this string.</summary>
		/// <param name="startIndex">The seek starting zero-based index.<see cref="System.Int32"/></param>
		/// <param name="chars">The character array to seek.<see cref="X11.TWchar[]"/></param>
		/// <returns>The zero-based index position of character to seek on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (int startIndex, TWchar[] chars)
		{
			int result = -1;
			
			lock (_string)
			{
				if (_string.Length == 0 || startIndex < 0 || startIndex >= _string.Length)
					result = -1;
				else
				{
					if (chars == null || chars.Length == 0)
						result = -1;
					else
					{
						for (int index = startIndex; index <= _string.Length - chars.Length; index++)
						{
							if (_string[index] == chars[0])
							{
								bool match = true;
								// Because:      index =< _string.Length - chars.Length
								// Consequently: chars.Length <= _string.Length - index
								// And if:       subIndex < chars.Length
								// Also:         subIndex < _string.Length - index
								// Hence:        No overflow is possible.
								for (int subIndex = 1; subIndex < chars.Length; subIndex++)
								{
									if (_string[index + subIndex] != chars[subIndex])
									{
										match = false;
										break;
									}
								}
								if (match == true)
								{
									result = index;
									break;
								}
							}
						}
					}
				}
			}
			return result;
		}
		
		/// <summary>Determine the zero-based index of the first occurrence of the specified string in this string.</summary>
		/// <param name="str">The string to seek.<see cref="X11.TWstring"/></param>
		/// <returns>The zero-based index position of character to seek on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (TWstring str)
		{	
			if (object.Equals(str, null)) // Prevent cyclic calls.
				return -1;
			
			int result = -1;
			
			// This will be locked by this.IndexOf (...);
			if (this._string != str._string) // Prevent a double-(dead-)lock (performs an adress compare)!
			{
				lock (str._string)
				{
					result = this.IndexOf (0, str._string);
				}
			}
			else
				result = this.IndexOf (0, str._string);
			
			return result;
		}
		
		/// <summary>Determine the zero-based index of the first occurrence of the specified string in this string.</summary>
		/// <param name="startIndex">The seek starting zero-based index.<see cref="System.Int32"/></param>
		/// <param name="str">The string to seek.<see cref="X11.TWstring"/></param>
		/// <returns>The zero-based index position of character to seek on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (int startIndex, TWstring str)
		{
			if (object.Equals(str, null)) // Prevent cyclic calls.
				return -1;
				
			int result = -1;
			
			// This will be locked by this.IndexOf (...);
			if (this._string != str._string) // Prevent a double-(dead-)lock (performs an adress compare)!
			{
				lock (str._string)
				{
					result = this.IndexOf (startIndex, str._string);
				}
			}
			else
				result = this.IndexOf (startIndex, str._string);
				
			return result;
		}
		
		#endregion Methods (indexing)
		
		#region Methods (formatting)
		
		/// <summary>Replace one or more format items in a specified string with the string representation of a specified object.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="arg0">The object to format.<see cref="System.Object"/></param>
		/// <returns>A copy of format in which any format items are replaced by the string representation of arg0.<see cref="X11.TWstring"/></returns>
		public static TWstring Format (TWstring format, object arg0)
		{
			TWstringBuilder b = FormatHelper (null, null, format, new Object[] {arg0});
			return b.ToTWstring ();
		}
		
		/// <summary>Replace one or more format items in a specified string with the string representation of two specified objects.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="arg0">The first object to format.<see cref="System.Object"/></param>
		/// <param name="arg1">The second object to format.<see cref="System.Object"/></param>
		/// <returns>A copy of format in which format items are replaced by the string representations of arg0 and arg1.<see cref="X11.TWstring"/></returns>
		public static TWstring Format (TWstring format, object arg0, object arg1)
		{
			TWstringBuilder b = FormatHelper (null, null, format, new Object[] {arg0, arg1});
			return b.ToTWstring ();
		}

		/// <summary>Replace one or more format items in a specified string with the string representation of three specified objects.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="arg0">The first object to format.<see cref="System.Object"/></param>
		/// <param name="arg1">The second object to format.<see cref="System.Object"/></param>
		/// <param name="arg2">The third object to format.<see cref="System.Object"/></param>
		/// <returns>A copy of format in which format items are replaced by the string representations of arg0, arg1, and arg2.<see cref="X11.TWstring"/></returns>
		public static TWstring Format (TWstring format, object arg0, object arg1, object arg2)
		{
			TWstringBuilder b = FormatHelper (null, null, format, new object[] {arg0, arg1, arg2});
			return b.ToTWstring ();
		}
		
		/// <summary>Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="args">An object array that contains zero or more objects to format. <see cref="System.Object[]"/></param>
		/// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.<see cref="X11.TWstring"/></returns>
		public static TWstring Format (TWstring format, params object[] args)
		{
			TWstringBuilder b = FormatHelper (null, null, format, args);
			return b.ToTWstring ();
		}
		
		#endregion Methods (formatting)
		
		#region Methods (Split)
		
		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified character array.</summary>
		/// <param name="separator">An array of characters that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWchar[]"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more characters in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter characters are not included in the elements of the returned array. Each element of separator defines a separate delimiter character.
		/// If this string instance does not contain any of the characters in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no characters, white-space characters are assumed to be the delimiters.</remarks>
		/// <remarks>No empty entries are removed from the result: If two delimiters are adjacent,
		/// or a delimiter is found at the beginning or end of this string instance, the corresponding array element contains TWstring.Empty.</remarks>
		public TWstring [] Split (params TWchar[] separator)
		{
			return Split (separator, int.MaxValue, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified character array.
		/// A parameter specifies the maximum number of substrings to return.</summary>
		/// <param name="separator">An array of characters that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWchar[]"/></param>
		/// <param name="count">The maximum number of substrings to return. <see cref="System.Int32"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more characters in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter characters are not included in the elements of the returned array. Each element of separator defines a separate delimiter character.
		/// If this string instance does not contain any of the characters in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no characters, white-space characters are assumed to be the delimiters.</remarks>
		/// <remarks>No empty entries are removed from the return value: If two delimiters are adjacent,
		/// or a delimiter is found at the beginning or end of this string instance, the corresponding array element contains TWstring.Empty.</remarks>
		public TWstring[] Split (TWchar[] separator, int count)
		{
			return Split (separator, count, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified character array.
		/// A parameter specifies the maximum number of substrings to return.</summary>
		/// <param name="separator">An array of characters that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWchar[]"/></param>
		/// <param name="options">StringSplitOptions.RemoveEmptyEntries to omit TWstring.Empty array elements from the array returned;
		/// or StringSplitOptions.None to include TWstring.Empty array elements in the array returned.<see cref="StringSplitOptions"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more characters in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter characters are not included in the elements of the returned array. Each element of separator defines a separate delimiter character.
		/// If this string instance does not contain any of the characters in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no characters, white-space characters are assumed to be the delimiters.</remarks>
		public TWstring[] Split (TWchar[] separator, StringSplitOptions options)
		{
			return Split (separator, Int32.MaxValue, options);
		}
		
		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified character array.
		/// Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <param name="separator">An array of characters that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWchar[]"/></param>
		/// <param name="count">The maximum number of substrings to return. <see cref="System.Int32"/></param>
		/// <param name="options">StringSplitOptions.RemoveEmptyEntries to omit TWstring.Empty array elements from the array returned;
		/// or StringSplitOptions.None to include TWstring.Empty array elements in the array returned.<see cref="StringSplitOptions"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more characters in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter characters are not included in the elements of the returned array. Each element of separator defines a separate delimiter character.
		/// If this string instance does not contain any of the characters in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no characters, white-space characters are assumed to be the delimiters.</remarks>
		public TWstring[] Split (TWchar[] separator, int count, StringSplitOptions options)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count", "Count cannot be less than zero.");
			
			if ((options != StringSplitOptions.None) && (options != StringSplitOptions.RemoveEmptyEntries))
				throw new ArgumentException ("Illegal enum value: " + options + ".", "options");

			bool removeEmpty = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

			if (_string.Length == 0 && removeEmpty)
				return new TWstring[0];

			if (count == 0)
				return new TWstring[0];
			
			if (count == 1)
				return new TWstring[] { this };

			return SplitByCharacters (separator, count, removeEmpty);
		}
		
		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified character array.
		/// Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <param name="separator">An array of characters that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWchar[]"/></param>
		/// <param name="count">The maximum number of substrings to return. <see cref="System.Int32"/></param>
		/// <param name="removeEmpty">Determine whether to omit TWstring.Empty array elements, or not.<see cref="System.Boolean"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more characters in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter characters are not included in the elements of the returned array. Each element of separator defines a separate delimiter character.
		/// If this string instance does not contain any of the characters in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no characters, white-space characters are assumed to be the delimiters.</remarks>
		private TWstring[] SplitByCharacters (TWchar[] separator, int count, bool removeEmpty)
		{
			if (separator == null || separator.Length == 0)
				separator = WhiteChars;

			int[]	split_points = null;
			int		total_points = 0;
			
			// Decrement count, to be able to do[A].
			count--;

			for (int index = 0; index < _string.Length; index++)
			{
				for (int subIndex = 0; subIndex < separator.Length; subIndex++)
				{
					if (_string[index] == separator[subIndex])
					{
						if (split_points == null)
						{
							// Initialize split points array.
							split_points = new int[8];
						}
						else if (total_points == split_points.Length)
						{
							// Enlarge split points array. (This method allocates a new array with the specified size, copies elements from the
							// old array to the new one, and then replaces the old array with the new one. array must be a one-dimensional array.)
							Array.Resize (ref split_points, split_points.Length * 2);
						}

						split_points[total_points] = index;
						total_points++;
						if (total_points == count && !removeEmpty)
						{
							// [A] Keep all the remaining characters for the last substring (no matter whether they contain any separator).
							break;
						}
					}
				}
			}

			if (total_points == 0)
				return new TWstring[] { this };

			var result = new TWstring[Math.Min (total_points, count) + 1];
			int prev_index = 0;
			int i = 0;
			
			if (!removeEmpty)
			{
				int length;
				
				for (; i < total_points; ++i)
				{
					int start = split_points[i];

					if (i == count)
						break;

					result[i] = Substring (prev_index, start - prev_index);
					
					prev_index = start + 1;
				}

				length = _string.Length - prev_index;
				result[i] = Substring (prev_index, length);
			}
			else // Remove sub-strings with a length of 0.
			{
				int used = 0;
				int length;
				
				for (; i < total_points; ++i)
				{
					int start = split_points[i];
					length = start - prev_index;
					
					if (length != 0)
					{
						if (used == count)
							break;

						result[used] = Substring (prev_index, length);
						used++;
					}

					prev_index = start + 1;
				}

				length = _string.Length - prev_index;
				if (length != 0)
				{
					result[used] = Substring (prev_index, length);
					used++;
				}

				if (used != result.Length)
					Array.Resize (ref result, used);
			}

			return result;
		}
		
		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified string array.</summary>
		/// <param name="separator">An array of strings that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWstring[]"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more strings in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter strings are not included in the elements of the returned array. Each element of separator defines a separate delimiter string.
		/// If this string instance does not contain any of the strings in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no strings, white-space characters are assumed to be the delimiters.</remarks>
		/// <remarks>No empty entries are removed from the result: If two delimiters are adjacent,
		/// or a delimiter is found at the beginning or end of this string instance, the corresponding array element contains TWstring.Empty.</remarks>
		public TWstring [] Split (TWstring[] separator)
		{
			return Split (separator, int.MaxValue, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified string array.
		/// A parameter specifies the maximum number of substrings to return.</summary>
		/// <param name="separator">An array of strings that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWstring[]"/></param>
		/// <param name="count">The maximum number of substrings to return. <see cref="System.Int32"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more strings in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter strings are not included in the elements of the returned array. Each element of separator defines a separate delimiter string.
		/// If this string instance does not contain any of the strings in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no strings, white-space characters are assumed to be the delimiters.</remarks>
		/// <remarks>No empty entries are removed from the return value: If two delimiters are adjacent,
		/// or a delimiter is found at the beginning or end of this string instance, the corresponding array element contains TWstring.Empty.</remarks>
		public TWstring[] Split (TWstring[] separator, int count)
		{
			return Split (separator, count, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified string array.
		/// A parameter specifies the maximum number of substrings to return.</summary>
		/// <param name="separator">An array of strings that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWstring[]"/></param>
		/// <param name="options">StringSplitOptions.RemoveEmptyEntries to omit TWstring.Empty array elements from the array returned;
		/// or StringSplitOptions.None to include TWstring.Empty array elements in the array returned.<see cref="StringSplitOptions"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more strings in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter strings are not included in the elements of the returned array. Each element of separator defines a separate delimiter string.
		/// If this string instance does not contain any of the strings in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no strings, white-space characters are assumed to be the delimiters.</remarks>
		public TWstring[] Split (TWstring[] separator, StringSplitOptions options)
		{
			return Split (separator, Int32.MaxValue, options);
		}
		
		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified string array.
		/// Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <param name="separator">An array of strings that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWstring[]"/></param>
		/// <param name="count">The maximum number of substrings to return. <see cref="System.Int32"/></param>
		/// <param name="options">StringSplitOptions.RemoveEmptyEntries to omit TWstring.Empty array elements from the array returned;
		/// or StringSplitOptions.None to include TWstring.Empty array elements in the array returned.<see cref="StringSplitOptions"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more strings in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter strings are not included in the elements of the returned array. Each element of separator defines a separate delimiter string.
		/// If this string instance does not contain any of the strings in separator, the returned array consists of a single element that contains this string instance (reference).
		/// If the separator parameter is null or contains no strings, white-space characters are assumed to be the delimiters.</remarks>
		public TWstring[] Split (TWstring[] separator, int count, StringSplitOptions options)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException ("count", "Count cannot be less than zero.");
			
			if ((options != StringSplitOptions.None) && (options != StringSplitOptions.RemoveEmptyEntries))
				throw new ArgumentException ("Illegal enum value: " + options + ".", "options");

			bool removeEmpty = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

			if (_string.Length == 0 && removeEmpty)
				return new TWstring[0];

			if (count == 0)
				return new TWstring[0];
			
			if (count == 1)
				return new TWstring[] { this };
			
			if (separator == null || separator.Length == 0)
				return SplitByCharacters (null, count, removeEmpty);
			else
				return SplitByStrings (separator, count, removeEmpty);
		}
		
		/// <summary>Returns a string array that contains the substrings in this string instance, that are delimited by elements of a specified string array.
		/// Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <param name="separator">An array of strings that delimit the substrings in this string instance, an empty array that contains no delimiters, or null. <see cref="X11.TWstring[]"/></param>
		/// <param name="count">The maximum number of substrings to return. <see cref="System.Int32"/></param>
		/// <param name="removeEmpty">Determine whether to omit TWstring.Empty array elements, or not.<see cref="System.Boolean"/></param>
		/// <returns>An array of strings whose elements contain the substrings in this string instance, that are delimited by one or more strings in separator.<see cref="X11.TWstring[]"/></returns>
		/// <remarks>Delimiter strings are not included in the elements of the returned array. Each element of separator defines a separate delimiter string.
		/// If this string instance does not contain any of the strings in separator, the returned array consists of a single element that contains this string instance (reference).</remarks>
		private TWstring[] SplitByStrings (TWstring[] separator, int count, bool removeEmpty)
		{
			List<TWstring> result = new List<TWstring> ();

			int characterIndex = 0;
			int matchCount = 0;
			while (characterIndex < _string.Length)
			{
				int matchingSeperatorIndex	= -1;
				int matchingCharacterIndex	= Int32.MaxValue;

				// Find the first position where any of the separators matches.
				for (int i = 0; i < separator.Length; ++i)
				{
					TWstring sep = separator [i];
					if (TWstring.IsNullOrEmpty (sep))
						continue;

					int match = IndexOf (characterIndex, sep);
					if (match >= 0)
					{
						// Keep the earliest match.
						if (match < matchingCharacterIndex)
						{
							matchingSeperatorIndex = i;
							matchingCharacterIndex = match;
						}
					}
				}

				if (matchingSeperatorIndex == -1)
					break;

				if (matchingCharacterIndex != characterIndex ||	// There is a non-empty string from last match to current match.
				    !removeEmpty)								// Empty strings shouldn't be removed.
				{
					if (result.Count == count - 1)
						break;
					result.Add (this.Substring (characterIndex, matchingCharacterIndex - characterIndex));
				}

				characterIndex = matchingCharacterIndex + separator [matchingSeperatorIndex].Length;

				matchCount ++;
			}

			if (matchCount == 0)
				return new TWstring [] { this };

			// This string instance containes separators only.
			if (removeEmpty		&&					// Yes, we want to remove empty strings.
			    matchCount != 0 &&					// Yes, ther have been any matches.
			    characterIndex == this.Length &&	// Yes, there are no characters left to process in this string instance.
			    result.Count == 0)					// Yes, all substrings are suppressed empty strings.
				return new TWstring[0];

			if (!removeEmpty ||						// No, we don't want to remove empty strings.
			    characterIndex != this.Length)		// Yes, there are characters left to process in this string instance.
				result.Add (this.Substring (characterIndex));

			return result.ToArray ();
		}
		
		#endregion Methods (Split)
		
		#region Methods (Trim)
		
		/// <summary>Removes all leading and trailing white-space characters from the current string instance. </summary>
		/// <returns>The string that remains after all white-space characters are removed from the start and end of the current string.<see cref="X11.TWstring"/></returns>
		public TWstring Trim ()
		{
			if (_string.Length == 0) 
				return Empty;
			
			int start = FindNotWhiteSpace (0, _string.Length, true);

			if (start == _string.Length)
				return Empty;

			int end = FindNotWhiteSpace (_string.Length - 1, start, false);

			int newLength = end - start + 1;
			return Substring (start, newLength);
		}
		
		/// <summary>Removes all leading and trailing occurrences of a set of characters specified in an array from the current string instance.</summary>
		/// <param name="trimChars">An array of characters to remove, or null.<see cref="X11.TWchar[]"/></param>
		/// <returns>The string that remains after all occurrences of the characters in the trimChars parameter are removed from the start and end of the
		/// current string instance. If trimChars is null or an empty array, white-space characters are removed instead.<see cref="X11.TWstring"/></returns>
		public TWstring Trim (params TWchar[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
				return Trim ();

			if (_string.Length == 0) 
				return Empty;
			
			int start = FindNotInTable (0, _string.Length, true, trimChars);

			if (start == _string.Length)
				return Empty;

			int end = FindNotInTable (_string.Length - 1, start, false, trimChars);

			int newLength = end - start + 1;
			return Substring (start, newLength);
		}
		
		/// <summary>Removes all leading occurrences of a set of characters specified in an array from the current string instance.</summary>
		/// <param name="trimChars">An array of characters to remove, or null to remove white spaces.<see cref="X11.TWchar[]"/></param>
		/// <returns>The string that remains after all occurrences of the characters in the trimChars parameter are removed from the start of the
		/// current string instance. If trimChars is null or an empty array, white-space characters are removed instead.<see cref="X11.TWstring"/></returns>
		public TWstring TrimStart (params TWchar[] trimChars)
		{
			if (_string.Length == 0) 
				return Empty;
			
			int start;
			if (trimChars == null || trimChars.Length == 0)
				start = FindNotWhiteSpace (0, _string.Length, true);
			else
				start = FindNotInTable (0, _string.Length, true, trimChars);

			return Substring (start, _string.Length - start);
		}
		
		/// <summary>Removes all trailing occurrences of a set of characters specified in an array from the current string instance.</summary>
		/// <param name="trimChars">An array of characters to remove, or null to remove white spaces.<see cref="X11.TWchar[]"/></param>
		/// <returns>The string that remains after all occurrences of the characters in the trimChars parameter are removed from the end of the
		/// current string instance. If trimChars is null or an empty array, white-space characters are removed instead.<see cref="X11.TWstring"/></returns>
		public TWstring TrimEnd (params TWchar[] trimChars)
		{
			if (_string.Length == 0) 
				return Empty;
			
			int end;
			if (trimChars == null || trimChars.Length == 0)
				end = FindNotWhiteSpace (_string.Length - 1, -1, false);
			else
				end = FindNotInTable (_string.Length - 1, -1, false, trimChars);

			return Substring (0, end + 1);
		}
		
		/// <summary>Find the zero-based index of the first character, that is not a white space character.</summary>
		/// <param name="start">The zero-based index to start the search from. This index WILL be included in search!<see cref="System.Int32"/></param>
		/// <param name="target">The zero-based index to end the search at. This index WILL NOT be included in search!<see cref="System.Int32"/></param>
		/// <param name="forward">The search direction. True for forward. False for backward.<see cref="System.Boolean"/></param>
		/// <returns>The zero-based index of the first character, that is not a white space character, on success, or target otherwise.<see cref="System.Int32"/></returns>
		private int FindNotWhiteSpace (int start, int target, bool forward)
		{
			if (forward)
			{
				if (start < 0)
					throw new ArgumentOutOfRangeException ("start", "Start must not underrun 0.");
				if (target > _string.Length)
					throw new ArgumentOutOfRangeException ("target", "Target must not exceed the string length.");
				
				for (int forIndex = start; forIndex < _string.Length && forIndex < target; forIndex++)
				{
					if (!TWstring.IsWhiteSpace (_string[forIndex]))
						return forIndex;
				}
				return target;
			}
			else
			{
				if (start >= _string.Length)
					throw new ArgumentOutOfRangeException ("start", "Start must not exceed the string length.");
				if (target < -1)
					throw new ArgumentOutOfRangeException ("target", "Target must not underrun -1.");
				
				for (int bakIndex = start; bakIndex >= 0 && bakIndex > target; bakIndex--)
				{
					if (!TWstring.IsWhiteSpace (_string[bakIndex]))
						return bakIndex;
				}
				return target;
			}
		}
		
		/// <summary>Find the zero-based index of the first character, that is not in table.</summary>
		/// <param name="start">The zero-based index to start the search from. This index WILL be included in search!<see cref="System.Int32"/></param>
		/// <param name="target">The zero-based index to end the search at. This index WILL NOT be included in search!<see cref="System.Int32"/></param>
		/// <param name="forward">The search direction. True for forward. False for backward.<see cref="System.Boolean"/></param>
		/// <param name="table">The array of characters to search for.<see cref="WTchar[]"/></param><returns>
		/// A <see cref="System.Int32"/>The zero-based index of the first character, that is not in table, on success, or target otherwise.</returns>
		private int FindNotInTable (int start, int target, bool forward, TWchar[] table)
		{
			if (table == null || table.Length == 0)
				throw new ArgumentNullException ("table");
			
			if (forward)
			{
				if (start < 0)
					throw new ArgumentOutOfRangeException ("start", "Start must not underrun 0.");
				if (target > _string.Length)
					throw new ArgumentOutOfRangeException ("target", "Target must not exceed the string length.");
				
				for (int forIndex = start; forIndex < _string.Length && forIndex < target; forIndex++)
				{
					for (int subIndex = 0; subIndex < table.Length; subIndex++)
					{
						if (_string[forIndex] == table[subIndex])
							return forIndex;
					}
				}
				return target;
			}
			else
			{
				if (start >= _string.Length)
					throw new ArgumentOutOfRangeException ("start", "Start must not exceed the string length.");
				if (target < -1)
					throw new ArgumentOutOfRangeException ("target", "Target must not underrun -1.");
				
				for (int bakIndex = start; bakIndex >= 0 && bakIndex > target; bakIndex--)
				{
					for (int subIndex = 0; subIndex < table.Length; subIndex++)
					{
						if (_string[bakIndex] == table[subIndex])
							return bakIndex;
					}
				}
				return target;
			}
		}
		
		#endregion Methods (Trim)
		
		#region Methods (misc)
		
		/// <summary>Return a deep copy of this instance of string.</summary>
		/// <returns>A deep copy of this instance of string.<see cref="X11.TWstring"/></returns>
		public TWstring Clone ()
		{
			return new TWstring (this);
		}
		
		/// <summary>Retrieves the hash code for this object.</summary>
		/// <returns>A 32-bit hash code, which is a signed integer.<see cref="System.Int32"/></returns>
		public override int GetHashCode () 
 		{
 			lock (_string)
			{
 				int h = 0;
				
 				for (int index = 0; index < _string.Length; index++)
				{
 					h = (h << 5) - h + (int)_string[index];
 				}
 				return h;
 			}
 		}
		
		/// <summary>Returns a new string in which all occurrences of a specified Unicode character in this instance are replaced with another specified Unicode character.</summary>
		/// <param name="oldChr">The character to be replaced. <see cref="X11.TWchar"/></param>
		/// <param name="newChr">The character to replace all occurrences of oldChar.<see cref="X11.TWchar"/></param>
		/// <returns>A string that is equivalent to this instance except that all instances of oldChar are replaced with newChar. If oldChar is not found in the current instance, the method returns the current instance unchanged.<see cref="X11.TWstring"/></returns>
		/// <remarks>This method is culture-insensitive.</remarks>
		public TWstring Replace (TWchar oldChr, TWchar newChr)
		{
			TWstring result = new TWstring (this);
			
			if (result._string.Length == 0 || oldChr == newChr)
				return result;

			int startPos = 0;
			startPos = result.IndexOf (startPos, oldChr);
			while (startPos >= 0)
			{
				result._string[startPos] = newChr;
				startPos = result.IndexOf (startPos + 1, oldChr);
			}
			
			return result;
		}
		
		/// <summary>Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.</summary>
		/// <param name="oldValue">The string to be replaced.<see cref="X11.TWstring"/></param>
		/// <param name="newValue">The string to replace all occurrences of oldValue.<see cref="X11.TWstring"/></param>
		/// <returns>A string that is equivalent to the current string except that all instances of oldValue are replaced with newValue. If oldValue is not found in the current instance, the method returns the current instance unchanged.<see cref="X11.TWstring"/></returns>
		/// <remarks>This method is culture-insensitive.</remarks>
		public TWstring Replace (TWstring oldValue, TWstring newValue)
		{
			if (object.Equals(oldValue, null)) // Prevent cyclic calls.
				throw new ArgumentNullException ("oldValue");
			
			TWstringBuilder result = new TWstringBuilder ();
			
			lock (oldValue._string)
			{
				if (oldValue._string.Length == 0)
					throw new ArgumentException ("oldValue is an empty string.");
	
				if (this.Length == 0)
					return new TWstring (this);
				
				if (object.Equals(newValue, null))
					newValue = new TWstring ();
	
				if (oldValue._string != newValue._string) // Prevent a double-(dead-)lock (performs an adress compare)!
				{
					lock (newValue._string)
					{
						int lastStartPos = 0;
						int currStartPos = 0;
						currStartPos = this.IndexOf (currStartPos, oldValue);
						while (currStartPos >= 0)
						{
							result.Append (this.Substring (lastStartPos, currStartPos - lastStartPos));
							lastStartPos = currStartPos + oldValue._string.Length;
							result.Append (newValue);
							currStartPos = this.IndexOf (currStartPos + oldValue._string.Length, oldValue);
						}
						if (lastStartPos >= 0)
							result.Append (this.Substring (lastStartPos));
					}
				}
				else
					result.Append (this);
			}
			
			return result.ToTWstring ();
		}
		
		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and continues to the end of the string.</summary>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. <see cref="System.Int32"/></param>
		/// <returns>A string that is equivalent to the substring that begins at startIndex in this instance,
		/// or Empty if startIndex is equal to the length of this instance.<see cref="X11.TWstring"/></returns>
		public TWstring Substring (int startIndex)
		{
			if (startIndex == 0)
				return new TWstring (this);
			
			if (startIndex < 0 || startIndex > _string.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (startIndex == _string.Length)
				return TWstring.Empty;
				
			TWstring result = new TWstring (_string.Length - startIndex);
			CharCopy (result, 0, this, startIndex, _string.Length - startIndex);
			
			return result;
		}
		
		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.</summary>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. <see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in the substring.<see cref="System.Int32"/></param>
		/// <returns>A string that is equivalent to the substring of length length that begins at startIndex in this instance,
		/// or Empty if startIndex is equal to the length of this instance and length is zero or length is zero.<see cref="X11.TWstring"/></returns>
		public TWstring Substring (int startIndex, int length)
		{
			if (startIndex < 0 || startIndex > _string.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || _string.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");
			
			if (startIndex == _string.Length)
				return TWstring.Empty;
				
			TWstring result = new TWstring (length);
			CharCopy (result, 0, this, startIndex, length);
			
			return result;
		}
		
		/// <summary>Returns a string that represents the this instance.</summary>
		/// <returns>A string that represents the this instance.<see cref="System.String"/></returns>
		public override string ToString ()
		{
			string result = string.Empty;
			
			lock (_string)
			{
				result = X11Utils.WcharArrayToString (_string);
			}
			
			return result;
		}

		#endregion Methods (misc)
		
        // ###############################################################################
        // ### O P E R A T O R S
        // ###############################################################################
		
		#region Operators
		
		/// <summary>Concatinate two strings.</summary>
		/// <param name="strA">The first string to add.<see cref="X11.TWstring"/></param>
		/// <param name="strB">The second string to add.<see cref="X11.TWstring"/></param>
		/// <returns>A new string representing the concatination of strA and strB.<see cref="X11.TWstring"/></returns>
        public static TWstring operator +(TWstring strA, TWstring strB)
        {   return TWstring.Concat(strA, strB);	}

        /// <summary>Evaluates two strings to determine equality.</summary>
		/// <param name="left">The first strings to compare.<see cref="X11.TWstring"/></param>
		/// <param name="right">The second strings to compare.<see cref="X11.TWstring"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator ==(TWstring left, TWstring right)
        {
		    // Prevent infinite loop and use object.ReferenceEquals()!
		    if (object.ReferenceEquals(left, right)) return true;
		    if (object.ReferenceEquals(left, null)) return false;
		    if (object.ReferenceEquals(right, null)) return false;
			
            return left.Equals (right);
        }

        /// <summary>Evaluates two strings to determine inequality.</summary>
		/// <param name="left">The first strings to compare.<see cref="X11.TWstring"/></param>
		/// <param name="right">The second strings to compare.<see cref="X11.TWstring"/></param>
		/// <returns>True on inequality, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(TWstring left, TWstring right)
        {
		    // Prevent infinite loop and use object.ReferenceEquals()!
		    if (object.ReferenceEquals(left, right)) return false;
		    if (object.ReferenceEquals(left, null)) return true;
		    if (object.ReferenceEquals(right, null)) return true;
			
            return !left.Equals (right);
        }
		
		#endregion Operators
		
	}

}