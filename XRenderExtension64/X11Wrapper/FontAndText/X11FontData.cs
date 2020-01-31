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
using System.Runtime.InteropServices;

namespace X11
{
	/// <summary>Provide consistent font data, irrespective whether internationalized text output is supported (via fontset) or not (via single font).</summary>
	public class X11FontData
	{
		
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
	
		/// <summary>Determine whether internationalized text output is enabled, using a fontset.</summary>
		private bool					_useFontset					= false;
		
		/// <summary>The current font/fontset specification. For applications that support fontsets a font name list, for applications that don't support fontsets a font name. Empty string for initially used default font/fontset.</summary>
		private string					_fontSpecification			= "";
		
		/// <summary>The X11 display pointer, this font data are assigned to.</summary>
		private IntPtr					_display					= IntPtr.Zero;
		
		/// <summary>The current font/fontset id. For applications that support fontsets a fontset id, for applications that don't support fontsets a font id. IntPtr.Zero for initially used default fontset.</summary>
		private X11.XID					_fontResourceId				= (X11.XID)0;
		
		/// <summary>The height of the font (without space between the lines).</summary>
		private int						_logicalHeight				= 0;
		
		/// <summary>The typical/average char width.</summary>
		private int						_typicalCharWidth			= 0;
		
		/// <summary>The ascent of the font.</summary>
		private int						_ascent						= 0;
		
		/// <summary>The descent of the font.</summary>
		private int						_descent					= 0;
		

        #endregion

        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary>Hidden default constructor.</summary>
		/// <param name="fontSpecification">The font name list.<see cref="System.String"/></param>
		/// <param name="display">The X11 display pointer, this font data are assigned to.<see cref="IntPtr"/></param>
		/// <param name="fontResourceId">The fontset resource id.<see cref="X11.XID"/></param>
		/// <param name="logicalHeight">The height of the font (without space between the lines).<see cref="System.Int32"/></param>
		/// <param name="ascent">The ascent of the font.<see cref="System.Int32"/></param>
		/// <param name="descent">The descent of the font.<see cref="System.Int32"/></param>
		private X11FontData (string fontSpecification, IntPtr display, X11.XID fontResourceId, int logicalHeight, int ascent, int descent)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				throw new ArgumentNullException ("fontSpecification");
			if (display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (fontResourceId == (X11.XID)0)
				throw new ArgumentNullException ("fontResourceId");
			
			_fontSpecification	= fontSpecification;
			_display			= display;
			_fontResourceId		= fontResourceId;
			_logicalHeight		= logicalHeight;
			_typicalCharWidth	= (logicalHeight + 2) / 3; // This is NOT a good assumption, but it is an assumption at all!
			_ascent				= ascent;
			_descent			= descent;
		}
		
		#endregion
		
        #region Factory
		
		/// <summary>Create font data for non-internationalized text output, using a single font.</summary>
		/// <param name="fontSpecification">The font name.<see cref="System.String"/></param>
		/// <param name="display">The X11 display pointer, this font data are assigned to.<see cref="IntPtr"/></param>
		/// <param name="fontResourceId">The font resource id.<see cref="X11.XID"/></param>
		/// <param name="logicalHeight">The height of the font (without space between the lines).<see cref="System.Int32"/></param>
		/// <param name="ascent">The ascent of the font.<see cref="System.Int32"/></param>
		/// <param name="descent">The descent of the font.<see cref="System.Int32"/></param>
		/// <returns>The new font data for non-internationalized text output, using a single font.<see cref="X11FontData"/></returns>
		public static X11FontData NewSingleFontData (string fontSpecification, IntPtr display, X11.XID fontResourceId, int logicalHeight, int ascent, int descent)
		{
			return new X11FontData (fontSpecification, display, fontResourceId, logicalHeight, ascent, descent);
		}
		
		/// <summary>Create font data for internationalized text output, using a fontset.</summary>
		/// <param name="fontSpecification">The font name list.<see cref="System.String"/></param>
		/// <param name="display">The X11 display pointer, this font data are assigned to.<see cref="IntPtr"/></param>
		/// <param name="fontResourceId">The fontset resource id.<see cref="X11.XID"/></param>
		/// <param name="logicalHeight">The height of the font (without space between the lines).<see cref="System.Int32"/></param>
		/// <param name="ascent">The ascent of the font.<see cref="System.Int32"/></param>
		/// <param name="descent">The descent of the font.<see cref="System.Int32"/></param>
		/// <returns>The new font data for internationalized text output, using a fontset.<see cref="X11FontData"/></returns>
		public static X11FontData NewFontSetData (string fontSpecification, IntPtr display, X11.XID fontResourceId, int logicalHeight, int ascent, int descent)
		{
			X11FontData fd = new X11FontData (fontSpecification, display, fontResourceId, logicalHeight, ascent, descent);
			fd._useFontset = true;
			return fd;
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
	
		/// <summary>Determine whether internationalized text output is enabled, using a fontset.</summary>
		public bool UseFontset
		{	get	{	return _useFontset;			}	}
		
		/// <summary>The current font/fontset specification. For applications that support fontsets a font name list, for applications that don't support fontsets a font name. Empty string for initially used default font/fontset.</summary>
		public string FontSpecification
		{	get	{	return _fontSpecification;	}	}
		
		/// <summary>The X11 display pointer, this font data are assigned to.</summary>
		public IntPtr Display
		{	get	{	return _display;			}	}
		
		/// <summary>The current font/fontset id. For applications that support fontsets a fontset id, for applications that don't support fontsets a font id. IntPtr.Zero for initially used default fontset.</summary>
		public X11.XID FontResourceId
		{	get	{	return _fontResourceId;		}	}
		
		/// <summary>Get the height of the font (without space between the lines).</summary>
		public int LogicalHeight
		{	get	{	return _logicalHeight;		}	}
		
		/// <summary>Get the ascent of the font.</summary>
		public int Ascent
		{	get	{	return _ascent;				}	}
		
		/// <summary>Get the descent of the font.</summary>
		public int Descent
		{	get	{	return _descent;			}	}
		
		/// <summary>Get the typical char width of the font.</summary>
		public int TypicalCharWidth
		{	get	{	return _typicalCharWidth;	}	}
		
		#endregion
	
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
	
		#region Static methods
		
		/// <summary>Modify the foundry part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="foundry">The foundry to set.<see cref="System.String"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationFoundry (string fontSpecification, string familyName)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 0)
				fontParts[1] = familyName.Trim ().ToLower ().ToString ();
			
			string result = "";
			for (int countPart = 1; countPart < fontParts.Length; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
			
		/// <summary>Modify the family name part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="familyName">The family name to set.<see cref="System.String"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationFamily (string fontSpecification, string familyName)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 1)
				fontParts[2] = familyName.Trim ().ToLower ().ToString ();
			
			string result = "";
			for (int countPart = 1; countPart < fontParts.Length; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
		
		/// <summary>Modify the size part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="size">The size to set. The size must be a positive number.<see cref="System.Single"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationSize (string fontSpecification, float size)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 6)
				fontParts[7] = ((int)(size + 0.5F)).ToString ();
			
			string result = "";
			for (int countPart = 1; countPart < fontParts.Length; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
		
		/// <summary>Modify the stretch part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="stretch">The stretch to set. The stretch must be a valid value. Typical values are "condensed", "normal" and "expanded".<see cref="System.String"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationStretch (string fontSpecification, string stretch)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 4)
				fontParts[5] = stretch.Trim ().ToLower ();
			
			string result = "";
			for (int countPart = 1; countPart < fontParts.Length; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
		
		/// <summary>Modify the slant part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="style">The slant to set. The slant must be a valid value. Typical values are "r", "i" and "o".<see cref="System.String"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationSlant (string fontSpecification, string style)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 3)
				fontParts[4] = style.Trim ().ToLower ();
			
			string result = "";
			for (int countPart = 1; countPart < fontParts.Length; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
		
		/// <summary>Modify the weight part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="weight">The weight to set. The weight must be a valid value. Typical values are "bold", "medium" and "regular".<see cref="System.String"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationWieght (string fontSpecification, string weight)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 2)
				fontParts[3] = weight.Trim ().ToLower ();
			
			string result = "";
			for (int countPart = 1; countPart < fontParts.Length; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
		
		/// <summary>Modify the script part of the indicated font specification.</summary>
		/// <param name="fontSpecification">The font specification to modify.<see cref="System.String"/></param>
		/// <param name="script">The script to set. The script must be a valid value. Typical values are "iso8859-*", "iso10646-*" and "ksc5601*".<see cref="System.String"/></param>
		/// <returns>The modified font specification on success, or an empty string otherwise.<see cref="System.String"/></returns>
		public static string ModifyFontSpecificationScript (string fontSpecification, string script)
		{
			if (string.IsNullOrEmpty (fontSpecification))
				return "";
			
			string[] fontParts = fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 12)
				fontParts[13] = script.Trim ().ToLower ();
			
			string result = "";
			// Attention! Old script definition might include the separator char too - and must be ommitted now!
			for (int countPart = 1; countPart < fontParts.Length && countPart < 14; countPart++)
				result += "-" + fontParts[countPart];
			
			return result;
		}
		
			
		#endregion Static methods
		
		#region Methods
		
		/// <summary>Free the resource.</summary>
		public void Unload()
		{
			if (_display != IntPtr.Zero && _fontResourceId != (X11.XID)0)
			{
				if (!_useFontset && _fontResourceId != (X11.XID)0)
					X11lib.XUnloadFont (_display, _fontResourceId);
				else if (_useFontset && _fontResourceId != (X11.XID)0)
					X11lib.XFreeFontSet (_display, _fontResourceId);
				
				_display = IntPtr.Zero;
				_fontResourceId = (X11.XID)0;
			}
		}
		
		
		/// <summary>Extract the font family name from the current font specification.</summary>
		/// <returns>The font family name.<see cref="System.String"/></returns>
		public string GetFamilyName ()
		{
			if (string.IsNullOrEmpty (_fontSpecification))
				return "*";
			
			string[] fontParts = _fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 1)
				return fontParts[2];
			
			return "*";
		}
		
		/// <summary>Extract the font size from the current font specification.</summary>
		/// <returns>The font size.<see cref="System.Single"/></returns>
		/// <remarks>Typical values are 10, 12 and 14.</remarks>
		public float GetSize ()
		{
			if (string.IsNullOrEmpty (_fontSpecification))
				return 12F;
			
			string[] fontParts = _fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 6)
			{
				float result = 12F;
				float.TryParse (fontParts[7], out result);
				return result;
			}
			return 12F;
		}
		
		/// <summary>Extract the font stretch from the current font specification.</summary>
		/// <returns>The font stretch.<see cref="System.String"/></returns>
		/// <remarks>Typical values are "condensed", "normal" and "expanded".</remarks>
		public string GetStretch ()
		{
			if (string.IsNullOrEmpty (_fontSpecification))
				return "normal";
			
			string[] fontParts = _fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 4)
				return fontParts[5];
			
			return "normal";
		}
		
		/// <summary>Extract the font slant from the current font specification.</summary>
		/// <returns>The font slant.<see cref="System.String"/></returns>
		/// <remarks>Typical values are "r", "i" and "o".</remarks>
		public string GetSlant ()
		{
			if (string.IsNullOrEmpty (_fontSpecification))
				return "r";
			
			string[] fontParts = _fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 3)
				return fontParts[4];
			
			return "r";
		}
		
		/// <summary>Extract the font weight from the current font specification.</summary>
		/// <returns>The font weight.<see cref="System.String"/></returns>
		/// <remarks>Typical values are "bold", "medium" and "regular".</remarks>
		public string GetWeight ()
		{
			if (string.IsNullOrEmpty (_fontSpecification))
				return "regular";
			
			string[] fontParts = _fontSpecification.Split (new char[] {'-'});
			if (fontParts.Length > 2)
				return fontParts[3];
			
			return "regular";
		}
		
		/// <summary>Set the typical char width.</summary>
		/// <param name="typicalCharWidth">The typical char width to set.<see cref="System.Int32"/></param>
		public void SetTypicalCharWidth (int typicalCharWidth)
		{	_typicalCharWidth = typicalCharWidth;	}
		
		#endregion
		
	}
}

