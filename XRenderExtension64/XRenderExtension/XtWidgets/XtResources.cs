// ======================
// The "Xt Shell Wrapper"
// ======================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: May 2013
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

using X11;

namespace Xt
{
	internal class XtResources
	{
        
		/// <summary> Keep the current thread's current UI culture property for all resource lookups. </summary>
        private static System.Globalization.CultureInfo resourceCulture = null;

		/// <summary> Get current thread's current UI culture LCID. </summary>
		private static int Lcid
		{	get
			{
				if (resourceCulture != null)
					return resourceCulture.LCID;
				else
					return System.Globalization.CultureInfo.CurrentUICulture.LCID;
			}
		}
		
        /// <summary> Get or set the current thread's current UI culture property for all resource lookups. </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
		
		#region Texts
		
		#endregion
		
		#region Images
		
		internal static TUint SMALL_ICON_WIDTH
		{	get	{	return (TUint)16;	}	}
			
		internal static TUint SMALL_ICON_HEIGHT
		{	get	{	return (TUint)16;	}	}
		
		private static TUchar[] _SMALL_EXCLAMATION_ICON_BITS = {
			(TUchar)0x00, (TUchar)0x00,
			(TUchar)0x80, (TUchar)0x01,
			(TUchar)0x80, (TUchar)0x01,
			(TUchar)0xC0, (TUchar)0x03,
			(TUchar)0xC0, (TUchar)0x03,
			(TUchar)0x60, (TUchar)0x06,
			(TUchar)0x60, (TUchar)0x06,
			(TUchar)0xB0, (TUchar)0x0D,
			(TUchar)0xB0, (TUchar)0x0D,
			(TUchar)0x98, (TUchar)0x19,
			(TUchar)0x18, (TUchar)0x18,
			(TUchar)0x8C, (TUchar)0x31,
			(TUchar)0x0C, (TUchar)0x30,
			(TUchar)0xFE, (TUchar)0x7F,
			(TUchar)0xFE, (TUchar)0x7F,
			(TUchar)0x00, (TUchar)0x00};
		internal static TUchar[] SMALL_EXCLAMATION_ICON_BITS
		{	get	{	return _SMALL_EXCLAMATION_ICON_BITS;	}	}
		
		internal static TUint BIG_ICON_WIDTH
		{	get	{	return (TUint)32;	}	}
			
		internal static TUint BIG_ICON_HEIGHT
		{	get	{	return (TUint)32;	}	}
			
		private static TUchar[] _BIG_EXCLAMATION_ICON_BITS = {
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x80, (TUchar)0x01, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x80, (TUchar)0x01, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xE0, (TUchar)0x07, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x60, (TUchar)0x06, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x70, (TUchar)0x0E, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0x30, (TUchar)0x0C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x38, (TUchar)0x1C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x18, (TUchar)0x18, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x9C, (TUchar)0x39, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xCC, (TUchar)0x33, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xEE, (TUchar)0x77, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xE6, (TUchar)0x67, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xE7, (TUchar)0xE7, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xE3, (TUchar)0xC7, (TUchar)0x00,
			(TUchar)0x80, (TUchar)0xE3, (TUchar)0xC7, (TUchar)0x01,
			(TUchar)0x80, (TUchar)0xC1, (TUchar)0x83, (TUchar)0x01,
			(TUchar)0xC0, (TUchar)0xC1, (TUchar)0x83, (TUchar)0x03,
			
			(TUchar)0xC0, (TUchar)0x80, (TUchar)0x01, (TUchar)0x03,
			(TUchar)0xE0, (TUchar)0x80, (TUchar)0x01, (TUchar)0x07,
			(TUchar)0x60, (TUchar)0x00, (TUchar)0x00, (TUchar)0x06,
			(TUchar)0x70, (TUchar)0x80, (TUchar)0x01, (TUchar)0x0E,
			
			(TUchar)0x30, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x0C,
			(TUchar)0x38, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x1C,
			(TUchar)0x18, (TUchar)0x80, (TUchar)0x01, (TUchar)0x18,
			(TUchar)0x1C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x38,
			
			(TUchar)0xFC, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x3F,
			(TUchar)0xFC, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x3F,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00};
		internal static TUchar[] BIG_EXCLAMATION_ICON_BITS
		{	get	{	return _BIG_EXCLAMATION_ICON_BITS;	}	}
			
		private static TUchar[] _BIG_INFORMATION_ICON_BITS = {
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x80, (TUchar)0x01, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x80, (TUchar)0x01, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xE0, (TUchar)0x07, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x60, (TUchar)0x06, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x70, (TUchar)0x0E, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0x30, (TUchar)0x0C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x38, (TUchar)0x1C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x18, (TUchar)0x18, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x9C, (TUchar)0x39, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xCC, (TUchar)0x33, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xCE, (TUchar)0x73, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x86, (TUchar)0x61, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x07, (TUchar)0xE0, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0x83, (TUchar)0xC1, (TUchar)0x00,
			(TUchar)0x80, (TUchar)0xC3, (TUchar)0xC3, (TUchar)0x01,
			(TUchar)0x80, (TUchar)0xE1, (TUchar)0x83, (TUchar)0x01,
			(TUchar)0xC0, (TUchar)0xF1, (TUchar)0x83, (TUchar)0x03,
			
			(TUchar)0xC0, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x03,
			(TUchar)0xE0, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x07,
			(TUchar)0x60, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x06,
			(TUchar)0x70, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x0E,
			
			(TUchar)0x30, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x0C,
			(TUchar)0x38, (TUchar)0xC0, (TUchar)0x17, (TUchar)0x1C,
			(TUchar)0x18, (TUchar)0x80, (TUchar)0x0F, (TUchar)0x18,
			(TUchar)0x1C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x38,
			
			(TUchar)0xFC, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x3F,
			(TUchar)0xFC, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x3F,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00};
		internal static TUchar[] BIG_INFORMATION_ICON_BITS
		{	get	{	return _BIG_INFORMATION_ICON_BITS;	}	}
		
		private static TUchar[] _BIG_QUESTION_ICON_BITS = {
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x80, (TUchar)0x01, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x80, (TUchar)0x01, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xE0, (TUchar)0x07, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x60, (TUchar)0x06, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x70, (TUchar)0x0E, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0x30, (TUchar)0x0C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x38, (TUchar)0x1C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x18, (TUchar)0x18, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xDC, (TUchar)0x3B, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0xEC, (TUchar)0x37, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x6E, (TUchar)0x76, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x36, (TUchar)0x6C, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x37, (TUchar)0xEC, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0x03, (TUchar)0xCC, (TUchar)0x00,
			(TUchar)0x80, (TUchar)0x03, (TUchar)0xC6, (TUchar)0x01,
			(TUchar)0x80, (TUchar)0x01, (TUchar)0x83, (TUchar)0x01,
			(TUchar)0xC0, (TUchar)0x81, (TUchar)0x81, (TUchar)0x03,
			
			(TUchar)0xC0, (TUchar)0x80, (TUchar)0x01, (TUchar)0x03,
			(TUchar)0xE0, (TUchar)0x80, (TUchar)0x01, (TUchar)0x07,
			(TUchar)0x60, (TUchar)0x00, (TUchar)0x00, (TUchar)0x06,
			(TUchar)0x70, (TUchar)0x80, (TUchar)0x01, (TUchar)0x0E,
			
			(TUchar)0x30, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x0C,
			(TUchar)0x38, (TUchar)0xC0, (TUchar)0x03, (TUchar)0x1C,
			(TUchar)0x18, (TUchar)0x80, (TUchar)0x01, (TUchar)0x18,
			(TUchar)0x1C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x38,
			
			(TUchar)0xFC, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x3F,
			(TUchar)0xFC, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x3F,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00};
		internal static TUchar[] BIG_QUESTION_ICON_BITS
		{	get	{	return _BIG_QUESTION_ICON_BITS;	}	}
		
		private static TUchar[] _BIG_STOP_ICON_BITS = {
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xFC, (TUchar)0x3F, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xFE, (TUchar)0x7F, (TUchar)0x00,

			(TUchar)0x00, (TUchar)0x07, (TUchar)0xE0, (TUchar)0x00,
			(TUchar)0x80, (TUchar)0x03, (TUchar)0xC0, (TUchar)0x01,
			(TUchar)0xC0, (TUchar)0x01, (TUchar)0x80, (TUchar)0x03,
			(TUchar)0xE0, (TUchar)0x00, (TUchar)0x00, (TUchar)0x07,

			(TUchar)0x70, (TUchar)0x00, (TUchar)0x00, (TUchar)0x0E,
			(TUchar)0x38, (TUchar)0x00, (TUchar)0x00, (TUchar)0x1C,
			(TUchar)0x1C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x38,
			(TUchar)0x0C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x30,

			(TUchar)0x0C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x30,
			(TUchar)0x0C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x30,
			(TUchar)0x0C, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x30,
			(TUchar)0x0C, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x30,

			(TUchar)0x0C, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x30,
			(TUchar)0x0C, (TUchar)0xFF, (TUchar)0xFF, (TUchar)0x30,
			(TUchar)0x0C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x30,
			(TUchar)0x0C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x30,
			
			(TUchar)0x0C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x30,
			(TUchar)0x1C, (TUchar)0x00, (TUchar)0x00, (TUchar)0x38,
			(TUchar)0x38, (TUchar)0x00, (TUchar)0x00, (TUchar)0x1C,
			(TUchar)0x70, (TUchar)0x00, (TUchar)0x00, (TUchar)0x0E,
			
			(TUchar)0xE0, (TUchar)0x00, (TUchar)0x00, (TUchar)0x07,
			(TUchar)0xC0, (TUchar)0x01, (TUchar)0x80, (TUchar)0x03,
			(TUchar)0x80, (TUchar)0x03, (TUchar)0xC0, (TUchar)0x01,
			(TUchar)0x00, (TUchar)0x07, (TUchar)0xE0, (TUchar)0x00,
			
			(TUchar)0x00, (TUchar)0xFE, (TUchar)0x7F, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0xFC, (TUchar)0x3F, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00,
			(TUchar)0x00, (TUchar)0x00, (TUchar)0x00, (TUchar)0x00};
		internal static TUchar[] BIG_STOP_ICON_BITS
		{	get	{	return _BIG_STOP_ICON_BITS;	}	}
		
		#endregion
			
	}
}

