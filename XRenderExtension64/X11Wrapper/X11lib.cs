// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: April 2013
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
using System.Diagnostics;
using System.Runtime.InteropServices;

// ==========================================================================================================
// Type mapping:
// ==========================================================================================================
// Xlib / X11 / Xt		C#				C / C++
// ----------------------------------------------------------------------------------------------------------
// Widget	    		System.IntPtr;
// XtPointer    		System.IntPtr;
// XFloat      			System.Single; // X11 32 Bit: 4 Bytes:
// XDouble     			System.Double; // X11 32 Bit: 8 Bytes:

namespace X11
{
	// ==========================================================================================================
	// Type mapping:
	// ==========================================================================================================
	// Xlib / X11 / Xt							C / C++
	// ----------------------------------------------------------------------------------------------------------
	public enum TChar		: sbyte		{	};  // X11 64/32 Bit: 1 Byte:                  -127 to 127
	public enum TUchar		: byte		{	};  // X11 64/32 Bit: 1 Byte:                     0 to 255
	public enum TWchar		: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum TShort		: short		{	};  // X11 64/32 Bit: 2 Byte:                -32767 to 32767
	public enum TUshort		: ushort	{	};  // X11 64/32 Bit: 2 Byte:                     0 to 65535
	public enum TInt		: int		{	};  // X11 64/32 Bit: 4 Bytes:       -2.147.483.648 to 2.147.483.647
	public enum TUint		: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295

	//public enum TLong		: int		{	};  // X11    32 Bit: 4 Bytes:       -2.147.483.648 to 2.147.483.647
	public enum TLong		: long		{	};  // X11 64    Bit: 8 Bytes: -9223372036854775807 to 9223372036854775807
	//public enum TUlong	: uint		{	};  // X11    32 Bit: 4 Bytes:                    0 to 4294967295
	public enum TUlong		: ulong		{	};  // X11 64    Bit: 8 Bytes:                    0 to 18446744073709551615

	public enum TLonglong	: long		{	};  // X11 64/32 Bit: 8 Bytes: -9223372036854775807 to 9223372036854775807
	public enum TUlonglong	: ulong		{	};  // X11 64/32 Bit: 8 Bytes:                    0 to 18446744073709551615
//	public enum TFloat		: float		{	};  // X11    32 Bit: 4 Byte:              -1.5E-45 to -3.4E+38 with a precision of 7 decimal digits
//	public enum TDouble		: double	{	};  // X11    32 Bit: 8 Byte:             -5.0E-324 to -1.7E+308 with a precision of 15-16 decimal digits
	public enum TBoolean	: sbyte		{	};  // X11    32 Bit: 1 Byte:                  -127 to 127

	//public enum TPixel	: uint		{	};  // X11    32 Bit: 4 Bytes:                    0 to 4294967295
	public enum TPixel		: ulong		{	};  // X11 64    Bit: 8 Bytes:                    0 to 18446744073709551615

//	public enum Atom		: IntPtr	{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum Colormap	: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum Drawable	: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum Font		: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
//	public enum Pixmap		: IntPtr	{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum Time		: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum VisualID	: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum Window		: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum XID			: uint		{	};  // X11 64/32 Bit: 4 Bytes:                    0 to 4294967295
	public enum XtEnum		: byte		{	};  // X11    32 Bit: 1 Byte:                     0 to 255
	public enum XCardinal	: uint		{	};  // X11    32 Bit: 4 Bytes:          -2147483647 to 2147483647
	public enum XDimension	: ushort	{	};  // X11    32 Bit: 2 Byte:                     0 to 65535
	public enum XPosition	: short		{	};  // X11    32 Bit: 2 Byte:                -32767 to 32767

	//public enum XtArgVal	: int		{	};  // X11    32 Bit: 4 Bytes:                    0 to 65535
	public enum XtArgVal	: long		{	};  // X11 64    Bit: 4 Bytes:                    0 to 65535
	//public enum XtVersionType : uint	{	};  // X11    32 Bit: 4 Bytes:                    0 to 4294967295
	public enum XtVersionType : ulong	{	};  // X11 64    Bit: 8 Bytes:                    0 to 18446744073709551615

	public enum XrmName		: int		{	};  // X11    32 Bit: 4 Bytes:               -32767 to 32767
	public enum XrmClass	: int		{	};  // X11    32 Bit: 4 Bytes:               -32767 to 32767

	//public enum XtValueMask : uint		{	};  // X11    32 Bit: 4 Bytes:                    0 to 4294967295
	public enum XtValueMask : ulong	{	};  // X11 64    Bit: 8 Bytes:                    0 to 18446744073709551615
	
	public class Interop
	{
	
		/// <summary>Define the platform types.</summary>
	    public enum Platform
	    {
			/// <summary>The platform type is 32 bit.</summary>
	        X86,
			
			/// <summary>The platform type is 64 bit.</summary>
	        X64,
			
			/// <summary>The platform type is unknown.</summary>
	        Unknown
	    }
		
		/// <summary>Test current platform type for 32 bit.</summary>
		/// <returns>True if current platform type is 32 bit, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool IsCurrentPlatform32Bit()
		{	return (sizeof (X11.TPixel) == sizeof (uint) ? true : false);	}
		
		/// <summary>Test current platform type for 64 bit.</summary>
		/// <returns>True if current platform type is 64 bit, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool IsCurrentPlatform64Bit()
		{	return (sizeof (X11.TPixel) == sizeof (ulong) ? true : false);	}
		
		/// <summary>Test for current platform type.</summary>
		/// <returns>The current platform type.<see cref="X11.Interop.Platform"/></returns>
		public static Platform CurrentPlatform()
		{	return (IsCurrentPlatform32Bit() ? Platform.X86 : (IsCurrentPlatform64Bit() ? Platform.X64 : Platform.Unknown));	}
	}
	
	public class LibC
	{
		public static TInt LC_ALL = (TInt)0;
		public static TInt LC_COLLATE = (TInt)1;
		public static TInt LC_CTYPE = (TInt)2;
		public static TInt LC_MONETARY = (TInt)3;
		public static TInt LC_NUMERIC = (TInt)4;
		public static TInt LC_TIME = (TInt)5;
		public static TInt LC_MESSAGES = (TInt)6;
	}
	
	public class X11atoms
	{
		// Our atoms
		public static readonly IntPtr AnyPropertyType			= (IntPtr)0;
		public static readonly IntPtr XA_PRIMARY				= (IntPtr)1;
		public static readonly IntPtr XA_SECONDARY				= (IntPtr)2;
		public static readonly IntPtr XA_ARC					= (IntPtr)3;
		public static readonly IntPtr XA_ATOM					= (IntPtr)4;
		public static readonly IntPtr XA_BITMAP					= (IntPtr)5;
		public static readonly IntPtr XA_CARDINAL				= (IntPtr)6;
		public static readonly IntPtr XA_COLORMAP				= (IntPtr)7;
		public static readonly IntPtr XA_CURSOR					= (IntPtr)8;
		public static readonly IntPtr XA_CUT_BUFFER0			= (IntPtr)9;
		public static readonly IntPtr XA_CUT_BUFFER1			= (IntPtr)10;
		public static readonly IntPtr XA_CUT_BUFFER2			= (IntPtr)11;
		public static readonly IntPtr XA_CUT_BUFFER3			= (IntPtr)12;
		public static readonly IntPtr XA_CUT_BUFFER4			= (IntPtr)13;
		public static readonly IntPtr XA_CUT_BUFFER5			= (IntPtr)14;
		public static readonly IntPtr XA_CUT_BUFFER6			= (IntPtr)15;
		public static readonly IntPtr XA_CUT_BUFFER7			= (IntPtr)16;
		public static readonly IntPtr XA_DRAWABLE				= (IntPtr)17;
		public static readonly IntPtr XA_FONT					= (IntPtr)18;
		public static readonly IntPtr XA_INTEGER				= (IntPtr)19;
		public static readonly IntPtr XA_PIXMAP					= (IntPtr)20;
		public static readonly IntPtr XA_POINT					= (IntPtr)21;
		public static readonly IntPtr XA_RECTANGLE				= (IntPtr)22;
		public static readonly IntPtr XA_RESOURCE_MANAGER		= (IntPtr)23;
		public static readonly IntPtr XA_RGB_COLOR_MAP			= (IntPtr)24;
		public static readonly IntPtr XA_RGB_BEST_MAP			= (IntPtr)25;
		public static readonly IntPtr XA_RGB_BLUE_MAP			= (IntPtr)26;
		public static readonly IntPtr XA_RGB_DEFAULT_MAP		= (IntPtr)27;
		public static readonly IntPtr XA_RGB_GRAY_MAP			= (IntPtr)28;
		public static readonly IntPtr XA_RGB_GREEN_MAP			= (IntPtr)29;
		public static readonly IntPtr XA_RGB_RED_MAP			= (IntPtr)30;
		public static readonly IntPtr XA_STRING					= (IntPtr)31;
		public static readonly IntPtr XA_VISUALID				= (IntPtr)32;
		public static readonly IntPtr XA_WINDOW					= (IntPtr)33;
		public static readonly IntPtr XA_WM_COMMAND				= (IntPtr)34;
		public static readonly IntPtr XA_WM_HINTS				= (IntPtr)35;
		public static readonly IntPtr XA_WM_CLIENT_MACHINE		= (IntPtr)36;
		public static readonly IntPtr XA_WM_ICON_NAME			= (IntPtr)37;
		public static readonly IntPtr XA_WM_ICON_SIZE			= (IntPtr)38;
		public static readonly IntPtr XA_WM_NAME				= (IntPtr)39;
		public static readonly IntPtr XA_WM_NORMAL_HINTS		= (IntPtr)40;
		public static readonly IntPtr XA_WM_SIZE_HINTS			= (IntPtr)41;
		public static readonly IntPtr XA_WM_ZOOM_HINTS			= (IntPtr)42;
		public static readonly IntPtr XA_MIN_SPACE				= (IntPtr)43; // Minimum spacing between words, in pixels.
		public static readonly IntPtr XA_NORM_SPACE				= (IntPtr)44; // Normal spacing between words, in pixels.
		public static readonly IntPtr XA_MAX_SPACE				= (IntPtr)45; // Maximum spacing between words, in pixels.
		public static readonly IntPtr XA_END_SPACE				= (IntPtr)46; // Additional spacing at the end of a sentence, in pixels.
		public static readonly IntPtr XA_SUPERSCRIPT_X			= (IntPtr)47; // With XA_SUPERSCRIPT_Y, the offset from the character origin where superscripts should begin, in pixels. If the origin is [x, y], superscripts should begin at the following coordinates: x  +  XA_SUPERSCRIPT_X, y  -  XA_SUPERSCRIPT_Y
		public static readonly IntPtr XA_SUPERSCRIPT_Y			= (IntPtr)48; // With XA_SUPERSCRIPT_X, the offset from the character origin where superscripts should begin, in pixels.
		public static readonly IntPtr XA_SUBSCRIPT_X			= (IntPtr)49; // With XA_SUBSCRIPT_Y, the offset from the character origin where subscripts should begin, in pixels. If the origin is [x, y], subscripts should begin at the following coordinates: x  +  XA_SUBSCRIPT_X, y  +  XA_SUBSCRIPT_Y
		public static readonly IntPtr XA_SUBSCRIPT_Y			= (IntPtr)50; // With XA_SUBSCRIPT_X, the offset from the character origin where subscripts should begin, in pixels. See the description under XA_SUBSCRIPT_X.
		public static readonly IntPtr XA_UNDERLINE_POSITION		= (IntPtr)51; // The y offset from the baseline to the top of an underline, in pixels.  If the baseline y-coordinate is y, then the top of the underline is at y + XA_UNDERLINE_POSITION.
		public static readonly IntPtr XA_UNDERLINE_THICKNESS	= (IntPtr)52; // Thickness of the underline, in pixels.
		public static readonly IntPtr XA_STRIKEOUT_ASCENT		= (IntPtr)53; // With XA_STRIKEOUT_DESCENT, the vertical extent for boxing or voiding characters, in pixels. If the baseline y-coordinate is y, the top of the strikeout box is y - XA_STRIKEOUT_ASCENT. The height of the box is as follows: XA_STRIKEOUT_ASCENT + XA_STRIKEOUT_DESCENT
		public static readonly IntPtr XA_STRIKEOUT_DESCENT		= (IntPtr)54; // With XA_STRIKEOUT_ASCENT, the vertical extent for boxing or voiding characters, in pixels. See the description under XA_STRIKEOUT_ASCENT.
		public static readonly IntPtr XA_ITALIC_ANGLE			= (IntPtr)55; // The angle of the dominant staffs of characters in the font, in degrees scaled by 64, relative to the 3 o'clock position from the character origin. Positive values indicate counterclockwise motion.
		public static readonly IntPtr XA_X_HEIGHT				= (IntPtr)56; // One ex, as in TeX, but expressed in units of pixels. Often the height of lowercase x.
		public static readonly IntPtr XA_QUAD_WIDTH				= (IntPtr)57; // One em, as in TeX, but expressed in units of pixels. Often the width of the digits 0 to 9.
		public static readonly IntPtr XA_WEIGHT					= (IntPtr)58; // Weight or boldness of the font, expressed as a value between 0 and 1000.
		public static readonly IntPtr XA_POINT_SIZE				= (IntPtr)59; // Point size of the font at ideal resolution, expressed in 1/10 points.
		public static readonly IntPtr XA_RESOLUTION				= (IntPtr)60; // Number of pixels per point, expressed in 1/100, at which the font was created.
		public static readonly IntPtr XA_COPYRIGHT				= (IntPtr)61; // Copyright date of the font.
		public static readonly IntPtr XA_NOTICE					= (IntPtr)62; // Copyright date of the font name.
		public static readonly IntPtr XA_FONT_NAME				= (IntPtr)63; // For example: -Adobe-Helvetica-Bold-R-Normal--10-100-75-75-P-60-ISO8859-1
		public static readonly IntPtr XA_FAMILY_NAME			= (IntPtr)64; // For example: Helvetica
		public static readonly IntPtr XA_FULL_NAME				= (IntPtr)65; // For example: Helvetica Bold
		public static readonly IntPtr XA_CAP_HEIGHT				= (IntPtr)66; // The y offset from the baseline to the top of capital letters, ignoring ascents. If the baseline y-coordinate is y, the top of the capitals is at y - XA_CAP_HEIGHT.
		public static readonly IntPtr XA_WM_CLASS				= (IntPtr)67;
		public static readonly IntPtr XA_WM_TRANSIENT_FOR		= (IntPtr)68;
	}
	
	/// <summary>Prototype a predicate procedure for XIfEvent / XCheckIfEvent / XPeekIfEvent.</summary>
	public delegate TBoolean X11EventPredicateProc (IntPtr display, ref XEvent e, IntPtr data);
	
	/// <summary>Helper class for convenient XIfEvent / XCheckIfEvent / XPeekIfEvent calls.</summary>
	public static class X11EventHelper
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "X11EventHelper";
		
		#endregion
		
		/// <summary>Counter of matches.</summary>
		/// <remarks>Must be resetted by the caller prior usage.</remarks>
		public  static int						Matches							= 0;
		
		/// <summary>Event structure.</summary>
		/// <remarks>Ready to use by the caller.</remarks>
		public static XEvent					Event							= new XEvent();
		
		/// <summary>The CountExposeMatches predicate procedure instance.</summary>
		private	static X11EventPredicateProc	CountExposeMatchesProc			= new X11EventPredicateProc (CountExposeMatches);
		
		/// <summary>The CountExposeMatches predicate procedure instance pointer.</summary>
		public	static IntPtr					CountExposeMatchesProcPtr		= Marshal.GetFunctionPointerForDelegate(CountExposeMatchesProc);
		
		/// <summary>The CountConfigureMatches predicate procedure instance.</summary>
		private	static X11EventPredicateProc	CountConfigureMatchesProc		= new X11EventPredicateProc (CountConfigureMatches);
		
		/// <summary>The CountConfigureMatches predicate procedure instance pointer.</summary>
		public	static IntPtr					CountConfigureMatchesProcPtr	= Marshal.GetFunctionPointerForDelegate(CountConfigureMatchesProc);
		
		/// <summary>The CountExposeMatches predicate procedure to use with XIfEvent / XCheckIfEvent / XPeekIfEvent calls.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="e">The event structure, used to return a matching event.<see cref="XEvent"/></param>
		/// <param name="data">The additional data passed to the predicate procedure.<see cref="IntPtr"/></param>
		/// <returns>Always false to prevent removing any event from the queue.<see cref="TBoolean"/></returns>
		private static TBoolean CountExposeMatches (IntPtr x11display, ref XEvent e, IntPtr window)
		{
			try
			{
				if (e.type == XEventName.Expose && e.ExposeEvent.window == window)
					X11EventHelper.Matches++;
			}
			finally
			{	;	}
			return (TBoolean)0;
		}
		
		/// <summary>The CountConfigureMatches predicate procedure to use with XIfEvent / XCheckIfEvent / XPeekIfEvent calls.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="e">The event structure, used to return a matching event.<see cref="XEvent"/></param>
		/// <param name="data">The additional data passed to the predicate procedure.<see cref="IntPtr"/></param>
		/// <returns>Always false to prevent removing any event from the queue.<see cref="TBoolean"/></returns>
		private static TBoolean CountConfigureMatches (IntPtr x11display, ref XEvent e, IntPtr window)
		{
			try
			{
				if (e.type == XEventName.ConfigureNotify && e.ConfigureEvent.window == window)
					X11EventHelper.Matches++;
			}
			finally
			{	;	}
			return (TBoolean)0;
		}
	}
	
	public class X11lib
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "X11EventHelper";
		
		/// <summary>Let the server insert the current system time.</summary>
		public const TUlong CurrentTime	= 0;
		
		#endregion
		
		[DllImport("XlibWrapper.so")]
		extern public static void WrapXTest (IntPtr dpy, IntPtr gc);

		[DllImport("XlibWrapper.so")]
		extern public static void WrapXGetGCValues (IntPtr dpy, IntPtr gc, uint flags, ref XGCValues xgcv);
		
		[DllImport("XlibWrapper.so")]
		extern public static void WrapMinimalXaw ();
	
		/// <summary> X11 integer point structure. </summary>
		public struct XPoint
		{
			/// <summary> The x-coordinate. </summary>
			public TShort X;
			
			/// <summary> The x-coordinate. </summary>
			public TShort Y;
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="x"> The x-coordinate. <see cref="X11.TShort"/> </param>
			/// <param name="y"> The y-coordinate. <see cref="X11.TShort"/> </param>
			public XPoint (TShort x, TShort y)
			{
				X = x;
				Y = y;
			}
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="x"> The x-coordinate. <see cref="X11.TInt"/> </param>
			/// <param name="y"> The y-coordinate. <see cref="X11.TInt"/> </param>
			public XPoint (TInt x, TInt y)
			{
				X = (TShort)x;
				Y = (TShort)y;
			}
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="x"> The x-coordinate. <see cref="System.Int32"/> </param>
			/// <param name="y"> The y-coordinate. <see cref="System.Int32"/> </param>
			public XPoint (int x, int y)
			{
				X = (TShort)x;
				Y = (TShort)y;
			}
		}
			
		[Flags]
		public enum GCattributemask : long
		{
			GCFunction						= (1L<<0),
			GCPlaneMask						= (1L<<1),
			GCForeground					= (1L<<2),
			GCBackground					= (1L<<3),
			GCLineWidth						= (1L<<4),
			GCLineStyle						= (1L<<5),
			GCCapStyle						= (1L<<6),
			GCJoinStyle						= (1L<<7),
			GCFillStyle						= (1L<<8),
			GCFillRule						= (1L<<9),
			GCTile							= (1L<<10),
			GCStipple						= (1L<<11),
			GCTileStipXOrigin				= (1L<<12),
			GCTileStipYOrigin				= (1L<<13),
			GCFont							= (1L<<14),
			GCSubwindowMode					= (1L<<15),
			GCGraphicsExposures				= (1L<<16),
			GCClipXOrigin					= (1L<<17),
			GCClipYOrigin					= (1L<<18),
			GCClipMask						= (1L<<19),
			GCDashOffset					= (1L<<20),
			GCDashList						= (1L<<21),
			GCArcMode						= (1L<<22)
		}

		public enum WindowClass : uint
		{
			CopyFromParent					= 0,
			InputOutput						= 1,
			InputOnly						= 2
		}
	
		// Tested: O.K.
		public enum TRevertTo : int
		{
			RevertToNone					= 0,
			RevertToPointerRoot				= 1,
			RevertToParent					= 2
		}
		
		public enum TImageFormat : int
		{
			XYBitmap						= 0,
			XYPixmap						= 1,
			ZPixmap							= 2
		}
		
		// Tested: O.K.
		public enum ColormapAllocType : int
		{
			AllocNone						= 0,
			AllocAll						= 1
		}
		
		public enum ScreenClass : int
		{
			StaticGray					= 0,
			GrayScale					= 1,
			StaticColor					= 2,
			PseudoColor					= 3,
			TrueColor					= 4,
			DirectColor					= 5
		}
		
		// Tested: O.K.
		[Flags]
		public enum WindowAttributeMask : uint
		{
			CWBackPixmap					= (1<<0),
			CWBackPixel						= (1<<1),
			CWBorderPixmap					= (1<<2),
			CWBorderPixel					= (1<<3),
			CWBitGravity					= (1<<4),
			CWWinGravity					= (1<<5),
			CWBackingStore					= (1<<6),
			CWBackingPlanes					= (1<<7),
			CWBackingPixel					= (1<<8),
			CWOverrideRedirect				= (1<<9),
			CWSaveUnder						= (1<<10),
			CWEventMask						= (1<<11),
			CWDontPropagate					= (1<<12),
			CWColormap						= (1<<13),
			CWCursor						= (1<<14)
		}
		
		public enum WindowMapState : int
		{
			IsUnmapped = 0,
			IsUnviewable = 1,
			IsViewable = 2
		}
		
		public struct XWindowAttributes
		{
			public TInt						x, y;					/* location of window */
			public TInt						width, height;			/* width and height of window */
			public TInt						border_width;			/* border width of window */
			public TInt						depth;					/* depth of window */
			public IntPtr					visual;					/* Visual: the associated visual structure */
			public IntPtr					root;					/* Window: root of screen containing window */
			public TInt						cls;					/* InputOutput, InputOnly*/
			public TInt						bit_gravity;			/* one of the bit gravity values */
			public TInt						win_gravity;			/* one of the window gravity values */
			public TInt						backing_store;			/* draw into umpapped window to speed up mapping (aviod Expose event): NotUseful = 0, WhenMapped = 1, Always = 2 */
			public TUlong					backing_planes;			/* planes to be preserved if possible */
			public TUlong					backing_pixel;			/* value to be used when restoring planes */
			public TBoolean					save_under;				/* boolean, should bits under be saved? */
			public IntPtr					colormap;				/* Colormap: color map to be associated with window */
			public TBoolean					map_installed;			/* boolean, is color map currently installed*/
			public WindowMapState			map_state;				/* TInt: IsUnmapped = 0, IsUnviewable = 1, IsViewable = 2 */
			public TLong					all_event_masks;		/* set of events all people have interest in*/
			public TLong					your_event_mask;		/* my event mask */
			public TLong					do_not_propagate_mask;	/* set of events that should not propagate */
			public TBoolean					override_redirect;		/* boolean value for override-redirect */
			public IntPtr					screen;					/* Screen: back pointer to correct screen */
		}
		
		// See: Chapter 4. Window Attributes
		// Tested: O.K.
		public struct XSetWindowAttributes
		{
			public IntPtr					background_pixmap;		/* background, None, or ParentRelative */
			public TPixel					background_pixel;		/* background pixel */ 
			public IntPtr					border_pixmap;			/* border of the window or CopyFromParent */ 
			public TPixel					border_pixel;			/* border pixel value */ 
			public TInt						bit_gravity;			/* one of bit gravity values */ 
			public TInt						win_gravity;			/* one of the window gravity values */
			public TInt						backing_store;			/* draw into umpapped window to speed up mapping (aviod Expose event): NotUseful = 0, WhenMapped = 1, Always = 2 */
			public TUlong					backing_planes;			/* planes to be preserved if possible */ 
			public TUlong					backing_pixel;			/* value to use in restoring planes */ 
			public TBoolean					save_under;				/* should bits under be saved? (popups) */
			public TLong					event_mask;				/* set of events that should be saved */
			public TLong					do_not_propagate_mask;	/* set of events that should not propagate */ 
			public TBoolean					override_redirect;		/* boolean value for override_redirect */ 
			public IntPtr					colormap;				/* color map to be associated with window */
			public IntPtr					cursor;					/* cursor to be displayed (or None) */		
		}
		
		public enum XCoordinateMode
		{
			CoordModeOrigin		= 0,	/* relative to the origin */
			CoordModePrevious   = 1 	/* relative to previous point */
		}
		
		public enum XPolygonShape
		{
			Complex				= 0,	/* paths may intersect */
			Nonconvex			= 1,	/* no paths intersect, but not convex */
			Convex				= 2 	/* wholly convex */
		}
		
		public enum XArcModes
		{
			ArcChord			= 0,	/* join endpoints of arc */
			ArcPieSlice			= 1 	/* join endpoints to center of arc */
		}
		
		[Flags]
		public enum XGCValueMask :uint
		{
			GCFunction			= (1<<0),
			GCPlaneMask			= (1<<1),
			GCForeground		= (1<<2),
			GCBackground		= (1<<3),
			GCLineWidth			= (1<<4),
			GCLineStyle			= (1<<5),
			GCCapStyle			= (1<<6),
			GCJoinStyle			= (1<<7),
			GCFillStyle			= (1<<8),
			GCFillRule			= (1<<9),
			GCTile				= (1<<10),
			GCStipple			= (1<<11),
			GCTileStipXOrigin	= (1<<12),
			GCTileStipYOrigin	= (1<<13),
			GCFont				= (1<<14),
			GCSubwindowMode		= (1<<15),
			GCGraphicsExposures	= (1<<16),
			GCClipXOrigin		= (1<<17),
			GCClipYOrigin		= (1<<18),
			GCClipMask			= (1<<19),
			GCDashOffset		= (1<<20),
			GCDashList			= (1<<21),
			GCArcMode			= (1<<22),
		}
		
		public enum XGCFunction : int
		{
			GXclear				= 0x0,       /* 0 */
			GXand				= 0x1,       /* src AND dst */
			GXandReverse		= 0x2,       /* src AND NOT dst */
			GXcopy				= 0x3,       /* src */
			GXandInverted		= 0x4,       /* NOT src AND dst */
			GXnoop				= 0x5,       /* dst */
			GXxor				= 0x6,       /* src XOR dst */
			GXor				= 0x7,       /* src OR dst */
			GXnor				= 0x8,       /* NOT src AND NOT dst */
			GXequiv				= 0x9,       /* NOT src XOR dst */
			GXinvert			= 0xa,       /* NOT dst */
			GXorReverse			= 0xb,       /* src OR NOT dst */
			GXcopyInverted		= 0xc,       /* NOT src */
			GXorInverted		= 0xd,       /* NOT src OR dst */
			GXnand				= 0xe,       /* NOT src OR NOT dst */
			GXset				= 0xf        /* 1 */
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>See: http://www.sbin.org/doc/Xlib/chapt_05.html</remarks>
		public struct XGCValues
		{
			public XGCFunction	function;			/* logical operation */
			public TPixel		plane_mask;			/* plane mask */
			public TPixel		foreground;			/* foreground pixel */
			public TPixel		background;			/* background pixel */
			public TInt			line_width;			/* line width (in pixels) */
			public XGCLineStyle	line_style;			/* TInt: LineSolid, LineOnOffDash, LineDoubleDash */
			public XGCCapStyle	cap_style;			/* TInt: CapNotLast, CapButt, CapRound, CapProjecting */
			public XGCJoinStyle	join_style;			/* TInt: JoinMiter, JoinRound, JoinBevel */
			public TInt			fill_style;			/* FillSolid, FillTiled, FillStippled FillOpaqueStippled*/
			public XGCFillRule	fill_rule;			/* TInt: EvenOddRule, WindingRule */
			public TInt			arc_mode;			/* ArcChord, ArcPieSlice */
			public IntPtr		tile;				/* tile pixmap for tiling operations */
			public IntPtr		stipple;			/* stipple 1 plane pixmap for stippling */
			public TInt			ts_x_origin;		/* offset for tile or stipple operations */
			public TInt			ts_y_origin;
			public IntPtr		font;				/* default text font for text operations */
			public TInt			subwindow_mode;		/* ClipByChildren, IncludeInferiors */
			public TBoolean		graphics_exposures;	/* boolean, should exposures be generated */
			public TInt			clip_x_origin;		/* origin for clipping */
			public TInt			clip_y_origin;
			public IntPtr		clip_mask;			/* bitmap clipping; other calls for rects */
			public TInt			dash_offset;		/* patterned/dashed line information */
			public TChar		dashes;
		}
		
		/// <summary>The line-style defines which sections of a line are drawn.</summary>
		/// <remarks>See: http://www.sbin.org/doc/Xlib/chapt_05.html</remarks>
		public enum XGCLineStyle : int
		{
			/// <summary>The full path of the line is drawn.</summary>
			LineSolid,
			
			/// <summary>Only the even dashes are drawn, and cap-style applies to all internal ends of the individual dashes,
			/// except CapNotLast is treated as CapButt.</summary>
			LineOnOffDash,
			
			/// <summary>The full path of the line is drawn, but the even dashes are filled differently than the odd dashes
			/// (see fill-style) with CapButt style used where even and odd dashes meet. Typically the dashes are drawn with
			/// the foreground pixel value, gaps are drawn with the background pixel values.</summary>
			LineDoubleDash
		}
		
		/// <summary>The cap-style defines how the endpoints of a path are drawn.</summary>
		/// <remarks>See: http://www.sbin.org/doc/Xlib/chapt_05.html</remarks>
		public enum XGCCapStyle : int
		{
			/// <summary>This is equivalent to CapButt except that for a line-width of zero the final endpoint is not drawn.</summary>
			CapNotLast = 0,
			
			/// <summary>The line is square at the endpoint (perpendicular to the slope of the line) with no projection beyond. </summary>
			CapButt = 1,
			
			/// <summary>The line has a circular arc with the diameter equal to the line-width, centered on the endpoint. (This is equivalent to CapButt for line-width of zero). </summary>
			CapRound = 2,
			
			/// <summary>The line is square at the end, but the path continues beyond the endpoint for a distance equal to half the line-width. (This is equivalent to CapButt for line-width of zero).</summary>
			CapProjecting = 3
		}
		
		/// <summary>The join-style defines how corners are drawn for wide lines.</summary>
		/// <remarks>See: http://www.sbin.org/doc/Xlib/chapt_05.html</remarks>
		public enum XGCJoinStyle : int
		{
			/// <summary>The outer edges of two lines extend to meet at an angle. However, if the angle is less than 11 degrees, then a JoinBevel join-style is used instead.</summary>
			JoinMiter = 0,
			
			/// <summary>The corner is a circular arc with the diameter equal to the line-width, centered on the joinpoint. </summary>
			JoinRound = 1,
			
			/// <summary>The corner has CapButt endpoint styles with the triangular notch filled.</summary>
			JoinBevel = 2
		}
		
		/// <summary>Specify how to fill text and polygon [e.g. by XDrawText(), XDrawText16(), XFillRectangle(), XFillPolygon(), and XFillArc()],
		/// how to draw lines [e.g. with XDrawLine(), XDrawSegments(), XDrawRectangle(), XDrawArc()] and
		/// to draw even dashes of a dashed line [with line-style LineOnOffDash, LineDoubleDash].</summary>
		public enum XGCFillStyle : int
		{
			/// <summary>Apply the foreground color.</summary>
			FillSolid			= 0,
			
			/// <summary>Apply a tile bitmap.</summary>
			FillTiled			= 1,
			
			/// <summary>Apply foreground masked by stipple.</summary>
			FillStippled		= 2,
			
			/// <summary>Apply a tile bitmap with the same width and height as stipple,
			/// but with background everywhere stipple has a zero and with foreground everywhere stipple has a one.</summary>
			FillOpaqueStippled	= 3
		}
		
		/// <summary>Define what pixels are inside a path to draw.</summary>
		/// <remarks>The fill-rule defines what pixels are inside (drawn) for paths given in
		/// XFillPolygon() requests and can be set to EvenOddRule or WindingRule.</remarks>
		/// <remarks>See: http://www.sbin.org/doc/Xlib/chapt_05.html</remarks>
		public enum XGCFillRule : int
		{
			/// <summary>For EvenOddRule, a point is inside if an infinite ray with the point as origin
			/// crosses the path an odd number of times.</summary>
			EvenOddRule			= 0,
			
			/// <summary>For WindingRule, a point is inside if an infinite ray with the point as origin
			/// crosses an unequal number of clockwise and counterclockwise directed path segments.</summary>
 			WindingRule			= 1
			
			// A clockwise directed path segment is one that crosses the ray from left to right as observed
			// from the point. A counterclockwise segment is one that crosses the ray from right to left as
			// observed from the point. The case where a directed line segment is coincident with the ray is
			// uninteresting because you can simply choose a different ray that is not coincident with a segment. 
		}
		
		public enum CursorFontShape : uint
		{
			XC_X_cursor				= 0,
			XC_arrow				= 2,
			XC_based_arrow_down		= 4,
			XC_based_arrow_up		= 6,
			XC_boat					= 8,
			XC_bogosity				= 10,
			XC_bottom_left_corner	= 12,
			XC_bottom_right_corner	= 14,
			XC_bottom_side			= 16,
			XC_bottom_tee			= 18,
			XC_box_spiral			= 20,
			XC_center_ptr			= 22,
			XC_circle				= 24,
			XC_clock				= 26,
			XC_coffee_mug			= 28,
			XC_cross				= 30,
			XC_cross_reverse		= 32,
			XC_crosshair			= 34,
			XC_diamond_cross		= 36,
			XC_dot					= 38,
			XC_dot_box_mask			= 40,
			XC_double_arrow			= 42,
			XC_draft_large			= 44,
			XC_draft_small			= 46,
			XC_draped_box			= 48,
			XC_exchange				= 50,
			XC_fleur				= 52,
			XC_gobbler				= 54,
			XC_gumby				= 56,
			XC_hand1				= 58,
			XC_hand2				= 60,
			XC_heart				= 62,
			XC_icon					= 64,
			XC_iron_cross			= 66,
			XC_left_ptr				= 68,
			XC_left_side			= 70,
			XC_left_tee				= 72,
			XC_leftbutton			= 74,
			XC_ll_angle				= 76,
			XC_lr_angle				= 78,
			XC_man					= 80,
			XC_middlebutton			= 82,
			XC_mouse				= 84,
			XC_pencil				= 86,
			XC_pirate				= 88,
			XC_plus					= 90,
			XC_question_arrow		= 92,
			XC_right_ptr			= 94,
			XC_right_side			= 96,
			XC_right_tee			= 98,
			XC_rightbutton			= 100,
			XC_rtl_logo				= 102,
			XC_sailboat				= 104,
			XC_sb_down_arrow		= 106,
			XC_sb_h_double_arrow	= 108,
			XC_sb_left_arrow		= 110,
			XC_sb_right_arrow		= 112,
			XC_sb_up_arrow			= 114,
			XC_sb_v_double_arrow	= 116,
			XC_shuttle				= 118,
			XC_sizing				= 120,
			XC_spider				= 122,
			XC_spraycan				= 124,
			XC_star					= 126,
			XC_target				= 128,
			XC_tcross				= 130,
			XC_top_left_arrow		= 132,
			XC_top_left_corner		= 134,
			XC_top_right_corner		= 136,
			XC_top_side				= 138,
			XC_top_tee				= 140,
			XC_trek					= 142,
			XC_ul_angle				= 144,
			XC_umbrella				= 146,
			XC_ur_angle				= 148,
			XC_watch				= 150,
			XC_xterm				= 152,
		}
		
		
		[StructLayout(LayoutKind.Sequential)]
		public struct XCharStruct
		{
			public X11.TShort lbearing;			/* origin to left edge of raster */
			public X11.TShort rbearing;			/* origin to right edge of raster */
			public X11.TShort width;			/* advance to next char's origin */
			public X11.TShort ascent;			/* baseline to top edge of raster */
			public X11.TShort descent;			/* baseline to bottom edge of raster */
			public X11.TUshort attributes;		/* per char flags (not predefined) */
		}


		[StructLayout(LayoutKind.Sequential)]
		public struct XChar2b /* X11 16 bit characters are two separate bytes. */
		{
			/// <summary>The first byte of a 2 byte character.</summary>
		    public X11.TUchar byte1;
		    
			/// <summary>The second byte of a 2 byte character.</summary>
		    public X11.TUchar byte2;
			
			/// <summary>The null character.</summary>
			public static X11lib.XChar2b Zero = new X11lib.XChar2b ((X11.TUchar)0, (X11.TUchar)0);
			
			/// <summary>Initialize a new X11lib.XChar2b instance.</summary>
			/// <param name="b1">The first byte of a 2 byte character.<see cref="X11.TUchar"/></param>
			/// <param name="b2">The first byte of a 2 byte character.<see cref="X11.TUchar"/></param>
			public XChar2b (X11.TUchar b1, X11.TUchar b2)
			{
				byte1 = b1;
				byte2 = b2;
			}
			
			/// <summary>Initialize a new X11lib.XChar2b instance.</summary>
			/// <param name="c">The character to create a X11lib.XChar2b for.<see cref="System.Char"/></param>
			public XChar2b (char c)
			{
				byte1 = (X11.TUchar)(c & 255);
				byte2 = (X11.TUchar)(c >> 8);
			}
			
			/// <summary>Evaluates two XChar2b to determine equality.</summary>
			/// <param name="left">The first XChar2b to compare.<see cref="XChar2b"/></param>
			/// <param name="right">The second XChar2b to compare.<see cref="XChar2b"/></param>
			/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
			public static bool operator ==(XChar2b left, XChar2b right)
			{	return left.Equals (right);		}
			
			/// <summary>Evaluates two XChar2b to determine inequality.</summary>
			/// <param name="left">The first XChar2b to compare.<see cref="XChar2b"/></param>
			/// <param name="right">The second XChar2b to compare.<see cref="XChar2b"/></param>
			/// <returns>True on inequality, or false otherwise.<see cref="System.Boolean"/></returns>
			public static bool operator !=(XChar2b left, XChar2b right)
			{	return !left.Equals (right);		}
			
			/// <summary>Determine whether the current instance of XChar2b and a specified object, which must also be a XChar2b, have the same value. (Overrides Object.Equals(Object).)</summary>
			/// <param name="other">The XChar2b to compare with the current instance of string.<see cref="System.Object"/></param>
			/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
			public override bool Equals (object other)
			{	return CompareTo (other) == 0;
			}
		
			/// <summary>Compares the current instance of XChar2b with a specified object, which must also be a XChar2b, and indicate whether this instance of XChar2b
			/// precedes, follows, or appears in the same position in the sort order as the specified XChar2b. </summary>
			/// <param name="other">The XChar2b to compare with the current instance of string.<see cref="XChar2b"/></param>
			/// <returns>-1 if the current instance of XChar2b is is less than the XChar2b to compare to,
			/// 1 if the current instance of XChar2b is greater than the XChar2b to compare to or
			/// 0 if the current instance of XChar2b is equal to the XChar2b to compare to.<see cref="System.Int32"/></returns>
			public int CompareTo (object other)
			{	return this.CompareTo ((XChar2b)other);	}
		
			/// <summary>Compares the current instance of XChar2b with a specified XChar2b and indicate whether this instance of XChar2b
			/// precedes, follows, or appears in the same position in the sort order as the specified XChar2b.</summary>
			/// <param name="other">The XChar2b to compare with the current instance of XChar2b.<see cref="XChar2b"/></param>
			/// <returns>-1 if the current instance of XChar2b is is less than the XChar2b to compare to,
			/// 1 if the current instance of XChar2b is greater than the XChar2b to compare to or
			/// 0 if the current instance of XChar2b is equal to the XChar2b to compare to.<see cref="System.Int32"/></returns>
			public int CompareTo (XChar2b other)
			{
				if (object.Equals(other, null)) // Prevent cyclic calls.
					return 1;
				
				if (this.byte1 < other.byte1)
					return -1;
				if (this.byte1 > other.byte1)
					return 1;
				
				if (this.byte2 < other.byte2)
					return -1;
				if (this.byte2 > other.byte2)
					return 1;
				
				return 0;
			}
			
			/// <summary>Retrieves the hash code for this object.</summary>
			/// <returns>A 32-bit hash code, which is a signed integer.<see cref="System.Int32"/></returns>
			public override int GetHashCode ()
			{	return (this.byte1.GetHashCode() ^ this.byte2.GetHashCode());	}
			
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct XFontStruct
		{
			public IntPtr		ext_data;			/* XExtData *: hook for extension to hang data */
			public X11.XID		fid;				/* Font: font id for this font */
			public TUint		direction;			/* unsigned: hint about the direction font is painted */
			public TUint		min_char_or_byte2;	/* unsigned: first character */
			public TUint		max_char_or_byte2;	/* unsigned: last character */
			public TUint		min_byte1;			/* unsigned: first row that exists */
			public TUint		max_byte1;			/* unsigned: last row that exists */
			public TBoolean		all_chars_exist;	/* Bool: flag if all characters have nonzero size */
			public TUint		default_char;		/* unsigned: char to print for undefined character */
			public TInt			n_properties;		/* int: how many properties there are */
			public IntPtr		properties;			/* XFontProp *: pointer to array of additional properties */
			public XCharStruct	min_bounds;			/* XCharStruct: minimum bounds over all existing char */
			public XCharStruct	max_bounds;			/* XCharStruct:	maximum bounds over all existing char */
			public IntPtr		per_char;			/* XCharStruct *: first_char to last_char information */
			public TInt			ascent;				/* int: logical extent above baseline for spacing */
			public TInt			descent;			/* int: logical decent below baseline for spacing */
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct XColor
		{
			public TPixel		pixel;				/* pixel value */
			public X11.TUshort	red, green, blue;	/* rgb values */
			public X11.TUchar	flags;				/* DoRed, DoGreen, DoBlue */	
			public X11.TUchar	pad;
			
			public const X11.TUchar DoRed   = (X11.TUchar)1;
			public const X11.TUchar DoGreen = (X11.TUchar)2;
			public const X11.TUchar DoBlue  = (X11.TUchar)4;
		};

		public static XColor NewXColor(TPixel pixel, TUchar r, TUchar g, TUchar b, TUchar flags, TUchar pad)
		{
			XColor result = new XColor();
			result.pixel = pixel;
			result.red = (TUshort)(((int)r) * 256);
			result.green = (TUshort)(((int)g) * 256);
			result.blue = (TUshort)(((int)b) * 256);
			result.flags = flags;
			result.pad = pad;
			
			return result;
		}
		
		/// <summary> Internal memory mapping structure for XClassHint structure. </summary>
		/// <remarks> First structure element is on offset 0. This can be used to free the structute itself. </remarks>
		[StructLayout(LayoutKind.Sequential)]
		private struct _XClassHint
		{
			/// <summary> The application name (might be changed during runtime). </summary>
			/// <remarks> Must be freed separately. </remarks>
			public IntPtr res_name;
			/// <summary> The application class name (should be constant during runtime). </summary>
			/// <remarks> Must be freed separately. </remarks>
			public IntPtr res_class;
			
			public static _XClassHint Zero = new _XClassHint ();
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct XClassHint
		{
			[MarshalAs(UnmanagedType.LPStr)] public string res_name;
			[MarshalAs(UnmanagedType.LPStr)] public string res_class;
			
			public static XClassHint Zero = new XClassHint ();
		}
		
		// Tested: O.K.
        [StructLayout(LayoutKind.Sequential)]
		public struct XTextProperty
		{
			public	IntPtr		val;			/* property data */
			public	IntPtr		encoding;		/* type of property */
			public	TInt		format;			/* 8, 16, or 32 */
			public	TUlong		nitems;			/* number of items in value */
		}
		
		// Tested: OK (for flags, icon_pixmap, icon_x, icon_y, icon_mask)
		[StructLayout(LayoutKind.Sequential)]
		public struct XWMHints
		{
			public	XWMHintMask	flags;			/* marks which fields in this structure are defined */
			public	TBoolean	input;			/* does this application rely on the window manager to get keyboard input? */
			public	TInt		initial_state;	/*	WithdrawnState	0,	NormalState	1,	IconicState	3 */
			public	IntPtr		icon_pixmap;	/* Pixmap: pixmap to be used as icon */
			public	IntPtr		icon_window;	/* Window: window to be used as icon */
			public	TInt		icon_x, icon_y;	/* initial position of icon */
			public	IntPtr		icon_mask;		/* Pixmap: pixmap to be used as mask for icon_pixmap */
			public	IntPtr		window_group;	/* XID: id of related window group */
		}
		
		public enum XWMHintMask : int

		{
			InputHint			= (1 << 0),
			StateHint			= (1 << 1),
			IconPixmapHint		= (1 << 2),
			IconWindowHint		= (1 << 3),
			IconPositionHint	= (1 << 4),
			IconMaskHint		= (1 << 5),
			WindowGroupHint		= (1 << 6),
			UrgencyHint			= (1 << 8),
			AllHints	 		= (InputHint|StateHint|IconPixmapHint|IconWindowHint|IconPositionHint|IconMaskHint|WindowGroupHint)
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct XRectangle
		{
			public TShort	x;
			public TShort	y;
			public TUshort  width;
			public TUshort  height;
			
			public XRectangle (TShort aX, TShort aY, TUshort aWidth, TUshort aHeight)
			{
				x = aX;
				y = aY;
				width = aWidth;
				height = aHeight;
			}
		};
		
		[StructLayout(LayoutKind.Sequential)]
		public struct XFontSetExtents
		{
		    public XRectangle max_ink_extent;		/* over all drawable characters */
		    public XRectangle max_logical_extent;	/* over all drawable characters */
		};
		
		public enum ClipRectanglesOrdering
		{
			Unsorted			= 0,
			YSorted				= 1,
			YXSorted			= 2,
			YXBanded			= 3
		};
		
		public enum Gravity
		{
			ForgetGravity		= 0,
			UnmapGravity		= 0,
			NorthWestGravity	= 1,
			NorthGravity		= 2,
			NorthEastGravity	= 3,
			WestGravity			= 4,
			CenterGravity		= 5,
			EastGravity			= 6,
			SouthWestGravity	= 7,
			SouthGravity		= 8,
			SouthEastGravity	= 9,
			StaticGravity		= 10
		}
		
		/// <summary>A KeySym is the encoding of a symbol on the cap of a key.</summary>
	    public enum XKeySym : uint
	    {
			XK_NONE				= 0x0000,
			XK_SPACE			= 0x0020,
			
			XK_EXCLAMATION		= 0x0021, // !
			XK_QUOTATIONMARK	= 0x0022, // "
			XK_SHARP			= 0x0023, // #
			XK_DOLLAR			= 0x0024, // $
			XK_PERCENT			= 0x0025, // %
			XK_AMPERSAND		= 0x0026, // &
			XK_APOSTROPHE		= 0x0027, // '
			XK_LEFTPARANTHESIS	= 0x0028, // (
			XK_RIGHTPARANTHESIS = 0x0029, // )
			XK_ASTERISK			= 0x002a, // *
			XK_PLUS				= 0x002b, // +
			XK_DECIMAL			= 0x002c, // ,
			XK_MINUS			= 0x002d, // -
			XK_DOT				= 0x002e, // .
			XK_SLASH			= 0x002f, // /
// BEGIN: German keyboard.
			XK_Decimal0			= 0x0030,
			XK_Decimal1			= 0x0031,
			XK_Decimal2			= 0x0032,
			XK_Decimal3			= 0x0033,
			XK_Decimal4			= 0x0034,
			XK_Decimal5			= 0x0035,
			XK_Decimal6			= 0x0036,
			XK_Decimal7			= 0x0037,
			XK_Decimal8			= 0x0038,
			XK_Decimal9			= 0x0039,
			XK_COLON			= 0x003a, // :
			XK_SEMICOLON		= 0x003b, // ;
			XK_LEFTANGLEBRACKET	= 0x003c, // <
			XK_EQUAL			= 0x003d, // =
			XK_LRIGHTANGLEBRACKET= 0x003e, // >
			XK_QUESTIONMARK		= 0x003f, // ?

			XK_LEFTBRACKET		= 0x005b, // [
			XK_BACKSLASH		= 0x005c, // \
			XK_LRIGHTBRACKET	= 0x005d, // ]
			XK_CARET			= 0x005e, // ^
			XK_UNDERSCORE		= 0x005f, // _
// END: German keyboard.
			XK_A				= 0x0061,
			XK_B				= 0x0062,
			XK_C				= 0x0063,
			XK_D				= 0x0064,
			XK_E				= 0x0065,
			XK_F				= 0x0066,
			XK_G				= 0x0067,
			XK_H				= 0x0068,
			XK_I				= 0x0069,
			XK_J				= 0x006A,
			XK_K				= 0x006B,
			XK_L				= 0x006C,
			XK_M				= 0x006D,
			XK_N				= 0x006E,
			XK_O				= 0x006F,
			XK_P				= 0x0070,
			XK_Q				= 0x0071,
			XK_R				= 0x0072,
			XK_S				= 0x0073,
			XK_T				= 0x0074,
			XK_U				= 0x0075,
			XK_V				= 0x0076,
			XK_W				= 0x0077,
			XK_X				= 0x0078,
			XK_Y				= 0x0079,
			XK_Z				= 0x007A,

			XK_PARAGRAPH		= 0x00a7, // §
			XK_DEGREE			= 0x00b0, // °

			XK_LEFTCURLYBRACE	= 0x007b, // {
			XK_PIPE				= 0x007c, // |
			XK_RIGHTCURLYBRACE	= 0x007d, // }
			XK_TILDE			= 0x007e, // ~
			
	        XK_Back_Space		= 0xFF08,
	        XK_Tab				= 0xFF09,
	        XK_Clear			= 0xFF0B,
	        XK_Return			= 0xFF0D,
			XK_Scroll_Lock		= 0xFF14,
			XK_Pause			= 0xFF13,
			XK_Escap			= 0xFF1b,
	        XK_Home				= 0xFF50,
	        XK_Left				= 0xFF51,
	        XK_Up				= 0xFF52,
	        XK_Right			= 0xFF53,
	        XK_Down				= 0xFF54,
	        XK_Page_Up			= 0xFF55,
	        XK_Page_Down		= 0xFF56,
	        XK_End				= 0xFF57,
	        XK_Begin			= 0xFF58,
			XK_Select			= 0xFF60,
			XK_Print			= 0xFF61,
			XK_Execute			= 0xFF62,
			XK_Insert			= 0xFF63,
	        XK_Menu				= 0xFF67,
	        XK_Help				= 0xFF6A,
			XK_Num_Lock			= 0xFF7F,
			XK_Num_Enter		= 0xFF8D,
			XK_Multiply			= 0xFFAA, // NUMPAD_Multiply
			XK_Add				= 0xFFAB, // NUMPAD_Add
			XK_Separator		= 0xFFAC, // NUMPAD_Separator
			XK_Subtract			= 0xFFAD, // NUMPAD_Subtract
			XK_Decimal			= 0xFFAE, // NUMPAD_Decimal
			XK_Divide			= 0xFFAF, // NUMPAD_Divide
			XK_Numpad_0			= 0xFFB0, // NUMPAD_0
			XK_Numpad_1			= 0xFFB1, // NUMPAD_1
			XK_Numpad_2			= 0xFFB2, // NUMPAD_2
			XK_Numpad_3			= 0xFFB3, // NUMPAD_3
			XK_Numpad_4			= 0xFFB4, // NUMPAD_4
			XK_Numpad_5			= 0xFFB5, // NUMPAD_5
			XK_Numpad_6			= 0xFFB6, // NUMPAD_6
			XK_Numpad_7			= 0xFFB7, // NUMPAD_7
			XK_Numpad_8			= 0xFFB8, // NUMPAD_8
			XK_Numpad_9			= 0xFFB9, // NUMPAD_9
			XK_F1				= 0xFFBE, // F1
			XK_F2				= 0xFFBF, // F2
			XK_F3				= 0xFFC0, // F3
			XK_F4				= 0xFFC1, // F4
			XK_F5				= 0xFFC2, // F5
			XK_F6				= 0xFFC3, // F6
			XK_F7				= 0xFFC4, // F7
			XK_F8				= 0xFFC5, // F8
			XK_F9				= 0xFFC6, // F9
			XK_F10				= 0xFFC7, // F10
			XK_F11				= 0xFFC8, // F11
			XK_F12				= 0xFFC9, // F12
			XK_F13				= 0xFFCA, // F13
			XK_F14				= 0xFFCB, // F14
			XK_F15				= 0xFFCC, // F15
			XK_F16				= 0xFFCD, // F16
			XK_F17				= 0xFFCE, // F17
			XK_F18				= 0xFFCF, // F18
			XK_F19				= 0xFFD0, // F19
			XK_F20				= 0xFFD1, // F20
			XK_F21				= 0xFFD2, // F21
			XK_F22				= 0xFFD3, // F22
			XK_F23				= 0xFFD4, // F23
			XK_F24				= 0xFFD5, // F24
			XK_Shift_L			= 0xFFE1,
	        XK_Shift_R			= 0xFFE2,
	        XK_Control_L		= 0xFFE3,
	        XK_Control_R		= 0xFFE4,
	        XK_Caps_Lock		= 0xFFE5,
	        XK_Shift_Lock		= 0xFFE6,
	        XK_Meta_L			= 0xFFE7,
	        XK_Meta_R			= 0xFFE8,
	        XK_Alt_L			= 0xFFE9,
	        XK_Alt_R			= 0xFFEA,
	        XK_Super_L			= 0xFFEB,
	        XK_Super_R			= 0xFFEC,
	        XK_Hyper_L			= 0xFFED,
			XK_Hyper_R			= 0xFFEE,
	        XK_Delete			= 0xFFFF,
	    }
		
		public static XKeySym XToKeySym (char c)
		{
			XKeySym result = XKeySym.XK_NONE;
			switch (c)
			{
				case ' ':		result = XKeySym.XK_SPACE; break;
				case '!':		result = XKeySym.XK_EXCLAMATION; break;

				case '"':		result = XKeySym.XK_QUOTATIONMARK; break;
				case '#':		result = XKeySym.XK_SHARP; break;
				case '$':		result = XKeySym.XK_DOLLAR; break;
				case '%':		result = XKeySym.XK_PERCENT; break;
				case '&':		result = XKeySym.XK_AMPERSAND; break;
				case '\'':		result = XKeySym.XK_APOSTROPHE; break;
				case '(':		result = XKeySym.XK_LEFTPARANTHESIS; break;
				case ')':		result = XKeySym.XK_RIGHTPARANTHESIS; break;
				case '*':		result = XKeySym.XK_ASTERISK; break;
				case '+':		result = XKeySym.XK_PLUS; break;
				case ',':		result = XKeySym.XK_DECIMAL; break;
				case '-':		result = XKeySym.XK_MINUS; break;
				case '.':		result = XKeySym.XK_DOT; break;
				case '/':		result = XKeySym.XK_SLASH; break;

				case '0':		result = XKeySym.XK_Decimal0; break;
				case '1':		result = XKeySym.XK_Decimal1; break;
				case '2':		result = XKeySym.XK_Decimal2; break;
				case '3':		result = XKeySym.XK_Decimal3; break;
				case '4':		result = XKeySym.XK_Decimal4; break;
				case '5':		result = XKeySym.XK_Decimal5; break;
				case '6':		result = XKeySym.XK_Decimal6; break;
				case '7':		result = XKeySym.XK_Decimal7; break;
				case '8':		result = XKeySym.XK_Decimal8; break;
				case '9':		result = XKeySym.XK_Decimal9; break;
				
				case ':':		result = XKeySym.XK_COLON; break;
				case ';':		result = XKeySym.XK_SEMICOLON; break;
				case '<':		result = XKeySym.XK_LEFTANGLEBRACKET; break;
				case '=':		result = XKeySym.XK_EQUAL; break;
				case '>':		result = XKeySym.XK_LRIGHTANGLEBRACKET; break;
				case '?':		result = XKeySym.XK_QUESTIONMARK; break;
			
				case '[':		result = XKeySym.XK_LEFTBRACKET; break;
				case '\\':		result = XKeySym.XK_BACKSLASH; break;
				case ']':		result = XKeySym.XK_LRIGHTBRACKET; break;
				case '^':		result = XKeySym.XK_CARET; break;
				case '_':		result = XKeySym.XK_UNDERSCORE; break;

				case 'a':
				case 'A':		result = XKeySym.XK_A; break;
				case 'b':
				case 'B':		result = XKeySym.XK_B; break;
				case 'c':
				case 'C':		result = XKeySym.XK_C; break;
				case 'd':
				case 'D':		result = XKeySym.XK_D; break;
				case 'e':
				case 'E':		result = XKeySym.XK_E; break;
				case 'f':
				case 'F':		result = XKeySym.XK_F; break;
				case 'g':
				case 'G':		result = XKeySym.XK_G; break;
				case 'h':
				case 'H':		result = XKeySym.XK_H; break;
				case 'i':
				case 'I':		result = XKeySym.XK_I; break;
				case 'j':
				case 'J':		result = XKeySym.XK_J; break;
				case 'k':
				case 'K':		result = XKeySym.XK_K; break;
				case 'l':
				case 'L':		result = XKeySym.XK_L; break;
				case 'm':
				case 'M':		result = XKeySym.XK_M; break;
				case 'n':
				case 'N':		result = XKeySym.XK_N; break;
				case 'o':
				case 'O':		result = XKeySym.XK_O; break;
				case 'p':
				case 'P':		result = XKeySym.XK_P; break;
				case 'q':
				case 'Q':		result = XKeySym.XK_Q; break;
				case 'r':
				case 'R':		result = XKeySym.XK_R; break;
				case 's':
				case 'S':		result = XKeySym.XK_S; break;
				case 't':
				case 'T':		result = XKeySym.XK_T; break;
				case 'u':
				case 'U':		result = XKeySym.XK_U; break;
				case 'v':
				case 'V':		result = XKeySym.XK_V; break;
				case 'w':
				case 'W':		result = XKeySym.XK_W; break;
				case 'x':
				case 'X':		result = XKeySym.XK_X; break;
				case 'y':
				case 'Y':		result = XKeySym.XK_Y; break;
				case 'z':
				case 'Z':		result = XKeySym.XK_Z; break;
				
				case '§':		result = XKeySym.XK_PARAGRAPH; break;
				case '°':		result = XKeySym.XK_DEGREE; break;

				case '{':		result = XKeySym.XK_LEFTCURLYBRACE; break;
				case '|':		result = XKeySym.XK_PIPE; break;
				case '}':		result = XKeySym.XK_RIGHTCURLYBRACE; break;
				case '~':		result = XKeySym.XK_TILDE; break;
			}
			return result;
		}
		
		[Flags]
		public enum XVisualInfoMask
		{
			VisualNoMask			= 0x0,
			VisualIDMask			= 0x1,
			VisualScreenMask		= 0x2,
			VisualDepthMask			= 0x4,
			VisualClassMask			= 0x8,
			VisualRedMaskMask		= 0x10,
			VisualGreenMaskMask		= 0x20,
			VisualBlueMaskMask		= 0x40,
			VisualColormapSizeMask	= 0x80,
			VisualBitsPerRGBMask	= 0x100,
			VisualAllMask			= 0x1FF
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct XVisualInfo
		{
			public	IntPtr		visual;
			public	TLong		visualid;
			public	TInt		screen;
			public	TInt		depth;
			public	TInt		cls;
			public	TUlong		red_mask;
			public	TUlong		green_mask;
			public	TUlong		blue_mask;
			public	TInt		colormap_size;
			public	TInt		bits_per_rgb;
		}
		
		// ##########################################################################################################
		// ###   W I N D O W   C R E A T I O N   A N D   M A N I P U L A T I O N
		// ##########################################################################################################

		#region Window creation and manipulation methods
		
		// Tested: O.K.
		/// <summary> Open a connection to the X server that controls a display. </summary>
		/// <param name="displayName"> Display name syntax is: hostname:number.screen_number; like: dual-headed:0.1; or empty sting for default. <see cref="String"/> </param>
		/// <returns> The display pointer on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XOpenDisplay (String x11displayName);
		
		// Tested: O.K.
		/// <summary> Close a connection to the X server that controls a display. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XCloseDisplay (IntPtr x11display);

		// Tested: O.K.
		/// <summary> Get the root window. Useful with functions that need a drawable of a particular screen and for creating top-level windows. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The root window on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XRootWindow (IntPtr x11display, TInt screenNumber);
		
		/// <summary> Get the root window of the specified screen. </summary>
		/// <param name="x11screen"> The connection to an X server. <see cref="System.IntPtr"/> </param>
		/// <returns> The window ID. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XRootWindowOfScreen (IntPtr x11screen);
			
		// Tested: O.K.
		/// <summary> Destroy the specified window as well as all of its subwindows. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to destroy. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static  void XDestroyWindow (IntPtr x11display, IntPtr x11window);
		
		// Tested: O.K.
		/// <summary> Get the default root window. Useful with functions that need a drawable of a particular screen and for creating top-level windows. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <returns> The default root window on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XDefaultRootWindow (IntPtr x11display);
	
		// Tested: O.K.
		/// <summary> Move specified window to the specified x and y coordinates, but do not change the window's size, raise the window, or change the mapping state of the window. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to move. <see cref="IntPtr"/> </param>
		/// <param name="x"> The new x coordinate, which defines the new location of the top-left pixel of the window's border or the window itself if it has no border. <see cref="System.Int32"/> </param>
		/// <param name="y"> The new y coordinate, which defines the new location of the top-left pixel of the window's border or the window itself if it has no border. <see cref="System.Int32"/> </param>
		[DllImport("libX11")]
		extern public static void XMoveWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y);
	
		// Tested: O.K.
		/// <summary> Change the inside dimensions of the specified window, not including its borders. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to resize. <see cref="IntPtr"/> </param>
		/// <param name="width"> The new width, which is the interior dimensions of the window after the call completes. <see cref="System.UInt32"/> </param>
		/// <param name="height"> The new heigth, which is the interior dimensions of the window after the call completes. <see cref="System.UInt32"/> </param>
		[DllImport("libX11")]
		extern public static void XResizeWindow (IntPtr x11display, IntPtr x11window, TUint width, TUint height);
	
		// Tested: O.K.
		/// <summary> Change the size and location of the specified window without raising it. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to move and resize. <see cref="IntPtr"/> </param>
		/// <param name="x"> The new x coordinate, which defines the new location of the top-left pixel of the window's border or the window itself if it has no border. <see cref="System.Int32"/> </param>
		/// <param name="y"> The new y coordinate, which defines the new location of the top-left pixel of the window's border or the window itself if it has no border. <see cref="System.Int32"/> </param>
		/// <param name="width"> The new width, which is the interior dimensions of the window after the call completes. <see cref="System.UInt32"/> </param>
		/// <param name="height"> The new heigth, which is the interior dimensions of the window after the call completes. <see cref="System.UInt32"/> </param>
		[DllImport("libX11")]
		extern public static void XMoveResizeWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height);
	
		// Tested: O.K.
		/// <summary> Change the parent window of the specified window. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to resize. <see cref="IntPtr"/> </param>
		/// <param name="x"> The new x coordinate, which defines the new location of the top-left pixel of the window inside the new parent window. <see cref="System.Int32"/> </param>
		/// <param name="y"> The new y coordinate, which defines the new location of the top-left pixel of the window inside the new parent window. <see cref="System.Int32"/> </param>
		/// <remarks> If the specified window is mapped, XReparentWindow() automatically performs an UnmapWindow request on it,
		///   removes it from its current position in the hierarchy, and inserts it as the child of the specified parent.
		///   The window is placed in the stacking order on top with respect to sibling windows.
		/// * After reparenting the specified window, XReparentWindow() causes the X server to generate a ReparentNotify event.
		///   The override_redirect member returned in this event is set to the window's corresponding attribute. Window manager
		///   clients usually should ignore this window if this member is set to True. Finally, if the specified window was
		///   originally mapped, the X server automatically performs a MapWindow request on it.
		/// * The X server performs normal exposure processing on formerly obscured windows. The X server might not generate Expose
		///   events for regions from the initial UnmapWindow request that are immediately obscured by the final MapWindow request. </remarks>
		[DllImport("libX11")]
		extern public static void XReparentWindow (IntPtr x11display, IntPtr x11window, IntPtr x11parentWindow, TInt x, TInt y);
		
		// Tested: O.K.
		/// <summary> Map the window and all of its subwindows that have had map requests to a display. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to map. <see cref="System.IntPtr"/> </param>
		/// <remarks>Take care for a window width/height grater than 0!</remarks>
		[DllImport("libX11")]
		extern public static void XMapWindow (IntPtr x11display, IntPtr x11window);

		// Tested: O.K.
		/// <summary> The XMapRaised() function essentially is similar to XMapWindow() in that it maps the window and all of its subwindows that have
		/// had map requests. However, it also raises the specified window to the top of the stack. For additional information, see XMapWindow().</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to map. <see cref="IntPtr"/> </param>
		/// <remarks>To activate a shell window (e.g. a XrwTransientShell) send _NET_ACTIVE_WINDOW event to root window.</remarks>
		[DllImport("libX11")]
		extern public static void XMapRaised(IntPtr x11display, IntPtr x11window);
		
		/// <summary> Unmap the specified window and causes the X server to generate an UnmapNotify event. If the specified window is already unmapped,
		/// XUnmapWindow () has no effect. Normal exposure processing on formerly obscured windows is performed. Any child window will no longer be visible
		/// until another map call is made on the parent. In other words, the subwindows are still mapped but are not visible until the parent is mapped.
		/// Unmapping a window will generate Expose events on windows that were formerly obscured by it. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to unmap. <see cref="System.IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XUnmapWindow (IntPtr x11display, IntPtr x11window);

		// Tested: O.K.
		/// <summary>The XRaiseWindow() function raises the specified window to the top of the stack so that no sibling window obscures it.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11window">The window to raise.<see cref="IntPtr"/></param>
		/// <remarks>Raising a mapped window may generate Expose events for the window and any mapped subwindows that were formerly obscured.</remarks>
		[DllImport("libX11")]
		extern public static void XRaiseWindow(IntPtr x11display, IntPtr x11window);
		
		// Tested: O.K.
		/// <summary>The XRaiseWindow() function circulates children of the specified window in the specified direction.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11window">The window who's children are to circulate.<see cref="IntPtr"/></param>
		/// <param name="direction">Specifies the direction (up or down) to circulate the child window. It can be passed RaiseLowest (0) or LowerHighest (1).<see cref="TInt"/></param>
		/// <remarks>To activate a shell window (e.g. a XrwTransientShell) send _NET_ACTIVE_WINDOW event to root window.</remarks>
		[DllImport("libX11")]
		extern public static void XCirculateSubwindows(IntPtr x11display, IntPtr x11window, TInt direction);

		// Tested: O.K.
		/// <summary> Create an unmapped subwindow for a specified parent window, returns the window ID of the created window, and causes the
		/// X server to generate a CreateNotify event.  The created window is placed on top in the stacking order with respect to siblings. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The parent window. <see cref="IntPtr"/> </param>
		/// <param name="x"> Tthe x coordinate, which is the top-left outside corner of the window's borders and relative to the inside of the parent window's borders. <see cref="X11.TInt"/> </param>
		/// <param name="x"> Tthe y coordinate, which is the top-left outside corner of the window's borders and relative to the inside of the parent window's borders. <see cref="X11.TInt"/> </param>
		/// <param name="width"> The width, which is the created window's inside dimensions and do not include the created window's borders. <see cref="X11.TUint"/> </param>
		/// <param name="height"> The height, which is the created window's inside dimensions and do not include the created window's borders. <see cref="X11.TUint"/> </param>
		/// <param name="outsideBorderWidth"> The outside (surrounding) border width of the created window in pixels. <see cref="X11.TInt"/> </param>
		/// <param name="border"> The border pixel value of the window. <see cref="TPixel"/> </param>
		/// <param name="background"> The background pixel value of the window. <see cref="TPixel"/> </param>
		/// <returns> The window ID of the created window on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XCreateSimpleWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height,
		                                                 TUint outsideBorderWidth, TPixel border, TPixel background);
		
		// Tested: O.K.
		/// <summary>Change the window attributes in the XSetWindowAttributes.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11window">The parent window.<see cref="IntPtr"/></param>
		/// <param name="valueMask">Specifies which window attributes are defined in the attributes argument. This mask is the bitwise
		/// inclusive OR of the valid attribute mask bits. If valuemask is zero, the attributes are ignored and are not referenced.
		/// The values and restrictions are the same as for XCreateWindow().<see cref="WindowAttributeMask"/></param>
		/// <param name="attributes">Specifies the structure from which the attribute values (as specified by the value mask)
		/// are to be taken.<see cref="XSetWindowAttributes"/></param>
		[DllImport("libX11")]
		extern public static void XChangeWindowAttributes (IntPtr x11display, IntPtr x11window, WindowAttributeMask valueMask, ref XSetWindowAttributes attributes);
		
		// Tested: O.K.
		/// <summary> Create an unmapped subwindow for a specified parent window, returns the window ID of the created window, and causes the
		/// X server to generate a CreateNotify event.  The created window is placed on top in the stacking order with respect to siblings. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The parent window. <see cref="IntPtr"/> </param>
		/// <param name="x"> Tthe x coordinate, which is the top-left outside corner of the window's borders and relative to the inside of the parent window's borders. <see cref="X11.TInt"/> </param>
		/// <param name="x"> Tthe y coordinate, which is the top-left outside corner of the window's borders and relative to the inside of the parent window's borders. <see cref="X11.TInt"/> </param>
		/// <param name="width"> The width, which is the created window's inside dimensions and do not include the created window's borders. <see cref="X11.TUint"/> </param>
		/// <param name="height"> The height, which is the created window's inside dimensions and do not include the created window's borders. <see cref="X11.TUint"/> </param>
		/// <param name="outsideBorderWidth"> The outside (surrounding) border width of the created window in pixels. <see cref="X11.TInt"/> </param>
		/// <param name="depth"> The window's depth.  A depth of CopyFromParent means the depth is taken from the parent. <see cref="TInt"/> </param>
		/// <param name="cls"> The created window's class.  You can pass InputOutput, InputOnly, or CopyFromParent.
		/// A class of CopyFromParent means the class is taken from the parent. <see cref="TUint"/> </param>
		/// <param name="x11visual"> The visual type.  A visual of CopyFromParent means the visual type is taken from the parent. <see cref="IntPtr"/> </param>
		/// <param name="valueMask"> Specifies which window attributes are defined in the attributes argument.  This mask is the bitwise inclusive OR of the valid
		/// attribute mask bits. If valuemask is zero, the attributes are ignored and are not referenced. <see cref="WindowAttributeMask"/> </param>
		/// <param name="attributes"> The structure from which the values (as specified by the value mask) are to be taken. The value mask should have the
		/// appropriate bits set to indicate which attributes have been set in the structure. <see cref="XSetWindowAttributes"/> </param>
		/// <returns> The window ID of the created window on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XCreateWindow (IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height, TUint outsideBorderWidth,
		                                           TInt depth, TUint cls, IntPtr x11visual, WindowAttributeMask valueMask, ref XSetWindowAttributes attributes);

		// Tested: O.K.
		/// <summary>Return the atom identifier associated with the specified atom_name string.
		/// If onlyIfExists is False, the atom is created if it does not exist. Therefore, XInternAtom() can return IntPtr.Zero.
		/// If the atom name is not in the Host Portable Character Encoding, the result is implementation dependent.
		/// Uppercase and lowercase matter; the strings "thing", "Thing", and "thinG" all designate different atoms.
		/// The atom will remain defined even after the client's connection closes. It will become undefined only when the last
		/// connection to the X server closes.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="atomName">The name of the atom to return.<see cref="System.SByte[]"/></param>
		/// <param name="onlyIfExists">Specifies a boolean value that indicates whether the atom must be created, if it doesn't exist.<see cref="System.Boolean"/></param>
		/// <returns>A pointer to the atom on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		/// <remarks>Define the atomName including terminating NULL like: X11Utils.StringToSByteArray ("WM_DELETE_WINDOW\0")</remarks>
		[DllImport("libX11")]
		extern public static IntPtr XInternAtom (IntPtr x11display, X11.TChar[] atomName, bool onlyIfExists);

		// Tested: O.K.
		/// <summary>Return the atom identifier associated with the specified atomName string.
		/// If onlyIfExists is False, the atom is created if it does not exist. Therefore, XInternAtom() can return IntPtr.Zero.
		/// If the atom name is not in the Host Portable Character Encoding, the result is implementation dependent.
		/// Uppercase and lowercase matter; the strings "thing", "Thing", and "thinG" all designate different atoms.
		/// The atom will remain defined even after the client's connection closes. It will become undefined only when the last
		/// connection to the X server closes.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="atomName">The name of the atom to return.<see cref="System.String"/></param>
		/// <param name="onlyIfExists">Specifies a boolean value that indicates whether the atom must be created, if it doesn't exist.<see cref="System.Boolean"/></param>
		/// <returns>A pointer to the atom on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		[DllImport("libX11")]
		extern public static IntPtr XInternAtom (IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string atomName, bool onlyIfExists);
		
		// Tested: O.K.
		/// <summary> Get the name associated with the specified atom. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="atom"> The atom for the property name to return. <see cref="IntPtr"/> </param>
		/// <returns> The name associated with the specified atom. <see cref="IntPtr"/> </returns>
		/// <remarks> To free the resulting string, call XFree(). </remarks>
		[DllImport("libX11")]
		extern public static IntPtr XGetAtomName(IntPtr x11display, IntPtr atom);
		
		// Tested: O.K.
		/// <summary> Replace the WM_PROTOCOLS property on the specified window with the list of atoms specified by the protocols argument.
		/// If the property does not already exist, XSetWMProtocols() sets the WM_PROTOCOLS property on the specified window to the list of
		/// atoms specified by the protocols argument. The property is stored with a type of ATOM and a format of 32.
		/// If it cannot intern the WM_PROTOCOLS atom, XSetWMProtocols() returns a zero status. Otherwise, it returns a nonzero status. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to replace the the WM_PROTOCOLS property. <see cref="IntPtr"/> </param>
		/// <param name="protocols"> Thelist of protocols as atom pointer. <see cref="IntPtr"/> </param>
		/// <param name="count"> The number of protocols in the list. <see cref="X11.TInt"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="X11.TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XSetWMProtocols(IntPtr x11display, IntPtr x11window, ref IntPtr protocols, TInt count);
		
		/// <summary> Flush the output buffer. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XFlush (IntPtr x11display);
		
		// Tested: O.K.
		/// <summary> Assign the indicated name to the specified window. A window manager can display the window name in some prominent place,
		/// such as the title bar, to allow users to identify windows easily. Some window managers may display a window's name in the window's
		/// icon, although they are encouraged to use the window's icon name if one is provided by the application. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to set the name for. <see cref="IntPtr"/> </param>
		/// <param name="windowName"> The name to set for the window. <see cref="System.SByte[]"/> </param>
		[DllImport("libX11")]
		extern public static void XStoreName(IntPtr x11display, IntPtr x11window, sbyte[] windowName);
		[DllImport("libX11")]
		extern public static void XStoreName(IntPtr x11display, IntPtr x11window, [MarshalAs(UnmanagedType.LPStr)] string windowName);
		
		/// <summary> Allocate and return a pointer to a XClassHint structure.
		/// Note that the pointer fields in the XClassHint structure are initially set to NULL.
		/// To free the memory allocated to this structure, use XFree(). </summary>
		/// <returns> The pointer to a XClassHint structure on success, or NULL otherwise (insufficient memory). <see cref="IntPtr"/> </returns>
		[DllImport("libX11", EntryPoint = "_XAllocClassHint")]
		extern private static IntPtr _XAllocClassHint ();
		
		// Tested: O.K.
		/// <summary> Allocate and return a pointer to a XClassHint structure.
		/// Note that the pointer fields in the XClassHint structure are initially set to NULL.
		/// To free the memory allocated to this structure, use XFree(). </summary>
		/// <param name="classHint"> The class hint to get. <see cref="XClassHint"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="System.Int32"/> </returns>
		public static int XAllocClassHint (out XClassHint classHint)
		{
			// IntPtr classHintPtr = _XAllocClassHint ();
			classHint = new XClassHint ();
			return 1;
		}
		
		// Tested: O.K.
		/// <summary> Free in-memory data that was created by an Xlib function. </summary>
		/// <param name="data"> The data that is to be freed. <see cref="System.IntPtr"/> </param>
		[DllImport("libX11", EntryPoint = "XFree")]
		extern public static void XFree (IntPtr data);
		
		// Tested: O.K.
		/// <summary> Return the class hint of the specified window to the members of the structure.
		/// If the data returned by the server is in the Latin Portable Character Encoding, then the returned
		/// strings are in the Host Portable Character Encoding. Otherwise, the result is implementation dependent.
		/// To free res_name and res_class when finished with the strings, use XFree() on each individually. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to get the class hint for. <see cref="System.IntPtr"/> </param>
		/// <param name="classHint"> The class hint to get. <see cref="X11._XClassHint"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="X11.TInt"/> </returns>
		[DllImport("libX11", EntryPoint = "XGetClassHint")]
		extern private static TInt _XGetClassHint (IntPtr x11display, IntPtr x11window, ref _XClassHint classHint);

		// Tested: O.K.
		/// <summary>Return a XClassHint structure of the specified window to the members of the structure.
		/// If the data returned by the server is in the Latin Portable Character Encoding, then the returned
		/// strings are in the Host Portable Character Encoding. Otherwise, the result is implementation dependent.</summary
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11window">The window to get the class hint for.<see cref="System.IntPtr"/></param>
		/// <param name="classHint">The class hint to get.<see cref="X11.XClassHint"/></param>
		/// <returns>Nonzero on success, or zero otherwise.<see cref="System.Int32"/></returns>
		public static int XGetClassHint (IntPtr x11display, IntPtr x11window, out XClassHint classHint)
		{
			_XClassHint _classHint = _XClassHint.Zero;
			
			if (_XGetClassHint (x11display, x11window, ref _classHint) == (TInt)0)
			{
				// Clean up unmanaged memory.
				// --------------------------
				// Typically: _classHint.res_name == IntPtr.Zero
				// Freeing a IntPtr.Zero is not prohibited.
				// Use first member (at offset 0) to free the structure itself.
				XFree (_classHint.res_name);
				
				classHint = XClassHint.Zero;
				return 0;
			}
			else
			{
				classHint = new XClassHint();
				// Marshal data from an unmanaged block of memory to a managed object.
				classHint.res_name  = (string)Marshal.PtrToStringAnsi (_classHint.res_name );
				classHint.res_class = (string)Marshal.PtrToStringAnsi (_classHint.res_class);
				
				// Clean up unmanaged memory.
				// --------------------------
				// Freeing a IntPtr.Zero is not prohibited.
				// First structure member (at offset 0) frees  the structure itself as well.
				XFree (_classHint.res_name);
				XFree (_classHint.res_class);
				
				return 1;
			}
		}
		
		// Tested: O.K.
		/// <summary> Set the class hint for the specified window.
		/// If the strings are not in the Host Portable Character Encoding, the result is implementation dependent.  </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the class hint for. <see cref="System.IntPtr"/> </param>
		/// <param name="classHint"> The class hint to set. <see cref="X11.XClassHint"/> </param>
		[DllImport("libX11")]
		extern public static void XSetClassHint (IntPtr x11display, IntPtr x11window, [MarshalAs(UnmanagedType.Struct)] ref XClassHint classHints);
		
		// Tested: O.K.
		/// <summary> Get the WM_TRANSIENT_FOR property for the specified window.  </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the WM_TRANSIENT_FOR property for. <see cref="System.IntPtr"/> </param>
		/// <param name="x11transientWindow"> The WM_TRANSIENT_FOR property of the specified window. <see cref="System.IntPtr"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="X11.TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XGetTransientForHint(IntPtr x11display, IntPtr x11window, ref IntPtr x11transientWindow);
			
		// Tested: O.K.
		/// <summary> Set the WM_TRANSIENT_FOR property for the specified window.  </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to get the WM_TRANSIENT_FOR property for. <see cref="System.IntPtr"/> </param>
		/// <param name="x11transientWindow"> The WM_TRANSIENT_FOR property of the specified window. <see cref="System.IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XSetTransientForHint(IntPtr x11display, IntPtr x11window, IntPtr x11transientWindow);
			
		// Tested: O.K.
		/// <summary> Get the window attributes for the specified window. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to get the window attributes for. <see cref="System.IntPtr"/> </param>
		/// <param name="classHint"> The window attributes to get. <see cref="X11.XWindowAttributes"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="X11.TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XGetWindowAttributes (IntPtr x11display, IntPtr x11window, [MarshalAs(UnmanagedType.Struct)] ref XWindowAttributes windowAttributes);
		
		// Tested: O.K.
		/// <summary> Takes the srcX and srcX coordinates relative to the source window's origin and returns these coordinates to destX and destY
		/// relative to the destination window's origin on success, or destX == 0 and destY == 0 otherwise. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11srcWindow"> The window the srcX and srcX coordinates are relative to the origin. <see cref="System.IntPtr"/> </param>
		/// <param name="x11destWindow"> The window the destX and destY coordinates are relative to the origin. <see cref="System.IntPtr"/> </param>
		/// <param name="srcX"> The x coordinate to translate within the source window. <see cref="TInt"/> </param>
		/// <param name="srcY"> The y coordinate to translate within the source window. <see cref="TInt"/> </param>
		/// <param name="destX"> The translated x coordinate within the destination window. <see cref="TInt"/> </param>
		/// <param name="destY"> The translated x coordinate within the destination window. <see cref="TInt"/> </param>
		/// <param name="childWindow"> The child window, if the coordinates are contained in a mapped child of the destination window. <see cref="IntPtr"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="X11.TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TBoolean XTranslateCoordinates(IntPtr x11display, IntPtr x11srcWindow, IntPtr x11destWindow, TInt srcX, TInt srcY, ref TInt destX, ref TInt destY, ref IntPtr childWindow);

		// Tested: O.K.
		/// <summary>Return the window, that owns the focus, and the current focus state.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window">Returns the focus window, 'PointerRoot', that is 1L, or 'None', that 0L. <see cref="System.IntPtr"/> </param>
		/// <param name="revertTo">Returns the current focus state (RevertToParent, RevertToPointerRoot, or RevertToNone).<see cref="X11lib.TRevertTo"/> </param>
		[DllImport("libX11")]
		extern public static void XGetInputFocus(IntPtr x11display, ref IntPtr x11window, ref TRevertTo revertTo);

		// Tested: O.K.
		/// <summary>Changes the input focus and the last-focus-change time.
		/// It has no effect if the specified time is earlier than the current last-focus-change time or is later than the current X server time.
		/// Otherwise, the last-focus-change time is set to the specified time (CurrentTime is replaced by the current X server time).
		/// XSetInputFocus() causes the X server to generate FocusIn and FocusOut events. </summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/></param>
		/// <param name="x11window">The window that shold get the focus, 'PointerRoot', that is 1L, or 'None', that 0L. <see cref="System.IntPtr"/></param>
		/// <param name="revertTo">Specifies where the input focus reverts to if the window becomes not viewable. Either RevertToParent, RevertToPointerRoot, or RevertToNone.<see cref="X11lib.TRevertTo"/></param>
		/// <param name="time">The time. Either a timestamp or 'CurrentTime', that is 0L. <see cref="TInt"/></param>
		[DllImport("libX11")]
		extern public static void XSetInputFocus(IntPtr x11display, IntPtr x11window, TRevertTo revertTo, TInt time);
		
		// Tested: O.K. (for one string)
        /// <summary> Set the specified text property to be of type STRING (format 8) with a value representing the concatenation of the specified list of
        /// null-separated character strings. An extra null byte (which is not included in the nitems member) is stored at the end of the val field of
        /// text property. The strings are assumed (without verification) to be in the STRING encoding. To free the storage for the val field, use XFree(). </summary>
        /// <param name="list"> The list of null-terminated character strings to set into the text property. <see cref="TChar[]"/> </param>
        /// <param name="count"> The number of strings to set to the text property. A <see cref="TInt"/> </param>
        /// <param name="textProperty"> The text property to set. <see cref="XTextProperty"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="X11.TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XStringListToTextProperty (ref TChar[] list, TInt count, ref XTextProperty textProperty);
		[DllImport("libX11")]
		extern public static TInt XStringListToTextProperty (ref TChar[] list, TInt count, IntPtr p);
		
		// Tested: O.K.
        /// <summary>Get the window manager name of a window (in other words the application window title).
        /// Convenience function calls XGetTextProperty() to get the WM_NAME property.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11window">The window to set the window manager name of a window for.<see cref="System.IntPtr"/> </param>
		/// <param name="textProperty">The text property holding the window manager name of a window to set.<see cref="XTextProperty"/></param>
		/// <returns>It returns a nonzero status on success, or it returns a zero status otherwise.<see cref="System.String"/></returns>
		[DllImport("libX11", EntryPoint = "XGetWMName")]
		extern public static TInt _XGetWMName(IntPtr x11display, IntPtr x11window, ref XTextProperty textProperty);
		[DllImport("libX11", EntryPoint = "XGetWMName")]
		extern public static TInt _XGetWMName(IntPtr x11display, IntPtr x11window, IntPtr p);
		
		// Tested: O.K.
        /// <summary> Get the window manager name of a window (in other words the application window title). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the window manager name of a window for. <see cref="System.IntPtr"/> </param>
		/// <param name="windowTitle"> The indow manager name of a window to get. <see cref="System.String"/> </param>
		public static string XGetWMName(IntPtr x11display, IntPtr x11window)
		{
			X11lib.XTextProperty titleNameProp = new X11lib.XTextProperty ();
			
			if (X11lib._XGetWMName(x11display, x11window, ref titleNameProp) == 0)
				return null;
			else
			{
				string title = null;
				if (titleNameProp.val == IntPtr.Zero || titleNameProp.format != (TInt)8 || titleNameProp.nitems > 0)
					title = Marshal.PtrToStringAuto (titleNameProp.val);

				X11lib.XFree (titleNameProp.val);
				titleNameProp.val = IntPtr.Zero;
				
				return title;
			}
		}
		
		// Tested: O.K.
        /// <summary> Set the window manager name of a window (in other words the application window title).
        /// Convenience function calls XSetTextProperty() to set the WM_NAME property. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the window manager name of a window for. <see cref="System.IntPtr"/> </param>
		/// <param name="textProperty"> The text property holding the window manager name of a window to set. <see cref="XTextProperty"/> </param>
		[DllImport("libX11", EntryPoint = "XSetWMName")]
		extern public static void _XSetWMName(IntPtr x11display, IntPtr x11window, ref XTextProperty textProperty);
		[DllImport("libX11", EntryPoint = "XSetWMName")]
		extern public static void _XSetWMName(IntPtr x11display, IntPtr x11window, IntPtr p);
			
		// Tested: O.K.
        /// <summary> Set the window manager name of a window (in other words the application window title). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the window manager name of a window for. <see cref="System.IntPtr"/> </param>
		/// <param name="windowTitle"> The indow manager name of a window to set. <see cref="System.String"/> </param>
		public static void XSetWMName(IntPtr x11display, IntPtr x11window, string windowTitle)
		{
			X11lib.XTextProperty titleNameProp = new X11lib.XTextProperty ();
			TChar[]              titleNameVal  = X11Utils.StringToSByteArray (windowTitle + "\0");
			
			// PROBLEM: Doesn't work on 64 bit!
			/*NEW*/IntPtr p = Marshal.AllocHGlobal (Marshal.SizeOf (titleNameProp));
			/*REPLACED*///if (X11lib.XStringListToTextProperty (ref titleNameVal, (TInt)1, ref titleNameProp) != (TInt)0)
			if (X11lib.XStringListToTextProperty (ref titleNameVal, (TInt)1, p) != (TInt)0)
			{
				/*NEW*/X11lib._XSetWMName (x11display, x11window, p);
				/*REPLACED*///X11lib._XSetWMName (x11display, x11window, ref titleNameProp);
				/*NEW*/titleNameProp = (X11lib.XTextProperty)Marshal.PtrToStructure (p, typeof(X11lib.XTextProperty));
				X11lib.XFree (titleNameProp.val);
				titleNameProp.val = IntPtr.Zero;
			}
			else
				SimpleLog.LogLine (TraceEventType.Error, "X11lib::XSetWMName () Can not set window name.");
		}
		
		// Tested: O.K.
        /// <summary> Set the window manager name of a window's icon (in other words the application icon title).
        /// Convenience function calls XSetTextProperty() to set the WM_ICON_NAME property. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the window manager name of a window's icon for. <see cref="System.IntPtr"/> </param>
		/// <param name="textProperty"> The text property holding the window manager name of a window's icon to set. <see cref="XTextProperty"/> </param>
		[DllImport("libX11", EntryPoint = "XSetWMIconName")]
		extern private static void _XSetWMIconName(IntPtr x11display, IntPtr x11window, ref XTextProperty textProperty);
		[DllImport("libX11", EntryPoint = "XSetWMIconName")]
		extern private static void _XSetWMIconName(IntPtr x11display, IntPtr x11window, IntPtr p);

		// Tested: O.K.
        /// <summary> Set the window manager name of a window's icon (in other words the application icon title). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the window manager name of a window's icon for. <see cref="System.IntPtr"/> </param>
		/// <param name="windowTitle"> The indow manager name of a window's icon to set. <see cref="System.String"/> </param>
		public static void XSetWMIconName(IntPtr x11display, IntPtr x11window, string windowIconTitle)
		{
			X11lib.XTextProperty iconNameProp  = new X11lib.XTextProperty ();
			TChar[]              iconNameVal   = X11Utils.StringToSByteArray (windowIconTitle + "\0");

			// PROBLEM: Doesn't work on 64 bit!
			/*NEW*/IntPtr p = Marshal.AllocHGlobal (Marshal.SizeOf (iconNameProp));
			if (X11lib.XStringListToTextProperty (ref iconNameVal, (TInt)1, p) != (TInt)0)
			/*REPLACED*///if (X11lib.XStringListToTextProperty (ref iconNameVal, (TInt)1, ref iconNameProp) != (TInt)0)
			{
				/*NEW*/X11lib._XSetWMIconName (x11display, x11window, p);
				/*REPLACED*///X11lib._XSetWMIconName (x11display, x11window, ref iconNameProp);
				/*NEW*/iconNameProp = (X11lib.XTextProperty)Marshal.PtrToStructure (p, typeof(X11lib.XTextProperty));
				X11lib.XFree (iconNameProp.val);
				iconNameProp.val = IntPtr.Zero;
			}
			else
				SimpleLog.LogLine (TraceEventType.Error, "X11lib::XSetWMIconName () Can not set window's icon name.");
		}
		
		// Tested: O.K.
		/// <summary> Allocate and return a pointer to a XWMHints structure. Note that all fields in the XWMHints structure are initially set to zero.
		/// If insufficient memory is available, XAllocWMHints() returns NULL. To free the memory allocated to this structure, use XFree(). </summary>
		/// <returns> The pointer to a XWMHints structure on success, or NULL otherwise. <see cref="XWMHints"/> </returns>
		/// <remarks> The (unmanaged) structure must be freed after usage. </remarks>
		[DllImport ("libX11", EntryPoint = "XAllocWMHints")]
        extern public static IntPtr _XAllocWMHints ();
        public static IntPtr XAllocWMHints (ref XWMHints wmHints)
		{	
			IntPtr   ptr = _XAllocWMHints ();
			Marshal.StructureToPtr (wmHints, ptr, false);
			
			return ptr;
		}
		
		/// <summary> Read the window manager hints and return NULL if no WM_HINTS property was set on the window or return a pointer to a XWMHints
		/// structure if it succeeds. When finished with the data, free the space used for it by calling XFree(). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to get the window manager hints for. <see cref="System.IntPtr"/> </param>
		/// <returns> The pointer to a XWMHints structure on success, or NULL if no WM_HINTS property was set on the window. <see cref="XWMHints"/> </returns>
		[DllImport ("libX11", EntryPoint = "XGetWMHints")]
        extern public static IntPtr _XGetWMHints (IntPtr x11display, IntPtr x11window);
        public static IntPtr XGetWMHints (IntPtr x11display, IntPtr x11window, ref XWMHints wmHints)
		{	
			IntPtr   ptr = _XGetWMHints (x11display, x11window);
			Marshal.StructureToPtr (wmHints, ptr, false);
			
			return ptr;
		}
		
		// Tested: O.K.
		/// <summary> Set the window manager hints that include icon information and location, the initial state of the window,
		/// and whether the application relies on the window manager to get keyboard input. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window to set the window manager hints for. <see cref="System.IntPtr"/> </param>
		/// <param name="wmHints"> The pointer to a XWMHints structure contining the values to apply. <see cref="XWMHints"/> </param>
		[DllImport ("libX11")]
        extern public static void XSetWMHints (IntPtr x11display, IntPtr x11window, ref XWMHints wmHints);
		
		// Tested: O.K.
		/// <summary> Flush the output buffer and then waits until all requests have been received and processed by the X server. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="discard"> The boolean value that indicates whether XSync() discards all events on the event queue. <see cref="TBoolean"/></param>
		/// <remarks> Any errors generated must be handled by the error handler. For each protocol error received by Xlib,
		/// XSync() calls the client application's error handling routine (see "Using the Default Error Handlers").
		/// Any events generated by the server are enqueued into the library's event queue. </remarks>
		[DllImport ("libX11")]
        extern public static void XSync (IntPtr x11display, TBoolean discard);
		
		/// <summary> Request that the X server report the events associated with the specified event mask. Initially, X will not report any of these events. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11window"> The window whose events you are interested in. <see cref="System.IntPtr"/> </param>
		/// <param name="eventMask"> Specifies the event mask. <see cref="X11lib.EventMask"/> </param>
		[DllImport("libX11")]
		extern public static void XSelectInput (IntPtr x11display, IntPtr x11window, EventMask eventMask);
		
		#endregion Window creation and manipulation methods
		
		// ##########################################################################################################
		// ###   E V E N T   P R O C E S S I N G
		// ##########################################################################################################
		
		#region Event processing methods
		
		// Tested: O.K.
		/// <summary>Return the number of events that have been received from the X server but have not been removed from the event queue.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <remarks>XPending() is identical to XEventsQueued() with the mode QueuedAfterFlush specified.</remarks>
		[DllImport("libX11")]
		public extern static TInt XPending (IntPtr x11display);
		
		// Tested: O.K.
		/// <summary>Return the number of events that have been received from the X server but have not been removed from the event queue.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <remarks>XPending() is identical to XEventsQueued() with the mode QueuedAlready specified.</remarks>
		[DllImport("libX11")]
		public extern static TInt XQLength (IntPtr x11display);
		
		// Tested: O.K.
		/// <summary> Copies the first event from the event queue into the specified XEvent structure and then removes it from the queue.
		/// If the event queue is empty, XNextEvent() flushes the output buffer and blocks until an event is received.  </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11event"> Returns the next event in the queue. <see cref="XEvent"/> </param>
		[DllImport("libX11")]
		public extern static void XNextEvent (IntPtr x11display, ref XEvent x11event);
		
		[DllImport("libX11")]
		public extern static TBoolean XWindowEvent (IntPtr x11display, IntPtr x11window, EventMask event_mask, ref XEvent x11event);
		
		// Tested: O.K.
		/// <summary> Search the event queue. The first event that matches the specified mask will be removed and copied into the specified
		/// eventReturn structure. This function never blocks and it returns a Bool indicating if the event was returned. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="eventMask"> The the event mask, specifying the events to observe. <see cref="X11lib.EventMask"/> </param>
		/// <param name="x11eventReturn"> Returns the matched event's associated structure. <see cref="IntPtr"/> </param>
		/// <returns> True, if an event was removed from the event queue and copied into the specified eventReturn structure, false otherwise. <see cref="TBoolean"/> </returns>
		[DllImport("libX11")]
		extern public static TBoolean XCheckMaskEvent (IntPtr x11display, EventMask eventMask, ref XEvent x11eventReturn);
		
		// Tested: O.K.
		/// <summary> Search the event queue and then the events available on the server connection for the first event that matches the specified
		/// window and event mask. This function never blocks and it returns a Bool indicating if the event was returned. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="eventMask"> The the event mask, specifying the events to observe. <see cref="X11lib.EventMask"/> </param>
		/// <param name="x11eventReturn"> Returns the matched event's associated structure. <see cref="IntPtr"/> </param>
		/// <returns>  If it finds a match, XCheckWindowEvent() removes that event, copies it into the specified XEvent structure, and returns True.
		/// The other events stored in the queue are not discarded. If the event you requested is not available, XCheckWindowEvent() returns False ,
		/// and the output buffer will have been flushed. <see cref="TBoolean"/> </returns>
		[DllImport("libX11")]
		//extern public static TBoolean XCheckWindowEvent (IntPtr x11display, IntPtr x11window, EventMask eventMask, out IntPtr eventReturn);
		public extern static TBoolean XCheckWindowEvent (IntPtr x11display, IntPtr x11window, EventMask event_mask, ref XEvent x11eventReturn);
		
		[DllImport("libX11")]
		public extern static TBoolean XCheckIfEvent (IntPtr x11display, ref XEvent x11event, IntPtr predProc, IntPtr data);
			
		[DllImport("libX11")]
		public extern static TBoolean XCheckTypedWindowEvent(IntPtr x11display, IntPtr x11window, XEventName event_type, ref XEvent x11event);

		[DllImport("libX11", EntryPoint = "XFilterEvent")]
		public extern static TBoolean XFilterEvent (ref XEvent xevent, IntPtr x11window);

		[DllImport("libX11")]
		public extern static void XPeekEvent (IntPtr x11display, ref XEvent x11event);
		
		// Tested: O.K.
		/// <summary> Identify the destination window, determine which clients should receive the specified event,
		/// ignore any active grabs and add the specified event to the output buffer. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The "destination window" the event is to be sent to,
		///   or "PointerWindow" (the window currently containing the mouse pointer),
		///   or "InputFocus" (the window that currently onwns the input focus). <see cref="IntPtr"/> </param>
		/// <param name="propagate"> Determine which clients should receive the specified event.
		///   * True or false with EMPTY eventMask, the event is sent to the client that created the "destination window".
		///     If that client no longer exists, no event is sent. <see cref="TBoolean"/> </param>
		/// <param name="eventMask"> The event mask, that must match any of the event types defined for the "destination window".
		///   * True with ANY eventMask, the event is sent to every client selecting on "destination window"
		///     any of the event types in the eventMask argument.
		///   * False with ANY eventMask and NO clients have selected the event, that is sent, on "destination window"
		///     the "destination window" is replaced with the closest ancestor of "destination window".
		/// <param name="eventSend"> The event that is to be sent (to be added to the output buffer). <see cref="XEvent"/> </param>
		/// <returns> Nonzero on success, or zero otherwise (if the conversion to wire protocol format failed). <see cref="X11.TInt"/> </returns>
        /// <remarks> MUST NOT BE CALLED ON A DIFFERENT THREAD! </remarks>
		[DllImport("libX11")]
		extern public static TInt XSendEvent (IntPtr x11display, IntPtr x11window, TBoolean propagate, TLong eventMask, ref XEvent eventSend);
		
		#endregion Event processing methods
		
		// ##########################################################################################################
		// ###   C L I P B O A R D
		// ##########################################################################################################
		
		#region Clipboard methods
		
		// Tested: O.K.
		/// <summary>Changes the owner and last-change time for the specified selection and has no effect if the specified time
		/// is earlier than the current last-change time of the specified selection or is later than the current X server time.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="atomSelection">The selection atom, that defines the selection buffer to use. Predefined are PRIMARY and SECONDARY.<see cref="IntPtr"/></param>
		/// <param name="x11window">The owner window, that specifies the widget that provides the data.<see cref="IntPtr"/></param>
		/// <param name="time">The time, this provision is made. It can be passed either a timestamp of the request triggering event or CurrentTime.<see cref="TUlong"/></param>
		/// <remarks>If the new owner (whether a widget or IntPtr.Zero) is not the same as the current owner of the selection
		/// and the current owner is not IntPtr.Zero, the current owner is sent a SelectionClear event.</remarks>
		/// <remarks>If the client that is the owner of a selection is later terminated (that is, its connection is closed) or
		/// if the owner window it has specified in the request is later destroyed,
		/// the owner of the selection automatically reverts to IntPtr.Zero, but the last-change time is not affected.</remarks>
		[DllImport ("libX11")]
		public extern static void XSetSelectionOwner (IntPtr x11display, IntPtr atomSelection, IntPtr x11window, TUlong time);
		
		// Tested: O.K.
		/// <summary>Retrive the window ID associated with the window that currently owns the specified selection.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="atomSelection">The selection atom, that defines the selection buffer to retrive the window ID for. Predefined are PRIMARY and SECONDARY.<see cref="IntPtr"/></param>
		/// <returns>The window ID associated with the window that currently owns the specified selection on success, or IntPtr.Zero otherwise.A <see cref="IntPtr"/></returns>
		[DllImport ("libX11")]
		public extern static IntPtr XGetSelectionOwner (IntPtr x11display, IntPtr atomSelection);
		
		public enum XChangePropertyMode : int
		{
			PropModeReplace	= 0,
			PropModePrepend	= 1,
			PropModeAppend	= 2
		}
		
		// Tested: O.K.
		/// <summary>Alter the property for the specified window and causes the X server to generate a PropertyNotify event on that window</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window">The requestor window, that specifies the widget that wants to have the property changed.<see cref="IntPtr"/></param>
		/// <param name="atomProperty">The property name, that identifies the data buffer for data transfer. It can also be passed IntPtr.Zero.<see cref="IntPtr"/></param>
		/// <param name="atomType">The atom identifier that defines the type of the property.<see cref="IntPtr"/></param>
		/// <param name="format">The format of the data, that should be viewed as a list of 8-bit, 16-bit, or 32-bit quantities. Possible values are 8, 16, and 32.<see cref="TInt"/></param>
		/// <param name="mode">Specifies the mode of the operation. You can pass PropModeReplace, PropModePrepend, or PropModeAppend.<see cref="TInt"/></param>
		/// <param name="data">The data in the specified format.<see cref="IntPtr"/></param>
		/// <param name="nElements">Specifies the number of elements of the specified data format.<see cref="TInt"/></param>
		[DllImport ("libX11")]
		public extern static void XChangeProperty (IntPtr x11display, IntPtr x11window, IntPtr atomProperty,
		                                           IntPtr atomType, TInt format, XChangePropertyMode mode, IntPtr data, TInt nElements);
		// Tested: O.K.
		/// <summary>Request that the specified selection be converted to the specified target type.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="atomSelection">The selection atom, that defines the selection buffer to use. Predefined are PRIMARY and SECONDARY.<see cref="IntPtr"/></param>
		/// <param name="atomTarget">The target atom, that defines the requested format.<see cref="IntPtr"/></param>
		/// <param name="atomProperty">The property name, that identifies the data buffer for data transfer. It can also be passed IntPtr.Zero.<see cref="IntPtr"/></param>
		/// <param name="requestor">The requestor window, that specifies the widget that requests the data.<see cref="IntPtr"/></param>
		/// <param name="time">The time, this request is made. It can be passed either a timestamp of the request triggering event or CurrentTime.<see cref="TUlong"/></param>
		[DllImport ("libX11")]
		public extern static void XConvertSelection (IntPtr x11display, IntPtr atomSelection, IntPtr atomTarget, IntPtr atomProperty, IntPtr requestor, TUlong time);

		// Tested: O.K.
		/// <summary>Obtain the actual type of the property; the actual format of the property; the number of 8-bit, 16-bit, or 32-bit items transferred;
		/// the number of bytes remaining to be read in the property; and a pointer to the data actually returned.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window, that specifies the widget whose property is to obtain. <see cref="System.IntPtr"/> </param>
		/// <param name="atomProperty">The property name, that identifies the data buffer for data transfer.<see cref="IntPtr"/></param>
		/// <param name="offset">The offset in the specified property (in 32-bit quantities) where the data is to be retrieved.<see cref="TLong"/></param>
		/// <param name="length">The length in 32-bit multiples of the data to be retrieved.<see cref="TLong"/></param>
		/// <param name="delete">A boolean value that determines whether the property is deleted.<see cref="System.Boolean"/></param>
		/// <param name="atomReqType">The atom identifier associated with the property type or AnyPropertyType.<see cref="IntPtr"/></param>
		/// <param name="atomActType">The atom identifier that defines the actual type of the property.<see cref="IntPtr"/></param>
		/// <param name="actFormat">The actual format of the property.<see cref="TInt"/></param>
		/// <param name="countItems">The actual number of 8-bit, 16-bit, or 32-bit items stored in the data.<see cref="TULong"/></param>
		/// <param name="countRemain">The number of bytes remaining to be read in the property if a partial read was performed.<see cref="TUlong"/></param>
		/// <param name="data">The data in the specified format.<see cref="IntPtr"/></param>
		/// <returns>Nonzero on success, or zero otherwise.<see cref="System.Int32"/></returns>
		[DllImport ("libX11")]
		public extern static TInt XGetWindowProperty(IntPtr x11display, IntPtr x11window, IntPtr atomProperty, TLong offset,
		                                             TLong length, bool delete, IntPtr atomReqType, ref IntPtr atomActType,
		                                             ref TInt actualFormat, ref TUlong countItems, ref TUlong countRemain,
		                                             ref IntPtr data);
			
		#endregion Clipboard methods
		
		// ##########################################################################################################
		// ###   C U R S O R   M A N I P U L A T I O N
		// ##########################################################################################################
		
		#region Cursor manitulation methods
		
		// Usage:
		// IntPtr cursor = Xlib.XCreateFontCursor (xdisplay, (uint)Gdk.CursorType.IronCross);
		// Xlib.XDefineCursor (xdisplay, xwindow, cursor);
		// Xlib.XUndefineCursor (xdisplay, xwindow);

		// Tested: O.K.
		/// <summary> X provides a set of standard cursor shapes in a special font named cursor.The shape argument specifies which glyph of the standard fonts to use. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="cursor_shape"> The glyph of the cursor font to set. <see cref="System.UInt32"/> </param>
		/// <returns> The cursor structure to use with XDefineCursor() on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport ("libX11")]
		public extern static IntPtr XCreateFontCursor (IntPtr x11display, CursorFontShape cursorFontShape);

		// Tested: O.K. on physical machines only.
		/// <summary> Set the indicated cursor. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to set the cursor for. <see cref="IntPtr"/> </param>
		/// <param name="cursor"> The cursor to set, or IntPtr.Zero to unset the current cursor. <see cref="IntPtr"/> </param>
		/// <remarks> !!! Requires subsequent call of XFlush to take effect !!! </remarks>
		/// <remarks> !!! Doesn't work reliable an virtual machines based on VM-Ware !!! </remarks>
		[DllImport ("libX11")]
		public extern static void XDefineCursor (IntPtr x11display, IntPtr x11window, IntPtr cursor);

		// Tested: O.K.
		/// <summary> Undoes the effect of a previous XDefineCursor() call for indicated window. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to set the cursor for. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XUndefineCursor(IntPtr x11display, IntPtr x11window);

		// Applies changes, bot not sure if it's O.K.
		// Usage:
		// Xlib.XRecolorCursor(xdisplay, cursor, ref wht, ref gry);
		[DllImport ("libX11")]
		internal extern static void XRecolorCursor (IntPtr x11display, IntPtr cursor, ref XColor foregroundColor, ref XColor backgroundColor);
		
		#endregion Cursor manitulation methods

		// ##########################################################################################################
		// ###   P O I N T E R   M A N I P U L A T I O N
		// ##########################################################################################################
		
		#region Pointer manipulation methods
		
		// Tested: O.K. on physical machines only.
		/// <summary> Move the pointer by the offsets (dest_x, dest_y) relative to the current position of the pointer. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="src_x11window"> Specify the source window or IntPtr.Zero. <see cref="IntPtr"/> </param>
		/// <param name="dest_x11window"> Specify the destination window or IntPtr.Zero. <see cref="IntPtr"/> </param>
		/// <param name="src_x"> X-coordinate to specify a rectangle in the source window. <see cref="System.Int32"/> </param>
		/// <param name="src_y"> X-coordinate to specify a rectangle in the source window. <see cref="System.Int32"/> </param>
		/// <param name="src_width"> Width to specify a rectangle in the source window. <see cref="System.UInt32"/> </param>
		/// <param name="src_height"> Height to specify a rectangle in the source window. <see cref="System.UInt32"/> </param>
		/// <param name="dest_x"> Specify the x-coordinates within the destination window. <see cref="System.Int32"/> </param>
		/// <param name="dest_y">Specify the y-coordinates within the destination window. <see cref="System.Int32"/> </param>
		/// <remarks> If src_x11window is IntPtr.Zero and dest_x11window is IntPtr.Zero, the pointer moves relative to the current position. </remarks>
		/// <remarks> If src_x11window is IntPtr.Zero and dest_x11window is a window, the pointer moves absolute to the window's origin. </remarks>
		/// <remarks> If src_x11window is a window, the pointer moves only within the src_x11window and if the specified rectangle contains the pointer. </remarks>
		/// <remarks> !!! Requires subsequent call of XFlush to take effect !!! </remarks>
		/// <remarks> !!! Doesn't work an virtual machines based on VM-Ware !!! </remarks>
		[DllImport ("libX11")]
		public extern static void XWarpPointer(IntPtr x11display, IntPtr src_x11window, IntPtr dest_x11window, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);
		
		// Tested: O.K.
		/// <summary> Return the root window, the pointer is logically on, and the pointer coordinates relative to the root window's origin. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The application's root window to query the pointer for. <see cref="System.IntPtr"/> </param>
		/// <param name="x11windowRootReturn"> The display's root window that the pointer is in (returned). <see cref="IntPtr"/> </param>
		/// <param name="x11windowChildReturn"> The widget's window that the pointer is in (returned) on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </param>
		/// <param name="rootReturnX"> The pointer's x-coordinate relative to the display's root window origin. <see cref="TInt"/> </param>
		/// <param name="rootReturnY"> The pointer's y-coordinate relative to the display's root window origin. <see cref="TInt"/> </param>
		/// <param name="winReturnX"> The pointer's x-coordinate relative to the widget's window origin on success, or 0 otherwise. <see cref="TInt"/> </param>
		/// <param name="winReturnY"> The pointer's y-coordinate relative to the widget's window origin on success, or 0 otherwise. <see cref="TInt"/> </param>
		/// <param name="maskReturn"> The current state of the modifier keys and pointer buttons. Use StateMask to compare. <see cref="TUint"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="TBoolean"/> </returns>
		/// <remarks> MUST NOT BE CALLED ON A DIFFERENT THREAD! </remarks>
		[DllImport ("libX11")]
		public extern static TBoolean XQueryPointer(IntPtr x11display, IntPtr x11window, ref IntPtr x11windowRootReturn, ref IntPtr x11windowChildReturn,
		                                            ref TInt rootReturnX, ref TInt rootReturnY, ref TInt winReturnX, ref TInt winReturnY, ref TUint maskReturn);
		
		
		/*
		#define GrabModeSync	0
		#define GrabModeAsync	1
		X11lib.XGrabPointer (_surface.Display, _surface.Window, (TBoolean)0, (TInt)EventMask.ButtonPressMask,
		                     (TInt)1, (TInt)1, this.ApplicationShell.Surface.Window, IntPtr.Zero, (TInt)0);
		*/
		/// <remarks>To activate a shell window (e.g. a XrwTransientShell) send _NET_ACTIVE_WINDOW event to root window.</remarks>
		[DllImport ("libX11")]
		public extern static TInt XGrabPointer (IntPtr x11display, IntPtr x11window, TBoolean ownerEvents,
		                                        TInt eventMask, TInt pointerMode, TInt keyboardMode,
		                                        IntPtr confineTo, IntPtr cursor, TInt time);
		// Dangerous to Use!!! Can freeze the application!!!
		// X11lib.XtGrabKeyboard (_surface.Display, _surface.Window, (TBoolean)0, (TInt)1, (TInt)1, (TInt)0);
		[DllImport ("libX11")]
		public extern static TInt XtGrabKeyboard (IntPtr x11display, IntPtr x11window, TBoolean ownerEvents, TInt pointerMode, TInt keyboardMode, TInt time);
		
		#endregion Pointer manipulation methods
		
		#region

		// Tested: O.K.
        [DllImport ("libX11")]
        extern public static void XFreeStringList (IntPtr ptr);

		#endregion
		
		// ##########################################################################################################
		// ###   D I S P L A Y ,   S C R E E N   A N D   V I S U A L
		// ##########################################################################################################
		
		#region Display, screen and visual methods
		
		// Tested: O.K.
		/// <summary>Return the number of available screens.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <returns>The number of available screens.<see cref="System.Int32"/></returns>
		[DllImport("libX11")]
		extern public static int XScreenCount(IntPtr x11display);

		// Tested: O.K.
		/// <summary>Return the default screen number referenced by the XOpenDisplay() function.
		/// This macro or function should be used to retrieve the screen number in applications that will use only a single screen.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <returns>The default screen number.<see cref="System.Int32"/></returns>
		[DllImport("libX11")]
		extern public static X11.TInt XDefaultScreen(IntPtr x11display);
		
		// Tested: O.K.
		/// <summary> Return the connection number for the specified display.
		/// On a POSIX-conformant system, this is the file descriptor of the connection. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <returns> The connection number. <see cref="System.Int32"/> </returns>
		[DllImport("libX11")]
		extern public static int XConnectionNumber (IntPtr x11display);
		
		// Tested: O.K.
		/// <summary> Return a pointer to the indicated screen. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The pointer to the indicated screen on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XScreenOfDisplay (IntPtr x11display, TInt screenNumber);

		// Tested: O.K.
		/// <summary> Return the default visual type for the specified screen. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The pointer to the default visual type on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XDefaultVisual (IntPtr x11display, TInt screenNumber);

		// Tested: O.K.
		/// <summary> Return  the visual information for a visual that matches the specified depth and class for a screen.
		/// Because multiple visuals that match the specified depth and class can exist, the exact visual chosen is undefined. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <param name="depth"> The requested depth of the screen. <see cref="TInt"/> </param>
		/// <param name="screenClass"> The requested class of the screen. <see cref="TInt"/> </param>
		/// <param name="visualInfo"> The matching visual information. <see cref="XVisualInfo"/> </param>
		/// <returns> The requested default depth. <see cref="TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XMatchVisualInfo (IntPtr x11display, TInt screenNumber, TInt depth, TInt screenClass, ref XVisualInfo visualInfo);
		
		/// <summary> Get an array of visual structures that have attributes equal to the attributes specified by vinfoTemplate. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="vinfoMask"> The mask specifying, what member(s) of vinfoTemplate should determine the requested visual structures. <see cref="TLong"/> </param>
		/// <param name="vinfoTemplate"> The visual attributes that are to be used in matching the requested visual structures. <see cref="XVisualInfo"/> </param>
		/// <param name="countVisualsReturn"> The number of matching visual structures. <see cref="TInt"/> </param>
		/// <returns> The array of visual structures that mmet the specified conditions on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XGetVisualInfo(IntPtr x11display, TLong vinfoMask, ref XVisualInfo vinfoTemplate, ref TInt countVisualsReturn);

		// Tested: O.K.
		/// <summary> Return the depth (number of planes) of the default root window for the specified screen.
		/// Other depths may also be supported on this screen (see .PN XMatchVisualInfo ). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The requested default depth. <see cref="TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XDefaultDepth (IntPtr x11display, TInt screenNumber);
		
		#endregion Display, screen and visual methods
		
		// ##########################################################################################################
		// ###   C O L O R M A P   A N D   C O L O R
		// ##########################################################################################################
		
		#region Colormap and color methods
			
		// Tested: O.K.
		/// <summary> Permanently allocated default colormap entry to convey the expected relative intensity of the color. Can be used in implementing a monochrome application. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The black pixel value. Only black and white pixel are guaranteed for all XServer. <see cref="TPixel"/> </returns>
		[DllImport("libX11")]
		extern public static TPixel XBlackPixel (IntPtr x11display, X11.TInt screenNumber);
		
		// Tested: O.K.
		/// <summary> Permanently allocated default colormap entry to convey the expected relative intensity of the color. Can be used in implementing a monochrome application. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The white pixel value. Only black and white pixel are guaranteed for all XServer. <see cref="TPixel"/> </returns>
		[DllImport("libX11")]
		extern public static TPixel XWhitePixel (IntPtr x11display, X11.TInt screenNumber);
		
		// Tested: O.K.
		/// <summary> Return the black pixel value of the specified screen. </summary>
		/// <param name="x11screen"> The screen pointer, that specifies  the appropriate screen structure. <see cref="IntPtr"/> </param>
		/// <returns> The black pixel value. Only black and white pixel are guaranteed for all XServer. <see cref="TPixel"/> </returns>
		[DllImport("X11")]
		extern public static TPixel XBlackPixelOfScreen(IntPtr x11screen);

		// Tested: O.K.
		/// <summary> Return the white pixel value of the specified screen. </summary>
		/// <param name="x11screen"> The screen pointer, that specifies  the appropriate screen structure. <see cref="IntPtr"/> </param>
		/// <returns> The white pixel value. Only black and white pixel are guaranteed for all XServer. <see cref="TPixel"/> </returns>
		[DllImport("X11")]
		extern public static TPixel XWhitePixelOfScreen(IntPtr x11screen);
		
		[DllImport ("libX11")]
		extern public static TInt XMaxCmapsOfScreen(IntPtr x11screen);

		// Tested: O.K.
		/// <summary> Return the default colormap for allocation on the specified screen.
		/// Most routine allocations of color should be made out of this colormap. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="screenNumber"> The screen number, that specifies the appropriate screen on the X server. <see cref="TInt"/> </param>
		/// <returns> The requested default colormap on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport ("libX11")]
		extern public static IntPtr XDefaultColormap (IntPtr x11display, X11.TInt screenNumber);
		
		// Tested: O.K.
		/// <summary> Obtain a list of the currently installed colormaps for a given windowm. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to get the installed colormaps for. <see cref="IntPtr"/> </param>
		/// <param name="numReturn"> The number of installed colormaps. <see cref="System.Int32"/> </param>
		/// <returns> The array of installed colormap IDs. <see cref="IntPtr"/> </returns>
		[DllImport ("libX11")]
		extern public static IntPtr XListInstalledColormaps(IntPtr x11display, IntPtr x11window, ref int numReturn);
		
		// Tested: O.K.
		[DllImport ("libX11", EntryPoint = "XCreateColormap")]
		extern private static IntPtr _XCreateColormap(IntPtr x11display, IntPtr x11window, IntPtr x11visual, TInt colormapAllocType);
		
		// Tested: O.K.
		/// <summary> Create a colormap of the specified visual type for the screen on which the specified window resides and returns the
		/// colormap ID associated with it. Note that the specified window is only used to determine the screen. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window on whose screen the colormap has to be created. A <see cref="IntPtr"/> </param>
		/// <param name="x11visual"> The visual type supported on the screen.
		/// If the visual type is not one supported by the screen, a BadMatch error results. <see cref="IntPtr"/> </param>
		/// <param name="alloc"> The colormap entries to be allocated. AllocNone or AllocAll can be passed. <see cref="IntPtr"/> </returns>
		/// <returns> The colormap ID on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		public static IntPtr XCreateColormap(IntPtr x11display, IntPtr x11window, IntPtr x11visual, ColormapAllocType colormapAllocType)
		{
			return _XCreateColormap(x11display, x11window, x11visual, (TInt) colormapAllocType);
		}
			
		/// <summary> Install the specified colormap for its associated screen. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="colormap"> The colormap to associate to a screen. <see cref="IntPtr"/> </param>
		/// <remarks> All windows associated with this colormap immediately display with true colors.
		/// To associate a windos with this colormap during creation use XCreateWindow, XCreateSimpleWindow or
		/// assign the colormap later using XChangeWindowAttributes, or XSetWindowColormap. </remarks>
		[DllImport ("libX11")]
		extern public static void XInstallColormap (IntPtr x11display, IntPtr colormap);
		
		/// <summary> Removes the specified colormap from the required list for its screen. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="colormap"> The colormap to remove from the screen. <see cref="IntPtr"/> </param>
		/// <remarks> As a result, the specified colormap might be uninstalled, and the X server might implicitly install
		///   or uninstall additional colormaps. Which colormaps get installed or uninstalled is server-dependent except
		///   that the required list must remain installed.
		/// If the specified colormap becomes uninstalled, the X server generates a ColormapNotify event on each window that
		///   has that colormap. In addition, for every other colormap that is installed or uninstalled as a result of a call
		///   to XUninstallColormap(), the X server generates a ColormapNotify event on each window that has that colormap.</remarks>
		[DllImport ("libX11")]
		extern public static void XUninstallColormap(IntPtr x11display, IntPtr colormap);
		
		[DllImport ("libX11")]
		extern public static void XFreeColormap(IntPtr x11display, IntPtr colormap);
		
		[DllImport ("libX11")]
		extern public static void XSetWindowColormap(IntPtr x11display, IntPtr x11window, IntPtr colormap);
		
		// Tested: O.K.
		/// <summary>Look up the string name of a color with respect to the screen associated with the specified colormap.
		/// It returns the exact color value.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11colormap">The colormap to use.<see cref="IntPtr"/></param>
		/// <param name="colorname">The color's name to look up.<see cref="System.String"/></param>
		/// <param name="x11color">The exact color.<see cref="XColor"/></param>
		/// <returns>Nonzero if the name is resolved, or it returns zero otherwise.<see cref="TInt"/></returns>
		[DllImport("libX11")]
		extern public static TInt XParseColor (IntPtr x11display, IntPtr x11colormap, [MarshalAs(UnmanagedType.LPStr)] string colorname, ref XColor x11color);
		
		// Tested: O.K.
		/// <summary>Look up the string name of a color with respect to the screen associated with the specified colormap.
		/// It returns both the exact color values and the closest values provided by the screen with respect to the visual type of the specified colormap.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11colormap">The colormap to use.<see cref="IntPtr"/></param>
		/// <param name="colorname">The color's name to look up.<see cref="System.String"/></param>
		/// <param name="x11colorExactDef">The exact color.<see cref="XColor"/></param>
		/// <param name="x11colorScrDef">The nearest color provided by the hardware.<see cref="XColor"/></param>
		/// <returns>Nonzero if the name is resolved, or it returns zero otherwise.<see cref="System.Int32"/></returns>
		[DllImport("libX11")]
		extern public static TInt XLookupColor (IntPtr x11display, IntPtr x11colormap, [MarshalAs(UnmanagedType.LPStr)] string colorname, ref XColor x11colorExactDef, ref XColor x11colorScrDef);
		
		// Tested: O.K.
		/// <summary>Return the current RGB value for the pixel in the XColor structure and sets the DoRed, DoGreen, and DoBlue flags.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="colormap">The colormap to use for pixel value decomposition (to RGB values).<see cref="IntPtr"/></param>
		/// <param name="xcolor">The color to decomposite (to RGB values).<see cref="XColor"/></param>
		/// <returns>Nonzero if the name is resolved, or it returns zero otherwise.<see cref="System.Int32"/></returns>
		[DllImport("libX11")]
		extern public static TInt XQueryColor (IntPtr x11display, IntPtr x11colormap, ref XColor x11color);
		
		// Tested: O.K.
		/// <summary> Allocate a read-only colormap entry corresponding to the closest RGB value supported by the hardware.
		/// XAllocColor() returns the pixel value of the color closest to the specified RGB elements supported by the hardware
		/// and returns the RGB value actually used. The corresponding colormap cell is read-only.
		/// Multiple clients that request the same effective RGB value can be assigned the same read-only entry,
		/// thus allowing entries to be shared. When the last client deallocates a shared cell, it is deallocated.
		/// XAllocColor() does not use or affect the flags in the XColor structure. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11colormap"> The colormap to use for nearest read-only color cell allocation. <see cref="IntPtr"/> </param>
		/// <param name="x11color"> The color to allocate the nearest read-only color cell for. <see cref="XColor"/> </param>
		/// <returns> Nonzero on success, or zero otherwise. <see cref="System.Int32"/> </returns>
		/// <remarks> !!!USE XAllocExactNamedColor() or XAllocExactNamedColor() FOR APPLICATIONS!!! </remarks>
		[DllImport("libX11")]
		extern private static TInt XAllocColor (IntPtr x11display, IntPtr x11colormap, ref XColor x11color);
		
		// Tested: O.K.
		/// <summary>Look up the string name of a color with respect to the screen associated with the specified colormap.
		/// It returns both the exact color values and the closest values provided by the screen with respect to the visual
		/// type of the specified colormap. The pixel value is returned in x11colorScrDef. </summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11colormap">The colormap to use.<see cref="IntPtr"/></param>
		/// <param name="colorname">The color's name to look up.<see cref="System.String"/></param>
		/// <param name="x11colorExactDef">The exact color.<see cref="XColor"/></param>
		/// <param name="x11colorScrDef">The nearest color provided by the hardware.<see cref="XColor"/></param>
		/// <returns>Nonzero if the name is resolved, or it returns zero otherwise.<see cref="System.Int32"/></returns>
		[DllImport("libX11")]
		extern private static TInt XAllocNamedColor (IntPtr x11display, IntPtr x11colormap, [MarshalAs(UnmanagedType.LPStr)] string colorname, ref XColor x11colorScrDef, ref XColor x11colorExactDef);
		
		/// <summary> Free the cells represented by pixels whose values are in the pixels array. The planes argument should
		/// not have any bits set to 1 in common with any of the pixels. The set of all pixels is produced by ORing
		/// together subsets of the planes argument with the pixels. The request frees all of these pixels that were allocated
		/// by the client (using XAllocColor(), XAllocNamedColor(), XAllocColorCells(), and XAllocColorPlanes()). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="colormap"> The colormap to use for color cell deallocation. <see cref="IntPtr"/> </param>
		/// <param name="pixels"> The array of pixel values that map to the cells in the specified colormap to free. <see cref="TPixel[]"/> </param>
		/// <param name="nPixels"> The number of colors to free. <see cref="TInt"/> </param>
		/// <param name="planes"> The planes to free. <see cref="TUlong"/> </param>
		[DllImport("libX11")]
		extern public static void XFreeColors (IntPtr x11display, IntPtr colormap, TPixel[] pixels, TInt nPixels, TUlong planes);
		
		[DllImport("libX11")]
		extern public static TInt XAllocColorCells (IntPtr x11display, IntPtr colormap, TBoolean contig, ref TUlong[] planeMasksReturn, TUint nplanes, 
												   ref TUlong[] pixelsReturn, TUint npixels);
		
		// Tested: O.K.
		/// <summary> Allocate a read-only colormap entry corresponding to the exact RGB value.
		/// XAllocExactColor() returns the pixel value of the exact color specified. The corresponding colormap cell is read-only.
		/// Multiple clients that request the same effective RGB value can be assigned the same read-only entry,
		/// thus allowing entries to be shared. When the last client deallocates a shared cell, it is deallocated. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11colormap"> The colormap to use. <see cref="IntPtr"/> </param>
		/// <param name="colorname"> The color's name to look up. <see cref="System.String"/> </param>
		/// <returns> The pixel value of the requested color on success, or 0 as fallback. <see cref="TColor"/> </returns>
		public static TColor XAllocExactNamedColor (IntPtr x11display, IntPtr x11colormap, string colorname)
		{
			if (x11display == IntPtr.Zero)
				return TColor.FallbackWhite;

			X11lib.XColor tmp = new X11lib.XColor ();
			X11lib.XParseColor (x11display, x11colormap, colorname, ref tmp);
			X11lib.XAllocColor (x11display, x11colormap, ref tmp);
			
			TColor color = TColor.FromPRGB (tmp.pixel, (byte)tmp.red, (byte)tmp.green, (byte)tmp.blue);
			return color;
		}
		
		// Tested: O.K.
		/// <summary> Allocate a read-only colormap entry corresponding to the closest RGB value supported by the hardware.
		/// XAllocExactColor() returns the pixel value of the closest color. The corresponding colormap cell is read-only.
		/// Multiple clients that request the same effective RGB value can be assigned the same read-only entry,
		/// thus allowing entries to be shared. When the last client deallocates a shared cell, it is deallocated. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11colormap"> The colormap to use. <see cref="IntPtr"/> </param>
		/// <param name="colorname"> The color's name to look up. <see cref="System.String"/> </param>
		/// <returns> The pixel value of the requested color on success, or 0 as fallback. <see cref="TColor"/> </returns>
		public static TColor XAllocClosestNamedColor (IntPtr x11display, IntPtr x11colormap, string colorname)
		{
			if (x11display == IntPtr.Zero)
				return TColor.FallbackBlack;
			if (string.IsNullOrEmpty (colorname))
				return TColor.FallbackBlack;

			string capitalizedColorname = colorname.ToUpper();
			if (capitalizedColorname == "#FFFFFF")
				colorname = "white";
			if (capitalizedColorname == "#000000")
				colorname = "black";
			X11lib.XColor exactColor   = new X11lib.XColor ();	// The exact color database definition, matching the given color name.
			X11lib.XColor closestColor = new X11lib.XColor ();	// The nearest colormap color, matching the given color name.
			X11lib.XAllocNamedColor (x11display, x11colormap, colorname, ref exactColor, ref closestColor);
			
/*			if (colorname.StartsWith ("#"))
			{
				System.Drawing.Color requestedColor = System.Drawing.ColorTranslator.FromHtml (colorname);
				return new TColor (closestColor.pixel, requestedColor.R, requestedColor.G, requestedColor.B);
			}
			else
				return new TColor (closestColor.pixel, (byte)exactColor.red, (byte)exactColor.green, (byte)exactColor.blue);
*/			return TColor.FromPRGB (closestColor.pixel, (byte)exactColor.red, (byte)exactColor.green, (byte)exactColor.blue);
		}
		
		/// <summary> Allocate a read-only colormap entry corresponding to the closest RGB value supported by the hardware.
		/// XAllocExactColor() returns the pixel value of the closest color. The corresponding colormap cell is read-only.
		/// Multiple clients that request the same effective RGB value can be assigned the same read-only entry,
		/// thus allowing entries to be shared. When the last client deallocates a shared cell, it is deallocated. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11colormap"> The colormap to use. <see cref="IntPtr"/> </param>
		/// <param name="colorname"> The color's name to look up. <see cref="System.String"/> </param>
		/// <param name="exact"> Returns true, if requested color has been allocated exactls, of false otherwise. <see cref="System.Boolean"/> </param>
		/// <returns> The pixel value of the requested color on success, or 0 as fallback. <see cref="TPixel"/> </returns>
		public static TPixel XAllocClosestNamedColor (IntPtr x11display, IntPtr x11colormap, string colorname, ref bool exact)
		{
			exact = false;
			if (x11display == IntPtr.Zero)
				return TColor.FallbackBlack.P;
			if (string.IsNullOrEmpty (colorname))
				return TColor.FallbackBlack.P;
			
			string capitalizedColorname = colorname.ToUpper();
			if (capitalizedColorname == "#FFFFFF")
				colorname = "white";
			if (capitalizedColorname == "#000000")
				colorname = "black";
			X11lib.XColor exactColor   = new X11lib.XColor ();
			X11lib.XColor closestColor = new X11lib.XColor ();
			X11lib.XAllocNamedColor (x11display, x11colormap, colorname, ref exactColor, ref closestColor);
			
			if (exactColor.red == closestColor.red && exactColor.green == closestColor.green && exactColor.blue == closestColor.blue)
				exact = true;
			else
				exact = false;
			
			return closestColor.pixel;
		}

		#endregion Colormap and color methods
		
		// ##########################################################################################################
		// ###   B I T M A P
		// ##########################################################################################################
		
		#region Bitmap methods
		
		// Tested: O.K.
		/// <summary> Allocate the memory needed for an XImage structure for the specified display *** but *** does not allocate space for the image itself.
		/// Rather, it initializes the structure byte-order, bit-order, and bitmap-unit values from the display and returns a pointer to the XImage structure.
		/// The red, green, and blue mask values are defined for Z format images only and are derived from the Visual structure passed in. Other values
		/// also are passed in. The offset permits the rapid displaying of the image without requiring each scanline to be shifted into position. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11visual"> The visual to store the image. <see cref="IntPtr"/> </param>
		/// <param name="depth"> The (color) depth of the image. <see cref="TUint"/> </param>
		/// <param name="imageFormat"> The image format. <see cref="TImageFormat"/></param>
		/// <param name="offset"> The number of pixels to ignore at the beginning of the scanline. <see cref="TInt"/> </param>
		/// <param name="data"> The image data. In other words, a char* / byte* to the image data array <see cref="System.Byte[]"/> </param>
		/// <param name="width"> The width of the image, in pixels. <see cref="TUint"/> </param>
		/// <param name="height"> The height of the image, in pixels. <see cref="TUint"/> </param>
		/// <param name="bitmapPad"> The quantum of a scanline (8, 16, or 32). In other words, the start of one scanline is separated in client memory
		/// from the start of the next scanline by an integer multiple of this many bits. <see cref="TInt"/> </param>
		/// <param name="bytesPerLine"> The number of bytes in the client image between the start of one scanline and the start of the next. <see cref="TInt"/> </param>
		/// <returns> The new XImage on success, or IntPtr.Zero on any error. <see cref="IntPtr"/> </returns>
		/// <remarks>  If a value of zero is passed in bytesPerLine, Xlib assumes that the scanlines are contiguous in memory and calculates the value of bytesPerLine itself. </remarks>
		[DllImport ("libX11")]
        extern public static IntPtr XCreateImage (IntPtr x11display, IntPtr x11visual, TUint depth, TImageFormat imageFormat, TInt offset, [MarshalAs(UnmanagedType.U1)] byte[] data, TUint width, TUint height, TInt bitmapPad, TInt bytesPerLine);
		[DllImport ("libX11")]
        extern public static IntPtr XCreateImage (IntPtr x11display, IntPtr x11visual, TUint depth, TImageFormat imageFormat, TInt offset, IntPtr data, TUint width, TUint height, TInt bitmapPad, TInt bytesPerLine);
		
		// Tested: O.K.
		/// <summary> Deallocate the memory associated with the XImage structure. </summary>
		/// <param name="x11ximage"> The image to destroy. <see cref="IntPtr"/> </param>
		/// <remarks> Note that when the image is created using XCreateImage(), XGetImage(), or XSubImage(), the destroy procedure (_XImage.f.destroy_image), that this macro calls,
		/// frees both the image structure and the data pointed to by the image structure. *** Danger *** Don't delete associated graphic data from managed code! </remarks>
		[DllImport("libX11")]
		extern public static void XDestroyImage(IntPtr x11ximage);

		// Tested: O.K.
		/// <summary>Combines an image with a rectangle of the specified drawable.
		/// The section of the image defined by the srcOffsetX, srcOffsetY, width, and height arguments is drawn on the specified part of the drawable.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11drawable">The drawable, the imag is to put on.<see cref="System.IntPtr"/></param>
		/// <param name="x11gc">The crapchics context to use for drawing.<see cref="System.IntPtr"/></param>
		/// <param name="image">The image to be combined with the rectangle.<see cref="System.IntPtr"/></param>
		/// <param name="srcOffsetX">The offset in X from the left edge of the image.<see cref="TInt"/></param>
		/// <param name="srOffsetY">The offset in Y from the top edge of the image.<see cref="TInt"/></param>
		/// <param name="destX">The x coordinate, which is relative to the origin of the drawable and is the coordinate of the subimage.<see cref="TInt"/></param>
		/// <param name="destY">The y coordinate, which is relative to the origin of the drawable and is the coordinate of the subimage.<see cref="TInt"/></param>
		/// <param name="width">The width of the subimage, which define the dimensions of the rectangle.<see cref="TUint"/></param>
		/// <param name="height">The height of the subimage, which define the dimensions of the rectangle.<see cref="TUint"/></param>
		[DllImport("libX11")]
		extern public static void XPutImage (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, IntPtr image, TInt srcOffsetX, TInt srcOffsetY, TInt destX, TInt destY, TUint width, TUint height);
		
		/// <summary> Get a pointer to an XImage structure. This structure provides the contents of the specified rectangle of the drawable in the specified format. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable, the imag is to put on. <see cref="System.IntPtr"/> </param>
		/// <param name="x"> The the x coordinate, which is relative to the origin of the drawable and define the upper-left corner of the rectangle. <see cref="TInt"/> </param>
		/// <param name="x"> The the y coordinate, which is relative to the origin of the drawable and define the upper-left corner of the rectangle. <see cref="TInt"/> </param>
		/// <param name="width"> The width of the subimage, which define the dimensions of the rectangle. <see cref="TUint"/> </param>
		/// <param name="height"> The height of the subimage, which define the dimensions of the rectangle. <see cref="TUint"/> </param>
		/// <param name="planeMask"> The plane mask. <see cref="TUlong"/> </param>
		/// <param name="format"> The format for the image (XYPixmap = (X11.TInt)1 or ZPixmap = (X11.TInt)2). <see cref="TUint"/> </param>
		[DllImport("libX11")]
		extern public static IntPtr XGetImage (IntPtr x11display, IntPtr x11drawable, TInt x, TInt y, TUint width, TUint height, TUlong planeMask, TInt format);
		
		// Tested: O.K.
		/// <summary>Create a pixmap of the width, height, and depth you specified and returns a pixmap ID that identifies it.
		/// It is valid to pass an InputOnly window to the drawable argument.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11drawable">The drawable, the pixmap is created on. The server uses the specified drawable to determine on which screen to create the pixmap.<see cref="System.IntPtr"/></param>
		/// <param name="width">The width, which define the dimensions of the pixmap.<see cref="TUint"/></param>
		/// <param name="height">The height, which define the dimensions of the pixmap.<see cref="TUint"/></param>
		/// <param name="depth">The depth of the pixmap.<see cref="TUint"/></param>
		/// <returns>The pixmap on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		/// <remarks>The width and height arguments must be nonzero, or a BadValue error results.
		/// The depth argument must be one of the depths supported by the screen of the specified drawable, or a BadValue error results.
		/// The pixmap can be used only on this screen and only with other drawables of the same depth.
		/// The initial contents of the pixmap are undefined.</remarks>
		[DllImport("libX11")]
		extern public static IntPtr XCreatePixmap (IntPtr x11display, IntPtr x11drawable, TUint width, TUint height, TUint depth);
		
		// Tested: O.K.
		/// <summary> Delete the association between the pixmap ID and the pixmap.
		/// Then, the X server frees the pixmap storage when there are no references to it.
		/// The pixmap should never be referenced again. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11pixmap"> The pixmap to free. <see cref="IntPtr"/> </param>
		/// <remarks> The XFreePixmap() function first deletes the association between the pixmap ID and the pixmap.
		/// Then, the X server frees the pixmap storage when there are no references to it. The pixmap should never be referenced again. </remarks>
		[DllImport("libX11")]
		extern public static void XFreePixmap(IntPtr x11display, IntPtr x11pixmap);
		
		/// <summary>Get the specified pixel from the indicated image.</summary>
		/// <param name="x11image">The image to get the pixel from.<see cref="IntPtr"/></param>
		/// <param name="x">The x coordinate of the pixel to get.<see cref="TInt"/></param>
		/// <param name="y">The x coordinate of the pixel to get.<see cref="TInt"/></param>
		/// <returns>The requestred pixel.<see cref="X11.TUlong"/></returns>
		[DllImport("libX11")]
		extern public static X11.TPixel XGetPixel(IntPtr x11image, TInt x, TInt y);
		
		// Tested: O.K.
		/// <summary> Set (with x11pixmap) or unset (with IntPtr.Zero) a clip mask (transparency mask). </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to apply the clip mask on. <see cref="System.IntPtr"/> </param>
		/// <param name="x11pixmap"> The clip mask (transparency mask) to apply. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XSetClipMask(IntPtr x11display, IntPtr X11gc, IntPtr  x11pixmap);
		
		[DllImport("libX11")]
		extern public static void XSetClipOrigin (IntPtr x11display, IntPtr X11gc, TInt clipOriginX, TInt clipOriginY);
		
		// Tested: O.K.
		/// <summary>  Change the clip-mask in the specified GC to the specified list of rectangles and sets the clip origin. The
		/// output is clipped to remain contained within the rectangles. The clip-origin is interpreted relative to the origin of
		/// whatever destination drawable is specified in a graphics request. The rectangle coordinates are interpreted relative
		/// to the clip-origin. The rectangles should be nonintersecting, or the graphics results will be undefined. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to apply the clip rectangles on. <see cref="System.IntPtr"/> </param>
		/// <param name="clipOriginX"> The x-coordinate of the clip-mask origin. <see cref="TInt"/> </param>
		/// <param name="clipOriginY"> The y-coordinate of the clip-mask origin. <see cref="TInt"/> </param>
		/// <param name="rectangles"> The array of rectangles that define the clip-mask.<see cref="XRectangle[]"/> </param>
		/// <param name="numRectangles"> The number of rectangles. <see cref="TInt"/> </param>
		/// <param name="ordering"> The ordering relations on the rectangles. Any value of ClipRectanglesOrdering can be passed. <see cref="TInt"/> </param>
		/// <remarks> Note that the list of rectangles can be empty, which effectively disables output.
		/// This is the opposite of passing None as the clip-mask in XCreateGC, XChangeGC, and XSetClipMask. </remarks>
		[DllImport("libX11")]
		extern public static void XSetClipRectangles (IntPtr x11display, IntPtr X11gc, TInt clipOriginX, TInt clipOriginY,
		                                              XRectangle[] rectangles, TInt numRectangles, TInt ordering);
		
		[DllImport("libX11")]
		extern public static void XCopyArea (IntPtr x11display, IntPtr X11drawableSrc, IntPtr X11drawableDest, IntPtr X11gc, TInt srcOffsetX, TInt srcOffsetY, TUint width, TUint height,  TInt dstOffsetX, TInt dstOffsetY);
		
		[DllImport("libX11")]
		extern public static void XCopyPlane (IntPtr x11display, IntPtr X11drawableSrc, IntPtr X11drawableDest, IntPtr X11gc, TInt srcOffsetX, TInt srcOffsetY, TUint width, TUint height,  TInt dstOffsetX, TInt dstOffsetY, TUlong plane);
		
		// Tested: O.K.
		/// <summary> Overwrite the pixel in the named image with the specified pixel value. The input pixel value must be in normalized format (that is,
		/// the least-significant byte of the long is the least-significant byte of the pixel). The image must contain the x and y coordinates. </summary>
		/// <param name="x11image"> The image to overwrite a containing pixel. <see cref="IntPtr"/> </param>
		/// <param name="x"> The logical x coordinate of the pixel to overwrite. <see cref="TInt"/> </param>
		/// <param name="y"> The logical y coordinate of the pixel to overwrite. <see cref="TInt"/> </param>
		/// <param name="pixel"> The pixel value to overwrite. <see cref="TPixel"/> </param>
		[DllImport("libX11")]
		extern public static void XPutPixel(IntPtr x11image, TInt x, TInt y, TPixel pixel);

		// Tested: O.K.
		/// <summary>Allows to include a bitmap file into the program code that was written out by XWriteBitmapFile () without reading in the bitmap file.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="X11drawableSrc">The drawable that indicates the screen.<see cref="IntPtr"/></param>
		/// <param name="data">The data in bitmap format.<see cref="TUchar[]"/></param>
		/// <param name="width">The width of the bitmap.<see cref="TUint"/></param>
		/// <param name="height">The height of the bitmap.<see cref="TUint"/></param>
		/// <returns>The server side pixmap on success, or NULL otherwise.<see cref="TPixmap"/></returns>
		/// <remarks>It is your responsibility to free the bitmap using XFreePixmap() when finished.</remarks>
		[DllImport("libX11")]
		extern public static IntPtr XCreateBitmapFromData (IntPtr x11display, IntPtr X11drawableSrc, TUchar[] data, TUint width, TUint height);
		                       
		[DllImport("libX11")]
		extern public static IntPtr XReadBitmapFile (IntPtr x11display, IntPtr X11drawableSrc, TChar[] filepath,
		                                           ref TUint widthReturn, ref TUint heightReturn, ref IntPtr pixmapReturn,
		                                           ref TInt xHotReturn, ref TInt yHotReturn);
		#endregion Bitmap methods
		
		// ##########################################################################################################
		// ###   D R A W I N G  P R E P A R A T I O N
		// ##########################################################################################################
		
		#region Drawing preparation pathods
		
		// Tested: O.K.
		/// <summary> Clear the entire area in the specified window and is equivalent to XClearArea(display, w, 0, 0, 0, 0, False).
		/// If the window has a defined background tile, the rectangle is tiled with a plane-mask of all ones and GXcopy function.
		/// If the window has background None, the contents of the window are not changed.
		/// If you specify a window whose class is InputOnly , a BadMatch error results. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11window"> The window to clear. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XClearWindow (IntPtr x11display, IntPtr x11window);
			
		// Tested: O.K.
		/// <summary> Create a graphics context.
		/// It can be used with any destination drawable having the same root and depth as the specified drawable. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="valuemask"> Determine which components in the GC are to be set using the information in the specified values structure.
		/// This argument is the bitwise inclusive OR of zero or more of the valid GC component mask bits. <see cref="System.UInt64"/> </param>
		/// <param name="values"> The values as specified by the valuemask in a XGCValues structure. <see cref="IntPtr"/> </param>
		/// <returns> The graphics context on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11")]
		extern public static IntPtr XCreateGC (IntPtr x11display, IntPtr x11drawable, TUlong valuesMask, IntPtr values);
		[DllImport("libX11")]
		extern public static IntPtr XCreateGC (IntPtr x11display, IntPtr x11drawable, TUlong valuesMask, ref XGCValues values);
		
		// Tested: O.K.
		/// <summary>  Destroy the specified GC as well as all the associated storage. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The graphics context to destroy. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XFreeGC (IntPtr x11display, IntPtr x11gc);
		
		/// <summary> Get the resource ID for the indicated graphics context. </summary>
		/// <param name="x11gc"> The graphics context to get the resource ID for. <see cref="System.IntPtr"/> </param>
		/// <returns> The resource ID for the indicated graphics context. <see cref="System.Int32"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XGContextFromGC (IntPtr x11gc);
		
		// Tested: O.K.
		/// <summary> Set the foreground to the specified graphics context. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11gc"> The graphics context. <see cref="System.IntPtr"/> </param>
		/// <param name="foreground"> The foreground to set. <see cref="TPixel"/> </param>
		[DllImport("libX11")]
		extern public static void XSetForeground (IntPtr x11display, IntPtr x11gc, TPixel foreground);

		/// <summary> Set the background to the specified graphics context. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11gc"> The graphics context. <see cref="System.IntPtr"/> </param>
		/// <param name="background"> The background to set. <see cref="TPixel"/> </param>
		[DllImport("libX11")]
		extern public static void XSetBackground (IntPtr x11display, IntPtr x11gc, TPixel background);

		/// <summary>Set the background of the window to the specified graphics context.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11window">The window to set the background for.<see cref="System.IntPtr"/></param>
		/// <param name="background">The background to set.<see cref="TPixel"/></param>
		/// <remarks>XSetWindowBackground() uses a pixmap of undefined size filled with the pixel value you passed.
		/// If the background of an InputOnly window is changed, a BadMatch error results.
		/// The XSetBackground() does not change the current contents of the window.</remarks>
		[DllImport("libX11")]
		extern public static void XSetWindowBackground (IntPtr x11display, IntPtr x11window, TPixel background);
		
		/// <summary>Set the background pixmap of the window to the specified pixmap.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11window">The window to set the background pixmap.<see cref="IntPtr"/></param>
		/// <param name="x11pixmap">The background pixmap, ParentRelative, or None.<see cref="IntPtr"/></param>
		/// <remarks>The background pixmap can immediately be freed if no further explicit references to it are to be made. If ParentRelative
		/// is specified, the background pixmap of the window's parent is used, or on the root window, the default background is restored.
		/// If the background of an InputOnly window is changed, a BadMatch error results.
		/// If the background is set to None, the window has no defined background. A XSetWindowBackground () redefines the background.
		/// The XSetWindowBackgroundPixmap() does not change the current contents of the window.</remarks>
		[DllImport("libX11")]
		extern public static void XSetWindowBackgroundPixmap (IntPtr x11display, IntPtr x11window, IntPtr x11pixmap);
		
		// Tested: O.K.
		/// <summary>Paint a rectangular area in the specified window according to the specified dimensions with the window's background pixel or pixmap.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11window">The window to paint the background for (and implicit the upper-left corner of the rectangle).<see cref="System.IntPtr"/></param>
		/// <param name="x">The x coordinate, which is relative to the origin of the window.<see cref="X11.TInt"/></param>
		/// <param name="y">The y coordinate, which is relative to the origin of the window.<see cref="X11.TInt"/></param>
		/// <param name="width">The width, which is the dimensions of the rectangle.
		/// If width is zero, it is replaced with the current width of the window minus x. <see cref="X11.TUint"/></param>
		/// <param name="height">The height, which is the dimensions of the rectangle.
		/// If height is zero, it is replaced with the current height of the window minus y.<see cref="X11.TUint"/></param>
		/// <param name="exposures">Determine whether 'Expose' events are to be generated.<see cref="System.Boolean"/></param>
		/// <remarks>The subwindow-mode effectively is ClipByChildren. If the window has a defined background tile, the rectangle
		/// clipped by any children is filled with this tile. If the window has background None, the contents of the window are not changed.
		/// In either case, if exposures is True, one or more Expose events are generated for regions of the rectangle that are either
		/// visible or are being retained in a backing store. If the area of an InputOnly window is cleared, a BadMatch error results.</remarks>
		[DllImport("libX11")]
		extern public static void XClearArea(IntPtr x11display, IntPtr x11window, TInt x, TInt y, TUint width, TUint height, bool exposures);

		// Tested: O.K.
		/// <summary> Set the pixel manipulation function of indicated graphics context. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="x11gc"> The graphics context to set the pixel manipulation function for. <see cref="System.IntPtr"/> </param>
		/// <param name="function"> The pixel manipulation function to set. <see cref="XGCFunction"/> </param>
		[DllImport("libX11")]
		extern public static void XSetFunction(IntPtr x11display, IntPtr x11gc, XGCFunction function);
		
		/// <summary>Set all attributes for line drawing.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/> </param>
		/// <param name="x11gc">The graphics context to set the line attributes for.<see cref="System.IntPtr"/> </param>
		/// <param name="lineWidth">The line width, that specifies the line-width to set for the specified GC.<see cref="TUint"/></param>
		/// <param name="lineStyle">The line style, that specifies the line-style to set for the specified GC. Can be LineSolid, LineOnOffDash, or LineDoubleDash.<see cref="XGCLineStyle"/></param>
		/// <param name="capStyle">The cap style, tat specifies the line cap-style to set for the specified GC. Can be CapNotLast, CapButt, CapRound, or CapProjecting.<see cref="XGCCapStyle"/></param>
		/// <param name="joinStyle">The join styl, that specifies the line join-style to set for the specified GC. Can be JoinMiter, JoinRound, or JoinBevel.<see cref="XGCJoinStyle"/></param>
		[DllImport("libX11")]
		extern public static void XSetLineAttributes(IntPtr x11display, IntPtr x11gc, TUint lineWidth, XGCLineStyle lineStyle, XGCCapStyle capStyle, XGCJoinStyle joinStyle);
		
		// Tested: O.K.
		/// <summary>The XGetGCValues() function returns the components specified by valuemask for the specified GC.
		/// If the valuemask contains a valid set of GC mask bits (GCFunction, GCPlaneMask, GCForeground, GCBackground,
		/// GCLineWidth, GCLineStyle, GCCapStyle, GCJoinStyle, GCFillStyle, GCFillRule, GCTile, GCStipple, GCTileStipXOrigin,
		/// GCTileStipYOrigin, GCFont, GCSubwindowMode, GCGraphicsExposures, GCClipXOrigin, GCCLipYOrigin, GCDashOffset,
		/// or GCArcMode) and no error occurs, XGetGCValues() sets the requested components in values and returns nonzero.
		/// Otherwise, it returns a zero status.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="x11gc">The crapchics context to get the values for.<see cref="System.IntPtr"/></param>
		/// <param name="valuemask">The flags, determining which values to get.<see cref="System.UInt32"/></param>
		/// <param name="values">The structure containing the investigated values.<see cref="XGCValues"/></param>
		/// <returns> Nonzero on success, or zero otherwise.<see cref="System.Int32"/></returns>
		/// <remarks> Note that the clip-mask and dash-list (represented by the GCClipMask and GCDashList bits,
		/// respectively, in the valuemask) cannot be requested. Also note that an invalid resource ID (with one
		/// or more of the three most-significant bits set to 1) will be returned for GCFont, GCTile, and GCStipple
		/// if the component has never been explicitly set by the client.</remarks>
		[DllImport("libX11")]
		extern public static int XGetGCValues(IntPtr x11display, IntPtr x11gc, TUint /* GCattributemask */ valuemask, ref XGCValues values);

		// Tested: O.K.
		/// <summary>Change the components specified by valuemask for the specified GC.
		/// The values argument contains the values to be set. The values and restrictions are the same as for XCreateGC().
		/// Changing the clip-mask overrides any previous XSetClipRectangles() request on the context.
		/// Changing the dash-offset or dash-list overrides any previous XSetDashes() request on the context.
		/// The order in which components are verified and altered is server-dependent.
		/// If an error is generated, a subset of the components may have been altered.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11gc">The crapchics context to change the values for.<see cref="IntPtr"/></param>
		/// <param name="valuemask">The flags, determining which values to change.<see cref="System.UInt32"/></param>
		/// <param name="values">The structure containing the change values.<see cref="XGCValues"/></param>
		[DllImport("libX11")]
		extern public static void XChangeGC(IntPtr x11display, IntPtr x11gc, X11.TUint valuemask, ref XGCValues values);
		
		// Tested: O.K.
		/// <summary>Set the fill-style in the specified GC.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11gc">The crapchics context to set the fill-style for.<see cref="IntPtr"/></param>
		/// <param name="fillStyle">Specifies the fill-rule has to be set for the specified GC. It can be passed FillSolid, FillTiled, FillStippled, or FillOpaqueStippled. .<see cref="X11.TInt"/></param>
		[DllImport("libX11", EntryPoint = "XSetFillStyle")]
		extern private static void _XSetFillStyle(IntPtr x11display, IntPtr x11gc, X11.TInt fillStyle);
		public static void XSetFillStyle(IntPtr x11display, IntPtr x11gc, XGCFillStyle fillStyle)
		{	_XSetFillStyle(x11display, x11gc, (X11.TInt)fillStyle);	}
		
		// Tested: O.K.
		/// <summary>Set (with x11pixmap) or unset (with IntPtr.Zero) the stipple mask (transparency mask) in the specified GC.
		/// The stipple must have a depth of one, or a BadMatch error results.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/> </param>
		/// <param name="x11gc">The grapchics context to apply the clip mask on.<see cref="System.IntPtr"/> </param>
		/// <param name="x11pixmap">The stipple mask (transparency mask) to apply to the GC.<see cref="IntPtr"/> </param>
		[DllImport("libX11", EntryPoint = "XSetStipple")]
		extern private static void _XSetStipple(IntPtr x11display, IntPtr x11gc, IntPtr x11pixmap);
		public static void XSetStipple(IntPtr x11display, IntPtr x11gc, IntPtr x11pixmap)
		{	_XSetStipple(x11display, x11gc, x11pixmap);	}
		
		// Tested: O.K.
		/// <summary>Set the fill tile in the specified GC. The tile and GC must have the same depth, or a BadMatch error results.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/> </param>
		/// <param name="x11gc">The grapchics context to apply the clip mask on.<see cref="System.IntPtr"/> </param>
		/// <param name="x11pixmap">The fill tile to apply to the GC.<see cref="IntPtr"/> </param>
		[DllImport("libX11", EntryPoint = "XSetTile")]
		extern private static void _XSetTile(IntPtr x11display, IntPtr x11gc, IntPtr x11pixmap);
		public static void XSetTile(IntPtr x11display, IntPtr x11gc, IntPtr x11pixmap)
		{	_XSetTile(x11display, x11gc, x11pixmap);	}
		
		/// <summary>Set the tile/stipple origin in the specified GC. When graphics requests call for tiling or stippling,
		/// the parent's origin will be interpreted relative to whatever destination drawable is specified in the graphics request. </summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/> </param>
		/// <param name="x11gc">The grapchics context to apply the clip mask on.<see cref="System.IntPtr"/> </param>
		/// <param name="originX">The x coordinate of the tile and stipple origin. <see cref="X11.TInt"/></param>
		/// <param name="originY">The y coordinate of the tile and stipple origin. <see cref="X11.TInt"/></param>
		[DllImport("libX11", EntryPoint = "XSetTSOrigin")]
		extern public static void XSetTSOrigin (IntPtr x11display, IntPtr x11gc, X11.TInt originX, X11.TInt originY);
		
		#endregion Drawing preparation methods
		
		// ##########################################################################################################
		// ###   S H A P E   D R A W I N G
		// ##########################################################################################################
		
		#region Shape drawing methods
		
		// Tested: O.K.
		/// <summary>Create a new empty region.</summary>
		/// <returns>The new empty region.<see cref="IntPtr"/></returns>
		/// <remarks>Don't forget to call XDestroyRegion().</remarks>
		[DllImport("libX11")]
		extern public static IntPtr XCreateRegion();
		
		// Tested: O.K.
		/// <summary>Update the destination region from a union of the specified rectangle and the specified source region.</summary>
		/// <param name="rect">The rectangle to update the destination region.<see cref="XRectangle"/></param>
		/// <param name="sourceRegion">The region to update the destination region.<see cref="IntPtr"/></param>
		/// <param name="destRegion">The destination region.<see cref="IntPtr"/></param>
		[DllImport("libX11")]
		extern public static void XUnionRectWithRegion (ref XRectangle rect, IntPtr sourceRegion, IntPtr destRegion);
		
		// Tested: O.K.
		/// <summary>Set the clip-mask in the GC to the specified region. The region is specified relative to the drawable's origin.
		/// The resulting GC clip origin is implementation dependent. Once it is set in the GC, the region can be destroyed.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11gc">The crapchics context to set the clip-mask for drawing. <see cref="IntPtr"/></param>
		/// <param name="region">The region that defines the clip-mask.<see cref="IntPtr"/></param>
		[DllImport("libX11")]
		extern public static void XSetRegion (IntPtr x11display, IntPtr X11gc, IntPtr region);
		
		// Tested: O.K.
		/// <summary>Destroy the indicated region.</summary>
		/// <param name="region">The region to destroy.<see cref="IntPtr"/></param>
		[DllImport("libX11")]
		extern public static void XDestroyRegion(IntPtr region);
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XDrawPoint")]
		extern private static void _XDrawPoint (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, X11.TInt x, X11.TInt y);

		// Tested: O.K.
		/// <summary> Draw a line between the specified set of points (x1, y1) and (x2, y2) using the components of the specified GC.
		/// It does not perform joining at coincident endpoints. For any given line, XDrawLine() does not draw a pixel more than once.
		/// If lines intersect, the intersecting pixels are drawn multiple times. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The point x-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="y"> The point y-coordinate. <see cref="System.Int32"/> </param>
		/// <remarks> XDrawPoint() use these GC components: function, plane-mask, foreground, subwindow-mode, clip-x-origin, clip-y-origin
		/// and clip-mask. XDrawPoint() also uses these GC mode-dependent component: foreground. </remarks>
		public static void XDrawPoint (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, int x, int y)
		{
			_XDrawPoint (x11display, x11drawable, X11gc, (X11.TInt)x, (X11.TInt)y);
		}
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XDrawLine")]
		extern private static void _XDrawLine (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, X11.TInt x1, X11.TInt y1, X11.TInt x2, X11.TInt y2);
		// Tested: O.K.
		/// <summary> Draw a line between the specified set of points (x1, y1) and (x2, y2) using the components of the specified GC.
		/// It does not perform joining at coincident endpoints. For any given line, XDrawLine() does not draw a pixel more than once.
		/// If lines intersect, the intersecting pixels are drawn multiple times. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x1"> The start point x-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="y1"> The start point y-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="x2"> The end point x-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="y2"> The end point x-coordinate. <see cref="System.Int32"/> </param>
		/// <remarks> XDrawLine() use these GC components: function, plane-mask, line-width, line-style, cap-style, fill-style, subwindow-mode,
		/// clip-x-origin, clip-y-origin, and clip-mask. XDrawLine() also uses these GC mode-dependent components: foreground, background, tile,
		/// stipple, tile-stipple-x-origin, tile-stipple-y-origin, dash-offset, and dash-list. </remarks>
		public static void XDrawLine (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, int x1, int y1, int x2, int y2)
		{
			_XDrawLine (x11display, x11drawable, X11gc, (X11.TInt)x1, (X11.TInt)y1, (X11.TInt)x2, (X11.TInt)y2);
		}
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XDrawArc")]
		extern private static void _XDrawArc (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, X11.TInt x, X11.TInt y, X11.TUint width, X11.TUint height, X11.TInt startAngle, X11.TInt sweepAngle);
		// Tested: O.K.
		/// <summary> Draw  a single circular or elliptical arc specified by a bounding rectangle and two angles.
		/// The center of the circle or ellipse is the center of the rectangle, and the major and minor axes are specified by the width and height.
		/// Positive angles indicate counterclockwise motion, and negative angles indicate clockwise motion.
		/// If the magnitude of angle2 is greater than 360 degrees, XDrawArc() truncates it to 360 degrees. 
		/// Specifying an arc with one endpoint and a clockwise extent draws the same pixels as specifying
		/// the other endpoint and an equivalent counterclockwise extent, except as it affects joins. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The bounding box x-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="y"> The bounding box y-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="width"> The bounding box width. <see cref="System.Int32"/> </param>
		/// <param name="height"> The bounding box height. <see cref="System.Int32"/> </param>
		/// <param name="startAngle"> The arc's start angle, relative to the 3 o clock position counterclockwise in units of degrees * 64. <see cref="System.Int32"/> </param>
		/// <param name="sweepAngle"> The angle between startAngle and the end of the arc, counterclockwise in units of degrees * 64. <see cref="System.Int32"/> </param>
		/// <remarks> XDrawLine() use these GC components: function, plane-mask, line-width, line-style, cap-style, join-style, fill-style,
		/// subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask. It also uses these GC mode-dependent components: foreground,
		/// background, tile, stipple, tile-stipple-x-origin, tile-stipple-y-origin, dash-offset, and dash-list. </remarks>
		public static void XDrawArc (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			_XDrawArc (x11display, x11drawable, X11gc, (X11.TInt)x, (X11.TInt)y, (X11.TUint)width, (X11.TUint)height, (X11.TInt)startAngle, (X11.TInt)sweepAngle);
		}
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XDrawLines")]
		extern private static void _XDrawLines (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, X11lib.XPoint[] points, X11.TInt numPoints, X11.TInt coordMode);
		// Tested: O.K.
		/// <summary> Draw numPoints-1 lines between each pair of points (point[i], point[i+1]) in the array of XPoint structure
		///   using the components of the specified GC. It draws the lines in the order listed in the array.
		/// The lines join correctly at all intermediate points, and if the first and last points coincide, the first and last lines
		///   also join correctly. For any given line, XDrawLines does not draw a pixel more than once.
		/// If thin (zero line-width) lines intersect, the intersecting pixels are drawn multiple times. If wide lines intersect, the
		///   intersecting pixels are drawn only once, as though the entire PolyLine protocol request were a single, filled shape. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="points"> The array of points, defining the lines to draw. <see cref="X11lib.XPoint[]"/> </param>
		/// <param name="numPoints"> The number of points in the array. <see cref="System.Int32"/> </param>
		/// <param name="coordMode"> The coordinate mode. You can pass CoordModeOrigin or CoordModePrevious.
		/// CoordModeOrigin treats all coordinates as relative to the origin, and CoordModePrevious treats all coordinates
		///   after the first as relative to the previous point. <see cref="X11lib.XCoordinateMode"/> </param>
		public static void XDrawLines (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11lib.XPoint[] points, int numPoints, X11lib.XCoordinateMode coordMode)
		{
			_XDrawLines (x11display, x11drawable, x11gc, points, (X11.TInt)numPoints, (X11.TInt)coordMode);
		}
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XDrawRectangle")]
		extern private static void _XDrawRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, X11.TInt x, X11.TInt y, X11.TUint width, X11.TUint height);
		// Tested: O.K.
		/// <summary> Draw the outlines of the specified rectangle as if a five-point PolyLine protocol request were specified for the rectangle
		/// For the specified rectangle, this function does not draw a pixel more than once. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The left top corner x-coordinate. <see cref="SX11.TInt"/> </param>
		/// <param name="y"> The left top corner y-coordinate. <see cref="SX11.TInt"/> </param>
		/// <param name="width"> The rectangle width. <see cref="X11.TUint"/> </param>
		/// <param name="height"> The rectangle height. <see cref="X11.TUint"/> </param>
		/// <remarks> The function uses these GC components: function, plane-mask, line-width, line-style, cap-style, join-style, fill-style, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also uses these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, tile-stipple-y-origin, dash-offset, and dash-list.  </remarks>
		public static void XDrawRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr X11gc, int x, int y, int width, int height)
		{
			_XDrawRectangle (x11display, x11drawable, X11gc, (X11.TInt)x, (X11.TInt)y, (X11.TUint)width, (X11.TUint)height);
		}
		
		// Tested: O.K.
		/// <summary> Draw the specified rectangle as if a four-point polygon protocol request. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The left top corner x-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="y"> The left top corner y-coordinate. <see cref="System.Int32"/> </param>
		/// <param name="width"> The rectangle width. <see cref="System.Int32"/> </param>
		/// <param name="height"> The rectangle height. <see cref="System.Int32"/> </param>
		/// <remarks> The function uses these GC components: function, plane-mask, fill-style, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also uses these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin. </remarks>
		public static void XDrawRectangle2 (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, int x, int y, int width, int height)
		{
			X11lib.XPoint[] points = {	new XPoint (x,         y),
										new XPoint (x + width, y),
										new XPoint (x + width, y + height),
										new XPoint (x,         y + height),
										new XPoint (x,         y) };
			_XDrawLines (x11display, x11drawable, x11gc, points, (X11.TInt)5, (X11.TInt)0);
			/*
			XDrawLine (x11display, x11drawable, x11gc, (X11.TInt)(x),         (X11.TInt)(y),          (X11.TInt)(x + width), (X11.TInt)(y));
			XDrawLine (x11display, x11drawable, x11gc, (X11.TInt)(x + width), (X11.TInt)(y),          (X11.TInt)(x + width), (X11.TInt)(y + height + 1));
			XDrawLine (x11display, x11drawable, x11gc, (X11.TInt)(x + width), (X11.TInt)(y + height), (X11.TInt)(x),         (X11.TInt)(y + height));
			XDrawLine (x11display, x11drawable, x11gc, (X11.TInt)(x),         (X11.TInt)(y + height), (X11.TInt)(x),         (X11.TInt)(y));
			*/
		}
		
		// Tested: O.K.
		/// <summary> Draw the specified rectangle as if a four-point polygon protocol request. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="rect"> The boundin rectangle. <see cref="System.Drawing.Rectangle"/> </param>
		/// <remarks> The function uses these GC components: function, plane-mask, fill-style, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also uses these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin. </remarks>
		public static void XDrawRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, System.Drawing.Rectangle rect)
		{
			X11lib.XPoint[] points = {	new XPoint (rect.X,              rect.Y),
										new XPoint (rect.X + rect.Width, rect.Y),
										new XPoint (rect.X + rect.Width, rect.Y + rect.Height),
										new XPoint (rect.X,              rect.Y + rect.Height),
										new XPoint (rect.X,              rect.Y) };
			_XDrawLines (x11display, x11drawable, x11gc, points, (X11.TInt)5, (X11.TInt)0);
		}
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XFillRectangle")]
		extern private static void _XFillRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11.TInt x, X11.TInt y, X11.TUint width, X11.TUint height);
		// Tested: O.K.
		/// <summary> Fill the specified rectangle as if a four-point FillPolygon protocol request.
		/// For any given rectangle, XFillRectangle() does not draw a pixel more than once. If rectangles intersect, the intersecting pixels are drawn multiple times. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The left top corner x-coordinate. <see cref="X11.TInt"/> </param>
		/// <param name="y"> The left top corner y-coordinate. <see cref="X11.TInt"/> </param>
		/// <param name="width"> The rectangle width. <see cref="X11.TUint"/> </param>
		/// <param name="height"> The rectangle height. <see cref="X11.TUint"/> </param>
		/// <remarks> The function uses these GC components: function, plane-mask, fill-style, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also uses these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin. </remarks>
		public static void XFillRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, int x, int y, int width, int height)
		{
			_XFillRectangle (x11display, x11drawable, x11gc, (X11.TInt)x, (X11.TInt)y, (X11.TUint)width, (X11.TUint)height);
		}
		
		// Tested: O.K.
		/// <summary> Fill the specified rectangle as if a four-point FillPolygon protocol request were specified for each rectangle. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="rect"> The boundin rectangle. <see cref="System.Drawing.Rectangle"/> </param>
		/// <remarks> The function uses these GC components: f function, plane-mask, fill-style, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also use these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin.  </remarks>
		public static void XFillRectangle (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, System.Drawing.Rectangle rect)
		{
			_XFillRectangle (x11display, x11drawable, x11gc, (X11.TInt)rect.X, (X11.TInt)rect.Y, (X11.TUint)rect.Width, (X11.TUint)rect.Height);
		}
		
		// Tested: O.K.
		[DllImport("libX11", EntryPoint = "XFillPolygon")]
		extern private static void _XFillPolygon (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11lib.XPoint[] points, X11.TInt numPoints, X11.TInt shapeComplexity, X11.TInt coordMode);
		// Tested: O.K.
		/// <summary> Fill the region closed by the specified path. The path is closed automatically if the last point in the list
		///   does not coincide with the first point. XFillPolygon() does not draw a pixel of the region more than once.
		/// The fill-rule of the GC controls the filling behavior of self-intersecting polygons.
		/// This function uses these GC components: function, plane-mask, fill-style, fill-rule, subwindow-mode, clip-x-origin,
		///   clip-y-origin, and clip-mask. It also uses these GC mode-dependent components: foreground, background, tile, stipple,
		///   tile-stipple-x-origin, and tile-stipple-y-origin. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="points"> The array of points, defining the polygon to fill. <see cref="X11lib.XPoint[]"/> </param>
		/// <param name="numPoints"> The number of points in the array. <see cref="System.Int32"/> </param>
		/// <param name="shapeComplexity"> The shape complexity. You can pass Complex, Convex or Nonconvex.
		/// * Complex means the path may self-intersect. Note that contiguous coincident points in the path are not treated as self-intersection.
		/// * Convex means for every pair of points inside the polygon, the line segment connecting them does not intersect the path.
		///   If known by the client, specifying Convex can improve performance.
		///   If Convex is specified for a path that is not convex, the graphics results are undefined. 
		/// * Nonconvex means the path does not self-intersect, but the shape is not wholly convex.
		///   If known by the client, specifying Nonconvex instead of Complex may improve performance.
		///   If Nonconvex is specified for a self-intersecting path, the graphics results are undefined. <see cref="XPolygonShape"/> </param>
		/// <param name="coordMode"> The coordinate mode. You can pass CoordModeOrigin or CoordModePrevious.
		///   CoordModeOrigin treats all coordinates as relative to the origin, and CoordModePrevious treats all coordinates
		///   after the first as relative to the previous point. <see cref="XCoordinateMode"/> </param>
		public static void XFillPolygon (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11lib.XPoint[] points, int numPoints, XPolygonShape shapeComplexity, XCoordinateMode coordMode)
		{
			_XFillPolygon (x11display, x11drawable, x11gc, points, (X11.TInt)numPoints, (X11.TInt)shapeComplexity, (X11.TInt)coordMode);
		}
		
		// Tested: O.K.
		/// <summary> Fill region closed by the infinitely thin path described by the specified arc and, depending on the arc-mode specified in the GC,
		/// one (ArcChord) or two (ArcPieSlice) line segments. For ArcChord , the single line segment joining the endpoints of the arc is used.
		/// For ArcPieSlice , the two line segments joining the endpoints of the arc with the center point are used. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The left top bounding rectangle x-coordinate. <see cref="SX11.TInt"/> </param>
		/// <param name="y"> The left top bounding rectangle y-coordinate. <see cref="SX11.TInt"/> </param>
		/// <param name="width"> The bounding rectangle width. <see cref="X11.TUint"/> </param>
		/// <param name="height"> The bounding rectangle height. <see cref="X11.TUint"/> </param>
		/// <param name="angle1"> The start of the arc relative to the three-o'clock position from the center, in units of degrees * 64. <see cref="X11.TInt"/> </param>
		/// <param name="angle2"> The the path and extent of the arc relative to the start of the arc, in units of degrees * 64.  <see cref="X11.TInt"/> </param>
		/// <remarks> The function uses these GC components: : function, plane-mask, fill-style, arc-mode, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also uses these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin. </remarks>
		[DllImport("libX11")]
		extern public static void XFillArc (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11.TInt x, X11.TInt y, X11.TUint width, X11.TUint height, X11.TInt angle1, X11.TInt angle2);
		
		// Tested: O.K.
		public static X11lib.XPoint[] XPathToPolygon (X11.X11PathSegmentCollection path)
		{
			if (path == null || path._elements.Count == 0)
				return new X11lib.XPoint[0];
			
			System.Collections.Generic.List<X11lib.XPoint> interpolation = new System.Collections.Generic.List<X11lib.XPoint>();
			
			System.Windows.Point last = new System.Windows.Point (0.0, 0.0);
			for (int counter = 0; counter < path._elements.Count; counter++)
			{
				if (path._elements[counter].Type == X11PathSegmentType.MoveTo)
				{
					X11.IMoveToPathSegment mt = path._elements[counter] as X11.IMoveToPathSegment;
					
					last.X = mt.Point.X;
					last.Y = mt.Point.Y;
					interpolation.Add (new X11lib.XPoint((X11.TShort)(last.X  + 0.5), (X11.TShort)(last.Y  + 0.5)));
				}
				else if (path._elements[counter].Type == X11PathSegmentType.Line)
				{
					X11.ILinePathSegment ln = path._elements[counter] as X11.ILinePathSegment;
					interpolation.Add (new X11lib.XPoint((X11.TShort)(ln.Start.X  + 0.5), (X11.TShort)(ln.Start.Y  + 0.5)));
					
					last.X = ln.End.X;
					last.Y = ln.End.Y;
					interpolation.Add (new X11lib.XPoint((X11.TShort)(last.X  + 0.5), (X11.TShort)(last.Y  + 0.5)));
				}
				else if (path._elements[counter].Type == X11PathSegmentType.LineTo)
				{
					X11.ILineToPathSegment lt = path._elements[counter] as X11.ILineToPathSegment;
					
					last.X = lt.End.X;
					last.Y = lt.End.Y;
					interpolation.Add (new X11lib.XPoint((X11.TShort)(last.X  + 0.5), (X11.TShort)(last.Y  + 0.5)));
				}
				else if (path._elements[counter].Type == X11PathSegmentType.PolyLine)
				{
					X11.IPolyLinePathSegment polylineSegment = path._elements[counter] as X11.IPolyLinePathSegment;
					
					foreach (System.Windows.Point point in polylineSegment.PointList)
						interpolation.Add (new X11lib.XPoint((X11.TShort)(point.X + 0.5), (X11.TShort)(point.Y + 0.5)));
					
					last.X = polylineSegment.PointList[polylineSegment.PointList.Count - 1].X;
					last.Y = polylineSegment.PointList[polylineSegment.PointList.Count - 1].Y;
				}
				else if (path._elements[counter].Type == X11PathSegmentType.Arc)
				{
					X11.IArcPathSegment ar = path._elements[counter] as X11.IArcPathSegment;
					
					// Convert 3 o'clock angle (X11) to 12 o'clock angle (mathematics) by adding the missing 90° from 12 o'clock to 3 o'clock.
					// Convert degrees to radiant by a pultiplication with Math.PI and a division by 180°.
					double                start = Math.PI * (90 + ar.StartAngle) / 180.0;
					double                end   = Math.PI * (90 + ar.StartAngle + ar.RotationAngle) / 180.0;
					System.Windows.Point center = ar.Center();

					int      resultPointNumber  = Mathematics.ArcCurve2D.CalculateNumberOfResultPoints (center.X, center.Y,
					                                                                                    ar.Size.Width, ar.Size.Height, start, end, false);
					System.Windows.Point[] resultPoints       = new System.Windows.Point[resultPointNumber];
					Mathematics.ArcCurve2D.CalculateInterpolationPoints (center.X, center.Y, ar.Size.Width, ar.Size.Height,
					                                                     start, end, false, resultPointNumber, resultPoints);
					
					for (int index = 0; index < resultPoints.Length; index++)
						interpolation.Add (new X11lib.XPoint((X11.TShort)(resultPoints[index].X + 0.5), (X11.TShort)(resultPoints[index].Y + 0.5)));
					
					last.X = resultPoints[resultPointNumber - 1].X;
					last.Y = resultPoints[resultPointNumber - 1].Y;
				}
				else if (path._elements[counter].Type == X11PathSegmentType.QuadraticBezier)
				{
					X11.IQuadraticBezierPathSegment qu = path._elements[counter] as X11.IQuadraticBezierPathSegment;
					
					System.Windows.Point[] inputPoints = new System.Windows.Point[3];
					inputPoints[0] = last;
					inputPoints[1] = qu.Control;
					inputPoints[2] = qu.End;
					
					int resultPointNumber = Mathematics.BezierCurve2D.CalculateNumberOfResultPoints (inputPoints);
					System.Windows.Point[] resultPoints = new System.Windows.Point[resultPointNumber];
					
					Mathematics.BezierCurve2D.CalculateInterpolationPoints (inputPoints, resultPointNumber, resultPoints);
					
					for (int index = 0; index < resultPoints.Length; index++)
						interpolation.Add (new X11lib.XPoint((X11.TShort)(resultPoints[index].X + 0.5), (X11.TShort)(resultPoints[index].Y + 0.5)));

					last.X = qu.End.X;
					last.Y = qu.End.Y;
				}
				else if (path._elements[counter].Type == X11PathSegmentType.CubicBezier)
				{
					X11.ICubicBezierPathSegment qu = path._elements[counter] as X11.ICubicBezierPathSegment;
						
					System.Windows.Point[] inputPoints = new System.Windows.Point[4];
					inputPoints[0] = last;
					inputPoints[1] = qu.Control1;
					inputPoints[2] = qu.Control2;
					inputPoints[3] = qu.End;
					
					int resultPointNumber = Mathematics.BezierCurve2D.CalculateNumberOfResultPoints (inputPoints);
					System.Windows.Point[] resultPoints = new System.Windows.Point[resultPointNumber];
					
					Mathematics.BezierCurve2D.CalculateInterpolationPoints (inputPoints, resultPointNumber, resultPoints);
					
					for (int index = 0; index < resultPoints.Length; index++)
						interpolation.Add (new X11lib.XPoint((X11.TShort)(resultPoints[index].X + 0.5), (X11.TShort)(resultPoints[index].Y + 0.5)));

					last.X = qu.End.X;
					last.Y = qu.End.Y;
				}
				else
				{
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::XPathToPolygon() Unknown path element type '{0}' on index '{1}' in argument 'path'.", path._elements[counter].Type.ToString (), counter);
				}
			}
			
			if (interpolation.Count == 0)
				return  new X11lib.XPoint[0];
			
			return interpolation.ToArray ();
		}
		
		#endregion Shape drawing methods
		
		// ##########################################################################################################
		// ###   S T R I N G   D R A W I N G  P R E P A R A T I O N
		// ##########################################################################################################

		#region String drawing preparation methods
		
		// Tested: O.K.
		/// <summary>Check if Xlib functions are capable of operating under the current locale.</summary>
		/// <returns>True, if Xlib functions are capable of operating under the current locale, or false otherwise.<see cref="System.Boolean"/></returns>
		/// <remarks>If it returns False, Xlib locale-dependent functions, for which the XLocaleNotSupported return status is defined,
		/// will return XLocaleNotSupported. Other Xlib locale-dependent routines will operate in the 'C' locale.</remarks>
		[DllImport ("libX11")]
        extern public static bool XSupportsLocale ();
		
		// Tested: O.K.
		/// <summary>Set the X modifiers for the current locale setting.</summary>
		/// <param name="modifiers">The null-terminated string of the form '{@category=value}', that is, having zero or more concatenated
		/// '@category=value' entries, where category is a category name and value is the (possibly empty) setting for that category.
		/// The values are encoded in the current locale. Category names are restricted to the POSIX Portable Filename Character Set.<see cref="TChar[]"/></param>
		/// <returns>The string reprecenting the current modifierrs on success (can be empty, if only implementation-dependent default modifiers are activated),
		/// or IntPtr.Zero if invalid values are given for one or more modifier categories and none of the current modifiers are changed.<see cref="IntPtr"/></returns>
		[DllImport ("libX11")]
        extern public static IntPtr XSetLocaleModifiers (TChar[] modifiers);

		#endregion String drawing preparation methods
		
		// ##########################################################################################################
		// ###   S T R I N G   D R A W I N G
		// ##########################################################################################################

		#region String drawing methods

		// Tested: O.K.
		/// <summary> Return the bounding box of the specified 8-bit character string in the specified font or the font contained in the specified GC.
		/// This function queries the X server and, therefore, suffer the round-trip overhead that is avoided by XTextExtents().
		/// The function returns a XCharStruct structure, whose members are set to the values as follows. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="resourceID"> Specifies either the font ID or the graphics context ID that contains the font. <see cref="X11.TInt"/> </param>
		/// <param name="text"> The text to measure. <see cref="System.SByte[]"/> </param>
		/// <param name="length"> The length of the text to measure. <see cref="System.Int32"/> </param>
		/// <param name="direction"> The direction hint (FontLeftToRight or FontRightToLeft). <see cref="X11.TInt"/> </param>
		/// <param name="fontAscent"> The font ascent. <see cref="X11.TInt"/> </param>
		/// <param name="fontDescent"> The font descent. <see cref="X11.TInt"/> </param>
		/// <param name="overall"> The overall size stored in the specified XCharStruct structure.
		/// The ascent member is set to the maximum of the ascent metrics of all characters in the string.
		/// The descent member is set to the maximum of the descent metrics.
		/// The width member is set to the sum of the character-width metrics of all characters in the string.
		/// For each character in the string, let W be the sum of the character-width metrics of all characters preceding it in the string.
		/// Let L be the left-side-bearing metric of the character plus W. Let R be the right-side-bearing metric of the character plus W.
		/// The lbearing member is set to the minimum L of all characters in the string. The rbearing member is set to the maximum R. <see cref="XCharStruct"/> </param>
		/// <remarks> Characters with all zero metrics are ignored.
		/// If the font has no defined default_char, the undefined characters in the string are also ignored. </remarks>
		[DllImport("libX11")]
		extern public static void XQueryTextExtents (IntPtr x11display, X11.TInt resourceID, X11.TChar[] text, X11.TInt length, ref X11.TInt direction, ref X11.TInt fontAscent, ref X11.TInt fontDescent, ref XCharStruct overall);
			
		/// <summary> Return the bounding box of the specified 8-bit character string in the specified font or the font contained in the specified GC.
		/// This function queries the X server and, therefore, suffer the round-trip overhead that is avoided by XTextExtents().
		/// The function returns a XCharStruct structure, whose members are set to the values as follows. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="resourceID"> Specifies either the font ID or the graphics context ID that contains the font. <see cref="X11.TInt"/> </param>
		/// <param name="text"> The text to measure. <see cref="System.SByte[]"/> </param>
		/// <param name="length"> The length of the text to measure. <see cref="System.Int32"/> </param>
		/// <param name="direction"> The direction hint (FontLeftToRight or FontRightToLeft). <see cref="X11.TInt"/> </param>
		/// <param name="fontAscent"> The font ascent. <see cref="X11.TInt"/> </param>
		/// <param name="fontDescent"> The font descent. <see cref="X11.TInt"/> </param>
		/// <param name="overall"> The overall size stored in the specified XCharStruct structure.
		/// The ascent member is set to the maximum of the ascent metrics of all characters in the string.
		/// The descent member is set to the maximum of the descent metrics.
		/// The width member is set to the sum of the character-width metrics of all characters in the string.
		/// For each character in the string, let W be the sum of the character-width metrics of all characters preceding it in the string.
		/// Let L be the left-side-bearing metric of the character plus W. Let R be the right-side-bearing metric of the character plus W.
		/// The lbearing member is set to the minimum L of all characters in the string. The rbearing member is set to the maximum R. <see cref="XCharStruct"/> </param>
		/// <remarks> Characters with all zero metrics are ignored.
		/// If the font has no defined default_char, the undefined characters in the string are also ignored.
		/// For fonts defined with linear indexing rather than 2-byte matrix indexing, each XChar2b structure is interpreted as a 16-bit number
		/// with byte1 as the most-significant byte. If the font has no defined default character, undefined characters in the string are taken
		/// to have all zero metrics.</remarks>
		[DllImport("libX11")]
		extern public static void XQueryTextExtents16 (IntPtr x11display, X11.XID resourceID, XChar2b[] text, X11.TInt length, ref X11.TInt direction, ref X11.TInt fontAscent, ref X11.TInt fontDescent, ref XCharStruct overall);
		
		[DllImport("libX11")]
		extern public static X11.TInt XTextWidth(IntPtr xFontStruct, X11.TChar[] text, X11.TInt length);
		[DllImport("libX11")]
		extern public static X11.TInt XTextWidth16(IntPtr xFontStruct, XChar2b[] text, X11.TInt length);
			
		// Tested: O.K.
		/// <summary> Draw a string without cleaning the background.
		/// Each character image, as defined by the font in the GC, is treated as an additional mask for a fill operation on the drawable.
		/// The drawable is modified only where the font character has a bit set to 1. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x"> The left top corner x-coordinate. <see cref="X11.TInt"/> </param>
		/// <param name="y"> The left top corner y-coordinate. <see cref="X11.TInt"/> </param>
		/// <param name="text"> The text to draw. <see cref="System.SByte[]"/> </param>
		/// <param name="length"> The length of the text to draw. <see cref="X11.TInt"/> </param>
		/// <remarks> The function uses these GC components: function, plane-mask, fill-style, font, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also use these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin.</remarks>
		[DllImport("libX11")]
		extern public static void XDrawString (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11.TInt x, X11.TInt y, X11.TChar[] text, X11.TInt length);
		
		// Tested: O.K.
		/// <summary> Draw a string without cleaning the background.
		/// Each character image, as defined by the font in the GC, is treated as an additional mask for a fill operation on the drawable.
		/// The drawable is modified only where the font character has a bit set to 1. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11drawable"> The drawable to draw on. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The crapchics context to use for drawing. <see cref="IntPtr"/> </param>
		/// <param name="x">The leftmost x-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="y">The baseline y-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="text"> The text to draw. <see cref="XChar2b[]"/> </param>
		/// <param name="length"> The length of the text to draw. <see cref="X11.TInt"/> </param>
		/// <remarks> The function uses these GC components: function, plane-mask, fill-style, font, subwindow-mode, clip-x-origin, clip-y-origin, and clip-mask.
		/// It also use these GC mode-dependent components: foreground, background, tile, stipple, tile-stipple-x-origin, and tile-stipple-y-origin.</remarks>
		[DllImport("libX11")]
		extern public static void XDrawString16 (IntPtr x11display, IntPtr x11drawable, IntPtr x11gc, X11.TInt x, X11.TInt y, XChar2b[] text, X11.TInt length);
		
		// Tested: O.K.
		/// <summary>Get the XFontSetExtents structure for the fonts used by the Xmb and Xwc layers for the given font set.</summary>
		/// <param name="x11fontSet">Specifies the fontset to obtain the maximum extents structure for.<see cref="X11.XID"/></param>
		/// <remarks>The XFontSetExtents structure is owned by Xlib and should not be modified or freed by the client. It will be
		/// freed by a call to XFreeFontSet with the associated XFontSet. Until freed, its contents will not be modified by Xlib.</remarks>
		[DllImport("libX11", EntryPoint = "XExtentsOfFontSet")]
		extern private static IntPtr _XExtentsOfFontSet (X11.XID x11fontSet);
		public static XFontSetExtents XExtentsOfFontSet (X11.XID x11fontSet)
		{
			IntPtr					result  = _XExtentsOfFontSet (x11fontSet);
			X11lib.XFontSetExtents	extents = (X11lib.XFontSetExtents)Marshal.PtrToStructure (result, typeof(X11lib.XFontSetExtents));
			
			return extents;
		}
		
		// Tested: O.K.
		/// <summary>Set the components of the specified overallInk and overallLogical arguments to the overall bounding box of the string's image
		/// and a logical bounding box for spacing purposes.</summary>
		/// <param name="xFontSet">Specifies the font set. <see cref="X11.XID"/></param>
		/// <param name="text">Specifies the character string.<see cref="X11.TWchar[]"/></param>
		/// <param name="length">Specifies the number of characters in the string argument.<see cref="X11.TInt"/></param>
		/// <param name="overallInk">Returns the overall ink bounding box of the string's image.<see cref="IntPtr"/></param>
		/// <param name="overallLogical">Returns the overall logical bounding box for spacing purposes.<see cref="IntPtr"/></param>
		/// <returns>???<see cref="TInt"/></returns>
		[DllImport("libX11", EntryPoint = "XwcTextExtents")]
		extern private static TInt _XwcTextExtents (X11.XID xFontSet, X11.TWchar[] text, X11.TInt length, IntPtr overallInk, IntPtr overallLogical);
		public static TInt XwcTextExtents (X11.XID xFontSet, X11.TWchar[] text, X11.TInt length, ref XRectangle overallInk, ref XRectangle overallLogical)
		{
			IntPtr pOverallInk		= Marshal.AllocHGlobal (Marshal.SizeOf (overallInk));
			IntPtr pOverallLogical	= Marshal.AllocHGlobal (Marshal.SizeOf (overallLogical));
			TInt result = _XwcTextExtents(xFontSet, text, (TInt)length, pOverallInk, pOverallLogical);
			overallInk		= (XRectangle)Marshal.PtrToStructure (pOverallInk,     typeof(X11lib.XRectangle));
			overallLogical	= (XRectangle)Marshal.PtrToStructure (pOverallLogical, typeof(X11lib.XRectangle));
			
			return result;
		}
		
		// Tested: O.K.
		/// <summary>Obtain the escapement of text.</summary>
		/// <param name="xFontSet">Specifies the font set. <see cref="X11.XID"/></param>
		/// <param name="text">Specifies the character string.<see cref="X11.TWchar[]"/></param>
		/// <param name="length">Specifies the number of characters in the string argument.<see cref="X11.TInt"/></param>
		/// <returns>The escapement in pixels of the specified string as a value, using the fonts loaded for the specified font set.
		/// The escapement is the distance in pixels in the primary draw direction from the drawing origin to the origin of the next character
		/// to be drawn, assuming that the rendering of the next character is not dependent on the supplied string.<see cref="TInt"/></returns>
		[DllImport("libX11")]
		extern public static TInt XwcTextEscapement(X11.XID xFontSet, X11.TWchar[] text, X11.TInt length); 
		
		// Tested: O.K.
		/// <summary>Draw the specified text with the foreground pixel.
		/// When the fontSet has missing charsets, each unavailable character is drawn with the default string returned by XCreateFontSet.
		/// The behavior for an invalid codepoint is undefined.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11drawable">The drawable to draw on.<see cref="IntPtr"/></param>
		/// <param name="fontSet">The fontset to use for drawing.<see cref="X11.XID"/></param>
		/// <param name="x11gc">The crapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The leftmost x-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="y">The baseline y-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="text">The text to draw.<see cref="System.SByte[]"/></param>
		/// <param name="length">The length of the text to draw (as number of bytes).<see cref="X11.TInt"/></param>
		[DllImport ("libX11")]
        extern public static void XmbDrawString   (IntPtr x11display, IntPtr x11drawable, X11.XID fontSet, IntPtr x11gc, X11.TInt x, X11.TInt y, sbyte[] text, X11.TInt length);

		// Tested: O.K.
		/// <summary>Draw the specified text with the foreground pixel.
		/// When the fontSet has missing charsets, each unavailable character is drawn with the default string returned by XCreateFontSet.
		/// The behavior for an invalid codepoint is undefined.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11drawable">The drawable to draw on.<see cref="IntPtr"/></param>
		/// <param name="fontSet">The fontset to use for drawing.<see cref="X11.XID"/></param>
		/// <param name="x11gc">The crapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The leftmost x-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="y">The baseline y-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="text">The text to draw.<see cref="X11.TWchar[]"/></param>
		/// <param name="length">The length of the text to draw (as number of wchars).<see cref="X11.TInt"/></param>
        [DllImport ("libX11")]
        extern public static void XwcDrawString   (IntPtr x11display, IntPtr x11drawable, X11.XID fontSet, IntPtr x11gc, X11.TInt x, X11.TInt y, X11.TWchar[] text, X11.TInt length);

		// Tested: O.K.
		/// <summary>Draw the specified text with the foreground pixel.
		/// When the fontSet has missing charsets, each unavailable character is drawn with the default string returned by XCreateFontSet.
		/// The behavior for an invalid codepoint is undefined.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11drawable">The drawable to draw on.<see cref="IntPtr"/></param>
		/// <param name="fontSet">The fontset to use for drawing.<see cref="X11.XID"/></param>
		/// <param name="x11gc">The crapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The leftmost x-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="y">The maseline y-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="text">The text to draw.<see cref="System.SByte[]"/></param>
		/// <param name="length">The length of the text to draw (as number of bytes).<see cref="X11.TInt"/></param>
        [DllImport ("libX11")]
        extern public static void Xutf8DrawString (IntPtr x11display, IntPtr x11drawable, X11.XID fontSet, IntPtr x11gc, X11.TInt x, X11.TInt y, sbyte[] text, X11.TInt length);
		
		#endregion String drawing methods
		
		// ##########################################################################################################
		// ###   K E Y B O A R D   I N P U T
		// ##########################################################################################################
		
		#region Keyboard input methods
		
		[Flags]
		public enum XimStyle : long
		{
			XIMPreeditArea		= 0x0001L,
			XIMPreeditCallbacks	= 0x0002L,
			XIMPreeditPosition	= 0x0004L,
			XIMPreeditNothing	= 0x0008L,
			XIMPreeditNone		= 0x0010L,
			XIMStatusArea		= 0x0100L,
			XIMStatusCallbacks	= 0x0200L,
			XIMStatusNothing	= 0x0400L,
			XIMStatusNone		= 0x0800L
		}
		
		private struct _XIMStyles
		{
    		public TUshort  count_styles;
    		public IntPtr	supported_styles;	// XIMStyle*
			
			public static _XIMStyles Zero = new _XIMStyles ();
		}
		
		// Tested: O.K.
		/// <summary> The possible X*LookupString() status. </summary>
		public enum LookupStringStatus
		{
			/// <summary> X*LookupString() result buffer too small. </summary>
			XBufferOverflow	= -1,
			
			/// <summary> X*LookupString() found nothing to process. </summary>
			XLookupNone		= 1,
			
			/// <summary> X*LookupString() provides character to process. </summary>
			XLookupChars	= 2,
			
			/// <summary> X*LookupString() provides key sym (special keys) to process. </summary>
			XLookupKeySym	= 3,
			
			/// <summary> X*LookupString() provides character and key sym (special keys) to process. </summary>
			XLookupBoth		= 4
		}
		
		// Tested: O.K.
		/// <summary> Open an input method, matching the current locale and modifiers specification.
		/// Current locale and modifiers are bound to the input method at opening time.
		/// The locale associated with an input method cannot be changed dynamically.
		/// This implies that the strings returned by XmbLookupString or XwcLookupString,
		/// for any input context affiliated with a given input method will be encoded in the locale
		/// current at the time the input method is opened.
		/// XOpenIM will identify a default input method corresponding to the current locale.
		/// That default can be modified using XSetLocaleModifiers for the input method modifier. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="resourceDb"> Specifies a pointer to the resource database. <see cref="IntPrt"/> </param>
		/// <param name="resName"> Specifies the full resource name of the application. <see cref="System.String"/> </param>
		/// <param name="resClass"> Specifies the full class name of the application. <see cref="System.String"/> </param>
		/// <returns> The input method on success, or IntPrt.Zero otherwise. <see cref="IntPrt"/> </returns>
		/// <remarks> The resourceDb argument is the resource database to be used by the input method for looking up resources
		/// that are private to the input method. It is not intended that this database be used to look up values
		/// that can be set as IC values in an input context. If db is NULL, no database is passed to the input method. </remarks>
		/// <remarks> The resName and resClass arguments specify the resource name and class of the application.
		/// They are intended to be used as prefixes by the input method when looking up resources that are common to all input
		/// contexts that may be created for this input method. The characters used for resource names and classes must be in the
		/// X Portable Character Set. The resources looked up are not fully specified if resName or resClass is IntPrt.Zero. </remarks>
		[DllImport("libX11")]
		extern public static IntPtr XOpenIM (IntPtr x11display, IntPtr resourceDb, [MarshalAs(UnmanagedType.LPStr)] string resName, [MarshalAs(UnmanagedType.LPStr)] string resClass);
		
		// Tested: O.K.
		/// <summary> Close the specified input method. </summary>
		/// <param name="inputMethod"> The input method to close. <see cref="IntPrt"/> </param>
		/// <returns> The result status. <see cref="TInt"/> </returns>
		[DllImport("libX11")]
		extern public static TInt XCloseIM (IntPtr inputMethod);
		
		// Tested: O.K.
		/// <summary> Query properties or features of the specified input method, using a variable argument list. </summary>
		/// <param name="inputMethod"> The input method to get properties or features for. <see cref="IntPtr"/> </param>
		/// <param name="valueName"> The value name of the property or feature to query for. <see cref="System.String"/> </param>
		/// <param name="valueReturn"> The property or feature query result. <see cref="IntPtr"/> </param>
		/// <param name="end"> The variable argument list end (IntPtr.Zero). <see cref="IntPtr"/> </param>
		/// <returns> IntPtr.Zero on success, or the name of the first argument that could not be set. <see cref="IntPtr"/> </returns>
		[DllImport("libX11", EntryPoint = "XGetIMValues")]
		extern private static IntPtr _XGetIMValues (IntPtr inputMethod, [MarshalAs(UnmanagedType.LPStr)] string valueName, IntPtr valueReturn, IntPtr end);
		
		// Tested: O.K.
		/// <summary> Query the input method's supported styles. </summary>
		/// <param name="inputMethod"> The input method to query the supported styles for. <see cref="IntPtr"/> </param>
		/// <param name="valueReturn"> The supported styles on successful query. <see cref="X11lib.XimStyle[]"/> </param>
		/// <returns> True on successful query, or false otherwise. <see cref="System.Boolean"/> </returns>
		public static bool XGetIMValueSupportedStyles (IntPtr inputMethod, out X11lib.XimStyle[] valueReturn)
		{
			IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(IntPtr.Zero));
			IntPtr firstErrorValueType = _XGetIMValues (inputMethod, "queryInputStyle", p, IntPtr.Zero);
			
			if (firstErrorValueType != IntPtr.Zero)
			{
				Marshal.FreeHGlobal (p);
				valueReturn = new X11lib.XimStyle[] { X11lib.XimStyle.XIMPreeditNone };
				return false;
			}
			else
			{
				IntPtr pDeref = Marshal.ReadIntPtr (p);
				_XIMStyles styles = (_XIMStyles) Marshal.PtrToStructure (pDeref, typeof(_XIMStyles));
				valueReturn = new X11lib.XimStyle[(int)styles.count_styles];
				for (int count = 0; count < (int)styles.count_styles; count++)
				{
					TUlong result = 0;
					if (X11.Interop.CurrentPlatform() == Interop.Platform.X86)
						result = (TUlong)Marshal.ReadInt32 (styles.supported_styles, count * sizeof(TLong));
					else
						result = (TUlong)Marshal.ReadInt64 (styles.supported_styles, count * sizeof(TLong));
					
					valueReturn[count] = (X11lib.XimStyle)result;
				}
				XFree (pDeref);
				Marshal.FreeHGlobal (p);
				return true;
			}
		}
		
		// Tested: O.K.
		/// <summary> Create an input context within the specified input method, using a variable argument list. </summary>
		/// <param name="inputMethod"> The input method to create an input context for. <see cref="IntPtr"/> </param>
		/// <param name="valueName1"> The value name of the first variable argument. <see cref="System.String"/> </param>
		/// <param name="value1"> The value of the first variable argument. <see cref="IntPtr"/> </param>
		/// <param name="valueName2"> The value name of the second variable argument. <see cref="System.String"/> </param>
		/// <param name="value2"> The value of the second variable argument. <see cref="IntPtr"/> </param>
		/// <param name="end"> The variable argument list end (IntPtr.Zero). <see cref="IntPtr"/> </param>
		/// <returns> The input context on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		[DllImport("libX11", EntryPoint = "XCreateIC")]
		extern private static IntPtr _XCreateIC (IntPtr inputMethod, [MarshalAs(UnmanagedType.LPStr)] string valueName1, IntPtr value1, [MarshalAs(UnmanagedType.LPStr)] string valueName2, IntPtr value2, IntPtr end); 
		
		// Tested: O.K.
		/// <summary> Create an input context within the specified input method. </summary>
		/// <param name="inputMethod"> The input method to create an input context for. <see cref="IntPtr"/> </param>
		/// <param name="inputStyle"> The input style to use. <see cref="System.Int64"/> </param>
		/// <param name="clientWindow"> The client window to create an input context for. <see cref="IntPtr"/> </param>
		/// <returns> The input context on succes, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		public static IntPtr XCreateIC (IntPtr inputMethod, long inputStyle, IntPtr clientWindow)
		{
			IntPtr inputContext = _XCreateIC (inputMethod, "inputStyle", (IntPtr) inputStyle, "clientWindow", clientWindow, IntPtr.Zero);
			return inputContext;
		}
		
		// Tested: O.K.
		/// <summary> Destroy the specified input context. </summary>
		/// <param name="inputContext"> The input context to destroy. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XDestroyIC(IntPtr inputContext);		
		
		// Tested: O.K.
		/// <summary> Query input context attributes, using a variable argument list. </summary>
		/// <param name="inputContext"> The input context to get values for. <see cref="IntPtr"/> </param>
		/// <param name="valueName"> The value name of the first variable argument. <see cref="System.String"/> </param>
		/// <param name="value"> The value of the first variable argument. <see cref="IntPtr"/> </param>
		/// <param name="end"> The variable argument list end (IntPtr.Zero). <see cref="IntPtr"/> </param>
		/// <returns> The input context on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		/// <returns> IntPtr.Zero on success, or the name of the first argument that could not be set. <see cref="IntPtr"/> </returns>
		[DllImport("libX11", EntryPoint = "XGetICValues")]
		extern private static IntPtr _XGetICValues (IntPtr inputContext, [MarshalAs(UnmanagedType.LPStr)] string valueName, IntPtr valueReturn, IntPtr end);
		
		// Tested: O.K.
		/// <summary> Query input context attributes. </summary>
		/// <param name="inputContext"> The input context to get values for. <see cref="IntPtr"/> </param>
		/// <param name="filterEvents"> The filter events to query the supported filter events for. <see cref="TLong"/> </param>
		/// <param name="clientWindow"> The client window to query the supported filter events for. <see cref="IntPtr"/> </param>
		/// <returns> True on successful query, or false otherwise. <see cref="System.Boolean"/> </returns>
		public static bool XGetICValueFilterEvents (IntPtr inputContext, out TLong filterEvents, IntPtr clientWindow)
		{
			IntPtr p = Marshal.AllocHGlobal (sizeof (TLong));
			IntPtr firstErrorValueType = _XGetICValues (inputContext, "filterEvents", p, IntPtr.Zero);
			
			if (firstErrorValueType != IntPtr.Zero)
			{
				Marshal.FreeHGlobal (p);
				filterEvents = 0;
				return false;
			}
			else
			{
				if (X11.Interop.CurrentPlatform() == Interop.Platform.X86)
					filterEvents = (TLong)Marshal.ReadInt32 (p);
				else
					filterEvents = (TLong)Marshal.ReadInt64 (p);
				Marshal.FreeHGlobal (p);
				return true;
			}
		}
				
		// Tested: O.K.
		/// <summary> Allow a client to notify an input method that the focus window attached to the specified input
		/// context has received keyboard focus. The input method should take action to provide appropriate feedback.
		/// Complete feedback specification is a matter of user interface policy.
		/// Calling XSetICFocus does not affect the focus window value. </summary>
		/// <param name="inputContext"> The input contect to set the focus for.  <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XSetICFocus (IntPtr inputContext);
		
		// Tested: O.K.
		/// <summary>  Allow a client to notify an input method that the specified input context has lost the keyboard
		/// focus and that no more input is expected on the focus window attached to that input context.
		/// The input method should take action to provide appropriate feedback. Complete feedback specification
		/// is a matter of user interface policy.
		/// Calling XUnsetICFocus does not affect the focus window value; the client may still receive events from
		/// the input method that are directed to the focus window. </summary>
		/// <param name="inputContext"> The input contect to unset the focus for.  <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XUnsetICFocus (IntPtr inputContext);

		// Tested: O.K.
		/// <summary>Return the string from the input method specified in the bufferReturn argument.
		/// If no string is returned, the buffer_return argument is unchanged.</summary>
		/// <param name="inputContext">The input contect to use.<see cref="IntPtr"/></param>
		/// <param name="evt">The key event to be used.<see cref="X11.XKeyEvent"/></param>
		/// <param name="bufferReturn">The string returned (if any) from the input method.<see cref="TWchar[]"/></param>
		/// <param name="bufferLength">The space available in the return buffer.<see cref="TInt"/></param>
		/// <param name="keysym">The KeySym (encoding of a symbol on the cap of a key) computed from the event if this argument is not IntPtr.Zero.<see cref="IntPtr"/></param>
		/// <param name="status">Returns a value indicating what kind of data is returned.<see cref="TInt"/></param>
		/// <returns>The length of the string in characters.<see cref="TInt"/></returns>
		/// <remarks>Typically the KeyPress event succeeds with XwcLookupString and the KeyRelease event fails.</remarks>
		[DllImport("libX11")]
		extern public static TInt XwcLookupString (IntPtr inputContext, ref X11.XKeyEvent evt, TWchar[] bufferReturn, TInt bufferLength, ref IntPtr keysym, ref TInt status);
		
		#endregion Keyboard input methods
		
		// ##########################################################################################################
		// ###   F O N T
		// ##########################################################################################################
		
		#region Font methods
		
		// Tested: O.K.
		/// <summary> Return an array of available font names (as controlled by the font search path; see XSetFontPath()) that
		/// match the string passed to the pattern argument. The pattern string can contain any characters, but each asterisk (*)
		/// is a wildcard for any number of characters, and each question mark (?) is a wildcard for a single character. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="pattern"> The font name pattern, to apply to the requested fontlist. <see cref="System.String"/> </param>
		/// <param name="maxNames"> The maximum number of font names to be returned. <see cref="TInt"/> </param>
		/// <param name="returnedNames"> The actual number of returned font names. <see cref="TInt"/> </param>
		/// <returns> The requested list of font names on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		/// <remarks> Each returned string is null-terminated. The client should call XFreeFontNames() when finished with the result to free the memory. </remarks>
		[DllImport("libX11")]
		extern public static IntPtr XListFonts (IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string pattern, TInt maxNames, ref TInt returnedNames);
		
		// Tested: O.K.
		/// <summary> Free the array and strings returned by XListFonts() or XListFontsWithInfo(). </summary>
		/// <param name="fontNameArray"> The array of font name strings to free. <see cref="IntPtr"/> </param>
		[DllImport("libX11")]
		extern public static void XFreeFontNames (IntPtr fontNameArray);
		
		// Tested: O.K.
		/// <summary> Load the specified font and returns its associated font resource id. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="name"> The name (pattern) of the font, which is a null-terminated string. <see cref="System.String"/> </param>
		/// <returns> The font ID on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		/// <remarks> To free a font that is no longer needed, use XFreeFont(). </remarks>
		[DllImport("libX11")]
		extern public static X11.XID XLoadFont (IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string name);
		
		// Tested: O.K.
		/// <summary> Query a pointer to the XFontStruct structure, which contains information associated with the font. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="fontID"> The font ID (e. g. from XLoadFont()). <see cref="X11.XID"/> </param>
		/// <returns> The XFontStruct structure on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		/// <remarks> To free this data, use XFreeFontInfo(). </remarks>
		[DllImport("libX11")]
		extern public static IntPtr XQueryFont (IntPtr x11display, X11.XID fontID);
		
		// Tested: O.K.
		/// <summary>Load the specified font and returns its appropriate XFontStruct structure.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="name">The name (pattern) of the font, which is a null-terminated string.<see cref="System.String"/></param>
		/// <returns>The XFontStruct structure on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		/// <remarks>To free a font that is no longer needed, use XFreeFont().</remarks>
		[DllImport("libX11")]
		extern public static IntPtr XLoadQueryFont (IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string name);
		
		/// <summary>Delete the association between the font resource id and the specified font.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="fontResourceId">The font resource id to delete the association to the font.<see cref="X11.XID"/></param>
		[DllImport("libX11")]
		extern public static void XUnloadFont (IntPtr x11display, X11.XID fontResourceId);

		/// <summary>Delete the association between the font resource id and the specified font and free the XFontStruct structure.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="fontResourceId">The font resource id to delete the association to the font.<see cref="IntPtr"/></param>
		[DllImport("libX11")]
		extern public static void XFreeFont (IntPtr x11display, IntPtr fontResourceId);

		/// <summary> Associate a font to the indicated grapchics context. </summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="IntPtr"/> </param>
		/// <param name="x11gc"> The grapchics context to associate the font with. <see cref="IntPtr"/> </param>
		/// <param name="fontResourceId"> The font resource Id to associate to the grapchics context. <see cref="X11.XID"/> </param>
		[DllImport("libX11")]
		extern public static void XSetFont (IntPtr x11display, IntPtr x11gc, X11.XID fontResourceId);
		
		// Tested: O.K.
		/// <summary> Free a font structure or an array of font structures, and optionally an array of font names.
		/// If NULL is passed for names, no font names are freed. </summary>
		/// <param name="names"> The list of font names to free optionally. <see cref="IntPtr"/> </param>
		/// <param name="freeXFontStruct"> The font structure to free. <see cref="IntPtr"/> </param>
		/// <param name="nameCount"> The number of font names to free optionally. <see cref="TInt"/> </param>
		[DllImport("libX11")]
		extern public static void XFreeFontInfo (IntPtr names, IntPtr freeXFontStruct, TInt nameCount);
		
		[DllImport("libX11")]
		extern public static TBoolean XGetFontProperty (IntPtr xFontStruct, IntPtr atom, ref TUlong valueReturn);
		
		#endregion Font methods
		
		// ##########################################################################################################
		// ###   F O N T S E T
		// ##########################################################################################################
		
		#region FontSet methods
		
		// Tested: O.K.
		/// <summary>Create a font set for the specified display. The font set is bound to the current locale when XCreateFontSet () is called.
		/// The font set may be used in subsequent calls to obtain font and character information and to image text in the locale of the font set.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="baseFontNameList">The list of base font names that Xlib uses to load the fonts needed for the locale.
		/// The base font names are a comma-separated list. The string is null-terminated and is assumed to be in the Host Portable Character Encoding; otherwise,
		/// the result is implementation dependent. White space immediately on either side of a separating comma is ignored.<see cref="System.String"/></param>
		/// <param name="missingCharSetList">Returns the missing charsets.<see cref="System.IntPtr"/></param>
		/// <param name="count">Returns the number of missing charsets.<see cref="System.Int32"/></param>
		/// <param name="terminator">Returns the string drawn for missing charsets.<see cref="System.IntPtr"/></param>
		/// <returns>The created font set for the specified display.<see cref="X11.XID"/></returns>
        [DllImport ("libX11")]
        extern public static X11.XID XCreateFontSet (IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string baseFontNameList, out IntPtr list, out X11.TInt count, IntPtr terminator);
		
		// Tested: O.K.
		/// <summary>Get a list of one or more XFontStructs structures and font names for the fonts used by the Xmb and Xwc layers for the given font set.</summary>
		/// <param name="x11fontSet">Specifies the fontset to obtain ne or more XFontStructs and font names for.<see cref="X11.XID"/></param>
		/// <param name="fontStructArray">Returns the list of XFontStructs structures.<see cref="IntPtr"/></param>
		/// <param name="fontNameArray">Returns the list of font names.<see cref="IntPtr"/></param>
		/// <returns>The number of XFontStruct structures and font names.<see cref="X11.TInt"/></returns>
		/// <remarks>A list of pointers to the XFontStruct structures is returned to fontStructArray.
		/// A list of pointers to null-terminated, fully specified font name strings in the locale of the font set is returned to fontNameArray.
		/// The fontNameArray order corresponds to the fontStructArray order. </remarks>
		[DllImport("libX11", EntryPoint = "XFontsOfFontSet")]
		extern private static X11.TInt _XFontsOfFontSet (X11.XID xFontSet, ref IntPtr fontStructArray, ref IntPtr fontNameArray);
		public static int XFontsOfFontSet (X11.XID xFontSet, out X11lib.XFontStruct[] fontStructArray, out string[] fontNameArray)
		{
			IntPtr fontStructListReturn = IntPtr.Zero;
			IntPtr fontNameListReturn   = IntPtr.Zero;
			int result = (int)_XFontsOfFontSet (xFontSet, ref fontStructListReturn, ref fontNameListReturn);
			
			fontStructArray	= new X11lib.XFontStruct[result];
			for (int countFontStruct = 0; countFontStruct < result; countFontStruct++)
			{
				IntPtr pfs = Marshal.ReadIntPtr (fontStructListReturn, countFontStruct * Marshal.SizeOf(typeof(IntPtr)));
				fontStructArray[countFontStruct] = (X11lib.XFontStruct)Marshal.PtrToStructure (pfs, typeof(X11lib.XFontStruct));
			}
			
			fontNameArray	= new string[result];
			for (int countFontName = 0; countFontName < result; countFontName++)
			{
				IntPtr pfn = Marshal.ReadIntPtr (fontNameListReturn, countFontName * Marshal.SizeOf(typeof(IntPtr)));
				fontNameArray[countFontName] = Marshal.PtrToStringAuto (pfn);
			}

			return result;
		}

		// Tested: O.K.
		/// <summary>Free a font set.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="fontset">Specifies the font set to free.<see cref="X11.XID"/></param>
		/// <remarks>Frees the associated base font name list, font name list, XFontStruct list, and XFontSetExtents(),  if any, as well.</remarks>
        [DllImport ("libX11")]
        extern public static void XFreeFontSet (IntPtr x11display, X11.XID fontset);
		
		#endregion FontSet methods
		
		// ##########################################################################################################
		// ###   F O N T S E T
		// ##########################################################################################################
		
		#region RenderingExtensions methods
		
		// Tested: O.K.
		/// <summary>Return a list (char**) of all extensions supported by the server.
		/// If the data returned by the server is in the Latin Portable Character Encoding, then the returned strings
		/// are in the Host Portable Character Encoding. Otherwise, the result is implementation dependent. </summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="numExtensions">The number of extensions listed.</param>
		/// <returns>The (char**) list of extension names on success.<see cref="IntPtr"/></returns>
		/// <remarks>Don't forget to call XFreeExtensionList().</remarks>
		[DllImport ("libX11", EntryPoint = "XListExtensions")]
        extern public static IntPtr _XListExtensions (IntPtr x11display, out X11.TInt numExtensions);
		
		// Tested: O.K.
		/// <summary>Return a list (line-break separated) of all extensions supported by the server.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="nameExtensions">The list (line-break separated) of all extensions supported by the server.<see cref="System.String"/></param>
		/// <returns>The (char**) list of extension names on success.<see cref="IntPtr"/></returns>
        /// <remarks>Don't forget to call XFreeExtensionList().</remarks>
		public static IntPtr XListExtensions (IntPtr x11display, out string nameExtensions)
		{
			if (x11display != IntPtr.Zero)
			{
				X11.TInt numExtensions = 0;
				IntPtr pListExtensionNames = X11lib._XListExtensions (x11display, out numExtensions);
				if (pListExtensionNames != IntPtr.Zero)
				{
					IntPtr pDeref			= Marshal.ReadIntPtr (pListExtensionNames);
					int    pOffset          = 0;
					string extensionName	= "";
					
					nameExtensions = "";
					
					for (int count = 0; count < (int)numExtensions; count++)
					{
						extensionName = Marshal.PtrToStringAuto ((IntPtr)((int)pDeref + pOffset));
						pOffset += sizeof(X11.TChar) * extensionName.Length + 1;
						
						if (nameExtensions == "")
							nameExtensions  = extensionName;
						else
							nameExtensions += "\n" + extensionName;
					}
				}
				else
					nameExtensions = String.Empty;
				
				return pListExtensionNames;
			}
			else
			{
				nameExtensions = String.Empty;
				return IntPtr.Zero;
			}
		}
		
		// Tested: O.K.
		/// <summary>Free the memory allocated by XListExtensions(). </summary>
		/// <param name="listExtensions">Specifies the list of extension names. <see cref="IntPtr"/></param>
		[DllImport ("libX11")]
        extern public static void XFreeExtensionList(IntPtr listExtensions);
		
		// Tested: O.K.
		/// <summary>Determine whether the named extension is present.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="name">The extension name.<see cref="System.String"/></param>
		/// <param name="majorOpcode">The major opcode for the indicated extension on success, or zero otherwise.
		/// Any minor opcode and the request formats are specific to the extension.<see cref="X11.TInt"/></param>
		/// <param name="firstEvent">If the extension involves additional event types, the base event type code on success, or zero otherwise.
		/// The format of the events is specific to the extension.<see cref="X11.TInt"/>T</param>
		/// <param name="firstError">If the extension involves additional error codes, the base error code on success, or zero otherwise.
		/// The format of additional data in the errors is specific to the extension.<see cref="X11.TInt"/></param>
		/// <returns>True if the indicated extension is not present, or false otherwise.<see cref="System.Boolean"/></returns>
		[DllImport ("libX11")]
        extern public static bool XQueryExtension(IntPtr x11display, [MarshalAs(UnmanagedType.LPStr)] string name,
		                                          out TInt majorOpcode, out TInt firstEvent, out TInt firstError);

		#endregion RenderingExtensions methods

		// ##########################################################################################################
		// ###   L I B C
		// ##########################################################################################################
		
		#region libc methods
		
		// Tested: O.K.
		/// <summary>This function is used to set or query the program's current locale.</summary>
		/// <param name="category">Determine which parts of the program's current locale should be modified.<see cref="TInt"/></param>
		/// <param name="locale">The parts of the program's current locale to be set.<see cref="TChar[]"/></param>
		/// <returns>The current (new) locale on success, or IntPtr.Zero on a wrong/unsupported 'locale' string.
		/// The return string is owned by the runtime library - DO NOT FREE OR MANIPULATE it.<see cref="IntPtr"/></returns>
		/// <remarks>The effects, a locale has:
		/// - What multibyte character sequences are valid, and how they are interpreted.
		/// - Classification of which characters in the local character set are considered alphabetic, and upper- and lower-case conversion conventions.
		/// - The collating sequence for the local language and character set (see section Collation Functions).
		/// - Formatting of numbers and currency amounts.
		/// - Formatting of dates and times (see section Formatting Date and Time).
		/// - What language to use for output, including error messages. (The C library doesn't yet help you implement this.)
		/// - What language to use for user answers to yes-or-no questions.
		/// - What language to use for more complex user input. (The C library doesn't yet help you implement this.) 
		/// See 'http://www.cs.utah.edu/dept/old/texinfo/glibc-manual-0.02/library_7.html' for details.</remarks>
		[DllImport("libc")]
		extern public static IntPtr setlocale (TInt category, TChar[] locale);
		
		// Tested: O.K., Use System.Runtime.InteropServices.Marshal.AllocHGlobal() instead.
		/// <summary> Allocate unused space for an object whose size in bytes is specified by size and whose value is unspecified.
		/// The order and contiguity of storage allocated by successive calls to malloc() is unspecified. </summary>
		/// <param name="size"> The size of the object to allocate space for in bytes. <see cref="TUint"/> </param>
		/// <returns> Upon successful completion with size not equal to 0, malloc() shall return a pointer to the allocated space.
		/// If size is 0, either a null pointer or a unique pointer that can be successfully passed to free() shall be returned.
		/// Otherwise, it shall return a null pointer and set *** errno *** to indicate the error. <see cref="IntPtr"/> </returns>
		[DllImport("libc")]
		extern public static IntPtr malloc (TUint size);
		
		// Tested: O.K., Use System.Runtime.InteropServices.Marshal.AllocHGlobal() instead.
		/// <summary> Allocate unused space for an array of nelem elements each of whose size in bytes is elsize.
		/// The space shall be initialized to all bits 0. The order and contiguity of storage allocated by successive
		/// calls to calloc() is unspecified. </summary>
		/// <param name="elements"> The number of elements to allocate an array for. <see cref="TUint"/> </param>
		/// <param name="elementsize"> The size of any element to allocate an array for in bytes. <see cref="TUint"/> </param>
		/// <returns> Upon successful completion with both nelem and elsize non-zero, calloc() shall return a pointer
		/// to the allocated space. If either nelem or elsize is 0, then either a null pointer or a unique pointer
		/// value that can be successfully passed to free() shall be returned. Otherwise, it shall return a null pointer
		/// and set *** errno *** to indicate the error. <see cref="IntPtr"/> </returns>
		[DllImport("libc")]
		extern public static IntPtr calloc (TUint elements, TUint elementsize);
		
		// Tested: O.K., Use System.Runtime.InteropServices.Marshal.FreeHGlobal() together wit ~.AllocHGlobal() instead.
		/// <summary> ause the space pointed to by ptr to be deallocated; that is, made available for further allocation.
		/// If pointer is a null pointer, no action shall occur. </summary>
		/// <param name="pointer"> The pointer to the space to deallocate. <see cref="IntPtr"/> </param>
		[DllImport("libc")]
		extern public static void free (IntPtr pointer);
		
		#endregion libc methods
		
	}
}