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
using System.Collections.Generic;
using System.Diagnostics;

namespace X11
{
	
	/// <summary>Store brush information.</summary>
	public class X11BrushInfo : IDisposable
	{
		
        // ###############################################################################
        // ### I N N E R   C L A S S E S
        // ###############################################################################

        #region Inner classes
		
		/// <summary>Define the possible types of brushes.</summary>
		public enum BrushType
		{
			/// <summary>This brush info represents a solid color brush.</summary>
			Solid,
			
			/// <summary>This brush info represents a linear gradient brush.</summary>
			LinearGradientm,
			
			/// <summary>This brush info represents a radial gradient brush.</summary>
			RadialGradient,
			
			/// <summary>This brush info represents a hatch (stipple) brush.</summary>
			Hatch,
			
			/// <summary>This brush info represents an image brush.</summary>
			Image
		}
		
		/// <summary>Define the possible types of hatches.</summary>
		public enum HatchType
		{
			/// <summary>The horizontal lined hatch. One line per 5 scan lines. Starting at scan line 1.</summary>
			Horizontal = 0,
			
			/// <summary>The horizontal lined hatch. One line per 5 scan lines. Starting at scan line 3.</summary>
			HorizontalCenter = 1,
			
			/// <summary>The vertical lined hatch. One line per 5 scan columns. Starting at scan column 1.</summary>
			Vertical = 2,
			
			/// <summary>The vertical lined hatch. One line per 5 scan columns. Starting at scan column 3.</summary>
			VerticalCenter = 3,
			
			/// <summary>The diagonal (left-top to right-bottom) lined hatch. One line per 8 scan lines.</summary>
			ForwardDiagonal = 4,
			
			/// <summary>The diagonal (left-bottom to right-top) lined hatch. One line per 8 scan lines.</summary>
			BackwardDiagonal = 5,
			
			/// <summary>The diagonal cross lined hatch. One forward diagonal line per 8 scan lines. One backward diagonal line per 8 scan lines.</summary>
			Cross			= 6,
			
			CrossCenter		= 7,
			
			DiagonalCross	= 8,
			
			G25Percent		= 11,
			
			G50Percent,
			
			G75Percent
		}
		
		/// <summary>Store pixmap information.</summary>
		public struct PixmapInfo
		{
			/// <summary>The pixmap width.</summary>
			private X11.TUint		_width;
			
			/// <summary>The pixmap height.</summary>
			private X11.TUint		_height;

			/// <summary>The pixmap bits.</summary>
			private X11.TUchar[]	_bits;
			
			/// <summary>Initialize a new instance if System.BrushInfo.PixmapInfo.</summary>
			/// <param name="width">The pixmap width.<see cref="System.Int32"/></param>
			/// <param name="height">The pixmap height.<see cref="System.Int32"/></param>
			/// <param name="bits">The pixmap bits.<see cref="X11.TUchar[]"/></param>
			public PixmapInfo (int width, int height, X11.TUchar[] bits)
			{
				_width  = (X11.TUint)width;
				_height = (X11.TUint)height;
				_bits   = bits;
			}
			
			/// <summary>Get the pixmap width.</summary>
			/// <returns>The pixmap width.<see cref="System.Int32"/></returns>
			public X11.TUint Width		{	get	{	return _width;	}	}
			
			/// <summary>Get the pixmap height.</summary>
			/// <returns>The pixmap height.<see cref="System.Int32"/></returns>
			public X11.TUint Height		{	get	{	return _height;	}	}
			
			/// <summary>Get the pixmap bits.</summary>
			/// <returns>The pixmap bits.<see cref="X11.TUchar[]"/></returns>
			public X11.TUchar[] Bits	{	get	{	return _bits;	}	}
		}
		
		/// <summary>Define a pixmap including it's transformation meta information.</summary>
		public struct TransformedPixmap
		{
			/// <summary>The pixmap to hold.</summary>
			public IntPtr Pixmap;
			
			/// <summary>The rotation angle applied the pixmap.</summary>
			public double RatationAngle;
			
			/// <summary>The zoom applied the pixmap.</summary>
			public double Zoom;
			
			/// <summary>Initialize a new struct.</summary>
			/// <param name="pixmap">The pixmap to hold.<see cref="IntPtr"/></param>
			/// <param name="ratationAngle">The rotation angle applied the pixmap.<see cref="System.Double"/></param>
			/// <param name="zoom"The zoom applied the pixmap.<see cref="System.Double"/></param>
			public TransformedPixmap (IntPtr pixmap, double ratationAngle, double zoom)
			{
				Pixmap = pixmap;
				RatationAngle = ratationAngle;
				Zoom = zoom;
			}
		}
		
        #endregion Inner classes
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string			CLASS_NAME = "BrushInfo";
		
		/// <summary>The smallest difference value that defines equality of two angle or zoom values.</summary>
		public const double			TRANSFORM_FUZZY = 0.0000000001;
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Static attributes
		
		/// <summary>The horizontal lined hatch. One line per 5 scan lines. Starting at scan line 1.</summary>
		public static PixmapInfo	HatchHorizontal = new PixmapInfo (15, 15, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 02*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																    	/*line 03*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 04*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 05*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 06*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 07*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																    	/*line 08*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 09*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 10*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 11*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 12*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 13*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 14*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 15*/ (X11.TUchar)0x00, (X11.TUchar)0x00});
		
		/// <summary>The horizontal lined hatch. One line per 5 scan lines. Starting at scan line 3.</summary>
		public static PixmapInfo	HatchHorizontalCenter = new PixmapInfo (15, 15, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 02*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 03*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 04*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																    	/*line 05*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 06*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 07*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 08*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 09*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																    	/*line 10*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 11*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 12*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 13*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 14*/ (X11.TUchar)0x00, (X11.TUchar)0x00,
																		/*line 15*/ (X11.TUchar)0x00, (X11.TUchar)0x00});
		
		/// <summary>The vertical lined hatch. One line per 5 scan columns. Starting at scan column 1.</summary>
		public static PixmapInfo	HatchVertical = new PixmapInfo (15, 15, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 02*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 03*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 04*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 05*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 06*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 07*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 08*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 09*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 10*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 11*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 12*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 13*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 14*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 15*/ (X11.TUchar)0x21, (X11.TUchar)0x04});
		
		/// <summary>The vertical lined hatch. One line per 5 scan columns. Starting at scan column 3.</summary>
		public static PixmapInfo	HatchVerticalCenter = new PixmapInfo (15, 15, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 02*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 03*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 04*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 05*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 06*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 07*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 08*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 09*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 10*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 11*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 12*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 13*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 14*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 15*/ (X11.TUchar)0x84, (X11.TUchar)0x10});
		
		/// <summary>The diagonal (left-top to right-bottom) lined hatch. One line per 8 scan lines.</summary>
		public static PixmapInfo	HatchForwardDiagonal = new PixmapInfo (16, 16, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x01, (X11.TUchar)0x01,
																		/*line 02*/ (X11.TUchar)0x02, (X11.TUchar)0x02,
																		/*line 03*/ (X11.TUchar)0x04, (X11.TUchar)0x04,
																		/*line 04*/ (X11.TUchar)0x08, (X11.TUchar)0x08,
																		/*line 05*/ (X11.TUchar)0x10, (X11.TUchar)0x10,
																		/*line 06*/ (X11.TUchar)0x20, (X11.TUchar)0x20,
																		/*line 07*/ (X11.TUchar)0x40, (X11.TUchar)0x40,
																		/*line 08*/ (X11.TUchar)0x80, (X11.TUchar)0x80,
																		/*line 09*/ (X11.TUchar)0x01, (X11.TUchar)0x01,
																		/*line 10*/ (X11.TUchar)0x02, (X11.TUchar)0x02,
																		/*line 11*/ (X11.TUchar)0x04, (X11.TUchar)0x04,
																		/*line 12*/ (X11.TUchar)0x08, (X11.TUchar)0x08,
																		/*line 13*/ (X11.TUchar)0x10, (X11.TUchar)0x10,
																		/*line 14*/ (X11.TUchar)0x20, (X11.TUchar)0x20,
																		/*line 15*/ (X11.TUchar)0x40, (X11.TUchar)0x40,
																		/*line 16*/ (X11.TUchar)0x80, (X11.TUchar)0x80});
		
		/// <summary>The diagonal (left-bottom to right-top) lined hatch. One line per 8 scan lines.</summary>
		public static PixmapInfo	HatchBackwardDiagonal = new PixmapInfo (16, 16, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x80, (X11.TUchar)0x80,
																		/*line 02*/ (X11.TUchar)0x40, (X11.TUchar)0x40,
																		/*line 03*/ (X11.TUchar)0x20, (X11.TUchar)0x20,
																		/*line 04*/ (X11.TUchar)0x10, (X11.TUchar)0x10,
																		/*line 05*/ (X11.TUchar)0x08, (X11.TUchar)0x08,
																		/*line 06*/ (X11.TUchar)0x04, (X11.TUchar)0x04,
																		/*line 07*/ (X11.TUchar)0x02, (X11.TUchar)0x02,
																		/*line 08*/ (X11.TUchar)0x01, (X11.TUchar)0x01,
																		/*line 09*/ (X11.TUchar)0x80, (X11.TUchar)0x80,
																		/*line 10*/ (X11.TUchar)0x40, (X11.TUchar)0x40,
																		/*line 11*/ (X11.TUchar)0x20, (X11.TUchar)0x20,
																		/*line 12*/ (X11.TUchar)0x10, (X11.TUchar)0x10,
																		/*line 13*/ (X11.TUchar)0x08, (X11.TUchar)0x08,
																		/*line 14*/ (X11.TUchar)0x04, (X11.TUchar)0x04,
																		/*line 15*/ (X11.TUchar)0x02, (X11.TUchar)0x02,
																		/*line 16*/ (X11.TUchar)0x01, (X11.TUchar)0x01});
		
		/// <summary>The diagonal cross lined hatch. One forward diagonal line per 8 scan lines. One backward diagonal line per 8 scan lines.</summary>
		public static PixmapInfo	HatchDiagonalCross = new PixmapInfo (16, 16, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x81, (X11.TUchar)0x81,
																		/*line 02*/ (X11.TUchar)0x42, (X11.TUchar)0x42,
																		/*line 03*/ (X11.TUchar)0x24, (X11.TUchar)0x24,
																		/*line 04*/ (X11.TUchar)0x18, (X11.TUchar)0x18,
																		/*line 05*/ (X11.TUchar)0x18, (X11.TUchar)0x18,
																		/*line 06*/ (X11.TUchar)0x24, (X11.TUchar)0x24,
																		/*line 07*/ (X11.TUchar)0x42, (X11.TUchar)0x42,
																		/*line 08*/ (X11.TUchar)0x81, (X11.TUchar)0x81,
																		/*line 09*/ (X11.TUchar)0x81, (X11.TUchar)0x81,
																		/*line 10*/ (X11.TUchar)0x42, (X11.TUchar)0x42,
																		/*line 11*/ (X11.TUchar)0x24, (X11.TUchar)0x24,
																		/*line 12*/ (X11.TUchar)0x18, (X11.TUchar)0x18,
																		/*line 13*/ (X11.TUchar)0x18, (X11.TUchar)0x18,
																		/*line 14*/ (X11.TUchar)0x24, (X11.TUchar)0x24,
																		/*line 15*/ (X11.TUchar)0x42, (X11.TUchar)0x42,
																		/*line 16*/ (X11.TUchar)0x81, (X11.TUchar)0x81});
		
		/// <summary>The cross lined hatch. One horizontal line per 5 scan lines. One vertical line per 5 scan columns.</summary>
		public static PixmapInfo	HatchCross = new PixmapInfo (15, 15, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 02*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 03*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 04*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 05*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 06*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 07*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 08*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 09*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 10*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 11*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 12*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 13*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 14*/ (X11.TUchar)0x21, (X11.TUchar)0x04,
																		/*line 15*/ (X11.TUchar)0x21, (X11.TUchar)0x04});
		
		/// <summary>The cross lined hatch. One horizontal line per 5 scan lines. One vertical line per 5 scan columns.</summary>
		public static PixmapInfo	HatchCrossCenter = new PixmapInfo (15, 15, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4, 13 14 15 16 9 10 11 12 */
																		/*line 01*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 02*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 03*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 04*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 05*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 06*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 07*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 08*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 09*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 10*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 11*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 12*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 13*/ (X11.TUchar)0xff, (X11.TUchar)0x7f,
																		/*line 14*/ (X11.TUchar)0x84, (X11.TUchar)0x10,
																		/*line 15*/ (X11.TUchar)0x84, (X11.TUchar)0x10});
		
		/// <summary>The diagonal cross lined hatch. One forward diagonal line per 8 scan lines. One backward diagonal line per 8 scan lines.</summary>
		public static PixmapInfo	Hatch25Percent = new PixmapInfo (8, 8, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 48 */
																		/*line 01*/ (X11.TUchar)0xAA,
																		/*line 02*/ (X11.TUchar)0x00,
																		/*line 03*/ (X11.TUchar)0xAA,
																		/*line 04*/ (X11.TUchar)0x00,
																		/*line 05*/ (X11.TUchar)0xAA,
																		/*line 06*/ (X11.TUchar)0x00,
																		/*line 07*/ (X11.TUchar)0xAA,
																		/*line 08*/ (X11.TUchar)0x00});
		
		/// <summary>The diagonal cross lined hatch. One forward diagonal line per 8 scan lines. One backward diagonal line per 8 scan lines.</summary>
		public static PixmapInfo	Hatch50Percent = new PixmapInfo (8, 8, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 4 */
																		/*line 01*/ (X11.TUchar)0xAA,
																		/*line 02*/ (X11.TUchar)0x55,
																		/*line 03*/ (X11.TUchar)0xAA,
																		/*line 04*/ (X11.TUchar)0x55,
																		/*line 05*/ (X11.TUchar)0xAA,
																		/*line 06*/ (X11.TUchar)0x55,
																		/*line 07*/ (X11.TUchar)0xAA,
																		/*line 08*/ (X11.TUchar)0x55});
		
		/// <summary>The diagonal cross lined hatch. One forward diagonal line per 8 scan lines. One backward diagonal line per 8 scan lines.</summary>
		public static PixmapInfo	Hatch62Percent = new PixmapInfo (8, 8, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 48 */
																		/*line 01*/ (X11.TUchar)0xAA,
																		/*line 02*/ (X11.TUchar)0x57,
																		/*line 03*/ (X11.TUchar)0xAA,
																		/*line 04*/ (X11.TUchar)0x75,
																		/*line 05*/ (X11.TUchar)0xAA,
																		/*line 06*/ (X11.TUchar)0x57,
																		/*line 07*/ (X11.TUchar)0xAA,
																		/*line 08*/ (X11.TUchar)0x75});
		
		/// <summary>The diagonal cross lined hatch. One forward diagonal line per 8 scan lines. One backward diagonal line per 8 scan lines.</summary>
		public static PixmapInfo	Hatch75Percent = new PixmapInfo (8, 8, new X11.TUchar[] {
																		/*     cols  5 6 7 8 1 2 3 48 */
																		/*line 01*/ (X11.TUchar)0xEE,
																		/*line 02*/ (X11.TUchar)0xBB,
																		/*line 03*/ (X11.TUchar)0xEE,
																		/*line 04*/ (X11.TUchar)0xBB,
																		/*line 05*/ (X11.TUchar)0xEE,
																		/*line 06*/ (X11.TUchar)0xBB,
																		/*line 07*/ (X11.TUchar)0xEE,
																		/*line 08*/ (X11.TUchar)0xBB});
		
		#endregion Static attributes

		#region Attributes
		
		/// <summary> Indicate whether Dispose() has already been called. </summary>
		protected bool					_disposed		= false;
		
		/// <summary>The type of brush, represented by this brush info.</summary>
		private BrushType				_brushType		= BrushType.Solid;
		
		/// <summary>The color value, if known.</summary>
		private X11.TColor				_color;

		/// <summary>The type of hatch, represented by this brush info.</summary>
		private HatchType				_hatchType		= HatchType.Horizontal;

		/// <summary>The bitmap (one bit) for hatch fill, represented by this brush info.</summary>
		private IntPtr					_hatchBitmap	= IntPtr.Zero;

		/// <summary>The display associated to the hatch bitmap, represented by this brush info.</summary>
		private IntPtr					_hatchDisplay	= IntPtr.Zero;
		
		/// <summary>The graphic to cteate the tile pixmap (for tile fill) from.</summary>
		private X11.X11Graphic			_tileGraphic	= null;
		
		/// <summary>List of already created, potential transformed, pixmap. A pixmap, once assigned to a GC, can't be deleted before it has been unassigned.</summary>
		/// <remarks>This list prevents deletion of pixmaps, that might still be assigned to a GC and it provides the recall of already created pixmaps.</remarks>
		private List<TransformedPixmap>	_tileStockPixmaps = new List<TransformedPixmap> ();
		
		/// <summary>The currently active pixmap (color depth) for tile fill, represented by this brush info.</summary>
		private IntPtr					_currentTilePixmap		= IntPtr.Zero;

		/// <summary>The display associated to the pixmap, represented by this brush info.</summary>
		private IntPtr					_tilePixmapDisplay	= IntPtr.Zero;
		
		/// <summary>The rotation angle, in radiants counter-clockwise, to apply to the pixmap (color depth) for tile fill, represented by this brush info.</summary>
		private double					_tilePixmapRotation	= 0.0;
		
		/// <summary>Indicate whether current tile pixmap is invalid due to any change in transformation.</summary>
		private bool					_currentTilePixmapInvalid = false;
		
		/// <summary>The offset to apply to hatch bitmap (for hatch brush) or tile pixmap (for tile brush),
		/// as long as neither x nor y offset are double.NaN.</summary>
		private System.Windows.Vector	_imageOffset	= new System.Windows.Vector (double.NaN, double.NaN);
		
		/// <summary>The image source uri, if available.</summary>
		private string					_imageUri;
		
		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific color.</summary>
		/// <param name="type">The type value.<see cref="System.X11BrushInfo.BrushType"/></param>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		private X11BrushInfo (BrushType type, X11.TColor color)
		{
			_brushType = type;
			_color = color;
		}
		
		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific color.</summary>
		/// <param name="type">The type value.<see cref="System.X11BrushInfo.BrushType"/></param>
		/// <param name="hatchType">The hatch to apply.<see cref="HatchType"/></param>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		private X11BrushInfo (BrushType type, HatchType hatchType, X11.TColor color)
		{
			_brushType = type;
			_hatchType = hatchType;
			_color = color;
		}

		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific color.</summary>
		/// <param name="type">The type value.<see cref="System.X11BrushInfo.BrushType"/></param>
		/// <param name="image">The graphic to cteate the tile pixmap for tile fill from.<see cref="X11.X11Graphic"/></param>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		private X11BrushInfo (BrushType type, X11.X11Graphic image, X11.TColor color)
		{
			_brushType = type;
			_tileGraphic = image;
			_color = color;
		}

        #endregion Construction

        #region Initialization
		
		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific color.</summary>
		/// <param name="colorName">The color name.<see cref="System.String"/></param>
		public static X11BrushInfo SolidBrushFromColorName (string colorName)
		{	return new X11BrushInfo (BrushType.Solid, X11.TColor.FromName (colorName));	}
		
		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific color.</summary>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		public static X11BrushInfo SolidBrushFromColor (X11.TColor color)
		{	return new X11BrushInfo (BrushType.Solid, color);	}
		
		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific hatch bitmap and color.</summary>
		/// <param name="hatchType">The hatch to apply.<see cref="HatchType"/></param>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		/// <returns>The requested brush info.<see cref="BrushInfo"/></returns>
		/// <remarks>The HatchBrush supports bitmap offset.</remarks>
		public static X11BrushInfo HatchBrush (HatchType hatchType, X11.TColor color)
		{	return new X11BrushInfo (BrushType.Hatch, hatchType, color);	}

		/// <summary>Initialize a new instance of the System.BrushInfo structure that has specific tile pixmap and color.</summary>
		/// <param name="uriSource">The image source uri.<see cref="System.String"/></param>
		/// <param name="image">The tile image to apply.<see cref="X11Graphic"/></param>
		/// <param name="color">The color value.<see cref="X11.TColor"/></param>
		/// <returns>The requested brush info.<see cref="BrushInfo"/></returns>
		/// <remarks>The TileBrush supports bitmap offset.</remarks>
		/// <exception cref="ArgumentNullException">If 'uriSource' is null or empty string.</exception>
		public static X11BrushInfo ImageBrush (string uriSource, X11.X11Graphic image, X11.TColor color)
		{
			if (string.IsNullOrEmpty (uriSource))
				throw new ArgumentNullException ();
			
			X11BrushInfo bi = new X11BrushInfo (BrushType.Image, image, color);
			bi._imageUri = uriSource;
			return bi;
		}

        #endregion Initialization
		
        // ###############################################################################
        // ### D E S T R U C T I O N
        // ###############################################################################

        #region Destruction

		/// <summary>IDisposable implementation.</summary>
		public void Dispose ()
		{	
			SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::Dispose ()");
			
			this.Dispose (true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Internal (inheritable) dispose by parent.</summary>
        /// <param name="disposing">Determine whether Dispose() has been called by the user (true) or the runtime from inside the finalizer (false).</param>
        /// <remarks>If disposing equals false, no references to other objects shold be called.</remarks>
		protected void Dispose (bool disposing)
		{	
	        if (!_disposed)
	        {
	            if (disposing)
	            {
	                // Free OWN managed objects.
	            }
	            // Free OWN unmanaged objects.
	            // Set large fields to null.
				if (_hatchBitmap != IntPtr.Zero)
				{
					X11.X11lib.XFreePixmap (_hatchDisplay, _hatchBitmap);
					_hatchDisplay = IntPtr.Zero;
					_hatchBitmap = IntPtr.Zero;
				}
				if (_tileGraphic != null)
				{
					_tileGraphic.Dispose ();
					_tileGraphic = null;
				}
				if (_tileStockPixmaps.Count > 0 || _currentTilePixmap != IntPtr.Zero)
				{
					for (int index = _tileStockPixmaps.Count - 1; index >= 0; index--)
					{
						if (_tileStockPixmaps[index].Pixmap != IntPtr.Zero)
						{
							if (_currentTilePixmap == _tileStockPixmaps[index].Pixmap)
								_currentTilePixmap = IntPtr.Zero;
							X11.X11lib.XFreePixmap (_tilePixmapDisplay, _tileStockPixmaps[index].Pixmap);
						}
						_tileStockPixmaps.RemoveAt (index);
					}
					_tileStockPixmaps.Clear ();

					if (_currentTilePixmap != IntPtr.Zero)
						X11.X11lib.XFreePixmap (_tilePixmapDisplay, _currentTilePixmap);
					_currentTilePixmap = IntPtr.Zero;
					
					_tilePixmapDisplay = IntPtr.Zero;
				}
				
				_disposed = true;
	        }
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties
		
		/// <summary>Get the the type of brush, represented by this brush info.</summary>
		public BrushType Type
		{	get	{	return _brushType;	}	}

		/// <summary>Get the color name.</summary>
		public string ColorName
		{	get	{	return _color.ToString ();	}	}
		
		/// <summary>Get the color value.</summary>
		public X11.TColor Color
		{	get	{	return _color;	}
			set	{	_color = value;	}
		}
		
		/// <summary>Get or set the color pixel value.</summary>
		public X11.TPixel ColorPixel
		{	get	{	return _color.P;	}
			set	{	_color.P = value;	}
		}
		
		/// <summary>Get the hatch for hatch brushes.</summary>
		public PixmapInfo Hatch 
		{	get
			{
				if (_brushType != X11BrushInfo.BrushType.Hatch)
					return HatchHorizontal;
				
				if (_hatchType == X11BrushInfo.HatchType.Horizontal)
					return HatchHorizontal;
				if (_hatchType == X11BrushInfo.HatchType.HorizontalCenter)
					return HatchHorizontalCenter;
				if (_hatchType == X11BrushInfo.HatchType.Vertical)
					return HatchVertical;
				if (_hatchType == X11BrushInfo.HatchType.VerticalCenter)
					return HatchVerticalCenter;
				if (_hatchType == X11BrushInfo.HatchType.ForwardDiagonal)
					return HatchForwardDiagonal;
				if (_hatchType == X11BrushInfo.HatchType.BackwardDiagonal)
					return HatchBackwardDiagonal;
				if (_hatchType == X11BrushInfo.HatchType.DiagonalCross)
					return HatchDiagonalCross;
				if (_hatchType == X11BrushInfo.HatchType.Cross)
					return HatchCross;
				if (_hatchType == X11BrushInfo.HatchType.CrossCenter)
					return HatchCrossCenter;
				if (_hatchType == X11BrushInfo.HatchType.G25Percent)
					return Hatch25Percent;
				if (_hatchType == X11BrushInfo.HatchType.G50Percent)
					return Hatch50Percent;
				if (_hatchType == X11BrushInfo.HatchType.G75Percent)
					return Hatch75Percent;
					
				return HatchHorizontal;
			}
		}
		
		/// <summary>Get the graphic to cteate the tile pixmap (for tile fill) from.</summary>
		public X11.X11Graphic TileGraphic
		{	get	{	return _tileGraphic;	}	}
		
		/// <summary>Get or set the rotation angle, in radiants counter-clockwise, to apply to the pixmap (color depth) for tile fill, represented by this brush info.</summary>
		public double TileRotation
		{	get	{	return _tilePixmapRotation;	}
			set
			{	double newRotation = value;
				
				while (newRotation > 2 * Math.PI)
					newRotation -= 2 * Math.PI;
				while (newRotation < 0.0)
					newRotation += 2 * Math.PI;
				
				if (newRotation >= 0.0 - TRANSFORM_FUZZY && newRotation <= 0.0 + TRANSFORM_FUZZY)
					newRotation = 0.0;
				if (newRotation >= 2 * Math.PI - TRANSFORM_FUZZY && newRotation <= 2 * Math.PI + TRANSFORM_FUZZY)
					newRotation = Math.PI;
				if (newRotation >= 2 * Math.PI - TRANSFORM_FUZZY && newRotation <= 2 * Math.PI + TRANSFORM_FUZZY)
					newRotation = 0.0;
				
				if (_tilePixmapRotation != newRotation)
				{
					if (_currentTilePixmap != IntPtr.Zero)
						_currentTilePixmapInvalid = true;

					_tilePixmapRotation = newRotation;
				}
			}
		}
		
		/// <summary>Determine whether hatch bitmap (for hatch brush) or tile pixmap (for tile brush) have a valid offset.</summary>
		public bool HasImageOffset
		{	get	{	return (_imageOffset.X != double.NaN && _imageOffset.Y != double.NaN);	}	}
		
		/// <summary>Get or set the hatch bitmap (for hatch brush) or tile pixmap (for tile brush) offset.</summary>
		/// <remarks>For a valid offset the x and y coordinates must not be equal to double.NaN.</remarks>
		public System.Windows.Vector ImageOffset
		{	get	{	return _imageOffset;	}
			set	{	_imageOffset = value;	}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary>Get the 'ready to use' bitmap (1 bit depth) for hatch brush.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="X11drawableSrc">The drawable that indicates the screen.<see cref="IntPtr"/></param>
		/// <returns>The 'ready to use' bitmap (1 bit depth) for hatch brush.<see cref="IntPtr"/></returns>
		internal IntPtr HatchBitmap (IntPtr x11display, IntPtr X11drawableSrc)
		{
			if (_brushType != X11BrushInfo.BrushType.Hatch)
				return IntPtr.Zero;
			
			if (_hatchBitmap != IntPtr.Zero)
				return _hatchBitmap;
			
			if (x11display != IntPtr.Zero && X11drawableSrc != IntPtr.Zero)
			{
				PixmapInfo hatch = this.Hatch;
				
				_hatchBitmap  = X11.X11lib.XCreateBitmapFromData (x11display, X11drawableSrc, hatch.Bits, hatch.Width, hatch.Height);
				_hatchDisplay = x11display;
				return _hatchBitmap;
			}
			
			SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::HatchBitmap () Neither display nor drawable can be IntPtr.Zero.");
			return IntPtr.Zero;
		}
		
		/// <summary>Init the tile pixmap of an image brush info.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="screenNumber"> The appropriate screen number on the host server. <see cref="System.Int32"/> </param>
		/// <param name="individualColormap"> The X11 application individual colormap, if any. <see cref="IntPtr"/> </param>
		/// <param name="graphicDepth"> The depth (number of planes) of the graphic - that holds color pixel information. <see cref="TInt"/> </para>
		/// <returns>True on success, or fals otherwise.<see cref="System.Boolean"/></returns>
		public bool InitTilePixmap (IntPtr x11display, int screenNumber, IntPtr individualColormap, int graphicDepth)
		{
			if (string.IsNullOrEmpty (_imageUri))
				return false;
			
			System.Uri uri = new System.Uri (_imageUri);
			
			_tileGraphic = new X11.X11Graphic (x11display, screenNumber, individualColormap, (X11.TInt)graphicDepth, uri.AbsolutePath);
			if (_tileGraphic != null)
				return true;
			else
				return false;
		}
		
		/// <summary>Get the 'ready to use' pixmap (color depth) for tile brush, with transform applied.</summary>
		/// <param name="x11display"> The display pointer, that specifies the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <param name="X11drawableSrc">The drawable that indicates the screen.<see cref="IntPtr"/></param>
		/// <returns>The 'ready to use' bitmap (1 bit depth) for hatch brush.<see cref="IntPtr"/></returns>
		internal IntPtr TilePixmap (IntPtr x11display, IntPtr X11drawableSrc)
		{
			if (_brushType != X11BrushInfo.BrushType.Image)
				return IntPtr.Zero;
			
			if (_currentTilePixmap != IntPtr.Zero && _currentTilePixmapInvalid == false)
				return _currentTilePixmap;
			
			foreach (TransformedPixmap stockPixmap in _tileStockPixmaps)
			{
				if (_tilePixmapRotation >= stockPixmap.RatationAngle - TRANSFORM_FUZZY && _tilePixmapRotation <= stockPixmap.RatationAngle + TRANSFORM_FUZZY &&
					/*_tileZoom*/      1.0 >= stockPixmap.Zoom - TRANSFORM_FUZZY && /*_tileZoom*/      1.0 <= stockPixmap.Zoom + TRANSFORM_FUZZY)
				{
					_currentTilePixmap = stockPixmap.Pixmap;
					_currentTilePixmapInvalid = false;
					return _currentTilePixmap;
				}
			}
			
			if (x11display != IntPtr.Zero && X11drawableSrc != IntPtr.Zero)
			{
				if (_tilePixmapRotation >= - TRANSFORM_FUZZY && _tilePixmapRotation <= TRANSFORM_FUZZY)
					_currentTilePixmap  = _tileGraphic.CreateIndependentGraphicPixmap (x11display, X11drawableSrc);
				else
					_currentTilePixmap  = _tileGraphic.CreateIndependentGraphicPixmapRotated (x11display, X11drawableSrc, _tilePixmapRotation);
				_tilePixmapDisplay    = x11display;
				_tileStockPixmaps.Add (new TransformedPixmap (_currentTilePixmap, _tilePixmapRotation, 1.0));
				
				_currentTilePixmapInvalid = false;
				
				return _currentTilePixmap;
			}
			
			SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::TilePixmap () Neither display nor drawable can be IntPtr.Zero.");
			return IntPtr.Zero;
		}

		#endregion Methods
		
	}

}