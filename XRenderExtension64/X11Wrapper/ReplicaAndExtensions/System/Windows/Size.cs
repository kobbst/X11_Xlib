// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: May 2015
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 * In case of problems with .NEt see: .NET Reference Source, http://referencesource-beta.microsoft.com/
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2015 Steffen Ploetz
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

namespace System.Windows
{
	/// <summary>Implements a structure that is used to describe the System.Windows.Size of an object.</summary>
	public class Size : IFormattable
    {

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "Size";

		#endregion Constants

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes		
		
		/// <summary>The width of the System.Windows.Size structure.</summary>
		private double						_width;
		
		/// <summary>The height of the System.Windows.Size structure.</summary>
		private double						_height;
		
		/// <summary>The value that represents a static empty System.Windows.Size.</summary>
		private static System.Windows.Size	_empty = new System.Windows.Size (double.NegativeInfinity, double.NegativeInfinity);
		
        #endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initialize a new instance of the System.Windows.Size structure and assigns it an initial width and height.</summary>
		/// <param name="width">The initial width of the instance of System.Windows.Size.<see cref="System.Double"/></param>
		/// <param name="height">The initial height of the instance of System.Windows.Size.<see cref="System.Double"/></param>
        public Size(double width, double height)
		{	_width = width;
			_height = height;
		}
		
		/// <summary>Initialize a new instance of the System.Windows.Size structure and assigns it an initial width and height.</summary>
		/// <param name="width">The initial width of the instance of System.Windows.Size.<see cref="System.Double"/></param>
		/// <param name="height">The initial height of the instance of System.Windows.Size.<see cref="System.Double"/></param>
        public Size(System.Drawing.Size size)
		{	_width = size.Width;
			_height = size.Height;
		}
		
        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

		/// <summary>Get or set the System.Windows.Size.Width of this instance of System.Windows.Size.</summary>
		/// <returns>The System.Windows.Size.Width of this instance of System.Windows.Size. The
        /// default value is 0. The value cannot be negative.<see cref="System.Double"/></returns>
        public double Width
		{	get	{	return _width;	}
			set	{	_width = Math.Abs(value);	}
		}

		/// <summary>Get or set the System.Windows.Size.Height of this instance of System.Windows.Size.</summary>
		/// <returns>The System.Windows.Size.Height of this instance of System.Windows.Size. The
        /// default is 0. The value cannot be negative.<see cref="System.Double"/></returns>
        public double Height
		{	get	{	return _height;	}
			set	{	_height = Math.Abs(value);	}
		}

		/// <summary>Get a value that represents a static empty System.Windows.Size.</summary>
		/// <returns>An empty instance of System.Windows.Size.<see cref="System.Windows.Size"/></returns>
        public static System.Windows.Size Empty
		{	get	{	return _empty;	}	}

		/// <summary>Get a value that indicates whether this instance of System.Windows.Size is System.Windows.Size.Empty.</summary>
        /// <returns>True if this instance of size is System.Windows.Size.Empty, or false otherwise false.</returns>
        public bool IsEmpty
		{	get	{	return this.Equals (_empty);	}	}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Operators

		/// <summary>Compare two instances of System.Windows.Size for inequality.</summary>
		/// <param name="size1">The first instance of System.Windows.Size to compare.<see cref="System.Windows.Size"/></param>
		/// <param name="size2">The second instance of System.Windows.Size to compare.<see cref="System.Windows.Size"/></param>
		/// <returns>True if the instances of System.Windows.Size are not equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(Size size1, Size size2)
		{	return (size1.Width != size2.Width && size1.Height != size2.Height);	}
		
		/// <summary>Compare two instances of System.Windows.Size for equality.</summary>
		/// <param name="size1">The first instance of System.Windows.Size to compare.<see cref="System.Windows.Size"/></param>
		/// <param name="size2">The second instance of System.Windows.Size to compare.<see cref="System.Windows.Size"/></param>
		/// <returns>True if the two instances of System.Windows.Size are equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator ==(System.Windows.Size size1, System.Windows.Size size2)
		{	return (size1.Width == size2.Width && size1.Height == size2.Height);	}
		
		/// <summary>Explicitly convert an instance of System.Windows.Size to an instance of System.Windows.Point.</summary>
		/// <param name="size">The System.Windows.Size value to be converted.<see cref="System.Windows.Size"/></param>
		/// <returns>A System.Windows.Point equal in value to this instance of System.Windows.Size.<see cref="System.Windows.Point"/></returns>
        public static explicit operator System.Windows.Point (System.Windows.Size size)
		{	return new System.Windows.Point (size.Width, size.Height);	}

		/// <summary>Explicitly convert an instance of System.Windows.Size to an instance of System.Windows.Vector.</summary>
		/// <param name="size">The System.Windows.Size value to be converted.<see cref="System.Windows.Size"/></param>
		/// <returns>A System.Windows.Vector equal in value to this instance of System.Windows.Size.<see cref="System.Windows.Vector"/></returns>
        public static explicit operator System.Windows.Vector(System.Windows.Size size)
		{	return new System.Windows.Vector (size.Width, size.Height);	}
		
		#endregion Operators
		
		#region Static methods

		/// <summary>Compare two instances of System.Windows.Size for equality.</summary>
		/// <param name="size1">The first instance of System.Windows.Size to compare.<see cref="System.Windows.Size"/></param>
		/// <param name="size2">The second instance of System.Windows.Size to compare.<see cref="System.Windows.Size"/></param>
		/// <returns>True if the instances of System.Windows.Size are equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool Equals(System.Windows.Size size1, System.Windows.Size size2)
		{	return (size1._width == size2._width && size1._height == size2._height);	}
		
		/// <summary>Convert the string representation of a point to its double-precision 2D point structure equivalent.
        /// A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="pointTag">A string containing a 2D point to convert.<see cref="System.String"/></param>
		/// <param name="size">When this method returns, the double-precision 2D point equivalent contains the
        /// point coordinates, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the s parameter
        /// is null, is not a point in a valid format, or represents a point with coordinates less than System.Double.MinValue or
        /// greater than System.Double.MaxValue. This parameter is passed uninitialized.<see cref="System.Windows.Size"/></param>
		/// <returns>True if s was converted successfully, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool TryParse (string s, out System.Windows.Size size)
		{
			size = new System.Windows.Size (0, 0);
			
			string[] sTags = s.Split (new char[] {','});
			if (sTags.Length != 2)
				return false;
			double w;
			double h;
			if (double.TryParse (sTags[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out w) &&
			    double.TryParse (sTags[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out h))
			{
				size.Width  = w;
				size.Height = h;
				return true;
			}
			else
				return false;
		}
        /*
		/// <summary>Return an instance of System.Windows.Size from a converted System.String.</summary>
		/// <param name="source">A System.String value to parse to a System.Windows.Size value.<see cref="System.String"/></param>
		/// <returns>An instance of System.Windows.Size.<see cref="System.Windows.Size"/></returns>
        public static System.Windows.Size Parse(string source);
        */
		
		#endregion Static methods
		
		#region Methods
		
		/// <summary>Compare an object to an instance of System.Windows.Size for equality.</summary>
		/// <param name="obj">The System.Object to compare.<see cref="System.Object"/></param>
		/// <returns>True if the sizes are equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool Equals(object obj)
		{	return obj is System.Windows.Size && Equals((System.Windows.Size)obj);	}

		/// <summary>Compare a value to an instance of System.Windows.Size for equality.</summary>
		/// <param name="value">The size to compare to this current instance of System.Windows.Size.<see cref="System.Windows.Size"/></param>
		/// <returns>True if the instances of System.Windows.Size are equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals(System.Windows.Size value)
		{	return (_width == value.Width && _height == value.Height);	}
		
		/// <summary>Get the hash code for this instance of System.Windows.Size.</summary>
		/// <returns>The hash code for this instance of System.Windows.Size.<see cref="System.Int32"/></returns>
        public override int GetHashCode()
		{	return _width.GetHashCode () ^ _height.GetHashCode ();	}

		/// <summary>Creates a System.String representation of this System.Windows.Size.</summary>
        /// <returns>A System.String containing the System.Windows.Size.Width and System.Windows.Size.Height
        /// values of this System.Windows.Point structure.<see cref="System.String"/></returns>
        public override string ToString ()
		{
			return string.Format ("{0};{1}", _width, _height);
		}

		/// <summary>Creates a System.String representation of this System.Windows.Size.</summary>
        /// <param name="provider">Culture-specific formatting information.<see cref="IFormatProvider"/></param>
        /// <returns>A System.String containing the System.Windows.Size.Width and System.Windows.Size.Height
        /// values of this System.Windows.Point structure.<see cref="System.String"/></returns>
        public string ToString (IFormatProvider provider)
		{
			return string.Format (provider, "{0};{1}", _width, _height);
		}

        /// <summary>Returns a string representation of the rectangle by using the specified format provider.</summary>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format
        /// defined for the type of the System.IFormattable implementation. <see cref="System.String"/></param>
		/// <param name="provider">Culture-specific formatting information.<see cref="IFormatProvider"/></param>
        /// <returns>A string representation of the current size that is determined by the
        /// specified format provider.<see cref="System.String"/></returns>
        public string ToString (string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty (format))
				return ToString (provider);
			else
				return string.Format (provider, format, _width, _height);
		}

		#endregion Methods

    }
}