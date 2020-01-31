// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: February 2015
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

namespace X11.Text
{
	/// <summary>Provide consistent styled character for styled text, irrespective whether internationalized
	/// text output is supported (via fontset and TWchar) or not (via single font and XChar2b).</summary>
	public struct TStyleChar
	{

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes

		/// <summary>The character of a styled character.</summary>
		private X11.TWchar				_c;
		
		/// <summary>The style of a styled character.</summary>
		private int						_styleIndex;

        #endregion

        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initialize a new X11.Text.StyleChar instance.</summary>
		/// <param name="c">The character of a styled character.<see cref="System.Char"/></param>
		/// <param name="styleIndex">The style of a styled character.<see cref="System.Int32"/></param>
		public TStyleChar (char c, int styleIndex)
		{
			_c = X11.X11Utils.CharToWchar (c);
			_styleIndex = styleIndex;
		}
		
		/// <summary>Initialize a new X11.Text.StyleChar instance.</summary>
		/// <param name="c16">The character of a styled character.<see cref="X11lib.XChar2b"/></param>
		/// <param name="styleIndex">The style of a styled character.<see cref="System.Int32"/></param>
		public TStyleChar (X11lib.XChar2b c16, int styleIndex)
		{
			_c = X11.X11Utils.CharToWchar (X11.X11Utils.XChar2bToChar (c16));
			_styleIndex = styleIndex;
		}
		
		/// <summary>Initialize a new X11.Text.StyleChar instance.</summary>
		/// <param name="c32">The character of a styled character.<see cref="X11.TWchar"/></param>
		/// <param name="styleIndex">The style of a styled character.<see cref="System.Int32"/></param>
		public TStyleChar (X11.TWchar c32, int styleIndex)
		{
			_c = c32;
			_styleIndex = styleIndex;
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary>Get the character of a styled character.</summary>
		public char C
		{	get	{	return X11.X11Utils.WcharToChar (_c);	}	}
		
		/// <summary>Get the character of a styled character.</summary>
		public X11lib.XChar2b C16
		{	get	{	return X11.X11Utils.WcharToXChar2b (_c);	}	}
		
		/// <summary>Get the character of a styled character.</summary>
		public X11.TWchar C32
		{	get	{	return _c;	}	}
		
		/// <summary>Get the style of a styled character.</summary>
		public int StyleIndex
		{	get	{	return _styleIndex;			}	}
		
		#endregion

	}
}

