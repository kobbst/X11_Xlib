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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace X11
{
	public interface IPathSegmentsCollection : /*IList<IPathSegment>,*/ ICollection<IPathSegment>, IEnumerable<IPathSegment>, IEnumerable
	{
		IPathSegment this[int index]
		{	get;	set;	}
	}

	public class X11PathSegmentCollection : IPathSegmentsCollection
	{
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "X11Path";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The inner list of path segments.</summary>
		internal List<IPathSegment>	_elements = null;
			
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>The default constructor.</summary>
		public X11PathSegmentCollection ()
		{	_elements = new List<IPathSegment>();	}
		
		/// <summary>The initializing constructor.</summary>
		public X11PathSegmentCollection (IEnumerable<IPathSegment> segments)
		{	
			_elements = new List<IPathSegment>();
			foreach (IPathSegment segment in segments)
			{
				IPathSegment ps = (IPathSegment)segment;
				if (ps == null)
					throw new ArgumentException ("The 'segments' must implement IPathSegment or a derived interface!");
				_elements.Add (ps);
			}
		}

		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################
		
		#region Interface properties
		
		/// <summary>Get or set the element at the specified index of the current instance.</summary>
		/// <param name="index">The index of the element to get or set.<see cref="System.Int32"/></param>
        public IPathSegment this[int index]
        {
            get { return (IPathSegment)_elements[index]; }
            set
			{
				IPathSegment ps = value as IPathSegment;
				if (ps != null)
					_elements[index] = ps;
			}
        }
		
		/// <summary>Get whether the collection is read only.</summary>
        public bool IsReadOnly
		{ get { return false; } }
        
		/// <summary>Get the number of elements contained in the current instance.</summary>
		public int Count
		{ get { return _elements.Count; } }

 		#endregion Interface properties
		
		#region Properties
		
		#endregion Properties

        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Interface methods
		
		/// <summary>Add an element to the end of the list.</summary>
		/// <param name="segment">The item to add.<see cref="IPathSegment"/></param>
        public void Add (IPathSegment segment)
        {
			IPathSegment ps = segment as IPathSegment;
			if (ps != null)
			{
				_elements.Add (ps);
				return;
			}
		}
		
		/// <summary>Remove the first occurrence of the specified element from the list.</summary>
		/// <param name="segment">The element to remove from the list.<see cref="IPathSegment"/></param>
		/// <returns>True on success, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Remove(IPathSegment segment)
		{
			IPathSegment ps = segment as IPathSegment;
			if (ps != null)
			{
				return _elements.Remove(ps);
			}
			else
				return false;
		}
		
		/// <summary>Remove all elements from the list.</summary>
        public void Clear()
		{	_elements.Clear();	}
		

        public void CopyTo(IPathSegment[] array, int arrayIndex)
		{	
			for (int i = 0; i < Count; i++)
			{
				if (array.Length - i - arrayIndex > 0)
					array[i + arrayIndex] = (IPathSegment)_elements[i];
			}
		}

		/// <summary>Determine whether the list contains a specific value.</summary>
		/// <param name="segment">The element to test containment for.<see cref="IPathSegment"/></param>
		/// <returns>True if element is contained in the list, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Contains(IPathSegment segment)
		{
			IPathSegment ps = segment as IPathSegment;
			if (ps != null)
			{
				return _elements.Remove(ps);
			}
			else
				return false;
		}

		#endregion Interface methods

		#region Methods
		
		/// <summary>Return an enumerator, in index order, that can be used to iterate over the list.</summary>
		/// <returns>The enumerator, in index order, that can be used to iterate over the list.<see cref="System.Collections.IEnumerator"/></returns>
		/// <remarks>Pay attention to cast the System.Object return value to IPathSegment within foreach () loop.</remarks>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
		    // call the generic version of the method
		    return this.GetEnumerator();
		}
		
		/// <summary>Returns an enumerator, in index order, that can be used to iterate over the list.</summary>
		/// <returns>The enumerator, in index order, that can be used to iterate over the
		/// list.<see cref="System.Collections.Generic.IEnumerator<IPathSegment>"/></returns>
        public IEnumerator<IPathSegment> GetEnumerator()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
				yield return ((IPathSegment)_elements[i]);
			}
        }
		
		/// <summary>Append a move to segment to the current figure.</summary>
		/// <param name="x">The point to move to x coordinate.<see cref="System.Double"/></param>
		/// <param name="y">The point to move to y coordinate.<see cref="System.Double"/></param>
		public void AddMove (double x, double y)
		{
			X11MoveToPathSegment mv = new X11MoveToPathSegment (x, y);
			_elements.Add (mv);
		}
		
		/// <summary>Append a move to segment to the current figure.</summary>
		/// <param name="x">The point to move to coordinates.<see cref="System.PointD"/></param>
		public void AddMove (System.Windows.Point point)
		{
			X11MoveToPathSegment mv = new X11MoveToPathSegment (point);
			_elements.Add (mv);
		}
		
		/// <summary>Append a line segment to the current figure.</summary>
		/// <param name="x1">The start point x coordinate.<see cref="System.Double"/></param>
		/// <param name="y1">The start point y coordinate.<see cref="System.Double"/></param>
		/// <param name="x2">The end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="y2">The end point y coordinate.<see cref="System.Double"/></param>
		public void AddLine (double x1, double y1, double x2, double y2)
		{
			X11LinePathSegment ln = new X11LinePathSegment (x1, y1, x2, y2);
			_elements.Add (ln);
		}
		
		/// <summary>Append a line segment to the current figure.</summary>
		/// <param name="point1">The start point coordinates.<see cref="System.PointD"/></param>
		/// <param name="point2">The end point coordinates.<see cref="System.PointD"/></param>
		public void AddLine (System.Windows.Point point1, System.Windows.Point point2)
		{
			X11LinePathSegment ln = new X11LinePathSegment (point1, point2);
			_elements.Add (ln);
		}
		
		/// <summary>Append a line segment to the current figure.</summary>
		/// <param name="x">The end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="y">The end point y coordinate.<see cref="System.Double"/></param>
		public void AddLine (double x, double y)
		{
			if (_elements.Count == 0)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::AddLine() Adding a line to without any previous element is not supported!");
				return;
			}
			
			X11LineToPathSegment lt = new X11LineToPathSegment (x, y);
			_elements.Add(lt);
		}
		
		/// <summary>Append a line segment to the current figure.</summary>
		/// <param name="point">The end point coordinates.<see cref="System.PointD"/></param>
		public void AddLine (System.Windows.Point point)
		{
			if (_elements.Count == 0)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::AddLine() Adding a line to without any previous element is not supported!");
				return;
			}
			
			X11LineToPathSegment lt = new X11LineToPathSegment (point);
			_elements.Add(lt);
		}
		
		/// <summary>Append an arc segment to the current figure.</summary>
		/// <param name="boxX">The arc's bounding box x coordinate.<see cref="System.Double"/></param>
		/// <param name="boxY">The arc's bounding box y coordinate.<see cref="System.Double"/></param>
		/// <param name="boxW">The arc's bounding box width.<see cref="System.Double"/></param>
		/// <param name="boxH">The arc's bounding box height.<see cref="System.Double"/></param>
		/// <param name="startAngle">The arc's start angle, relative to the 3 o clock position clockwise in units of degrees.<see cref="System.Double"/></param>
		/// <param name="sweepAngle">The angle between startAngle and the end of the arc, clockwise in units of degrees.<see cref="System.Double"/></param>
		public void AddArc (double boxX, double boxY, double boxW, double boxH, double startAngle, double sweepAngle)
		{
			X11ArcPathSegment ar = new X11ArcPathSegment (boxX, boxY, boxW, boxH, startAngle, sweepAngle);
			_elements.Add(ar);
		}
		
		/// <summary>Append an quadric bezier segment to the current figure.</summary>
		/// <param name="xControl">The quadric bezier's control point x coordinate.<see cref="System.Double"/></param>
		/// <param name="yControl">The quadric bezier's control point Y coordinate.<see cref="System.Double"/></param>
		/// <param name="xEnd">The quadric bezier's end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="yEnd">The quadric bezier's end point y coordinate.<see cref="System.Double"/></param>
		public void AddQuadraticBezier (double xControl, double yControl, double xEnd, double yEnd)
		{
			X11QuadraticBezierPathSegment qb = new X11QuadraticBezierPathSegment (xControl, yControl, xEnd, yEnd);
			_elements.Add(qb);
		}
		
		/// <summary>Append an quadric bezier segment to the current figure.</summary>
		/// <param name="control">The quadric bezier's control point coordinates.<see cref="System.PointD"/></param>
		/// <param name="end">The quadric bezier's end point coordinate.<see cref="System.PointD"/></param>
		public void AddQuadraticBezier (System.Windows.Point control, System.Windows.Point end)
		{
			X11QuadraticBezierPathSegment qb = new X11QuadraticBezierPathSegment (control, end);
			_elements.Add(qb);
		}
		
		/// <summary>Append an cubic bezier segment to the current figure.</summary>
		/// <param name="xControl1">The cubic bezier's control point 1 x coordinate.<see cref="System.Double"/></param>
		/// <param name="yControl1">The cubic bezier's control point 1 Y coordinate.<see cref="System.Double"/></param>
		/// <param name="xControl2">The cubic bezier's control point 2 x coordinate.<see cref="System.Double"/></param>
		/// <param name="yControl2">The cubic bezier's control point 2 Y coordinate.<see cref="System.Double"/></param>
		/// <param name="xEnd">The cubic bezier's end point x coordinate.<see cref="System.Double"/></param>
		/// <param name="yEnd">The cubic bezier's end point y coordinate.<see cref="System.Double"/></param>
		public void AddCubicBezier (double xControl1, double yControl1,
		                            double xControl2, double yControl2, double xEnd, double yEnd)
		{
			X11CubicBezierPathSegment cb = new X11CubicBezierPathSegment (xControl1, yControl1,
			                                                              xControl2, yControl2, xEnd, yEnd);
			_elements.Add(cb);
		}
		
		/// <summary>Append an cubic bezier segment to the current figure.</summary>
		/// <param name="control">The cubic bezier's control point 1 coordinates.<see cref="System.PointD"/></param>
		/// <param name="control">The cubic bezier's control point 2 coordinates.<see cref="System.PointD"/></param>
		/// <param name="end">The cubic bezier's end point coordinate.<see cref="System.PointD"/></param>
		public void AddCubicBezier (System.Windows.Point control1, System.Windows.Point control2, System.Windows.Point end)
		{
			X11CubicBezierPathSegment cb = new X11CubicBezierPathSegment (control1, control2, end);
			_elements.Add(cb);
		}
		
        #endregion Methods
		
	}

}