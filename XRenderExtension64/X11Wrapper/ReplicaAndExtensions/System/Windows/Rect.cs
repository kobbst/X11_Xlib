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
	/// <summary>Describes the width, height, and location of a rectangle.</summary>
	public struct Rect : IFormattable
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "Rect";

		#endregion Constants

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes		

		/// <summary>The x-axis value of the left side of the rectangle.</summary>
		private double			_left;

		/// <summary>The width of the rectangle.</summary>
		private double			_width;

		/// <summary>The y-axis value of the top side of the rectangle.</summary>
		private double			_top;

		/// <summary>The height of the rectangle.</summary>
		private double			_height;
		
		/// <summary>The special value that represents a rectangle with no position or area.</summary>
		private static Rect		_empty = new Rect ();

        #endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary>Initialize a new instance of the System.Windows.Rect structure that is of
        /// the specified size and is located at (0,0).</summary>
		/// <param name="size">A System.Windows.Size structure that specifies the width and height of the rectangle.<see cref="System.Windows.Size"/></param>
        public Rect(System.Windows.Size size)
		{
			_left = 0.0;
			_width = size.Width;
			_top = 0.0;
			_height = size.Height;
			
			if (_width < 0)
			{
				_width -= _width;
				_left -= _width;
			}
			if (_height < 0)
			{
				_height -= _height;
				_top -= _height;
			}
		}

		/// <summary>Initialize a new instance of the System.Windows.Rect structure that is exactly
        /// large enough to contain the two specified points.</summary>
		/// <param name="point1">The first point that the new rectangle must contain.<see cref="System.Windows.Point"/></param>
		/// <param name="point2">The second point that the new rectangle must contain.<see cref="System.Windows.Point"/></param>
        public Rect(System.Windows.Point point1, System.Windows.Point point2)
		{
			_left = point1.X;
			_width = point2.X - point1.X;
			_top = point1.Y;
			_height = point2.Y - point1.Y;
			
			if (_width < 0)
			{
				_width -= _width;
				_left -= _width;
			}
			if (_height < 0)
			{
				_height -= _height;
				_top -= _height;
			}
		}

		/// <summary>Initialize a new instance of the System.Windows.Rect structure that has
        /// the specified top-left corner location and the specified width and height.</summary>
		/// <param name="location">A point that specifies the location of the top-left corner of the rectangle.<see cref="System.Windows.Point"/></param>
		/// <param name="size">A System.Windows.Size structure that specifies the width and height of the rectangle.<see cref="System.Windows.Size"/></param>
        public Rect(Point location, Size size)
		{
			_left = location.X;
			_width = size.Width;
			_top = location.Y;
			_height = size.Height;
			
			if (_width < 0)
			{
				_width -= _width;
				_left -= _width;
			}
			if (_height < 0)
			{
				_height -= _height;
				_top -= _height;
			}
		}

		/// <summary>Initializes a new instance of the System.Windows.Rect structure that is exactly large enough to contain
        /// the specified point and the sum of the specified point and the specified vector.</summary>
		/// <param name="point">The first point the rectangle must contain.<see cref="System.Windows.Point"/></param>
		/// <param name="vector">The amount to offset the specified point. The resulting rectangle will be
        /// exactly large enough to contain both points.<see cref="System.Windows.Vector"/></param>
        public Rect(System.Windows.Point point, System.Windows.Vector vector)
		{
			_left = point.X;
			_width = vector.X;
			_top = point.Y;
			_height = vector.Y;
			
			if (_width < 0)
			{
				_width -= _width;
				_left -= _width;
			}
			if (_height < 0)
			{
				_height -= _height;
				_top -= _height;
			}
		}

		/// <summary>Initializes a new instance of the System.Windows.Rect structure that has
        /// the specified x-coordinate, y-coordinate, width, and height.</summary>
		/// <param name="x">The x-coordinate of the top-left corner of the rectangle.<see cref="System.Double"/></param>
		/// <param name="y">The y-coordinate of the top-left corner of the rectangle.<see cref="System.Double"/></param>
		/// <param name="width">The width of the rectangle.<see cref="System.Double"/></param>
		/// <param name="height">The height of the rectangle.<see cref="System.Double"/></param>
		/// <exception cref="System.ArgumentException">The width is a negative value. -or- The height is a negative value.</exception>
        public Rect(double x, double y, double width, double height)
		{
			_left = x;
			_width = width;
			_top = y;
			_height = height;
			
			if (_width < 0)
			{
				_width -= _width;
				_left -= _width;
			}
			if (_height < 0)
			{
				_height -= _height;
				_top -= _height;
			}
		}

        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

		/// <summary>Get the y-axis value of the bottom of the rectangle.</summary>
		/// <returns>The y-axis value of the bottom of the rectangle. If the rectangle is empty,
        /// the value is System.Double.NegativeInfinity.<see cref="System.Double"/></returns>
		/// <exception cref="System.ArgumentException">System.Windows.Rect.Height is set to a negative value.</exception>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Height is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double Bottom
		{	get	{	return _top + _height;	}
			set	{	Height = value - _top;	}
		}

		/// <summary>Get the position of the bottom-left corner of the rectangle.</summary>
		/// <returns>The position of the bottom-left corner of the rectangle.<see cref="System.Windows.Point"/></returns>
        public System.Windows.Point BottomLeft
		{	get	{	return new System.Windows.Point (_left, _top + _height);	}	}

		/// <summary>Get the position of the bottom-right corner of the rectangle.</summary>
		/// <returns>The position of the bottom-right corner of the rectangle.<see cref="System.Windows.Point"/></returns>
        public System.Windows.Point BottomRight
		{	get	{	return new System.Windows.Point (_left + _width, _top + _height);	}	}

		/// <summary>Get a special value that represents a rectangle with no position or area.</summary>
		/// <returns>The empty rectangle, which has System.Windows.Rect.X and System.Windows.Rect.Y
        /// property values of System.Double.PositiveInfinity, and has System.Windows.Rect.Width
        /// and System.Windows.Rect.Height property values of System.Double.NegativeInfinity.<see cref="System.Windows.Rect"/></returns>
        public static System.Windows.Rect Empty
		{	get	{	return _empty;	}
		}

		/// <summary>Get or set the height of the rectangle.</summary>
		/// <returns>A positive number that represents the height of the rectangle. The default is 0.<see cref="System.Double"/></returns>
		/// <exception cref="System.ArgumentException">System.Windows.Rect.Height is set to a negative value.</exception>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Height is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double Height
		{	get	{	return _height;	}
			set
			{
				if (value < 0.0)
					throw new System.ArgumentException ("Height must be a positive value.");
				if (System.Object.ReferenceEquals (this, _empty))
					throw new System.InvalidOperationException ("Height can not be set on an System.Windows.Rect.Empty rectangle.");
				_height = value;
			}
		}

		/// <summary>Get a value that indicates whether the rectangle is the System.Windows.Rect.Empty rectangle.</summary>
		/// <returns>True if the rectangle is the System.Windows.Rect.Empty rectangle; otherwise, false.<see cref="System.Boolean"/></returns>
        public bool IsEmpty
		{	get	{	return (_left == _empty.Left && _top == _empty.Top && _width == _empty.Width && _height == _empty.Height ? true : false);	}
		}

		/// <summary>Get the x-axis value of the left side of the rectangle.<see cref="System.Double"/></summary>
		/// <returns>The x-axis value of the left side of the rectangle.</returns>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.X is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double Left
		{	get	{	return _left;	}
			set {	X = value;		}
		}

		/// <summary>Get or set the position of the top-left corner of the rectangle.</summary>
		/// <returns>The position of the top-left corner of the rectangle. The default is (0, 0).<see cref="System.Windows.Point"/></returns>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Location is set on an System.Windows.Rect.Empty rectangle.</exception>
        public System.Windows.Point Location
		{	get	{	return new System.Windows.Point (_left, _top);	}
			set	{	_left = value.X;	_top = value.Y;	}
		}		

		/// <summary>Get the x-axis value of the right side of the rectangle.</summary>
		/// <returns>The x-axis value of the right side of the rectangle.<see cref="System.Double"/></returns>
		/// <exception cref="System.ArgumentException">System.Windows.Rect.Width is set to a negative value.</exception>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Width is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double Right
		{	get	{	return _left + _width;	}
			set	{	Width = value - _left;	}
		}

		/// <summary>Get or set the width and height of the rectangle.</summary>
		/// <returns>A System.Windows.Size structure that specifies the width and height of the rectangle.<see cref="System.Windows.Size"/></returns>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Size is set on an System.Windows.Rect.Empty rectangle.</exception>
        public System.Windows.Size Size
		{	get	{	return new System.Windows.Size (_width, _height);	}
			set	{	_width = value.Width;	_height = value.Height;	}
		}

		/// <summary>Gets the y-axis position of the top of the rectangle.</summary>
		/// <returns>The y-axis position of the top of the rectangle.<see cref="System.Double"/></returns>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Y is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double Top
		{	get	{	return _top;	}
			set {	Y = value;		}
		}

		/// <summary>Gets the position of the top-left corner of the rectangle.</summary>
		/// <returns>The position of the top-left corner of the rectangle.<see cref="System.Windows.Point"/></returns>
        public Point TopLeft
		{	get	{	return new System.Windows.Point (_left, _top);	}	}

		/// <summary>Gets the position of the top-right corner of the rectangle.</summary>
		/// <returns>The position of the top-right corner of the rectangle.<see cref="System.Windows.Point"/></returns>
        public Point TopRight
		{	get	{	return new System.Windows.Point (_left + _width, _top);	}	}

		/// <summary>Get or set the width of the rectangle.</summary>
		/// <returns>A positive number that represents the width of the rectangle. The default is 0.<see cref="System.Double"/></returns>
		/// <exception cref="System.ArgumentException">System.Windows.Rect.Width is set to a negative value.</exception>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Width is set on an System.Windows.Rect.Empty rectangle.</exception>
		public double Width
		{	get	{	return _width;	}
			set
			{
				if (value < 0.0)
					throw new System.ArgumentException ("Width must be a positive value.");
				if (System.Object.ReferenceEquals (this, _empty))
					throw new System.InvalidOperationException ("Width can not be set on an System.Windows.Rect.Empty rectangle.");
				_width = value;
			}
		}

		/// <summary>Get or set the x-axis value of the left side of the rectangle.</summary>
		/// <returns>The x-axis value of the left side of the rectangle.<see cref="System.Double"/></returns>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.X is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double X
		{	get	{	return _left;	}
			set
			{
				if (System.Object.ReferenceEquals (this, _empty))
					throw new System.InvalidOperationException ("X can not be set on an System.Windows.Rect.Empty rectangle.");
				_left = value;
			}
		}

		/// <summary>Get or set the y-axis value of the top side of the rectangle.</summary>
		/// <returns>The y-axis value of the top side of the rectangle.<see cref="System.Double"/></returns>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect.Y is set on an System.Windows.Rect.Empty rectangle.</exception>
        public double Y
		{	get	{	return _top;	}
			set
			{
				if (System.Object.ReferenceEquals (this, _empty))
					throw new System.InvalidOperationException ("Y can not be set on an System.Windows.Rect.Empty rectangle.");
				_top = value;
			}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Operators

		/// <summary>Test whether two System.Media.Rect structures differ in location or size.</summary>
		/// <param name="left">The System.Media.Rect structure that is to the left of the inequality operator.<see cref="System.Media.Rect"/></param>
		/// <param name="right">The System.Media.Rect structure that is to the right of the inequality operator.<see cref="System.Media.Rect"/></param>
		/// <returns>This operator returns true if any of the System.Media.Rect.X , System.Media.Rect.Y,
        /// System.Media.Rect.Width, or System.Media.Rect.Height properties of the two System.Media.Rect
        /// structures are unequal, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(Rect left, Rect right)
		{
			if (left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height)
				return false;
			else
				return true;
		}

		/// <summary>Test whether two System.Media.Rect structures have equal location and size.</summary>
		/// <param name="left">The System.Media.Rect structure that is to the left of the equality operator.<see cref="System.Media.Rect"/></param>
		/// <param name="right">The System.Media.Rect structure that is to the right of the equality operator.<see cref="System.Media.Rect"/></param>
		/// <returns>This operator returns true if the two specified System.Media.Rect structures have equal
        /// System.Media.Rect.X, System.Media.Rect.Y, System.Media.Rect.Width, and System.Media.Rect.Height
        /// properties.<see cref="System.Boolean"/></returns>
        public static bool operator ==(Rect left, Rect right)
		{
			if (left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height)
				return true;
			else
				return false;
		}
		
		#endregion Operators
		
		#region Methods
		
		/// <summary>Determine if the rectangular region represented by rect is entirely contained within this System.Windows.Rect structure.</summary>
		/// <param name="rect">The System.Windows.Rect to test.<see cref="System.Windows.Rect"/></param>
		/// <returns>This method returns true if the rectangular region represented by rect is entirely contained within
        /// the rectangular region represented by this System.Windows.Rect, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Contains(Rect rect)
		{
			if (_left <= rect.X && _top <= rect.Y && Right >= rect.Right && Bottom >= rect.Bottom)
				return true;
			else
				return false;
		}

		/// <summary>Determine if the specified point is contained within this System.Windows.Rect structure.</summary>
		/// <param name="x">The x-coordinate of the point to test.<see cref="System.Double"/></param>
		/// <param name="y">The y-coordinate of the point to test. <see cref="System.Double"/></param>
		/// <returns>This method returns true if the point defined by x and y is contained within
        /// this System.Windows.Rect structure; otherwise false.<see cref="System.Boolean"/></returns>
        public bool Contains(double x, double y)
		{
			if (_left <= x && _top <= y && Right >= x && Bottom >= y)
				return true;
			else
				return false;
		}

		/// <summary>Determine if the specified point is contained within this System.Windows.Rect structure.</summary>
		/// <param name="x">The x-coordinate of the point to test.<see cref="System.Double"/></param>
		/// <param name="y">The y-coordinate of the point to test. <see cref="System.Double"/></param>
		/// <param name="fuzzy">The hit test fuzzy value. <see cref="System.Double"/></param>
		/// <returns>This method returns true if the point defined by x and y is contained within
        /// this System.Windows.Rect structure; otherwise false.<see cref="System.Boolean"/></returns>
        public bool Contains(double x, double y, double fuzzy)
		{
			if (_left - fuzzy <= x && _top - fuzzy <= y && Right + fuzzy >= x && Bottom + fuzzy >= y)
				return true;
			else
				return false;
		}
		
		/// <summary>Expand this rectangle instance to contain indicated point, if required.</summary>
		/// <param name="point">The point to be containd.<see cref="System.Windows.Point"/></param>
		/// <exception cref="System.InvalidOperationException">System.Windows.Rect is to expand on an System.Windows.Rect.Empty rectangle.</exception>
		public void ExpandToContain (System.Windows.Point point)
		{
			if (System.Object.ReferenceEquals (this, _empty))
				throw new System.InvalidOperationException ("Expansion can not be done on an System.Windows.Rect.Empty rectangle.");

			if (double.IsNaN(point.X) || double.IsNaN(point.Y))
				return;
			
			if (double.IsNaN(_left) || double.IsNaN(_top))
			{
				Width = 0;
				Left  = point.X;
				
				Height = 0;
				Top    = point.Y;

				return;
			}
			
			if (point.X < _left)
			{
				Width = _width + _left - point.X;
				Left  = point.X;
			}
			else if (point.X > _left + _width)
				Width = point.X - _left;
			
			if (point.Y < _top)
			{
				Height = _height + _top - point.Y;
				Top    = point.Y;
			}
			else if (point.Y > _top + _height)
				Height = point.Y - _top;
		}
		
		/// <summary>Get the hash code for this System.Windows.Rect structure. For information
        /// about the use of hash codes, see Object.GetHashCode.</summary>
		/// <returns>The hash code for this System.Windows.Rect.<see cref="System.Int32"/></returns>
        public override int GetHashCode()
		{	return Left.GetHashCode() ^ Top.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();	}

		/// <summary>Test whether obj is a System.Windows.Rect with the same location and size of this System.Windows.Rect.</summary>
		/// <param name="obj">The System.Object to test.<see cref="System.Object"/></param>
		/// <returns>This method returns true if obj is a System.Windows.Rect and its X, Y, Width, and Height properties
        /// are equal to the corresponding properties of this System.Windows.Rect, or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool Equals (object obj)
		{	return obj is System.Windows.Rect && this.Equals ((System.Windows.Rect)obj);		}

		/// <summary>Test whether obj is a System.Windows.Rect with the same location and size of this System.Windows.Rect.</summary>
		/// <param name="obj">The System.Windows.Rect to test.<see cref="System.Windows.Rect"/></param>
		/// <returns>This method returns true if obj is a System.Windows.Rect and its X, Y, Width, and Height properties
        /// are equal to the corresponding properties of this System.Windows.Rect, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals (Rect obj)
		{
			if (X == obj.X && Y == obj.Y && Width == obj.Width && Height == obj.Height)
				return true;
			else
				return false;
		}
		
        /// <summary>Creates a System.Windows.Rect structure with upper-left corner and
        /// lower-right corner at the specified locations.</summary>
        /// <param name="left">The x-coordinate of the upper-left corner of the rectangular region.<see cref="System.Double"/></param>
        /// <param name="top">The y-coordinate of the upper-left corner of the rectangular region.<see cref="System.Double"/></param>
        /// <param name="right">The x-coordinate of the lower-right corner of the rectangular region.<see cref="System.Double"/></param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of the rectangular region.<see cref="System.Double"/></param>
        /// <returns>The new System.Windows.Rect that this method creates.<see cref="System.Windows.Rect"/></returns>
        public static System.Windows.Rect FromLTRB(double left, double top, double right, double bottom)
		{	return new Rect (left, top, right - left, bottom - top);	}
		
        /// <summary>Creates a System.Windows.Rect structure as coordinate aligned bounding box around indicated point array.</summary>
		/// <param name="points">The array of topnts to be enloased in a bounding box rectangle.<see cref="System.Windows.Point[]"/></param>
		/// <returns>The rectangle as coordinate aligned bounding box around indicated point array.<see cref="System.Windows.Rect"/></returns>
		public static System.Windows.Rect FromBounds (System.Windows.Point[] points)
		{
			if (points == null || points.Length == 0)
				return new System.Windows.Rect (0.0, 0.0, 0.0, 0.0);
			
			System.Windows.Rect result = new System.Windows.Rect (points[0].X, points[0].Y, 0, 0);
			for (int pointIdx = 0; pointIdx < points.Length; pointIdx++)
				result.ExpandToContain (points[pointIdx]);
			
			return result;
		}
		
		/// <summary>Expand the rectangle by using the specified System.Windows.Size, in all directions.</summary>
		/// <param name="size">Specifies the amount to expand the rectangle. The System.Windows.Size structure's
        /// System.Windows.Size.Width property specifies the amount to increase the rectangle's
        /// System.Windows.Rect.Left and System.Windows.Rect.Right properties. The System.Windows.Size
        /// structure's System.Windows.Size.Height property specifies the amount to increase
        /// the rectangle's System.Windows.Rect.Top and System.Windows.Rect.Bottom properties.<see cref="Size"/></param>
        /// <exception cref="System.InvalidOperationException">This method is called on the System.Windows.Rect.Empty rectangle.</exception>
        public void Inflate(Size size)
		{
			if (System.Object.ReferenceEquals (this, _empty))
				throw new System.InvalidOperationException ("Height can not be set on an System.Windows.Rect.Empty rectangle.");
			_left -= size.Width;
			_width += 2 * size.Width;
			_top -= size.Height;
			_height += 2 * size.Height;
		}

		/// <summary>Expand or shrinks the rectangle by using the specified width and height amounts, in all directions.</summary>
		/// <param name="width">The amount by which to expand or shrink the left and right sides of the rectangle.<see cref="System.Double"/></param>
		/// <param name="height">The amount by which to expand or shrink the top and bottom sides of the rectangle.<see cref="System.Double"/></param>
        /// <exception cref="System.InvalidOperationException">This method is called on the System.Windows.Rect.Empty rectangle.</exception>
        public void Inflate(double width, double height)
		{
			if (System.Object.ReferenceEquals (this, _empty))
				throw new System.InvalidOperationException ("Height can not be set on an System.Windows.Rect.Empty rectangle.");
			_left -= width;
			_width += 2 * width;
			_top -= height;
			_height += 2 * height;
		}

		/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
		/// <param name="x">The amount to offset the location horizontally.<see cref="System.Double"/></param>
		/// <param name="y">The amount to offset the location vertically.<see cref="System.Double"/></param>
        public void Offset(double x, double y)
		{	_left +=x; _top += y;	}
		

		/// <summary>Returns a string representation of the rectangle.
        /// A <see cref="System.String"/>A string representation of the current rectangle. The string has the following
        /// form: "System.Windows.Rect.X,System.Windows.Rect.Y,System.Windows.Rect.Width,System.Windows.Rect.Height".</returns>
        public override string ToString ()
		{	return string.Format ("{0};{1};{2};{3}", _left, _top, _width, _height);		}

        /// <summary>Returns a string representation of the rectangle by using the specified format provider.</summary>
        /// <param name="provider">Culture-specific formatting information.<see cref="IFormatProvider"/></param>
        /// <returns>A string representation of the current rectangle that is determined by the
        /// specified format provider.<see cref="System.String"/></returns>
        public string ToString (IFormatProvider provider)
		{	return string.Format (provider, "{0};{1};{2};{3}", _left, _top, _width, _height);	}

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
				return string.Format (provider, format, _left, _top, _width, _height);
		}
		
		#endregion Methods

	}
}

