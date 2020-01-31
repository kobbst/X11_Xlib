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

// Replica
using System.Windows;

namespace System.Windows.Media
{
    /// <summary>Represents a 3x3 affine transformation matrix used for transformations in 2-D space.</summary>
    public struct Matrix : IFormattable
    {
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "Matrix";

		#endregion Constants

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################
		
		#region Attributes		
		
		/// <summary>The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M11 coefficient (at col. 1 and row 1).</summary>
		private double _m11;
		
		/// <summary>The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M12 coefficient (at col. 1 and row 2).</summary>
		private double _m12;
		
		/// <summary>The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M21 coefficient (at col. 2 and row 1).</summary>
		private double _m21;
		
		/// <summary>The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M22 coefficient (at col. 2 and row 2).</summary>
		private double _m22;
		
		/// <summary>The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.OffsetX coefficient.</summary>
		private double _offsetX;
		
		/// <summary>The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.OffsetY coefficient.</summary>
		private double _offsetY;
		
		/// <summary>The identity System.Windows.Media.Matrix.</summary>
		private static System.Windows.Media.Matrix _identity = new System.Windows.Media.Matrix (1, 0, 0, 1, 0, 0);
		
        #endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initialize a new instance of the System.Windows.Media.Matrix structure.</summary>
		/// <param name="m11">The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M11
        /// coefficient.<see cref="System.Double"/></param>
		/// <param name="m12">The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M12
        /// coefficient.<see cref="System.Double"/></param>
		/// <param name="m21">The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M21
        /// coefficient.<see cref="System.Double"/></param>
		/// <param name="m22">The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M22
        /// coefficient.<see cref="System.Double"/></param>
		/// <param name="offsetX">The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.OffsetX
		/// coefficient.<see cref="System.Double"/></param>
		/// <param name="offsetY">The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.OffsetY
		/// coefficient.<see cref="System.Double"/></param>
        public Matrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
		{
			_m11 = m11;
			_m12 = m12;
			_m21 = m21;
			_m22 = m22;
			_offsetX = offsetX;
			_offsetY = offsetY;
		}
		
		/// <summary>Initialize a new instance of the System.Windows.Media.Matrix structure.</summary>
		/// <param name="m">The matrix to create the new matrix from.<see cref="System.Windows.Media.Matrix"/></param>
        public Matrix(ref System.Windows.Media.Matrix m)
		{
			_m11 = m.M11;
			_m12 = m.M12;
			_m21 = m.M21;
			_m22 = m.M22;
			_offsetX = m.OffsetX;
			_offsetY = m.OffsetY;
		}

        #endregion Construction

        #region Operators

		/// <summary>Determine whether the two specified System.Windows.Media.Matrix structures are not identical.</summary>
		/// <param name="matrix1">The first System.Windows.Media.Matrix structure to compare.<see cref="System.Windows.Media.Matrix"/></param>
		/// <param name="matrix2">The second System.Windows.Media.Matrix structure to compare.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>True if matrix1 and matrix2 are not identical; otherwise, false.<see cref="System.Boolean"/></returns>
        public static bool operator !=(System.Windows.Media.Matrix matrix1, System.Windows.Media.Matrix matrix2)
		{
			if (matrix1._m11 == matrix2._m11 && matrix1._m12 == matrix2._m12 && matrix1._m21 == matrix2._m21 && matrix1._m22 == matrix2._m22 &&
			    matrix1._offsetX == matrix2._offsetX && matrix1._offsetY == matrix2._offsetY)
				return false;
			return true;
		}


 		/// <summary>Multiply a System.Windows.Media.Matrix structure by another System.Windows.Media.Matrix structure.</summary>
		/// <param name="trans1">The first System.Windows.Media.Matrix structure to multiply.<see cref="System.Windows.Media.Matrix"/></param>
		/// <param name="trans2">The second System.Windows.Media.Matrix structure to multiply.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>The result of multiplying trans1 by trans2.<see cref="Matrix"/></returns>
        public static System.Windows.Media.Matrix operator *(System.Windows.Media.Matrix trans1, System.Windows.Media.Matrix trans2)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Media.Matrix result = new System.Windows.Media.Matrix(1, 0, 0, 1, 0, 0);
			
			result._m11     = trans1._m11     * trans2._m11   +   trans1._m21     * trans2._m12;
			result._m21     = trans1._m11     * trans2._m21   +   trans1._m21     * trans2._m22;

			result._m12     = trans1._m12     * trans2._m11   +   trans1._m22     * trans2._m12;
			result._m22     = trans1._m12     * trans2._m21   +   trans1._m22     * trans2._m22;

			result._offsetX = trans1._offsetX * trans2._m11   +   trans1._offsetY * trans2._m12;
			result._offsetY = trans1._offsetX * trans2._m21   +   trans1._offsetY * trans2._m22;
			
			return result;
		}
	
		/// <summary>Determines whether the two specified System.Windows.Media.Matrix structures are identical.</summary> 
		/// <param name="matrix1">The first System.Windows.Media.Matrix structure to compare.<see cref="System.Windows.Media.Matrix"/></param>
		/// <param name="matrix2">The second System.Windows.Media.Matrix structure to compare.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>True if matrix1 and matrix2 are identical; otherwise, false.<see cref="System.Boolean"/></returns>
        public static bool operator ==(System.Windows.Media.Matrix matrix1, System.Windows.Media.Matrix matrix2)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			

			if (matrix1._m11 == matrix2._m11 && matrix1._m12 == matrix2._m12 &&
			    matrix1._m21 == matrix2._m21 && matrix1._m22 == matrix2._m22 &&
			    matrix1._offsetX == matrix2._offsetX && matrix1._offsetY == matrix2._offsetY)
				return true;
			return false;
		}

		#endregion Operators
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

		/// <summary>Get the determinant of this System.Windows.Media.Matrix structure.</summary>
		/// <returns>The determinant of this System.Windows.Media.Matrix.<see cref="System.Windows.Media.Matrix"/></returns>
        public double Determinant
        {  get
	        {	// Matrix structure:
				// m11 m12 OffsetX
				// m21 m22 OffsetY
			
				return (_m11 * _m22 - _m21 * _m12);
				//return _m11 * _m22 * 1.0      +  _m12 * _offsetY * 0.0  +  _offsetX * _m21 * 0.0 −
				//        0.0 * _m22 * _offsetX −   0.0 * _offsetY * _m11 −    1.0 *    _m21 * _m12;
	        }
        }
		
		/// <summary>Get a value that indicates whether this System.Windows.Media.Matrix structure is invertible.</summary>
        /// <returns>True if the System.Windows.Media.Matrix has an inverse; otherwise, false.
        /// The default is true.<see cref="System.Boolean"/></returns>
        public bool HasInverse
		{	get
			{	try
				{
					Invert(this);
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>Get an identity System.Windows.Media.Matrix.</summary>
		/// <returns>An identity matrix.<see cref="System.Windows.Media.Matrix"/></returns>
        public static Matrix Identity
        {	get
			{	return _identity;	}
		}

		/// <summary>Gets a value that indicates whether this System.Windows.Media.Matrix structure is an identity matrix.</summary>
		/// <returns>True if the System.Windows.Media.Matrix structure is an identity matrix, or false otherwise. The default is true.</returns>
        public bool IsIdentity
		{	get
			{
				if (_m11 == 1 && _m12 == 0 && _m21 == 0 && _m22 == 1 && _offsetX == 0 && _offsetY == 0)
					return true;
				return false;
			}
		}
		
        /// <summary>Get or set the value of the first row and first column of this System.Windows.Media.Matrix structure.</summary>
        /// <returns>The value of the first row and first column of this System.Windows.Media.Matrix.
        /// The default value is 1.</returns>
        public double M11
		{	get	{	return _m11;	}
			set	{	_m11 = value;	}
		}

		/// <summary>Get or set the value of the first row and second column of this System.Windows.Media.Matrix structure.</summary>
		/// <returns>The value of the first row and second column of this System.Windows.Media.Matrix.
        /// The default value is 0.</returns>
        public double M12
		{	get	{	return _m21;	}
			set	{	_m21 = value;	}
		}
		
		/// <summary>Get or set the value of the second row and first column of this System.Windows.Media.Matri structure.</summary>
		/// <returns>The value of the second row and first column of this System.Windows.Media.Matrix.
        /// The default value is 0.</returns>
        public double M21
		{	get	{	return _m12;	}
			set	{	_m12 = value;	}
		}

		/// <summary>Get or set the value of the second row and second column of this System.Windows.Media.Matrix structure.</summary>
		/// <returns>The value of the second row and second column of this System.Windows.Media.Matrix
        /// structure. The default value is 1.</returns>
        public double M22
		{	get	{	return _m22;	}
			set	{	_m22 = value;	}
		}

		/// <summary>Get or set the value of the third row and first column of this System.Windows.Media.Matrix structure.</summary>
		/// <returns>The value of the third row and first column of this System.Windows.Media.Matrix
        /// structure. The default value is 0.</returns>
        public double OffsetX
		{	get	{	return _offsetX;	}
			set	{	_offsetX = value;	}
		}

		/// <summary>Get or set the value of the third row and second column of this System.Windows.Media.Matrix structure.</summary>
		/// <returns>The value of the third row and second column of this System.Windows.Media.Matrix
        /// structure. The default value is 0.</returns>
        public double OffsetY
		{	get	{	return _offsetY;	}
			set	{	_offsetY = value;	}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Methods

		/// <summary>Append the specified System.Windows.Media.Matrix structure to this System.Windows.Media.Matrix structure</summary>
		/// <param name="matrix">The System.Windows.Media.Matrix structure to append to this System.Windows.Media.Matrix
        /// structure.<see cref="System.Windows.Media.Matrix"/></param>
        public void Append(System.Windows.Media.Matrix matrix)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Media.Matrix result = new System.Windows.Media.Matrix(1, 0, 0, 1, 0, 0);
			
			result._m11     = _m11     * matrix._m11   +   _m21     * matrix._m12;
			result._m21     = _m11     * matrix._m21   +   _m21     * matrix._m22;

			result._m12     = _m12     * matrix._m11   +   _m22     * matrix._m12;
			result._m22     = _m12     * matrix._m21   +   _m22     * matrix._m22;

			result._offsetX = _offsetX * matrix._m11   +   _offsetY * matrix._m12;
			result._offsetY = _offsetX * matrix._m21   +   _offsetY * matrix._m22;
			
			_m11 = result._m11;
			_m21 = result._m21;
			_m12 = result._m12;
			_m22 = result._m22;
			_offsetX = result._offsetX + matrix._offsetX;
			_offsetY = result._offsetY + matrix._offsetY;
		}

		/// <summary>Determine whether the specified System.Windows.Media.Matrix structure is
        /// identical to this instance.</summary>
		/// <param name="value">The instance of System.Windows.Media.Matrix to compare to this instance.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>True if instances are equal, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals (System.Windows.Media.Matrix value)
		{
			if (value._m11 == _m11 && value._m12 == _m12 && value._m21 == _m21 && value._m22 == _m22 &&
			    value._offsetX == _offsetX && value._offsetY == _offsetY)
				return true;
			return false;
		}
        
		/// <summary>Determine whether the specified System.Object is a System.Windows.Media.Matrix
        /// structure that is identical to this System.Windows.Media.Matrix.</summary>
		/// <param name="obj">The System.Object to compare.<see cref="System.Object"/></param>
		/// <returns>True if o is a System.Windows.Media.Matrix structure that is identical to
        /// this System.Windows.Media.Matrix structure; otherwise, false.<see cref="System.Boolean"/></returns>
        public override bool Equals (object obj)
		{	return obj is System.Windows.Media.Matrix && this.Equals ((System.Windows.Media.Matrix)obj);		}

		/// <summary>Retrieves the hash code for this object.</summary>
		/// <returns>A 32-bit hash code, which is a signed integer.<see cref="System.Int32"/></returns>
        public override int GetHashCode ()
		{	return	_m11.GetHashCode() ^ _m12.GetHashCode() ^ _m21.GetHashCode() ^ _m22.GetHashCode() ^
					_offsetX.GetHashCode() ^ _offsetY.GetHashCode();	}

		/// <summary>Inverts this System.Windows.Media.Matrix structure.</summary>
		/// <exception cref="System.InvalidOperationException">The System.Windows.Media.Matrix structure is not invertible.</exception>
        public void Invert()
		{	Invert (this);	}
		
		/// <summary>Invert the indicated System.Windows.Media.Matrix structure.</summary>
		/// <param name="m">The matrix to invert.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>The inverted matrix.<see cref="System.Windows.Media.Matrix"/></returns>
		/// <exception cref="System.InvalidOperationException">The System.Windows.Media.Matrix structure is not invertible.</exception>
        private static System.Windows.Media.Matrix Invert (System.Windows.Media.Matrix m)
		{
			// Check whether inverse can be calculated.
			if (m._m11 == 0 && m._m21 == 0)
				throw new System.InvalidOperationException ();
			if (m._m12 == 0 && m._m22 == 0)
				throw new System.InvalidOperationException ();
				
			// Use Gauss-Jordan.
			double[] row0 = new double[4];
			double[] row1 = new double[4];
			
			if (m._m11 != 0)
			{
				row0[0] = m._m11;
				row0[1] = m._m12;
				row0[2] = 1;
				row0[3] = 0;
				row1[0] = m._m21;
				row1[1] = m._m22;
				row1[2] = 0;
				row1[3] = 1;
			}
			else
			{
				row1[0] = m._m11;
				row1[1] = m._m12;
				row1[2] = 1;
				row1[3] = 0;
				row0[0] = m._m21;
				row0[1] = m._m22;
				row0[2] = 0;
				row0[3] = 1;
			}
			
			// Step 1.
			if (row1[0] == 0)
			{	// Skip step 1.
				;
			}
			else
			{
				// Calculate step 1.
				double factor1 = -row1[0] / row0[0];
				
				for (int index = 0; index < 4; index++)
				{
					row1[index] += row0[index] * factor1;
				}
			}
			
			// Check whether inverse can be calculated.
			if (row1[1] == 0)
				throw new System.InvalidOperationException ();
			
			// Step 2.
			if (row0[1] == 0)
			{	// Skip step 2.
				;
			}
			else
			{	// Calculate step 2.
				
				double factor2 = -row0[1] / row1[1];
				
				for (int index = 0; index < 4; index++)
				{
					row0[index] += row1[index] * factor2;
				}
			}
			
			// Step 3.
			if (row0[0] < 0)
			{
				for (int index = 0; index < 4; index++)
				{
					row0[index] = -row0[index];
				}
			}
			if (row1[1] < 0)
			{
				for (int index = 0; index < 4; index++)
				{
					row1[index] = -row1[index];
				}
			}
			
			return new System.Windows.Media.Matrix (row0[2], row0[3], row1[2], row1[3],
													(m._offsetX != 0 ? 1 / m._offsetX : double.MaxValue),
													(m._offsetY != 0 ? 1 / m._offsetY : double.MaxValue));
		}
		

		/// <summary>Transform the specified bounding box and return an axis-aligned bounding
        /// box that is exactly large enough to contain it.</summary>
		/// <param name="rect">The bounding box to transform.<see cref="System.Windows.Rect"/></param>
		/// <returns>The smallest axis-aligned bounding box that can contain the transformed rect.<see cref="System.Windows.Rect"/></returns>
        public System.Windows.Rect TransformBounds(System.Windows.Rect rect)
		{
			System.Windows.Point untransformed = new System.Windows.Point (0, 0);
			System.Windows.Point transformed   = new System.Windows.Point (0, 0);
			System.Windows.Rect  result        = new System.Windows.Rect  (0, 0, 0, 0);
			
			for (int index = 0; index < 4; index++)
			{
				if (index == 0)
					untransformed = new Point (rect.Left,  rect.Top);
				else if (index == 1)
					untransformed = new Point (rect.Right, rect.Top);
				else if (index == 2)
					untransformed = new Point (rect.Right, rect.Bottom);
				else
					untransformed = new Point (rect.Left,  rect.Bottom);					

				transformed = Transform(untransformed);
				
				if (index == 0)
				{
					result.X      = transformed.X;
					result.Y      = transformed.Y;
					result.Right  = transformed.X;
					result.Bottom = transformed.Y;
				}
				else
				{
					if (result.Left   > transformed.X)
						result.X      = transformed.X;
					if (result.Top    > transformed.Y)
						result.Y      = transformed.Y;
					if (result.Right  < transformed.X)
						result.Right  = transformed.X;
					if (result.Bottom < transformed.Y)
						result.Bottom = transformed.Y;
				}
			}
			return result;
		}

		/// <summary>Transform the specified point and return the result.</summary>
		/// <param name="point">The point to transform.<see cref="System.Windows.Point"/></param>
		/// <returns>The result of transforming point.<see cref="System.Windows.Point"/</returns>
        public System.Windows.Point Transform (System.Windows.Point point)
		{
			// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Point result = new System.Windows.Point (0, 0);
			
			result.X = _m11 * point.X + _m12 * point.Y + _offsetX;
			result.Y = _m21 * point.X + _m22 * point.Y + _offsetY;
			
			return result;
		}
        
		/// <summary>Transform the specified points by this System.Windows.Media.Matrix.</summary>
		/// <param name="points">The points to transform. The original points in the array are replaced by
        /// their transformed values.<see cref="System.Windows.Point[]"/></param>
        public void Transform (System.Windows.Point[] points)
		{
			// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Point result = new System.Windows.Point (0, 0);
			for (int index = 0; index < points.Length; index++)
			{
				result.X = _m11 * points[index].X + _m12 * points[index].Y + _offsetX;
				result.Y = _m21 * points[index].X + _m22 * points[index].Y + _offsetY;
				
				points[index].X = result.X;
				points[index].Y = result.Y;
			}
		}

		/// <summary>Transform the specified size and return the result.</summary>
		/// <param name="size">The size to transform.<see cref="System.Windows.Size"/></param>
		/// <returns>The result of transforming size.<see cref="System.Windows.Size"/</returns>
        public System.Windows.Size Transform (System.Windows.Size size)
		{
			// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Size result = new System.Windows.Size (0, 0);
			
			result.Width  = _m11 * size.Width + _m12 * size.Height + _offsetX;
			result.Height = _m21 * size.Width + _m22 * size.Height + _offsetY;
			
			return result;
		}
        
		/// <summary>Transform the specified sizes by this System.Windows.Media.Matrix.</summary>
		/// <param name="sizes">The sizes to transform. The original sizes in the array are replaced by
        /// their transformed values.<see cref="System.Windows.Size[]"/></param>
        public void Transform (System.Windows.Size[] sizes)
		{
			// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Size result = new System.Windows.Size (0, 0);
			for (int index = 0; index < sizes.Length; index++)
			{
				result.Width  = _m11 * sizes[index].Width + _m12 * sizes[index].Height + _offsetX;
				result.Height = _m21 * sizes[index].Width + _m22 * sizes[index].Height + _offsetY;
				
				sizes[index].Width  = result.Width;
				sizes[index].Height = result.Height;
			}
		}
        
		/// <summary>Transforms the specified vector by this System.Windows.Media.Matrix.</summary>
		/// <param name="vector">The vector to transform.<see cref="Vector"/></param>
		/// <returns>The result of transforming vector .<see cref="System.Windows.Vector"/></returns>
        public System.Windows.Vector Transform (Vector vector)
		{
			// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Vector result = new System.Windows.Vector (0, 0);
			
			result.X = _m11 * vector.X + _m12 * vector.Y + _offsetX;
			result.Y = _m21 * vector.X + _m22 * vector.Y + _offsetY;
			
			return result;
		}
        
		/// <summary>Transforms the specified vectors by this System.Windows.Media.Matrix.</summary>
		/// <param name="vectors">The vectors to transform. The original vectors in the array are replaced by
        /// their transformed values.<see cref="Vector[]"/></param>
        public void Transform (Vector[] vectors)
		{
			// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Vector result = new System.Windows.Vector (0, 0);
			for (int index = 0; index < vectors.Length; index++)
			{
				result.X = _m11 * vectors[index].X + _m12 * vectors[index].Y + _offsetX;
				result.Y = _m21 * vectors[index].X + _m22 * vectors[index].Y + _offsetY;
				
				vectors[index].X = result.X;
				vectors[index].Y = result.Y;
			}
		}

		/// <summary>Determine whether the two specified System.Windows.Media.Matrix structures are identical.</summary>
		/// <param name="matrix1">The first System.Windows.Media.Matrix structure to compare.<see cref="System.Windows.Media.Matrix"/></param>
		/// <param name="matrix2">The second System.Windows.Media.Matrix structure to compare.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>True if matrix1 and matrix2 are identical; otherwise, false.<see cref="System.Boolean"/></returns>
        public static bool Equals (System.Windows.Media.Matrix matrix1, System.Windows.Media.Matrix matrix2)
		{
			if (matrix1._m11 == matrix2._m11 && matrix1._m12 == matrix2._m12 &&
			    matrix1._m21 == matrix2._m21 && matrix1._m22 == matrix2._m22 &&
			    matrix1._offsetX == matrix2._offsetX && matrix1._offsetY == matrix2._offsetY)
				return true;
			return false;
		}
        
		/// <summary>Multiply a System.Windows.Media.Matrix structure by another System.Windows.Media.Matrix structure.</summary>
		/// <param name="trans1">The first System.Windows.Media.Matrix structure to multiply.<see cref="System.Windows.Media.Matrix"/></param>
		/// <param name="trans2">The second System.Windows.Media.Matrix structure to multiply.<see cref="System.Windows.Media.Matrix"/></param>
		/// <returns>The result of multiplying trans1 by trans2.<see cref="System.Windows.MediaMatrix"/></returns>
        public static System.Windows.Media.Matrix Multiply (System.Windows.Media.Matrix trans1, System.Windows.Media.Matrix trans2)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Media.Matrix result = new System.Windows.Media.Matrix(1, 0, 0, 1, 0, 0);
			
			result._m11     = trans1._m11     * trans2._m11   +   trans1._m21     * trans2._m12;
			result._m21     = trans1._m11     * trans2._m21   +   trans1._m21     * trans2._m22;

			result._m12     = trans1._m12     * trans2._m11   +   trans1._m22     * trans2._m12;
			result._m22     = trans1._m12     * trans2._m21   +   trans1._m22     * trans2._m22;

			result._offsetX = trans1._offsetX * trans2._m11   +   trans1._offsetY * trans2._m12;
			result._offsetY = trans1._offsetX * trans2._m21   +   trans1._offsetY * trans2._m22;
			
			return result;
		}
        
        /*  
		/// <summary>Convert a System.String representation of a matrix into the equivalent System.Windows.Media.Matrix structure.</summary>
		/// <param name="source">The System.String representation of the matrix.<see cref="System.String"/></param>
		/// <returns>The equivalent System.Windows.Media.Matrix structure.<see cref="System.Windows.Media.Matrix"/></returns>
        public static System.Windows.Media.Matrix Parse(string source);
        */
		/// <summary>Prepend the specified System.Windows.Media.Matrix structure onto this System.Windows.Media.Matrix  structure.</summary>
		/// <param name="matrix">The System.Windows.Media.Matrix structure to prepend to this System.Windows.Media.Matrix
        //     structure.<see cref="System.Windows.Media.Matrix"/></param>
        public void Prepend (System.Windows.Media.Matrix matrix)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			System.Windows.Media.Matrix result = new System.Windows.Media.Matrix(1, 0, 0, 1, 0, 0);
			
			result._m11     = matrix._m11     * _m11   +   matrix._m21     * _m12;
			result._m21     = matrix._m11     * _m21   +   matrix._m21     * _m22;

			result._m12     = matrix._m12     * _m11   +   matrix._m22     * _m12;
			result._m22     = matrix._m12     * _m21   +   matrix._m22     * _m22;

			result._offsetX = matrix._offsetX * _m11   +   matrix._offsetY * _m12;
			result._offsetY = matrix._offsetX * _m21   +   matrix._offsetY * _m22;
			
			_m11 = result._m11;
			_m21 = result._m21;
			_m12 = result._m12;
			_m22 = result._m22;
			_offsetX = result._offsetX + _offsetX;
			_offsetY = result._offsetY + _offsetY;
		}
        
		/// <summary>Apply a rotation of the specified angle about the origin of this System.Windows.Media.Matrix structure.</summary>
		/// <param name="angle">The angle, in radiants counter-clockwise, by which to rotate this matrix.<see cref="System.Double"/></param>
        public void RotateRadiants (double angle)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			double sin  = Math.Sin (angle);
			double cos	= Math.Cos (angle);
			System.Windows.Media.Matrix matrix = new System.Windows.Media.Matrix(cos, sin, -sin, cos, 0, 0);
			
			Append (matrix);
		}
        
		/// <summary>Apply a rotation of the specified angle about the origin of this System.Windows.Media.Matrix structure.</summary>
		/// <param name="angle">The angle, in degrees clockwise, by which to rotate this matrix.<see cref="System.Double"/></param>
        public void Rotate (double angle)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			double ar   = -(Math.PI / 180) * angle;
			double sin  = Math.Sin (ar);
			double cos	= Math.Cos (ar);
			System.Windows.Media.Matrix matrix = new System.Windows.Media.Matrix(cos, sin, -sin, cos, 0, 0);
			
			Append (matrix);
		}

/*
		/// <summary>Rotates this matrix about the specified point.</summary>
		/// <param name="angle">The angle, in degrees, by which to rotate this matrix.<see cref="System.Double"/></param>
		/// <param name="centerX">The x-coordinate of the point about which to rotate this matrix.<see cref="System.Double"/></param>
		/// <param name="centerY">The y-coordinate of the point about which to rotate this matrix.<see cref="System.Double"/></param>
        public void RotateAt(double angle, double centerX, double centerY)
		{
		}
        
        //
        // Summary:
        //     Prepends a rotation of the specified angle at the specified point to this
        //     System.Windows.Media.Matrix structure.
        //
        // Parameters:
        //   angle:
        //     The rotation angle, in degrees.
        //
        //   centerX:
        //     The x-coordinate of the rotation center.
        //
        //   centerY:
        //     The y-coordinate of the rotation center.
        public void RotateAtPrepend(double angle, double centerX, double centerY);
*/
        
		/// <summary>Prepend a rotation of the specified angle to this System.Windows.Media.Matrix structure.</summary>
		/// <param name="angle">The angle, in radiants counter-clockwise, of rotation to prepend.<see cref="System.Double"/></param>
        public void RotateRadiantsPrepend(double angle)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			double sin  = Math.Sin (angle);
			double cos	= Math.Cos (angle);
			System.Windows.Media.Matrix matrix = new System.Windows.Media.Matrix(cos, sin, -sin, cos, 0, 0);

			Prepend (matrix);
		}
        
		/// <summary>Prepend a rotation of the specified angle to this System.Windows.Media.Matrix structure.</summary>
		/// <param name="angle">The angle, in degrees clockwise, of rotation to prepend.<see cref="System.Double"/></param>
        public void RotatePrepend(double angle)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			double ar   = -(Math.PI / 180) * angle;
			double sin  = Math.Sin (ar);
			double cos	= Math.Cos (ar);
			System.Windows.Media.Matrix matrix = new System.Windows.Media.Matrix(cos, sin, -sin, cos, 0, 0);

			Prepend (matrix);
		}
		
		/// <summary>Append the specified scale vector to this System.Windows.Media.Matrix structure.</summary>
		/// <param name="scaleX">The value by which to scale this System.Windows.Media.Matrix along the x-axis.<see cref="System.Double"/></param>
		/// <param name="scaleY">The value by which to scale this System.Windows.Media.Matrix along the y-axis.<see cref="System.Double"/></param>
        public void Scale(double scaleX, double scaleY)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			_m11 *= scaleX;	_m12 *= scaleY;
			_m21 *= scaleX; _m22 *= scaleY;
		}

/*
		/// <summary>Scale this System.Windows.Media.Matrix by the specified amount about the specified point.</summary>
		/// <param name="scaleX">The amount by which to scale this System.Windows.Media.Matrix along the x-axis.<see cref="System.Double"/></param>
		/// <param name="scaleY">The amount by which to scale this System.Windows.Media.Matrix along the y-axis.<see cref="System.Double"/></param>
		/// <param name="centerX">The x-coordinate of the scale operation's center point.<see cref="System.Double"/></param>
		/// <param name="centerY">The y-coordinate of the scale operation's center point.<see cref="System.Double"/></param>
        public void ScaleAt(double scaleX, double scaleY, double centerX, double centerY);

		/// <summary>Prepend the specified scale about the specified point of this System.Windows.Media.Matrix.</summary>
		/// <param name="scaleX">The x-axis scale factor. <see cref="System.Double"/></param>
		/// <param name="scaleY">The y-axis scale factor.<see cref="System.Double"/></param>
		/// <param name="centerX">The x-coordinate of the point about which the scale operation is performed.<see cref="System.Double"/></param>
		/// <param name="centerY">The y-coordinate of the point about which the scale operation is performed.<see cref="System.Double"/></param>
        public void ScaleAtPrepend(double scaleX, double scaleY, double centerX, double centerY);
*/

		/// <summary>Prepends the specified scale vector to this System.Windows.Media.Matrix structure.</summary>
		/// <param name="scaleX">The value by which to scale this System.Windows.Media.Matrix structure along
        /// the x-axis.<see cref="System.Double"/></param>
		/// <param name="scaleY">The value by which to scale this System.Windows.Media.Matrix structure along
        /// the y-axis.<see cref="System.Double"/></param>
        public void ScalePrepend(double scaleX, double scaleY)
		{	Prepend (new System.Windows.Media.Matrix (scaleX, scaleY, scaleX, scaleY, 0, 0));	}

        /// <summary>Change this System.Windows.Media.Matrix structure into an identity matrix.</summary>    
        public void SetIdentity()
		{
			_m11 = 1;
			_m21 = 0;
			_m12 = 0;
			_m22 = 1;
			_offsetX = 0;
			_offsetY = 0;
		}

/*
		/// <summary>Appends a skew of the specified degrees in the x and y dimensions to this
        /// System.Windows.Media.Matrix structure.</summary>
		/// <param name="skewX">The angle in the x dimension by which to skew this System.Windows.Media.Matrix.<see cref="System.Double"/></param>
		/// <param name="skewY">The angle in the y dimension by which to skew this System.Windows.Media.Matrix.<see cref="System.Double"/></param>
        public void Skew(double skewX, double skewY);

		/// <summary>Prepend a skew of the specified degrees in the x and y dimensions to this
        /// System.Windows.Media.Matrix structure.</summary>
		/// <param name="skewX">The angle in the x dimension by which to skew this System.Windows.Media.Matrix.<see cref="System.Double"/></param>
		/// <param name="skewY">The angle in the y dimension by which to skew this System.Windows.Media.Matrix.<see cref="System.Double"/></param>
        public void SkewPrepend(double skewX, double skewY);
*/
        
		/// <summary>Append a translation of the specified offsets to this System.Windows.Media.Matrix structure.</summary>
		/// <param name="offsetX">The amount to offset this System.Windows.Media.Matrix along the x-axis.<see cref="System.Double"/></param>
		/// <param name="offsetY">The amount to offset this System.Windows.Media.Matrix along the y-axis.<see cref="System.Double"/></param>
        public void Translate (double offsetX, double offsetY)
		{	// Matrix structure:
			// m11 m12 OffsetX
			// m21 m22 OffsetY
			
			_offsetX += offsetX;
			_offsetY += offsetY;
		}
        
        /// <summary>Prepend a translation of the specified offsets to this System.Windows.Media.Matrix structure.</summary>
		/// <param name="offsetX">The amount to offset this System.Windows.Media.Matrix along the x-axis.<see cref="System.Double"/></param>
		/// <param name="offsetY">The amount to offset this System.Windows.Media.Matrix along the y-axis.<see cref="System.Double"/></param>
        public void TranslatePrepend (double offsetX, double offsetY)
		{	Prepend (new System.Windows.Media.Matrix (1, 0, 0, 1, offsetX, offsetY));	}
		
		#endregion Methods
		
		#region IFormattable methods

		/// <summary>Convert the current value of a System.Windows.Media.PathFigureCollection
        /// to a System.String.</summary>
		/// <returns>A string representation of the System.Windows.Media.PathFigureCollection.<see cref="System.String"/></returns>
        public override string ToString ()
		{	return string.Format ("{0},{1},{2},{3},{4},{5}", _m11, _m12, _m21, _m22, _offsetX, _offsetY);	}

		/// <summary>Convert the current value of a System.Windows.Media.PathFigureCollection
        /// to a System.String using the specified culture-specific formatting information.</summary>
		/// <param name="provider">Culture-specific formatting information.<see cref="IFormatProvider"/></param>
		/// <returns>A string representation of the System.Windows.Media.PathFigureCollection.<see cref="System.String"/></returns>
        public string ToString (IFormatProvider provider)
		{	return string.Format (provider, "{0},{1},{2},{3},{4},{5}", _m11, _m12, _m21, _m22, _offsetX, _offsetY);	}

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
				return string.Format (provider, format, _m11, _m12, _m21, _m22, _offsetX, _offsetY);
		}
		
		#endregion IFormattable methods

	}
}