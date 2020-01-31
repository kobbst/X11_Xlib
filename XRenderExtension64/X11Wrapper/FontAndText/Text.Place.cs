// ==================
// The X11 C# wrapper
// ==================

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

namespace X11.Text
{

	// ##################################################################################################################################
	// Ideas and source code snippets taken from: http://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting
	// ##################################################################################################################################
	
    /// <summary>Line index and char index of a place inside a multiline text.</summary>
    public struct Place : IEquatable<Place>, IComparable<Place>
    {

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes

		/// <summary>The char index of a place inside text line.</summary>
        private int					_charIndex;
		
		/// <summary>The line index of a place inside a multiline text.</summary>
        private int					_lineIndex;
		
		/// <summary>The static instance reoresenting an empty place.</summary>
		private static Place		_empty = new Place(-1, -1);

		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="charIndex">The char index of a place inside text line.<see cref="System.Int32"/></param>
		/// <param name="lineIndex">The line index of a place inside a multiline text.<see cref="System.Int32"/></param>
        public Place(int charIndex, int lineIndex)
        {
            this._charIndex = charIndex;
            this._lineIndex = lineIndex;
        }

        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################
		
		#region Properties

		/// <summary>Get or set the char index of a place inside text line.</summary>
        public int CharIndex
		{	get	{	return _charIndex;	}
			set	{	_charIndex = value;	}
		}
		
		/// <summary>Get or set the line index of a place inside a multiline text.</summary>
        public int LineIndex
		{	get	{	return _lineIndex;	}
			set	{	_lineIndex = value;	}
		}
		
		/// <summary>Get the static instance reoresenting an empty place.</summary>
        public static Place Empty
        {
            get { return _empty; }
        }
		
		#endregion Properties
		
		#region Legacy properties
		
		/// <summary>Get or set the char index of a place inside text line.</summary>
        public int iChar
		{	get	{	return _charIndex;	}
			set	{	_charIndex = value;	}
		}
		
		/// <summary>Get or set the line index of a place inside a multiline text.</summary>
        public int iLine
		{	get	{	return _lineIndex;	}
			set	{	_lineIndex = value;	}
		}
		
		#endregion Legacy properties

        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region IEquatable<T> methods

		/// <summary>Check equality of current Place instance with indicated Place instance.</summary>
		/// <param name="other">The Place instance to use for equality check.<see cref="Place"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals(Place other)
        {
            return _charIndex == other._charIndex && _lineIndex == other._lineIndex;
        }
		
		#endregion IEquatable<T> methods
		
		#region IComparable<T> methods
		
		/// <summary>Comparecurrent Place instance with indicated Place instance.</summary>
		/// <param name="other">The Place instance to use for comparison.<see cref="Place"/></param>
		/// <returns>A value that indicates the relative order of the places being compared.
		/// Less than zero means: This instance precedes other in the sort order.
		/// Equal to zero means: This instance occurs in the same position in the sort order as other.
		/// Grater than zero means: This instance follows other in the sort order.<see cref="System.Int32"/></returns>
		public int CompareTo (Place other)
		{
			if (_lineIndex < other._lineIndex)
				return -1;
			if (_lineIndex == other._lineIndex && _charIndex < other._charIndex)
				return -1;

			if (_lineIndex > other._lineIndex)
				return 1;
			if (_lineIndex == other._lineIndex && _charIndex > other._charIndex)
				return 1;
			
			return 0;
		}
		
		#endregion IComparable<T> methods

		#region Methods
		
		/// <summary>Add a place offset to the current Place instance.</summary>
		/// <param name="dCharIndex">The char index offset of a place inside text line.<see cref="System.Int32"/></param>
		/// <param name="dLineIndex">The line index offset of a place inside a multiline text.<see cref="System.Int32"/></param>
        public void Offset (int dCharIndex, int dLineIndex)
        {
            _charIndex += dCharIndex;
            _lineIndex += dLineIndex;
        }

		/// <summary>Check equality of current Place instance with indicated Place instance.</summary>
		/// <param name="obj">The Place instance to use for equality check.<see cref="System.Object"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool Equals (object obj)
        {
            return (obj is Place) && Equals((Place)obj);
        }
		
		/// <summary>Generate a hash code for the current instance.</summary>
		/// <returns>A int containing the hash code for the current instance.<see cref="System.Int32"/></returns>
        public override int GetHashCode ()
        {
            return _charIndex.GetHashCode() ^ _lineIndex.GetHashCode();
        }
		
		/// <summary>Get the string representation of the current Place instance.</summary>
		/// <returns>The tring representation of the current Place instance.<see cref="System.String"/></returns>
        public override string ToString ()
        {
            return "(" + _charIndex + "," + _lineIndex + ")";
        }
		
		#endregion Methods
		
		#region Operators
		
		/// <summary>Add two Place instances.</summary>
		/// <param name="p1">The first Place instance to add.<see cref="Place"/></param>
		/// <param name="p2">The second Place instance to add.<see cref="Place"/></param>
		/// <returns>A new Place instance representing the sum.<see cref="Place"/></returns>
        public static Place operator +(Place p1, Place p2)
        {
            return new Place(p1._charIndex + p2._charIndex, p1._lineIndex + p2._lineIndex);
        }
		
		/// <summary>Evaluates two instances of Place to determine inequality.</summary>
		/// <param name="left">The first Place instance to compare.<see cref="Place"/></param>
		/// <param name="right">The second Place instance to compare.<see cref="Place"/></param>
		/// <returns>True on inequality, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(Place left, Place right)
        {
            return !left.Equals(right);
        }

        /// <summary>Evaluates two instances of Place to determine equality.</summary>
		/// <param name="left">The first Place instance to compare.<see cref="Place"/></param>
		/// <param name="right">The second Place instance to compare.<see cref="Place"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator ==(Place left, Place right)
        {
            return left.Equals(right);
        }
		
		/// <summary>Evaluates two instances of Place to determine whether one instance is less than the other.</summary>
		/// <param name="left">The first Place instance to compare.<see cref="Place"/></param>
		/// <param name="right">The second Place instance to compare.<see cref="Place"/></param>
		/// <returns>True if left is less than right, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator <(Place left, Place right)
        {
            if (left._lineIndex < right._lineIndex) return true;
            if (left._lineIndex > right._lineIndex) return false;
            if (left._charIndex < right._charIndex) return true;
            return false;
        }

        /// <summary>Evaluates two instances of Place to determine whether one instance is less than or equal to the other.</summary>
		/// <param name="left">The first Place instance to compare.<see cref="Place"/></param>
		/// <param name="right">The second Place instance to compare.<see cref="Place"/></param>
		/// <returns>True if left is less than or equal to right, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator <=(Place left, Place right)
        {
            if (left.Equals(right)) return true;
            if (left._lineIndex < right._lineIndex) return true;
            if (left._lineIndex > right._lineIndex) return false;
            if (left._charIndex < right._charIndex) return true;
            return false;
        }

        /// <summary>Evaluates two instances of Place to determine whether one instance s greater than the other.</summary>
		/// <param name="left">The first Place instance to compare.<see cref="Place"/></param>
		/// <param name="right">The second Place instance to compare.<see cref="Place"/></param>
		/// <returns>True if left is greater than right, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator >(Place left, Place right)
        {
            if (left._lineIndex > right._lineIndex) return true;
            if (left._lineIndex < right._lineIndex) return false;
            if (left._charIndex > right._charIndex) return true;
            return false;
        }

        /// <summary>Evaluates two instances of Place to determine whether one instance s greater than or equal the other.</summary>
		/// <param name="left">The first Place instance to compare.<see cref="Place"/></param>
		/// <param name="right">The second Place instance to compare.<see cref="Place"/></param>
		/// <returns>True if left is greater than or equal to right, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator >=(Place left, Place right)
        {
            if (left.Equals(right)) return true;
            if (left._lineIndex > right._lineIndex) return true;
            if (left._lineIndex < right._lineIndex) return false;
            if (left._charIndex > right._charIndex) return true;
            return false;
        }
		
		#endregion Operators

    }

}