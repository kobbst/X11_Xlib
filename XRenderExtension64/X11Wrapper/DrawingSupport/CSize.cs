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
	
	/// <summary>Provide a reference type equivalent to System.Drawing.Size (value type).</summary>
	public sealed class CSize // : Object
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants
		
		/// <summary>Gets a Size structure that has a Height and Width value of 0.</summary>
		public static readonly CSize Empty = new CSize (0, 0);
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The width.</summary>
		private int _width;
		
		/// <summary>The height.</summary>
		private int _height;

        #endregion

        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>Hidden default constructor.</summary>
		private CSize ()
		{
			_width = 0;
			_height = 0;
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="width">The initial width.<see cref="System.Int32"/></param>
		/// <param name="height">The initial height.<see cref="System.Int32"/></param>
		public CSize (int width, int height)
		{
			_width = width;
			_height = height;
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary>Gets or sets the horizontal component of this CSize class.</summary>
		public int Width
		{
			get {	return _width;	}
			set	{	_width = value;	}
		}
		
		/// <summary>Gets or sets the vertical component of this CSize class.</summary>
		public int Height
		{
			get {	return _height;	}
			set	{	_height = value;	}
		}
		
		/// <summary>Tests whether this Size structure has width and height of 0.</summary>
		public bool IsEmpty
		{
			get 	{	return (_width == 0 && _height == 0 ? true : false);	}
		}
		
		#endregion
	
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
	
		#region Methods
		
		/// <summary>Convert to value type.</summary>
		/// <returns>The value type equivalent.<see cref="System.Drawing.Size"/></returns>
		public System.Drawing.Size ToSize()
		{	return new System.Drawing.Size (_width, _height);		}
		
		/// <summary>Set a new size.</summary>
		/// <param name="size">The new size.<see cref="System.Drawing.Size"/></param>
		public void Set (System.Drawing.Size size)
		{
			_width = size.Width;
			_height = size.Height;
		}
		
		#endregion
		
	}

}