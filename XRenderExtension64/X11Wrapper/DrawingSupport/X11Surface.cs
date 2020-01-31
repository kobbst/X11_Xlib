// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: June 2014
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2014 Steffen Ploetz
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
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;

using X11.Text;

namespace X11
{

	/// <summary>X Window System rendering surface to render graphics to X Window System windows and pixmaps using the XLib library.</summary>
	/// <remarks>This object provides methods for drawing objects onto any drawable.</remarks>
	public class X11Surface
	{
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "X11Surface";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The X11 display pointer.</summary>
		protected IntPtr		_display;
		
		/// <summary>The X11 screen number.</summary>
		protected int			_screenNumber;
		
		/// <summary>The X11 *** parent *** window for gadgets or the X11 *** own *** window for widgets or a X11 pixmap.</summary>
		protected IntPtr		_drawable;
		
		/// <summary>The X11 visual to use for drawing to drawable. The depth of the visual must match the depth of the drawable.
		/// Currently, only TrueColor visuals are fully supported.</summary>
		protected IntPtr		_visual;
		
		/// <summary>The X11 visual's color depth.</summary>
		protected int			_depth;
		
		/// <summary>The X11 colormap pointer.</summary>
		protected IntPtr		_colormap;
			
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The X11 display pointer.<see cref="IntPtr"/></param>
		/// <param name="screenNumber">The X11 screen number.<see cref="System.Int32"/></param>
		/// <param name="drawable">The X11 *** parent *** window for gadgets or
		/// the X11 *** own *** window for widgets or
		/// the X11 pixmap of a 'writable picture' bitmap image.<see cref="IntPtr"/></param>
		/// <param name="visual">The X11 visual to use for drawing to drawable. The depth of the visual must match the depth of the drawable.
		/// Currently, only TrueColor visuals are fully supported.<see cref="IntPtr"/></param>
		/// <param name="depth">The X11 visual's color depth.<see cref="System.Int32"/></param>
		/// <param name="colormap">The X11 colormap pointer.<see cref="IntPtr"/></param>
		public X11Surface (IntPtr display, int screenNumber, IntPtr drawable, IntPtr visual, int depth, IntPtr colormap)
		{
			if (display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (drawable == IntPtr.Zero)
				throw new ArgumentNullException ("drawable");
			if (visual == IntPtr.Zero)
				throw new ArgumentNullException ("visual");
			if (colormap == IntPtr.Zero)
				throw new ArgumentNullException ("colormap");
			
			_display = display;
			_screenNumber = screenNumber;
			_drawable = drawable;
			_visual = visual;
			_depth = depth;
			_colormap = colormap;
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary>Get the X11 display pointer.</summary>
		public IntPtr Display
		{ get {	return _display;	} }
	
		/// <summary>Get the X11 screen number.</summary>
		public int ScreenNumber
		{ get {	return _screenNumber;	} }
	
		/// <summary>Get the X11 *** parent *** window for gadgets or
		/// the X11 *** own *** window for widgets or
		/// the X11 pixmap of a 'writable picture' bitmap image.</summary>
		public IntPtr Drawable
		{ get {	return _drawable;	} }
	
		/// <summary>Get the X11 visual to use for drawing to drawable.</summary>
		/// <remarks> The depth of the visual must match the depth of the drawable.
		/// Currently, only TrueColor visuals are fully supported.</remarks>
		public IntPtr Visual
		{ get {	return _visual;	} }
	
		/// <summary>Get the X11 visual's color depth.</summary>
		public int Depth
		{ get {	return _depth;	} }
		
		/// <summary>Get the X11 colormap pointer.</summary>
		public IntPtr Colormap
		{ get {	return _colormap;	} }
		
		#endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Set methods
		
		/// <summary>Set the X11 display pointer.</summary>
		/// <param name="display">The display to set.<see cref="IntPtr"/></param>
		public void SetDisplay (IntPtr display)
		{
			_display = display;
		}
		
		/// <summary>Set the X11 *** parent *** window for gadgets or the X11 *** own *** window for widgets or the X11 pixmap of a 'writable picture' bitmap image.</summary>
		/// <param name="drawable">The X11 *** parent *** window for gadgets or the X11 *** own *** window for widgets or the X11 pixmap of a 'writable picture' bitmap image to set.<see cref="IntPtr"/></param>
		public void SetDrawable (IntPtr drawable)
		{
			_drawable = drawable;
		}
		
		/// <summary>Set the X11 visual pointer and color depth.</summary>
		/// <param name="visual">The X11 visual pointer.<see cref="IntPtr"/></param>
		/// <param name="depth">The X11 color depth.<see cref="System.Int32"/></param>
		public void SetVisual (IntPtr visual, int depth)
		{
			_visual = visual;
			_depth = depth;
		}
		
		#endregion
		
		#region Drawing methods
		
		/// <summary> Look up the underlaying colormap to investigate the closest color value provided. </summary>
		/// <param name="colorName"> The color's name to look up. <see cref="System.String"/> </param>
		/// <returns> The closest color value. <see cref="TColor"/> </returns>
		public TColor AllocateClosestColor (string colorName)
		{	return X11lib.XAllocClosestNamedColor (_display, _colormap, colorName);		}
				
		/// <summary>Determine the RGB values of the indicated color pixel.</summary>
		/// <param name="color">The color pixel to determine the RGB values for. <see cref="X11.TPixel"/> </param>
		/// <returns>The HTML color name of the indicated color pixel on success, or "#FFFFFF" as fallback otherwise.<see cref="System.String"/> </returns>
		public string HtmlNameForColor (TPixel color)
		{	X11lib.XColor pixelColor = new X11lib.XColor();
			pixelColor.pixel = color;
			pixelColor.flags = (TUchar)(X11lib.XColor.DoRed | X11lib.XColor.DoGreen | X11lib.XColor.DoBlue);
			
			TInt result = X11lib.XQueryColor (_display, _colormap, ref pixelColor);
			if ((int)result != 0)
				return string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}", pixelColor.red, pixelColor.green, pixelColor.blue);
			else
				// Fall back to none.
				return "";
		}
		
		/// <summary>Determine the RGB values of the indicated color pixel.</summary>
		/// <param name="color">The color pixel to determine the RGB values for. <see cref="X11.TPixel"/> </param>
		/// <returns>The RGB color components of the indicated color pixel on success, or 0 as fallback otherwise.<see cref="System.Int32"/> </returns>
		public int RgbForColor (TPixel color)
		{	X11lib.XColor pixelColor = new X11lib.XColor();
			pixelColor.pixel = color;
			pixelColor.flags = (TUchar)(X11lib.XColor.DoRed | X11lib.XColor.DoGreen | X11lib.XColor.DoBlue);
			
			TInt result = X11lib.XQueryColor (_display, _colormap, ref pixelColor);
			if ((int)result != 0)
				return ((int)pixelColor.red / 256) * 65536 + ((int)pixelColor.green / 256) * 256 + ((int)pixelColor.blue / 256);
			else
				// Fall back to black.
				return 0;
		}
				
		/// <summary>Determine the RGB values of the indicated color pixel.</summary>
		/// <param name="color">The color pixel to determine the RGB values for. <see cref="X11.TColor"/> </param>
		/// <returns> The HTML color name of the indicated color pixel on success, or "#FFFFFF" as fallback otherwise. <see cref="System.String"/> </returns>
		public string HtmlNameForColor (TColor color)
		{	return string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}", color.R, color.G, color.B);
		}
				
		/// <summary>Measure the text's bounding box.</summary>
		/// <param name="gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="styleList">The list of text styles. Must match the font style indices, used in 'textData'.<see cref="StyleList"/></param>
		/// <param name="styleText">The text to measure.<see cref="X11.StyleText"/></param>
		/// <param name="start">The start position with line index and character index. (This place is completely included.)<see cref="X11.Text.Place"/></param>
		/// <param name="end">The end position with line index and character index. (This place is completely included.)<see cref="X11.Text.Place"/></param>
		/// <param name="lineBoundings">The array of line boundings.<see cref="System.Drawing.Size[]"/></param>
		/// <returns>The bounding box on success, or a box of 5 x 5 otherwise.<see cref="System.Drawing.Size"/></returns>
		public Size TextBoundings (IntPtr gc, StyleList styleList, StyleText styleText, Place start, Place end, ref Size[] lineBoundings)
		{
			Size   result = new Size (5, 5);
			lineBoundings = null;
			
			if (gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::TextBoundings () Argument null: gc");
				return result;
			}
			if (styleList == null || styleList.Count == 0 || styleList[0].FontData == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::TextBoundings () Argument null or empty: styleList");
				return result;
			}
			if (styleText == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::TextBoundings () Argument null: textData");
				return result;
			}
			
			List<Size> lineResult = new List<Size>();
			result = new Size (0, 0);
			
			int firstLine = Math.Max (0,                   Math.Min (start.LineIndex, end.LineIndex));
			int lastLine  = Math.Min (styleText.LineCount, Math.Max (start.LineIndex, end.LineIndex));
			
			for (int lineIndex = firstLine; lineIndex <= lastLine; lineIndex++)
			{
				TStyleChar[] line = styleText[lineIndex];
				if (line == null)
					continue;
				
				int currentWidth              = 0;
				int currentHeight             = 0;
				int currentStyleIndex         = 0;
				int unprocessedStyleIndex     = 0;
				int unprocessedStartCharIndex = 0;
				
				// It is necessary to overflow the line's last character index to loop once for empty lines.
				for (int currentCharIndex = 0; currentCharIndex <= line.Length; currentCharIndex++)
				{
					if (currentCharIndex < line.Length)
					{
						// Get a valid style index.
						currentStyleIndex = line[currentCharIndex].StyleIndex;
						if (currentStyleIndex < 0)
							currentStyleIndex = 0;
						
						// Initialize the last style index at the very beginning of a line.
						if (currentCharIndex == 0)
							unprocessedStyleIndex = currentStyleIndex;
					}
					// else
					//     Skip changes on 'currentStyleIndex'.
					
					if (unprocessedStyleIndex == currentStyleIndex && currentCharIndex < line.Length)
						continue;
					// else
					//     Fall through and calculate text extends.
					
					int currentLength = currentCharIndex - unprocessedStartCharIndex;
					
				
					if (styleList[unprocessedStyleIndex].FontData.UseFontset)
					{
						X11lib.XRectangle overallInc      = new X11lib.XRectangle();
						X11lib.XRectangle overallLogical  = new X11lib.XRectangle();
						if (line.Length > 0)
						{
							X11.TWchar[] subText = styleText.SubText32 (lineIndex, unprocessedStartCharIndex, currentLength);
							X11lib.XwcTextExtents (styleList[unprocessedStyleIndex].FontData.FontResourceId,
							                       subText, (X11.TInt)subText.Length,
							                       ref overallInc, ref overallLogical);
						}
						else
						{
							// Calculate the height of any character.
							X11.TWchar[] replacement = new X11.TWchar[] { (X11.TWchar)0 };
							X11lib.XwcTextExtents (styleList[unprocessedStyleIndex].FontData.FontResourceId, replacement, (X11.TInt)1, ref overallInc, ref overallLogical);
							overallLogical.width = (TUshort)0;
						}
						
						currentWidth += (int)overallLogical.width;
						currentHeight = Math.Max (currentHeight, (int)overallLogical.height);
					}
					else
					{
						// TInt gcID = X11lib.XGContextFromGC (gc);
						// if (gcID != 0)
						// {	XQueryTextExtents (this.Display, gcID, ...);									}
						// else
						// {	Cannot investigate default font ressource ID of underlaying graphics context.	}
						
						TInt direction = 0;
						TInt fontAscent = 0;
						TInt fontDescent = 0;
						X11lib.XCharStruct xCharStruct = new X11lib.XCharStruct ();
						
						if (line.Length > 0)
						{
							X11lib.XChar2b[] subText = styleText.SubText16 (lineIndex, unprocessedStartCharIndex, currentLength);
							X11lib.XQueryTextExtents16 (this.Display, styleList[unprocessedStyleIndex].FontData.FontResourceId,
							                            subText, (X11.TInt)subText.Length,
							                            ref direction, ref fontAscent, ref fontDescent, ref xCharStruct);
						}
						else
						{
							// Calculate the height of any character.
							X11lib.XChar2b[] replacement = new X11lib.XChar2b[] { new X11lib.XChar2b ('X')  };
							X11lib.XQueryTextExtents16 (this.Display, styleList[unprocessedStyleIndex].FontData.FontResourceId, replacement, (X11.TInt)1, ref direction, ref fontAscent, ref fontDescent, ref xCharStruct);
							xCharStruct.width = (TShort)0;
						}
						currentWidth += (int)xCharStruct.width;
						currentHeight = Math.Max (currentHeight, (int)fontAscent + (int)fontDescent);
					}
					
					unprocessedStartCharIndex = currentCharIndex;
					unprocessedStyleIndex     = currentStyleIndex;
				}
				
				lineResult.Add (new Size (currentWidth, currentHeight));
				result.Width   = Math.Max (result.Width, currentWidth);
				result.Height += currentHeight;
			}
			
			lineBoundings = lineResult.ToArray ();
			return result;
		}
		
		/// <summary>Draw a string without cleaning the background.</summary>
		/// <param name="gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The leftmost x-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="y">The baseline y-coordinate to start drawing left to right.<see cref="X11.TInt"/></param>
		/// <param name="styleList">The list of text styles. Must match the font style indices, used in 'textData'.<see cref="StyleList"/></param>
		/// <param name="suppressStyleColors">Indicate whether style colors are to apply.
		/// If not, the text is drawn with the current foreground color. This ispecially usefil for insenitive text.<see cref="System.Boolean"/></param>
		/// <param name="styleText">The text to draw.<see cref="X11.StyleText"/></param>
		/// <param name="start">The start position with line index and character index. (This place is completely included.)<see cref="X11.Text.Place"/></param>
		/// <param name="end">The end position with line index and character index. (This place is completely included.)<see cref="X11.Text.Place"/></param>
		public void DrawString (IntPtr gc, int x, int y, StyleList styleList, bool suppressStyleColors, StyleText styleText, Place start, Place end)
		{
			if (gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawString () Argument null: gc");
				return;
			}
			if (styleList == null || styleList.Count == 0 || styleList[0].FontData == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawString () Argument null or empty: styleList");
				return;
			}
			if (styleText == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawString () Argument null: textData");
				return;
			}
			
			int firstLine = Math.Max (0,                  Math.Min (start.LineIndex, end.LineIndex));
			int lastLine  = Math.Min (styleText.LineCount - 1, Math.Max (start.LineIndex, end.LineIndex));
			int firstChar = (start.LineIndex < end.LineIndex ? start.CharIndex : (start.LineIndex == end.LineIndex ? Math.Min (start.CharIndex, end.CharIndex) : end.CharIndex));
			int lastChar  = (start.LineIndex < end.LineIndex ? end.CharIndex   : (start.LineIndex == end.LineIndex ? Math.Max (start.CharIndex, end.CharIndex) : start.CharIndex));
			
			if (lastLine < 0)
				return;
			
			if (firstChar < 0)
				firstChar = 0;
			if (lastChar >= styleText.LineCharCount(lastLine))
				lastChar = styleText.LineCharCount(lastLine) - 1;
			
			if (firstLine >= styleText.LineCount)
				return;
			if (firstLine == lastLine && firstChar > lastChar)
				return;
			
			int currentStyleIndex = -1;
			for (int lineIndex = firstLine; lineIndex <= lastLine; lineIndex++)
			{
				int minCharIndexOfLine  = (lineIndex == firstLine ? firstChar : 0); 
				int maxCharIndexOfLine  = (lineIndex == lastLine  ? lastChar  : styleText.LineCharCount(lineIndex) - 1);
				int startIndexSubstring = minCharIndexOfLine;
				
				for (int currentIndex = startIndexSubstring; currentIndex <= maxCharIndexOfLine; currentIndex++)
				{
					currentStyleIndex = styleText.StyleIndex (lineIndex, currentIndex);
					
					if (currentStyleIndex < 0 || currentStyleIndex >= styleList.Count || styleList[currentStyleIndex] == null)
					{	SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawString () Style index '" + currentStyleIndex.ToString () +
						                   "' out of range for character index '" + currentIndex.ToString () + "' of string " + styleText.Text (lineIndex) + ".");
						currentStyleIndex = 0;
					}
					
					// Do we have to draw a (sub-) string or can we go on...
					if (currentIndex == maxCharIndexOfLine || // End of line - we must draw.
					    currentStyleIndex != styleText.StyleIndex (lineIndex, currentIndex + 1))
					{
						int lengthSubstring = currentIndex + 1 - startIndexSubstring;
						
						bool underline = (styleList[currentStyleIndex].FontStyle & FontStyle.Underline) == FontStyle.Underline;
						bool strikeout = (styleList[currentStyleIndex].FontStyle & FontStyle.Strikeout) == FontStyle.Strikeout;
						TInt extent = 0;
						
						// Update x position, if necessary.
						if (currentIndex < maxCharIndexOfLine ||
						    underline || strikeout ||
						    !styleList[currentStyleIndex].BackColor.IsFullyTransparent)
						{
							if (styleList[currentStyleIndex].FontData.UseFontset)
								extent = X11lib.XwcTextEscapement (styleList[currentStyleIndex].FontData.FontResourceId,
								                                   styleText.SubText32 (lineIndex, startIndexSubstring, lengthSubstring), (TInt)lengthSubstring);
							else
							{	IntPtr xFontStruct = X11lib.XQueryFont (this.Display, styleList[currentStyleIndex].FontData.FontResourceId);
								extent = X11lib.XTextWidth16 (xFontStruct, styleText.SubText16(lineIndex, startIndexSubstring, lengthSubstring), (X11.TInt)lengthSubstring);
							}
						}
						
						if (!styleList[currentStyleIndex].BackColor.IsFullyTransparent)
						{
							X11.TColor currentFg = GetCurrentForeground (gc);
							SetForeground (gc, styleList[currentStyleIndex].BackColor);
							int height = styleList[currentStyleIndex].FontData.LogicalHeight;
							X11lib.XFillRectangle (this.Display, this.Drawable, gc, x, y - (int)(height * 0.85F), (int)extent, height);
							SetForeground (gc, currentFg);
						}
						if (underline)
						{
							if (!suppressStyleColors)
								SetForeground (gc, styleList[currentStyleIndex].ForeColor);
							int strikeY = y + (int)(styleList[currentStyleIndex].FontData.LogicalHeight * 0.1F);
							X11lib.XDrawLine (this.Display, this.Drawable, gc, x, strikeY, x + (int)extent, strikeY);
						}
						if (strikeout)
						{
							if (!suppressStyleColors)
								SetForeground (gc, styleList[currentStyleIndex].ForeColor);
							int strikeY = y - (int)(styleList[currentStyleIndex].FontData.LogicalHeight * 0.3F);
							X11lib.XDrawLine (this.Display, this.Drawable, gc, x, strikeY, x + (int)extent, strikeY);
						}
						
						if (!suppressStyleColors)
							SetForeground (gc, styleList[currentStyleIndex].ForeColor);
						if (styleList[currentStyleIndex].FontData.UseFontset)
							X11lib.XwcDrawString  (this.Display, this.Drawable, styleList[currentStyleIndex].FontData.FontResourceId, gc, (TInt)x, (TInt)y,
							                       styleText.SubText32(lineIndex, startIndexSubstring, lengthSubstring), (X11.TInt)lengthSubstring);
						else
						{
							X11lib.XSetFont       (this.Display, gc, styleList[currentStyleIndex].FontData.FontResourceId);
							X11lib.XDrawString16  (this.Display, this.Drawable, gc, (TInt)x, (TInt)y,
							                       styleText.SubText16(lineIndex, startIndexSubstring, lengthSubstring), (X11.TInt)lengthSubstring);
						}
						
												x += (int)extent;
						startIndexSubstring = currentIndex + 1;
					}
				}
			}
		}
		
		/// <summary>Set the specilied color as foreground color for drawing.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		/// <returns>True on success, or false otherwise.<see cref="System.Boolean"/></returns>
		public bool SetForeground (IntPtr x11gc, TColor color)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::SetForeground () Argument null: x11gc");
				return false;
			}
			
			try
			{
				if (!color.IsPixelInitialized)
				{
					bool   exact = true;
					string name  = string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}", color.R, color.G, color.B);
					color.P = X11lib.XAllocClosestNamedColor (this.Display, this.Colormap, name, ref exact);
				}
				X11lib.XSetForeground (this.Display, x11gc, color.P);
				return true;
			}
			catch (Exception e)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::SetForeground () has thrown exception: {0}\n{1}", e.Message, e.StackTrace);
				return false;
			}
		}
		
		/// <summary>Get the indicated GC's current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <returns>The indicated GC's current foreground color on success, or TColor.FallbackBlack otherwise.<see cref="TColor"/></returns>
		public TColor GetCurrentForeground (IntPtr x11gc)
		{
			X11.X11lib.XGCValues values = new X11.X11lib.XGCValues();
			if (X11lib.XGetGCValues (this.Display, x11gc, (TUint) X11.X11lib.GCattributemask.GCForeground, ref values) != 0)
			{
				X11lib.XColor pixelColor = new X11lib.XColor();
				pixelColor.pixel = values.foreground;
				pixelColor.flags = (TUchar)(X11lib.XColor.DoRed | X11lib.XColor.DoGreen | X11lib.XColor.DoBlue);
			
				if (X11lib.XQueryColor (_display, _colormap, ref pixelColor) != 0)
					return TColor.FromPRGB (pixelColor.pixel, (byte)pixelColor.red, (byte)pixelColor.green, (byte)pixelColor.blue);
				else
					return TColor.FallbackBlack;
			}
			else
				return TColor.FallbackBlack;
		}
		
		/// <summary>Set the specified fill operation (solid, hatch, tile) of the x11gc.</summary>
		/// <param name="x11gc">The graphics contect to set the specified fill options for.<see cref="IntPtr"/></param>
		/// <param name="fill">The fill options to set.<see cref="X11BrushInfo"/></param>
		/// <returns>True on success, or false otherwise.<see cref="System.Boolean"/></returns>
		public bool SetFill (IntPtr x11gc, X11BrushInfo fill)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::SetFill () Argument null: x11gc");
				return false;
			}
			
			if (fill == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::SetFill () Argument null: fill");
				return false;
			}
			
			try
			{
				if (!fill.Color.IsPixelInitialized)
					fill.ColorPixel = this.AllocateClosestColor (fill.Color.ColorHTMLName ()).P;
				SetForeground (x11gc, fill.Color);
					
				if (fill.Type == X11BrushInfo.BrushType.Hatch)
				{
					X11.X11lib.XSetFillStyle (this.Display, x11gc, X11.X11lib.XGCFillStyle.FillStippled); 
					X11.X11lib.XSetStipple   (this.Display, x11gc, fill.HatchBitmap (this.Display, this.Drawable));
				}
				else if (fill.Type == X11BrushInfo.BrushType.Image)
				{
					X11.X11lib.XSetFillStyle (this.Display, x11gc, X11.X11lib.XGCFillStyle.FillTiled);
					X11.X11lib.XSetTile      (this.Display, x11gc, fill.TilePixmap (this.Display, this.Drawable));
					
					if (fill.HasImageOffset)
						X11.X11lib.XSetTSOrigin  (this.Display, x11gc,
						                          (X11.TInt)fill.ImageOffset.X, (X11.TInt)fill.ImageOffset.Y);
					else
						X11.X11lib.XSetTSOrigin  (this.Display, x11gc, (X11.TInt)0, (X11.TInt)0);
				}
				return true;
			}
			catch (Exception e)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::SetFill () has thrown exception: {0}\n{1}", e.Message, e.StackTrace);
				return false;
			}
		}
		
		/// <summary>Unset the specified fill operation (hatch, tile) of the x11gc back to fill.</summary>
		/// <param name="x11gc">The graphics contect to set the specified fill options for.<see cref="IntPtr"/></param>
		/// <param name="fill">The fill options to set.<see cref="X11BrushInfo"/></param>
		/// <returns>True on success, or false otherwise.<see cref="System.Boolean"/></returns>
		public bool UnsetFill (IntPtr x11gc, X11BrushInfo fill)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::UnsetFill () Argument null: x11gc");
				return false;
			}
			
			if (fill == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::UnsetFill () Argument null: fill");
				return false;
			}
			
			try
			{
				if (fill.Type == X11BrushInfo.BrushType.Hatch)
					X11.X11lib.XSetFillStyle (this.Display, x11gc, X11.X11lib.XGCFillStyle.FillSolid);
				else if (fill.Type == X11BrushInfo.BrushType.Image)
					X11.X11lib.XSetFillStyle (this.Display, x11gc, X11.X11lib.XGCFillStyle.FillSolid);
				return true;
			}
			catch (Exception e)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::UnsetFill () has thrown exception: {0}\n{1}", e.Message, e.StackTrace);
				return false;
			}
		}
		
		/// <summary>Set the specified stroke attributes for drawing.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="color">The stroke thickness to use.<see cref="X11.TColor"/></param>
		/// <param name="lineWidth">The line width, that specifies the line-width to set for the specified GC.<see cref="X11.TUint"/></param>
		/// <param name="lineStyle">The line style, that specifies the line-style to set for the specified GC. Can be LineSolid, LineOnOffDash, or LineDoubleDash.<see cref="X11.X11lib.XGCLineStyle"/></param>
		/// <param name="capStyle">The cap style, tat specifies the line cap-style to set for the specified GC. Can be CapNotLast, CapButt, CapRound, or CapProjecting.<see cref="X11.X11lib.XGCCapStyle"/></param>
		/// <param name="joinStyle">The join styl, that specifies the line join-style to set for the specified GC. Can be JoinMiter, JoinRound, or JoinBevel.<see cref="X11.X11lib.XGCJoinStyle"/></param>
		public void SetLineAttributes (IntPtr x11gc, X11.TUint lineWidth, X11.X11lib.XGCLineStyle lineStyle, X11.X11lib.XGCCapStyle capStyle,
		                               X11.X11lib.XGCJoinStyle joinStyle)
		{
			X11lib.XSetLineAttributes (this.Display, x11gc, lineWidth, lineStyle, capStyle, joinStyle);
		}
		
		/// <summary>Set the specified dash attributes for drawing.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="dashOffset">The dash offset, that specifies the offset from the figure start point to the first dash start point.<see cref="System.Int32"/></param>
		/// <param name="dashLength">The dash length, that specifies the length of dashes and gaps.<see cref="System.Int32"/></param>
		public void SetDashAttributes (IntPtr x11gc, int dashOffset, int dashLength)
		{
			X11.X11lib.XGCValues cgv = new X11.X11lib.XGCValues ();
			cgv.dash_offset = (X11.TInt)dashOffset;
			cgv.dashes = (X11.TChar)dashLength;
			X11lib.XChangeGC (this.Display, x11gc, (X11.TUint)(X11.X11lib.XGCValueMask.GCDashList | X11.X11lib.XGCValueMask.GCDashOffset), ref cgv);
		}
		
		/// <summary>Draw the specified point with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="x">The point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="y">The point y coordinate.<see cref="System.Int32"/></param>
		public void DrawPoint (IntPtr gc, int x, int y)
		{
			if (gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawPoint () Argument null: gc");
				return;
			}
			
			X11lib.XDrawPoint (this.Display, this.Drawable, gc, x, y);
		}
		
		/// <summary>Draw the specified point after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="x">The point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="y">The point y coordinate.<see cref="System.Int32"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawPoint (IntPtr x11gc, int x, int y, TColor color)
		{
			SetForeground (x11gc, color);
			DrawPoint (x11gc, x, y);
		}
									
		/// <summary>Draw the specified line with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="x1">The start point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="y1">The start point y coordinate.<see cref="System.Int32"/></param>
		/// <param name="x2">The end point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="y2">The end point y coordinate.<see cref="System.Int32"/></param>
		public void DrawLine (IntPtr x11gc, int x1, int y1, int x2, int y2)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawLine () Argument null: gc");
				return;
			}
			
			X11lib.XDrawLine (this.Display, this.Drawable, x11gc, x1, y1, x2, y2);
		}
									
		/// <summary>Draw the specified line after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="x1">The start point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="y1">The start point y coordinate.<see cref="System.Int32"/></param>
		/// <param name="x2">The end point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="y2">The end point y coordinate.<see cref="System.Int32"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawLine (IntPtr x11gc, int x1, int y1, int x2, int y2, TColor color)
		{
			SetForeground (x11gc, color);
			DrawLine (x11gc, x1, y1, x2, y2);
		}
		
		/// <summary>Draw the specified lines with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11lib.XPoint[]"/> </param>
		/// <param name="coordMode"> The coordinate mode. You can pass CoordModeOrigin or CoordModePrevious.
		/// CoordModeOrigin treats all coordinates as relative to the origin, and CoordModePrevious treats all coordinates
		///   after the first as relative to the previous point. <see cref="XCoordinateMode"/> </param>
		public void DrawLines (IntPtr x11gc, X11lib.XPoint[] points, X11lib.XCoordinateMode coordMode)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawLines () Argument null: gc");
				return;
			}
			
			X11lib.XDrawLines (this.Display, this.Drawable, x11gc, points, points.Length, coordMode);
		}
		
		/// <summary>Draw the specified lines after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11lib.XPoint[]"/> </param>
		/// <param name="coordMode"> The coordinate mode. You can pass CoordModeOrigin or CoordModePrevious.
		/// CoordModeOrigin treats all coordinates as relative to the origin, and CoordModePrevious treats all coordinates
		///   after the first as relative to the previous point. <see cref="XCoordinateMode"/> </param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawLines (IntPtr x11gc, X11lib.XPoint[] points, X11lib.XCoordinateMode coordMode, TColor color)
		{
			SetForeground (x11gc, color);
			DrawLines (x11gc, points, coordMode);
		}
		
		/// <summary>Draw the specified lines with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="System.Windows.Point[]"/> </param>
		/// <param name="coordMode"> The coordinate mode. You can pass CoordModeOrigin or CoordModePrevious.
		/// CoordModeOrigin treats all coordinates as relative to the origin, and CoordModePrevious treats all coordinates
		///   after the first as relative to the previous point. <see cref="XCoordinateMode"/> </param>
		public void DrawLines (IntPtr x11gc, System.Windows.Point[] points, X11lib.XCoordinateMode coordMode)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawLines () Argument null: gc");
				return;
			}
			
			X11lib.XPoint[] xPoints = new X11lib.XPoint[points.Length];
			for (int pointIdx = 0; pointIdx < points.Length; pointIdx++)
			     xPoints[pointIdx] = new X11lib.XPoint ((X11.TShort)((int)(points[pointIdx].X + 0.5)), (X11.TShort)((int)(points[pointIdx].Y + 0.5)));
				
			X11lib.XDrawLines (this.Display, this.Drawable, x11gc, xPoints, xPoints.Length, coordMode);
		}
		
		/// <summary>Draw the specified lines after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="System.Windows.Point[]"/> </param>
		/// <param name="coordMode"> The coordinate mode. You can pass CoordModeOrigin or CoordModePrevious.
		/// CoordModeOrigin treats all coordinates as relative to the origin, and CoordModePrevious treats all coordinates
		///   after the first as relative to the previous point. <see cref="XCoordinateMode"/> </param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawLines (IntPtr x11gc, System.Windows.Point[] points, X11lib.XCoordinateMode coordMode, TColor color)
		{
			SetForeground (x11gc, color);
			DrawLines (x11gc, points, coordMode);
		}
		
		/// <summary>Draw the specified lines with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11lib.XPoint[]"/> </param>
		/// <param name="coplexity"> The polygon coplexity mode. You can pass Complex, Nonconvex or Convex.
		/// Complex treats as any parts of the polygon path may intersect, Nonconvex treats as NO parts of the polygon path may intersect
		/// but shape may NOT be convex and Convex treats as all parts of the polygon are convex.. <see cref="X11lib.XPolygonShape"/> </param>
		public void FillPolygon (IntPtr x11gc, X11lib.XPoint[] points, X11lib.XPolygonShape coplexity)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawLines () Argument null: gc");
				return;
			}
			
			X11lib.XFillPolygon (this.Display, this.Drawable, x11gc, points, points.Length, coplexity, X11lib.XCoordinateMode.CoordModeOrigin);
		}
		
		/// <summary>Fill the specified polygon after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11lib.XPoint[]"/> </param>
		/// <param name="coplexity"> The polygon coplexity mode. You can pass Complex, Nonconvex or Convex.
		/// Complex treats as any parts of the polygon path may intersect, Nonconvex treats as NO parts of the polygon path may intersect
		/// but shape may NOT be convex and Convex treats as all parts of the polygon are convex.. <see cref="X11lib.XPolygonShape"/> </param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillPolygon (IntPtr x11gc, X11lib.XPoint[] points, X11lib.XPolygonShape coplexity, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillPolygon (x11gc, points, coplexity);
				UnsetFill(x11gc, fill);
			}
		}
		
		/// <summary>Fill the specified polygon with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="System.Windows.Point[]"/> </param>
		/// <param name="coplexity"> The polygon coplexity mode. You can pass Complex, Nonconvex or Convex.
		/// Complex treats as any parts of the polygon path may intersect, Nonconvex treats as NO parts of the polygon path may intersect
		/// but shape may NOT be convex and Convex treats as all parts of the polygon are convex.. <see cref="X11lib.XPolygonShape"/> </param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillPolygon (IntPtr x11gc, System.Windows.Point[] points, X11lib.XPolygonShape coplexity)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawLines () Argument null: gc");
				return;
			}
			
			X11lib.XPoint[] xPoints = new X11lib.XPoint[points.Length];
			for (int pointIdx = 0; pointIdx < points.Length; pointIdx++)
			     xPoints[pointIdx] = new X11lib.XPoint ((X11.TShort)((int)(points[pointIdx].X + 0.5)), (X11.TShort)((int)(points[pointIdx].Y + 0.5)));
			
			X11lib.XFillPolygon (this.Display, this.Drawable, x11gc, xPoints, xPoints.Length, coplexity, X11lib.XCoordinateMode.CoordModeOrigin);
		}
		
		/// <summary>Fill the specified polygon after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="System.Windows.Point[]"/> </param>
		/// <param name="coplexity"> The polygon coplexity mode. You can pass Complex, Nonconvex or Convex.
		/// Complex treats as any parts of the polygon path may intersect, Nonconvex treats as NO parts of the polygon path may intersect
		/// but shape may NOT be convex and Convex treats as all parts of the polygon are convex.. <see cref="X11lib.XPolygonShape"/> </param>
		/// <param name="fill">The brush to use for fill.<see cref="System.Windows.Media.Brush"/></param>
		public void FillPolygon (IntPtr x11gc, System.Windows.Point[] points, X11lib.XPolygonShape coplexity, X11.X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillPolygon (x11gc, points, coplexity);
				UnsetFill(x11gc, fill);
			}
		}
		
		/// <summary>Fill the specified polygon and draw the outline after setting the specilied colors as foreground colors.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		/// <param name="coplexity"> The polygon coplexity mode. You can pass Complex, Nonconvex or Convex.
		/// Complex treats as any parts of the polygon path may intersect, Nonconvex treats as NO parts of the polygon path may intersect
		/// but shape may NOT be convex and Convex treats as all parts of the polygon are convex.. <see cref="X11lib.XPolygonShape"/> </param>
		/// <param name="strokeColor">The color to use for outline.<see cref="X11.TColor"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillAndDrawPolygon (IntPtr x11gc, X11lib.XPoint[] points, X11lib.XPolygonShape coplexity, TColor strokeColor, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				X11lib.XFillPolygon (this.Display, this.Drawable, x11gc, points, points.Length,
				                     coplexity, X11lib.XCoordinateMode.CoordModeOrigin);
				UnsetFill(x11gc, fill);
			}
			
			SetForeground (x11gc, strokeColor);
			X11lib.XDrawLines (this.Display, this.Drawable, x11gc, points, points.Length, X11lib.XCoordinateMode.CoordModeOrigin);
		}
		
		/// <summary>Fill the specified polygon and draw the outline after setting the specilied colors as foreground colors.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		/// <param name="coplexity"> The polygon coplexity mode. You can pass Complex, Nonconvex or Convex.
		/// Complex treats as any parts of the polygon path may intersect, Nonconvex treats as NO parts of the polygon path may intersect
		/// but shape may NOT be convex and Convex treats as all parts of the polygon are convex.. <see cref="X11lib.XPolygonShape"/> </param>
		/// <param name="strokeColor">The color to use for outline.<see cref="X11.TColor"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillAndDrawPolygon (IntPtr x11gc, System.Windows.Point[] points, X11lib.XPolygonShape coplexity, TColor strokeColor, X11BrushInfo fill)
		{
			X11lib.XPoint[] xPoints = new X11lib.XPoint[points.Length];
			for (int pointIdx = 0; pointIdx < points.Length; pointIdx++)
			     xPoints[pointIdx] = new X11lib.XPoint ((X11.TShort)((int)(points[pointIdx].X + 0.5)), (X11.TShort)((int)(points[pointIdx].Y + 0.5)));

			FillAndDrawPolygon (x11gc, xPoints, coplexity, strokeColor, fill);
		}
		
		/// <summary>Draw the specified path with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="path"> The collection of path segments, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		public void DrawPath (IntPtr x11gc, X11PathSegmentCollection path)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawPath () Argument null: gc");
				return;
			}
			
			X11lib.XPoint[] polygon = X11lib.XPathToPolygon (path);
			if (polygon == null || polygon.Length == 0)
				return;
			X11lib.XDrawLines (this.Display, this.Drawable, x11gc, polygon, polygon.Length, X11lib.XCoordinateMode.CoordModeOrigin);
		}
		
		/// <summary>Draw the specified path after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawPath (IntPtr x11gc, X11PathSegmentCollection path, TColor color)
		{
			SetForeground (x11gc, color);
			DrawPath (x11gc, path);
		}
		
		/// <summary>Fill the specified path with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		public void FillPath (IntPtr x11gc, X11PathSegmentCollection path)
		{
			X11lib.XPoint[] polygon = X11lib.XPathToPolygon (path);
			if (polygon == null || polygon.Length == 0)
				return;
			X11lib.XFillPolygon (this.Display, this.Drawable, x11gc, polygon, polygon.Length,
			                     X11lib.XPolygonShape.Complex, X11lib.XCoordinateMode.CoordModeOrigin);
		}
		
		/// <summary>Fill the specified path after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillPath (IntPtr x11gc, X11PathSegmentCollection path, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillPath (x11gc, path);
				UnsetFill(x11gc, fill);
			}
		}
		
		/// <summary>Fill the specified path and draw the outline after setting the specilied colors as foreground colors.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="points"> The array of points, defining the lines to draw. <see cref="X11.X11PathSegmentCollection"/> </param>
		/// <param name="strokeColor">The color to use for outline.<see cref="X11.TColor"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillAndDrawPath (IntPtr x11gc, X11PathSegmentCollection path, TColor strokeColor, X11BrushInfo fill)
		{
			X11lib.XPoint[] polygon = X11lib.XPathToPolygon (path);
			if (polygon == null || polygon.Length == 0)
				return;
			
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				X11lib.XFillPolygon (this.Display, this.Drawable, x11gc, polygon, polygon.Length,
				                     X11lib.XPolygonShape.Complex, X11lib.XCoordinateMode.CoordModeOrigin);
				UnsetFill(x11gc, fill);
			}
			
			SetForeground (x11gc, strokeColor);
			X11lib.XDrawLines (this.Display, this.Drawable, x11gc, polygon, polygon.Length, X11lib.XCoordinateMode.CoordModeOrigin);
		}
		
		/// <summary>Draw the specified rectangle as if a four-point FillPolygon protocol request and the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The left top corner x-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="y">The left top corner y-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="width">The rectangle width.<see cref="X11.TUint"/></param>
		/// <param name="height">The rectangle height.<see cref="X11.TUint"/></param>
		public void DrawRectangle (IntPtr x11gc, int x, int y, int width, int height)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawRectangle () Argument null: gc");
				return;
			}
			
			X11lib.XDrawRectangle (this.Display, this.Drawable, x11gc, x, y, width, height);
		}
		
		/// <summary>Draw the specified rectangle as if a four-point FillPolygon protocol request
		/// after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The left top corner x-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="y">The left top corner y-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="width">The rectangle width.<see cref="X11.TUint"/></param>
		/// <param name="height">The rectangle height.<see cref="X11.TUint"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawRectangle (IntPtr x11gc, int x, int y, int width, int height, TColor color)
		{
			SetForeground (x11gc, color);
			DrawRectangle (x11gc, x, y, width, height);
		}
		
		/// <summary>Draw the specified rectangle as if a four-point FillPolygon protocol request and the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="rect">The rectangle coordinates.<see cref="System.Drawing.Rectangle"/></param>
		public void DrawRectangle (IntPtr x11gc, System.Drawing.Rectangle rect)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::FillRectangle () Argument null: gc");
				return;
			}
			
			X11lib.XDrawRectangle (this.Display, this.Drawable, x11gc, rect);
		}
		
		/// <summary>Draw the specified rectangle as if a four-point FillPolygon protocol request
		/// after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="rect">The rectangle coordinates.<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawRectangle (IntPtr x11gc, System.Drawing.Rectangle rect, TColor color)
		{
			SetForeground (x11gc, color);
			DrawRectangle (x11gc, rect);
		}
		
		/// <summary>Fill the specified rectangle as if a four-point FillPolygon protocol request and the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The left top corner x-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="y">The left top corner y-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="width">The rectangle width.<see cref="X11.TUint"/></param>
		/// <param name="height">The rectangle height.<see cref="X11.TUint"/></param>
		public void FillRectangle (IntPtr x11gc, int x, int y, int width, int height)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::FillRectangle () Argument null: gc");
				return;
			}
			
			X11lib.XFillRectangle (this.Display, this.Drawable, x11gc, x, y, width, height);
		}
		
		/// <summary>Fill the specified rectangle as if a four-point FillPolygon protocol request
		/// after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The left top corner x-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="y">The left top corner y-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="width">The rectangle width.<see cref="X11.TUint"/></param>
		/// <param name="height">The rectangle height.<see cref="X11.TUint"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void FillRectangle (IntPtr x11gc, int x, int y, int width, int height, TColor color)
		{
			SetForeground (x11gc, color);
			FillRectangle (x11gc, x, y, width, height);
		}
		
		/// <summary>Fill the specified rectangle as if a four-point FillPolygon protocol request
		/// after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The left top corner x-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="y">The left top corner y-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="width">The rectangle width.<see cref="X11.TUint"/></param>
		/// <param name="height">The rectangle height.<see cref="X11.TUint"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillRectangle (IntPtr x11gc, int x, int y, int width, int height, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillRectangle (x11gc, x, y, width, height);
				UnsetFill(x11gc, fill);
			}
		}
		
		/// <summary>Fill the specified rectangle and draw the outline as if a four-point FillPolygon protocol request
		/// after setting the specilied colors as foreground colors.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="x">The left top corner x-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="y">The left top corner y-coordinate.<see cref="SX11.TInt"/></param>
		/// <param name="width">The rectangle width.<see cref="X11.TUint"/></param>
		/// <param name="height">The rectangle height.<see cref="X11.TUint"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillAndDrawRectangle (IntPtr x11gc, int x, int y, int width, int height, TColor color, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillRectangle (x11gc, x, y, width, height);
				UnsetFill(x11gc, fill);
			}
			
			SetForeground (x11gc, color);
			DrawRectangle (x11gc, x, y, width, height);
		}
		
		/// <summary>Fill the specified rectangle as if a four-point FillPolygon protocol request and the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="rect">The rectangle coordinates.<see cref="System.Drawing.Rectangle"/></param>
		public void FillRectangle (IntPtr x11gc, System.Drawing.Rectangle rect)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::FillRectangle () Argument null: gc");
				return;
			}
			
			X11lib.XFillRectangle (this.Display, this.Drawable, x11gc, rect);
		}
		
		/// <summary>Fill the specified rectangle as if a four-point FillPolygon protocol request
		/// after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="rect">The rectangle coordinates.<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void FillRectangle (IntPtr x11gc, System.Drawing.Rectangle rect, TColor color)
		{
			SetForeground (x11gc, color);
			FillRectangle (x11gc, rect);
		}
		
		/// <summary>Fill the specified rectangle as if a four-point FillPolygon protocol request
		/// after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="rect">The rectangle coordinates.<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillRectangle (IntPtr x11gc, System.Drawing.Rectangle rect, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillRectangle (x11gc, rect);
				UnsetFill(x11gc, fill);
			}
		}
		
		/// <summary>Fill the specified rectangle and draw the outline as if a four-point FillPolygon protocol request
		/// after setting the specilied colors as foreground colors.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="rect">The rectangle coordinates.<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		/// <param name="fill">The brush info to use for fill.<see cref="X11.X11BrushInfo"/></param>
		public void FillAndDrawRectangle (IntPtr x11gc, System.Drawing.Rectangle rect, TColor color, X11BrushInfo fill)
		{
			if (fill != null)
			{
				SetFill  (x11gc, fill);
				FillRectangle (x11gc, rect);
				UnsetFill(x11gc, fill);
			}
			
			SetForeground (x11gc, color);
			DrawRectangle (x11gc, rect);
		}
									
		/// <summary>Draw the specified arc with the current foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="centerX">The center point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="centerY">The center point y coordinate.<see cref="System.Int32"/></param>
		/// <param name="radiusX">The radius x coordinate.<see cref="System.Int32"/></param>
		/// <param name="radiusY">The radius y coordinate.<see cref="System.Int32"/></param>
		/// <param name="startAngle">The start angle relative to the 3 o'clock position from the center in units of degrees * 64.<see cref="System.Int32"/></param>
		/// <param name="sweepAngle">The sweep angle relative to the start angle in units of degrees * 64.<see cref="System.Int32"/></param>
		public void DrawArc (IntPtr x11gc, int centerX, int centerY, int radiusX, int radiusY, int startAngle, int sweepAngle)
		{
			if (x11gc == IntPtr.Zero)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::DrawLine () Argument null: gc");
				return;
			}
			
			X11lib.XDrawArc (this.Display, this.Drawable, x11gc, centerX - radiusX, centerY - radiusY, 2 * radiusX, 2 * radiusY, startAngle, sweepAngle);
		}

		/// <summary>Draw the specified arc after setting the specilied color as foreground color.</summary>
		/// <param name="x11gc">The grapchics context to use for drawing.<see cref="IntPtr"/></param>
        /// <param name="centerX">The center point x coordinate.<see cref="System.Int32"/></param>
		/// <param name="centerY">The center point y coordinate.<see cref="System.Int32"/></param>
		/// <param name="radiusX">The radius x coordinate.<see cref="System.Int32"/></param>
		/// <param name="radiusY">The radius y coordinate.<see cref="System.Int32"/></param>
		/// <param name="startAngle">The start angle relative to the 3 o'clock position from the center in units of degrees * 64.<see cref="System.Int32"/></param>
		/// <param name="sweepAngle">The sweep angle relative to the start angle in units of degrees * 64.<see cref="System.Int32"/></param>
		/// <param name="color">The color to use.<see cref="X11.TColor"/></param>
		public void DrawArc (IntPtr x11gc, int centerX, int centerY, int radiusX, int radiusY, int startAngle, int sweepAngle, TColor color)
		{
			SetForeground (x11gc, color);
			DrawArc (x11gc, centerX , centerY, radiusX, radiusY, startAngle, sweepAngle);
		}
		
		#endregion
		
	}
	
}