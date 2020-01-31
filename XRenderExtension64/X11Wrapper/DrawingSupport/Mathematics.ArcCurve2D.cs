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
    /// <summary>Calculate an arc curve.</summary>
	public class ArcCurve2D
	{
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "ArcCurve2D";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes

		#endregion Attributes

		public ArcCurve2D ()
		{
        }
		
		/// <summary>Calculate the preferred number of result points.</summary>
		/// <param name="centerX">The arc center point x coordinate.<see cref="System.Double"/></param>
		/// <param name="centerY">The arc center point y coordinate.<see cref="System.Double"/></param>
		/// <param name="radiusX">The arc radius at x axis.<see cref="System.Double"/></param>
		/// <param name="radiusY">The arc radius at y axis.<see cref="System.Double"/></param>
		/// <param name="startAngle">The arc start angle in radiants relative to the 12 o'clock position.<see cref="System.Double"/></param>
		/// <param name="endAngle">The arc end angle in radiants relative to the 12 o'clock position.<see cref="System.Double"/></param>
		/// <param name="reverseDirection">The arc has to be drawn in reverse direction (clockwise).<see cref="System.Boolean"/></param>
		/// <returns>The preferred number of result points.<see cref="System.Int32"/></returns>
		public static int CalculateNumberOfResultPoints (double centerX, double centerY, double radiusX, double radiusY,
		                                                 double startAngle, double endAngle, bool reverseDirection)
		{
			double radius			= Math.Sqrt (radiusX * radiusX + radiusY *radiusY);
			int    pointsPerQudrant = 4;
			
			if (radius > 25)
				pointsPerQudrant = 6;
			if (radius > 100)
				pointsPerQudrant = 8;
			
			double angle = Math.Abs (endAngle - startAngle);
			if (reverseDirection)
				angle = Math.PI * 2 - angle;

			if (angle > 3 * Math.PI / 2)
				pointsPerQudrant *= 4;
			else if (angle > Math.PI)
				pointsPerQudrant *= 3;
			else if (angle > Math.PI / 2)
				pointsPerQudrant *= 2;
			
			if (angle < Math.PI / 16)
				pointsPerQudrant = 1;
			else if (angle < Math.PI / 8)
				pointsPerQudrant = 2;
			else if (angle < Math.PI / 4)
				pointsPerQudrant = 3;
			
			return 1 + pointsPerQudrant;
		}
		
        /// <summary>Calculate the interpolation list for a arc curve.</summary>
		/// <param name="centerX">The arc center point x coordinate.<see cref="System.Double"/></param>
		/// <param name="centerY">The arc center point y coordinate.<see cref="System.Double"/></param>
		/// <param name="radiusX">The arc radius at x axis.<see cref="System.Double"/></param>
		/// <param name="radiusY">The arc radius at y axis.<see cref="System.Double"/></param>
		/// <param name="startAngle">The arc start angle in radiants relative to the 12 o'clock position.<see cref="System.Double"/></param>
		/// <param name="endAngle">The arc end angle in radiants relative to the 12 o'clock position.<see cref="System.Double"/></param>
		/// <param name="reverseDirection">The arc has to be drawn in reverse direction (counterclockwise).<see cref="System.Boolean"/></param>
		/// <param name="resultPointNumber">The number of requested result points.<see cref="System.Int32"/></param>
        /// <param name="resultPoints">The calculated result points.<see cref="System.Windows.Point[]"/></param>
        public static void CalculateInterpolationPoints(double centerX, double centerY, double radiusX, double radiusY, double startAngle, double endAngle,
		                                                bool reverseDirection, int resultPointNumber, System.Windows.Point[] resultPoints)
        {
			if (resultPointNumber <= 0)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CalculateInterpolationPoints () Requires a positive number of result points!");
				return;
			}
			
			double angle     = Math.Abs (endAngle - startAngle);
			if (reverseDirection)
				angle = Math.PI * 2 - angle;
			
			if (endAngle < startAngle)
				angle = -angle;
			
			double angleStep = angle / (resultPointNumber - 1);
			if (reverseDirection)
				angleStep = -angleStep;
			
			double currentAngle = startAngle;
			for (int resultPointIndex = 0; resultPointIndex < resultPointNumber; resultPointIndex++)
			{
				resultPoints[resultPointIndex].X = centerX + (radiusX * Math.Sin(currentAngle));
				// Y-axis has negative direction!
				resultPoints[resultPointIndex].Y = centerY - (radiusY * Math.Cos(currentAngle));
				
				if (resultPointIndex == resultPointNumber - 2)
					currentAngle = endAngle;
				else
					currentAngle += angleStep;
			}
		}
	}
}