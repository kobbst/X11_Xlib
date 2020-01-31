// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: November 2014
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
using System.Drawing;

namespace X11
{
	public class X11PaintEventArgs : EventArgs
	{
		/// <summary>The rectangle in which to paint.</summary>
		private Rectangle   _clipRect;
		
		/// <summary>The graphics used to paint.</summary>
        private X11Graphics _graphics;
		
		/// <summary>Initializes a new instance of the X11PaintEventArgs class with the specified graphics and clipping rectangle.</summary>
		/// <param name="graphics">he X11Graphics used to paint the item.<see cref="X11Graphics"/></param>
		/// <param name="clipRect">The Rectangle that represents the rectangle in which to paint.<see cref="Rectangle"/></param>
		public X11PaintEventArgs(X11Graphics graphics, Rectangle clipRect)
		{
			_graphics = graphics;
			_clipRect = clipRect;
		}

        /// <summary>Get the rectangle in which to paint.</summary>
		/// <remarks>The Rectangle in which to paint.</remarks>
		public Rectangle ClipRectangle
		{	get	{	return _clipRect;	}	}
		
		/// <summary>Get the graphics used to paint.</summary>
		/// <returns>The X11Graphics object used to paint. This object provides methods for drawing objects on the display device.</returns>
        public X11Graphics Graphics
		{	get	{	return _graphics;	}	}

	}
}