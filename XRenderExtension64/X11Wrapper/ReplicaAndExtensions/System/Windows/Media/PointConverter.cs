// =====================
// The "Roma Widget Set"
// =====================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: June 2015
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
using System.ComponentModel;
using System.Globalization;

using X11;

namespace System.Windows
{
    /// <summary>Used to convert a System.Windows.Media.Brush object to or from another object type.</summary>
    public sealed class PointConverter : TypeConverter
    {

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "PointConverter";

		#endregion Constants
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		//Initialize a new instance of the System.Windows.Media.PointConverter class.
        public PointConverter()
		{	;	}

		#endregion Construction


		/// <summary>Determine whether this class can convert an object of a given type to a  System.Windows.Point object.</summary>
		/// <param name="context">The conversion context.<see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
		/// <param name="sourceType">The type from which to convert.<see cref="System.Type"/></param>
		/// <returns>Return true if conversion is possible (object is string type), or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
		{
			if (sourceType == typeof (System.String))
			    return true;
		    else
				return false;
		}

		/// <summary>Determine whether this class can convert an object of System.Windows.Point to the specified destination type.</summary>
		/// <param name="context">The conversion context.<see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
		/// <param name="destinationType">The destination type.<see cref="System.Type"/></param>
		/// <returns>Return true if conversion is possible, or false otherwise.<see cref="System.Boolean"/></returns>
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType)
		{
			if (destinationType == typeof (System.Windows.Point))
			    return true;
		    else
				return false;
		}

		/// <summary>Attempt to convert from an object of a given type to a System.Windows.Media.Brush object.</summary>
		/// <param name="context">The conversion context.<see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
		/// <param name="culture">The culture information that applies to the conversion.<see cref="System.Globalization.CultureInfo"/></param>
		/// <param name="value">The object to convert.<see cref="System.Object"/></param>
		/// <returns>Returns a new System.Windows.Media.Brush object on success, or NULL otherwise.<see cref="System.Object"/></returns>
		/// <exception cref="System.NotSupportedException">The value is NULL or cannot be converted to a System.Windows.Media.Brush.</exception>
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value == null)
				throw new NotSupportedException (CLASS_NAME + "::ConvertFrom () : Can not convert a null value.");
			if ((value as System.String) == null)
				throw new NotSupportedException (CLASS_NAME + "::ConvertFrom () : Can not convert from other types than System.String and derived classes.");
			
			string strPoint = value as string;

			if (string.IsNullOrEmpty (strPoint))
				return null;
			
		    strPoint = strPoint.Trim();
            if (strPoint.Length == 0)
				return null;
			
			System.Windows.Point result;
			if (System.Windows.Point.TryParse (strPoint, out result) == true)
				return result;
			
            throw new ArgumentException(CLASS_NAME + "::ConvertFrom () : Requested format is: x, y");
		}

		/// <summary>Attempt to convert a System.Windows.Point object to a specified type, using the
        /// specified context and culture information.</summary>
		/// <param name="context">The conversion context.<see cref="System.ComponentModel.ITypeDescriptorContext"/></param>
		/// <param name="culture">The current culture information.<see cref="System.Globalization.CultureInfo"/></param>
		/// <param name="value">The System.Windows.Media.Brush to convert.<see cref="System.Object"/></param>
		/// <param name="destinationType">The destination type that the value object is converted to.<see cref="System.Type"/></param>
		/// <returns>An object that represents the converted value.<see cref="System.Object"/></returns>
		/// <exception cref="System.NotSupportedException">The value is NULL or it is not a System.Windows.Media.Brush-or-destinationType
        /// is not a valid destination type.</exception>
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
		{
			if (value == null)
				throw new NotSupportedException (CLASS_NAME + "::ConvertTo () : Can not convert a null value.");
			if (!(value is System.Windows.Point))
				throw new NotSupportedException (CLASS_NAME + "::ConvertTo () : Can not convert from other types than System.Windows.Point and derived classes.");
			if (destinationType == typeof (System.String))
				throw new NotSupportedException (CLASS_NAME + "::ConvertTo () : Can not convert to other types than System.String.");
			
            string sep = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ListSeparator;

			return ((System.Windows.Point)value).X.ToString (System.Globalization.CultureInfo.InvariantCulture) + sep +
				((System.Windows.Point)value).Y.ToString (System.Globalization.CultureInfo.InvariantCulture);				
		}
    }
}