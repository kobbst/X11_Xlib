// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: June 2013
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2013 Steffen Ploetz
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
	/// <summary> The utility class to work with colors. </summary>
	public static class X11ColorModel
	{
		/// <summary>read-only GrayScale. </summary>
		public static TInt StaticGray	= (TInt)0;

		/// <summary>  A degenerate case of PseudoColor, when within a triple, R=G=B. </summary>
		public static TInt GrayScale	= (TInt)1;

		/// <summary> read-only PseudoColor. </summary>
		public static TInt StaticColor	= (TInt)2;

		/// <summary>  The pixel value indexes the colormap (an array of triples) to produce RGB values. </summary>
		public static TInt PseudoColor	= (TInt)3;

		/// <summary>  Like DirectColor, except that colormap has predefined read-only, server-dependent, values. </summary>
		public static TInt TrueColor	= (TInt)4;

		/// <summary> A pixel value is separated into 3 fields, each used for indexing separate R,G,B arrays. </summary>
		public static TInt DirectColor	= (TInt)5;

		/// <summary> Determines whether visual supports direct color (32 or 24 bit depth). </summary>
		/// <param name='display'> The display pointer, that specifies the connection to the X server </param>
		/// <param name='scrnID'> The screen number, that specifies the appropriate screen on the host server. </param>
		/// <returns> <c>true</c> if visual supports direct color, otherwise, <c>false</c>. </returns>
		public static bool IsDirectColorVisual (IntPtr display, TInt scrnID)
		{
			X11lib.XVisualInfo	visInfo = new X11lib.XVisualInfo();
			
			if (X11lib.XMatchVisualInfo (display, scrnID, (TInt)32, DirectColor, ref visInfo) != 0)
			{
				//X11lib.XFree (visInfo);
				return true;
			}
			else if (X11lib.XMatchVisualInfo (display, scrnID, (TInt)24, DirectColor, ref visInfo) != 0)
			{
				//X11lib.XFree (visInfo);
				return true;
			}
			else
				return false;
		}

		/// <summary> Determines whether visual supports true color (32 or 24 bit depth). </summary>
		/// <param name='display'> The display pointer, that specifies the connection to the X server </param>
		/// <param name='scrnID'> The screen number, that specifies the appropriate screen on the host server. </param>
		/// <returns> <c>true</c> if visual supports direct color, otherwise, <c>false</c>. </returns>
		public static bool IsTrueColorVisual (IntPtr display, TInt scrnID)
		{
			X11lib.XVisualInfo	visInfo = new X11lib.XVisualInfo();
			
			if (X11lib.XMatchVisualInfo (display, scrnID, (TInt)32, TrueColor, ref visInfo) != 0)
			{
				//X11lib.XFree (visInfo);
				return true;
			}
			else if (X11lib.XMatchVisualInfo (display, scrnID, (TInt)24, TrueColor, ref visInfo) != 0)
			{
				//X11lib.XFree (visInfo);
				return true;
			}
			else
				return false;
		}
	}
}