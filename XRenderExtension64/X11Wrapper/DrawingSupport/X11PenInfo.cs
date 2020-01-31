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

namespace X11
{
	
	/// <summary>Store pen information.</summary>
	public class X11PenInfo
	{

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string			CLASS_NAME = "PenInfo";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The color name, if known.</summary>
		private X11.TColor			_color;

		/// <summary>The thickness of the stroke produced by this pen.</summary>
        private double				_thickness = 1.0;

		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initialize a new instance of the System.PenInfo structure that has specific color.</summary>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		private X11PenInfo (X11.TColor color)
		{
			_color = color;
		}

        #endregion Construction

        #region Initialization
		
		/// <summary>Initialize a new instance of the System.PenInfo structure that has specific color.</summary>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		public static X11PenInfo FromColorName (string colorName)
		{
			return new X11PenInfo (X11.TColor.FromName (colorName.Trim ()));
		}
		
		/// <summary>Initialize a new instance of the System.PenInfo structure that has specific color.</summary>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		public static X11PenInfo FromColor (X11.TColor color)
		{
			return new X11PenInfo (color);
		}

        #endregion Initialization
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties

		/// <summary>Get the color name.</summary>
		/// <returns>The color name.<see cref="System.String"/></returns>
		public string ColorName
		{	get	{	return _color.ToString ();	}	}
		
		/// <summary>Get the color value.</summary>
		/// <returns>The color value.<see cref="X11.TColor"/></returns>
		public X11.TColor Color
		{	get	{	return _color;	}	}

		/// <summary>Get or set the thickness of the stroke produced by this pen.</summary>
		/// <returns>The thickness of the stroke produced by this System.Windows.Media.Pen. Default is 1.<see cref="System.Double"/></returns>
        public double Thickness
		{	get	{	return _thickness;	}
			set	{	_thickness = value;	}
		}
		
        #endregion Properties
		
	}
	
}