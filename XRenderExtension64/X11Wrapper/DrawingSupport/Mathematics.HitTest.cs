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
using System.Diagnostics;

using X11;

namespace Mathematics
{
	/// <summary>Implement generic hit test metods.</summary>
	public class HitTest
	{

		#region Static methods
		
		/// <summary>Determine whether indicated coordinates hit the line.</summary>
		/// <param name="start">The line's start point.<see cref="System.Windows.Point"/></param>
		/// <param name="end">The line's start point.<see cref="System.Windows.Point"/></param>
		/// <param name="x">The x coordinate to test.<see cref="System.Double"/></param>
		/// <param name="y">The y coordinate to test.<see cref="System.Double"/></param>
		/// <param name="epsilon">The hit test fuzzy value inclufing numerical inaccuracy.<see cref="System.Double"/></param>
		/// <returns>True if hit test is successful, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool PointOnLineSegmentOld (System.Windows.Point start, System.Windows.Point end, double testX, double testY, double epsilon)
		{
			// USE the equiation of a line.
			// ============================
			
			// Calculate gradient.
			double dX = end.X - start.X;
			double dY = end.Y - start.Y;
			double mN = double.PositiveInfinity;	// Normal  gradient.
			double mI = double.PositiveInfinity;	// Inverse gradient.
			
			if (dX != 0.0)
				mN = dY / dX;
			if (dY != 0.0)
				mI = dX / dY;
			
			if (Math.Abs (mN) < Math.Abs (mI))
			{
				if (testX < Math.Min (start.X, end.X) - epsilon || Math.Max (start.X, end.X) + epsilon < testX)
					return false;
				
				double yForX = mN * (testX - start.X) + start.Y;
				
				if (yForX - epsilon <= testY && testY <= yForX + epsilon)
					return true;
				else
					return false;
			}
			else // (mI < mN)
			{
				if (testY < Math.Min (start.Y, end.Y) - epsilon || Math.Max (start.Y, end.Y) + epsilon < testY)
					return false;
				
				double xForY = mI * (testY - start.Y) + start.X;
				
				if (xForY - epsilon <= testX && testX <= xForY + epsilon)
					return true;
				else
					return false;
			}
		}

		/// <summary>Determine whether indicated coordinates hit the line.</summary>
		/// <param name="start">The line's start point.<see cref="System.Windows.Point"/></param>
		/// <param name="end">The line's start point.<see cref="System.Windows.Point"/></param>
		/// <param name="x">The x coordinate to test.<see cref="System.Double"/></param>
		/// <param name="y">The y coordinate to test.<see cref="System.Double"/></param>
		/// <param name="epsilon">The hit test fuzzy value inclufing numerical inaccuracy.<see cref="System.Double"/></param>
		/// <returns>True if hit test is successful, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool PointOnLineSegment (System.Windows.Point start, System.Windows.Point end, double testX, double testY, double epsilon) // epsilon = 0.001
		{
			// USE a fast exclufing algorithm.
			// ===============================
			
			if (testX - Math.Max(start.X, end.X) > epsilon || 
			    Math.Min(start.X, end.X) - testX > epsilon || 
			    testY - Math.Max(start.Y, end.Y) > epsilon || 
			    Math.Min(start.Y, end.Y) - testY > epsilon)
				return false;
		
			if (Math.Abs(end.X - start.X) < epsilon)
				return Math.Abs(start.X - testX) < epsilon || Math.Abs(end.X - testX) < epsilon;
			if (Math.Abs(end.Y - start.Y) < epsilon)
				return Math.Abs(start.Y - testY) < epsilon || Math.Abs(end.Y - testY) < epsilon;
	
			double x = start.X + (testY - start.Y) * (end.X - start.X) / (end.Y - start.Y);
			double y = start.Y + (testX - start.X) * (end.Y - start.Y) / (end.X - start.X);
		
			return Math.Abs(testX - x) < epsilon || Math.Abs(testY - y) < epsilon;
		}
		
		/// <summary>Determine whether indicated coordinates are inside an (auto-closed) polygon.</summary>
		/// <param name="polygon">The (auto-closed) polygon to thest.<see cref="System.Windows.Point[]"/></param>
		/// <param name="testX">The x coordinate to test.<see cref="System.Double"/></param>
		/// <param name="testY">The y coordinate to test.<see cref="System.Double"/></param>
		/// <returns>True if inside test is successful, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool PointInPolygon (System.Windows.Point[] polygon, double testX, double testY)
		{
			// Original publishing: http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
			// C# converted solution: http://stackoverflow.com/questions/217578/point-in-polygon-aka-hit-test
			
			// Bounding box test.
/*			double minX = polygon[0].X;
			double maxX = polygon[0].X;
			double minY = polygon[0].Y;
			double maxY = polygon[0].Y;
			
			for (int i = 1; i < polygon.Length; i++)
			{
			    Point q = polygon[i];
			    minX = Math.Min (q.X, minX);
			    maxX = Math.Max (q.X, maxX);
			    minY = Math.Min (q.Y, minY);
			    maxY = Math.Max (q.Y, maxY);
			}
			
			if ( testX < minX || testX > maxX || testY < minY || testY > maxY )
			{
			    return false;
			}
*/
			// http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
			bool inside = false;
			
			for (int endPointIdx = 0, startPointIdx = polygon.Length - 1; endPointIdx < polygon.Length; startPointIdx = endPointIdx++)
			{
			    if ( ( polygon[endPointIdx].Y > testY ) != ( polygon[startPointIdx].Y > testY ) &&
			         testX < ( polygon[startPointIdx].X - polygon[endPointIdx].X ) * ( testY - polygon[endPointIdx].Y ) / ( polygon[startPointIdx].Y - polygon[endPointIdx].Y ) + polygon[endPointIdx].X )
			    {
			        inside = !inside;
			    }
			}
			
			return inside;
		}
		
		#endregion Static methods
	}
}
