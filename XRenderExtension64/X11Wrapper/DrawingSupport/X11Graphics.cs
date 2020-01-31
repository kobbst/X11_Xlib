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

namespace X11
{
	
	/// <summary>Combine surface and graphics context for easy parameter passing.</summary>
	/// <remarks>This object provides methods for drawing objects on the display device.</remarks>
	public class X11Graphics
	{
		/// <summary>The surface.</summary>
		private X11Surface _surface;
		
		/// <summary>The graphics context.</summary>
		private IntPtr     _gc;
		
		/// <summary>The initializing constructor.</summary>
		/// <param name="surface">The surface.<see cref="X11Surface"/></param>
		/// <param name="gc">The graphics context.<see cref="IntPtr"/></param>
		public X11Graphics (X11Surface surface, IntPtr gc)
		{
			_surface = surface;
			_gc      = gc;
		}
		
		/// <summary>Get or set the surface.</summary>
		public X11Surface Surface
		{	get	{	return	_surface;	}
			set	{	_surface = value;	}
		}
		
		/// <summary>Get or set the graphics context.</summary>
		public IntPtr     GC
		{	get	{	return	_gc;	}
			set	{	_gc = value;	}
		}
		
	}
	
}

