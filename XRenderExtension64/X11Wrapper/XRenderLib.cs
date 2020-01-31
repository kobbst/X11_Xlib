// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: July 2015
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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace X11
{
	
	public class XRenderRequests
	{
		public static TInt X_RenderQueryVersion = (TInt)0;
		public static TInt X_RenderQueryPictFormats = (TInt)1;
		public static TInt X_RenderQueryPictIndexValues = (TInt)2;
		/* 0.7 */
		public static TInt X_RenderQueryDithers = (TInt)3;
		public static TInt X_RenderCreatePicture = (TInt)4;
		public static TInt X_RenderChangePicture = (TInt)5;
		public static TInt X_RenderSetPictureClipRectangles = (TInt)6;
		public static TInt X_RenderFreePicture = (TInt)7;
		public static TInt X_RenderComposite = (TInt)8;
		public static TInt X_RenderScale = (TInt)9;
		public static TInt X_RenderTrapezoids = (TInt)10;
		public static TInt X_RenderTriangles = (TInt)11;
		public static TInt X_RenderTriStrip = (TInt)12;
		public static TInt X_RenderTriFan = (TInt)13;
		public static TInt X_RenderColorTrapezoids = (TInt)14;
		public static TInt X_RenderColorTriangles = (TInt)15;
		/* public static TInt X_RenderTransform = (TInt)16 */
		public static TInt X_RenderCreateGlyphSet = (TInt)17;
		public static TInt X_RenderReferenceGlyphSet = (TInt)18;
		public static TInt X_RenderFreeGlyphSet = (TInt)19;
		public static TInt X_RenderAddGlyphs = (TInt)20;
		public static TInt X_RenderAddGlyphsFromPicture = (TInt)21;
		public static TInt X_RenderFreeGlyphs = (TInt)22;
		public static TInt X_RenderCompositeGlyphs8 = (TInt)23;
		public static TInt X_RenderCompositeGlyphs16 = (TInt)24;
		public static TInt X_RenderCompositeGlyphs32 = (TInt)25;
		public static TInt X_RenderFillRectangles = (TInt)26;
		/* 0.5 */
		public static TInt X_RenderCreateCursor = (TInt)27;
		/* 0.6 */
		public static TInt X_RenderSetPictureTransform = (TInt)28;
		public static TInt X_RenderQueryFilters = (TInt)29;
		public static TInt X_RenderSetPictureFilter = (TInt)30;

		public static TInt RenderNumberRequests = (TInt)(X_RenderSetPictureFilter + 1);
	}
	
	public class XRenderErrors
	{
		public static TInt BadPictFormat = (TInt)0;
		public static TInt BadPicture = (TInt)1;
		public static TInt BadPictOp = (TInt)2;
		public static TInt BadGlyphSet = (TInt)3;
		public static TInt BadGlyph = (TInt)4;
		public static TInt RenderNumberErrors = (TInt)(BadGlyph + 1);
	}
	
	public class XRenderPictureOp
	{
		public static TInt PictTypeIndexed = (TInt)0;
		public static TInt PictTypeDirect = (TInt)1;
		
		public static TInt PictOpMinimum = (TInt)0;
		
		public static TInt PictOpClear = (TInt)0;
		public static TInt PictOpSrc = (TInt)1;
		public static TInt PictOpDst = (TInt)2;
		public static TInt PictOpOver = (TInt)3;
		public static TInt PictOpOverReverse = (TInt)4;
		public static TInt PictOpIn = (TInt)5;
		public static TInt PictOpInReverse = (TInt)6;
		public static TInt PictOpOut = (TInt)7;
		public static TInt PictOpOutReverse = (TInt)8;
		public static TInt PictOpAtop = (TInt)9;
		public static TInt PictOpAtopReverse = (TInt)10;
		public static TInt PictOpXor = (TInt)11;
		public static TInt PictOpAdd = (TInt)12;
		public static TInt PictOpSaturate = (TInt)13;
		public static TInt PictOpMaximum = (TInt)13;
		/* * Operators only available in version 0.2 */
		public static TInt PictOpDisjointMinimum = (TInt)0x10;
		public static TInt PictOpDisjointClear = (TInt)0x10;
		public static TInt PictOpDisjointSrc = (TInt)0x11;
		public static TInt PictOpDisjointDst = (TInt)0x12;
		public static TInt PictOpDisjointOver = (TInt)0x13;
		public static TInt PictOpDisjointOverReverse = (TInt)0x14;
		public static TInt PictOpDisjointIn = (TInt)0x15;
		public static TInt PictOpDisjointInReverse = (TInt)0x16;
		public static TInt PictOpDisjointOut = (TInt)0x17;
		public static TInt PictOpDisjointOutReverse = (TInt)0x18;
		public static TInt PictOpDisjointAtop = (TInt)0x19;
		public static TInt PictOpDisjointAtopReverse = (TInt)0x1a;
		public static TInt PictOpDisjointXor = (TInt)0x1b;
		public static TInt PictOpDisjointMaximum = (TInt)0x1b;
		public static TInt PictOpConjointMinimum = (TInt)0x20;
		public static TInt PictOpConjointClear = (TInt)0x20;
		public static TInt PictOpConjointSrc = (TInt)0x21;
		public static TInt PictOpConjointDst = (TInt)0x22;
		public static TInt PictOpConjointOver = (TInt)0x23;
		public static TInt PictOpConjointOverReverse = (TInt)0x24;
		public static TInt PictOpConjointIn = (TInt)0x25;
		public static TInt PictOpConjointInReverse = (TInt)0x26;
		public static TInt PictOpConjointOut = (TInt)0x27;
		public static TInt PictOpConjointOutReverse = (TInt)0x28;
		public static TInt PictOpConjointAtop = (TInt)0x29;
		public static TInt PictOpConjointAtopReverse = (TInt)0x2a;
		public static TInt PictOpConjointXor = (TInt)0x2b;
		public static TInt PictOpConjointMaximum = (TInt)0x2b;
	}
	
	public class XRenderPolyEdge
	{
		public static TInt PolyEdgeSharp = (TInt)0;
		public static TInt PolyEdgeSmooth = (TInt)1;
		
		public static TInt PolyModePrecise = (TInt)0;
		public static TInt PolyModeImprecise = (TInt)1;
	}
	
	[Flags]
	public enum XRenderCreatePictureValueMask : uint
	{
		CPNone             = 0,
		CPRepeat           = (1 << 0),
		CPAlphaMap         = (1 << 1),
		CPAlphaXOrigin     = (1 << 2),
		CPAlphaYOrigin     = (1 << 3),
		CPClipXOrigin      = (1 << 4),
		CPClipYOrigin      = (1 << 5),
		CPClipMask         = (1 << 6),
		CPGraphicsExposure = (1 << 7),
		CPSubwindowMode    = (1 << 8),
		CPPolyEdge         = (1 << 9),
		CPPolyMode         = (1 << 10),
		CPDither           = (1 << 11),
		CPComponentAlpha   = (1 << 12),
		CPLastBit          = 11
	}
	
	[Flags]
	public enum XRenderPictStandardFormat
	{
		PictStandardARGB32  = 0,	// depth 32, bits 31-24 A, 23-16 R, 15-8 G, 7-0 B
		PictStandardRGB24   = 1,	// depth 24, bits 23-16 R, 15-8 G, 7-0 B
		PictStandardA8	    = 2,	// depth 8, bits 7-0 A
		PictStandardA4	    = 3,	// depth 4, bits 3-0 A
		PictStandardA1	    = 4,	// depth 1, bits 0 A
		PictStandardNUM	    = 5
	}
	
	public class XRenderFilters
	{
		/* Filters included in 0.6 */
		public static string FilterNearest = "nearest";
		public static string FilterBilinear = "bilinear";
		public static string FilterFast = "fast";
		public static string FilterGood = "good";
		public static string FilterBest = "best";

		public static TInt FilterAliasNone = (TInt)(-1);
	}
	
	public class XRenderSubPixelOrder
	{
		/* Subpixel orders included in 0.6 */
		public static TInt SubPixelUnknown = (TInt)0;
		public static TInt SubPixelHorizontalRGB = (TInt)1;
		public static TInt SubPixelHorizontalBGR = (TInt)2;
		public static TInt SubPixelVerticalRGB = (TInt)3;
		public static TInt SubPixelVerticalBGR = (TInt)4;
		public static TInt SubPixelNone = (TInt)5;
	}
	
	public class XRenderLib
	{
		
		public static X11.TInt XDoubleToFixed (double val)	{	return (TInt)((int)(val * 65536));	}
		public static double   XFixedToDouble (double val)	{	return (val / 65536.0);	}
		
		// Tested: O.K.
		/// <summary>Determine whether render extension is available on display.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="eventBasep">The first event number used by the extension (note that Render currently uses no events).<see cref="X11.TInt"/></param>
		/// <param name="errorBasep">he first error number used by the extension.<see cref="X11.TInt"/></param>
		/// <returns>True if the Render extension is available on display, or false otherwise.<see cref="System.Boolean"/></returns>
		[DllImport ("libXrender")]
        extern public static bool XRenderQueryExtension (IntPtr x11display, out TInt eventBasep, out TInt errorBasep);
		
		// Tested: O.K.
		/// <summary>Discover the current Render version number.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="majorVersionp">The major version number less than or equal to the library version numbers RENDER_MAJOR.<see cref="X11.TInt"/></param>
		/// <param name="minorVersionp">The minor version number less than or equal to the library version numbers RENDER_MAJOR.<see cref="X11.TInt"/></param>
		/// <returns>Zero if the Render extension is not present or soome error occurred while attempting to discover the current Render
		/// version number, or 1 otherwise.<see cref="TInt"/></returns>
		[DllImport ("libXrender")]
        extern public static TInt XRenderQueryVersion (IntPtr x11display, out TInt majorVersionp, out TInt minorVersionp);
		
		/// <summary>Fetch the available PictFormat information from the X server.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <returns>1 if the available PictFormat information has been fetched successfully from the X server, or 0 otherwise.<see cref="TInt"/></returns>
		[DllImport ("libXrender")]
        extern public static TInt XRenderQueryFormats (IntPtr x11display);
		
		/// <summary>Investigate the subpixel order.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// A <see cref="TInt"/>The subpixel order.</returns>
		/// <remarks>Applications interested in the geometry of the elements making up a single pixel on the screen should use
		/// XRenderQuerySubpixelOrder and do not cache the return value. XRenderSetSubpixelOrder is used by the XRandR library
		/// to update the value stored by Xrender when the subpixel order changes as a result of screen reconfiguration.</remarks>
		[DllImport ("libXrender")]
        extern public static TInt XRenderQuerySubpixelOrder (IntPtr x11display, TInt screenNumber);
		
		/// <summary>Set the subpixel order.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <param name="subpixel">
		/// A <see cref="System.Int32"/></param>
		/// <returns>
		/// A <see cref="System.Boolean"/></returns>
		/// <remarks>Applications interested in the geometry of the elements making up a single pixel on the screen should use
		/// XRenderQuerySubpixelOrder and do not cache the return value. XRenderSetSubpixelOrder is used by the XRandR library
		/// to update the value stored by Xrender when the subpixel order changes as a result of screen reconfiguration.</remarks>
		[DllImport ("libXrender")]
        extern public static bool XRenderSetSubpixelOrder (IntPtr x11display, TInt screenNumber, int subpixel);

		[StructLayout(LayoutKind.Sequential)]
		public struct XRenderDirectFormat
		{ 
			X11.TShort		red;		/* Bit offset for red. */
			X11.TShort		redMask; 
			X11.TShort		green;		/* Bit offset for green. */
			X11.TShort		greenMask; 
			X11.TShort		blue; 		/* Bit offset for blue. */
			X11.TShort		blueMask; 
			X11.TShort		alpha; 
			X11.TShort		alphaMask; 
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XRenderPictFormat
		{ 
			public IntPtr				id;			/* PictFormat */ 
			public X11.TInt				type;
			public X11.TInt				depth;
			public XRenderDirectFormat	direct;		/* XRenderDirectFormat */
			public IntPtr				colormap;	/* Colormap */
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XRenderPictureAttributes
		{ 
		    public TInt		repeat;
		    public IntPtr	alpha_map; /* Picture */
		    public TInt		alpha_x_origin;
		    public TInt		alpha_y_origin;
		    public TInt		clip_x_origin;
		    public TInt		clip_y_origin;
		    public IntPtr	clip_mask; /* Pixmap */
		    public bool		graphics_exposures;
		    public TInt		subwindow_mode;
		    public TInt		poly_edge;
		    public TInt		poly_mode;
		    public IntPtr	dither; /* Atom */
		    public bool		component_alpha;
		}
		
		
		/// <summary>Find the PictFormat suitable for use with the specified format.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="xRenderPictStandardFormat">The requested XRenderPictStandardFormat.<see cref="TInt"/></param>
		/// <returns>The PictFormat suitable for use with the specified format on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		[DllImport ("libXrender", EntryPoint = "XRenderFindStandardFormat")]
        extern public static IntPtr _XRenderFindStandardFormat (IntPtr x11display, TInt xRenderPictStandardFormat);
        public static XRenderPictFormat XRenderFindStandardFormat (IntPtr x11display, TInt xRenderPictStandardFormat)
		{
			XRenderPictFormat rpf = new XRenderPictFormat ();
			rpf.id = IntPtr.Zero;
			try
			{
				IntPtr pRpf = _XRenderFindStandardFormat (x11display, xRenderPictStandardFormat);
				if (pRpf != IntPtr.Zero)
					rpf = (XRenderPictFormat)Marshal.PtrToStructure (pRpf, typeof(XRenderPictFormat));
				// The 'pRpf' must not be free.
			}
			finally
			{	;	}
			
			return rpf;
		}

		/// <summary>Find the PictFormat suitable for use with the specified visual.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11visual">The visual to store the image. <see cref="IntPtr"/></param>
		/// <returns>The PictFormat suitable for use with the specified visual on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		[DllImport ("libXrender", EntryPoint = "XRenderFindVisualFormat")]
        extern public static IntPtr _XRenderFindVisualFormat (IntPtr x11display, IntPtr x11visual);
		public static XRenderPictFormat XRenderFindVisualFormat (IntPtr x11display, IntPtr x11visual)
		{
			XRenderPictFormat rpf = new XRenderPictFormat ();
			rpf.id = IntPtr.Zero;
			try
			{
				IntPtr pRpf = _XRenderFindVisualFormat (x11display, x11visual);
				if (pRpf != IntPtr.Zero)
					rpf = (XRenderPictFormat)Marshal.PtrToStructure (pRpf, typeof(XRenderPictFormat));
				// The 'pRpf' must not be free.
			}
			finally
			{	;	}
			
			return rpf;
		}
		
		// Tested: O.K.
		/// <summary>Create a picture for indicated drawable and specified format.  Any values specified in
		/// 'pXRenderPictureAttributes' and 'valuemask' are used in replace of the default values.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11drawable">The drawable to create a picture for<see cref="IntPtr"/></param>
		/// <param name="xRenderPictFormat">The desired format of the picture.<see cref="XRenderPictFormat"/></param>
		/// <param name="valueMask">Specifies which picture attributes are defined in the pXRenderPictureAttributes argument.
		/// This mask is the bitwise nclusive OR of the valid attribute mask bits. If valuemask is zero, the attributes are
		/// ignored and are not referenced.<see cref="XRenderCreatePictureValueMask"/></param>
		/// <param name="xRenderPictureAttributes">Specifies the structure from which the attribute values (as specified by the value mask)
		/// are to be taken.<see cref="XRenderPictureAttributes"/> </param>
		/// <returns>The picture for indicated drawable and specified format on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		[DllImport ("libXrender", EntryPoint = "XRenderCreatePicture")]
        extern public static IntPtr _XRenderCreatePicture (IntPtr x11display, IntPtr x11drawable, IntPtr /* *XRenderPictFormat*/ xRenderPictFormat,
		                                                   XRenderCreatePictureValueMask valueMask, ref XRenderPictureAttributes pXRenderPictureAttributes);
		[DllImport ("libXrender", EntryPoint = "XRenderCreatePicture")]
        extern public static IntPtr XRenderCreatePicture (IntPtr x11display, IntPtr x11drawable, ref XRenderPictFormat xRenderPictFormat,
		                                                  XRenderCreatePictureValueMask valueMask, ref XRenderPictureAttributes xRenderPictureAttributes);
		
		// Tested: O.K.
		/// <summary>Instruct the server to free picture.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11renderPicture">The picture to free.<see cref="IntPtr"/></param>
		[DllImport ("libXrender")]
        extern public static void XRenderFreePicture (IntPtr x11display, IntPtr x11renderPicture);

		[StructLayout(LayoutKind.Sequential)]
		public struct XRenderColor
		{
		    public TUshort   red;
		    public TUshort   green;
		    public TUshort   blue;
		    public TUshort   alpha;
			
			public XRenderColor (TUshort aRed, TUshort aGreen, TUshort aBlue, TUshort aAlpha)
			{
				red = aRed;
				green = aGreen;
				blue = aBlue;
				alpha = aAlpha;
			}
		}
		
		// Tested: O.K.
		/// <summary>Composite rectangles of the specified color.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="xRenderPictureOp">The render operation to apply.<see cref="TInt"/></param>
		/// <param name="pictureDestination">The picture to render into.<see cref="IntPtr"/></param>
		/// <param name="color">The color to apply.<see cref="XRenderColor"/></param>
		/// <param name="x"> The left top corner x-coordinate. <see cref="X11.TInt"/> </param>
		/// <param name="y"> The left top corner y-coordinate. <see cref="X11.TInt"/> </param>
		/// <param name="width"> The rectangle width. <see cref="X11.TUint"/> </param>
		/// <param name="height"> The rectangle height. <see cref="X11.TUint"/> </param>
		[DllImport ("libXrender")]
        extern public static void XRenderFillRectangle (IntPtr x11display, TInt xRenderPictureOp, IntPtr pictureDestination,
												        ref XRenderColor color, TInt x, TInt y, TUint width, TUint height);

		[DllImport ("libXrender")]
        extern public static void XRenderFillRectangles (IntPtr x11display, TInt operation, IntPtr pictureDestination,
												         ref XRenderColor color, ref X11lib.XRectangle rectangles, TInt numRects);

		[StructLayout(LayoutKind.Sequential)]
		public struct XPointFixed
		{ 
		    public TInt  x;/* XFixed */
		    public TInt  y;/* XFixed */
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XLineFixed
		{ 
		    public XPointFixed	p1; 
		    public XPointFixed	p2; 
		}
 

		[StructLayout(LayoutKind.Sequential)]
		public struct XTriangle
		{
			public XPointFixed	p1; 
			public XPointFixed	p2; 
			public XPointFixed	p3; 
		}
 

		[StructLayout(LayoutKind.Sequential)]
		public struct XCircle
		{
			public TInt  x;     /* XFixed */
			public TInt  y;     /* XFixed */
			public TInt  radius;/* XFixed */
		}
 
		[StructLayout(LayoutKind.Sequential)]
		public struct XTrapezoid
		{
			public TInt        top;   /* XFixed */
			public TInt        bottom;/* XFixed */
			public XLineFixed	left; 
			public XLineFixed	right; 
		}
		
		// Tested: O.K.
		[StructLayout(LayoutKind.Sequential)]
		public struct XLinearGradient
		{ 
			public XPointFixed  p1; 
			public XPointFixed  p2; 
		} 

		[StructLayout(LayoutKind.Sequential)]
		public struct XRadialGradient
		{
		    public XCircle inner; 
		    public XCircle outer; 
		}
 

		[StructLayout(LayoutKind.Sequential)]
		public struct XConicalGradient
		{
		    public XPointFixed center; 
		    public TInt        angle;  /* XFixed. Start angle, relative to the positive X-axis, counterclock-wise in degrees. */
		}
		
		// Tested: O.K.
		/// <summary>Create a picture, containing a linar gradient with the indicated dimensions.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="gradient">The gradient geometry (for linear gradient a line from p1 to p2).<see cref="XLinearGradient"/></param>
		/// <param name="stops">The color stop positions, relative to the gradient geometry.<see cref="X11.TInt[]"/></param>
		/// <param name="colors">The color premultiplied color values.<see cref="XRenderColor[]"/></param>
		/// <param name="nstops">The number of color stops.<see cref="X11.TInt"/></param>
		/// <returns>The picture, containing a linar gradient, on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		[DllImport ("libXrender")]
        extern public static IntPtr XRenderCreateLinearGradient (IntPtr x11display, ref XLinearGradient gradient, X11.TInt[] stops,
		                                                         XRenderColor[] colors, X11.TInt nstops);

		[DllImport ("libXrender")]
        extern public static IntPtr XRenderCreateRadialGradient (IntPtr x11display, ref XRadialGradient gradient, X11.TInt[] stops,
		                                                         XRenderColor[] colors, X11.TInt nstops);

		[DllImport ("libXrender")]
        extern public static IntPtr XRenderCreateConicalGradient (IntPtr x11display, ref XConicalGradient gradient, X11.TInt[] stops,
		                                                          XRenderColor[] colors, X11.TInt nstops);
		[DllImport ("libXrender")]
        extern public static void XRenderComposite (IntPtr x11display, X11.TInt operation, IntPtr src, IntPtr mask, IntPtr dst,
													X11.TInt src_x,  X11.TInt src_y,   X11.TInt mask_x, X11.TInt mask_y,
													X11.TInt dst_x,  X11.TInt dst_y,   X11.TUint width, X11.TUint height);

	}
}