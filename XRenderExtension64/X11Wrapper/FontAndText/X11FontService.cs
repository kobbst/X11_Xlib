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
using System.Runtime.InteropServices;

namespace X11
{
	public static class X11FontService
	{
		/// <summary>The key, that identifies a font data uniquely.</summary>
		public struct FontDataKey
		{
			/// <summary>The font specification, that identifies a font/fontset.</summary>
			public string FontSpecification;
			
			/// <summary>The display pointer, that specifies the connection to the X server.</summary>
			public IntPtr X11Display;
			
			/// <summary>The flag defining whether to use a fontset or a single font.</summary>
			public bool   UseFontset;
			
			/// <summary>Create a new X11.X11FontService.FontDataKey instance.</summary>
			/// <param name="fontSpecification">The font specification, that identifies a font/fontset.<see cref="System.String"/></param>
			/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
			/// <param name="useFontset">The flag defining whether to use a fontset or a single font.<see cref="System.Boolean"/></param>
			public FontDataKey (string fontSpecification, IntPtr x11display, bool useFontset)
			{
				FontSpecification = fontSpecification;
				X11Display        = x11display;
				UseFontset        = useFontset;
			}
		}
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "X11FontData";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
	
		/// <summary>The list of currently loaded fonts.</summary>
		private static Dictionary<FontDataKey, X11FontData> _loadedFonts = new Dictionary<FontDataKey, X11FontData>();

		#endregion Attributes

        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary>Prepare a font or fonteset for utilization with Xrw.</summary>
		/// <param name="fontSpecification">The font specification, that identifies a font/fontset.<see cref="System.String"/></param>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="useFontset">The flag defining whether to use a fontset or a single font.<see cref="System.Boolean"/></param>
		/// <param name="fontData">The resulting font data on success, or null otherwise.<see cref="X11.X11FontData"/></param>
		/// <returns>True on success, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool PrepareFont (string fontSpecification, IntPtr x11display, bool useFontset, ref X11.X11FontData fontData)
		{
			fontData = null;
			
			foreach (KeyValuePair<FontDataKey, X11FontData> loadedFont in _loadedFonts)
			{
				if (loadedFont.Key.FontSpecification == fontSpecification && loadedFont.Key.X11Display == x11display && loadedFont.Key.UseFontset)
				{
					fontData = loadedFont.Value;
					return true;
				}
			}
			
			FontDataKey key = new FontDataKey (fontSpecification, x11display, useFontset); 
			if (useFontset)
			{
				IntPtr  missingCharsetList;
				TInt    missingCharsetCount;
				X11.XID fontsetResourceId = X11lib.XCreateFontSet (x11display, fontSpecification, out missingCharsetList,
				                                                   out missingCharsetCount, IntPtr.Zero);
				// Check whether directly matching fontset has been loaded, and - if not - load the most similar fontset (fuzzy).
				int fuzzyFactor = 0;
				string fuzzyFontSpecification = (fontsetResourceId == (X11.XID)0 ? fontSpecification : null);
				while (fontsetResourceId == (X11.XID)0 && fuzzyFactor < 3)
				{
					string lastFuzzyFontSpecification = fuzzyFontSpecification;
					if (fuzzyFactor == 0)
						fuzzyFontSpecification = X11FontData.ModifyFontSpecificationStretch (fuzzyFontSpecification, "*");
					if (fuzzyFactor == 1)
						fuzzyFontSpecification = X11FontData.ModifyFontSpecificationWieght  (fuzzyFontSpecification, "*");
					if (fuzzyFactor == 2)
						fuzzyFontSpecification = X11FontData.ModifyFontSpecificationSlant   (fuzzyFontSpecification, "*");
					
					fuzzyFactor++;
					// Safe time if no change has been made.
					if (lastFuzzyFontSpecification == fuzzyFontSpecification)
						continue;
					
					if (!string.IsNullOrEmpty(lastFuzzyFontSpecification) && lastFuzzyFontSpecification.Trim() != "")
					{
						fontsetResourceId = X11lib.XCreateFontSet (x11display, fuzzyFontSpecification, out missingCharsetList,
				                                                   out missingCharsetCount, IntPtr.Zero);
						if (fontsetResourceId != (X11.XID)0)
						{
							SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
							                   "::PrepareFont () Fuzzy load fontset with specification '" + fuzzyFontSpecification + "' " +
							                   "instead of '" + fontSpecification + "' succeeded.");
						}
					}
				}
				
				// Check whether directly matching or most similar fontset has been loaded, and - if not - load a fallback fontset.
				string extFontSpecification = null;
				if (fontsetResourceId == (X11.XID)0)
				{
					// Let the font server guess a fallback fontset.
					if (!string.IsNullOrEmpty(fontSpecification) && fontSpecification.Trim() != "" && !fontSpecification.Trim().EndsWith (",*"))
					{
						extFontSpecification = fontSpecification + ",*";

						SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME +
						                   "::PrepareFont () Can not load a fontset with specification '" + fontSpecification + "'.");
						SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
						                   "::PrepareFont () Retry to load a fontset with specification '" + extFontSpecification + "'.");
	
						fontsetResourceId = X11lib.XCreateFontSet (x11display, extFontSpecification, out missingCharsetList,
				                                                   out missingCharsetCount, IntPtr.Zero);
					}
					// The font specification already includs a joker to guess a fallback fontset.
					else
					{
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME +
						                   "::PrepareFont () Can not load a fontset with specification '" + fontSpecification + "'.");
						
		                // No success at all - even with a guess of a fallback fontset!
						return false;
					}
				}

				// Check whether matching fontset has been loaded.
				if (fontsetResourceId == (X11.XID)0)
				{
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME +
					                   "::PrepareFont () Can not load a fontset with specification '" + extFontSpecification + "'.");
					
	                // No success at all - even with a guess of a fallback fontset!
					return false;
				}
				
				if (!string.IsNullOrEmpty (extFontSpecification))
					SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
					                   "::PrepareFont () Successfully loaded best matching fontset for specification '" + fontSpecification + "' " +
					                   "using specification '" + extFontSpecification + "'.");
				else if (!string.IsNullOrEmpty (fuzzyFontSpecification))
					SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
					                   "::PrepareFont () Successfully loaded best matching fontset for specification '" + fontSpecification + "' " +
					                   "using specification '" + fuzzyFontSpecification + "'.");
				else
					SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
					                   "::PrepareFont () Successfully loaded best matching fontset for specification '" + fontSpecification + "'.");
				
				for (int countCharSet = 0; countCharSet < (int)missingCharsetCount; countCharSet++)
				{
					IntPtr p = Marshal.ReadIntPtr (missingCharsetList, countCharSet * Marshal.SizeOf(typeof(IntPtr)));
					string s = Marshal.PtrToStringAuto (p);
					if (!string.IsNullOrEmpty (extFontSpecification))
						SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME +
						                   "::PrepareFont () Fontset for specification '" + extFontSpecification + "' is missing font for charset  '" + s + "'.");
					else if (!string.IsNullOrEmpty (fuzzyFontSpecification))
						SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME +
						                   "::PrepareFont () Fontset for specification '" + fuzzyFontSpecification + "' is missing font for charset  '" + s + "'.");
					else
						SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME +
						                   "::PrepareFont () Fontset for specification '" + fontSpecification + "' is missing font for charset  '" + s + "'.");
				}
				
				// Calculate maximum font height, ascent and descent.
				int						ascent  = 0;
				int						descent = 0;
				X11lib.XFontSetExtents	extents = X11lib.XExtentsOfFontSet (fontsetResourceId);
				
				X11lib.XFontStruct[]	fontStructArray;
				string[]				fontNameArray;
				int						maxFonts;
				
				maxFonts = X11lib.XFontsOfFontSet (fontsetResourceId, out fontStructArray, out fontNameArray);
				for (int countFonts = 0; countFonts < maxFonts; countFonts++)
				{
					if (ascent  < (int)fontStructArray[countFonts].ascent)
						ascent  = (int)fontStructArray[countFonts].ascent;
					if (descent < (int)fontStructArray[countFonts].descent)
						descent = (int)fontStructArray[countFonts].descent;
				}
				
				string finalFontSpecification = null;
				if (!string.IsNullOrEmpty (extFontSpecification))
					finalFontSpecification = extFontSpecification;
				else if (!string.IsNullOrEmpty (fuzzyFontSpecification))
					finalFontSpecification = fuzzyFontSpecification;
				else
					finalFontSpecification = fontSpecification;
				
				// Maximum font height, ascent and descent might be frequently used for calculation.
				fontData = X11FontData.NewFontSetData (finalFontSpecification, x11display, fontsetResourceId,
				                                       (int)extents.max_logical_extent.height, ascent, descent);
				
				IntPtr gc = X11lib.XCreateGC (x11display, X11lib.XDefaultRootWindow (x11display), 0, IntPtr.Zero);
				if (gc != IntPtr.Zero)
				{
					fontData.SetTypicalCharWidth (AverageCharacterWidth(x11display, gc, fontData));
					X11lib.XFreeGC (x11display, gc);
				}
				_loadedFonts.Add (key, fontData);
				return true;
			}
			// Use font, if fontset isn't supported.
			else // of (useFontset)
			{
				// Load font and query font structure (to get maximum font height, ascent and descent).
				IntPtr fontStructure = X11lib.XLoadQueryFont (x11display, fontSpecification);
				
				// Check whether directly matching font has been loaded, and - if not - load the most similar font (fuzzy).
				int fuzzyFactor = 0;
				string fuzzyFontSpecification = (fontStructure == IntPtr.Zero ? fontSpecification : null);
				while (fontStructure == IntPtr.Zero && fuzzyFactor < 3)
				{
					string lastFuzzyFontSpecification = fuzzyFontSpecification;
					if (fuzzyFactor == 0)
						fuzzyFontSpecification = X11FontData.ModifyFontSpecificationStretch (fuzzyFontSpecification, "*");
					if (fuzzyFactor == 1)
						fuzzyFontSpecification = X11FontData.ModifyFontSpecificationWieght  (fuzzyFontSpecification, "*");
					if (fuzzyFactor == 2)
						fuzzyFontSpecification = X11FontData.ModifyFontSpecificationSlant   (fuzzyFontSpecification, "*");
					
					fuzzyFactor++;
					// Safe time if no change has been made.
					if (lastFuzzyFontSpecification == fuzzyFontSpecification)
						continue;
					
					if (!string.IsNullOrEmpty(lastFuzzyFontSpecification) && lastFuzzyFontSpecification.Trim() != "")
					{
						fontStructure = X11lib.XLoadQueryFont (x11display, lastFuzzyFontSpecification);
						if (fontStructure != IntPtr.Zero)
						{
							SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
							                   "::PrepareFont () Fuzzy load font with specification '" + fuzzyFontSpecification + "' " +
							                   "instead of '" + fontSpecification + "' succeeded.");
						}
					}
				}
				
				// Check whether directly matching or most similar font has been loaded, and - if not - load a fallback font.
				string extFontSpecification = null;
				if (fontStructure != IntPtr.Zero)
				{
					// Let the font server guess a fallback fontset.
					if (!string.IsNullOrEmpty(fontSpecification) && fontSpecification.Trim() != "" && !fontSpecification.Trim().EndsWith (",*"))
					{
						extFontSpecification = fontSpecification + ",*";

						SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME +
						                   "::PrepareFont () Can not load a fontset with specification '" + fontSpecification + "'.");
						SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
						                   "::PrepareFont () Retry to load a fontset with specification '" + extFontSpecification + "'.");
	
						fontStructure = X11lib.XLoadQueryFont (x11display, extFontSpecification);
					}
					// The font specification already includs a joker to guess a fallback fontset.
					else
					{
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME +
						                   "::PrepareFont () Can not load a font with specification '" + fontSpecification + "'.");
						
		                // No success at all - even with a guess of a fallback font!
						return false;
					}
				}
				
				// Check whether matching font has been loaded.
				if (fontStructure == IntPtr.Zero)
				{
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME +
					                   "::PrepareFont () Can not load a font with specification '" + fontSpecification + "'.");
				    // No success at all - even with a guess of a fallback font!
					return false;
				}
				
				if (!string.IsNullOrEmpty (extFontSpecification))
					SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
					                   "::PrepareFont () Successfully loaded best matching font for specification '" + fontSpecification + "' " +
					                   "using specification '" + extFontSpecification + "'.");
				else if (!string.IsNullOrEmpty (fuzzyFontSpecification))
					SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
					                   "::PrepareFont () Successfully loaded best matching font for specification '" + fontSpecification + "' " +
					                   "using specification '" + fuzzyFontSpecification + "'.");
				else
					SimpleLog.LogLine (TraceEventType.Information, CLASS_NAME +
					                   "::PrepareFont () Successfully loaded best matching font for specification '" + fontSpecification + "'.");
				
				X11lib.XFontStruct	fs  = (X11lib.XFontStruct)Marshal.PtrToStructure (fontStructure, typeof(X11lib.XFontStruct));

				string finalFontSpecification = null;
				if (!string.IsNullOrEmpty (extFontSpecification))
					finalFontSpecification = extFontSpecification;
				else if (!string.IsNullOrEmpty (fuzzyFontSpecification))
					finalFontSpecification = fuzzyFontSpecification;
				else
					finalFontSpecification = fontSpecification;
				
				// Maximum font height, ascent and descent might be frequently used for calculation.
				fontData = X11FontData.NewSingleFontData (finalFontSpecification, x11display, fs.fid,
				                                          (int)fs.ascent + (int)fs.descent, (int)fs.ascent, (int)fs.descent);
				
				IntPtr gc = X11lib.XCreateGC (x11display, X11lib.XDefaultRootWindow (x11display), 0, IntPtr.Zero);
				if (gc != IntPtr.Zero)
				{
					fontData.SetTypicalCharWidth (AverageCharacterWidth(x11display, gc, fontData));
					X11lib.XFreeGC (x11display, gc);
				}
				_loadedFonts.Add (key, fontData);
				return true;
			}
		}
		
		/// <summary>Calculate the average character width of indicated font data.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="IntPtr"/></param>
		/// <param name="x11gc">The crapchics context to use for drawing.<see cref="IntPtr"/></param>
		/// <param name="fontData">The font data to calculate the average character width for.<see cref="X11FontData"/></param>
		/// <returns>The average character width of indicated font data.<see cref="System.Int32"/></returns>
		public static int AverageCharacterWidth (IntPtr x11display, IntPtr x11gc, X11FontData fontData)
		{
			if (fontData == null)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::AverageCharacterWidth () Argument null: fontData");
				return 0;
			}
			

			if (fontData.UseFontset)
			{
				X11lib.XRectangle overallInc      = new X11lib.XRectangle();
				X11lib.XRectangle overallLogical  = new X11lib.XRectangle();
				
				X11.TWchar[] text = new X11.TWchar[] { (X11.TWchar)'X', (X11.TWchar)' ', (X11.TWchar)'i'};
				X11lib.XwcTextExtents (fontData.FontResourceId, text, (X11.TInt)text.Length, ref overallInc, ref overallLogical);
				
				return (int)(((int)overallLogical.width + 2) / 3);
			}
			else
			{
				TInt direction = 0;
				TInt fontAscent = 0;
				TInt fontDescent = 0;
				X11lib.XCharStruct xCharStruct = new X11lib.XCharStruct ();
				X11lib.XChar2b[] text = new X11lib.XChar2b[] { new X11lib.XChar2b ('X'), new X11lib.XChar2b (' '), new X11lib.XChar2b ('i')  };
				
				X11lib.XSetFont (x11display, x11gc, fontData.FontResourceId);
				X11lib.XQueryTextExtents16 (x11display, fontData.FontResourceId, text, (X11.TInt)text.Length, ref direction, ref fontAscent, ref fontDescent, ref xCharStruct);
				
				return (int)(((int)xCharStruct.width + 2) / 3);
			}

		}
		
		/// <summary>Free all loaded fonts and remove them from this service. </summary>
		public static void Clear ()
		{
			foreach (KeyValuePair<FontDataKey, X11FontData> loadedFont in _loadedFonts)
			{
				loadedFont.Value.Unload ();
			}
			_loadedFonts.Clear ();
		}
		
		#endregion Methods
	}
}