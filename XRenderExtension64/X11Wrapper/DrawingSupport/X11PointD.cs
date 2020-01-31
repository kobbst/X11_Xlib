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

namespace X11
{

	/// <summary>Define the minimum functionality of a simple 2D double precision point.</summary>
    public interface IPointD
	{

        /// <summary>Get or set the X-coordinate value of this structure.</summary>
        /// <returns>The X-coordinate value of this  structure. The default value is 0.</returns>
		double X	{	get;	set;	}
		
		/// <summary>Get or set the Y-coordinate value of this structure.</summary>
		/// <returns>The Y-coordinate value of this  structure. The default value is 0.</returns>
		double Y	{	get;	set;	}
	}
	
	/// <summary>A simple 2D double precision point.</summary>
    public struct X11PointD : X11.IPointD
    {

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "PointD";

		#endregion Constants

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes		
		
		/// <summary>The x-coordinate of the this structure.</summary>
		private double		_x;
		
		/// <summary>The y-coordinate of the this structure.</summary>
		private double		_y;
		
        #endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>The initializing constructor.</summary>
		/// <param name="x">The x coordinate.<see cref="System.Double"/></param>
		/// <param name="y">The y coordinate.<see cref="System.Double"/></param>
		public X11PointD (double x, double y)
		{	_x = x;
			_y = y;
		}
		
		/// <summary>The initializing constructor.</summary>
		/// <param name="x">The ptnio coordinates.<see cref="X11.IPointD"/></param>
		public X11PointD (X11.IPointD point)
		{	_x = point.X;
			_y = point.Y;
		}

        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

        /// <summary>Get or set the X-coordinate value of this structure.</summary>
        /// <returns>The X-coordinate value of this structure. The default value is 0.</returns>
		public double X
		{	get	{	return _x;	}
			set	{	_x = value;	}
		}
		
		/// <summary>Get or set the Y-coordinate value of this structure.</summary>
		/// <returns>The Y-coordinate value of this structure. The default value is 0.</returns>
		public double Y 
		{	get	{	return _y;	}
			set	{	_y = value;	}
		}

        #endregion Properties
    }
	
}