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
using System.Collections.Generic;
using System.Diagnostics;

using X11;

namespace X11.Text
{
	
	/// <summary>The common functionality of a text style.</summary>
	public interface ITextStyle
	{
				
        /// <summary>Get or set the foreground (text) color.</summary>
		TColor ForeColor
		{	get;	set;	}
		
		/// <summary>Get the background color.</summary>
		TColor BackColor
		{	get;	}
		
		/// <summary>Get the style (regular, italic, bold, bold-italic).</summary>
		System.Drawing.FontStyle FontStyle
		{	get;	}

		/// <summary>Get the font data representing the font style.</summary>
		X11FontData FontData
		{	get;	set;	}
	}
	
	/// <summary>Defines a text style.</summary>
	public class Style : ITextStyle
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string			CLASS_NAME = "Style";
	
        #endregion
		
		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary> Indicate whether Dispose() has already been called. </summary>
		protected bool						_disposed	= false;
		
        /// <summary>The foreground (text) color.</summary>
		protected TColor					_foreColor = TColor.FallbackWhite;
		
		/// <summary>The background color.</summary>
		protected TColor					_backColor = TColor.FallbackBlack;
		
		/// <summary>The style (regular, italic, bold, bold-italic).</summary>
		protected System.Drawing.FontStyle	_fontStyle = System.Drawing.FontStyle.Regular;

		/// <summary>The font data representing the font style.</summary>
		protected X11FontData				_fontData = null;
		
		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>The hidden default constructor.</summary>
		private Style ()
		{
		}
		
		/// <summary>The initializing constructor. Initializes a new instance of the Range class.</summary>
        /// <param name="foreColor">The foreground (text) color.<see cref="X11.TColor"/></param>
		/// <param name="backColor">The background color.<see cref="X11.TColor"/></param>
		/// <param name="fontStyle">The font style.<see cref="System.Drawing.FontStyle"/></param>
        /// <param name="fontData">The font data.<see cref="X11.X11FontData"/></param>
        public Style(TColor foreColor, TColor backColor, System.Drawing.FontStyle fontStyle, X11FontData fontData)
        {
			if (fontData == null)
				throw new ArgumentNullException ("fontData");
			
			_foreColor = foreColor;
			_backColor = backColor;
			_fontStyle = fontStyle;
			
			_fontData  = fontData;
		}

        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
				
        /// <summary>Get the foreground (text) color.</summary>
		public TColor ForeColor
		{	get {	return _foreColor;	}
			set	{	_foreColor = value;	}
		}
		
		/// <summary>Get the background color.</summary>
		public TColor BackColor
		{	get {	return _backColor;	}	}
		
		/// <summary>Get the style (regular, italic, bold, bold-italic).</summary>
		public System.Drawing.FontStyle FontStyle
		{	get {	return _fontStyle;	}	}

		/// <summary>Get the font data representing the font style.</summary>
		public X11FontData FontData
		{	get {	return _fontData;	}
			set
			{	if (value == null)
					throw new ArgumentNullException ();
				
				if (_fontData != null && _fontData.FontSpecification == value.FontSpecification)
					return;
				
				_fontData = value;
			}
		}
		
		#endregion
		
	}

}