// ==================
// The Xpm C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: October 2013
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
using System.Runtime.InteropServices;

namespace X11
{
	public class Xpmlib
	{
		
		[Flags]
		public enum XpmResult : int
		{
			XpmColorError					= 1,
			XpmSuccess						= 0,
			XpmOpenFailed					= -1,
			XpmFileInvalid					= -2,
			XpmNoMemory						= -3,
			XpmColorFailed					= -4
  		}
		
		
		[Flags]
		public enum XpmValueMask : long
		{
			XpmVisual						= (1L<<0),	/* visual                        | Default value is: DefaultVisual(display, DefaultScreen(display)) */
			XpmColormap						= (1L<<1),	/* colormap                      | Default value is: DefaultColormap(display, DefaultScreen(display)) */
			XpmDepth						= (1L<<2),	/* depth                         | Default value is: DefaultDepth(display, DefaultScreen(display)) */
			XpmSize							= (1L<<3),	/* width, height                 | Set when creating an XImage or a Pixmap. */
			XpmHotspot						= (1L<<4),	/* x_hotspot, y_hotspot          | Set if hotspot coordinates are found when parsing. */
			XpmCharsPerPixel				= (1L<<5),	/* cpp */
			XpmColorSymbols					= (1L<<6),	/* colorsymbols, numcolorsymbols */
			XpmRgbFilename					= (1L<<7),	/* rgb_fname */
			XpmInfos						= (1L<<8),	/* cpp, pixels, npixels, colorTable, ncolors, hints_cmt, colors_cmt, pixels_cmt, mask_pixel | Obsolete; colorTable cast to (XpmColor **) */
			XpmPixels						= (1L<<9),	/* pixels, npixels               | npixels differs from ncolors if several colors are bound to the same pixel, and if there is a mask (color = None). */
			XpmExtensions					= (1L<<10),	/* extensions, nextensions */
			XpmExactColors					= (1L<<11),	/* exactColors                   | Possible values are False (0) or True (1) */
			XpmCloseness					= (1L<<12),	/* closeness                     | Possible values are integers within the range: 0 to 65535 */
			XpmRGBCloseness					= (1L<<13),	/* red_closeness, green_closeness, blue_closeness | Possible values are integers within the range: 0 to 65535 */
			XpmColorKey						= (1L<<14), /* color_key                     | Possible values are: XPM_MONO, XPM_GRAY4, XPM_GRAY, XPM_COLOR */
			XpmColorTable					= (1L<<15),	/* colorTable, ncolors */
			XpmReturnAllocPixels			= (1L<<16)	/* alloc_pixels, nalloc_pixels   | nalloc_pixels differs from npixels when one pixel, given through the XpmColorSymbols, is used */
		}

		// Tested: O.K.
		/// <summary> Creates X images using XpmCreateImageFromData () and thus returns the same errors.
		/// In addition on success it then creates the related pixmaps, using XPutImage, which are returned to
		/// pixmap and shapemask if not NULL, and finally destroys the created images using XDestroyImage (). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> Specifies which screen the pixmap is created on. <see cref="IntPtr"/> </param>
		/// <param name="data"> Specifies the location of the data. <see cref="IntPtr"/> </param>
		/// <param name="pixmap"> Returns the pixmap which is created. <see cref="IntPtr"/> </param>
		/// <param name="shapemask"> Returns the shape mask pixmap which is created if the color None is used. <see cref="IntPtr"/> </param>
		/// <param name="attributes"> SSpecifies the location of a structure to get and store information (or NULL). <see cref="IntPtr"/> </param>
		/// <returns> If the file cannot be opened it returns XpmOpenFailed.
		/// If the file can be opened but does not contain valid XPM data, it returns XpmFileInvalid.
		/// If insufficient working storage is allocated, it returns XpmNoMemory. <see cref="TInt"/> </returns>
		/// <remarks> Do not forget to free the returned pixmaps, the colors, and possibly the data returned
		/// into the XpmAttributes structure when done. </remarks>
		[DllImport("libXpm")]
		extern public static XpmResult XpmCreatePixmapFromData(IntPtr x11display, IntPtr x11drawable, IntPtr data, ref IntPtr pixmap, ref IntPtr shapemask, ref IntPtr attributes);
	}
}

