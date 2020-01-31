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

namespace X11
{
	/// <summary>Define a value type to support legacy X11 drawing (using TPixel) and newer APIs (using RGB).</summary>
	/// <remarks>Keep behaviour and interface an near to System.Drawing.Color as possible.</remarks>
	public struct TColor
	{

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The X11 color pixel, suitable for the current color model (for legacy drawing).</summary>
		private TPixel			_pixel;
		
		/// <summary>Determine whether the pixel value has been initialized.</summary>
		private bool			_pixelInited;
		
		/// <summary>The alpha channel (for advanced drawing with GL or Cairo).</summary>
		/// <remarks>A value of 0 means completely transparent, a value of 255 means completely opaque.</remarks>
		private byte			_alpha;
		
		/// <summary>The red color component (for advanced drawing with GL or Cairo).</summary>
		private byte			_red;
		
		/// <summary>The green color component (for advanced drawing with GL or Cairo).</summary>
		private byte			_green;
		
		/// <summary>The blue color component (for advanced drawing with GL or Cairo).</summary>
		private byte			_blue;
		
		/// <summary>The fallback color WHITE.</summary>
		public static TColor	FallbackWhite = new TColor ((TPixel)0, 255, 255, 255);
		
		/// <summary>The fallback color BLACK.</summary>
		public static TColor	FallbackBlack = new TColor ((TPixel)1, 0, 0, 0);
		
		/// <summary>The fallback color fully transparent WHITE.</summary>
		public static TColor	FallbackTransparentWhite = new TColor ((TPixel)0, 0, 255, 255, 255);

		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction, Just like System.Drawing.Color hide the constructors.
		
		/// <summary>Initialize a new X11.TColor instance without alpha (opaque).</summary>
		/// <param name="pixel">The X11 color pixel, suitable for the current color model (for legacy drawing).<see cref="TPixel"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		private TColor (TPixel pixel, byte red, byte green, byte blue)
		{
			_pixel	= pixel;
			_pixelInited = true;
			_alpha	= 255;
			_red	= red;
			_green	= green;
			_blue	= blue;
		}
		
		/// <summary>Initialize a new X11.TColor instance with alpha (transparency).</summary>
		/// <param name="pixel">The X11 color pixel, suitable for the current color model (for legacy drawing).<see cref="TPixel"/></param>
		/// <param name="alpha">The color transparency component (0 = fully transparent, 256 = fully opaque).<see cref="System.Byte"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		private TColor (TPixel pixel, byte alpha, byte red, byte green, byte blue)
		{
			_pixel	= pixel;
			_pixelInited = true;
			_alpha	= alpha;
			_red	= red;
			_green	= green;
			_blue	= blue;
		}
		
		/// <summary>Initialize a new X11.TColor instance with alpha (transparency).</summary>
		/// <param name="alpha">The color transparency component (0 = fully transparent, 256 = fully opaque).<see cref="System.Byte"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		private TColor (byte alpha, byte red, byte green, byte blue)
		{
			_pixel	= 0;
			_pixelInited = false;
			_alpha	= alpha;
			_red	= red;
			_green	= green;
			_blue	= blue;
		}
		
		#endregion Construction	
		
        #region Initialization, Just like System.Drawing.Color provide public static initializators.
		
		/// <summary>Initialize a new X11.TColor instance with pixel value but without alpha value (initialize as completely opaque).</summary>
		/// <param name="pixel">The X11 color pixel, suitable for the current color model (for legacy drawing).<see cref="TPixel"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		public static TColor FromPRGB (TPixel pixel, byte red, byte green, byte blue)
		{	return new TColor (pixel, red, green, blue);		}
		
		/// <summary>Initialize a new X11.TColor instance with pixel value and with alpha value (transparency).</summary>
		/// <param name="pixel">The X11 color pixel, suitable for the current color model (for legacy drawing).<see cref="TPixel"/></param>
		/// <param name="alpha">The color transparency component (0 = fully transparent, 256 = fully opaque).<see cref="System.Byte"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <returns>The color.<see cref="TColor"/></returns>
		public static TColor FromPARGB (TPixel pixel, byte alpha, byte red, byte green, byte blue)
		{	return new TColor (pixel, alpha, red, green, blue);		}
		
		/// <summary>Initialize a new X11.TColor instance with alpha value (transparency) but without pixel value (must be initialized at first utilization).</summary>
		/// <param name="alpha">The color transparency component (0 = fully transparent, 256 = fully opaque).<see cref="System.Byte"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <returns>The color.<see cref="TColor"/></returns>
		public static TColor FromARGB (byte alpha, byte red, byte green, byte blue)
		{	return new TColor (alpha, red, green, blue);		}
		
		/// <summary>Initialize a new X11.TColor instance without alpha value (transparency) and without pixel value (must be initialized at first utilization).</summary>
		/// <param name="alpha">The color transparency component (0 = fully transparent, 256 = fully opaque).<see cref="System.Byte"/></param>
		/// <param name="red">The red color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="green">The green color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <param name="blue">The blue color component (for advanced drawing with GL or Cairo).<see cref="System.Byte"/></param>
		/// <returns>The color.<see cref="TColor"/></returns>
		public static TColor FromRGB (byte red, byte green, byte blue)
		{	return new TColor (255, red, green, blue);		}
		
		/// <summary>Initialize a new X11.TColor instance without alpha value (transparency) and without pixel value (must be initialized at first utilization).</summary>
		/// <param name="rgb">The red, green and blue color component .<see cref="System.Int32"/></param>
		/// <returns>The color.<see cref="TColor"/></returns>
		public static TColor FromRGB (int rgb)
		{	return new TColor (255, (byte)((rgb & 0x00FF0000) >> 16), (byte)((rgb & 0x0000FF00) >> 8), (byte)(rgb & 0x000000FF));		}
		
		/// <summary>Initialize a new X11.TColor instance with alpha value (transparency) but without pixel value (must be initialized at first utilization).</summary>
		/// <param name="colorName">The color name to initialize the color.<see cref="System.String"/></param>
		/// <returns>The color.<see cref="TColor"/></returns>
		/// <remarks>The 'colorName' must either be a HTML color name (with syntax #RGB, #RRGGBB, #AARRGGBB) or a known color name (e. g. 'Red').</remarks>
		public static TColor FromName (string colorName)
		{	if (string.IsNullOrEmpty (colorName))
				return FallbackBlack;
			
			// Support hexadecimal color with format RGB, RRGGBB or AARRGGBB
			if (colorName.Length == 4 && colorName.StartsWith ("#") ||
			    colorName.Length == 7 && colorName.StartsWith ("#") ||
			    colorName.Length == 9 && colorName.StartsWith ("#"))
			{
				byte[] colorComponents = BytesFromARGB(colorName.Substring (1));
				
				return FromARGB (colorComponents[0], colorComponents[1], colorComponents[2], colorComponents[3]);
			}
			else
			{
				foreach (X11.ColorMapping cm in X11ColorNames.ColorMap)
				{
					if (cm.Name == colorName)
					{
						return FromRGB (cm.RGB);
					}
				}
			}
			return FallbackBlack;
		}
		
		/// <summary>Parse a hexadecimal string of 3, 4, 6 or 8 characters length to a byte array of color components A, R, G and B.</summary>
		/// <param name="color">The hexadecimal string of 3, 4, 6 or 8 characters length to parse.<see cref="System.String"/></param>
		/// <returns>The color on success, or a color with fallback values A=255, R=0, G=0 and B=0 for unparsable color components.<see cref="System.Byte[]"/></returns>
		private static byte[] BytesFromARGB (string color)
		{
			byte[] result = new byte[4];
			// Initialize as completely opaque black.
			result[0] = (byte)255;
			result[1] = 0;
			result[2] = 0;
			result[2] = 0;
			
			if (color.Length != 3 && color.Length != 4 && color.Length != 6 && color.Length != 8)
			{	// throw new ArgumentException ("The 'color' must have a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
				SimpleLog.LogLine (TraceEventType.Error, "TColor::BytesFromARGB ('" + color + "'); the 'color' must have a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
				return result;
			}
			
			// Determine the syntax.
			bool withA   = (color.Length == 4 || color.Length == 8 ? true : false);
			bool twoChar = (color.Length == 6 || color.Length == 8 ? true : false);
			
			// Extract the components.
			string  sA = (withA ? (twoChar ? color.Substring (0, 2) : color.Substring (0, 1)) : "");
			string  sR = (withA ? (twoChar ? color.Substring (2, 2) : color.Substring (1, 1)) : (twoChar ? color.Substring (0, 2) : color.Substring (0, 1)));
			string  sG = (withA ? (twoChar ? color.Substring (4, 2) : color.Substring (2, 1)) : (twoChar ? color.Substring (2, 2) : color.Substring (1, 1)));
			string  sB = (withA ? (twoChar ? color.Substring (6, 2) : color.Substring (3, 1)) : (twoChar ? color.Substring (4, 2) : color.Substring (2, 1)));
			
			byte val = (byte)255;
			
			// Parse A.
			if (withA)
			{
				if (byte.TryParse (sA, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out val) != true)
				{	// throw new ArgumentException ("The 'color' must have a hexadecimal alpha component within a length of 4 (ARGB) or 8 (AARRGGBB) characters.");
					SimpleLog.LogLine (TraceEventType.Error, "TColor::BytesFromARGB ('" + color + "'); the 'color' must have a hexadecimal alpha component within a length of 4 (ARGB) or 8 (AARRGGBB) characters.");
				}
				result[0] = val;
			}
			
			// Parse R.
			val = (byte)0;
			if (byte.TryParse (sR, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out val) != true)
			{	// throw new ArgumentException ("The 'color' must have a hexadecimal red component within a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
				SimpleLog.LogLine (TraceEventType.Error, "TColor::BytesFromARGB ('" + color + "'); the 'color' must have a hexadecimal red component within a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
			}
			result[1] = val;
			
			// Parse G.
			val = (byte)0;
			if (byte.TryParse (sG, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out val) != true)
			{	// throw new ArgumentException ("The 'color' must have a hexadecimal green component within a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
				SimpleLog.LogLine (TraceEventType.Error, "TColor::BytesFromARGB ('" + color + "'); the 'color' must have a hexadecimal red component within a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
			}
			result[2] = val;
			
			// Parse B.
			val = (byte)0;
			if (byte.TryParse (sB, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out val) != true)
			{	// throw new ArgumentException ("The 'color' must have a hexadecimal blue component within a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
				SimpleLog.LogLine (TraceEventType.Error, "TColor::BytesFromARGB ('" + color + "'); the 'color' must have a hexadecimal red component within a length of 3 (RGB), 4 (ARGB); 6 (RRGGBB) or 8 (AARRGGBB) characters.");
			}
			result[3] = val;
			
			return result;
		}
		
		#endregion Initialization	
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################
		
		#region Static properties - Windows predefined colors.
 
		/// <summary>Get a system-defined color AliceBlue that has an ARGB value of #FFF0F8FF.</summary>
		public static TColor AliceBlue				{ get { return X11.TColor.FromName ("#FFF0F8FF"); } }
		
		/// <summary>Get a system-defined color AntiqueWhite that has an ARGB value of #FFFAEBD7.</summary>
		public static TColor AntiqueWhite			{ get { return X11.TColor.FromName ("#FFFAEBD7"); } }
		
		/// <summary>Get a system-defined color Aqua that has an ARGB value of #FF00FFFF.</summary>
		public static TColor Aqua					{ get { return X11.TColor.FromName ("#FF00FFFF"); } }
		
		/// <summary>Get a system-defined color Aquamarine that has an ARGB value of #FF7FFFD4.</summary>
		public static TColor Aquamarine				{ get { return X11.TColor.FromName ("#FF7FFFD4"); } }
		
		/// <summary>Get a system-defined color Azure that has an ARGB value of #FFF0FFFF.</summary>
		public static TColor Azure					{ get { return X11.TColor.FromName ("#FFF0FFFF"); } }
		
		/// <summary>Get a system-defined color Beige that has an ARGB value of #FFF5F5DC.</summary>
		public static TColor Beige					{ get { return X11.TColor.FromName ("#FFF5F5DC"); } } 
		
		/// <summary>Get a system-defined colorBisque  that has an ARGB value of #FFFFE4C4.</summary>
		public static TColor Bisque					{ get { return X11.TColor.FromName ("#FFFFE4C4"); } } 
		
		/// <summary>Get a system-defined color Black that has an ARGB value of #FF000000.</summary>
		public static TColor Black					{ get { return X11.TColor.FromName ("#FF000000"); } } 
		
		/// <summary>Get a system-defined color BlanchedAlmond that has an ARGB value of #FFFFEBCD.</summary>
		public static TColor BlanchedAlmond			{ get { return X11.TColor.FromName ("#FFFFEBCD"); } }
		
		/// <summary>Get a system-defined color Blue that has an ARGB value of #FF0000FF.</summary>
		public static TColor Blue					{ get { return X11.TColor.FromName ("#FF0000FF"); } }
		
		/// <summary>Get a system-defined color BlueViolet that has an ARGB value of #FF8A2BE2.</summary>
		public static TColor BlueViolet				{ get { return X11.TColor.FromName ("#FF8A2BE2"); } }
		
		/// <summary>Get a system-defined color Brown that has an ARGB value of #FFA52A2A.</summary>
		public static TColor Brown					{ get { return X11.TColor.FromName ("#FFA52A2A"); } }
		
		/// <summary>Get a system-defined color BurlyWood that has an ARGB value of #FFDEB887.</summary>
		public static TColor BurlyWood				{ get { return X11.TColor.FromName ("#FFDEB887"); } }
		
		/// <summary>Get a system-defined color CadetBlue that has an ARGB value of #FF5F9EA0.</summary>
		public static TColor CadetBlue				{ get { return X11.TColor.FromName ("#FF5F9EA0"); } }
		
		/// <summary>Get a system-defined color Chartreuse that has an ARGB value of #FF7FFF00.</summary>
		public static TColor Chartreuse				{ get { return X11.TColor.FromName ("#FF7FFF00"); } }
		
		/// <summary>Get a system-defined color Chocolate that has an ARGB value of #FFD2691E.</summary>
		public static TColor Chocolate				{ get { return X11.TColor.FromName ("#FFD2691E"); } }
		
		/// <summary>Get a system-defined color Coral that has an ARGB value of #FFFF7F50.</summary>
		public static TColor Coral					{ get { return X11.TColor.FromName ("#FFFF7F50"); } }
		
		/// <summary>Get a system-defined color CornflowerBlue that has an ARGB value of #FF6495ED.</summary>
		public static TColor CornflowerBlue			{ get { return X11.TColor.FromName ("#FF6495ED"); } }
		
		/// <summary>Get a system-defined color Cornsilk that has an ARGB value of #FFFFF8DC.</summary>
		public static TColor Cornsilk				{ get { return X11.TColor.FromName ("#FFFFF8DC"); } }
		
		/// <summary>Get a system-defined color Crimson that has an ARGB value of #FFDC143C.</summary>
		public static TColor Crimson				{ get { return X11.TColor.FromName ("#FFDC143C"); } }
		
		/// <summary>Get a system-defined color Cyan that has an ARGB value of #FF00FFFF.</summary>
		public static TColor Cyan					{ get { return X11.TColor.FromName ("#FF00FFFF"); } }
		
		/// <summary>Get a system-defined color DarkBlue that has an ARGB value of #FF00008B.</summary>
		public static TColor DarkBlue				{ get { return X11.TColor.FromName ("#FF00008B"); } }
		
		/// <summary>Get a system-defined color DarkCyan that has an ARGB value of #FF008B8B.</summary>
		public static TColor DarkCyan				{ get { return X11.TColor.FromName ("#FF008B8B"); } }
		
		/// <summary>Get a system-defined color DarkGoldenrod that has an ARGB value of #FFB8860B.</summary>
		public static TColor DarkGoldenrod			{ get { return X11.TColor.FromName ("#FFB8860B"); } }
		
		/// <summary>Get a system-defined color DarkGray that has an ARGB value of #FFA9A9A9.</summary>
		public static TColor DarkGray				{ get { return X11.TColor.FromName ("#FFA9A9A9"); } }
		
		/// <summary>Get a system-defined color DarkGreen that has an ARGB value of #FF006400.</summary>
		public static TColor DarkGreen				{ get { return X11.TColor.FromName ("#FF006400"); } }
		
		/// <summary>Get a system-defined color DarkKhaki that has an ARGB value of #FFBDB76B.</summary>
		public static TColor DarkKhaki				{ get { return X11.TColor.FromName ("#FFBDB76B"); } }
		
		/// <summary>Get a system-defined color DarkMagenta that has an ARGB value of #FF8B008B.</summary>
		public static TColor DarkMagenta			{ get { return X11.TColor.FromName ("#FF8B008B"); } }
		
		/// <summary>Get a system-defined color DarkOliveGreen that has an ARGB value of #FF556B2F.</summary>
		public static TColor DarkOliveGreen			{ get { return X11.TColor.FromName ("#FF556B2F"); } }
		
		/// <summary>Get a system-defined color DarkOrange that has an ARGB value of #FFFF8C00.</summary>
		public static TColor DarkOrange				{ get { return X11.TColor.FromName ("#FFFF8C00"); } }
		
		/// <summary>Get a system-defined color DarkOrchid that has an ARGB value of #FF9932CC.</summary>
		public static TColor DarkOrchid				{ get { return X11.TColor.FromName ("#FF9932CC"); } }
		
		/// <summary>Get a system-defined color DarkRed that has an ARGB value of #FF8B0000.</summary>
		public static TColor DarkRed				{ get { return X11.TColor.FromName ("#FF8B0000"); } }
		
		/// <summary>Get a system-defined color DarkSalmon that has an ARGB value of #FFE9967A.</summary>
		public static TColor DarkSalmon				{ get { return X11.TColor.FromName ("#FFE9967A"); } }
		
		/// <summary>Get a system-defined color DarkSeaGreen that has an ARGB value of #FF8FBC8F.</summary>
		public static TColor DarkSeaGreen			{ get { return X11.TColor.FromName ("#FF8FBC8F"); } }
		
		/// <summary>Get a system-defined color DarkSlateBlue that has an ARGB value of #FF483D8B.</summary>
		public static TColor DarkSlateBlue			{ get { return X11.TColor.FromName ("#FF483D8B"); } }
		
		/// <summary>Get a system-defined color DarkSlateGray that has an ARGB value of #FF2F4F4F.</summary>
		public static TColor DarkSlateGray			{ get { return X11.TColor.FromName ("#FF2F4F4F"); } }
		
		/// <summary>Get a system-defined color DarkTurquoise that has an ARGB value of #FF00CED1.</summary>
		public static TColor DarkTurquoise			{ get { return X11.TColor.FromName ("#FF00CED1"); } }
		
		/// <summary>Get a system-defined color DarkViolet that has an ARGB value of #FF9400D3.</summary>
		public static TColor DarkViolet				{ get { return X11.TColor.FromName ("#FF9400D3"); } }
		
		/// <summary>Get a system-defined color DeepPink that has an ARGB value of #FFFF1493.</summary>
		public static TColor DeepPink				{ get { return X11.TColor.FromName ("#FFFF1493"); } }
		
		/// <summary>Get a system-defined color DeepSkyBlue that has an ARGB value of #FF00BFFF.</summary>
		public static TColor DeepSkyBlue			{ get { return X11.TColor.FromName ("#FF00BFFF"); } }
		
		/// <summary>Get a system-defined color DimGray that has an ARGB value of #FF696969.</summary>
		public static TColor DimGray				{ get { return X11.TColor.FromName ("#FF696969"); } }
		
		/// <summary>Get a system-defined color DodgerBlue that has an ARGB value of #FF1E90FF.</summary>
		public static TColor DodgerBlue				{ get { return X11.TColor.FromName ("#FF1E90FF"); } }
		
		/// <summary>Get a system-defined color Firebrick that has an ARGB value of #FFB22222.</summary>
		public static TColor Firebrick				{ get { return X11.TColor.FromName ("#FFB22222"); } }
		
		/// <summary>Get a system-defined color FloralWhite that has an ARGB value of #FFFFFAF0.</summary>
		public static TColor FloralWhite			{ get { return X11.TColor.FromName ("#FFFFFAF0"); } }
		
		/// <summary>Get a system-defined color ForestGreen that has an ARGB value of #FF228B22.</summary>
		public static TColor ForestGreen			{ get { return X11.TColor.FromName ("#FF228B22"); } }
		
		/// <summary>Get a system-defined color Fuchsia that has an ARGB value of #FFFF00FF.</summary>
		public static TColor Fuchsia				{ get { return X11.TColor.FromName ("#FFFF00FF"); } }
		
		/// <summary>Get a system-defined color Gainsboro that has an ARGB value of #FFDCDCDC.</summary>
		public static TColor Gainsboro				{ get { return X11.TColor.FromName ("#FFDCDCDC"); } }
		
		/// <summary>Get a system-defined color GhostWhite that has an ARGB value of #FFF8F8FF.</summary>
		public static TColor GhostWhite				{ get { return X11.TColor.FromName ("#FFF8F8FF"); } }
		
		/// <summary>Get a system-defined color Gold that has an ARGB value of #FFFFD700.</summary>
		public static TColor Gold					{ get { return X11.TColor.FromName ("#FFFFD700"); } }
		
		/// <summary>Get a system-defined color Goldenrod that has an ARGB value of #FFDAA520.</summary>
		public static TColor Goldenrod				{ get { return X11.TColor.FromName ("#FFDAA520"); } }
		
		/// <summary>Get a system-defined color Gray that has an ARGB value of #FF808080.</summary>
		public static TColor Gray					{ get { return X11.TColor.FromName ("#FF808080"); } }
		
		/// <summary>Get a system-defined color Green that has an ARGB value of #FF008000.</summary>
		public static TColor Green					{ get { return X11.TColor.FromName ("#FF008000"); } }
		
		/// <summary>Get a system-defined color GreenYellow that has an ARGB value of #FFADFF2F.</summary>
		public static TColor GreenYellow			{ get { return X11.TColor.FromName ("#FFADFF2F"); } }
		
		/// <summary>Get a system-defined color Honeydew that has an ARGB value of #FFF0FFF0.</summary>
		public static TColor Honeydew				{ get { return X11.TColor.FromName ("#FFF0FFF0"); } }
		
		/// <summary>Get a system-defined color HotPink that has an ARGB value of #FFFF69B4.</summary>
		public static TColor HotPink				{ get { return X11.TColor.FromName ("#FFFF69B4"); } }
		
		/// <summary>Get a system-defined color IndianRed that has an ARGB value of #FFCD5C5C.</summary>
		public static TColor IndianRed				{ get { return X11.TColor.FromName ("#FFCD5C5C"); } }
		
		/// <summary>Get a system-defined color Indigo that has an ARGB value of #FF4B0082.</summary>
		public static TColor Indigo					{ get { return X11.TColor.FromName ("#FF4B0082"); } }
		
		/// <summary>Get a system-defined color Ivory that has an ARGB value of #FFFFFFF0.</summary>
		public static TColor Ivory					{ get { return X11.TColor.FromName ("#FFFFFFF0"); } }
		
		/// <summary>Get a system-defined color Khaki that has an ARGB value of #FFF0E68C.</summary>
		public static TColor Khaki					{ get { return X11.TColor.FromName ("#FFF0E68C"); } }
		
		/// <summary>Get a system-defined color Lavender that has an ARGB value of #FFE6E6FA.</summary>
		public static TColor Lavender				{ get { return X11.TColor.FromName ("#FFE6E6FA"); } }
		
		/// <summary>Get a system-defined color LavenderBlush that has an ARGB value of #FFFFF0F5.</summary>
		public static TColor LavenderBlush			{ get { return X11.TColor.FromName ("#FFFFF0F5"); } }
		
		/// <summary>Get a system-defined color LawnGreen that has an ARGB value of #FF7CFC00.</summary>
		public static TColor LawnGreen				{ get { return X11.TColor.FromName ("#FF7CFC00"); } }
		
		/// <summary>Get a system-defined color LemonChiffon that has an ARGB value of #FFFFFACD.</summary>
		public static TColor LemonChiffon			{ get { return X11.TColor.FromName ("#FFFFFACD"); } }
		
		/// <summary>Get a system-defined color LightBlue that has an ARGB value of #FFADD8E6.</summary>
		public static TColor LightBlue				{ get { return X11.TColor.FromName ("#FFADD8E6"); } }
		
		/// <summary>Get a system-defined color LightCoral that has an ARGB value of #FFF08080.</summary>
		public static TColor LightCoral				{ get { return X11.TColor.FromName ("#FFF08080"); } }
		
		/// <summary>Get a system-defined color LightCyan that has an ARGB value of #FFE0FFFF.</summary>
		public static TColor LightCyan				{ get { return X11.TColor.FromName ("#FFE0FFFF"); } }
		
		/// <summary>Get a system-defined color LightGoldenrodYellow that has an ARGB value of #FFFAFAD2.</summary>
		public static TColor LightGoldenrodYellow	{ get { return X11.TColor.FromName ("#FFFAFAD2"); } }
		
		/// <summary>Get a system-defined color LightGray that has an ARGB value of #FFD3D3D3.</summary>
		public static TColor LightGray				{ get { return X11.TColor.FromName ("#FFD3D3D3"); } }
		
		/// <summary>Get a system-defined color LightGreen that has an ARGB value of #FF90EE90.</summary>
		public static TColor LightGreen				{ get { return X11.TColor.FromName ("#FF90EE90"); } }
		
		/// <summary>Get a system-defined color LightPink that has an ARGB value of #FFFFB6C1.</summary>
		public static TColor LightPink				{ get { return X11.TColor.FromName ("#FFFFB6C1"); } }
		
		/// <summary>Get a system-defined color LightSalmon that has an ARGB value of #FFFFA07A.</summary>
		public static TColor LightSalmon			{ get { return X11.TColor.FromName ("#FFFFA07A"); } }
		
		/// <summary>Get a system-defined color LightSeaGreen that has an ARGB value of #FF20B2AA.</summary>
		public static TColor LightSeaGreen			{ get { return X11.TColor.FromName ("#FF20B2AA"); } }
		
		/// <summary>Get a system-defined color LightSkyBlue that has an ARGB value of #FF87CEFA.</summary>
		public static TColor LightSkyBlue			{ get { return X11.TColor.FromName ("#FF87CEFA"); } }
		
		/// <summary>Get a system-defined color LightSlateGray that has an ARGB value of #FF778899.</summary>
		public static TColor LightSlateGray			{ get { return X11.TColor.FromName ("#FF778899"); } }
		
		/// <summary>Get a system-defined color LightSteelBlue that has an ARGB value of #FFB0C4DE.</summary>
		public static TColor LightSteelBlue			{ get { return X11.TColor.FromName ("#FFB0C4DE"); } }
		
		/// <summary>Get a system-defined color LightYellow that has an ARGB value of #FFFFFFE0.</summary>
		public static TColor LightYellow			{ get { return X11.TColor.FromName ("#FFFFFFE0"); } }
		
		/// <summary>Get a system-defined color Lime that has an ARGB value of #FF00FF00.</summary>
		public static TColor Lime					{ get { return X11.TColor.FromName ("#FF00FF00"); } }
		
		/// <summary>Get a system-defined color LimeGreen that has an ARGB value of #FF32CD32.</summary>
		public static TColor LimeGreen				{ get { return X11.TColor.FromName ("#FF32CD32"); } }
		
		/// <summary>Get a system-defined color Linen that has an ARGB value of #FFFAF0E6.</summary>
		public static TColor Linen					{ get { return X11.TColor.FromName ("#FFFAF0E6"); } }
		
		/// <summary>Get a system-defined color Magenta that has an ARGB value of #FFFF00FF.</summary>
		public static TColor Magenta				{ get { return X11.TColor.FromName ("#FFFF00FF"); } }
		
		/// <summary>Get a system-defined color Maroon that has an ARGB value of #FF800000.</summary>
		public static TColor Maroon					{ get { return X11.TColor.FromName ("#FF800000"); } }
		
		/// <summary>Get a system-defined color MediumAquamarine that has an ARGB value of #FF66CDAA.</summary>
		public static TColor MediumAquamarine		{ get { return X11.TColor.FromName ("#FF66CDAA"); } }
		
		/// <summary>Get a system-defined color MediumBlue that has an ARGB value of #FF0000CD.</summary>
		public static TColor MediumBlue				{ get { return X11.TColor.FromName ("#FF0000CD"); } }
		
		/// <summary>Get a system-defined color MediumOrchid that has an ARGB value of #FFBA55D3.</summary>
		public static TColor MediumOrchid			{ get { return X11.TColor.FromName ("#FFBA55D3"); } }
		
		/// <summary>Get a system-defined color MediumPurple that has an ARGB value of #FF9370DB.</summary>
		public static TColor MediumPurple			{ get { return X11.TColor.FromName ("#FF9370DB"); } }
		
		/// <summary>Get a system-defined color MediumSeaGreen that has an ARGB value of #FF3CB371.</summary>
		public static TColor MediumSeaGreen			{ get { return X11.TColor.FromName ("#FF3CB371"); } }
		
		/// <summary>Get a system-defined color MediumSlateBlue that has an ARGB value of #FF7B68EE.</summary>
		public static TColor MediumSlateBlue		{ get { return X11.TColor.FromName ("#FF7B68EE"); } }
		
		/// <summary>Get a system-defined color MediumSpringGreen that has an ARGB value of #FF00FA9A.</summary>
		public static TColor MediumSpringGreen		{ get { return X11.TColor.FromName ("#FF00FA9A"); } }
		
		/// <summary>Get a system-defined color MediumTurquoise that has an ARGB value of #FF48D1CC.</summary>
		public static TColor MediumTurquoise		{ get { return X11.TColor.FromName ("#FF48D1CC"); } }
		
		/// <summary>Get a system-defined color MediumVioletRed that has an ARGB value of #FFC71585.</summary>
		public static TColor MediumVioletRed		{ get { return X11.TColor.FromName ("#FFC71585"); } }
		
		/// <summary>Get a system-defined color MidnightBlue that has an ARGB value of #FF191970.</summary>
		public static TColor MidnightBlue			{ get { return X11.TColor.FromName ("#FF191970"); } }
		
		/// <summary>Get a system-defined color MintCream that has an ARGB value of #FFF5FFFA.</summary>
		public static TColor MintCream				{ get { return X11.TColor.FromName ("#FFF5FFFA"); } }
		
		/// <summary>Get a system-defined color MistyRose that has an ARGB value of #FFFFE4E1.</summary>
		public static TColor MistyRose				{ get { return X11.TColor.FromName ("#FFFFE4E1"); } }
		
		/// <summary>Get a system-defined color Moccasin that has an ARGB value of #FFFFE4B5.</summary>
		public static TColor Moccasin				{ get { return X11.TColor.FromName ("#FFFFE4B5"); } }
		
		/// <summary>Get a system-defined color NavajoWhite that has an ARGB value of #FFFFDEAD.</summary>
		public static TColor NavajoWhite			{ get { return X11.TColor.FromName ("#FFFFDEAD"); } }
		
		/// <summary>Get a system-defined color Navy that has an ARGB value of #FF000080.</summary>
		public static TColor Navy					{ get { return X11.TColor.FromName ("#FF000080"); } }
		
		/// <summary>Get a system-defined color OldLace that has an ARGB value of #FFFDF5E6.</summary>
		public static TColor OldLace				{ get { return X11.TColor.FromName ("#FFFDF5E6"); } }
		
		/// <summary>Get a system-defined color Olive that has an ARGB value of #FF808000.</summary>
		public static TColor Olive					{ get { return X11.TColor.FromName ("#FF808000"); } }
		
		/// <summary>Get a system-defined color OliveDrab that has an ARGB value of #FF6B8E23.</summary>
		public static TColor OliveDrab				{ get { return X11.TColor.FromName ("#FF6B8E23"); } }
		
		/// <summary>Get a system-defined color Orange that has an ARGB value of #FFFFA500.</summary>
		public static TColor Orange					{ get { return X11.TColor.FromName ("#FFFFA500"); } }
		
		/// <summary>Get a system-defined color OrangeRed that has an ARGB value of #FFFF4500.</summary>
		public static TColor OrangeRed				{ get { return X11.TColor.FromName ("#FFFF4500"); } }
		
		/// <summary>Get a system-defined color Orchid that has an ARGB value of #FFDA70D6.</summary>
		public static TColor Orchid					{ get { return X11.TColor.FromName ("#FFDA70D6"); } }
		
		/// <summary>Get a system-defined color PaleGoldenrod that has an ARGB value of #FFEEE8AA.</summary>
		public static TColor PaleGoldenrod			{ get { return X11.TColor.FromName ("#FFEEE8AA"); } }
		
		/// <summary>Get a system-defined color PaleGreen that has an ARGB value of #FF98FB98.</summary>
		public static TColor PaleGreen				{ get { return X11.TColor.FromName ("#FF98FB98"); } }
		
		/// <summary>Get a system-defined color PaleTurquoise that has an ARGB value of #FFAFEEEE.</summary>
		public static TColor PaleTurquoise			{ get { return X11.TColor.FromName ("#FFAFEEEE"); } }
		
		/// <summary>Get a system-defined color PaleVioletRed that has an ARGB value of #FFDB7093.</summary>
		public static TColor PaleVioletRed			{ get { return X11.TColor.FromName ("#FFDB7093"); } }
		
		/// <summary>Get a system-defined color PapayaWhip that has an ARGB value of #FFFFEFD5.</summary>
		public static TColor PapayaWhip				{ get { return X11.TColor.FromName ("#FFFFEFD5"); } }
		
		/// <summary>Get a system-defined color PeachPuff that has an ARGB value of #FFFFDAB9.</summary>
		public static TColor PeachPuff				{ get { return X11.TColor.FromName ("#FFFFDAB9"); } }
		
		/// <summary>Get a system-defined color Peru that has an ARGB value of #FFCD853F.</summary>
		public static TColor Peru					{ get { return X11.TColor.FromName ("#FFCD853F"); } }
		
		/// <summary>Get a system-defined color Pink that has an ARGB value of #FFFFC0CB.</summary>
		public static TColor Pink					{ get { return X11.TColor.FromName ("#FFFFC0CB"); } }
		
		/// <summary>Get a system-defined color Plum that has an ARGB value of #FFDDA0DD.</summary>
		public static TColor Plum					{ get { return X11.TColor.FromName ("#FFDDA0DD"); } }
		
		/// <summary>Get a system-defined color PowderBlue that has an ARGB value of #FFB0E0E6.</summary>
		public static TColor PowderBlue				{ get { return X11.TColor.FromName ("#FFB0E0E6"); } }
		
		/// <summary>Get a system-defined color Purple that has an ARGB value of #FF800080.</summary>
		public static TColor Purple					{ get { return X11.TColor.FromName ("#FF800080"); } }
		
		/// <summary>Get a system-defined color Red that has an ARGB value of #FFFF0000.</summary>
		public static TColor Red					{ get { return X11.TColor.FromName ("#FFFF0000"); } }
		
		/// <summary>Get a system-defined color RosyBrown that has an ARGB value of #FFBC8F8F.</summary>
		public static TColor RosyBrown				{ get { return X11.TColor.FromName ("#FFBC8F8F"); } }
		
		/// <summary>Get a system-defined color RoyalBlue that has an ARGB value of #FF4169E1.</summary>
		public static TColor RoyalBlue				{ get { return X11.TColor.FromName ("#FF4169E1"); } }
		
		/// <summary>Get a system-defined color SaddleBrown that has an ARGB value of #FF8B4513.</summary>
		public static TColor SaddleBrown			{ get { return X11.TColor.FromName ("#FF8B4513"); } }
		
		/// <summary>Get a system-defined color Salmon that has an ARGB value of #FFFA8072.</summary>
		public static TColor Salmon					{ get { return X11.TColor.FromName ("#FFFA8072"); } }
		
		/// <summary>Get a system-defined color SandyBrown that has an ARGB value of #FFF4A460.</summary>
		public static TColor SandyBrown				{ get { return X11.TColor.FromName ("#FFF4A460"); } }
		
		/// <summary>Get a system-defined color SeaGreen that has an ARGB value of #FF2E8B57.</summary>
		public static TColor SeaGreen				{ get { return X11.TColor.FromName ("#FF2E8B57"); } }
		
		/// <summary>Get a system-defined color SeaShell that has an ARGB value of #FFFFF5EE.</summary>
		public static TColor SeaShell				{ get { return X11.TColor.FromName ("#FFFFF5EE"); } }
		
		/// <summary>Get a system-defined color Sienna that has an ARGB value of #FFA0522D.</summary>
		public static TColor Sienna					{ get { return X11.TColor.FromName ("#FFA0522D"); } }
		
		/// <summary>Get a system-defined color Silver that has an ARGB value of #FFC0C0C0.</summary>
		public static TColor Silver					{ get { return X11.TColor.FromName ("#FFC0C0C0"); } }
		
		/// <summary>Get a system-defined color SkyBlue that has an ARGB value of #FF87CEEB.</summary>
		public static TColor SkyBlue				{ get { return X11.TColor.FromName ("#FF87CEEB"); } }
		
		/// <summary>Get a system-defined color SlateBlue that has an ARGB value of #FF6A5ACD.</summary>
		public static TColor SlateBlue				{ get { return X11.TColor.FromName ("#FF6A5ACD"); } }
		
		/// <summary>Get a system-defined color SlateGray that has an ARGB value of #FF708090.</summary>
		public static TColor SlateGray				{ get { return X11.TColor.FromName ("#FF708090"); } }
		
		/// <summary>Get a system-defined color Snow that has an ARGB value of #FFFFFAFA.</summary>
		public static TColor Snow					{ get { return X11.TColor.FromName ("#FFFFFAFA"); } }
		
		/// <summary>Get a system-defined color SpringGreen that has an ARGB value of #FF00FF7F.</summary>
		public static TColor SpringGreen			{ get { return X11.TColor.FromName ("#FF00FF7F"); } }
		
		/// <summary>Get a system-defined color SteelBlue that has an ARGB value of #FF4682B4.</summary>
		public static TColor SteelBlue				{ get { return X11.TColor.FromName ("#FF4682B4"); } }
		
		/// <summary>Get a system-defined color Tan that has an ARGB value of #FFD2B48C.</summary>
		public static TColor Tan					{ get { return X11.TColor.FromName ("#FFD2B48C"); } }
		
		/// <summary>Get a system-defined color Teal that has an ARGB value of #FF008080.</summary>
		public static TColor Teal					{ get { return X11.TColor.FromName ("#FF008080"); } }
		
		/// <summary>Get a system-defined color Thistle that has an ARGB value of #FFD8BFD8.</summary>
		public static TColor Thistle				{ get { return X11.TColor.FromName ("#FFD8BFD8"); } }
		
		/// <summary>Get a system-defined color Tomato that has an ARGB value of #FFFF6347.</summary>
		public static TColor Tomato					{ get { return X11.TColor.FromName ("#FFFF6347"); } } 
		
		/// <summary>Get a system-defined color Transparent that has a system-defined color.</summary>
		public static TColor Transparent			{ get { return FallbackTransparentWhite; } } 
		
		/// <summary>Get a system-defined color Turquoise that has an ARGB value of #FF40E0D0.</summary>
		public static TColor Turquoise				{ get { return X11.TColor.FromName ("#FF40E0D0"); } }
		
		/// <summary>Get a system-defined color Violet that has an ARGB value of #FFEE82EE.</summary>
		public static TColor Violet					{ get { return X11.TColor.FromName ("#FFEE82EE"); } }
		
		/// <summary>Get a system-defined color Wheat that has an ARGB value of #FFF5DEB3.</summary>
		public static TColor Wheat					{ get { return X11.TColor.FromName ("#FFF5DEB3"); } }
		
		/// <summary>Get a system-defined color White that has an ARGB value of #FFFFFFFF.</summary>
		public static TColor White					{ get { return X11.TColor.FromName ("#FFFFFFFF"); } }
		
		/// <summary>Get a system-defined color WhiteSmoke that has an ARGB value of #FFF5F5F5.</summary>
		public static TColor WhiteSmoke				{ get { return X11.TColor.FromName ("#FFF5F5F5"); } }
		
		/// <summary>Get a system-defined color Yellow that has an ARGB value of #FFFFFF00.</summary>
		public static TColor Yellow					{ get { return X11.TColor.FromName ("#FFFFFF00"); } }
		
		/// <summary>Get a system-defined color YellowGreen that has an ARGB value of #FF9ACD32.</summary>
		public static TColor YellowGreen			{ get { return X11.TColor.FromName ("#FF9ACD32"); } } 
		
		#endregion Static properties
		
        #region Properties
		
		/// <summary>Get or set the X11 color pixel, suitable for the current color model (for legacy drawing).</summary>
		public TPixel P
		{	get	{	return _pixel;	}
			set	{	_pixel = value;	_pixelInited = true;}
		}
		
		/// <summary>Get or set whether the pixel value has been initialized.</summary>
		public bool IsPixelInitialized
		{	get	{	return _pixelInited;	}
		}
		
		/// <summary>Get or set the red color component (for advanced drawing with GL or Cairo).</summary>
		public byte R
		{	get	{	return _red;	}
			set	{	if (_red != value)		{	_red = value;	_pixelInited = false;	}	}
		}
		
		/// <summary>Get or set the green color component (for advanced drawing with GL or Cairo).</summary>
		public byte G
		{	get	{	return _green;	}
			set	{	if (_green != value)	{	_green = value;	_pixelInited = false;	}	}
		}
		
		/// <summary>Get or set the blue color component (for advanced drawing with GL or Cairo).</summary>
		public byte B
		{	get	{	return _blue;	}
			set	{	if (_blue != value)		{	_blue = value;	_pixelInited = false;	}	}
		}
		
		/// <summary>Get or set the color transparency component (0 = fully transparent, 255 = fully opaque).</summary>
		public byte A
		{	get	{	return _alpha;	}
			set	{	if (_alpha != value)	{	_alpha = value;	_pixelInited = false;	}	}
		}
		
		/// <summary>Get or set the color transparency component (0 = fully transparent, 255 = fully opaque).</summary>
		public int ARGB
		{	get	{	return (((int)_alpha) << 24) + (((int)_red) << 16) + (((int)_green) << 8) + ((int)_blue);	}
		}
		
		/// <summary>Get whether the color is fully transparent.</summary>
		public bool IsFullyTransparent
		{	get	{	return (_alpha == 0);	}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Methods
		
		/// <summary>Determine whether the current instance of color and a specified color have the same value.</summary>
		/// <param name="other">The color to compare with the current instance of color.<see cref="X11.TColor"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public bool Equals (TColor other)
		{	return (CompareTo (other) == 0);	}
		
		/// <summary>Determine whether the current instance of color and a specified object, which must also be a color, have the same value.</summary>
		/// <param name="other">The color to compare with the current instance of color.<see cref="System.Object"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool Equals (object other)
		{	return (CompareTo (other) == 0);	}
		
		/// <summary>Compares the current instance of color with a specified object, which must also be a color, and indicate whether this instance of color
		/// precedes, follows, or appears in the same position in the sort order as the specified color. </summary>
		/// <param name="other">The color to compare with the current instance of color.<see cref="System.Object"/></param>
		/// <returns>-1 if the current instance of color is is less than the color to compare to,
		/// 1 if the current instance of color is greater than the color to compare to or
		/// 0 if the current instance of color is equal to the color to compare to.<see cref="System.Int32"/></returns>
		public int CompareTo (object other)
		{	return this.CompareTo ((TColor)other);	}
		
		/// <summary>Compares the current instance of color with a specified color and indicate whether this instance of color
		/// precedes, follows, or appears in the same position in the sort order as the specified color.</summary>
		/// <param name="other">The color to compare with the current instance of color.<see cref="X11.TColor"/></param>
		/// <returns>-1 if the current instance of color is is less than the color to compare to,
		/// 1 if the current instance of color is greater than the color to compare to or
		/// 0 if the current instance of color is equal to the color to compare to.<see cref="System.Int32"/></returns>
		public int CompareTo (TColor other)
		{
			if (this.R + this.G + this.B > other.R + other.G + other.B)
				return 1;
			if (this.R + this.G + this.B < other.R + other.G + other.B)
				return -1;

			if (this.A > other.A)
				return 1;
			if (this.A < other.A)
				return -1;
			return 0;
		}
		
		/// <summary>Retrieves the hash code for this object.</summary>
		/// <returns>A 32-bit hash code, which is a signed integer.<see cref="System.Int32"/></returns>
		public override int GetHashCode () 
 		{
 			return this.R.GetHashCode () + this.G.GetHashCode () + this.B.GetHashCode () + this.A.GetHashCode ();
 		}
		
		/// <summary>Creates a System.String representation of this X11.TColor.</summary>
        /// <returns>A System.String containing the (X11.TColor.A,) X11.TColor.R, X11.TColor.G and X11.TColor.B
        /// values of this X11.TColor structure.<see cref="System.String"/></returns>
        public string ColorHTMLName ()
		{
			if (this.A == 255)
				return string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}", this.R, this.G, this.B);
			else
				return string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}{3,2:X2}", this.A, this.R, this.G, this.B);
		}
		
		/// <summary>Creates a System.String representation of this X11.TColor.</summary>
        /// <returns>A System.String containing the (X11.TColor.A,) X11.TColor.R, X11.TColor.G and X11.TColor.B
        /// values of this X11.TColor structure.<see cref="System.String"/></returns>
        public override string ToString ()
		{	return ColorHTMLName ();	}
		
		#endregion Methods
		
        // ###############################################################################
        // ### O P E R A T O R S
        // ###############################################################################
		
		#region Operators

        /// <summary>Evaluates two colors to determine equality.</summary>
		/// <param name="left">The first color to compare.<see cref="X11.TColor"/></param>
		/// <param name="right">The second color to compare.<see cref="X11.TColor"/></param>
		/// <returns>True on equality, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator ==(TColor left, TColor right)
        {   return (left.CompareTo (right) == 0);	}

        /// <summary>Evaluates two colors to determine inequality.</summary>
		/// <param name="left">The first color to compare.<see cref="X11.TColor"/></param>
		/// <param name="right">The second color to compare.<see cref="X11.TColor"/></param>
		/// <returns>True on inequality, or false otherwise.<see cref="System.Boolean"/></returns>
        public static bool operator !=(TColor left, TColor right)
        {    return (left.CompareTo (right) != 0);	}
		
		#endregion Operators

	}
}