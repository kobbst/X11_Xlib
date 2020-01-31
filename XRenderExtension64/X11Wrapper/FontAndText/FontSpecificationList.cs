// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: October 2014
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 * In case of problems with .NEt see: .NET Reference Source, http://referencesource-beta.microsoft.com/
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

namespace X11
{
	/// <summary>The list of "bitmap and scalable" fonts, structured by FontFoundry, FontFamily, FontCharacterSet, ...</summary>
	public class FontSpecificationList : IComparer<FontSpecificationList.FontFoundry>
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const String CLASS_NAME = "FontSpecificationList";
		
        #endregion
		
        // ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

        #region Attributes	
		
		/// <summary> The list of available font foundries. </summary>
		public List<FontSpecificationList.FontFoundry> Foundries = new List<FontSpecificationList.FontFoundry> ();			
		
		/// <summary> The list of available full qualified font names. </summary>
		private List<string>				_qualifiedFontNames  = new List<string> ();
		
		/// <summary>Sort fonts with this charset to the top.</summary>
		private string _preferredCharSet	= "";
		
        #endregion
		
        // ###############################################################################
        // ### I N N E R   C L A S S E S
        // ###############################################################################

        #region Inner classes	
		
		/// <summary> The font pixel tenthOfPoint spacing tenthOfAveragePixelWidth. </summary>
		public class FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth
		{
		
			/// <summary> Name constructor. </summary>
			/// <param name="pixel"> The value of the font pixel. A string represented number. <see cref="System.String"/> </param>
			/// <param name="tenthOfPoints"> The value of the font tenthOfPoints. A string represented number. <see cref="System.String"/> </param>
			/// <param name="spacing"> The value of the font spacing. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="tenthOfAveragePixelWidth"> The value of the font tenthOfAveragePixelWidth. A string represented number. <see cref="System.String"/> </param>
			public static string ToName (string pixel, string tenthOfPoints, string spacing, string tenthOfAveragePixelWidth)
			{
				return pixel + "-" + tenthOfPoints + "-" + spacing + "-" + tenthOfAveragePixelWidth;
			}
				
			/// <summary> The font pixel name. </summary>
			private string _pixel;
			
			/// <summary> The font tenthOfPoints name. </summary>
			private string _tenthOfPoints;
			
			/// <summary> The font spacing name. </summary>
			private string _spacing;
			
			/// <summary> The font tenthOfAveragePixelWidth string. </summary>
			private string _tenthOfAveragePixelWidth;
			
			/// <summary> The font pixel tenthOfPoint spacing tenthOfAveragePixelWidth name. </summary>
			private string _name;
							
			/// <summary> The font pixel tenthOfPoint spacing tenthOfAveragePixelWidth sortable name. </summary>
			private string _sortName;
							
			/// <summary> Initializing constructor. </summary>
			/// <param name="pixel"> The value of the font pixel. A string represented number. <see cref="System.String"/> </param>
			/// <param name="tenthOfPoints"> The value of the font tenthOfPoints. A string represented number. <see cref="System.String"/> </param>
			/// <param name="spacing"> The value of the font spacing. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="tenthOfAveragePixelWidth"> The value of the font tenthOfAveragePixelWidth. A string represented number. <see cref="System.String"/> </param>
			public FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth (string pixel, string tenthOfPoints, string spacing, string tenthOfAveragePixelWidth)
			{
				_pixel = pixel;
				_tenthOfPoints = tenthOfPoints;
				_spacing = spacing;
				_tenthOfAveragePixelWidth = tenthOfAveragePixelWidth;

				_name = ToName (pixel, tenthOfPoints, spacing, tenthOfAveragePixelWidth);
				
				string val = "000" + pixel;
				_sortName = val.Substring (Math.Max (val.Length - 3, pixel.Length - 3));
				val = "000" + tenthOfPoints; 
				_sortName += "-" + val.Substring (Math.Max (val.Length - 3, tenthOfPoints.Length - 3));
				_sortName += "-" + spacing;
				val = "000" + tenthOfAveragePixelWidth; 
				_sortName += "-" + val.Substring (Math.Max (val.Length - 3, tenthOfAveragePixelWidth.Length - 3));
			}
			
			/// <summary> Get the font pixel name. </summary>
			public string Pixel
			{	get	{	return _pixel;	}	}
			
			/// <summary> Get the font tenthOfPoints name. </summary>
			public string TenthOfPoints
			{	get	{	return _tenthOfPoints;	}	}
			
			/// <summary> Get the font spacing value. </summary>
			public string Spacing
			{	get	{	return _spacing;	}	}
			
			/// <summary> The font tenthOfAveragePixelWidth value. </summary>
			public string TenthOfAveragePixelWidth
			{	get	{	return _tenthOfAveragePixelWidth;	}	}
			
			/// <summary> Get the font pixel tenthOfPoint spacing tenthOfAveragePixelWidth name. </summary>
			public string Name
			{	get	{	return _name;	}	}
			
			/// <summary> Get the font pixel tenthOfPoint spacing tenthOfAveragePixelWidth sortable name. </summary>
			public string SortName
			{	get	{	return _sortName;	}	}
		}

		/// <summary> The font vertical-resolution-DPI. </summary>
		public class FontHorzResolutionDpiVertResolutionDpi : IComparer<FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth>
		{
		
			/// <summary> Name constructor. </summary>
			/// <param name="horzResolutionDpi"> The value of the horizontal-resolution-DPI. A string represented number. <see cref="System.String"/> </param>
			/// <param name="vertResolutionDpi"> The value of the vertical-resolution-DPI. A string represented number. <see cref="System.String"/> </param>
			public static string ToName (string horzResolutionDpi, string vertResolutionDpi)
			{
				return horzResolutionDpi + "-" + vertResolutionDpi;
			}
			
			/// <summary> The horizontal-resolution-DPI name. </summary>
			private string _horzResolutionDpi;

			/// <summary> The vertical-resolution-DPI name. </summary>
			private string _vertResolutionDpi;
			
			/// <summary> The font vertical-resolution-DPI horizontal-resolution-DPI name. </summary>
			private string _name;
			
			/// <summary> The font vertical-resolution-DPI horizontal-resolution-DPI sortable name. </summary>
			private string _sortName;
			
			/// <summary> The font vertical-resolution-DPI associated font pixels. </summary>
			public List<FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth> Sizes = new List<FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth> ();
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="horzResolutionDpi"> The value of the horizontal-resolution-DPI. A string represented number. <see cref="System.String"/> </param>
			/// <param name="vertResolutionDpi"> The value of the vertical-resolution-DPI. A string represented number. <see cref="System.String"/> </param>
			public FontHorzResolutionDpiVertResolutionDpi (string horzResolutionDpi, string vertResolutionDpi)
			{
				_horzResolutionDpi = horzResolutionDpi;
				_vertResolutionDpi = vertResolutionDpi;

				_name = ToName (horzResolutionDpi, vertResolutionDpi);
				
				string val = "000" + horzResolutionDpi;
				_sortName = val.Substring (Math.Max (val.Length - 3, horzResolutionDpi.Length - 3));
				val = "000" + vertResolutionDpi; 
				_sortName += "-" + val.Substring (Math.Max (val.Length - 3, vertResolutionDpi.Length - 3));
			}
			
			/// <summary> Get the horizontal-resolution-DPI name. </summary>
			public string HorzResolutionDpi
			{	get	{	return _horzResolutionDpi;	}	}

			/// <summary> Get the vertical-resolution-DPI name. </summary>
			public string VertResolutionDpi
			{	get	{	return _vertResolutionDpi;	}	}
			
			/// <summary> Get the font vertical-resolution-DPI horizontal-resolution-DPI name. </summary>
			public string Name
			{	get	{	return _name;	}	}
			
			/// <summary> Get the font vertical-resolution-DPI horizontal-resolution-DPI sortable name. </summary>
			public string SortName
			{	get	{	return _sortName;	}	}
			
			/// <summary> Get the associated font pixels. </summary>
			/// <param name="name"> The name of the requested font pixels. <see cref="System.String"/> </param>
			/// <returns> The font pixels on success, or null otherwise. <see cref="FontPixel"/> </returns>
			public FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth GetPixelTenthOfPointSpacingTenthOfAveragePixelWidth (string name)
			{
				foreach (FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth size in Sizes)
				{
					if (size.Name == name)
						return size;
				}
				return null;
			}
		
			/// <summary> Compare two font pixel for sort. </summary>
			/// <param name="a"> The first font pixel to compare. <see cref="FontSpecification.FontPixel"/> </param>
			/// <param name="b"> The second font pixel to compare. <see cref="FontSpecification.FontPixel"/> </param>
			/// <returns> -1 on a < b, 0 on a = b or 1 on a > b. <see cref="System.Int32"/> </returns>
			public int Compare (FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth a, FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth b)
			{
				return a.SortName.CompareTo (b.SortName);
			}
		}
		
		/// <summary> The font weight. </summary>
		public class FontWeightSlantWidthFlag : IComparer<FontHorzResolutionDpiVertResolutionDpi>
		{
		
			/// <summary> Name constructor. </summary>
			/// <param name="weight"> The name of the font weight. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="slant"> The name of the font slant. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="width"> The name of the font width. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="additionalStyle"> The name of the font additional style. NOT a string represented number. <see cref="System.String"/> </param>
			public static string ToName (string weight, string slant, string width, string additionalStyle)
			{
				return weight + "-" + slant + "-" + width + "-" + additionalStyle;
			}
			
			/// <summary> The font weight name. </summary>
			private string _weight;
			
			/// <summary> The font slant slant. </summary>
			private string _slant;
			
			/// <summary> The font width width. </summary>
			private string _width;
			
			/// <summary> The font additional style name. </summary>
			private string _additionalStyle;
			
			/// <summary> The font weight slant width flag name. </summary>
			private string _name;
			
			/// <summary> The font weight slant width associated font horizontal-resolution-DPIs vertical-resolution-DPIs. </summary>
			public List<FontHorzResolutionDpiVertResolutionDpi> ResolutionDpis = new List<FontHorzResolutionDpiVertResolutionDpi> ();
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="weight"> The name of the font weight. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="slant"> The name of the font slant. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="width"> The name of the font width. NOT a string represented number. <see cref="System.String"/> </param>
			/// <param name="additionalStyle"> The name of the font additional style. NOT a string represented number. <see cref="System.String"/> </param>
			public FontWeightSlantWidthFlag (string weight, string slant, string width, string additionalStyle)
			{
				_weight = weight;
				_slant  = slant;
				_width  = width;
				_additionalStyle   = additionalStyle;
				
				_name = ToName (weight, slant, width, additionalStyle);
			}
			
			/// <summary> Get the font weight name. </summary>
			public string Weight
			{	get	{	return _weight;	}	}
			
			/// <summary> Get the font weight slant. </summary>
			public string Slant
			{	get	{	return _slant;	}	}
			
			/// <summary> Get the font weight width. </summary>
			public string Width
			{	get	{	return _width;	}	}
			
			/// <summary> The font additional style name. </summary>
			public string AdditionalStyle
			{	get	{	return _additionalStyle;	}	}
			
			/// <summary> Get the font weight slant width name. </summary>
			public string Name
			{	get	{	return _name;	}	}
			
			/// <summary> Get the associated font pixels. </summary>
			/// <param name="name"> The name of the requested font pixels. <see cref="System.String"/> </param>
			/// <returns> The font pixels on success, or null otherwise. <see cref="FontPixel"/> </returns>
			public FontHorzResolutionDpiVertResolutionDpi GetHorzResolutionDpiVertResolutionDpi (string name)
			{
				foreach (FontHorzResolutionDpiVertResolutionDpi resolutionDPI in ResolutionDpis)
				{
					if (resolutionDPI.Name == name)
						return resolutionDPI;
				}
				return null;
			}
		
			/// <summary> Compare two font horizontal-resolution-DPI vertical-resolution-DPI for sort. </summary>
			/// <param name="a"> The first font horizontal-resolution-DPI vertical-resolution-DPI compare. <see cref="FontSpecification.FontHorzResolutionDpiVertResolutionDpi"/> </param>
			/// <param name="b"> The second font horizontal-resolution-DPI vertical-resolution-DPI to compare. <see cref="FontSpecification.FontHorzResolutionDpiVertResolutionDpi"/> </param>
			/// <returns> -1 on a < b, 0 on a = b or 1 on a > b. <see cref="System.Int32"/> </returns>
			public int Compare (FontHorzResolutionDpiVertResolutionDpi a, FontHorzResolutionDpiVertResolutionDpi b)
			{
				return a.SortName.CompareTo (b.SortName);
			}
		}
		
		/// <summary> The font character set. </summary>
		public class FontCharacterSet : IComparer<FontWeightSlantWidthFlag>
		{
			/// <summary> The font character set name. </summary>
			public string Name;
			
			/// <summary> The font family value. </summary>
			public string Value;
			
			/// <summary> The font character set associated font wieghts. </summary>
			public List<FontWeightSlantWidthFlag> WeightSlantWidths = new List<FontWeightSlantWidthFlag> ();
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="charSet"> The name of the font character set. <see cref="System.String"/> </param>
			/// <param name="variant"> The name variant of the font character set. <see cref="System.String"/> </param>
			public FontCharacterSet (string charSet, string variant)
			{
				Name = charSet + "-" + variant;
				string val = "000" + variant;
				Value = charSet + "-" + val.Substring (Math.Max (val.Length - 3, variant.Length - 3));
			}
			
			/// <summary> Get the associated font weight slant width. </summary>
			/// <param name="name"> The name of the requested font weight slant width flag. <see cref="System.String"/> </param>
			/// <returns> The font weight slant width on success, or null otherwise. <see cref="FontWeight"/> </returns>
			public FontWeightSlantWidthFlag GetWeightSlantWidthFlag (string name)
			{
				foreach (FontWeightSlantWidthFlag fw in WeightSlantWidths)
				{
					if (fw.Name == name)
						return fw;
				}
				return null;
			}
		
			/// <summary>Compare two font weight slant width flag for sort.</summary>
			/// <param name="a">The first font weight slant width flag to compare.<see cref="FontSpecification.FontWeight"/></param>
			/// <param name="b">The second font weight slant width flag to compare.<see cref="FontSpecification.FontWeight"/></param>
			/// <returns>-1 on a < b, 0 on a = b or 1 on a > b.<see cref="System.Int32"/></returns>
			public int Compare (FontWeightSlantWidthFlag a, FontWeightSlantWidthFlag b)
			{
				return a.Name.CompareTo (b.Name);
			}
		}
		
		/// <summary> The font family. </summary>
		public class FontFamily : IComparer<FontCharacterSet>
		{
			/// <summary> The font family name. </summary>
			public string Name;
		
			/// <summary>Sort fonts with this charset to the top.</summary>
			private string _preferredCharSet;
			
			/// <summary> The font family associated font character sets. </summary>
			public List<FontCharacterSet> CharacterSets = new List<FontCharacterSet> ();
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="name"> The name of the font family. <see cref="System.String"/> </param>
			/// <param name="preferredCharSet"> The preferred charset or empty string for no preference.
			/// Sort fonts with this charset to the top. <see cref="System.String"/> </param>
			public FontFamily (string name, string preferredCharSet)
			{
				Name = name;
				_preferredCharSet = preferredCharSet;
			}
			
			/// <summary> The font foundry name wit a capital start letter. </summary>
			public string DisplayName
			{	
				get {	return X11Utils.CapitalStartLetter (Name, true);	}
			}
			
			/// <summary> Get the associated font character set. </summary>
			/// <param name="name"> The name of the requested font character set. <see cref="System.String"/> </param>
			/// <param name="variant"> The name variant of the requested font character set. <see cref="System.String"/> </param>
			/// <returns> The font character set on success, or null otherwise. <see cref="FontCharacterSet"/> </returns>
			public FontCharacterSet GetCharacterSet (string name, string variant)
			{
				string find = name + "-" + variant;
				
				foreach (FontCharacterSet fc in CharacterSets)
				{
					if (fc.Name == find)
						return fc;
				}
				return null;
			}
		
			/// <summary> Compare two font character set for sort. </summary>
			/// <param name="a"> The first font character set to compare. <see cref="FontSpecification.FontCharacterSet"/> </param>
			/// <param name="b"> The second font character set to compare. <see cref="FontSpecification.FontCharacterSet"/> </param>
			/// <returns> -1 on a < b, 0 on a = b or 1 on a > b. <see cref="System.Int32"/> </returns>
			public int Compare (FontCharacterSet a, FontCharacterSet b)
			{
				if (a == null && b == null)
					return 0;
				if (a == null)
					return -1;
				if (b == null)
					return 1;
				
				if (!string.IsNullOrEmpty(_preferredCharSet)  &&
				    a.Value.StartsWith (_preferredCharSet) &&
				    !b.Value.StartsWith (_preferredCharSet)   )
					return -1;
				if (!string.IsNullOrEmpty(_preferredCharSet)  &&
				    b.Value.StartsWith (_preferredCharSet) &&
				    !a.Value.StartsWith (_preferredCharSet)   )
					return 1;
				
				return a.Value.CompareTo (b.Value);
			}
		}
		
		/// <summary> The font foundry. </summary>
		public class FontFoundry : IComparer<FontFamily>
		{
			/// <summary> The font foundry name. </summary>
			public string Name;
			
			/// <summary> The font foundry associated font families. </summary>
			public List<FontFamily> Families = new List<FontFamily> ();
			
			/// <summary> Initializing constructor. </summary>
			/// <param name="name"> The name of the font foundry. <see cref="System.String"/> </param>
			public FontFoundry (string name)
			{
				Name = name;
			}
			
			/// <summary> The font family name wit a capital start letter. </summary>
			public string DisplayName
			{	
				get {	return X11Utils.CapitalStartLetter (Name, true);	}
			}
			
			/// <summary> Get the associated font family. </summary>
			/// <param name="name"> The name of the requested font family. <see cref="System.String"/> </param>
			/// <returns> The font family on success, or null otherwise. <see cref="FontFamily"/> </returns>
			public FontFamily GetFamily (string name)
			{
				foreach (FontFamily ff in Families)
				{
					if (ff.Name == name || ff.DisplayName == name)
						return ff;
				}
				return null;
			}
		
			/// <summary> Compare two font family for sort. </summary>
			/// <param name="a"> The first font family to compare. <see cref="FontSpecification.FontFamily"/> </param>
			/// <param name="b"> The second font family to compare. <see cref="FontSpecification.FontFamily"/> </param>
			/// <returns> -1 on a < b, 0 on a = b or 1 on a > b. <see cref="System.Int32"/> </returns>
			public int Compare (FontFamily a, FontFamily b)
			{
				return a.Name.CompareTo (b.Name);
			}
		}
		
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display to initialize the font specifications for.<see cref="IntPtr"/></param>
		/// <param name="preferredCharSet">The preferred charset or empty string for no preference.
		/// Sort fonts with this charset to the top. <see cref="System.String"/> </param>
		public FontSpecificationList (IntPtr display, string preferredCharSet)
		{
			_preferredCharSet = preferredCharSet;
			
			TInt numFonts = 0;
			string fontFilter   = "-*-*-*-*-*-*-*-*-*-*-*-*-*";
			//string fontFilter = "-*-*-*-*-*-*-0-0-0-0-*-*-*";
			//                   -1-2-3-4-5-6-7-8-9-A-B-C-D
			//                    1 = Foundry
			//                    2 = Family
			//                    3 = Weight
			//                    4 = Slant
			//                    5 = Width
			//                    6 = Extra
			//                    7 = Pixel
			//                    8 = TenthOfPoint
			//                    9 = Horz resolution
			//                    A = Vert resolution
			//                    B = Spacing
			//                    C = TenthOfAveragePixelWidth
			//                    D = Characterset
			IntPtr fontNameArray = X11lib.XListFonts (display, fontFilter, (TInt)20000, ref numFonts);
			for (int countFont = 0; countFont < (int)numFonts; countFont++)
			{
				IntPtr fontNameDeref = System.Runtime.InteropServices.Marshal.ReadIntPtr (fontNameArray, IntPtr.Size * countFont);
				string fontName      = System.Runtime.InteropServices.Marshal.PtrToStringAnsi (fontNameDeref);
				if (!string.IsNullOrEmpty (fontName))
				{
					_qualifiedFontNames.Add (fontName);
					this.AddFontSpecification (fontName);
					
					// This takes a lot of time!!!
					/*
					IntPtr fontResourceId = X11lib.XLoadFont (display, fontName);
					if (fontResourceId != IntPtr.Zero)
					{
						IntPtr fontStruct = X11lib.XQueryFont (display, fontResourceId);
						if (fontStruct != IntPtr.Zero)
						{
							string name = "";
							TUlong val = (TUlong)0;
							TBoolean res = X11lib.XGetFontProperty (fontStruct, X11atoms.XA_FAMILY_NAME, ref val);
							if (res == (TBoolean)1)
							{
								IntPtr nameRef = X11lib.XGetAtomName (display, (IntPtr)val);
								if (nameRef != IntPtr.Zero)
								{
									name = System.Runtime.InteropServices.Marshal.PtrToStringAuto (nameRef);
									X11lib.XFree (nameRef);
								}
							}
							X11lib.XFreeFontInfo (IntPtr.Zero, fontStruct, (TInt)0);
						}
						//X11lib.XFreeFont (display, fontResourceId);
					}
					*/
					
				}
			}
			X11lib.XFreeFontNames (fontNameArray);
			fontNameArray = IntPtr.Zero;
			// http://coot.googlecode.com/svn/trunk/ccp4mg-utils/pygl/x11_font.cc
			
			_qualifiedFontNames.Sort ();
			this.Sort ();
		}

		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################
		
		#region Properties
		
		/// <summary>The list of available full qualified font names.</summary>
		public List<string>	QualifiedFontNames
		{	get	{	return _qualifiedFontNames;	}	}
		
        #endregion

        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

        #region Methods
		
		/// <summary> Get the indicated foundry. </summary>
		/// <param name="name"> The foundry name of the requested foundry. <see cref="System.String"/> </param>
		/// <returns> The foundry on success, or null otherwise. <see cref="FontSpecificationList.FontFoundry"/> </returns>
		public FontFoundry GetFoundry (string name)
		{
			foreach (FontFoundry ff in Foundries)
			{
				if (ff.Name == name || ff.DisplayName == name)
					return ff;
			}
			return null;
		}
		
		/// <summary> Add a new font specification. </summary>
		/// <param name="fontSpecification"> The font specification string. <see cref="System.String"/> </param>
		/// <remarks> Syntax is: "-foundry-family-weight-slant-width-additional style-pixels-tenths of points-horz resolution DPI-vert resolution DPI-spacing-tenths of average pixel width-charter set" </remarks>
		public void AddFontSpecification (string fontSpecification)
		{
			string[] spec = fontSpecification.Split ('-');
			if (!string.IsNullOrEmpty (spec[0]))
			{
				SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME + "::AddFontSpecification (" + fontSpecification + ") Font specification has leading characters '" + spec[0] + "' but shouldn't.");
			}
			
			if (spec.Length < 14)
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::AddFontSpecification (" + fontSpecification + ") Font specification contains only " + spec.Length + " parts but must contain 14!");
				return;
			}
			
			FontFoundry foundry = GetFoundry (spec[1]);
			if (foundry == null)
			{
				foundry = new FontFoundry (spec[1]);
				Foundries.Add (foundry);

			}

			FontFamily family = foundry.GetFamily (spec[2]);
			if (family == null)
			{
				family = new FontFamily (spec[2], _preferredCharSet);
				foundry.Families.Add (family);
			}
			
			FontCharacterSet characterSet = family.GetCharacterSet (spec[13], (spec.Length > 14 ? spec[14] : ""));
			if (characterSet == null)
			{
				characterSet = new FontCharacterSet (spec[13], (spec.Length > 14 ? spec[14] : ""));
				family.CharacterSets.Add (characterSet);
			}
			
			string styleName = FontWeightSlantWidthFlag.ToName (spec[3], spec[4], spec[5], spec[6]);
			FontWeightSlantWidthFlag weightSlantWidth = characterSet.GetWeightSlantWidthFlag (styleName);
			if (weightSlantWidth == null)
			{
				weightSlantWidth = new FontWeightSlantWidthFlag (spec[3], spec[4], spec[5], spec[6]);
				characterSet.WeightSlantWidths.Add (weightSlantWidth);
			}
			
			string resolutionName = FontHorzResolutionDpiVertResolutionDpi.ToName (spec[9], spec[10]);
			FontHorzResolutionDpiVertResolutionDpi resolution = weightSlantWidth.GetHorzResolutionDpiVertResolutionDpi (resolutionName);
			if (resolution == null)
			{
				resolution = new FontHorzResolutionDpiVertResolutionDpi (spec[9], spec[10]);
				weightSlantWidth.ResolutionDpis.Add (resolution);
			}
			
			string sizeName = FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth.ToName (spec[7], spec[8], spec[11], spec[12]);
			FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth size = resolution.GetPixelTenthOfPointSpacingTenthOfAveragePixelWidth (sizeName);
			if (size == null)
			{
				size = new FontPixelTenthOfPointSpacingTenthOfAveragePixelWidth (spec[7], spec[8], spec[11], spec[12]);
				resolution.Sizes.Add (size);
			}
		}
		
		/// <summary> Compare two font foundry for sort. </summary>
		/// <param name="a"> The first font foundry to compare. <see cref="FontSpecification.FontFoundry"/> </param>
		/// <param name="b"> The second font foundry to compare. <see cref="FontSpecification.FontFoundry"/> </param>
		/// <returns> -1 on a < b, 0 on a = b or 1 on a > b. <see cref="System.Int32"/> </returns>
		public int Compare (FontSpecificationList.FontFoundry a, FontSpecificationList.FontFoundry b)
		{
			return a.Name.CompareTo (b.Name);
		}
		
		/// <summary> Sort all elements on all levels. </summary>
		public void Sort ()
		{
			Foundries.Sort (this.Compare);
			foreach (FontFoundry foundry in Foundries)
			{
				foundry.Families.Sort (foundry.Compare);
				foreach (FontFamily family in foundry.Families)
				{
					family.CharacterSets.Sort (family.Compare);
					foreach (FontCharacterSet characterSet in family.CharacterSets)
					{
						characterSet.WeightSlantWidths.Sort (characterSet.Compare);
						foreach (FontWeightSlantWidthFlag weightSlantWidth in characterSet.WeightSlantWidths)
						{
							weightSlantWidth.ResolutionDpis.Sort (weightSlantWidth.Compare);
							foreach (FontHorzResolutionDpiVertResolutionDpi resolution in weightSlantWidth.ResolutionDpis)
							{
								resolution.Sizes.Sort (resolution.Compare);
							}
						}
					}
				}
			}
		}
		
        #endregion
	}
}