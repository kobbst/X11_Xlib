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

using X11;

namespace X11.Text
{

	/// <summary>List of currently defined text styles.</summary>
	public class StyleList : List<ITextStyle>
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string			CLASS_NAME = "StyleList";
	
        #endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Methods
		
		/// <summary>Get the first index of an already existing style matching all predicates or get the index of a newly created style.</summary>
		/// <param name="foreColor">The fore/test color.Can be null to omit drawing.<see cref="TColor"/></param>
		/// <param name="backColor">The back color. Can be null to draw transparent.<see cref="System.Nullable<TPixel>"/></param>
		/// <param name="fontStyle">The font style.<see cref="System.Drawing.FontStyle"/></param>
		/// <param name="fontData">The font data.<see cref="X11.X11FontData"/></param>
		/// <returns>The index of a matching style.<see cref="System.Int32"/></returns>
		public int GetOrCreateStyle (TColor foreColor, TColor backColor, System.Drawing.FontStyle fontStyle, X11.X11FontData fontData)
		{
			for (int index = 0; index < this.Count; index++)
			{
				if (this[index].ForeColor == foreColor)
				{
					if (this[index].BackColor == backColor)
					{
						if (this[index].FontStyle == fontStyle &&
						    this[index].FontData.FontSpecification == fontData.FontSpecification)
							return index;
					}
				}
			}
			Text.Style style = new Text.Style (foreColor, backColor, fontStyle, fontData);
			this.Add (style);
			return this.Count - 1;
		}
		
		#endregion Methods
		
	}
	
	/// <summary>Stack of currently defined text styles.</summary>
	public class StyleStack
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string			CLASS_NAME = "StyleStack";
	
        #endregion
		
		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
        /// <summary>The list of currently defined text styles.</summary>
		protected List<ITextStyle>		_styles = null;
		
		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Create a new X11.Text.StyleStack instance.</summary>
		public StyleStack ()
		{	_styles = new List<ITextStyle> ();
		}

        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
				
        /// <summary>Get the number of elements in the current instance.</summary>
		public int Count
		{	get {	return _styles.Count;	}	}
		
		/// <summary>Get the indicated ITextStyle color on success, or null otherwise.</summary>
		public ITextStyle this[int index]
		{	get
			{	if (_styles != null && index >= 0 && _styles.Count > index)	return _styles[index];
				else														return null;
			}
		}
		
		#endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Methods
		
		/// <summary>Add an item to the end of the list.</summary>
		/// <param name="item">The style to add to the list.<see cref="ITextStyle"/></param>
		/// <remarks>The list is limited to a maximum of 32 styles.</remarks>
		public void Add (ITextStyle item)
		{
			if (_styles.Count >= 32)
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::Add () Can't handle more than 32 styles!");
			else
				_styles.Add (item);
		}
		
		/// <summary>Add the elements of the specified collection to the end of the list.</summary>
		/// <param name="collection">The collection whose elements are added to the end of the list.<see cref="IEnumerable<ITextStyle>"/></param>
		/// <remarks>The list is limited to a maximum of 32 styles.</remarks>
		public void AddRange (IEnumerable<ITextStyle> collection)
		{
			if (collection == null)
				throw new ArgumentNullException ("collection");
			
			foreach (ITextStyle item in collection)
			{
				if (_styles.Count >= 32)
				{
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::AddRange () Can't handle more than 32 styles!");
					return;
				}
				else
					_styles.Add (item);
			}
		}
		
		/// <summary>Remove all elements from the list.</summary>
		public void Clear ()
		{	_styles.Clear ();
		}
		
		/// <summary>Determine whether the list contains a specific value. </summary>
		/// <param name="item">The item (item can be null) to locate in the current collection.</param>
		/// <returns>True if item is found in the list, or false otherwise.<see cref="System.Boolean"/></returns>
		public bool Contains (ITextStyle item)
		{	return _styles.Contains (item);
		}
		
		/// <summary>Return an enumerator, in index order, that can be used to iterate over the list.</summary>
		/// <returns>An enumerator for the list.<see cref="List<ITextStyle>.Enumerator"/></returns>
		public List<ITextStyle>.Enumerator GetEnumerator ()
		{	return _styles.GetEnumerator ();
		}
		
		/// <summary>Search for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire List.</summary>
		/// <param name="match">The predicate delegate that specifies the element to search for.<see cref="Predicate<ITextStyle>"/></param>
		/// <returns>The first element that matches the conditions defined by the specified predicate if found, or the default value for type ITextStyle otherwise.<see cref="ITextStyle"/></returns>
		public ITextStyle Find (Predicate<ITextStyle> match)
		{	return _styles.Find (match);
		}
		
		/// <summary>Search for the specified object and returns the zero-based index of the first occurrence within the entire list. </summary>
		/// <param name="item">The item to search the index for.<see cref="ITextStyle"/></param>
		/// <returns>The zero-based index of the first occurrence of item within the List, if found, or -1 otherwise.<see cref="System.Int32"/></returns>
		public int IndexOf (ITextStyle item)
		{	return _styles.IndexOf (item);
		}
		
		/// <summary>Get the first index of an already existing style matching all predicates or get the index of a newly created style.</summary>
		/// <param name="foreColor">The fore/test color.Can be null to omit drawing.<see cref="X11.TColor"/></param>
		/// <param name="backColor">The back color. Can be null to draw transparent.<see cref="X11.TColor"/></param>
		/// <param name="fontStyle">The font style.<see cref="System.Drawing.FontStyle"/></param>
		/// <param name="fontData">The font data.<see cref="X11.X11FontData"/></param>
		/// <returns>The index of a matching style.<see cref="System.Int32"/></returns>
		public int GetOrCreateStyle (TColor foreColor, TColor backColor, System.Drawing.FontStyle fontStyle, X11.X11FontData fontData)
		{
			for (int index = 0; index < _styles.Count; index++)
			{
				if (_styles[index].ForeColor == foreColor)
				{
					if (_styles[index].BackColor == backColor)
					{
						if (_styles[index].FontStyle == fontStyle &&
						    _styles[index].FontData.FontSpecification == fontData.FontSpecification)
							return index;
					}
				}
			}
			Text.Style style = new Text.Style (foreColor, backColor, fontStyle, fontData);
			_styles.Add (style);
			return _styles.Count - 1;
		}
		
		#endregion Methods

	}
}

