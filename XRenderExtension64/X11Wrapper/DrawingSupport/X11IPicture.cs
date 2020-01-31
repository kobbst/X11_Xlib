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
using System.Diagnostics;
using System.Drawing;

namespace X11
{
	
	/// <summary>Define a common interface for 'fix pictures' based on a bitmap image 'X11Graphic' and 'writable pictures' based on a drawable image pixmap 'X11Image'.</summary>
	public interface X11IPicture: IDisposable
	{

		#region Properties
		
		/// <summary>Get the bitmap width.</summary>
		int Width	{	get;	}
		
		/// <summary>Get the bitmap height.</summary>
		int Height	{	get;	}
		
		/// <summary>Get the bitmap size.</summary>
		System.Drawing.Size Size	{	get;	}
		
		/// <summary>Get the depth (number of planes) of the graphic.</summary>
		int GraphicDepth	{	get;	}

		#endregion Properties

		#region Methods
		
		/// <summary>Draw the image in the indicated window, using the indicated graphics context.</summary>
		/// <param name="window">The window to draw the pitmap on.<see cref="System.IntPtr"/></param>
		/// <param name="gc">The crapchics context to use for drawing.<see cref="System.IntPtr"/></param>
		/// <param name="destX">The x coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		/// <param name="destY">The y coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		void Draw (IntPtr window, IntPtr gc, TInt destX, TInt destY);

		#endregion Methods
	}

}

