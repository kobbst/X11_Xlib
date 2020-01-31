// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: March 2015
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
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
using System.Collections.Generic;
using System.Diagnostics;

namespace X11
{

	/// <summary>Define the supported element types.</summary>
	public enum X11PathSegmentType
	{
		/// <summary>The abstract element type.</summary>
		Unknown = 0,
		/// <summary>The move to element type.</summary>
		MoveTo    = 1,
		/// <summary>The line element type.</summary>
		Line    = 2,
		/// <summary>The line to element type.</summary>
		LineTo  = 3,
		/// <summary>The polyline element type.</summary>
		PolyLine = 4,
		/// <summary>The arc element type.</summary>
		Arc  = 5,
		/// <summary>The quadric bezier element type.</summary>
		QuadraticBezier = 6,
		/// <summary>The cubic bezier element type.</summary>
		CubicBezier = 7
	}

	public interface IPathSegment
	{
		/// <summary>Get the type of the segment.</summary>
		X11PathSegmentType Type	{	get;	}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		string MarkupToken ();
		
	}

	/// <summary>The abstract path element.</summary>
	public abstract class X11PathSegment : IPathSegment
	{
		public X11PathSegmentType _type = X11PathSegmentType.Unknown;

		/// <summary>Get the type of the segment.</summary>
		public X11PathSegmentType Type	{	get		{	return _type;	}	}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public virtual string MarkupToken ()
		{
			// Must be implemented by every derived class.
			throw new NotImplementedException ();
		}
	}
	
	public interface IMoveToPathSegment : IPathSegment
	{
		/// <summary>Get the end point coordinates.</summary>
		System.Windows.Point Point	{	get;	set;	}
	}
	
	/// <summary>The line path element.</summary>
	public class X11MoveToPathSegment : X11PathSegment, IMoveToPathSegment
	{
		/// <summary>The end point coordinates.</summary>
		protected System.Windows.Point _point;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x">The point to move to x coordinate.<see cref="System.Double"/></param>
		/// <param name="y">The point to move to y coordinate.<see cref="System.Double"/></param>
		public X11MoveToPathSegment (double x, double y)
		{
			_type  = X11PathSegmentType.MoveTo;
			_point = new System.Windows.Point (x, y);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x">The point to move to coordinates.<see cref="System.PointD"/></param>
		public X11MoveToPathSegment (System.Windows.Point point)
		{
			_type  = X11PathSegmentType.MoveTo;
			_point = point;
		}
		
		/// <summary>Get the point to move to coordinates.</summary>
		public System.Windows.Point Point
		{	get	{	return _point;	}
			set	{	_point = value;	}
		}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			return "M " + _point.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}
	}
	
	public interface ILinePathSegment : IPathSegment
	{
		/// <summary>Get the start point coordinates.</summary>
		System.Windows.Point Start	{	get;	set;	}
		
		/// <summary>Get the end point coordinates.</summary>
		System.Windows.Point End	{	get;	set;	}
	}
	
	/// <summary>The line path element.</summary>
	public class X11LinePathSegment : X11PathSegment, ILinePathSegment
	{
		/// <summary>The start point  coordinates.</summary>
		protected System.Windows.Point _start;
		/// <summary>The end point coordinates.</summary>
		protected System.Windows.Point _end;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x1">The start point x coordinate.<see cref="System.Double"/></param>
		/// <param name="y1">The start point y coordinate.<see cref="System.Double"/></param>
		/// <param name="x2">The end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="y2">The end point y coordinate.<see cref="System.Double"/></param>
		public X11LinePathSegment (double x1, double y1, double x2, double y2)
		{
			_type  = X11PathSegmentType.Line;
			_start = new System.Windows.Point (x1, y1);
			_end   = new System.Windows.Point (x2, y2);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="start">The start point coordinates.<see cref="System.Windows.Point"/></param>
		/// <param name="end">The end point coordinates.<see cref="System.Windows.Point"/></param>
		public X11LinePathSegment (System.Windows.Point start, System.Windows.Point end)
		{
			_type  = X11PathSegmentType.Line;
			_start = start;
			_end   = end;
		}
		
		/// <summary>Get the start point coordinates.</summary>
		public System.Windows.Point Start
		{	get	{	return _start;	}
			set	{	_start = value;	}
		}
		
		/// <summary>Get the end point coordinates.</summary>
		public System.Windows.Point End
		{	get	{	return _end;		}
			set	{	_end = value;	}
		}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			return "L2 " + _start.ToString (System.Globalization.CultureInfo.InvariantCulture) +
				" " + _end.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}
	}
	
	public interface ILineToPathSegment : IPathSegment
	{		
		/// <summary>Get or set the end point coordinates.</summary>
		System.Windows.Point End	{	get;	set;	}
	}
	
	/// <summary>The line path element.</summary>
	public class X11LineToPathSegment : X11PathSegment, ILineToPathSegment
	{
		/// <summary>The end point coordinates.</summary>
		protected System.Windows.Point _end;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x">The end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="y">The end point y coordinate.<see cref="System.Double"/></param>
		public X11LineToPathSegment (double x, double y)
		{
			_type = X11PathSegmentType.LineTo;
			_end   = new System.Windows.Point (x, y);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="end">The end point coordinates.<see cref="System.Windows.Point"/></param>
		public X11LineToPathSegment (System.Windows.Point end)
		{
			_type = X11PathSegmentType.LineTo;
			End   = end;
		}
		
		/// <summary>Get or set the end point coordinates.</summary>
		public System.Windows.Point End
		{	get	{	return _end;	}
			set	{	_end = value;	}
		}		
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			return "L " + _end.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}
	}

	public interface IPolyLinePathSegment
	{
		/// <summary>Get or set the points.</summary>
		List< System.Windows.Point> PointList	{	get;	set;	}
		
		/// <summary>Get or set the closed flag.</summary>
		bool Closed	{	get;	set;	}
	}
	
	public class X11PolyLinePathSegment : X11PathSegment, IPolyLinePathSegment
	{
		/// <summary>The points.</summary>
		protected List< System.Windows.Point> _points = new List<System.Windows.Point>();
		
		/// <summary>The closed flag.</summary>
		protected bool _closed = false;
		
			/// <summary>Default constructor.</summary>
		public X11PolyLinePathSegment ()
		{
			_type = X11PathSegmentType.PolyLine;
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x">The points.<see cref="System.Windows.Point[]"/></param>
		public X11PolyLinePathSegment (System.Windows.Point[] points)
		{
			_type = X11PathSegmentType.PolyLine;
			_points.AddRange (points);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x">The points.<see cref="IEnumerable<System.Windows.Point>"/></param>
		public X11PolyLinePathSegment (IEnumerable<System.Windows.Point> points)
		{
			_type = X11PathSegmentType.PolyLine;
			_points.AddRange (points);
		}
		
		/// <summary>Get or set the points.</summary>
		public List< System.Windows.Point> PointList
		{	get	{	return _points;	}
			set	{	_points = value;	}
		}
		
		/// <summary>Get or set the closed flag.</summary>
		public bool Closed
		{	get	{	return _closed;	}
			set	{	_closed = value;	}
		}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			string result = "P";
			foreach (System.Windows.Point point in _points)
				result += point.ToString (System.Globalization.CultureInfo.InvariantCulture);
			return result;
		}
	}
	
	public interface IArcPathSegment : IPathSegment
	{

        #region Box/center based arc definition properties		
		
		/// <summary>The arc's center point.</summary>
		System.Windows.Point Center();
		
		/// <summary>The arc's bounding box.</summary>
		System.Windows.Rect Box_Rect	{	get;	set;	}
		
		/// <summary>The arc's start angle, relative to the 3 o clock position clockwise in units of degrees.</summary>
		double StartAngle	{	get;	set;	}
		
		/// <summary>The angle between startAngle and the end of the arc, clockwise in units of degrees.</summary>
		double RotationAngle	{	get;	set;	}

        #endregion Box/center based arc definition properties
		
		//////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Start/end based arc definition properties

		/// <summary>Get or set the endpoint of the elliptical arc.</summary>
		/// <returns>The point to which the arc is drawn. The default value is (0,0).</returns>
        System.Windows.Point Point	{	get;	set;	}
		
		/// <summary>Get or set the x- and y-radius of the arc as a System.Windows.Size structure.</summary>
		/// <returns> A System.Windows.Size structure that describes the x- and y-radius of the
        /// elliptical arc. The System.Windows.Size structure's System.Windows.Size.Width
        /// property specifies the arc's x-radius; its System.Windows.Size.Height property
        /// specifies the arc's y-radius. The default value is 0,0.</returns>
        System.Windows.Size Size	{	get;	set;	}

		/// <summary>Get or set a value that indicates whether the arc should be greater than 180 degrees.</summary>
		/// <returns>True if the arc should be greater than 180 degrees; otherwise, false. The
        /// default value is false.</returns>
        bool IsLargeArc	{	get;	set;	}

		/// <summary>Get or set a value that specifies whether the arc is drawn in the System.Windows.Media.SweepDirection.Clockwise
        /// or System.Windows.Media.SweepDirection.Counterclockwise direction.</summary>
        /// <returns>A value that specifies the direction in which the arc is drawn. The default
        /// value is System.Windows.Media.SweepDirection.Counterclockwise.</returns>
        System.Windows.Media.SweepDirection SweepDirection	{	get;	set;	}

        #endregion Start/end based arc definition properties
	}
	
	/// <summary>The arc path element.</summary>
	public class X11ArcPathSegment : X11PathSegment, IArcPathSegment
	{
		/// <summary>The arc's bounding box.</summary>
		protected System.Windows.Rect _box;

		/// <summary>The arc's start angle, relative to the 3 o clock position clockwise in units of degrees.</summary>
		protected double _startAngle;
		
		/// <summary>The angle between startAngle and the end of the arc, clockwise in units of degrees.</summary>
		protected double _sweepAngle;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="boxX">The arc's bounding box x coordinate.<see cref="System.Double"/></param>
		/// <param name="boxY">The arc's bounding box y coordinate.<see cref="System.Double"/></param>
		/// <param name="boxW">The arc's bounding box width.<see cref="System.Double"/></param>
		/// <param name="boxH">The arc's bounding box height.<see cref="System.Double"/></param>
		/// <param name="startAngle">The arc's start angle, relative to the 3 o clock position clockwise in units of degrees.<see cref="System.Double"/></param>
		/// <param name="sweepAngle">The angle between startAngle and the end of the arc, clockwise in units of degrees.<see cref="System.Double"/></param>
		public X11ArcPathSegment (double boxX, double boxY, double boxW, double boxH, double startAngle, double sweepAngle)
		{
			_type = X11PathSegmentType.Arc;
			_box  = new System.Windows.Rect (boxX, boxY, boxW, boxH);
			_startAngle = startAngle;
			_sweepAngle = sweepAngle;
		}

        #region Box/center based arc definition properties		
		
		/// <summary>The arc's center point.</summary>
		public System.Windows.Point  Center()
		{	return new System.Windows.Point (_box.X + _box.Width  / 2, _box.Y + _box.Height / 2);	}
		
		
		/// <summary>The arc's bounding box.</summary>
		public System.Windows.Rect Box_Rect
		{	get	{	return _box;				}
			set	{	_box = value;				}
		}
		
		/// <summary>The arc's start angle, relative to the 3 o clock position clockwise in units of degrees.</summary>
		public double StartAngle
		{	get	{	return _startAngle;			}
			set	{	_startAngle = value;		}
		}
		
		/// <summary>The angle between startAngle and the end of the arc, clockwise in units of degrees.</summary>
		public double RotationAngle
		{	get	{	return _sweepAngle;			}
			set	{	_sweepAngle = value;		}
		}

        #endregion Box/center based arc definition properties
		
		/// <summary>The arc's radius vector x coordinate.</summary>
		public double RadiusX
		{	get	{	return _box.Width  / 2;		}
			set	{	_box.Width = value * 2;		}
		}
		
		/// <summary>The arc's radius vector Y coordinate.</summary>
		public double RadiusY
		{	get	{	return _box.Height  / 2;	}
			set	{	_box.Height = value * 2;	}
		}
		
		/// <summary>The arc's bounding box x coordinate.</summary>
		public double Box_X
		{	get	{	return _box.X;				}
			set	{	_box.X = value;				}
		}
		
		/// <summary>The arc's bounding box Y coordinate.</summary>
		public double Box_Y
		{	get	{	return _box.Y;				}
			set	{	_box.Y = value;				}
		}
		
		/// <summary>The arc's bounding box width.</summary>
		public double Box_Width
		{	get	{	return _box.Width;			}
			set	{	_box.Width = value;			}
		}
		
		/// <summary>The arc's bounding box height.</summary>
		public double Box_Height
		{	get	{	return _box.Height;			}
			set	{	_box.Height = value;		}
		}
		
		/// <summary>The arc's bounding box right coordinate.</summary>
		public double Box_Right
		{	get	{ return _box.X + _box.Width;	}	}
		
		/// <summary>The arc's bounding box bottom coordinate.</summary>
		public double Box_Bottom
		{	get { return _box.Y + _box.Height;	}	}
		
        #region Start/end based arc definition properties
		
		//////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>Get or set the endpoint of the elliptical arc.</summary>
		/// <returns>The point to which the arc is drawn. The default value is (0,0).</returns>
        public System.Windows.Point Point
		{	get	{	throw new NotImplementedException ();	}
			set	{	throw new NotImplementedException ();	}
		}
		
		/// <summary>Get or set the x- and y-radius of the arc as a System.Windows.Size structure.</summary>
		/// <returns> A System.Windows.Size structure that describes the x- and y-radius of the
        /// elliptical arc. The System.Windows.Size structure's System.Windows.Size.Width
        /// property specifies the arc's x-radius; its System.Windows.Size.Height property
        /// specifies the arc's y-radius. The default value is 0,0.</returns>
		public System.Windows.Size Size
		{	get	{	return new System.Windows.Size (_box.Width / 2, _box.Height  / 2);	}
			set	{	_box.Width = value.Width * 2;		_box.Height = value.Height * 2;	}
		}

		/// <summary>Get or set a value that indicates whether the arc should be greater than 180 degrees.</summary>
		/// <returns>True if the arc should be greater than 180 degrees; otherwise, false. The
        /// default value is false.</returns>
        public bool IsLargeArc
		{	get	{	throw new NotImplementedException ();	}
			set	{	throw new NotImplementedException ();	}
		}

		/// <summary>Get or set a value that specifies whether the arc is drawn in the System.Windows.Media.SweepDirection.Clockwise
        /// or System.Windows.Media.SweepDirection.Counterclockwise direction.</summary>
        /// <returns>A value that specifies the direction in which the arc is drawn. The default
        /// value is System.Windows.Media.SweepDirection.Counterclockwise.</returns>
        public System.Windows.Media.SweepDirection SweepDirection
		{	get	{	throw new NotImplementedException ();	}
			set	{	throw new NotImplementedException ();	}
		}

        #endregion Start/end based arc definition properties
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			/* ToDo: Complete implementation. */
			return "A " + Center().ToString (System.Globalization.CultureInfo.InvariantCulture) +
				" " + Size.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}
	}
	
	public interface IQuadraticBezierPathSegment : IPathSegment
	{
		/// <summary>Get the control point coordinates.</summary>
		System.Windows.Point Control	{	get;	set;	}
		
		/// <summary>Get the end point coordinates.</summary>
		System.Windows.Point End	{	get;	set;	}
			
		/// <summary>Get a polygonal approximation's interpolation points.</summary>
		/// <param name="start">The segment start point.<see cref="System.Windows.Point"/></param>
		/// <param name="tolerance">The maximum bounds on the distance between points in the polygonal approximation
        /// of the geometry. Smaller values produce more accurate results but cause slower
    	/// execution. If tolerance is less than .000001, .000001 is used instead.<see cref="System.Double"/></param>
        System.Windows.Point[] FlattenedPathGeometryInterpolationPoints (System.Windows.Point start, double tolerance);
	}
	
	/// <summary>The quadric bezier path element.</summary>
	public class X11QuadraticBezierPathSegment : X11PathSegment, IQuadraticBezierPathSegment
	{
		/// <summary>The quadric bezier's control point coordinates.</summary>
		public System.Windows.Point _control;
		/// <summary>The quadric bezier's end point coordinates.</summary>
		public System.Windows.Point _end;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="xControl">The control point x coordinate.<see cref="System.Double"/></param>
		/// <param name="yControl">The control point y coordinate.<see cref="System.Double"/></param>
		/// <param name="xEnd">The end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="yEnd">The end point y coordinate.<see cref="System.Double"/></param>
		public X11QuadraticBezierPathSegment (double xControl, double yControl, double xEnd, double yEnd)
		{
			_type = X11PathSegmentType.QuadraticBezier;
			_control = new System.Windows.Point (xControl, yControl);
			_end = new System.Windows.Point (xEnd, yEnd);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="control">The control point coordinates.<see cref="System.Windows.Point"/></param>
		/// <param name="end">The end point coordinates.<see cref="System.Windows.Point"/></param>
		public X11QuadraticBezierPathSegment (System.Windows.Point control, System.Windows.Point end)
		{
			_type = X11PathSegmentType.QuadraticBezier;
			_control = control;
			_end = end;
		}
		
		/// <summary>Get or set the control point coordinates.</summary>
		public System.Windows.Point Control
		{	get	{	return _control;	}
			set	{	_control = value;	}
		}
		
		/// <summary>Get or set the end point coordinates.</summary>
		public System.Windows.Point End
		{	get	{	return _end;	}
			set	{	_end = value;	}
		}
			
		/// <summary>Get a polygonal approximation's interpolation points.</summary>
		/// <param name="start">The segment start point.<see cref="System.Windows.Point"/></param>
		/// <param name="tolerance">The maximum bounds on the distance between points in the polygonal approximation
        /// of the geometry. Smaller values produce more accurate results but cause slower
    	/// execution. If tolerance is less than .000001, .000001 is used instead.<see cref="System.Double"/></param>
        public System.Windows.Point[] FlattenedPathGeometryInterpolationPoints (System.Windows.Point start, double tolerance)
		{
			System.Windows.Point[] inputPoints = new System.Windows.Point[3];
			inputPoints[0] = new System.Windows.Point (start);
			inputPoints[1] = new System.Windows.Point (_control);
			inputPoints[2] = new System.Windows.Point (_end);

			int resultPointNumber = Mathematics.BezierCurve2D.CalculateNumberOfResultPoints (inputPoints);
			System.Windows.Point[] resultPoints = new System.Windows.Point [resultPointNumber];
			Mathematics.BezierCurve2D.CalculateInterpolationPoints (inputPoints, resultPointNumber, resultPoints);
			
			return resultPoints;
		}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			return "Q " + _control.ToString (System.Globalization.CultureInfo.InvariantCulture) +
				" " + _end.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}
	}
	
	public interface ICubicBezierPathSegment : IPathSegment
	{
		/// <summary>Get the control point 1 coordinates.</summary>
		System.Windows.Point Control1	{	get;	set;	}
		
		/// <summary>Get the control point 2 coordinates.</summary>
		System.Windows.Point Control2	{	get;	set;	}
		
		/// <summary>Get the end point coordinates.</summary>
		System.Windows.Point End	{	get;	set;	}
			
		/// <summary>Get a polygonal approximation's interpolation points.</summary>
		/// <param name="start">The segment start point.<see cref="System.Windows.Point"/></param>
		/// <param name="tolerance">The maximum bounds on the distance between points in the polygonal approximation
        /// of the geometry. Smaller values produce more accurate results but cause slower
    	/// execution. If tolerance is less than .000001, .000001 is used instead.<see cref="System.Double"/></param>
        System.Windows.Point[] FlattenedPathGeometryInterpolationPoints (System.Windows.Point start, double tolerance);
	}
	
	/// <summary>The cubic bezier path element.</summary>
	public class X11CubicBezierPathSegment : X11PathSegment, ICubicBezierPathSegment
	{
		/// <summary>The cubic bezier's control point 1 coordinates.</summary>
		public System.Windows.Point _control1;
		/// <summary>The cubic bezier's control point 2 coordinates.</summary>
		public System.Windows.Point _control2;
		/// <summary>The cubic bezier's end point coordinates.</summary>
		public System.Windows.Point _end;
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="xControl1">The control point 1 x coordinate.<see cref="System.Double"/></param>
		/// <param name="yControl1">The control point 1 y coordinate.<see cref="System.Double"/></param>
		/// <param name="xControl2">The control point 2 x coordinate.<see cref="System.Double"/></param>
		/// <param name="yControl2">The control point 2 y coordinate.<see cref="System.Double"/></param>
		/// <param name="xEnd">The end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="yEnd">The end point y coordinate.<see cref="System.Double"/></param>
		public X11CubicBezierPathSegment (double xControl1, double yControl1, double xControl2, double yControl2, double xEnd, double yEnd)
		{
			_type = X11PathSegmentType.CubicBezier;
			_control1 = new System.Windows.Point (xControl1, yControl1);
			_control2 = new System.Windows.Point (xControl2, yControl2);
			_end = new System.Windows.Point (xEnd, yEnd);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="control1">The control point 1 coordinates.<see cref="System.Windows.Point"/></param>
		/// <param name="control2">The control point 2 coordinates.<see cref="System.Windows.Point"/></param>
		/// <param name="end">The end point coordinates.<see cref="System.Windows.Point"/></param>
		public X11CubicBezierPathSegment (System.Windows.Point control1, System.Windows.Point control2, System.Windows.Point end)
		{
			_type = X11PathSegmentType.CubicBezier;
			_control1 = control1;
			_control2 = control2;
			_end = end;
		}
		
		/// <summary>Get or set the control point 1 coordinates.</summary>
		public System.Windows.Point Control1
		{	get	{	return _control1;	}
			set	{	_control1 = value;	}
		}
		
		/// <summary>Get or set the control point 2 coordinates.</summary>
		public System.Windows.Point Control2
		{	get	{	return _control2;	}
			set	{	_control2 = value;	}
		}
		
		/// <summary>Get or set the end point coordinates.</summary>
		public System.Windows.Point End
		{	get	{	return _end;	}
			set	{	_end = value;	}
		}
			
		/// <summary>Get a polygonal approximation's interpolation points.</summary>
		/// <param name="start">The segment start point.<see cref="System.Windows.Point"/></param>
		/// <param name="tolerance">The maximum bounds on the distance between points in the polygonal approximation
        /// of the geometry. Smaller values produce more accurate results but cause slower
    	/// execution. If tolerance is less than .000001, .000001 is used instead.<see cref="System.Double"/></param>
        public System.Windows.Point[] FlattenedPathGeometryInterpolationPoints (System.Windows.Point start, double tolerance)
		{
			System.Windows.Point[] inputPoints = new System.Windows.Point[4];
			inputPoints[0] = new System.Windows.Point (start);
			inputPoints[1] = new System.Windows.Point (_control1);
			inputPoints[2] = new System.Windows.Point (_control2);
			inputPoints[3] = new System.Windows.Point (_end);

			int resultPointNumber = Mathematics.BezierCurve2D.CalculateNumberOfResultPoints (inputPoints);
			System.Windows.Point[] resultPoints = new System.Windows.Point [resultPointNumber];
			Mathematics.BezierCurve2D.CalculateInterpolationPoints (inputPoints, resultPointNumber, resultPoints);
			
			return resultPoints;
		}
		
		/// <summary>Return the markup token representing this path segment.</summary>
		/// <returns>The markup token representing this path segment.<see cref="System.String"/></returns>
		public override string MarkupToken ()
		{
			return "C " + _control1.ToString (System.Globalization.CultureInfo.InvariantCulture) +
				" " + _control2.ToString (System.Globalization.CultureInfo.InvariantCulture) +
				" " + _end.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}