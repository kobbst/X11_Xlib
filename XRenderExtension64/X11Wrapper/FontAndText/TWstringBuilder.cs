// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: December 2014
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
using System.Diagnostics;
using System.Runtime.Serialization;

namespace X11
{
	// For additional functionality see:
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Text/StringBuilder.cs

	[Serializable]
	public sealed class TWstringBuilder : ISerializable
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string		CLASS_NAME = "TWstringBuilder";
		
		private const int		DEFAULT_CAPACITY = 16;

        #endregion Constants
			
		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The number of currently used characters in the buffer.</summary>
		private int				_length			= 0;
		
		/// <summary>The underlying buffer.</summary>
		private TWstring		_str			= TWstring.Empty;
		
		/// <summary>The last truncated buffer, ready to use.</summary>
		private TWstring		_cached_str		= null;
		
		/// <summary>The maximim possible number of characters in the buffer.</summary>
		private int				_maxCapacity	= Int32.MaxValue;

		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary>Initializes a new instance of the TWstringBuilder class.</summary>
		public TWstringBuilder ()
			: this (TWstring.Empty, 0, 0, DEFAULT_CAPACITY, Int32.MaxValue)
		{}

		/// <summary>Initializes a new instance of the TWstringBuilder class from the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of this instance.
		/// If value is null, the new TWstringBuilder will contain the empty string (that is, it contains Empty).<see cref="X11.TWstring"/></param>
		public TWstringBuilder (TWstring value)
			: this(value == null ? TWstring.Empty : value, 0, value == null ? 0 : value.Length, DEFAULT_CAPACITY, Int32.MaxValue)
		{}
		
		/// <summary>Initializes a new instance of the TWstringBuilder class from the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of this instance.
		/// If value is null, the new TWstringBuilder will contain the empty string (that is, it contains Empty).<see cref="X11.TWstring"/></param>
		/// <param name="capacity">The suggested starting number of possible characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		public TWstringBuilder( TWstring value, int capacity)
			: this(value == null ? TWstring.Empty : value, 0, value == null ? 0 : value.Length, capacity, Int32.MaxValue)
		{}
		
		/// <summary>Initializes a new instance of the TWstringBuilder class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance.
		/// If value is null, the new TWstringBuilder will contain the empty string (that is, it contains Empty).<see cref="X11.TWstring"/></param>
		/// <param name="startIndex">The position within value where the substring begins.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in the substring.<see cref="System.Int32"/></param>
		/// <param name="capacity">The suggested starting number of characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		public TWstringBuilder (TWstring value, int startIndex, int length, int capacity) 
			: this (value, startIndex, length, capacity, Int32.MaxValue)
		{}

		/// <summary>Initializes a new instance of the TWstringBuilder class with a suggested starting size.</summary>
		/// <param name="capacity">The suggested starting number of possible characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		public TWstringBuilder (int capacity)
			: this (TWstring.Empty, 0, 0, capacity, Int32.MaxValue)
		{}

		/// <summary>Initializes a new instance of the TWstringBuilder class with suggested starting size and maximum capacity.</summary>
		/// <param name="capacity">The suggested starting number of possible characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		/// <param name="maxCapacity">The maximim possible number of characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		public TWstringBuilder (int capacity, int maxCapacity)
			: this (TWstring.Empty, 0, 0, capacity, maxCapacity)
		{}

		/// <summary>Initializes a new instance of the TWstringBuilder class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance.
		/// If value is null, the new TWstringBuilder will contain the empty string (that is, it contains Empty).<see cref="X11.TWstring"/></param>
		/// <param name="startIndex">The position within value where the substring begins.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in the substring.<see cref="System.Int32"/></param>
		/// <param name="capacity">The suggested starting number of possible characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		/// <param name="maxCapacity">The maximim possible number of characters in the buffer for this TWstringBuilder class instance.<see cref="System.Int32"/></param>
		private TWstringBuilder(TWstring value, int startIndex, int length, int capacity, int maxCapacity)
		{
			// First, check the parameters and throw appropriate exceptions if needed
			if (value == null)
				value = TWstring.Empty;

			if (startIndex < 0)
				throw new System.ArgumentOutOfRangeException ("startIndex", startIndex, "StartIndex cannot be less than zero.");

			if(length < 0)
				throw new System.ArgumentOutOfRangeException ("length", length, "Length cannot be less than zero.");

			if (capacity < 0)
				throw new System.ArgumentOutOfRangeException ("capacity", capacity, "capacity must be greater than zero.");

			if (maxCapacity < 1)
				throw new System.ArgumentOutOfRangeException ("maxCapacity", "maxCapacity is less than one.");

			if (maxCapacity < DEFAULT_CAPACITY)
				throw new System.ArgumentOutOfRangeException ("maxCapacity", "maxCapacity is less than DEFAULT_CAPACITY.");
			
			if (capacity > maxCapacity)
				throw new System.ArgumentOutOfRangeException ("capacity", "Capacity exceeds maximum capacity.");

			if (startIndex > value.Length - length)
				throw new System.ArgumentOutOfRangeException ("startIndex", startIndex, "StartIndex and length must refer to a location within the string.");
			
			// Second, set current capacity and max. capacity.
			if (capacity == 0)
				capacity = DEFAULT_CAPACITY;
			_maxCapacity = maxCapacity;
			
			InternalEnsureCapacity ((length > capacity) ? length : capacity);

			if (length > 0)
				TWstring.CharCopy (_str, 0, value, startIndex, length);
			
			_length = length;
		}
		
        #endregion Construction

        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties

		/// <summary>Get the maximim possible number of characters in this string buffer instance.</summary>
		public int MaxCapacity
		{	get	{	return _maxCapacity;	}	}
		
		/// <summary>Get or set the current number of characters in this string buffer instance.</summary>
		public int Capacity
		{	get
			{	if (_str.Length == 0)
					return Math.Min (_maxCapacity, DEFAULT_CAPACITY);
				
				return _str.Length;
			}
			set
			{	if (value < _length)
					throw new ArgumentException( "Capacity must be larger than length." );

				if (value > _maxCapacity)
					throw new ArgumentOutOfRangeException ("value", "Capacity must be less than or equal to MaxCapacity.");

				InternalEnsureCapacity(value);
			}
		}

		/// <summary>Get or set the currently used number of characters in this string buffer instance.</summary>
		/// <remarks>Never truncate the string.</remarks>
		public int Length
		{
			get
			{	return _length;		}

			set
			{	if( value < 0 || value > _maxCapacity)
					throw new ArgumentOutOfRangeException();

				if (value == _length)
					return;

				if (value < _length)
				{
					// Never truncate the string.
					// Make sure that we invalidate any cached string.
					InternalEnsureCapacity (value);
					_length = value;
				}
				else
				{
					// Expand the capacity to the new length and
					// pad the string with NULL characters.
					Append (TWstring.NULL_CHAR, value - _length);
				}
			}
		}
		
		/// <summary>Get or set the character at a specified position in the current buffer instance.</summary>
		/// <param name="index">A zero-based position index in the current string. <see cref="System.Int32"/></param>
		public TWchar this [int index]
		{
			get
			{	if (index < 0 || index >= _length)
					throw new IndexOutOfRangeException();

				return _str[index];
			} 

			set
			{	if (index < 0 || index >= _length)
					throw new IndexOutOfRangeException();

				if (null != _cached_str)
					// Make sure that we invalidate any cached string.
					InternalEnsureCapacity (_length);
				
				TWstring.SetChar (_str, index, value);
			}
		}

		#endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Methods

		/// <summary>Returns a string that represents the this instance.</summary>
		/// <returns>A string that represents the this instance.<see cref="System.String"/></returns>
		public override string ToString ()
		{
			if (_length == 0)
				return String.Empty;

			if (_cached_str != null)
				return _cached_str.ToString ();

			_cached_str = _str.Substring (0, _length);
			return _cached_str.ToString ();
		}

		/// <summary>Returns a string that represents the this instance.</summary>
		/// <returns>A string that represents the this instance.<see cref="System.X11.TWstring"/></returns>
		public TWstring ToTWstring ()
		{
			if (_length == 0)
				return TWstring.Empty;

			if (_cached_str != null)
				return _cached_str;

			_cached_str = _str.Substring (0, _length);
			return _cached_str;
		}
		
		/// <summary>Returns a substring from this string buffer instance.</summary>
		/// <param name="startIndex">The position within this string buffer instance where the substring begins.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in the substring.<see cref="System.Int32"/></param>
		/// <returns>A substring from this string buffer instance.<see cref="X11.TWstring"/></returns>
		public TWstring ToTWstring (int startIndex, int length) 
		{
			if (startIndex < 0 || startIndex > _str.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || _str.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");
			
			if (startIndex == _str.Length)
				return TWstring.Empty;
				
			if (startIndex == 0 && length == _length)
				return ToTWstring ();
			else
				return _str.Substring (startIndex, length);
		}
		
		/// <summary>Prepare the minimum requested number of possible characters in this string buffer instance.</summary>
		/// <param name="capacity">The minimum requested number of possible characters in this string buffer instance.<see cref="System.Int32"/></param>
		/// <returns>The currently used number of characters in this string buffer instance. Might be greater than the minimum requested number.<see cref="System.Int32"/></returns>
		/// <remarks>Never truncate the string.</remarks>
		public int EnsureCapacity (int capacity) 
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException ("Capacity must be greater than 0." );

			if( capacity <= _str.Length )
				return _str.Length;

			InternalEnsureCapacity (capacity);

			return _str.Length;
		}
		
		/// <summary>Determine whether the current instance of string buffer and a specified string buffer have the same value.</summary>
		/// <param name="sb">The string buffer to compare with the current instance of string buffer.<see cref="TWstringBuilder"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals (TWstringBuilder sb) 
		{
			if (object.Equals (sb, null)) // Prevent cyclic calls.
				return false;
			
			if (_length == sb.Length && _str == sb._str )
				return true;

			return false;
		}
		
		/// <summary>Removes the specified range of characters from this string buffer instance.</summary>
		/// <param name="startIndex">The zero-based position index in this instance where removal begins. <see cref="System.Int32"/></param>
		/// <param name="length">The number of characters to remove.<see cref="System.Int32"/></param>
		/// <returns>A reference to this instance after the excise operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Remove (int startIndex, int length)
		{
			if (startIndex < 0 || startIndex > _str.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || _str.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");
			
			if (_cached_str != null)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (_length);
			
			// Copy everything after the 'removed' part to the start 
			// of the removed part and truncate the length.
			if (_length - (startIndex + length) > 0)
				TWstring.CharCopy (_str, startIndex, _str, startIndex + length, _length - (startIndex + length));

			_length -= length;

			return this;
		}			       
		
		/// <summary>Replaces all occurrences of a specified character in this instance with another specified character. </summary>
		/// <param name="oldChar">The character to replace.<see cref="X11.TWchar"/></param>
		/// <param name="newChar">The character that replaces oldChar.<see cref="X11.TWchar"/></param>
		/// <returns>A reference to this instance with oldChar replaced by newChar.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Replace (TWchar oldChar, TWchar newChar) 
		{
			return Replace( oldChar, newChar, 0, _length);
		}
	
		/// <summary>Replaces, within a substring of this string buffer instance, all occurrences of a specified character with another specified character.</summary>
		/// <param name="oldChar">The character to replace.<see cref="X11.TWchar"/></param>
		/// <param name="newChar">The character that replaces oldChar.<see cref="X11.TWchar"/></param>
		/// <param name="startIndex">The zero-based position index in this instance where the substring begins. <see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in the substring, replacing should be applied to. <see cref="System.Int32"/></param>
		/// <returns>A reference to this instance with oldChar replaced by newChar in the range from startIndex to startIndex + count -1.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Replace (TWchar oldChar, TWchar newChar, int startIndex, int length) 
		{
			if (startIndex < 0 || startIndex > _str.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || _str.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");
			
			if (null != _cached_str)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (_str.Length);

			for (int replaceIterate = startIndex; replaceIterate < startIndex + length; replaceIterate++ )
			{	if( _str [replaceIterate] == oldChar )
					TWstring.SetChar (_str, replaceIterate, newChar);
			}

			return this;
		}
		
		/// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
		/// <param name="oldValue">The string to replace.<see cref="X11.TWstring"/></param>
		/// <param name="newValue">The string that replaces oldValue, or null.<see cref="X11.TWstring"/></param>
		/// <returns>A reference to this instance with all instances of oldValue replaced by newValue.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Replace(TWstring oldValue, TWstring newValue )
		{
			return Replace (oldValue, newValue, 0, _length);
		}
		
		/// <summary>Replaces, within a substring of this string buffer instance, all occurrences of a specified string with another specified string.</summary>
		/// <param name="oldValue">The string to replace.<see cref="X11.TWstring"/></param>
		/// <param name="newValue">The string that replaces oldValue, or null.<see cref="X11.TWstring"/></param>
		/// <param name="startIndex">The position in this instance where the substring begins.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in the substring, replacing should be applied to. <see cref="System.Int32"/></param>
		/// <returns>A reference to this instance with all instances of oldValue replaced by newValue in the range from startIndex to startIndex + count - 1.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Replace (TWstring oldValue, TWstring newValue, int startIndex, int length ) 
		{
			if (oldValue == null)
				throw new ArgumentNullException ("oldValue", "The old value cannot be null.");

			if (oldValue.Length == 0)
				throw new ArgumentException ("oldValue", "The old value cannot be zero length.");

			if (startIndex < 0 || startIndex > _str.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || _str.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");

			TWstring substr  = _str.Substring(startIndex, length);
			TWstring replace = substr.Replace(oldValue, newValue);

			InternalEnsureCapacity (replace.Length + (_length - length));

			// shift end part
			if (replace.Length < length)
				TWstring.CharCopy (_str, startIndex + replace.Length, _str, startIndex + length, _length - startIndex  - length);
			else if (replace.Length > length)
				TWstring.CharCopyReverse (_str, startIndex + replace.Length, _str, startIndex + length, _length - startIndex  - length);

			// copy middle part back into _str
			TWstring.CharCopy (_str, startIndex, replace, 0, replace.Length);
			
			_length = replace.Length + (_length - length);

			return this;
		}
	
		/// <summary>Ensure the capacity of _str meets the requirements, preserve the current value and clear _cached_str.</summary>
		/// <param name="newTotalSize">The new total size to hold the complete new value.<see cref="System.Int32"/></param>
		/// <remarks>Never truncate the buffer.</remarks>
		private void InternalEnsureCapacity (int newTotalSize) 
		{
			if (newTotalSize > _str.Length || (object) _cached_str == (object) _str)
			{
				int capacity = _str.Length;

				// Resize string, is required.
				if (newTotalSize > capacity)
				{
					capacity = DEFAULT_CAPACITY * ((newTotalSize + 1) / DEFAULT_CAPACITY);
					
					// Ensure, the new capacity fits the needs.
					if (newTotalSize > capacity)
						capacity = newTotalSize;

					if (capacity >= Int32.MaxValue || capacity < 0)
						capacity = Int32.MaxValue;

					if (capacity > _maxCapacity && newTotalSize <= _maxCapacity)
						capacity = _maxCapacity;
					
					if (capacity > _maxCapacity)
						throw new ArgumentOutOfRangeException ("size", "capacity was less than the current size.");
				}
				
				// Transfer current value to resized string.
				TWstring tmp = new TWstring (capacity);
				if (_length > 0)
					TWstring.CharCopy (tmp, 0, _str, 0, _length);

				_str = tmp;
			}

			_cached_str = null;
		}
		
		/// <summary>Copy the characters from a specified segment of this instance to a specified segment of a destination Char array.</summary>
		/// <param name="sourceIndex">The starting position in this instance where characters will be copied from. The index is zero-based.<see cref="System.Int32"/></param>
		/// <param name="destination">The array where characters will be copied.<see cref="X11.TWchar[]"/></param>
		/// <param name="destinationIndex">The starting position in destination where characters will be copied. The index is zero-based.<see cref="System.Int32"/></param>
		/// <param name="count">The number of characters to be copied.<see cref="System.Int32"/></param>
		public void CopyTo (int sourceIndex, TWchar[] destination, int destinationIndex, int count)
		{
			if (destination == null)
				throw new ArgumentNullException ("destination");
			
			if ((Length - count < sourceIndex) ||
			    (destination.Length - count < destinationIndex) ||
			    (sourceIndex < 0 || destinationIndex < 0 || count < 0))
				throw new ArgumentOutOfRangeException ("count");

			for (int i = 0; i < count; i++)
				destination [destinationIndex+i] = _str [sourceIndex+i];
		}

        #endregion Methods

        #region Append methods
		
		/// <summary>Appends the string representation of a specified Boolean value to this string buffer instance.</summary>
		/// <param name="value">The Boolean value to append. <see cref="System.Boolean"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (bool value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified 8-bit unsigned integer to this string buffer instance.</summary>
		/// <param name="value">The Byte value to append. <see cref="System.Byte"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (byte value)
		{	return Append (new TWstring (value.ToString()));	}

		/// <summary>Appends the string representation of a specified character to this string buffer instance.</summary>
		/// <param name="value">The character to append. <see cref="X11.TWchar[]"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (TWchar value) 
		{
			int needed_cap = _length + 1;
			if (_cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);

			TWstring.SetChar (_str, _length, value);
			_length = needed_cap;

			return this;
		}

		/// <summary>Appends the string representation of the characters in a specified array to this string buffer instance.</summary>
		/// <param name="value">The array of characters to append.  <see cref="X11.TWchar[]"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (TWchar[] value) 
		{
			if (value == null)
				return this;

			int needed_cap = _length + value.Length;
			if (_cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);
			
			// Copy in stuff from the insert buffer.
			TWstring.CharCopy (_str, _length, value, 0, value.Length);
			_length = needed_cap;

			return this;
		} 
		
		/// <summary>Appends the string representation of a specified decimal number to this string buffer instance.</summary>
		/// <param name="value">The Decimal value to append.<see cref="System.Decimal"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (decimal value)
		{	return Append (new TWstring (value.ToString()));	}

		/// <summary>Appends the string representation of a specified double-precision floating-point number to this string buffer instance.</summary>
		/// <param name="value">The Double value to append.<see cref="System.Double"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (double value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified 16-bit signed integer to this string buffer instance.</summary>
		/// <param name="value">The Int16 value to append.<see cref="System.Int16"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (short value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified 32-bit signed integer to this string buffer instance.</summary>
		/// <param name="value">The Int32 value to append.<see cref="System.Int32"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (int value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified 64-bit signed integer to this string buffer instance.</summary>
		/// <param name="value">The Int64 value to append.<see cref="System.Int64"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (long value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified object to this string buffer instance.</summary>
		/// <param name="value">The Object value to append.<see cref="System.Object"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (object value)
		{	if (value == null)
				return this;

			return Append (new TWstring (value.ToString()));
		}
		
		/// <summary>Appends the string representation of a specified 8-bit signed integer to this string buffer instance.</summary>
		/// <param name="value">The SByte value to append. <see cref="System.SByte"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (sbyte value)
		{	return Append (new TWstring (value.ToString()));	}

		/// <summary>Appends the string representation of a specified single-precision floating-point number to this string buffer instance.</summary>
		/// <param name="value">The Single value to append.<see cref="System.Single"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (float value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends a copy of the specified string to this string buffer instance.</summary>
		/// <param name="value">The string to append.<see cref="X11.TWstring"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (TWstring value) 
		{
			if (value == null)
				return this;
			
			int needed_cap = _length + value.Length;
			if (_cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);

			TWstring.CharCopy (_str, _length, value, 0, value.Length);
			_length = needed_cap;
			
			return this;
		}
		
		/// <summary>Appends the string representation of a specified 16-bit unsigned integer to this string buffer instance.</summary>
		/// <param name="value">The UInt16 value to append.<see cref="System.UInt16"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (ushort value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified 32-bit unsigned integer to this string buffer instance.</summary>
		/// <param name="value">The UInt32 value to append.<see cref="System.UInt32"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (uint value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends the string representation of a specified 64-bit unsigned integer to this string buffer instance.</summary>
		/// <param name="value">The UInt64 value to append.<see cref="System.UInt64"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (ulong value)
		{	return Append (new TWstring (value.ToString()));	}
		
		/// <summary>Appends a specified number of copies of the string representation of a Unicode character to this string buffer instance.</summary>
		/// <param name="value">The character to append.<see cref="X11.TWchar"/></param>
		/// <param name="repeatCount">The number of times to append value.<see cref="System.Int32"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (TWchar value, int repeatCount) 
		{
			if( repeatCount < 0 )
				throw new ArgumentOutOfRangeException();

			// Make sure that we invalidate any cached string.
			InternalEnsureCapacity (_length + repeatCount);
			
			for (int i = 0; i < repeatCount; i++)
				TWstring.SetChar (_str, _length++, value);

			return this;
		}
		
		/// <summary>Appends the string representation of a specified subarray of Unicode characters to this string buffer instance.</summary>
		/// <param name="value">A array of characters that contains the substring to append.<see cref="X11.TWchar[]"/></param>
		/// <param name="startIndex">The zero-based starting position index in value.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in value to append.<see cref="System.Int32"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (TWchar[] value, int startIndex, int length ) 
		{
			if (value == null)
			{
				if (!(startIndex == 0 && length == 0))
					throw new ArgumentNullException ("value");

				return this;
			}

			if (startIndex < 0 || startIndex > value.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || value.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("length");
			
			int needed_cap = _length + length;
			// Make sure that we invalidate any cached string.
			InternalEnsureCapacity (needed_cap);

			TWstring.CharCopy (_str, _length, value, startIndex, length);
			_length = needed_cap;

			return this;
		}
		
		/// <summary>Appends a copy of a specified substring to this instance.</summary>
		/// <param name="value">The string that contains the substring to append.<see cref="X11.TWstring"/></param>
		/// <param name="startIndex">The zero-based starting position index in value.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in value to append.<see cref="System.Int32"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Append (TWstring value, int startIndex, int length) 
		{
			if (value == null)
			{
				if (startIndex != 0 && length != 0)
					throw new ArgumentNullException ("value");
					
				return this;
			}

			if (startIndex < 0 || startIndex > value.Length)
				throw new ArgumentOutOfRangeException ("startIndex");
			
			if (length < 0 || value.Length - startIndex < length)
				throw new ArgumentOutOfRangeException ("count");
			
			int needed_cap = _length + length;
			if ( _cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);

			TWstring.CharCopy (_str, _length, value, startIndex, length);
			_length = needed_cap;

			return this;
		}
		
		/// <summary>Appends the default line terminator to the end of the current string buffer instance.</summary>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder AppendLine ()
		{
			return Append (System.Environment.NewLine);
		}
		
		/// <summary>Appends a copy of the specified string followed by the default line terminator to the end of the current string buffer instance.</summary>
		/// <param name="value">The string to append.<see cref="X11.TWstring"/></param>
		/// <returns>A reference to this string buffer instance after the append operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder AppendLine (TWstring value)
		{
			return Append (value).Append (System.Environment.NewLine);
		}
		
		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
		/// Each format item is replaced by the string representation of a single argument.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="arg0">An object to format.<see cref="System.Object"/></param>
		/// <returns>A reference to this instance with format appended. Each format item in format is replaced by the string representation of arg0.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder AppendFormat (TWstring format, object arg0)
		{
			return TWstring.FormatHelper (this, null, format, new object [] { arg0 });
		}
		
		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
		/// Each format item is replaced by the string representation of either of two arguments.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="arg0">The first object to format. <see cref="System.Object"/></param>
		/// <param name="arg1">The second object to format.<see cref="System.Object"/></param>
		/// <returns>A reference to this instance with format appended. Each format item in format is replaced by the string representation of the corresponding object argument.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder AppendFormat (TWstring format, object arg0, object arg1)
		{
			return TWstring.FormatHelper (this, null, format, new object [] { arg0, arg1 });
		}
		
		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
		/// Each format item is replaced by the string representation of either of three arguments.</summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="arg0">The first object to format. <see cref="System.Object"/></param>
		/// <param name="arg1">The second object to format.<see cref="System.Object"/></param>
		/// <param name="arg2">The third object to format. <see cref="System.Object"/></param>
		/// <returns>A reference to this instance with format appended. Each format item in format is replaced by the string representation of the corresponding object argument.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder AppendFormat (TWstring format, object arg0, object arg1, object arg2)
		{
			return TWstring.FormatHelper (this, null, format, new object [] { arg0, arg1, arg2 });
		}
		
		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance.
		/// Each format item is replaced by the string representation of a corresponding argument in a parameter array. </summary>
		/// <param name="format">A composite format string.<see cref="X11.TWstring"/></param>
		/// <param name="args">An array of objects to format. <see cref="System.Object[]"/></param>
		/// <returns>A reference to this instance with format appended. Each format item in format is replaced by the string representation of the corresponding object argument.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder AppendFormat (TWstring format, params object[] args) 
		{ 
			return TWstring.FormatHelper (this, null, format, args); 
		}

        #endregion Append methods

        #region Insert methods

		/// <summary>Inserts the string representation of a Boolean value into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Boolean value to insert.<see cref="System.Boolean"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, bool value )
		{	return Insert (index, new TWstring (value.ToString()));		}
		
		/// <summary>Inserts the string representation of a specified 8-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Byte value to insert.<see cref="System.Byte"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, byte value )
		{	return Insert (index, new TWstring (value.ToString()));		}
		
		/// <summary>Inserts the string representation of a specified Unicode character into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The character to insert.<see cref="X11.TWchar"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, TWchar value) 
		{
			if (index < 0 || index > _length)
				throw new ArgumentOutOfRangeException ("index");

			int needed_cap = _length + 1;
			if (_cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);

			// Move everything to the right of the insert point.
			TWstring.CharCopyReverse (_str, index + 1, _str, index, _length - index);
			
			TWstring.SetChar (_str, index, value);
			_length = needed_cap;

			return this;
		}
		
		/// <summary>Inserts the string representation of a specified array of Unicode characters into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The array of characters to insert.<see cref="X11.TWchar[]"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, TWchar[] value) 
		{
			if (index < 0 || index > _length)
				throw new ArgumentOutOfRangeException ("index");
			
			if (value == null || value.Length == 0)
				return this;

			int needed_cap = _length + value.Length;
			if (_cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);
			
			// Move everything to the right of the insert point.
			TWstring.CharCopyReverse (_str, index + value.Length, _str, index, _length - index);
			
			// Copy in stuff from the insert buffer.
			TWstring.CharCopy (_str, index, value, 0, value.Length);
			_length = needed_cap;

			return this;
		}
		
		/// <summary>Inserts the string representation of a decimal number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Decimal value to insert.<see cref="System.Decimal"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, decimal value )
		{	return Insert (index, new TWstring (value.ToString()));	}
		
		/// <summary>Inserts the string representation of a double-precision floating-point number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Double value to insert.<see cref="System.Double"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, double value )
		{	return Insert (index, new TWstring (value.ToString()));	}
		
		/// <summary>Inserts the string representation of a specified 16-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Int16 value to insert.<see cref="System.Int16"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, short value )
		{	return Insert (index, new TWstring (value.ToString()));	}
		
		/// <summary>Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Int32 value to insert.<see cref="System.Int32"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, int value )
		{	return Insert (index, new TWstring (value.ToString()));	}
		
		/// <summary>Inserts the string representation of a 64-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Int64 value to insert.<see cref="System.Int64"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, long value )
		{	return Insert (index, new TWstring (value.ToString()));	}
	
		/// <summary>Inserts the string representation of an object into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Object value to insert.<see cref="System.Object"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert( int index, object value )
		{	return Insert (index, new TWstring (value.ToString()));	}
		
		/// <summary>Inserts the string representation of a specified 8-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The SByte value to insert.<see cref="System.SByte"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert( int index, sbyte value )
		{	return Insert (index, new TWstring (value.ToString()));	}
		
		/// <summary>Inserts the string representation of a single-precision floating point number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Single value to insert.<see cref="System.Single"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, float value)
		{	return Insert (index, new TWstring (value.ToString()));	}
			
		/// <summary>Inserts a string into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The string to insert.<see cref="X11.TWstring"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, TWstring value) 
		{
			if (index < 0 || index > _length)
				throw new ArgumentOutOfRangeException ("index");

			if (value == null || value.Length == 0)
				return this;

			int needed_cap = _length + value.Length;
			if (_cached_str != null || _str.Length < needed_cap)
				// Make sure that we invalidate any cached string.
				InternalEnsureCapacity (needed_cap);

			// Move everything to the right of the insert point.
			TWstring.CharCopyReverse (_str, index + value.Length, _str, index, _length - index);
			
			// Copy in stuff from the insert buffer.
			TWstring.CharCopy (_str, index, value, 0, value.Length);
			_length = needed_cap;

			return this;
		}
		
		/// <summary>Inserts the string representation of a 16-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Byte value to insert.<see cref="System.UInt16"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, ushort value)
		{	return Insert (index, new TWstring (value.ToString()) );	}
			
		/// <summary>Inserts the string representation of a 32-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Byte value to insert.<see cref="System.UInt32"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, uint value)
		{	return Insert (index, new TWstring (value.ToString()) );	}
		
		/// <summary>Inserts the string representation of a 64-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The Byte value to insert.<see cref="System.UInt64"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, ulong value)
		{	return Insert (index, new TWstring (value.ToString()) );	}
				
		/// <summary>Inserts one or more copies of a specified string into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The string to insert.<see cref="X11.TWstring"/></param>
		/// <param name="count">The number of times to insert value. <see cref="System.Int32"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, TWstring value, int count) 
		{
			if (index < 0 || index > _length)
				throw new ArgumentOutOfRangeException ("index");

			if (count < 0)
				throw new ArgumentOutOfRangeException("count");

			if (value != null && !TWstring.IsNullOrEmpty (value))
				for (int insertCount = 0; insertCount < count; insertCount++)
					Insert (index, value);

			return this;
		}
		
		/// <summary>Inserts the string representation of a specified subarray of characters into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.<see cref="System.Int32"/></param>
		/// <param name="value">The array of characters to insert.<see cref="X11.TWchar[]"/></param>
		/// <param name="startIndex">The starting index within value.<see cref="System.Int32"/></param>
		/// <param name="length">The number of characters in value to append.<see cref="System.Int32"/></param>
		/// <returns>A reference to this instance after the insert operation has completed.<see cref="TWstringBuilder"/></returns>
		public TWstringBuilder Insert (int index, TWchar[] value, int startIndex, int length)
		{
			if (value == null)
			{
				if (startIndex == 0 && length == 0)
					return this;

				throw new ArgumentNullException ("value");
			}

			if (length < 0 || startIndex < 0 || startIndex > value.Length - length)
				throw new ArgumentOutOfRangeException ();

			return Insert (index, new TWstring (value, startIndex, length));
		}

        #endregion Insert methods

        #region ISerializable methods
		
		/// <summary>Populates a SerializationInfo with the data needed to serialize the target object.</summary>
		/// <param name="info">The SerializationInfo to populate data to.<see cref="SerializationInfo"/></param>
		/// <param name="context">The destination (see StreamingContext) for this serialization. <see cref="StreamingContext"/></param>
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("m_MaxCapacity", _maxCapacity);
			info.AddValue ("Capacity", Capacity);
			info.AddValue ("m_StringValue", ToString ());
			info.AddValue ("m_currentThread", 0);
		}
		
		/// <summary>Initializes a new instance of the TWstringBuilder class from the specified string and capacity.</summary>
		/// <param name="info">The SerializationInfo to reclaim data from.<see cref="SerializationInfo"/></param>
		/// <param name="context">The source (see StreamingContext) for this de-serialization. <see cref="StreamingContext"/></param>
		TWstringBuilder (SerializationInfo info, StreamingContext context)
		{
			TWstring s = new TWstring (info.GetString ("m_StringValue"));
			if (s == null)
				s = TWstring.Empty;
			_length = s.Length;
			
			_maxCapacity = info.GetInt32 ("m_MaxCapacity");
			if (_maxCapacity < 0)
				_maxCapacity = Int32.MaxValue;
			Capacity = info.GetInt32 ("Capacity");
		}

        #endregion ISerializable methods
	}
}