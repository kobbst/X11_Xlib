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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Drawing
{
	
	// Extension methods for System.Drawing.Size.
	// To use extension methods, you MUST include the namespace (using System.Drawing.Size;)!	
	public static class SizeExtensions
	{
		
		/// <summary>Add a size (summand) to a size.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Size"/></param>
		/// <param name="size">The size to get a size added.<see cref="System.Drawing.Size"/></param>
		/// <param name="summand">The size to add.<see cref="System.Drawing.Size"/></param>
		public static void Add (this System.Drawing.Size copyOnValueTypes, ref System.Drawing.Size size, System.Drawing.Size summand)
		{
			size.Width += summand.Width;
			size.Height += summand.Height;			
		}
	
		/// <summary>Add a width and height to a size.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Size"/></param>
		/// <param name="size">The size to get a width and height added.<see cref="System.Drawing.Size"/></param>
		/// <param name="width">The width to add.<see cref="System.Int32"/></param>
		/// <param name="height">The height to add.<see cref="System.Int32"/></param>
		public static void Add (this System.Drawing.Size copyOnValueTypes, ref System.Drawing.Size size, int width, int height)
		{
			size.Width += width;
			size.Height += height;			
		}
	
		/// <summary>Subtract a size from a size.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Size"/></param>
		/// <param name="size">The size to get a size subtracted.<see cref="System.Drawing.Size"/></param>
		/// <param name="subtrahend">The size to subtract.<see cref="System.Drawing.Size"/></param>
		public static void Subtract (this System.Drawing.Size copyOnValueTypes, ref System.Drawing.Size size, System.Drawing.Size subtrahend)
		{
			size.Width -= subtrahend.Width;
			size.Height -= subtrahend.Height;			
		}
	
		/// <summary>Subtract a width and height from a size.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Size"/></param>
		/// <param name="size">The size to get a width and height subtracted.<see cref="System.Drawing.Size"/></param>
		/// <param name="width">The width to subtract.<see cref="System.Int32"/></param>
		/// <param name="height">The height to subtract.<see cref="System.Int32"/></param>
		public static void Subtract (this System.Drawing.Size copyOnValueTypes, ref System.Drawing.Size size, int width, int height)
		{
			size.Width -= width;
			size.Height -= height;			
		}
		
	}
	
	// Extension methods for System.Drawing.Rectangle.
	// To use extension methods, you MUST include the namespace (using System.Drawing.Size;)!	
	public static class RectExtensions
	{
		
		/// <summary>Set all values.</summary>
		/// <param name="rect">A reference to the rectangle, that shold be set.
		/// Setting can't be performed on the 'this' instance, because it is a value type (deep copy).<see cref="System.Drawing.Rectangle"/> </param>
		/// <param name="x">The x-coordinate. <see cref="System.Int32"/></param>
		/// <param name="y">The y-coordinate. <see cref="System.Int32"/></param>
		/// <param name="width">The width. <see cref="System.Int32"/></param>
		/// <param name="height">The height. <see cref="System.Int32"/></param>
		public static void Set (this System.Drawing.Rectangle copyOnValueTypes, ref Rectangle rect, int x, int y, int width, int height)
		{
			rect.X = x;
			rect.Y = y;
			rect.Width = width;
			rect.Height = height;
		}
		
		/// <summary>Reset all values.</summary>
		/// <param name="copyOnValueTypes">A reference to the rectangle, that shold be set.
		/// Setting can't be performed on the 'this' instance, because it is a value type (deep copy).<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="rect">The new coordinates. <see cref="TRectangle"/></param>
		public static void Set (this System.Drawing.Rectangle copyOnValueTypes, ref Rectangle rect, Rectangle setter)
		{
			rect.X = setter.X;
			rect.Y = setter.Y;
			rect.Width = setter.Width;
			rect.Height = setter.Height;
		}
		
		/// <summary>Get the top left position.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Rectangle"/></param>
		/// <returns>The top left position of a rectangle.<see cref="System.Drawing.Point"/></returns>
		public static Point Position (this System.Drawing.Rectangle copyOnValueTypes)
		{
			return new Point (copyOnValueTypes.X, copyOnValueTypes.Y);
		}
		
		/// <summary>Get the size.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Rectangle"/></param>
		/// <returns>The size of a rectangle.<see cref="System.Drawing.Size"/></returns>
		public static Size Size (this System.Drawing.Rectangle copyOnValueTypes)
		{
			return new Size (copyOnValueTypes.Width, copyOnValueTypes.Height);
		}
			
		/// <summary>Test indicated point for containment.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="x">The x-coordinate to check for containment.<see cref="System.Int32"/></param>
		/// <param name="y">The y-coordinate to check for containment.<see cref="System.Int32"/></param>
		/// <returns>True, if pint is contained, false otherwise.<see cref="System.Boolean"/></returns>
		public static bool Contained (this System.Drawing.Rectangle copyOnValueTypes, int x, int y)
		{
			if (x >= copyOnValueTypes.X && x <= copyOnValueTypes.Right && y >= copyOnValueTypes.Y && y <= copyOnValueTypes.Bottom)
				return true;
			else
				return false;
		}
			
		/// <summary>Test indicated point for containment.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Rectangle"/></param>
		/// <param name="point">The point to test for containment.<see cref="System.Drawing.Point"/></param>
		/// <returns>True, if pint is contained, false otherwise.<see cref="System.Boolean"/></returns>
		public static bool Contained (this System.Drawing.Rectangle copyOnValueTypes, Point point)
		{
			if (point.X >= copyOnValueTypes.X && point.X <= copyOnValueTypes.Right && point.Y >= copyOnValueTypes.Y && point.Y <= copyOnValueTypes.Bottom)
				return true;
			else
				return false;
		}
			
		/// <summary>Test indicated point for containment.</summary>
		/// <param name="point">The point to test for containment.<see cref="System.Drawing.Point"/></param>
		/// <param name="fuzzy">The additional fuzzy offset.<see cref="System.Drawing.Point"/></param>
		/// <returns>True, if pint is contained, false otherwise.<see cref="System.Boolean"/></returns>
		public static bool FuzzyContained (this System.Drawing.Rectangle copyOnValueTypes, Point point, Size fuzzy)
		{
			if (point.X + fuzzy.Width  >= copyOnValueTypes.X &&
			    point.X - fuzzy.Width  <= copyOnValueTypes.Right &&
			    point.Y + fuzzy.Height >= copyOnValueTypes.Y &&
			    point.Y - fuzzy.Height <= copyOnValueTypes.Bottom)
				return true;
			else
				return false;
		}
		
	}

}

namespace System.Xml
{
	
	// Extension methods for ystem.Xml.XmlNode.
	// To use extension methods, you MUST include the namespace (using System.Drawing.Size;)!	
	public static class XmlNodeExtensions
	{
		
		/// <summary>Get an XML attribute's value safely by the attribute's name.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Size"/></param>
		/// <param name="name">The attribute name to get the value safely for.<see cref="System.String"/></param>
		/// <param name="fallbackValue">The fallback value, if the attribute's value can't be determined.<see cref="System.String"/></param>
		/// <returns>The attribute's value on success, or the fallback value otherwise.<see cref="System.String"/></returns>
		public static string AttributeValue (this System.Xml.XmlNode copyOnValueTypes, string name, string fallbackValue)
		{
			if (copyOnValueTypes == null)
				return fallbackValue;
			
			return (copyOnValueTypes.Attributes[name] != null ? copyOnValueTypes.Attributes[name].Value : fallbackValue);
		}
		
		/// <summary>Get an XML attribute's value safely by the attribute's local name.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.Drawing.Size"/></param>
		/// <param name="localName">The local attribute name to get the value safely for.<see cref="System.String"/></param>
		/// <param name="fallbackValue">The fallback value, if the attribute's value can't be determined.<see cref="System.String"/></param>
		/// <returns>The attribute's value on success, or the fallback value otherwise.<see cref="System.String"/></returns>
		public static string AttributeValueLocal (this System.Xml.XmlNode copyOnValueTypes, string localName, string fallbackValue)
		{
			if (copyOnValueTypes == null || copyOnValueTypes.Attributes == null)
				return fallbackValue;
			
			foreach (XmlAttribute attr in copyOnValueTypes.Attributes)
			{
				if (attr.LocalName == localName)
					return attr.Value;
			}
			return fallbackValue;
		}
		
		/// <summary>Get the first sub-node of requested type.</summary>
		/// <param name="copyOnValueTypes">The reference to the type to extent.<see cref="System.XML.XmlNode"/></param>
		/// <param name="typeName">The type name of the sub-node to get.<see cref="System.String"/></param>
		/// <returns>The requested sub-node on success, or null otherwise.<see cref="System.XML.XmlNode"/></returns>
		public static System.Xml.XmlNode FirstChildOfTypeName (this System.Xml.XmlNode copyOnValueTypes, string typeName)
		{
			foreach (XmlNode child in copyOnValueTypes.ChildNodes)
				if (child.Name == typeName)
					return child;
			return null;
		}
	
	}
}

namespace X11
{
	
	/// <summary> The orientation, a collection can have. </summary>
	public enum TOrientation
	{
		/// <summary> The collection is oriented horizontally. </summary>
		Horizontal,
		
		/// <summary> The collection is oriented vertically. </summary>
		Vertical
	}
	
	/// <summary> The shadow type to apply a 3D look. </summary>
	public enum TFrameType
	{
		/// <summary> No frame at all. </summary>
		None,
		
		/// <summary> No 3D effect. </summary>
		FlatRounded,
		
		/// <summary> The frame appears in raised 3D effect. </summary>
		Raised,
		
		/// <summary> The frame appears in sunken 3D effect. </summary>
		Sunken,
		
		/// <summary> The frame appears in chiseled 3D effect. </summary>
		Chiseled,
		
		/// <summary> The frame appears in chiseled 3D effect. </summary>
		ChiseledRightOnly,
		
		/// <summary> The frame appears in ledged 3D effect. </summary>
		Ledged,
		
		/// <summary> The frame appears in ledged 3D effect. </summary>
		LedgedRightOnly,
		
		/// <summary> The frame appears as bottom aligned raised 3D effect. </summary>
		RaisedTopOnly,
		
		/// <summary> The frame appears as right aligned raised 3D effect. </summary>
		RaisedLeftOnly,
		
		/// <summary> The frame appears as left aligned raised 3D effect. </summary>
		RaisedRightOnly,
		
		/// <summary> The frame appears as top aligned raised 3D effect. </summary>
		RaisedBottomOnly,
		
		/// <summary> The frame appears as top aligned sunken 3D effect. </summary>
		SunkenTopOnly,
		
		/// <summary> The frame appears as left aligned tsunken 3D effect. </summary>
		SunkenLeftOnly,
		
		/// <summary> The frame appears as right aligned sunken 3D effect. </summary>
		SunkenRightOnly,
		
		/// <summary> The frame appears as bottom aligned sunken 3D effect. </summary>
		SunkenBottomOnly,
		
		/// <summary> The frame appears as bottom aligned menu item delimiter. </summary>
		MenuDelimiter
	}

}

