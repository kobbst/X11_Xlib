// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: August 2014
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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace X11.Text
{
	
	/// <summary>Provide consistent styled text, irrespective whether internationalized text
	/// output is supported (via fontset and TWchar) or not (via single font and XChar2b).</summary>
	public class StyleText
	{
		
        // ###############################################################################
        // ### I N N E R   C L A S S E S
        // ###############################################################################

        #region Inner classes
		
		private class StackableSpan
		{
			public TColor  ForeColor;
			public TColor  BackColor;
			public string  SizeChange;
			public Boolean Underline;
			public Boolean Strike;
			
			public StackableSpan (TColor  foreColor, TColor backColor, string sizeChange, bool underline, bool strike)
			{	ForeColor  = foreColor;
				BackColor  = backColor;
				SizeChange = sizeChange;
				Underline  = underline;
				Strike     = strike;
			}
		}
		
        #endregion Inner classes

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string						CLASS_NAME       = "StyleText";
		
		public const int						SIZE_XX_SMALL    = 7;
		public const int						SIZE_X_SMALL     = 8;
		public const int						SIZE_SMALL       = 10;
		public const int						SIZE_MEDIUM      = 12;
		public const int						SIZE_LARGE       = 14;
		public const int						SIZE_X_LARGE     = 18;
		public const int						SIZE_XX_LARGE    = 24;

        /// <summary>The strike on tag for C# text.</summary>
        public static readonly string			MARKUP_ON        = "<markup>";

        /// <summary>The strike off tag for C# text.</summary>
        public static readonly string			MARKUP_OFF       = "</markup>";

        /// <summary>The strike on tag for C# text.</summary>
        public static readonly string			STRIKE_ON        = "<s>";

        /// <summary>The strike off tag for C# text.</summary>
        public static readonly string			STRIKE_OFF       = "</s>";

        /// <summary>The underline on tag for C# text.</summary>
        public static readonly string			UNDERLINE_ON     = "<u>";

        /// <summary>The underline off tag for C# text.</summary>
        public static readonly string			UNDERLINE_OFF    = "</u>";

        /// <summary>The bold on tag for C# text.</summary>
        public static readonly string			BOLD_ON          = "<b>";

        /// <summary>The bold off tag for C# text.</summary>
        public static readonly string			BOLD_OFF         = "</b>";

        /// <summary>The italic on tag for C# text.</summary>
        public static readonly string			ITALIC_ON        = "<i>";

        /// <summary>The italic off tag for C# text.</summary>
        public static readonly string			ITALIC_OFF       = "</i>";

        /// <summary>The big tag for C# text.</summary>
        public static readonly string			BIG              = "<big>";

        /// <summary>The small tag for C# text.</summary>
        public static readonly string			SMALL            = "<small>";

        /// <summary>The span on tag for C# text.</summary>
        public static readonly string			SPAN_ON          = "<span*>";

        /// <summary>The span off tag for C# text.</summary>
        public static readonly string			SPAN_OFF         = "</span>";

        /// <summary>The strike on tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	STRIKE_ON16      = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('s'), new X11lib.XChar2b ('>') };

        /// <summary>The strike off tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	STRIKE_OFF16     = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('\\'), new X11lib.XChar2b ('s'), new X11lib.XChar2b ('>') };

        /// <summary>The underline on tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	UNDERLINE_ON16   = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('u'), new X11lib.XChar2b ('>') };

        /// <summary>The underline off tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	UNDERLINE_OFF16  = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('\\'), new X11lib.XChar2b ('u'), new X11lib.XChar2b ('>') };

        /// <summary>The bold on tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	BOLD_ON16        = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('b'), new X11lib.XChar2b ('>') };

        /// <summary>The bold off tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	BOLD_OFF16       = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('\\'), new X11lib.XChar2b ('b'), new X11lib.XChar2b ('>') };

        /// <summary>The italic on tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	ITALIC_ON16      = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('i'), new X11lib.XChar2b ('>') };

        /// <summary>The italic off tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	ITALIC_OFF16     = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('\\'), new X11lib.XChar2b ('i'), new X11lib.XChar2b ('>') };

        /// <summary>The big tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	BIG16            = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('b'), new X11lib.XChar2b ('i'), new X11lib.XChar2b ('g'), new X11lib.XChar2b ('>') };

        /// <summary>The small tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	SAMLL16          = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('s'), new X11lib.XChar2b ('m'), new X11lib.XChar2b ('a'), new X11lib.XChar2b ('l'), new X11lib.XChar2b ('l'), new X11lib.XChar2b ('>') };

        /// <summary>The span on tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	SPAN_ON16        = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('s'), new X11lib.XChar2b ('p'), new X11lib.XChar2b ('a'), new X11lib.XChar2b ('n'), new X11lib.XChar2b ('*'), new X11lib.XChar2b ('>') };

        /// <summary>The span on tag for 2 byte array text.</summary>
        public static readonly X11lib.XChar2b[]	SPAN_OFF16       = { new X11lib.XChar2b ('<'), new X11lib.XChar2b ('/'), new X11lib.XChar2b ('s'), new X11lib.XChar2b ('p'), new X11lib.XChar2b ('a'), new X11lib.XChar2b ('n'), new X11lib.XChar2b ('>') };

        /// <summary>The strike on tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		STRIKE_ON32      = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('s'),  X11.X11Utils.CharToWchar ('>') };

        /// <summary>The strike off tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		STRIKE_OFF32     = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('\\'), X11.X11Utils.CharToWchar ('s'), X11.X11Utils.CharToWchar ('>') };

        /// <summary>The underline on tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		UNDERLINE_ON32   = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('u'),  X11.X11Utils.CharToWchar ('>') };

        /// <summary>The underline off tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		UNDERLINE_OFF32  = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('\\'), X11.X11Utils.CharToWchar ('u'), X11.X11Utils.CharToWchar ('>') };

        /// <summary>The bold on tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		BOLD_ON32        = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('b'),  X11.X11Utils.CharToWchar ('>') };

        /// <summary>The bold off tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		BOLD_OFF32       = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('\\'), X11.X11Utils.CharToWchar ('b'), X11.X11Utils.CharToWchar ('>') };

        /// <summary>The italic on tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		ITALIC_ON32      = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('i'),  X11.X11Utils.CharToWchar ('>') };

        /// <summary>The italic off tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		ITALIC_OFF32     = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('\\'), X11.X11Utils.CharToWchar ('i'), X11.X11Utils.CharToWchar ('>') };

        /// <summary>The big tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		BIG32            = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('b'),  X11.X11Utils.CharToWchar ('i'), X11.X11Utils.CharToWchar ('g'),  X11.X11Utils.CharToWchar ('>') };

        /// <summary>The small tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		SMALL32          = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('s'), X11.X11Utils.CharToWchar ('m'), X11.X11Utils.CharToWchar ('a'), X11.X11Utils.CharToWchar ('l'), X11.X11Utils.CharToWchar ('l'), X11.X11Utils.CharToWchar ('>') };

        /// <summary>The span on tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		SPAN_ON32       = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('s'),  X11.X11Utils.CharToWchar ('p'), X11.X11Utils.CharToWchar ('a'), X11.X11Utils.CharToWchar ('n'), X11.X11Utils.CharToWchar ('*'), X11.X11Utils.CharToWchar ('>') };

        /// <summary>The span on tag for 2 byte array text.</summary>
        public static readonly X11.TWchar[]		SPAN_OFF32      = { X11.X11Utils.CharToWchar ('<'), X11.X11Utils.CharToWchar ('/'),  X11.X11Utils.CharToWchar ('s'), X11.X11Utils.CharToWchar ('p'), X11.X11Utils.CharToWchar ('a'), X11.X11Utils.CharToWchar ('n'), X11.X11Utils.CharToWchar ('>') };
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The text as StyleChar array for drawing.</summary>
		private List<TStyleChar[]>	_text			= new List<TStyleChar[]> ();

        #endregion

        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="text">The text as C# string (2 byte (0...65.536) character array).<see cref="System.String"/></param>
		/// <param name="styleList">The list of text styles. Can be null.<see cref="StyleList"/></param>
		/// <param name="surface">The surface, required to prepare additional styles. Can be null.<see cref="X11Surface"/></param>
		/// <param name="parseMarkup">The surface, required to prepare additional styles. Can be null.<see cref="System.Boolean"/></param>
		/// <remarks>If 'styleList' is null or empty, the resultant StyleText is style-less. Otherwise new styles can be added, if required.</remarks>
		/// <remarks>If 'surface' is null, the resultant StyleText can't contain colored styles.</remarks>
		public StyleText (string text, StyleList styleList, X11Surface surface, bool parseMarkup)
		{
			if (string.IsNullOrEmpty (text))
			{
				_text = new List<TStyleChar[]> ();
				return;
			}
			
			List<TStyleChar> newText = new List<TStyleChar> ();
			if (!parseMarkup)
			{
				int currentStyleIndex = -1;
				
				if (styleList != null && styleList.Count > 0)
					currentStyleIndex = 0;

				for (int index = 0; index < text.Length; index++)
					newText.Add (new TStyleChar (text[index], currentStyleIndex));
			}
			else
			{
				int   stackMarkupText   = 0;
				int   currentStyleIndex = -1;
				int   stackStrikeout    = 0;
				int   stackUnderline    = 0;
				int   stackBold         = 0;
				int   stackItalic       = 0;
				
				Stack<StackableSpan> stackSpan = new Stack<StackableSpan> ();
				
				if (styleList != null && styleList.Count > 0)
				{
					currentStyleIndex = 0;
					if ((styleList[currentStyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
						stackStrikeout++;
					if ((styleList[currentStyleIndex].FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
						stackUnderline++;
				}
				
				int index = 0;
				for (; index < text.Length; index++)
				{
					if (text.SubstringEqual (index, MARKUP_ON) == true)
					{
						stackMarkupText++;
						index += MARKUP_ON.Length - 1;
						continue;
					}
					if (text.SubstringEqual (index, MARKUP_OFF) == true)
					{
						stackMarkupText--;
						index += MARKUP_OFF.Length - 1;
						continue;
					}
					if (stackMarkupText > 0 && styleList != null && styleList.Count > 0)
					{
						// <s>
						if (text.SubstringEqual (index, STRIKE_ON) == true)
						{
							stackStrikeout++;
							
							if (stackStrikeout == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Strikeout) != System.Drawing.FontStyle.Strikeout)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle | System.Drawing.FontStyle.Strikeout, style.FontData);
							}
							index += STRIKE_ON.Length - 1;
							continue;
						}
						// </s>
						else if (text.SubstringEqual (index, STRIKE_OFF) == true)
						{
							stackStrikeout--;
							
							if (stackStrikeout == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle & ~System.Drawing.FontStyle.Strikeout, style.FontData);
							}
							index += STRIKE_OFF.Length - 1;
							continue;
						}
						// <u>
						else if (text.SubstringEqual (index, UNDERLINE_ON) == true)
						{
							stackUnderline++;
							
							if (stackUnderline == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Underline) != System.Drawing.FontStyle.Underline)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle | System.Drawing.FontStyle.Underline, style.FontData);
							}
							index += UNDERLINE_ON.Length - 1;
							continue;
						}
						// </u>
						else if (text.SubstringEqual (index, UNDERLINE_OFF) == true)
						{
							stackUnderline--;
							
							if (stackUnderline == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle & ~System.Drawing.FontStyle.Underline, style.FontData);
							}
							index += UNDERLINE_OFF.Length - 1;
							continue;
						}
						// <b>
						else if (text.SubstringEqual (index, BOLD_ON) == true)
						{
							stackBold++;
							
							if (stackBold == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if (surface != null && style.FontData.GetWeight() != "bold")
								{	string fs = X11FontData.ModifyFontSpecificationWieght (style.FontData.FontSpecification, "bold");
									X11FontData newFontData = null;
									if (X11FontService.PrepareFont (fs, surface.Display, style.FontData.UseFontset, ref newFontData) == true)
										currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle, newFontData);
								}
							}
							index += BOLD_ON.Length - 1;

							continue;
						}
						// </b>
						else if (text.SubstringEqual (index, BOLD_OFF) == true)
						{
							stackBold--;
							
							if (stackBold == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if (surface != null && style.FontData.GetWeight() == "bold")
								{	string fs = X11FontData.ModifyFontSpecificationWieght (style.FontData.FontSpecification, "medium");
									X11FontData newFontData = null;
									if (X11FontService.PrepareFont (fs, surface.Display, style.FontData.UseFontset, ref newFontData) == true)
										currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle, newFontData);
								}
							}
							index += BOLD_OFF.Length - 1;
							continue;
						}
						// <i>
						else if (text.SubstringEqual (index, ITALIC_ON) == true)
						{
							stackItalic++;
							
							if (stackItalic == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if (surface != null && style.FontData.GetSlant() != "i" && style.FontData.GetSlant() != "o")
								{	string fs = X11FontData.ModifyFontSpecificationSlant (style.FontData.FontSpecification, "o");
									X11FontData newFontData = null;
									if (X11FontService.PrepareFont (fs, surface.Display, style.FontData.UseFontset, ref newFontData) == true)
										currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle, newFontData);
								}
							}
							index += ITALIC_ON.Length - 1;

							continue;
						}
						// </i>
						else if (text.SubstringEqual (index, ITALIC_OFF) == true)
						{
							stackItalic--;
							
							if (stackItalic == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if (surface != null && style.FontData.GetSlant() == "o")
								{	string fs = X11FontData.ModifyFontSpecificationSlant (style.FontData.FontSpecification, "r");
									X11FontData newFontData = null;
									if (X11FontService.PrepareFont (fs, surface.Display, style.FontData.UseFontset, ref newFontData) == true)
										currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle, newFontData);
								}
							}
							index += ITALIC_OFF.Length - 1;
							continue;
						}
						// <big>
						else if (text.SubstringEqual (index, BIG) == true)
						{
							ITextStyle style = styleList[currentStyleIndex];
							int    newSize   = IncrementFontSize ((int)(style.FontData.GetSize () + 0.5F));
							string fontSpec  = X11FontData.ModifyFontSpecificationSize (style.FontData.FontSpecification, newSize);
							X11FontData newFontData = null;
							if (surface != null && X11FontService.PrepareFont (fontSpec, surface.Display, style.FontData.UseFontset, ref newFontData) == true)
								currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle, newFontData);
						
							index += BIG.Length - 1;
							continue;
						}
						// <small>
						else if (text.SubstringEqual (index, SMALL) == true)
						{
							ITextStyle style = styleList[currentStyleIndex];
							int    newSize   = DecrementFontSize((int)(style.FontData.GetSize () + 0.5F));
							string fontSpec  = X11FontData.ModifyFontSpecificationSize (style.FontData.FontSpecification, newSize);
							X11FontData newFontData = null;
							if (surface != null && X11FontService.PrepareFont (fontSpec, surface.Display, style.FontData.UseFontset, ref newFontData) == true)
								currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle, newFontData);
						
							index += SMALL.Length - 1;
							continue;
						}
						// <span> and </span>
						else
						{
							// <span>
							string match = text.SubstringMatch (index, SPAN_ON);
							if (!string.IsNullOrEmpty (match))
							{
								ITextStyle style = styleList[currentStyleIndex];
								
								string  foreground = string.Empty;
								string  background = string.Empty;
								bool    underline  = style != null ? (style.FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline : false;
								bool    strikeout  = style != null ? (style.FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout : false;
								TColor  fgColor    = style != null ? style.ForeColor : TColor.FallbackBlack;
								TColor  bgColor    = style != null ? style.BackColor : TColor.FallbackBlack;
								string  slant      = string.Empty;
								string  weight     = string.Empty;
								string  sizeChange = string.Empty;
								string  size       = string.Empty;
								
								for (int fgIndex = 0; fgIndex < match.Length; fgIndex++)
								{
									string foregroundMatch = match.SubstringMatch (fgIndex, "foreground=\"*\"");
									if (!string.IsNullOrEmpty (foregroundMatch))
									{
										foreground = foregroundMatch.Replace ("foreground=\"", "").Replace ("\"", "").Trim ();
									}
									string backgroundMatch = match.SubstringMatch (fgIndex, "background=\"*\"");
									if (!string.IsNullOrEmpty (backgroundMatch))
									{
										background = backgroundMatch.Replace ("background=\"", "").Replace ("\"", "").Trim ();
									}
									string underlineMatch = match.SubstringMatch (fgIndex, "underline=\"*\"");
									if (!string.IsNullOrEmpty (underlineMatch))
									{
										underlineMatch = underlineMatch.Replace ("underline=\"", "").Replace ("\"", "").Trim ();
										if (underlineMatch.ToLower() == "single" ||
										    underlineMatch.ToLower() == "double" ||
										    underlineMatch.ToLower() == "low")
											underline = true;
									}
									string strikeoutMatch = match.SubstringMatch (fgIndex, "strikethrough=\"*\"");
									if (!string.IsNullOrEmpty (strikeoutMatch))
									{
										strikeoutMatch = strikeoutMatch.Replace ("strikethrough=\"", "").Replace ("\"", "").Trim ();
										if (strikeoutMatch.ToLower() == "true")
											strikeout = true;
									}
									string slantMatch = match.SubstringMatch (fgIndex, "style=\"*\"");
									if (!string.IsNullOrEmpty (slantMatch))
									{
										slant = slantMatch.Replace ("style=\"", "").Replace ("\"", "").Trim ();
										if ((style.FontData.GetSlant () == "o" || style.FontData.GetSlant () == "i") &&
										    (slant == "oblique" || slant == "italic"))
										    slant = string.Empty;
										if ((style.FontData.GetSlant () != "o" && style.FontData.GetSlant () != "i") &&
										    (slant == "normal"))
										    slant = string.Empty;
									}
									string weightMatch = match.SubstringMatch (fgIndex, "weight=\"*\"");
									if (!string.IsNullOrEmpty (weightMatch))
									{
										weight = weightMatch.Replace ("weight=\"", "").Replace ("\"", "").Trim ();
										if ((style.FontData.GetWeight () == "bold") &&
											(weight == "bold" || weight == "ultrabold" || weight == "heavy"))
										    weight = string.Empty;
										if ((style.FontData.GetWeight () != "bold") &&
											(weight == "ultralight" || weight == "light" || weight == "normal"))
										    weight = string.Empty;
									}
									string sizeMatch = match.SubstringMatch (fgIndex, "size=\"*\"");
									if (!string.IsNullOrEmpty (sizeMatch))
									{
										sizeMatch = sizeMatch.Replace ("size=\"", "").Replace ("\"", "").Trim ();
										size = sizeMatch;
										if (sizeMatch == "larger" || sizeMatch == "smaller")
										{
											sizeChange = sizeMatch;
											size = sizeMatch;
										}
										if (sizeMatch == "xx-small" || sizeMatch == "x-small" || sizeMatch == "small" || sizeMatch == "medium" ||
											sizeMatch == "large"    || sizeMatch == "x-large" || sizeMatch == "xx-large")
										{
											sizeChange = style.FontData.GetSize ().ToString ();
											size = sizeMatch;
										}
									}
								}

								if (surface != null)
								{
									if (!string.IsNullOrEmpty (foreground))
										fgColor = X11lib.XAllocClosestNamedColor (surface.Display, surface.Colormap, foreground);
									if (!string.IsNullOrEmpty (background))
										bgColor = X11lib.XAllocClosestNamedColor (surface.Display, surface.Colormap, background);
								}
								
								// The <u> and <s> flags don't need a new FontData instance.
								System.Drawing.FontStyle fs = System.Drawing.FontStyle.Regular;
								if (underline)
									fs |= System.Drawing.FontStyle.Underline;
								if (strikeout)
									fs |= System.Drawing.FontStyle.Strikeout;
								
								if (surface != null)
								{
									// The style (<i>), weight (<b>) and size (<big>, ...) attributes need a new FontData instance.
									string fontSpec = style.FontData.FontSpecification;
									
									if (slant == "oblique" || slant == "italic")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSlant (fontSpec, "o");
									}
									if (weight == "bold" || weight == "ultrabold" || weight == "heavy")
									{
										fontSpec = X11FontData.ModifyFontSpecificationWieght (fontSpec, "bold");
									}
									if (weight == "ultralight" || weight == "light" || weight == "normal")
									{
										fontSpec = X11FontData.ModifyFontSpecificationWieght (fontSpec, "regular");
									}
										
									if (size == "larger")
									{
										float newSize  = IncrementFontSize ((int)(style.FontData.GetSize () + 0.5F));
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, newSize);
									}
									if (size == "smaller")
									{
										float newSize  = DecrementFontSize ((int)(style.FontData.GetSize () + 0.5F));
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, newSize);
									}
									if (size == "xx-small")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_XX_SMALL);
									}
									if (size == "x-small")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_X_SMALL);
									}
									if (size == "small")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_SMALL);
									}
									if (size == "medium")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_MEDIUM);
									}
									if (size == "large")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_LARGE);
									}
									if (size == "x-large")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_X_LARGE);
									}
									if (size == "xx-large")
									{
										fontSpec = X11FontData.ModifyFontSpecificationSize (fontSpec, SIZE_XX_LARGE);
									}
									
									X11FontData newFontData = null;
									if (fontSpec != style.FontData.FontSpecification)
									{
										X11FontService.PrepareFont (fontSpec, surface.Display, style.FontData.UseFontset, ref newFontData);
									}
									

									if (style.ForeColor != fgColor ||
									    style.BackColor != bgColor ||
									    style.FontStyle != fs ||
									    newFontData     != null)
									{
										if (newFontData == null)
											newFontData = style.FontData;
										
										// Remember old style before style change.
										stackSpan.Push (new StackableSpan (style.ForeColor, style.BackColor, sizeChange,
										                                   (style.FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline,
										                                   (style.FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout));
										// Prepare new style.
										currentStyleIndex = styleList.GetOrCreateStyle (fgColor, bgColor, fs, newFontData);
									}
								}
								index += match.Length - 1;
								continue;
							}
							// <span>
							else if (text.SubstringEqual (index, SPAN_OFF) == true)
							{
								ITextStyle style = styleList[currentStyleIndex];
								StackableSpan ss = stackSpan.Count > 0 ? stackSpan.Pop () : null;
								
								if (ss != null)
								{	
									// The <u> and <s> flags don't need a new FontData instance.
									System.Drawing.FontStyle fs = System.Drawing.FontStyle.Regular;
									if (ss.Underline)
										fs |= System.Drawing.FontStyle.Underline;
									if (ss.Strike)
										fs |= System.Drawing.FontStyle.Strikeout;
									
									// The style (<i>), weight (<b>) and size (<big>, ...) attributes need to restore old FontData instance.
									X11FontData oldFontData = null;
									float oldSize = style.FontData.GetSize ();
									if (ss.SizeChange == "larger")
										oldSize = DecrementFontSize ((int)(style.FontData.GetSize () + 0.5F));
									else if (ss.SizeChange == "smaller")
										oldSize = IncrementFontSize ((int)(style.FontData.GetSize () + 0.5F));
									else if (!string.IsNullOrEmpty (ss.SizeChange))
									{
										float.TryParse (ss.SizeChange, out oldSize);
									}
									if (!string.IsNullOrEmpty (ss.SizeChange))
									{
										string      fontSpec    = X11FontData.ModifyFontSpecificationSize (style.FontData.FontSpecification, oldSize);
										X11FontService.PrepareFont (fontSpec, surface.Display, style.FontData.UseFontset, ref oldFontData);
									}
									
									if (oldFontData == null)
										oldFontData = style.FontData;
									currentStyleIndex = styleList.GetOrCreateStyle (ss.ForeColor, ss.BackColor, fs, oldFontData);
								}
								index += "</span>".Length - 1;
								continue;
							}
						}
					}
					if (text[index] == '\n' || index < text.Length - 1 && text[index] == '\r' && text[index + 1] == '\n')
					{
						_text.Add (newText.ToArray());
						newText.Clear ();
					}
					else
						newText.Add (new TStyleChar (text[index], currentStyleIndex));
				}
			}
			_text.Add (newText.ToArray());
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="text16">The text as 2 byte array for drawing with X11lib.XDrawString16().<see cref="X11lib.XChar2b[]"/></param>
		/// <param name="styleList">The list of text styles. Can be null.<see cref="StyleList"/></param>
		/// <param name="surface">The surface, required to prepare additional styles. Can be null.<see cref="X11Surface"/></param>
		/// <param name="parseMarkup">The surface, required to prepare additional styles. Can be null.<see cref="System.Boolean"/></param>
		/// <remarks>If 'styleList' is null or empty, the resultant StyleText is style-less. Otherwise new styles can be added, if required.</remarks>
		/// <remarks>If 'surface' is null, the resultant StyleText can't contain colored styles.</remarks>
		public StyleText (X11lib.XChar2b[] text16, StyleList styleList, X11Surface surface, bool parseMarkup)
		{
			if (text16 == null || text16.Length == 0 || (text16.Length == 1 && text16[0] == X11lib.XChar2b.Zero))
			{
				_text = new List<TStyleChar[]> ();
				return;
			}
			
			List<TStyleChar> newText = new List<TStyleChar> ();
			if (!parseMarkup)
			{
				int currentStyleIndex = -1;
				
				if (styleList != null && styleList.Count > 0)
					currentStyleIndex = 0;

				for (int index = 0; index < text16.Length; index++)
					newText.Add (new TStyleChar (text16[index], currentStyleIndex));
			}
			else
			{
				int currentStyleIndex = -1;
				int stackStrikeout    = 0;
				int stackUnderline    = 0;
				X11lib.XChar2b cr     = new X11lib.XChar2b ('\r');
				X11lib.XChar2b nl     = new X11lib.XChar2b ('\n');
				
				if (styleList != null && styleList.Count > 0)
				{
					currentStyleIndex = 0;
					if ((styleList[currentStyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
						stackStrikeout++;
					if ((styleList[currentStyleIndex].FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
						stackUnderline++;
				}
				
				int index = 0;
				for (; index < text16.Length; index++)
				{
					if (styleList != null && styleList.Count > 0)
					{
						if (X11.X11Utils.SubstringEqual (text16, index, STRIKE_ON16) == true)
						{
							stackStrikeout++;
							
							if (stackStrikeout == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Strikeout) != System.Drawing.FontStyle.Strikeout)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle | System.Drawing.FontStyle.Strikeout, style.FontData);
							}
							index += STRIKE_ON16.Length - 1;
							continue;
						}
						else if (X11.X11Utils.SubstringEqual (text16, index, STRIKE_OFF16) == true)
						{
							stackStrikeout--;
							
							if (stackStrikeout == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle & ~System.Drawing.FontStyle.Strikeout, style.FontData);
							}
							index += STRIKE_OFF16.Length - 1;
							continue;
						}
						else if (X11.X11Utils.SubstringEqual (text16, index, UNDERLINE_ON16) == true)
						{
							stackUnderline++;
							
							if (stackUnderline == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Underline) != System.Drawing.FontStyle.Underline)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle | System.Drawing.FontStyle.Underline, style.FontData);
							}
							index += UNDERLINE_ON16.Length - 1;
							continue;
						}
						else if (X11.X11Utils.SubstringEqual (text16, index, UNDERLINE_OFF16) == true)
						{
							stackUnderline--;
							
							if (stackUnderline == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle & ~System.Drawing.FontStyle.Underline, style.FontData);
							}
							index += UNDERLINE_OFF16.Length - 1;
							continue;
						}
						else
						{
							X11lib.XChar2b[] match = X11.X11Utils.SubstringMatch (text16, index, SPAN_ON16);
							if (match == null || match.Length == 0)
							{
							}
						}
					}
					if (text16[index] == nl || index < text16.Length - 1 && text16[index] == cr && text16[index + 1] == nl)
					{
						_text.Add (newText.ToArray());
						newText.Clear ();
					}
					else
						newText.Add (new TStyleChar (text16[index], currentStyleIndex));
				}
			}
			_text.Add (newText.ToArray());
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="text32">The text as 4 byte array for drawing with X11lib.XwcDrawString().<see cref="X11.TWchar[]"/></param>
		/// <param name="styleList">The list of text styles. Can be null.<see cref="StyleList"/></param>
		/// <param name="surface">The surface, required to prepare additional styles. Can be null.<see cref="X11Surface"/></param>
		/// <param name="parseMarkup">The surface, required to prepare additional styles. Can be null.<see cref="System.Boolean"/></param>
		/// <remarks>If 'styleList' is null or empty, the resultant StyleText is style-less. Otherwise new styles can be added, if required.</remarks>
		/// <remarks>If 'surface' is null, the resultant StyleText can't contain colored styles.</remarks>
		public StyleText (X11.TWchar[] text32, StyleList styleList, X11Surface surface, bool parseMarkup)
		{
			if (text32 == null || text32.Length == 0 || (text32.Length == 1 && text32[0] == 0))
			{
				_text = new List<TStyleChar[]> ();
				return;
			}
			
			List<TStyleChar> newText = new List<TStyleChar> ();
			if (!parseMarkup)
			{
				int currentStyleIndex = -1;
				
				if (styleList != null && styleList.Count > 0)
					currentStyleIndex = 0;

				for (int index = 0; index < text32.Length; index++)
					newText.Add (new TStyleChar (text32[index], currentStyleIndex));
			}
			else
			{
				int currentStyleIndex = -1;
				int stackStrikeout    = 0;
				int stackUnderline    = 0;
				
				if (styleList != null && styleList.Count > 0)
				{
					currentStyleIndex = 0;
					if ((styleList[currentStyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
						stackStrikeout++;
					if ((styleList[currentStyleIndex].FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
						stackUnderline++;
				}
				
				int index = 0;
				for (; index < text32.Length; index++)
				{
					if (styleList != null && styleList.Count > 0)
					{
						if (X11.X11Utils.SubstringEqual (text32, index, STRIKE_ON32) == true)
						{
							stackStrikeout++;
							
							if (stackStrikeout == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Strikeout) != System.Drawing.FontStyle.Strikeout)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle | System.Drawing.FontStyle.Strikeout, style.FontData);
							}
							index += STRIKE_ON32.Length - 1;
							continue;
						}
						else if (X11.X11Utils.SubstringEqual (text32, index, STRIKE_OFF32) == true)
						{
							stackStrikeout--;
							
							if (stackStrikeout == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle & ~System.Drawing.FontStyle.Strikeout, style.FontData);
							}
							index += STRIKE_OFF32.Length - 1;
							continue;
						}
						else if (X11.X11Utils.SubstringEqual (text32, index, UNDERLINE_ON32) == true)
						{
							stackUnderline++;
							
							if (stackUnderline == 1)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Underline) != System.Drawing.FontStyle.Underline)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle | System.Drawing.FontStyle.Underline, style.FontData);
							}
							index += UNDERLINE_ON32.Length - 1;
							continue;
						}
						else if (X11.X11Utils.SubstringEqual (text32, index, UNDERLINE_OFF32) == true)
						{
							stackUnderline--;
							
							if (stackUnderline == 0)
							{	ITextStyle style = styleList[currentStyleIndex];
								if ((style.FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
									currentStyleIndex = styleList.GetOrCreateStyle (style.ForeColor, style.BackColor, style.FontStyle & ~System.Drawing.FontStyle.Underline, style.FontData);
							}
							index += UNDERLINE_OFF32.Length - 1;
							continue;
												}
						else
						{
							X11.TWchar[] match = X11.X11Utils.SubstringMatch (text32, index, SPAN_ON32);
							if (match == null || match.Length == 0)
							{
							}
						}
					}
					if (text32[index] == (X11.TWchar)'\n' || index < text32.Length - 1 && text32[index] == (X11.TWchar)'\r' && text32[index + 1] == (X11.TWchar)'\n')
					{
						_text.Add (newText.ToArray());
						newText.Clear ();
					}
					else
						newText.Add (new TStyleChar (text32[index], currentStyleIndex));
				}
			}
			_text.Add (newText.ToArray());
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary>Get the number of style character lines.</summary>
		public int LineCount
		{	get	{	return _text.Count;		}	}
		
		/// <summary>Get the total number (for all lines) of style characters.</summary>
		public int TotalCharCount
		{	get
			{	int length = 0;
				foreach (TStyleChar[] line in _text)
					length += line.Length;
				return length;	}
		}
		
		/// <summary>Get the indicated line of style characters on success, or null otherwise.</summary>
		public TStyleChar[] this[int lineIndex]
		{	get
			{	if (lineIndex >= 0 && lineIndex < _text.Count)
					return _text[lineIndex];
				return null;
			}
		}
		
		/// <summary>Get the indicated style character on success, or null otherwise.</summary>
		/// <exception cref="IndexOutOfRangeException">If 'place.LineIndex' is smaller zero or exceeds the available lines.</exception>
		/// <exception cref="IndexOutOfRangeException">If 'place.CharIndex' is smaller zero or exceeds the available characters.</exception>
		public TStyleChar this[Place place]
		{	get
			{	if (place.LineIndex < 0 || place.LineIndex >= _text.Count)
					throw new IndexOutOfRangeException ();

				TStyleChar[] line = _text[place.LineIndex];
				if (line == null || place.CharIndex < 0 || place.CharIndex >= line.Length)
					throw new IndexOutOfRangeException ();

				return line[place.CharIndex];
			}
		}
		
		/// <summary>Get the first possible position of style characters.</summary>
		public Place Start
		{	get	{	return new Place (0, 0);	}
		}
		
		/// <summary>Get the last possible position of style characters.</summary>
		public Place End
		{	get
			{	int lineIndex = Math.Max (0, _text.Count - 1);
				return new Place (_text.Count > 0 && _text[lineIndex] != null ? _text[lineIndex].Length - 1 : -1, lineIndex);
			}
		}
		
		#endregion
	
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		/// <summary>Increment the font size using common sizes. 8, 10, 12, 14, 18, 24</summary>
		/// <param name="oldSize">The size to increment.<see cref="System.Int32"/></param>
		/// <returns>The new size.<see cref="System.Int32"/></returns>
		public int IncrementFontSize (int oldSize)
		{
			if (oldSize < SIZE_X_SMALL)
				return SIZE_X_SMALL;
			if (oldSize < SIZE_SMALL)
				return SIZE_SMALL;
			if (oldSize < SIZE_MEDIUM)
				return SIZE_MEDIUM;
			if (oldSize < SIZE_LARGE)
				return SIZE_LARGE;
			if (oldSize < SIZE_X_LARGE)
				return SIZE_X_LARGE;
			if (oldSize < SIZE_XX_LARGE)
				return SIZE_XX_LARGE;
			
			// Fallback.
			return 14;
		}
	
		/// <summary>Decrement the font size using common sizes. 8, 10, 12, 14, 18, 24</summary>
		/// <param name="oldSize">The size to increment.<see cref="System.Int32"/></param>
		/// <returns>The new size.<see cref="System.Int32"/></returns>
		public int DecrementFontSize (int oldSize)
		{
			if (oldSize > SIZE_X_LARGE)
				return SIZE_X_LARGE;
			if (oldSize > SIZE_LARGE)
				return SIZE_LARGE;
			if (oldSize > SIZE_MEDIUM)
				return SIZE_MEDIUM;
			if (oldSize > SIZE_SMALL)
				return SIZE_SMALL;
			if (oldSize > SIZE_X_SMALL)
				return SIZE_X_SMALL;
			if (oldSize > SIZE_XX_SMALL)
				return SIZE_XX_SMALL;
			
			// Fallback.
			return 14;
		}
		
		#region Character and style access methods
		
		/// <summary>Get the indicated character as C# character (2 byte (0...65.536) character).</summary>
		/// <param name="lineIndex">The index of the line, to get the the text as C# string for.<see cref="System.Int32"/></param>
		/// <param name="charIndex">The index of the requested character.<see cref="System.Int32"/></param>
		/// <returns>The text as C# character (2 byte (0...65.536) character).<see cref="System.Char"/></returns>
		public char C (int lineIndex, int charIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return (char)0;
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || charIndex < 0 || charIndex >= line.Length)
				return (char)0;
			
			return line[charIndex].C;
		}
		
		/// <summary>Get the indicated character as 2 byte character for drawing with X11lib.XDrawString16().</summary>
		/// <param name="lineIndex">The index of the line, to get the the text as C# string for.<see cref="System.Int32"/></param>
		/// <param name="charIndex">The index of the requested character.<see cref="System.Int32"/></param>
		/// <returns>The text as 2 byte character for drawing with X11lib.XDrawString16().<see cref="X11lib.XChar2b"/></returns>
		public X11lib.XChar2b C16 (int lineIndex, int charIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return new X11lib.XChar2b((TUchar)0, (TUchar)0);
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || charIndex < 0 || charIndex >= line.Length)
				return new X11lib.XChar2b((TUchar)0, (TUchar)0);
			
			return line[charIndex].C16;
		}
		
		/// <summary>Get the indicated character as 4 byte character for drawing with X11lib.XwcDrawString().</summary>
		/// <param name="lineIndex">The index of the line, to get the the text as C# string for.<see cref="System.Int32"/></param>
		/// <param name="charIndex">The index of the requested character.<see cref="System.Int32"/></param>
		/// <returns>The text as 4 byte character for drawing with X11lib.XwcDrawString().<see cref="X11.TWchar"/></returns>
		public X11.TWchar C32 (int lineIndex, int charIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return (X11.TWchar)0;
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || charIndex < 0 || charIndex >= line.Length)
				return (X11.TWchar)0;
			
			return line[charIndex].C32;
		}
		
		/// <summary>Get the indicated style character's style index.</summary>
		/// <param name="lineIndex">The index of the line, to get the the text as C# string for.<see cref="System.Int32"/></param>
		/// <param name="charIndex">The index of the requested style character's style index.<see cref="System.Int32"/></param>
		/// <returns>The style character's style index on success, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int StyleIndex (int lineIndex, int charIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return -1;
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || charIndex <0 || charIndex >= line.Length)
				return -1;
			return line[charIndex].StyleIndex;
		}
	
		/// <summary>Count the number of different referenced styles.</summary>
		/// <returns>The number of different referenced styles.<see cref="System.Int32"/></returns>
		public int CountReferencedStyles ()
		{
			List<int> styleIndices = new List<int> ();
			for (int lineIndex = 0; lineIndex < _text.Count; lineIndex++)
			{
				TStyleChar[] line = _text[lineIndex];
				for (int index = 0; index < line.Length; index++)
				{
					int styleIndex = line[index].StyleIndex;
					if (!styleIndices.Contains (styleIndex))
						styleIndices.Add (styleIndex);
				}
			}
			return styleIndices.Count;
		}
		
		#endregion Character and style access methods
	
		#region Text and subtext access methods
		
		/// <summary>Get the complete (not marked up) text of all lines as C# string (2 byte (0...65.536) character array).</summary>
		/// <returns>The complete (not marked up) text of all lines as C# string (2 byte (0...65.536) character array).<see cref="System.String"/></returns>
		public string TotalText ()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
			for (int lineIndex = 0; lineIndex < _text.Count; lineIndex++)
				sb.Append (Text(lineIndex));
			return sb.ToString ();
		}
		
		/// <summary>Get the (not marked up) text of indicated line as C# string (2 byte (0...65.536) character array).</summary>
		/// <returns>The (not marked up) text as C# string (2 byte (0...65.536) character array).<see cref="X11lib.XChar2b[]"/></returns>
		/// <param name="lineIndex">The index of the line, to get the text as C# string for.<see cref="System.Int32"/></param>
		/// <returns>The text as C# string (2 byte (0...65.536) character array).<see cref="X11lib.XChar2b[]"/></returns>
		public string Text (int lineIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return string.Empty;
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0)
				return string.Empty;
			
			char[] text = new char[line.Length];
			for (int index = 0; index < line.Length; index++)
				text[index] = line[index].C;

			return new string (text);
		}
		
		/// <summary>Get the complete markup text of all lines as C# string (2 byte (0...65.536) character array).</summary>
		/// <param name="styleList">The list of text styles. Can be null.<see cref="StyleList"/></param>
		/// <param name="surface">The surface, required to prepare additional styles. Can be null.<see cref="X11Surface"/></param>
		/// <returns>The complete markup text of all lines as C# string (2 byte (0...65.536) character array).<see cref="System.String"/></returns>
		public string TotalMarkupText (StyleList styleList, X11Surface surface)
		{
			if (styleList == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::TotalMarkupText () Parameter 'styleList' must not be null. Fall back to plain text.");
				return TotalText ();
			}	
			if (surface == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::TotalMarkupText () Parameter 'surface' must not be null. Fall back to plain text.");
				return TotalText ();
			}	
			
			// This shortcut is wrong for <s> and <u>.
			// if (CountReferencedStyles () <= 1)
			//	return TotalText ();

			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
			for (int lineIndex = 0; lineIndex < _text.Count; lineIndex++)
			{
				if (lineIndex > 0)
					sb.Append ("\n");
				sb.Append (MarkupText(lineIndex, styleList, surface));
			}
			
			return sb.ToString ();
		}
		
		/// <summary>Get the markup text of indicated line as C# string (2 byte (0...65.536) character array).</summary>
		/// <returns>The markup text as C# string (2 byte (0...65.536) character array).<see cref="X11lib.XChar2b[]"/></returns>
		/// <param name="lineIndex">The index of the line, to get the text as C# string for.<see cref="System.Int32"/></param>
		/// <param name="styleList">The list of text styles. Can be null.<see cref="StyleList"/></param>
		/// <param name="surface">The surface, required to prepare additional styles. Can be null.<see cref="X11Surface"/></param>
		/// <returns>The markup text as C# string (2 byte (0...65.536) character array).<see cref="X11lib.XChar2b[]"/></returns>
		public string MarkupText (int lineIndex, StyleList styleList, X11Surface surface)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::MarkupText () Parameter 'lineIndex' out of valid range. Skip processing.");
				return string.Empty;
			}					
			
			if (styleList == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::MarkupText () Parameter 'styleList' must not be null. Fall back to plain text.");
				return Text (lineIndex);
			}					
			if (surface == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::MarkupText () Parameter 'surface' must not be null. SFall back to plain text.");
				return Text (lineIndex);
			}
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0)
				return string.Empty;
			
			int stackStrikeout    = 0;
			int stackUnderline    = 0;
			int stackSpan         = 0;
			int stackBold         = 0;
			int stackItalic       = 0;

			List<char> text = new List<char>();
			for (int index = 0; index < line.Length; index++)
			{
				if (index == 0)
				{	// For the first character - there is no predecessor style to compare for differences.
					
					// Ensure there is a style, that can be evaluated.
					if (line[index].StyleIndex >= 0 && line[index].StyleIndex < styleList.Count)
					{
						// At the beginning of a markup only start tags can occure.
						
						// <s>
						if ((styleList[line[index].StyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
						{
							text.Add ('<');text.Add ('s');text.Add ('>');
							stackStrikeout++;
						}
						// <u>
						if ((styleList[line[index].StyleIndex].FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
						{
							text.Add ('<');text.Add ('u');text.Add ('>');
							stackUnderline++;
						}
						// <b>
						if (styleList[line[index].StyleIndex].FontData.GetWeight() == "bold")
						{
							if (stackBold == 0)
							{	text.Add ('<');text.Add ('b');text.Add ('>');	}
							stackBold++;
						}
						// <i>
						if (styleList[line[index].StyleIndex].FontData.GetSlant() == "i" || styleList[line[index].StyleIndex].FontData.GetSlant() == "o")
						{
							if (stackItalic == 0)
							{	text.Add ('<');text.Add ('i');text.Add ('>');	}
							stackItalic++;
						}
						// <span>
						{
							string span = "<span foreground=\"" + surface.HtmlNameForColor (styleList[line[index].StyleIndex].ForeColor) + "\">";
							text.AddRange (span.ToCharArray ());
							stackSpan++;
						}
						if (!styleList[line[index].StyleIndex].BackColor.IsFullyTransparent)
						{
							string span = "<span background=\"" + surface.HtmlNameForColor (styleList[line[index].StyleIndex].BackColor) + "\">";
							text.AddRange (span.ToCharArray ());
							stackSpan++;
						}
					}
				}
				else if (index > 0 && line[index - 1].StyleIndex != line[index].StyleIndex)
				{	// For the second and following characters - there is a predecessor style to compare for differences.
				
					// Ensure there is a style, that can be evaluated.
					if (line[index - 1].StyleIndex >= 0 && line[index - 1].StyleIndex < styleList.Count &&
					    line[index].StyleIndex     >= 0 && line[index].StyleIndex     < styleList.Count)
					{
						// In the middle of a markup start and end tags can occure.
						
						// <s> and </s>
						if ((styleList[line[index - 1].StyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) !=
						    (styleList[line[index].StyleIndex].FontStyle     & System.Drawing.FontStyle.Strikeout))
						{
							if ((styleList[line[index].StyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout)
							{
								if (stackStrikeout == 0)
								{	text.Add ('<');text.Add ('s');text.Add ('>');	}
								stackStrikeout++;
							}
							if ((styleList[line[index].StyleIndex].FontStyle & System.Drawing.FontStyle.Strikeout) != System.Drawing.FontStyle.Strikeout)
							{
								if (stackStrikeout == 1)
								{	text.Add ('<');text.Add ('/');text.Add ('s');text.Add ('>');	}
								stackStrikeout--;
							}
						}
						// <u> and </u>
						if ((styleList[line[index - 1].StyleIndex].FontStyle & System.Drawing.FontStyle.Underline) !=
						    (styleList[line[index].StyleIndex].FontStyle     & System.Drawing.FontStyle.Underline))
						{
							if ((styleList[line[index].StyleIndex].FontStyle & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
							{
								if (stackUnderline == 0)
								{	text.Add ('<');text.Add ('u');text.Add ('>');	}
								stackUnderline++;
							}
							if ((styleList[line[index].StyleIndex].FontStyle & System.Drawing.FontStyle.Underline) != System.Drawing.FontStyle.Underline)
							{
								if (stackUnderline == 1)
								{	text.Add ('<');text.Add ('/');text.Add ('u');text.Add ('>');	}
								stackUnderline--;
							}
						}
						// <b> and </b>
						if (styleList[line[index - 1].StyleIndex].FontData.GetWeight() != "bold" &&
						    styleList[line[index].StyleIndex].FontData.GetWeight()     == "bold")
						{
							if (stackBold == 0)
							{	text.Add ('<');text.Add ('b');text.Add ('>');	}
							stackBold++;
						}
						if (styleList[line[index - 1].StyleIndex].FontData.GetWeight() == "bold" &&
						    styleList[line[index].StyleIndex].FontData.GetWeight()     != "bold")
						{
							if (stackBold == 1)
							{	text.Add ('<');text.Add ('/');text.Add ('b');text.Add ('>');	}
							stackBold--;
						}
						// <i> and </i>
						if ((styleList[line[index - 1].StyleIndex].FontData.GetSlant() != "i" && styleList[line[index - 1].StyleIndex].FontData.GetSlant() != "o") &&
						    (styleList[line[index].StyleIndex].FontData.GetSlant()     == "i" || styleList[line[index].StyleIndex].FontData.GetSlant()     == "o"))
						{
							if (stackItalic == 0)
							{	text.Add ('<');text.Add ('i');text.Add ('>');	}
							stackItalic++;
						}
						if ((styleList[line[index - 1].StyleIndex].FontData.GetSlant() == "i" || styleList[line[index - 1].StyleIndex].FontData.GetSlant() == "o") &&
						    (styleList[line[index].StyleIndex].FontData.GetSlant()     != "i" && styleList[line[index].StyleIndex].FontData.GetSlant()     != "o"))
						{
							if (stackItalic == 1)
							{	text.Add ('<');text.Add ('/');text.Add ('i');text.Add ('>');	}
							stackItalic--;
						}
						
						// <big>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_SMALL ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_SMALL   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_SMALL    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_MEDIUM  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_LARGE   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_LARGE    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_LARGE ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_LARGE  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE)
						{
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
						}
						// <big><big>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_SMALL   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_MEDIUM  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_SMALL    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_LARGE   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_LARGE ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_LARGE    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE)
						{
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
						}
						// <big><big><big>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_MEDIUM  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_LARGE   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_SMALL    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_LARGE ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE)
						{
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
						}
						// <big><big><big><big>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_LARGE   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_LARGE ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_SMALL    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE)
						{
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
						}
						// <big><big><big><big><big>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_LARGE ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE)
						{
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
						}
						// <big><big><big><big><big><big>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE)
						{
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
							text.Add ('<');text.Add ('b');text.Add ('i');text.Add ('g');text.Add ('>');
						}
						
						// <small>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_SMALL    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_SMALL    ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_LARGE    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_LARGE  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_LARGE    ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_LARGE)
						{
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
						}
						// <small><small>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_SMALL    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_LARGE    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_SMALL    ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_LARGE  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_LARGE)
						{
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
						}
						// <small><small><small>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_MEDIUM   && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_LARGE    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_LARGE  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_SMALL    ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_MEDIUM)
						{
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
						}
						// <small><small><small><small>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_LARGE    && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_LARGE  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_SMALL  ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_SMALL)
						{
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
						}
						// <small><small><small><small><small>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_X_LARGE  && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL ||
						    styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_X_SMALL)
						{
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
						}
						// <small><small><small><small><small><small>
						if (styleList[line[index - 1].StyleIndex].FontData.GetSize() == SIZE_XX_LARGE && styleList[line[index].StyleIndex].FontData.GetSize() == SIZE_XX_SMALL)
						{
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
							text.Add ('<');text.Add ('s');text.Add ('m');text.Add ('a');text.Add ('l');text.Add ('l');text.Add ('>');
						}
						
						// <span>
						if (styleList[line[index - 1].StyleIndex].ForeColor != styleList[line[index].StyleIndex].ForeColor)
						{
							bool cngForeground = false;
							bool newBackground = false;
							bool cngBackground = false;
							bool clnBackground = false;
							
							if (styleList[line[index - 1].StyleIndex].ForeColor != styleList[line[index].StyleIndex].ForeColor)
								cngForeground = true;

							if (styleList[line[index - 1].StyleIndex].BackColor   != styleList[line[index].StyleIndex].BackColor)
								cngBackground = true;
							
							if (cngForeground || cngBackground || clnBackground)
							{
								if (stackSpan > 0)
								{
									text.Add ('<');text.Add ('/');text.Add ('s');text.Add ('p');text.Add ('a');text.Add ('n');text.Add ('>');
									stackSpan--;
								}
							}
							if (cngForeground || newBackground || cngBackground)
							{
								string span = "";
								if (cngForeground && !newBackground && !cngBackground)
									span = "<span foreground=\"" + surface.HtmlNameForColor (styleList[line[index].StyleIndex].ForeColor) + "\">";
								if (!cngForeground && (newBackground || cngBackground))
									span = "<span background=\"" + surface.HtmlNameForColor (styleList[line[index].StyleIndex].BackColor) + "\">";
								if (cngForeground && (newBackground || cngBackground))
									span = "<span foreground=\"" + surface.HtmlNameForColor (styleList[line[index].StyleIndex].ForeColor) +
										   "\" background=\"" + surface.HtmlNameForColor (styleList[line[index].StyleIndex].BackColor) + "\">";
								text.AddRange (span.ToCharArray ());
								stackSpan++;
							}
						}
					}
				}
				text.Add (line[index].C);
			}
			
			while (stackSpan > 0)
			{	text.Add ('<');text.Add ('/');text.Add ('s');text.Add ('p');text.Add ('a');text.Add ('n');text.Add ('>');
				stackSpan--;
			}
			while (stackItalic > 0)
			{	text.Add ('<');text.Add ('/');text.Add ('i');text.Add ('>');
				stackItalic--;
			}
			while (stackBold > 0)
			{	text.Add ('<');text.Add ('/');text.Add ('b');text.Add ('>');
				stackBold--;
			}			
			while (stackUnderline > 0)
			{	text.Add ('<');text.Add ('/');text.Add ('u');text.Add ('>');
				stackUnderline--;
			}
			while (stackStrikeout > 0)
			{	text.Add ('<');text.Add ('/');text.Add ('s');text.Add ('>');
				stackStrikeout--;
			}
			return new string (text.ToArray ());
		}
		
		/// <summary>Get the text  of indicated line as 2 byte array for drawing with X11lib.XDrawString16().</summary>
		/// <param name="lineIndex">The index of the line, to get the text as 2 byte array for.<see cref="System.Int32"/></param>
		/// <returns>The text as 2 byte array for drawing with X11lib.XDrawString16().<see cref="X11lib.XChar2b[]"/></returns>
		public X11lib.XChar2b[] Text16 (int lineIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return new X11lib.XChar2b[0];
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0)
				return new X11lib.XChar2b[0];
			
			X11lib.XChar2b[] text16 = new X11lib.XChar2b[line.Length];
			for (int index = 0; index < line.Length; index++)
				text16[index] = line[index].C16;
			
			return text16;
		}
		
		/// <summary>Get the text  of indicated line as 4 byte array for drawing with X11lib.XwcDrawString().</summary>
		/// <param name="lineIndex">The index of the line, to get the text as 4 byte array for.<see cref="System.Int32"/></param>
		/// <returns>The text as 4 byte array for drawing with X11lib.XwcDrawString().<see cref="X11lib.XChar2b[]"/></returns>
		public X11.TWchar[] Text32 (int lineIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return new X11.TWchar[0];
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0)
				return new X11.TWchar[0];
			
			X11.TWchar[] text32 = new X11.TWchar[line.Length];
			for (int index = 0; index < line.Length; index++)
				text32[index] = line[index].C32;

			return text32;
		}
		
		/// <summary>Get the text  of indicated line, start index and length as C# string (2 byte (0...65.536) character array).</summary>
		/// <param name="lineIndex">The index of the line, to get the sub text as C# string for.<see cref="System.Int32"/></param>
		/// <param name="startCharIndex">The index to start the part to copy.<see cref="System.Int32"/></param>
		/// <param name="length">The length of the part to copy.<see cref="System.Int32"/></param>
		/// <returns>The text as C# string (2 byte (0...65.536) character array).<see cref="X11lib.XChar2b[]"/></returns>
		public string SubText (int lineIndex, int startCharIndex, int length)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return string.Empty;
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || startCharIndex >= line.Length)
				return string.Empty;
			
			if (length > line.Length - startCharIndex)
				length = line.Length - startCharIndex;
			
			if (length == 0)
				return string.Empty;
				
			char[] text = new char[length];
			for (int index = 0; index < length; index++)
				text[index] = line[startCharIndex + index].C;
			
			return new string (text);
		}
		
		/// <summary>Get the text  of indicated line, start index and length as 2 byte array for drawing with X11lib.XDrawString16().</summary>
		/// <param name="lineIndex">The index of the line, to get the sub text as 2 byte array for.<see cref="System.Int32"/></param>
		/// <param name="start">The index to start the part to copy.<see cref="System.Int32"/></param>
		/// <param name="length">The length of the part to copy.<see cref="System.Int32"/></param>
		/// <returns>The text as 2 byte array for drawing with X11lib.XDrawString16().<see cref="X11lib.XChar2b[]"/></returns>
		public X11lib.XChar2b[] SubText16 (int lineIndex, int startCharIndex, int length)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return new X11lib.XChar2b[0];
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || startCharIndex >= line.Length)
				return new X11lib.XChar2b[0];
			
			if (length > line.Length - startCharIndex)
				length = line.Length - startCharIndex;
			
			if (length == 0)
				return new X11lib.XChar2b[0];
			
			X11lib.XChar2b[] text16 = new X11lib.XChar2b[length];
			for (int index = 0; index < length; index++)
				text16[index] = line[startCharIndex + index].C16;
			
			return text16;
		}
		
		/// <summary>Get the text  of indicated line, start index and length as 4 byte array for drawing with X11lib.XwcDrawString().</summary>
		/// <param name="lineIndex">The index of the line, to get the sub text as 4 byte array for.<see cref="System.Int32"/></param>
		/// <param name="start">The index to start the part to copy.<see cref="System.Int32"/></param>
		/// <param name="length">The length of the part to copy.<see cref="System.Int32"/></param>
		/// <returns>The text as 4 byte array for drawing with X11lib.XwcDrawString().<see cref="X11lib.XChar2b[]"/></returns>
		public X11.TWchar[] SubText32 (int lineIndex, int startCharIndex, int length)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return new X11.TWchar[0];
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || startCharIndex >= line.Length)
				return new X11.TWchar[0];
			
			if (length > line.Length - startCharIndex)
				length = line.Length - startCharIndex;
			
			if (length == 0)
				return new X11.TWchar[0];
			
			X11.TWchar[] text32 = new X11.TWchar[length];
			for (int index = 0; index < length; index++)
				text32[index] = line[startCharIndex + index].C32;

			return text32;
		}
		
		/// <summary>Partial copy constructor.</summary>
		/// <param name="lineIndex">The index of the line, to get the the sub text data for.<see cref="System.Int32"/></param>
		/// <param name="start">The index to start the part to copy.<see cref="System.Int32"/></param>
		/// <param name="length">The length of the part to copy.<see cref="System.Int32"/></param>
		/// <returns>The partial text data.<see cref="X11.StyleText"/></returns>
		public StyleText SubStyleText (int lineIndex, int startCharIndex, int length)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return new StyleText ("", null, null, false);
				
			TStyleChar[] line = _text[lineIndex];
			if (line == null || line.Length == 0 || startCharIndex >= line.Length)
				return new StyleText ("", null, null, false);
			else
			{
				StyleText result     = new StyleText ("", null, null, false);
				int      realLength = Math.Min (line.Length - startCharIndex, length);
				
				if (realLength < 0)
					realLength = line.Length - startCharIndex;
				if (result._text.Count == 0)
					result._text.Add (new TStyleChar[realLength]);
				else
					result._text[0] = new TStyleChar[realLength];
				
				for (int index = 0; index < realLength; index++)
				{
					result._text[0][index] = new TStyleChar (line[startCharIndex + index].C32, line[startCharIndex + index].StyleIndex);
				}
				return result;
			}
		}
	
		#endregion Text and subtext access methods
	
		#region Methods
		
		/// <summary>Get the number of style characters for indicated line.</summary>
		/// <param name="lineIndex">The line to get the number of style characters for.<see cref="System.Int32"/></param>
		/// <returns>The number of style characters for indicated line on suffess, or 0 otherwise.<see cref="System.Int32"/></returns>
		public int LineCharCount (int lineIndex)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return 0;
			
			return _text[lineIndex].Length;
		}
		
		/// <summary>Copy constructor for the complete (multiline) text.</summary>
		public StyleText Clone()
		{
			if (this.LineCount == 0)
				return new StyleText ("", null, null, false);
			else
			{
				StyleText result = new StyleText ("", null, null, false);
				for (int lineIndex = 0; lineIndex < this.LineCount; lineIndex++)
				{
					if (lineIndex == 0)
						result._text[0] = new TStyleChar[this[0].Length];
					else
						result._text.Add (new TStyleChar[_text[lineIndex].Length]);
				
					for (int charIndex = 0; charIndex < _text[lineIndex].Length; charIndex++)
					{
						result._text[lineIndex][charIndex] = new TStyleChar (_text[lineIndex][charIndex].C32, _text[lineIndex][charIndex].StyleIndex);
					}
				}
				return result;
			}
		}
		
		/// <summary>Append a string with current style.</summary>
		/// <param name="lineIndex">The index of the line, to append text to.<see cref="System.Int32"/></param>
		/// <param name="text">The string to append.<see cref="System.String"/></param>
		public void Append (int lineIndex, string text)
		{
			if (string.IsNullOrEmpty (text))
				return;
			
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return;
				
			TStyleChar[] line = _text[lineIndex];
			
			int         currentStyleIndex = 0;
			TStyleChar[] newStyleChars     = new TStyleChar[line.Length + text.Length];

			for (int index = 0; index < line.Length; index++)
			{
				newStyleChars[index] = new TStyleChar (line[index].C32, line[index].StyleIndex);
				currentStyleIndex = line[index].StyleIndex;
			}
			
			int offset = line.Length;
			for (int index = 0; index < text.Length; index++)
			{
				newStyleChars[offset + index] = new TStyleChar (text[index], currentStyleIndex);
			}
			
			this._text[lineIndex] = newStyleChars;
		}
		
		/// <summary>Append a line of style characters at the ond of currently registered lines.</summary>
		/// <param name="lineCharacters">The line of style characters to append.<see cref="StyleChar[]"/></param>
		public void AppendLine (TStyleChar[] lineCharacters)
		{
			_text.Add (lineCharacters);
		}
		
		/// <summary>Set the indicated line of style characters.</summary>
		/// <param name="lineIndex">The index of the line, to set.<see cref="System.Int32"/></param>
		/// <param name="lineCharacters">The line of style characters to set.<see cref="StyleChar[]"/></param>
		public void SetLine (int lineIndex, TStyleChar[] lineCharacters)
		{
			if (lineIndex < 0 || lineIndex >= _text.Count)
				return;
				
			_text[lineIndex] = lineCharacters;
		}
		
		#endregion

	}
}