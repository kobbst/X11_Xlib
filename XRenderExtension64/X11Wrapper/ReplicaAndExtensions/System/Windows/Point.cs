// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: April 2015
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
    /// <summary>Represents an x- and y-coordinate pair in two-dimensional space.</summary>
    [System.ComponentModel.TypeConverter(typeof(System.Windows.PointConverter))]
    public struct Point : IFormattable
    {

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "Point";

		#endregion Constants

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes		
		
		/// <summary>The x-coordinate of the System.Windows.Point structure.</summary>
		private double		_x;
		
		/// <summary>The y-coordinate of the System.Windows.Point structure.</summary>
		private double		_y;
		
        #endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary>Creates a new System.Windows.Point structure that contains the specified coordinates.</summary>
        /// <param name="x">The x-coordinate of the new System.Windows.Point structure.<see cref="System.Double"/></param>
        /// <param name="y">The y-coordinate of the new System.Windows.Point structure.<see cref="System.Double"/></param>
        public Point(double x, double y)
		{	_x = x;
			_y = y;
		}

		/// <summary>Creates a new System.Windows.Point structure that contains the specified coordinates.</summary>
        /// <param name="point">The coordinates of the new System.Windows.Point structure.<see cref="System.Windows.Point"/></param>
        public Point(System.Windows.Point point)
		{	_x = point.X;
			_y = point.Y;
		}
		
        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

        /// <summary>Get or set the System.Windows.Point.X-coordinate value of this System.Windows.Point structure.</summary>
        /// <returns>The System.Windows.Point.X-coordinate value of this System.Windows.Point
        /// structure. The default value is 0.</returns>
		public double X
		{	get	{	return _x;	}
			set	{	_x = value;	}
		}
		
		/// <summary>Get or set the System.Windows.Point.Y-coordinate value of this System.Windows.Point.</summary>
		/// <returns>The System.Windows.Point.Y-coordinate value of this System.Windows.Point
        /// structure. The default value is 0.</returns>
		public double Y 
		{	get	{	return _y;	}
			set	{	_y = value;	}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Operators
		
		/// <summary>Adds the specified points and returns the sum.</summary>
		/// <param name="point1">The first point to add.<see cref="System.Windows.Point"/></param>
		/// <param name="point1">The second point to add.<see cref="System.Windows.Point"/></param>
		/// <returns>The result point.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point operator +(System.Windows.Point point1, System.Windows.Point point2)
		{	return new Point (point1.X + point2.X, point1.Y + point2.Y);	}
		
		/// <summary>Subtracts the specified point from another specified point and returns the difference.</summary>
		/// <param name="point1">The point from which point2 is subtracted.<see cref="System.Windows.Point"/></param>
		/// <param name="point2">The point to subtract from point1.<see cref="System.Windows.Point"/></param>
		/// <returns>The difference between point1 and point2.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point operator -(System.Windows.Point point1, System.Windows.Point point2)
		{	return new Point (point1.X - point2.X, point1.Y - point2.Y);	}
        
		/// <summary>Compares two point structures for inequality.</summary>
		/// <param name="point1">The first point to compare.<see cref="System.Windows.Point"/></param>
		/// <param name="point2">The second point to compare.<see cref="System.Windows.Point"/></param>
		/// <returns>True if point1 and point2 have different X or Y coordinates, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(System.Windows.Point point1, System.Windows.Point point2)
		{	return (point1.X != point2.X || point1.Y != point2.Y);	}
        
		/// <summary>Compares two point structures for equality.</summary>
		/// <param name="point1">The first point to compare.<see cref="System.Windows.Point"/></param>
		/// <param name="point2">The second point to compare.<see cref="System.Windows.Point"/></param>
		/// <returns>True if point1 and point2 have equal X and Y coordinates, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator ==(System.Windows.Point point1, System.Windows.Point point2)
		{	return (point1.X == point2.X && point1.Y == point2.Y);	}
        
		/// <summary>Subtract the specified System.Windows.Vector from the specified System.Windows.Point
        /// and returns the resulting System.Windows.Point.</summary>
		/// <param name="point">The point from which vector is subtracted.<see cref=System.Windows."Point"/></param>
		/// <param name="vector">The vector to subtract from point1<see cref="System.Windows.Vector"/></param>
		/// <returns>The difference between point and vector.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point operator -(System.Windows.Point point, System.Windows.Vector vector)
		{	return new Point (point.X - vector.X, point.Y - vector.Y);	}
        
		/// <summary>Transform the specified System.Windows.Point by the specified System.Windows.Media.Matrix.</summary>
		/// <param name="point">The point to transform.<see cref="System.Windows.Point"/></param>
		/// <param name="matrix">The transformation matrix.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>The result of transforming the specified point using the specified matrix.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point operator *(System.Windows.Point point, System.Windows.Media.Matrix matrix)
		{	return matrix.Transform (point);	}
        
		/// <summary>Translate the specified System.Windows.Point by the specified System.Windows.Vector
        /// and returns the result.</summary>
		/// <param name="point">The point to translate.<see cref="System.Windows.Point"/></param>
		/// <param name="vector">The amount by which to translate point.<see cref="System.Windows.Vector"/></param>
		/// <returns>The result of translating the specified point by the specified vector.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point operator +(System.Windows.Point point, System.Windows.Vector vector)
		{	return new Point (point.X + vector.X, point.Y + vector.Y);	}
		
		/// <summary>Create a System.Windows.Vector structure with an System.Windows.Vector.X value equal to the point's
        /// System.Windows.Point.X value and a System.Windows.Vector.Y value equal to the point's System.Windows.Point.Y value.</summary>
		/// <param name="point">The point to convert.<see cref="System.Windows.Point"/></param>
		/// <returns>A vector with an System.Windows.Vector.X value equal to the point's System.Windows.Point.X value and
        /// a System.Windows.Vector.Y value equal to the point's System.Windows.Point.Y value.<see cref="System.Windows.Vector"/></returns>
        public static explicit operator Vector(System.Windows.Point point)
		{	return new System.Windows.Vector (point.X, point.Y);	}
        
		/// <summary>Create a System.Windows.Size structure with a System.Windows.Size.Width equal to this point's System.Windows.Point.X
        /// value and a System.Windows.Size.Height equal to this point's System.Windows.Point.Y value.</summary>
		/// <param name="point">The point to convert.<see cref="System.Windows.Point"/></param>
		/// <returns>A System.Windows.Size structure with a System.Windows.Size.Width equal to this point's System.Windows.Point.X
        /// value and a System.Windows.Size.Height equal to this point's System.Windows.Point.Y value.<see cref="System.Windows.Size"/>
		/// </returns>
        public static explicit operator Size(System.Windows.Point point)
		{	return new System.Windows.Size (point.X, point.Y);	}
        		
		#endregion Operators
		
		#region Static methods
		
		/// <summary>Convert the string representation of a point to its double-precision 2D point structure equivalent.
        /// A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="strPoint">A string containing a 2D point to convert.<see cref="System.String"/></param>
		/// <param name="point">When this method returns, the double-precision 2D point equivalent contains the
        /// point coordinates, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the s parameter
        /// is null, is not a point in a valid format, or represents a point with coordinates less than System.Double.MinValue or
        /// greater than System.Double.MaxValue. This parameter is passed uninitialized.<see cref="System.Windows.Point"/></param>
		/// <returns>True if s was converted successfully, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool TryParse (string strPoint, out System.Windows.Point point)
		{
			point = new System.Windows.Point (0, 0);
			
            char separator = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ListSeparator[0];
            string[] tokens = strPoint.Split(new char[] {separator});
			if (tokens.Length != 2)
				return false;
			double x;
			double y;
			if (double.TryParse (tokens[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out x) &&
			    double.TryParse (tokens[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out y))
			{
				point.X = x;
				point.Y = y;
				return true;
			}
			else
				return false;
		}
		
		/*
		/// <summary>Construct a System.Windows.Point from the specified System.String.</summary>
		/// <param name="source">A string representation of a point.<see cref="System.String"/></param>
		/// <returns>The equivalent System.Windows.Point structure.<see cref="Point"/></returns>
		/// <exception cref="System.FormatException">The source is not composed of two comma- or space-delimited double values.</exception>
		/// <exception cref="System.InvalidOperationException">The source does not contain two numbers.-or-source contains too many delimiters.</exception>
        public static System.Windows.Point Parse(string source);
		*/
		
		/// <summary>Add a System.Windows.Vector to a System.Windows.Point and returns the result
        /// as a System.Windows.Point structure.</summary>
		/// <param name="point">The System.Windows.Point structure to add.<see cref="System.Windows.Point"/></param>
		/// <param name="vector">The System.Windows.Vector structure to add.<see cref="System.Windows.Vector"/></param>
		/// <returns>Returns the sum of point and vector.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point Add(System.Windows.Point point, System.Windows.Vector vector)
		{	return new Point (point.X + vector.X, point.Y + vector.Y);	}
		
		/// <summary>Transform the specified System.Windows.Point structure by the specified
        /// System.Windows.Media.Matrix structure.</summary>
		/// <param name="point">The point to transform.<see cref="System.Windows.Point"/></param>
		/// <param name="matrix">The transformation matrix.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>The transformed point.<see cref="Point"/></returns>
        public static System.Windows.Point Multiply(System.Windows.Point point, System.Windows.Media.Matrix matrix)
		{	return matrix.Transform (point);	}

		/// <summary>Subtracts the specified System.Windows.Point from another specified System.Windows.Point
        /// and returns the difference as a System.Windows.Vector.</summary>
		/// <param name="point1">The point from which point2 is subtracted.<see cref="System.Windows.Point"/></param>
		/// <param name="point2">The point to subtract from point1.<see cref="System.Windows.Point"/></param>
		/// <returns>The difference between point1 and point2.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Subtract(System.Windows.Point point1, System.Windows.Point point2)
		{	return new System.Windows.Vector (point1.X - point2.X, point1.Y - point2.Y);	}

		/// <summary>Subtracts the specified System.Windows.Vector from the specified System.Windows.Point
        /// and returns the resulting System.Windows.Point.</summary>
		/// <param name="point">The point from which vector is subtracted.<see cref="System.Windows.Point"/></param>
		/// <param name="vector">The vector to subtract from point.<see cref="System.Windows.Vector"/></param>
		/// <returns>The difference between point and vector.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point Subtract(System.Windows.Point point, System.Windows.Vector vector)
		{	return new System.Windows.Point (point.X - vector.X, point.Y - vector.Y);	}

		#endregion Static methods
		
		#region Methods

		/// <summary>Determine whether the specified System.Object is a System.Windows.Point
        /// and whether it contains the same coordinates as this System.Windows.Point.</summary>
		/// <param name="obj">The System.Object to compare.<see cref="System.Object"/></param>
		/// <returns>True if o is a System.Windows.Point and contains the same System.Windows.Point.X and
        /// System.Windows.Point.Y values as this System.Windows.Point; otherwise, false.<see cref="System.Boolean"/></returns>
        public override bool Equals (object obj)
		{	return obj is Point && this.Equals ((Point)obj);		}
		
		/// <summary>Compare two System.Windows.Point structures for equality.</summary>
		/// <param name="value">The point to compare to this instance.<see cref="System.Windows.Point"/></param>
		/// <returns>True if both System.Windows.Point structures contain the same System.Windows.Point.X
        /// and System.Windows.Point.Y values; otherwise, false.<see cref="System.Boolean"/></returns>
        public bool Equals (System.Windows.Point value)
		{
			if (_x == value._x && _y == value._y)
				return true;
			else
				return false;
		}
		
		/// <summary>Compare two System.Windows.Point structures for equality.</summary>
		/// <param name="point1">The first point to compare.<see cref="Point"/></param>
		/// <param name="point2">The second point to compare.<see cref="Point"/></param>
		/// <returns>True if point1 and point2 contain the same System.Windows.Point.X and System.Windows.Point.Y
        /// values; otherwise, false.<see cref="System.Boolean"/></returns>
        public static bool Equals (Point point1, Point point2)
		{
			if (point1._x == point2._x && point1._y == point2._y)
				return true;
			else
				return false;
		}
		
		/// <summary>Returns the hash code for this System.Windows.Point.</summary>
		/// <returns>The hash code for this System.Windows.Point structure.<see cref="System.Int32"/></returns>
        public override int GetHashCode()
		{
			return (_x.GetHashCode () ^ _y.GetHashCode ());
		}

		/// <summary>Offsets a point's System.Windows.Point.X and System.Windows.Point.Y coordinates
        /// by the specified amounts.</summary>
		/// <param name="offsetX">The amount to offset the point'sSystem.Windows.Point.X coordinate.<see cref="System.Double"/></param>
		/// <param name="offsetY">The amount to offset thepoint's System.Windows.Point.Y coordinate.<see cref="System.Double"/></param>
        public void Offset(double offsetX, double offsetY)
		{	_x += offsetX; _y += offsetY;	}

		/// <summary>Creates a System.String representation of this System.Windows.Point.</summary>
        /// <returns>A System.String containing the System.Windows.Point.X and System.Windows.Point.Y
        /// values of this System.Windows.Point structure.<see cref="System.String"/></returns>
        public override string ToString ()
		{
			return string.Format ("{0};{1}", _x, _y);
		}

		/// <summary>Creates a System.String representation of this System.Windows.Point.</summary>
        /// <param name="provider">Culture-specific formatting information.<see cref="IFormatProvider"/></param>
        /// <returns>A System.String containing the System.Windows.Point.X and System.Windows.Point.Y
        /// values of this System.Windows.Point structure.<see cref="System.String"/></returns>
        public string ToString (IFormatProvider provider)
		{
			return string.Format (provider, "{0};{1}", _x, _y);
		}

        /// <summary>Returns a string representation of the rectangle by using the specified format provider.</summary>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format
        /// defined for the type of the System.IFormattable implementation. <see cref="System.String"/></param>
		/// <param name="provider">Culture-specific formatting information.<see cref="IFormatProvider"/></param>
        /// <returns>A string representation of the current rectangle that is determined by the
        /// specified format provider.<see cref="System.String"/></returns>
        public string ToString (string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty (format))
				return ToString (provider);
			else
				return string.Format (provider, format, _x, _y);
		}

		#endregion Methods

	}
}