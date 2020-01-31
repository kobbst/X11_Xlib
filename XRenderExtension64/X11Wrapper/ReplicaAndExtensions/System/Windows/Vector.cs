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
    /// <summary>Represent a displacement in 2-D space.</summary>
    public struct Vector : IFormattable
    {

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "Vector";

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
		
		/// <summary>Initialize a new instance of the System.Windows.Vector structure.</summary>
		/// <param name="x">The System.Windows.Vector.X-offset of the new System.Windows.Vector.<see cref="System.Double"/>/param>
		/// <param name="y">The System.Windows.Vector.Y-offset of the new System.Windows.Vector.<see cref="System.Double"/></param>
        public Vector (double x, double y)
		{	_x = x;
			_y = y;
		}
		
        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

		/// <summary>Get the length of this vector.</summary>
		/// <returns>The length of this vector.<see cref="System.Double"/></returns>
        public double Length
		{	get	{	return Math.Sqrt(_x * _x + _y * _y);	}	}

		/// <summary>Get the square of the length of this vector.</summary>
		/// <returns>The square of the System.Windows.Vector.Length of this vector.<see cref="System.Double"/></returns>
        public double LengthSquared
		{	get	{	return _x * _x + _y * _y;	}	}

		/// <summary>Get or set the System.Windows.Vector.X component of this vector.</summary>
        /// <returns>The System.Windows.Vector.X component of this vector. The default value is 0.<see cref="System.Double"/></returns>
        public double X
		{	get	{	return _x;	}
			set	{	_x = value;	}
		}

		/// <summary>Get or sets the System.Windows.Vector.Y component of this vector.</summary>
        /// <returns>The System.Windows.Vector.Y component of this vector. The default value is 0.<see cref="System.Double"/></returns>
         public double Y
		{	get	{	return _y;	}
			set	{	_y = value;	}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Operators

		/// <summary>Negate the specified vector.</summary>
		/// <param name="System.Windows.Vector">The vector to negate.<see cref="System.Windows.Vector"/></param>
		/// <returns>A vector with System.Windows.Vector.X and System.Windows.Vector.Y values opposite
        /// of the System.Windows.Vector.X and System.Windows.Vector.Y values of vector.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector operator -(System.Windows.Vector vector)
		{	return new System.Windows.Vector (-vector.X, -vector.Y);	}
		
		/// <summary>Subtract one specified vector from another.</summary>
		/// <param name="vector1">The vector from which vector2 is subtracted.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The vector to subtract from vector1.<see cref="System.Windows.Vector"/></param>
		/// <returns>The difference between vector1 and vector2.<see cref="System.Windows.Vector"/></returns>
        public static Vector operator -(System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return new System.Windows.Vector (vector1.X - vector2.X, vector1.Y - vector2.Y);	}
		
		/// <summary>Compare two vectors for inequality.</summary>
		/// <param name="vector1">The first vector to compare.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to compare.<see cref="System.Windows.Vector"/></param>
		/// <returns>True if the System.Windows.Vector.X and System.Windows.Vector.Y components
        /// of vector1 and vector2 are different, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return (vector1.X != vector2.X && vector1.Y != vector2.Y);	}
		
		/// <summary>Multiply the specified scalar by the specified vector and returns the resulting vector.</summary>
		/// <param name="scalar">The scalar to multiply.<see cref="System.Double"/></param>
		/// <param name="vector">The vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <returns> The result of multiplying scalar and vector.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector operator *(double scalar, System.Windows.Vector vector)
		{	return new System.Windows.Vector (vector.X * scalar, vector.Y * scalar);	}

		/// <summary>Multiply the specified vector by the specified scalar and returns the resulting vector.</summary>
		/// <param name="vector">The vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <param name="scalar">The scalar to multiply.<see cref="System.Double"/></param>
		/// <returns>The result of multiplying vector and scalar.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector operator *(System.Windows.Vector vector, double scalar)
		{	return new System.Windows.Vector (vector.X * scalar, vector.Y * scalar);	}

		/// <summary>Transform the coordinate space of the specified vector using the specified
        /// System.Windows.Media.Matrix. </summary>
		/// <param name="vector">The vector to transform.<see cref="Vector"/></param>
		/// <param name="matrix">The transformation to apply to vector.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>The result of transforming vector by matrix.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector operator *(System.Windows.Vector vector, System.Windows.Media.Matrix matrix)
		{	return matrix.Transform (vector);	}

		/// <summary>Calculate the dot product of the two specified vector structures and returns
        /// the result as a System.Double.</summary>
		/// <param name="vector1">The first vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <returns>Return a System.Double containing the scalar dot product of vector1 and vector2, which is calculated
        /// using the following formula:vector1.X * vector2.X + vector1.Y * vector2.Y<see cref="System.Double"/></returns>
        public static double operator *(System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return vector1.X * vector2.X + vector1.Y * vector2.Y;	}

		/// <summary>Divide the specified vector by the specified scalar and returns the resulting vector.</summary>
		/// <param name="vector">The vector to divide.<see cref="System.Windows.Vector"/></param>
		/// <param name="scalar">The scalar by which vector will be divided.<see cref="System.Double"/></param>
		/// <returns>The result of dividing vector by scalar.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector operator /(System.Windows.Vector vector, double scalar)
		{	if (scalar != 0)
				return new System.Windows.Vector (vector.X / scalar, vector.X / scalar);
			else
				return new System.Windows.Vector (double.MaxValue, double.MaxValue);
		}

		/// <summary>Translate a point by the specified vector and returns the resulting point.</summary>
		/// <param name="vector">The vector used to translate point.<see cref="System.Windows.Vector"/></param>
		/// <param name="point">The point to translate.<see cref="System.Windows.Point"/></param>
		/// <returns>The result of translating point by vector.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point operator +(System.Windows.Vector vector, System.Windows.Point point)
		{	return new System.Windows.Point (vector.X + point.X, vector.Y + point.Y);	}

		/// <summary>Add two vectors and returns the result as a vector.</summary>
		/// <param name="vector1">The first vector to add.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to add.<see cref="System.Windows.Vector"/></param>
		/// <returns>The sum of vector1 and vector2. <see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector operator +(System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return new System.Windows.Vector (vector1.X + vector2.X, vector1.Y + vector2.Y);	}

		/// <summary>Compare two vectors for equality.</summary>
		/// <param name="vector1">The first vector to compare.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to compare.<see cref="System.Windows.Vector"/></param>
		/// <returns>True if the System.Windows.Vector.X and System.Windows.Vector.Y components
        /// of vector1 and vector2 are equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator ==(System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return (vector1.X == vector2.X && vector1.Y == vector2.Y);	}
		
		/// <summary>Create a System.Windows.Size from the offsets of this vector.</summary>
		/// <param name="vector">The vector to convert.<see cref="System.Windows.Vector"/></param>
		/// <returns>A System.Windows.Size with a System.Windows.Size.Width equal to the absolute value of this vector's
        /// System.Windows.Vector.X property and a System.Windows.Size.Height equal to the absolute value
        /// of this vector's System.Windows.Vector.Y property.<see cref="System.Windows.Size"/></returns>
        public static explicit operator System.Windows.Size (System.Windows.Vector vector)
		{	return new System.Windows.Size (vector.X, vector.Y);	}

		/// <summary>Create a System.Windows.Point with the System.Windows.Vector.X and System.Windows.Vector.Y
        /// values of this vector.</summary>
		/// <param name="vector">The vector to convert.<see cref="System.Windows.Vector"/>
		/// </param>A point with System.Windows.Point.X- and System.Windows.Point.Y-coordinate values equal to
        /// the System.Windows.Vector.X and System.Windows.Vector.Y offset values of vector.<see cref="System.Windows.Point"/></returns>
        public static explicit operator System.Windows.Point (System.Windows.Vector vector)
		{	return new System.Windows.Point (vector.X, vector.Y);	}
		
		#endregion Operators
		
		#region Static methods

		/// <summary>Translate the specified point by the specified vector and returns the resulting point.</summary>
		/// <param name="vector">The amount to translate the specified point.<see cref="System.Windows.Vector"/></param>
		/// <param name="point">The point to translate.<see cref="System.Windows.Point"/></param>
		/// <returns>The result of translating point by vector.<see cref="System.Windows.Point"/></returns>
        public static System.Windows.Point Add (System.Windows.Vector vector, System.Windows.Point point)
		{	return new System.Windows.Point (vector.X * point.X, vector.Y * point.Y);	}
		
		/// <summary>Add two vectors and returns the result as a System.Windows.Vector structure.</summary>
		/// <param name="vector1">The first vector to add.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to add.<see cref="System.Windows.Vector"/></param>
		/// <returns>The sum of vector1 and vector2.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Add (System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return new System.Windows.Vector (vector1.X + vector2.X, vector1.Y + vector2.Y);	}

		/// <summary>Retrieve the angle, expressed in degrees, between the two specified vectors.</summary>
		/// <param name="vector1">The first vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <returns>The angle, in radiants clockwise, between vector1 and vector2.<see cref="System.Double"/></returns>
        public static double AngleBetweenRad (System.Windows.Vector vector1, System.Windows.Vector vector2)
        {	double length = vector1.Length * vector2.Length;
			if (length == 0)
				return 0;
			double cosine = Multiply (vector1, vector2) / (length);
			return (vector2.Y >= 0 ? Math.Acos (cosine) : 2 * Math.PI - Math.Acos (cosine));
		}

		/// <summary>Retrieve the angle, expressed in degrees clockwise, between the two specified vectors.</summary>
		/// <param name="vector1">The first vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <returns>The angle, in degrees clockwise, between vector1 and vector2.<see cref="System.Double"/></returns>
        public static double AngleBetween (System.Windows.Vector vector1, System.Windows.Vector vector2)
        {	double length = vector1.Length * vector2.Length;
			if (length == 0)
				return 0;
			double cosine = Multiply (vector1, vector2) / (length);
			return (vector2.Y >= 0 ? Math.Acos (cosine) * 180 / Math.PI : 360 - Math.Acos (cosine) * 180 / Math.PI);
		}

		/// <summary>Calculate the cross product of two vectors.</summary>
		/// <param name="vector1">The first vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <returns>The cross product of vector1 and vector2. The following formula is used to calculate
        /// the cross product: (Vector1.X * Vector2.Y) - (Vector1.Y * Vector2.X)<see cref="System.Double"/></returns>
        public static double CrossProduct (System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return (vector1.X * vector2.Y) - (vector1.Y * vector2.X);	}
		
		/// <summary> Calculate the determinant of two vectors.</summary>
		/// <param name="vector1">The first vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to evaluate.<see cref="System.Windows.Vector"/></param>
		/// <returns>The determinant of vector1 and vector2.<see cref="System.Double"/></returns>
        public static double Determinant (System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return (vector1.X - vector2.Y) - (vector1.Y - vector2.X);	}
        
		/// <summary>Divide the specified vector by the specified scalar and returns the result
        /// as a System.Windows.Vector.</summary>
		/// <param name="vector">The vector structure to divide.<see cref="System.Windows.Vector"/></param>
		/// <param name="scalar">The amount by which vector is divided.<see cref="System.Double"/></param>
		/// <returns>The result of dividing vector by scalar.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Divide (System.Windows.Vector vector, double scalar)
		{	if (scalar != 0)
				return new System.Windows.Vector (vector.X / scalar, vector.X / scalar);
			else
				return new System.Windows.Vector (double.MaxValue, double.MaxValue);
		}

		/// <summary>Compare the two specified vectors for equality.</summary>
		/// <param name="vector1">The first vector to compare.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector to compare.<see cref="System.Windows.Vector"/></param>
		/// <returns>True if t he System.Windows.Vector.X and System.Windows.Vector.Y components
        /// of vector1 and vector2 are equal; otherwise, false.<see cref="System.Boolean"/>
		/// </returns>
        public static bool Equals (System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return (vector1.X == vector2.X && vector1.Y == vector2.Y);	}

		/// <summary>Multiply the specified scalar by the specified vector and returns the resulting
        /// System.Windows.Vector.</summary>
		/// <param name="scalar">The scalar to multiply.<see cref="System.Double"/></param>
		/// <param name="vector">The vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <returns>The result of multiplying scalar and vector.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Multiply (double scalar, System.Windows.Vector vector)
		{	return new System.Windows.Vector (vector.X * scalar, vector.Y * scalar);	}

		/// <summary>Multiplie the specified vector by the specified scalar and returns the resulting
        /// System.Windows.Vector.</summary>
		/// <param name="vector">The vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <param name="scalar">The scalar to multiply.<see cref="System.Double"/></param>
		/// <returns>The result of multiplying vector and scalar.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Multiply (System.Windows.Vector vector, double scalar)
		{	return new System.Windows.Vector (vector.X * scalar, vector.Y * scalar);	}

		/// <summary>Transform the coordinate space of the specified vector using the specified
        /// System.Windows.Media.Matrix.</summary>
		/// <param name="vector">The vector structure to transform.<see cref="System.Windows.Vector"/></param>
		/// <param name="matrix">The transformation to apply to vector.<see cref="Matrix"/></param>
		/// <returns>The result of transforming vector by matrix.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Multiply(System.Windows.Vector vector, System.Windows.Media.Matrix matrix)
		{	return matrix.Transform (vector);	}

		/// <summary>Calculate the dot product of the two specified vectors and returns the result
        /// as a System.Double.</summary>
		/// <param name="vector1">The first vector to multiply.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The second vector structure to multiply.<see cref="System.Windows.Vector"/></param>
		/// <returns>A System.Double containing the scalar dot product of vector1 and vector2, which is calculated using
        /// the following formula: (vector1.X * vector2.X) + (vector1.Y * vector2.Y)<see cref="System.Double"/></returns>
        public static double Multiply (System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return vector1.X * vector2.X + vector1.Y * vector2.Y;	}
		
		/// <summary>Convert the string representation of a point to its double-precision 2D point structure equivalent.
        /// A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="pointTag">A string containing a 2D point to convert.<see cref="System.String"/></param>
		/// <param name="vector">When this method returns, the double-precision 2D point equivalent contains the
        /// point coordinates, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the s parameter
        /// is null, is not a point in a valid format, or represents a point with coordinates less than System.Double.MinValue or
        /// greater than System.Double.MaxValue. This parameter is passed uninitialized.<see cref="System.Windows.Vector"/></param>
		/// <returns>True if s was converted successfully, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool TryParse (string s, out System.Windows.Vector vector)
		{
			vector = new System.Windows.Vector (0, 0);
			
			string[] sTags = s.Split (new char[] {','});
			if (sTags.Length != 2)
				return false;
			double x;
			double y;
			if (double.TryParse (sTags[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out x) &&
			    double.TryParse (sTags[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out y))
			{
				vector.X = x;
				vector.Y = y;
				return true;
			}
			else
				return false;
		}
        /*   
		/// <summary>Convert a string representation of a vector into the equivalent System.Windows.Vector
        /// structure.</summary>
		/// <param name="source">The string representation of the vector.<see cref="System.String"/></param>
		/// <returns>The equivalent System.Windows.Vector structure.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Parse(string source);
        */
		/// <summary>Subtract the specified vector from another specified vector.</summary>
		/// <param name="vector1">The vector from which vector2 is subtracted.<see cref="System.Windows.Vector"/></param>
		/// <param name="vector2">The vector to subtract from vector1.<see cref="System.Windows.Vector"/></param>
		/// <returns>The difference between vector1 and vector2.<see cref="System.Windows.Vector"/></returns>
        public static System.Windows.Vector Subtract (System.Windows.Vector vector1, System.Windows.Vector vector2)
		{	return new System.Windows.Vector (vector1.X - vector2.X, vector1.X - vector2.Y);	}
		
		#endregion Static methods
		
		#region Methods

		/// <summary>Determine whether the specified System.Object is a System.Windows.Vector
        //// structure and, if it is, whether it has the same System.Windows.Vector.X
        //// and System.Windows.Vector.Y values as this vector.</summary>
		/// <param name="obj">The vector to compare.<see cref="System.Object"/></param>
		/// <returns>True if o is a System.Windows.Vector and has the same System.Windows.Vector.X
        //     and System.Windows.Vector.Y values as this vector, or false otherwise.<see cref="System.Boolean"/>
		/// </returns>
        public override bool Equals(object obj)
		{	return obj is System.Windows.Vector && Equals((System.Windows.Vector)obj);	}

		/// <summary>Compare two vectors for equality.</summary>
		/// <param name="value">The vector to compare with this vector.<see cref="System.Windows.Vector"/></param>
		/// <returns>True if value has the same System.Windows.Vector.X and System.Windows.Vector.Y
        /// values as this vector; otherwise, false.<see cref="System.Boolean"/></returns>
        public bool Equals(System.Windows.Vector value)
		{	return (_x == value.X && _y == value.Y);	}

		/// <summary>Return the hash code for this vector.</summary>
		/// <returns>The hash code for this instance.<see cref="System.Int32"/></returns>
        public override int GetHashCode()
		{	return _x.GetHashCode () ^ _y.GetHashCode ();	}

        /// <summary>Negate this vector. The vector has the same magnitude as before, but its direction is now opposite.</summary>    
        public void Negate()
		{	_x = -_x;	_y = -_y;	}
		
        /// <summary>Normalizes this vector.</summary>
        public void Normalize()
		{	double length = Length;
			if (length != 0)
			{	_x = _x / length; _y = _y / length;	}
			else
			{	_x = 1; _y = 1;	}
		}

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
        /// <returns>A string representation of the current vector that is determined by the
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