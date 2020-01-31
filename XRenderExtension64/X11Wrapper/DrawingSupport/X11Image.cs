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
	
	/// <summary>The windowless base class to provide (color and transparent) 'writable picture' bitmap image.</summary>
	public class X11Image : X11IPicture
	{
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary>The class name constant.</summary>
        public const string	CLASS_NAME = "X11Image";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The surface, rpresenting this image.</summary>
		protected X11Surface	_imageSurface	= null;
		
		/// <summary>The width/height assigned.</summary>
		/// <remarks>Declared as reference type (X11.CSize) instead of a value type (System.Drawing.Size) to enable a derived class to incorporate
		/// a size into another class attribute, even if the size has already been defined in classe's base class as dedicated attribute.</remarks>
		protected CSize			_size = new CSize (0, 0);
		
		#endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="colormap">The X11 colormap pointer.<see cref="IntPtr"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="System.Int32"/></para>
		/// <param name="width">The width of the image to create.<see cref="System.Int32"/></param>
		/// <param name="height">The height of the image to create.<see cref="System.Int32"/></param>
		public X11Image (IntPtr display, int screenNumber, IntPtr colormap, int graphicDepth, int width, int height)
		{
			if (display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (colormap == IntPtr.Zero)
				throw new ArgumentNullException ("colormap");
			
			IntPtr rootWindow   = X11lib.XRootWindow    (display, (X11.TInt)screenNumber);
			IntPtr rootVisual   = X11lib.XDefaultVisual (display, (X11.TInt)screenNumber);
			if (rootVisual == IntPtr.Zero)
				throw new NullReferenceException ("rootVisual");
			IntPtr imageXPixmap = X11lib.XCreatePixmap(display, rootWindow, (X11.TUint)width, (X11.TUint)height, (X11.TUint)graphicDepth);
			if (imageXPixmap == IntPtr.Zero)
				throw new NullReferenceException ("imageXPixmap");
			
			_size.Width = width;
			_size.Height = height;
			_imageSurface = new X11Surface (display, screenNumber, imageXPixmap, rootVisual, graphicDepth, colormap);
		}

        #endregion Construction
		
        // ###############################################################################
        // ### D E S T R U C T I O N
        // ###############################################################################

        #region Destruction
		
		/// <summary>IDisposable implementation.</summary>
		public void Dispose ()
		{
			// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::Dispose ()");
			
			if (_imageSurface.Drawable != IntPtr.Zero)
			{
				X11lib.XFreePixmap (_imageSurface.Display, _imageSurface.Drawable);
				_imageSurface.SetDrawable (IntPtr.Zero);
			}
		}
		
		#endregion Destruction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
	
		/// <summary>Get the width.</summary>
		public int Width
		{ get {	return _size.Width;	} }
	
		/// <summary>Get the height.</summary>
		public int Height
		{ get {	return _size.Height;	} }
		
		/// <summary>Get the bitmap size.</summary>
		public System.Drawing.Size Size
		{
			get	{	return new System.Drawing.Size (_size.Width, _size.Height);	}
		}
		
		/// <summary>Get the depth (number of planes) of the graphic.</summary>
		public int GraphicDepth
		{
			get { return _imageSurface.Depth; }
		}
		
		/// <summary>Get the X11 image pixmap.</summary>
		public IntPtr ImagePixmap
		{
			get { return _imageSurface.Drawable; }
		}
		
		/// <summary>The surface, rpresenting this image.</summary>
		public X11Surface Surface
		{
			get { return _imageSurface; }
		}

		#endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary>Get the image as System.Drawing.Bitmap.</summary>
		/// <returns>The image as bitmap on success, or null otherwise.<see cref="System.Drawing.Bitmap"/></returns>
		public System.Drawing.Bitmap GetBitmap ()
		{
			if (_imageSurface == null || _imageSurface.Drawable == IntPtr.Zero)
				return null;
			
			IntPtr image = X11lib.XGetImage (_imageSurface.Display, _imageSurface.Drawable,
			                                 0, 0, (X11.TUint)_size.Width, (X11.TUint)_size.Height,
			                                 (X11.TUlong)(UInt32.MaxValue), (X11.TInt)2);
			if (image == IntPtr.Zero)
				return null;
				
			System.Drawing.Bitmap bmp   = new System.Drawing.Bitmap (_size.Width, _size.Height);
			System.Drawing.Color  color = System.Drawing.Color.Black;
			for (int scanLine = 0; scanLine < _size.Height; scanLine++)
			{
				for (int scanCol = 0; scanCol < _size.Width; scanCol++)
				{
					X11.TPixel pixel = X11lib.XGetPixel (image, (X11.TInt)scanCol, (X11.TInt)scanLine);
					if (_imageSurface.Depth >= 24)
						color = System.Drawing.Color.FromArgb ((int)pixel);
					else
						color = System.Drawing.Color.FromArgb (_imageSurface.RgbForColor (pixel));
					bmp.SetPixel (scanCol, scanLine, color);
				}
			}
			
			X11lib.XDestroyImage (image);
			return bmp;
		}
		
		/// <summary>Draw the image in the indicated window, using the indicated graphics context.</summary>
		/// <param name="window">The window to draw the pitmap on.<see cref="System.IntPtr"/></param>
		/// <param name="gc">The crapchics context to use for drawing.<see cref="System.IntPtr"/></param>
		/// <param name="destX">The x coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		/// <param name="destY">The y coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		public void Draw (IntPtr window, IntPtr gc, TInt destX, TInt destY)
		{
			X11lib.XCopyArea (_imageSurface.Display, _imageSurface.Drawable, window, gc,
			                  (X11.TInt)0, (X11.TInt)0, (X11.TUint)_size.Width, (X11.TUint)_size.Height, destX, destY);
		}

		#endregion Methods
		
	}

}

