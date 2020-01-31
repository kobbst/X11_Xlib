// =====================
// The "Roma Widget Set"
// =====================

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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace X11
{
	// Change color depth:
	// [Ctrl]+[Alt]+[F1] (change to ascii terminal)
	// init 3 (inits run level 3 and stops X)
	// edit /etc/X11/xorg.conf.d/50-screen.conf by adding: DefaultDepth    16 (http://www.unixboard.de/vb3/showthread.php?48739-OpenSuse-12-1-Xorg-Konfiguration-xorg-conf)
	// init 5 (inits run level 5 and starts X)
	
	/// <summary>The windowless base class to provide (color and transparent) 'fix picture' bitmap graphic.</summary>
	public class X11Graphic : X11IPicture
	{

		/// <summary>Predefined images for icons.</summary>
		public enum StockTheme
		{
			/// <summary>Looks like Win95.</summary>
			WinClassic = 0,
			
			/// <summary>Looks like WinXP/Office2007.</summary>
			WinLuna = 1,
			
			/// <summary>Looks like Vista/Win7/Office2010.</summary>
			WinRoyale = 2,
			
			/// <summary>Looks like Win8/Office2013.</summary>
			WinMidori = 3,
			
			/// <summary>Looks like XGnome 2.30.0.</summary>
			Gtk2
		}

		/// <summary>Predefined images for icons.</summary>
		public enum StockIcon
		{
			/// <summary>No bitmap.</summary>
			None,
			
			/// <summary>Abort icon, 16 x 16 pixel @ 32k color.</summary>
			Abort16,
			
			/// <summary>Abort icon, 24 x 24 pixel @ 32k color.</summary>
			Abort24,
			
			/// <summary>Abort icon, 32 x 32 pixel @ 32k color.</summary>
			Abort32,
			
			/// <summary>Abort icon, 48 x 48 pixel @ 32k color.</summary>
			Abort48,
			
			/// <summary>Ribbon (Win7/Office2010) application button icon, 16 x 16 pixel @ 32k color.</summary>
			AppButton16,
			
			/// <summary>Arrow down icon, 16 x 16 pixel @ 32k color.</summary>
			ArrowDown16,
			
			/// <summary>Arrow left icon, 16 x 16 pixel @ 32k color.</summary>
			ArrowLeft16,
			
			/// <summary>Arrow right icon, 16 x 16 pixel @ 32k color.</summary>
			ArrowRight16,
			
			/// <summary>Arrow up icon, 16 x 16 pixel @ 32k color.</summary>
			ArrowUp16,
			
			/// <summary>Attention icon, 16 x 16 pixel @ 32k color.</summary>
			Attention16,
			
			/// <summary>Attention icon, 24 x 24 pixel @ 32k color.</summary>
			Attention24,
			
			/// <summary>Attention icon, 32 x 32 pixel @ 32k color.</summary>
			Attention32,
			
			/// <summary>Attention icon, 48 x 48 pixel @ 32k color.</summary>
			Attention48,
			
			/// <summary>Bottom icon, 16 x 16 pixel @ 32k color.</summary>
			Bottom16,
			
			/// <summary>Bottom icon, 24 x 24 pixel @ 32k color.</summary>
			Bottom24,
			
			/// <summary>Bottom icon, 32 x 32 pixel @ 32k color.</summary>
			Bottom32,
			
			/// <summary>Bottom icon, 48 x 48 pixel @ 32k color.</summary>
			Bottom48,
			
			/// <summary>Cancel icon, 16 x 16 pixel @ 32k color.</summary>
			Cancel16,
			
			/// <summary>Cancel icon, 24 x 24 pixel @ 32k color.</summary>
			Cancel24,
			
			/// <summary>Cancel icon, 32 x 32 pixel @ 32k color.</summary>
			Cancel32,
			
			/// <summary>Cancel icon, 48 x 48 pixel @ 32k color.</summary>
			Cancel48,
			
			/// <summary>Copy icon, 16 x 16 pixel @ 32k color.</summary>
			Copy16,
			
			/// <summary>Copy icon, 24 x 24 pixel @ 32k color.</summary>
			Copy24,
			
			/// <summary>Copy icon, 32 x 32 pixel @ 32k color.</summary>
			Copy32,
			
			/// <summary>Copy icon, 48 x 48 pixel @ 32k color.</summary>
			Copy48,
			
			/// <summary>Cut icon, 16 x 16 pixel @ 32k color.</summary>
			Cut16,
			
			/// <summary>Cut icon, 24 x 24 pixel @ 32k color.</summary>
			Cut24,
			
			/// <summary>Cut icon, 32 x 32 pixel @ 32k color.</summary>
			Cut32,
			
			/// <summary>Cut icon, 48 x 48 pixel @ 32k color.</summary>
			Cut48,
			
			/// <summary>Computer icon, 16 x 16 pixel @ 32k color.</summary>
			Computer16,
			
			/// <summary>Computer icon, 24 x 24 pixel @ 32k color.</summary>
			Computer24,
			
			/// <summary>Computer icon, 32 x 32 pixel @ 32k color.</summary>
			Computer32,
			
			/// <summary>Computer icon, 48 x 48 pixel @ 32k color.</summary>
			Computer48,
			
			/// <summary>Down icon, 16 x 16 pixel @ 32k color.</summary>
			Down16,
			
			/// <summary>Down icon, 24 x 24 pixel @ 32k color.</summary>
			Down24,
			
			/// <summary>Down icon, 32 x 32 pixel @ 32k color.</summary>
			Down32,
			
			/// <summary>Down icon, 48 x 48 pixel @ 32k color.</summary>
			Down48,
			
			/// <summary>Drive icon, 16 x 16 pixel @ 32k color.</summary>
			Drive16,
			
			/// <summary>Drive icon, 24 x 24 pixel @ 32k color.</summary>
			Drive24,
			
			/// <summary>Drive icon, 32 x 32 pixel @ 32k color.</summary>
			Drive32,
			
			/// <summary>Drive icon, 48 x 48 pixel @ 32k color.</summary>
			Drive48,
			
			/// <summary>Error icon, 16 x 16 pixel @ 32k color.</summary>
			Error16,
			
			/// <summary>Error icon, 24 x 24 pixel @ 32k color.</summary>
			Error24,
			
			/// <summary>Error icon, 32 x 32 pixel @ 32k color.</summary>
			Error32,
			
			/// <summary>Error icon, 48 x 48 pixel @ 32k color.</summary>
			Error48,
			
			/// <summary>Generic file icon, 16 x 16 pixel @ 32k color.</summary>
			FileGeneric16,
			
			/// <summary>Generic file icon, 24 x 24 pixel @ 32k color.</summary>
			FileGeneric24,
			
			/// <summary>Generic file icon, 32 x 32 pixel @ 32k color.</summary>
			FileGeneric32,
			
			/// <summary>Generic file icon, 48 x 48 pixel @ 32k color.</summary>
			FileGeneric48,			
			
			/// <summary>Folder close icon, 16 x 16 pixel @ 32k color.</summary>
			FolderClose16,
			
			/// <summary>Folder close icon, 24 x 24 pixel @ 32k color.</summary>
			FolderClose24,
			
			/// <summary>Folder close icon, 32 x 32 pixel @ 32k color.</summary>
			FolderClose32,
			
			/// <summary>Folder close icon, 48 x 48 pixel @ 32k color.</summary>
			FolderClose48,
			
			/// <summary>Folder open icon, 16 x 16 pixel @ 32k color.</summary>
			FolderOpen16,
			
			/// <summary>Folder open icon, 24 x 24 pixel @ 32k color.</summary>
			FolderOpen24,
			
			/// <summary>Folder open icon, 32 x 32 pixel @ 32k color.</summary>
			FolderOpen32,
			
			/// <summary>Folder open icon, 48 x 48 pixel @ 32k color.</summary>
			FolderOpen48,
			
			/// <summary>Bitmap font icon, 16 x 16 pixel @ 32k color.</summary>
			FontBitmapFont16,
			
			/// <summary>Bitmap font icon, 24 x 24 pixel @ 32k color.</summary>
			FontBitmapFont24,
			
			/// <summary>Bitmap font icon, 32 x 32 pixel @ 32k color.</summary>
			FontBitmapFont32,
			
			/// <summary>Bitmap font icon, 48 x 48 pixel @ 32k color.</summary>
			FontBitmapFont48,
			
			/// <summary>Open font icon, 16 x 16 pixel @ 32k color.</summary>
			FontOpenFont16,
			
			/// <summary>Open font icon, 24 x 24 pixel @ 32k color.</summary>
			FontOpenFont24,
			
			/// <summary>Open font icon, 32 x 32 pixel @ 32k color.</summary>
			FontOpenFont32,
			
			/// <summary>Open font icon, 48 x 48 pixel @ 32k color.</summary>
			FontOpenFont48,
			
			/// <summary>Truetype font icon, 16 x 16 pixel @ 32k color.</summary>
			FontTrueType16,
			
			/// <summary>Truetype font icon, 24 x 24 pixel @ 32k color.</summary>
			FontTrueType24,
			
			/// <summary>Truetype font icon, 32 x 32 pixel @ 32k color.</summary>
			FontTrueType32,
			
			/// <summary>Truetype font icon, 48 x 48 pixel @ 32k color.</summary>
			FontTrueType48,
			
			/// <summary>Ignore icon, 16 x 16 pixel @ 32k color.</summary>
			Ignore16,
			
			/// <summary>Ignore icon, 24 x 24 pixel @ 32k color.</summary>
			Ignore24,
			
			/// <summary>Ignore icon, 32 x 32 pixel @ 32k color.</summary>
			Ignore32,
			
			/// <summary>Ignore icon, 48 x 48 pixel @ 32k color.</summary>
			Ignore48,
			
			/// <summary>Information icon, 16 x 16 pixel @ 32k color.</summary>
			Information16,
			
			/// <summary>Information icon, 24 x 24 pixel @ 32k color.</summary>
			Information24,
			
			/// <summary>Information icon, 32 x 32 pixel @ 32k color.</summary>
			Information32,
			
			/// <summary>Information icon, 48 x 48 pixel @ 32k color.</summary>
			Information48,
			
			/// <summary>My folder icon, 16 x 16 pixel @ 32k color.</summary>
			MyFolder16,
			
			/// <summary>My folder icon, 24 x 24 pixel @ 32k color.</summary>
			MyFolder24,
			
			/// <summary>My folder icon, 32 x 32 pixel @ 32k color.</summary>
			MyFolder32,
			
			/// <summary>My folder icon, 48 x 48 pixel @ 32k color.</summary>
			MyFolder48,
			
			/// <summary>Network icon, 16 x 16 pixel @ 32k color.</summary>
			Network16,
			
			/// <summary>Network icon, 24 x 24 pixel @ 32k color.</summary>
			Network24,
			
			/// <summary>Network icon, 32 x 32 pixel @ 32k color.</summary>
			Network32,
			
			/// <summary>Network icon, 48 x 48 pixel @ 32k color.</summary>
			Network48,
			
			/// <summary>No icon, 16 x 16 pixel @ 32k color.</summary>
			No16,
			
			/// <summary>No icon, 24 x 24 pixel @ 32k color.</summary>
			No24,
			
			/// <summary>No icon, 32 x 32 pixel @ 32k color.</summary>
			No32,
			
			/// <summary>No icon, 48 x 48 pixel @ 32k color.</summary>
			No48,
			
			/// <summary>Question icon, 16 x 16 pixel @ 32k color.</summary>
			Ok16,
			
			/// <summary>Question icon, 16 x 16 pixel @ 32k color.</summary>
			Ok24,
			
			/// <summary>Question icon, 16 x 16 pixel @ 32k color.</summary>
			Ok32,
			
			/// <summary>Question icon, 16 x 16 pixel @ 32k color.</summary>
			Ok48,
			
			/// <summary>Paste icon, 16 x 16 pixel @ 32k color.</summary>
			Paste16,
			
			/// <summary>Paste icon, 16 x 16 pixel @ 32k color.</summary>
			Paste24,
			
			/// <summary>Paste icon, 16 x 16 pixel @ 32k color.</summary>
			Paste32,
			
			/// <summary>Paste icon, 16 x 16 pixel @ 32k color.</summary>
			Paste48,
			
			/// <summary>Pipette icon, 16 x 16 pixel @ 32k color.</summary>
			Pipette16,
			
			/// <summary>Pipette icon, 16 x 16 pixel @ 32k color.</summary>
			Pipette24,
			
			/// <summary>Pipette icon, 16 x 16 pixel @ 32k color.</summary>
			Pipette32,
			
			/// <summary>Pipette icon, 16 x 16 pixel @ 32k color.</summary>
			Pipette48,
			
			/// <summary>Retry icon, 16 x 16 pixel @ 32k color.</summary>
			Retry16,
			
			/// <summary>Retry icon, 16 x 16 pixel @ 32k color.</summary>
			Retry24,
			
			/// <summary>Retry icon, 16 x 16 pixel @ 32k color.</summary>
			Retry32,
			
			/// <summary>Retry icon, 16 x 16 pixel @ 32k color.</summary>
			Retry48,
			
			/// <summary>Close application nemu icon for ribbin's application menu.</summary>
			RibbonClose,
			
			/// <summary>Exit application icon for ribbin's application menu.</summary>
			RibbonExit,
			
			/// <summary>New file icon for ribbin's application menu.</summary>
			RibbonNew,
			
			/// <summary>Open file icon for ribbin's application menu.</summary>
			RibbonOpen,
			
			/// <summary>Application options icon for ribbin's application menu.</summary>
			RibbonOptions,
			
			/// <summary>Save file icon for ribbin's application menu.</summary>
			RibbonSave,
			
			/// <summary>Save file as icon for ribbin's application menu.</summary>
			RibbonSaveAs,
			
			/// <summary>Text vertical align bottom icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignBottom,
			
			/// <summary>Text horizontal align left icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignCenter,
			
			/// <summary>Text horizontal align justify icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignJustify,
			
			/// <summary>Text horizontal align left icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignLeft,
			
			/// <summary>Text vertical align middle icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignMiddle,
			
			/// <summary>Text horizontal align left icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignRight,
			
			/// <summary>Text vertical align top icon, 16 x 16 pixel @ 2/32k color.</summary>
			TextAlignTop,
			
			/// <summary>Text bold top icon, 16 x 16 pixel @ 32k color.</summary>
			TextBold,
			
			/// <summary>Text italic top icon, 16 x 16 pixel @ 32k color.</summary>
			TextItalic,
			
			/// <summary>Text sub positioned top icon, 16 x 16 pixel @ 32k color.</summary>
			TextSub,
			
			/// <summary>Text super positioned top icon, 16 x 16 pixel @ 32k color.</summary>
			TextSuper,
			
			/// <summary>Text underline top icon, 16 x 16 pixel @ 32k color.</summary>
			TextUnderline,
			
			/// <summary>Question icon, 16 x 16 pixel @ 32k color.</summary>
			Question16,
			
			/// <summary>Question icon, 24 x 24 pixel @ 32k color.</summary>
			Question24,
			
			/// <summary>Question icon, 32 x 32 pixel @ 32k color.</summary>
			Question32,
			
			/// <summary>Question icon, 48 x 48 pixel @ 32k color.</summary>
			Question48,
			
			/// <summary>Toggle off icon, 16 x 16 pixel @ 32k color.</summary>
			ToggleOff16,
			
			/// <summary>Toggle on icon, 16 x 16 pixel @ 32k color.</summary>
			ToggleOn16,
			
			/// <summary>Toggle unset icon, 16 x 16 pixel @ 32k color.</summary>
			ToggleUnset16,
			
			/// <summary>Radio off icon, 16 x 16 pixel @ 32k color.</summary>
			RadioOff16,
			
			/// <summary>Radio on icon, 16 x 16 pixel @ 32k color.</summary>
			RadioOn16,
			
			/// <summary>Top icon, 16 x 16 pixel @ 32k color.</summary>
			Top16,
			
			/// <summary>Top icon, 24 x 24 pixel @ 32k color.</summary>
			Top24,
			
			/// <summary>Top icon, 32 x 32 pixel @ 32k color.</summary>
			Top32,
			
			/// <summary>Top icon, 48 x 48 pixel @ 32k color.</summary>
			Top48,
			
			/// <summary>Up icon, 16 x 16 pixel @ 32k color.</summary>
			Up16,
			
			/// <summary>Up icon, 24 x 24 pixel @ 32k color.</summary>
			Up24,
			
			/// <summary>Up icon, 32 x 32 pixel @ 32k color.</summary>
			Up32,
			
			/// <summary>Up icon, 48 x 48 pixel @ 32k color.</summary>
			Up48,
			
			/// <summary>Warning icon, 16 x 16 pixel @ 32k color.</summary>
			Warning16,
			
			/// <summary>Warning icon, 24 x 24 pixel @ 32k color.</summary>
			Warning24,
			
			/// <summary>Warning icon, 32 x 32 pixel @ 32k color.</summary>
			Warning32,
			
			/// <summary>Warning icon, 48 x 48 pixel @ 32k color.</summary>
			Warning48,
			
			/// <summary>Yes icon, 16 x 16 pixel @ 32k color.</summary>
			Yes16,
			
			/// <summary>Yes icon, 24 x 24 pixel @ 32k color.</summary>
			Yes24,
			
			/// <summary>Yes icon, 32 x 32 pixel @ 32k color.</summary>
			Yes32,
			
			/// <summary>Yes icon, 48 x 48 pixel @ 32k color.</summary>
			Yes48,
			
			/// <summary>ZoomIn icon, 16 x 16 pixel @ 32k color.</summary>
			ZoomIn16,
			
			/// <summary>ZoomIn icon, 24 x 24 pixel @ 32k color.</summary>
			ZoomIn24,
			
			/// <summary>ZoomIn icon, 32 x 32 pixel @ 32k color.</summary>
			ZoomIn32,
			
			/// <summary>ZoomIn icon, 48 x 48 pixel @ 32k color.</summary>
			ZoomIn48,
			
			/// <summary>ZoomOut icon, 16 x 16 pixel @ 32k color.</summary>
			ZoomOut16,
			
			/// <summary>ZoomOut icon, 24 x 24 pixel @ 32k color.</summary>
			ZoomOut24,
			
			/// <summary>ZoomOut icon, 32 x 32 pixel @ 32k color.</summary>
			ZoomOut32,
			
			/// <summary>ZoomOut icon, 48 x 48 pixel @ 32k color.</summary>
			ZoomOut48
		}
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary>The class name constant.</summary>
        public const string		CLASS_NAME = "XrwGraphic";

		/// <summary>Specifies the quantum of a scanline (8, 16, or 32). In other words, the start of one scanline is
		/// separated in client memory from the start of the next scanline by an integer multiple of this many bits.</summary>
		private const TInt      IMAGE_PADDING = (TInt)8; // Scanlines must start on word boundaries.
		
		/// <summary>Specifies the number of bytes in the client image between the start of one scanline and the start of the next.</summary>
		private const TInt      ASSUME_CONTIGUOUS = (TInt)0;
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The X11 display pointer.</summary>
		protected IntPtr		_display;
		
		/// <summary>The X11 screen number.</summary>
		protected int			_screenNumber;
		
		/// <summary>The X11 application individual colormap, if any.</summary>
		protected IntPtr		_individualColormap;
		
		/// <summary>The bitmap width.</summary>
		private int				_width			= 0;
		
		/// <summary>The bitmap height.</summary>
		private int				_height			= 0;
		
		/// <summary>The depth (number of planes) of the graphic - that holds color pixel information.</summary>
		private int 		    _graphicDepth	= 1;
		
		/// <summary>The depth (number of planes) of the clip mask - that holds binary information only.</summary>
		private int 		    _clipDepth		= 1;
		
		/// <summary>The X11 image of the graphic.</summary>
		private IntPtr			_graphicXImage	= IntPtr.Zero;
		
		/// <summary>The X11 image of the transparency mask.</summary>
		private IntPtr			_transpXImage	= IntPtr.Zero;
		
		/// <summary>The X11 pixmap of the transparency mask.</summary>
		private IntPtr			_transpXPixmap	= IntPtr.Zero;
		
        #endregion

		// ###############################################################################
        // ### S T A T I C   M E T H O D S
        // ###############################################################################

		#region Static methods
		
		/// <summary>Try to parse a stock icon.</summary>
		/// <param name="iconName">The name of the stock icon to parse.<see cref="System.String"/></param>
		/// <param name="icon">The parsed stock icon on success.<see cref="X11Graphic.StockIcon"/></param>
		/// <returns>True on success, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool TryParseStockIcon (string iconName, ref X11Graphic.StockIcon icon)
		{
			System.Array values = Enum.GetValues(typeof(X11Graphic.StockIcon));			
				
			for (int cntValues = 0; cntValues < values.Length; cntValues++)
			{
				if (values.GetValue(cntValues).ToString() == iconName)
				{
					icon = (X11Graphic.StockIcon)values.GetValue(cntValues);
					return true;
				}
			}
			return false;
		}
		
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="TInt"/></para>
		/// <param name="icon">The bitmap to create.<see cref="XrwBitmap.Icon"/></param>
		public X11Graphic (IntPtr display, int screenNumber, TInt graphicDepth, StockIcon icon)
			: this (display, screenNumber, IntPtr.Zero, graphicDepth, icon, StockTheme.Gtk2)
		{
			;//StockTheme
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="individualColormap">The X11 application individual colormap, if any.<see cref="IntPtr"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="TInt"/></para>
		/// <param name="icon">The bitmap to create.<see cref="XrwBitmap.Icon"/></param>
		public X11Graphic (IntPtr display, int screenNumber, IntPtr individualColormap, TInt graphicDepth, StockIcon icon)
			: this (display, screenNumber, individualColormap, graphicDepth, icon, StockTheme.Gtk2)
		{
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="individualColormap">The X11 application individual colormap, if any.<see cref="IntPtr"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="TInt"/></para>
		/// <param name="icon">The bitmap to create.<see cref="XrwBitmap.Icon"/></param>
		/// <param name="theme">The theme to use (if stock icon is themed).<see cref="XrwBitmap.Icon"/></param>
		public X11Graphic (IntPtr display, int screenNumber, IntPtr individualColormap, TInt graphicDepth, StockIcon icon, StockTheme theme)
		{
			// Check arguments.
			if (display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (icon == X11Graphic.StockIcon.None)
				throw new ArgumentNullException ("icon");
			
			_display = display;
			_screenNumber = screenNumber;
			_individualColormap = individualColormap;
			_graphicDepth = (int)graphicDepth;

			// Load bitmap.
			Bitmap bmp = null;
			try
			{
				System.IO.Stream bmpStream = null;
				
				if (icon == X11Graphic.StockIcon.Abort16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ABORT_16, false);
				else if (icon == X11Graphic.StockIcon.Abort24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ABORT_24, false);
				else if (icon == X11Graphic.StockIcon.Abort32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ABORT_32, false);
				else if (icon == X11Graphic.StockIcon.Abort48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ABORT_48, false);
				
				else if (icon == X11Graphic.StockIcon.AppButton16)
				{
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_APPBUTTON_16, false);
				}
				else if (icon == X11Graphic.StockIcon.ArrowDown16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWDOWN_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWDOWN_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ArrowLeft16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWLEFT_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWLEFT_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ArrowRight16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWRIGHT_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWRIGHT_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ArrowUp16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWUP_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ARROWUP_16_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Attention16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ATTENTION_16, false);
				else if (icon == X11Graphic.StockIcon.Attention24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ATTENTION_24, false);
				else if (icon == X11Graphic.StockIcon.Attention32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ATTENTION_32, false);
				else if (icon == X11Graphic.StockIcon.Attention48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ATTENTION_48, false);

				else if (icon == X11Graphic.StockIcon.Bottom16)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_16_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Bottom24)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_24_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Bottom32)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_32_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Bottom48)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_BOTTOM_48_GTK2, false);
				
				else if (icon == X11Graphic.StockIcon.Cancel16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CANCEL_16, false);
				else if (icon == X11Graphic.StockIcon.Cancel24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CANCEL_24, false);
				else if (icon == X11Graphic.StockIcon.Cancel32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CANCEL_32, false);
				else if (icon == X11Graphic.StockIcon.Cancel48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CANCEL_48, false);
				
				else if (icon == X11Graphic.StockIcon.Copy16)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_16_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Copy24)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_24_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Copy32)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_32_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Copy48)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COPY_48_GTK2, false);
				
				else if (icon == X11Graphic.StockIcon.Cut16)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_16_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Cut24)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_24_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Cut32)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_32_GTK2, false);
				else if (icon == X11Graphic.StockIcon.Cut48)
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_CUT_48_GTK2, false);
				
				else if (icon == X11Graphic.StockIcon.Computer16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Computer24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Computer32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Computer48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_COMPUTER_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Down16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Down24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Down32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Down48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DOWN_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Drive16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Drive24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Drive32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Drive48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_DRIVE_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Error16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ERROR_16, false);
				else if (icon == X11Graphic.StockIcon.Error24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ERROR_24, false);
				else if (icon == X11Graphic.StockIcon.Error32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ERROR_32, false);
				else if (icon == X11Graphic.StockIcon.Error48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ERROR_48, false);
				
				else if (icon == X11Graphic.StockIcon.FileGeneric16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FILEGENERIC_16, false);
				else if (icon == X11Graphic.StockIcon.FileGeneric24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FILEGENERIC_24, false);
				else if (icon == X11Graphic.StockIcon.FileGeneric32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FILEGENERIC_32, false);
				else if (icon == X11Graphic.StockIcon.FileGeneric48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FILEGENERIC_48, false);

				else if (icon == X11Graphic.StockIcon.FolderClose16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderClose24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderClose32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderClose48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDERCLOSE_48_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderOpen16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderOpen24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderOpen32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.FolderOpen48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FOLDEROPEN_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.FontBitmapFont16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTBITMAPFONT_16, false);
				else if (icon == X11Graphic.StockIcon.FontBitmapFont24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTBITMAPFONT_24, false);
				else if (icon == X11Graphic.StockIcon.FontBitmapFont32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTBITMAPFONT_32, false);
				else if (icon == X11Graphic.StockIcon.FontBitmapFont48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTBITMAPFONT_48, false);
					
				else if (icon == X11Graphic.StockIcon.FontOpenFont16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTOPENFONT_16, false);
				else if (icon == X11Graphic.StockIcon.FontOpenFont24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTOPENFONT_24, false);
				else if (icon == X11Graphic.StockIcon.FontOpenFont32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTOPENFONT_32, false);
				else if (icon == X11Graphic.StockIcon.FontOpenFont48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTOPENFONT_48, false);
					
				else if (icon == X11Graphic.StockIcon.FontTrueType16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTTRUETYPE_16, false);
				else if (icon == X11Graphic.StockIcon.FontTrueType24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTTRUETYPE_24, false);
				else if (icon == X11Graphic.StockIcon.FontTrueType32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTTRUETYPE_32, false);
				else if (icon == X11Graphic.StockIcon.FontTrueType48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_FONTTRUETYPE_48, false);
					
				else if (icon == X11Graphic.StockIcon.Ignore16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_IGNORE_16, false);
				else if (icon == X11Graphic.StockIcon.Ignore24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_IGNORE_24, false);
				else if (icon == X11Graphic.StockIcon.Ignore32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_IGNORE_32, false);
				else if (icon == X11Graphic.StockIcon.Ignore48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_IGNORE_48, false);
					
				else if (icon == X11Graphic.StockIcon.Information16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_INFO_16, false);
				else if (icon == X11Graphic.StockIcon.Information24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_INFO_24, false);
				else if (icon == X11Graphic.StockIcon.Information32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_INFO_32, false);
				else if (icon == X11Graphic.StockIcon.Information48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_INFO_48, false);
					
				else if (icon == X11Graphic.StockIcon.MyFolder16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.MyFolder24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.MyFolder32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.MyFolder48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_MYFOLDER_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Network16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Network24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Network32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Network48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NETWORK_48_GTK2, false);
				}
					
				else if (icon == X11Graphic.StockIcon.No16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NO_16, false);
				else if (icon == X11Graphic.StockIcon.No24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NO_24, false);
				else if (icon == X11Graphic.StockIcon.No32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NO_32, false);
				else if (icon == X11Graphic.StockIcon.No48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_NO_48, false);
				
				else if (icon == X11Graphic.StockIcon.Ok16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_OK_16, false);
				else if (icon == X11Graphic.StockIcon.Ok24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_OK_24, false);
				else if (icon == X11Graphic.StockIcon.Ok32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_OK_32, false);
				else if (icon == X11Graphic.StockIcon.Ok48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_OK_48, false);
				
				else if (icon == X11Graphic.StockIcon.Paste16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Paste24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Paste32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Paste48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PASTE_48_GTK2, false);
				}
					
				else if (icon == X11Graphic.StockIcon.Pipette16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PIPETTE_16, false);
				else if (icon == X11Graphic.StockIcon.Pipette24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PIPETTE_24, false);
				else if (icon == X11Graphic.StockIcon.Pipette32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PIPETTE_32, false);
				else if (icon == X11Graphic.StockIcon.Pipette48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_PIPETTE_48, false);
				
				else if (icon == X11Graphic.StockIcon.Retry16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RETRY_16, false);
				else if (icon == X11Graphic.StockIcon.Retry24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RETRY_24, false);
				else if (icon == X11Graphic.StockIcon.Retry32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RETRY_32, false);
				else if (icon == X11Graphic.StockIcon.Retry48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RETRY_48, false);
				
				else if (icon == X11Graphic.StockIcon.RibbonClose)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_CLOSE_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_CLOSE_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_CLOSE_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.RibbonExit)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_EXIT_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_EXIT_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_EXIT_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.RibbonNew)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_NEW_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_NEW_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_NEW_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.RibbonOpen)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_OPEN_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_OPEN_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_OPEN_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.RibbonOptions)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_OPTIONS_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_OPTIONS_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_OPTIONS_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.RibbonSave)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_SAVE_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_SAVE_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_SAVE_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.RibbonSaveAs)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_SAVEAS_32_WIN, false);
					else if (theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_SAVEAS_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RIBBON_SAVEAS_32_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.TextAlignBottom)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNBOTTOM_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNBOTTOM_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextAlignCenter)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNCENTER_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNCENTER_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextAlignJustify)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNJUSTIFY_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNJUSTIFY_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextAlignLeft)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNLEFT_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNLEFT_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextAlignMiddle)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNMIDDLE_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNMIDDLE_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextAlignRight)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNRIGHT_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNRIGHT_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextAlignTop)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNTOP_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTALIGNTOP_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextBold)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATBOLD_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATBOLD_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextItalic)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATITALIC_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATITALIC_16_WIN, false);
				}
				else if (icon == X11Graphic.StockIcon.TextSub)
				{
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATSUB_16, false);
				}
				else if (icon == X11Graphic.StockIcon.TextSuper)
				{
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATSUPER_16, false);
				}
				else if (icon == X11Graphic.StockIcon.TextUnderline)
				{
					if (theme == StockTheme.Gtk2)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATUNDERLINE_16_GTK2, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TEXTFORMATUNDERLINE_16_WIN, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Top16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Top24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Top32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Top48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOP_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Question16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_QUESTION_16, false);
				else if (icon == X11Graphic.StockIcon.Question24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_QUESTION_24, false);
				else if (icon == X11Graphic.StockIcon.Question32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_QUESTION_32, false);
				else if (icon == X11Graphic.StockIcon.Question48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_QUESTION_48, false);

				else if (icon == X11Graphic.StockIcon.ToggleOff16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOGGLEOFF_16, false);
				else if (icon == X11Graphic.StockIcon.ToggleOn16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOGGLEON_16, false);
				else if (icon == X11Graphic.StockIcon.ToggleUnset16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_TOGGLEUNSET_16, false);
				else if (icon == X11Graphic.StockIcon.RadioOff16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RADIOOFF_16, false);
				else if (icon == X11Graphic.StockIcon.RadioOn16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_RADIOON_16, false);
				
				else if (icon == X11Graphic.StockIcon.Up16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Up24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Up32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.Up48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_UP_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.Warning16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_WARNING_16, false);
				else if (icon == X11Graphic.StockIcon.Warning24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_WARNING_24, false);
				else if (icon == X11Graphic.StockIcon.Warning32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_WARNING_32, false);
				else if (icon == X11Graphic.StockIcon.Warning48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_WARNING_48, false);
				
				else if (icon == X11Graphic.StockIcon.Yes16)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_YES_16, false);
				else if (icon == X11Graphic.StockIcon.Yes24)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_YES_24, false);
				else if (icon == X11Graphic.StockIcon.Yes32)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_YES_32, false);
				else if (icon == X11Graphic.StockIcon.Yes48)
					bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_YES_48, false);
				
				else if (icon == X11Graphic.StockIcon.ZoomIn16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ZoomIn24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ZoomIn32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ZoomIn48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMIN_48_GTK2, false);
				}
				
				else if (icon == X11Graphic.StockIcon.ZoomOut16)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_16_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_16_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ZoomOut24)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_24_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_24_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ZoomOut32)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_32_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_32_GTK2, false);
				}
				else if (icon == X11Graphic.StockIcon.ZoomOut48)
				{
					if (theme == StockTheme.WinClassic || theme == StockTheme.WinLuna || theme == StockTheme.WinRoyale)
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_48_WIN, false);
					else
						bmpStream = new System.IO.MemoryStream (X11.Properties.Resources.IMAGE_ZOOMOUT_48_GTK2, false);
				}
				
				bmp = new Bitmap (bmpStream);
				bmpStream.Close ();
			}
			catch (Exception e)
			{
				throw e;
			}
			if (bmp == null)
				throw new OperationCanceledException ("Failed to create bitmap from '" + icon.ToString() + "'.");
			
			InitializeGraphicRessources (bmp);
		}
		
		/// <summary>Initializing constructor.</summary>
		/// <param name="x11display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="individualColormap">The X11 application individual colormap, if any.<see cref="IntPtr"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="TInt"/></para>
		/// <param name="filename">The path of the graphic file.<see cref="System.String"/></param>
		public X11Graphic (IntPtr x11display, int screenNumber, IntPtr individualColormap, TInt graphicDepth, string filename)
		{
			// Check arguments.
			if (x11display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (string.IsNullOrEmpty (filename))
				throw new ArgumentNullException ("filename");
			
			if (!System.IO.File.Exists (filename))
			{
				SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME + "::ctr () Can not find graphic file '" + filename + "'!");
				
				string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
				assemblyLocation = System.IO.Path.GetDirectoryName (assemblyLocation);
				filename = System.IO.Path.Combine (assemblyLocation, filename);
				if (!System.IO.File.Exists (filename))
				{
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::ctr () Can not find graphic file '" + filename + "'!");
					throw new ArgumentException ("File not found.", "filename");
				}
			}
			
			_display = x11display;
			_screenNumber = screenNumber;
			_individualColormap = individualColormap;
			_graphicDepth = (int)graphicDepth;

			// Load bitmap.
			Bitmap bmp = null;
			try
			{
				bmp = new Bitmap (filename);
			}
			catch (Exception e)
			{
				throw e;
			}
			if (bmp == null)
				throw new OperationCanceledException ("Failed to create bitmap from '" + filename + "'.");
			
			InitializeGraphicRessources (bmp);
		}
	
		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="individualColormap">The X11 application individual colormap, if any.<see cref="IntPtr"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="TInt"/></para>
		/// <param name="bmpStream">The stream of bitmap bytes.<see cref="System.IO.Stream"/></param>
		public X11Graphic (IntPtr display, int screenNumber, IntPtr individualColormap, TInt graphicDepth, System.IO.Stream bmpStream)
		{
			// Check arguments.
			if (display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (bmpStream == null)
				throw new ArgumentNullException ("bmpStream");
			
			_display = display;
			_screenNumber = screenNumber;
			_individualColormap = individualColormap;
			_graphicDepth = (int)graphicDepth;

			// Load bitmap.
			Bitmap bmp = null;
			try
			{
				bmp = new Bitmap (bmpStream);
			}
			catch (Exception e)
			{
				throw e;
			}
			if (bmp == null)
				throw new OperationCanceledException ("Failed to create bitmap from stream.");
			
			InitializeGraphicRessources (bmp);
		}
	
		/// <summary>Initializing constructor.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="screenNumber">The appropriate screen number on the host server.<see cref="System.Int32"/></param>
		/// <param name="individualColormap">The X11 application individual colormap, if any.<see cref="IntPtr"/></param>
		/// <param name="graphicDepth">The depth (number of planes) of the graphic - that holds color pixel information.<see cref="TInt"/></para>
		/// <param name="bitmap">The bitmap.<see cref="System.Drawing.Bitmap"/></param>
		public X11Graphic (IntPtr display, int screenNumber, IntPtr individualColormap, TInt graphicDepth, System.Drawing.Bitmap bitmap)
		{
			// Check arguments.
			if (display == IntPtr.Zero)
				throw new ArgumentNullException ("display");
			if (bitmap == null)
				throw new ArgumentNullException ("bitmap");
			
			_display = display;
			_screenNumber = screenNumber;
			_individualColormap = individualColormap;
			_graphicDepth = (int)graphicDepth;

			InitializeGraphicRessources (bitmap);
		}
			
		/// <summary>Initialize local ressources for all constructors.</summary>
		/// <param name="bmp">The bitmap associated with this graphic.<see cref="Bitmap"/></param>
		private void InitializeGraphicRessources (Bitmap bmp)
		{
			IntPtr visual   = X11lib.XDefaultVisual (_display, (TInt)_screenNumber);
			if (visual == IntPtr.Zero)
				throw new OperationCanceledException ("Failed to investigate default visual.");

			_width        = bmp.Width;
			_height       = bmp.Height;
			
			// Prepare bitmap conversion.
			int		colorPixelBytes		= 4;
			int     maskLineBytes		= (int)((_width + (int)IMAGE_PADDING - 1) / (int)IMAGE_PADDING);
			
			if (_graphicDepth > 16)
				colorPixelBytes   = 4;
			else if (_graphicDepth > 8)
				colorPixelBytes   = 2;
			else
				colorPixelBytes   = 1;

			// ### Allocate bitmap conversion data.
			// The bitmap color data. Use a temporary managed byte array for speed.
			byte[]	graphicData			= new byte[_height * _width * colorPixelBytes];
			// The bitmap transparency data. Use a temporary managed byte array for speed.
			byte[]	maskData			= new byte[_height * maskLineBytes];
			// Quick access to current line's color pixel.
			int		graphicPixelIndex	= 0;
			// Quick access to current line's mask pixel.
			int		maskPixelIndex		= 0;
			// Reduce slow calls to bmp.GetPixel (x,y).
			Color   pixelColor			= Color.Black;
			// Determine whether transparency is required.
			bool	transparency		= false;
			
			// See: http://bbdock.nethence.com/download/bbdock-0.2.9/src/Render.cc
			// for RGBA to pixmap converter for different color depth (32, 24, 16) and ghosted image.
			
			if (colorPixelBytes == 4)
			{
				// ### Convert bitmap on a true color system.
				for (int y = 0; y < _height; y++)
				{
					for (int x = 0; x < _width; x++)
					{
						graphicPixelIndex = (y * _width + x) * colorPixelBytes;
						maskPixelIndex    = y * maskLineBytes + (x >> 3);
						pixelColor = bmp.GetPixel (x,y);
						
						graphicData[graphicPixelIndex + 0] = pixelColor.B; // B
						graphicData[graphicPixelIndex + 1] = pixelColor.G; // G
						graphicData[graphicPixelIndex + 2] = pixelColor.R; // R
						graphicData[graphicPixelIndex + 3] = pixelColor.A; // A
							
						if (pixelColor. B == 255 && pixelColor.G == 255 && pixelColor.R == 255)
						{
							byte summand = (byte)(1<<(x % 8));
							maskData[maskPixelIndex] = (byte)(maskData[maskPixelIndex] + summand);
							transparency             = true;
						}
					}
				}
			}
			else
			{
				// ### Convert bitmap on a hi color system.
				// Get the screen ID to determine the colormap.
				TInt	scrnID				= X11lib.XDefaultScreen (_display);
				// Get the colormap to determine dedicated color (pixel) values).
				IntPtr	colormap			= (_individualColormap != IntPtr.Zero ? _individualColormap : X11lib.XDefaultColormap (_display, scrnID));
				// Prepare a pixel (color entry).
				TPixel  pixel				= 0;

				for (int y = 0; y < _height; y++)
				{
					for (int x = 0; x < _width; x++)
					{
						graphicPixelIndex = (y * _width + x) * colorPixelBytes;
						maskPixelIndex    = y * maskLineBytes + (x >> 3);
						pixelColor = bmp.GetPixel (x,y);
						
						string color = string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}", pixelColor.R, pixelColor.G, pixelColor.B);
						bool   exact = false;
						pixel = X11lib.XAllocClosestNamedColor (_display, colormap, color, ref exact);
						
						if (!exact)
						{
							X11lib.XColor xColor = new X11lib.XColor ();
							xColor.pixel = pixel;
							X11lib.XQueryColor (_display, colormap, ref xColor);
							SimpleLog.LogLine (TraceEventType.Warning, CLASS_NAME + "::InitializeGraphicRessources () " +
							                    "The color map entries have been running out! (" + color + " is set to " + string.Format ("#{0,2:X2}{1,2:X2}{2,2:X2}", xColor.red, xColor.green, xColor.blue) + ")");
						}
						
						if (colorPixelBytes == 2)
						{
							short LWORD = (short)((int)pixel);
						
							graphicData[graphicPixelIndex + 0] = (byte)(LWORD);
							graphicData[graphicPixelIndex + 1] = (byte)(LWORD / 256);
						}
						else
						{
							graphicData[graphicPixelIndex] = (byte)((int)pixel);
						}
						
						if (pixelColor. B == 255 && pixelColor.G == 255 && pixelColor.R == 255)
						{
							byte summand = (byte)(1<<(x % 8));
							maskData[maskPixelIndex] = (byte)(maskData[maskPixelIndex] + summand);
							transparency               = true;
						}
					}
				}
			}
			
			// ### Create XImage.
			// The bitmap color data.
			IntPtr	graphicDataHandle = Marshal.AllocHGlobal(graphicData.Length);
			// Allocate not movable memory.
			Marshal.Copy (graphicData, 0, graphicDataHandle, graphicData.Length);
			// Client side XImage storage.
			_graphicXImage = X11lib.XCreateImage (_display, visual, (TUint)_graphicDepth,
			                                      X11lib.TImageFormat.ZPixmap, (TInt)0,
			                                      graphicDataHandle, (TUint)_width, (TUint)_height,
			                                      IMAGE_PADDING, ASSUME_CONTIGUOUS);
			if (_graphicXImage == IntPtr.Zero)
				throw new OperationCanceledException ("Image creation for graphic failed.");
			
			if (transparency == true)
			{
				IntPtr	maskDataHandle = Marshal.AllocHGlobal(maskData.Length);				// The bitmap bitmap transparency data.
				Marshal.Copy (maskData, 0, maskDataHandle, maskData.Length);				// Allocate not movable memory.
				// Client side storage.
				_transpXImage = X11lib.XCreateImage (_display, visual, (X11.TUint)_clipDepth, X11lib.TImageFormat.XYBitmap, (X11.TInt)0,
				                                   maskDataHandle, (X11.TUint)_width, (X11.TUint)_height, IMAGE_PADDING, ASSUME_CONTIGUOUS);
				if (_transpXImage == IntPtr.Zero)
					throw new OperationCanceledException ("Image creation for transparency mask failed.");
			}
		}
		
        #endregion
		
        // ###############################################################################
        // ### D E S T R U C T I O N
        // ###############################################################################

        #region Destruction
		
		/// <summary>IDisposable implementation.</summary>
		public void Dispose ()
		{
			// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::Dispose ()");
			
			if (_graphicXImage != IntPtr.Zero)
			{
				// Note: The destroy procedure (_XImage.f.destroy_image), that this macro calls,
				// frees both - the image structure and the data pointed to by the image structure.
				X11lib.XDestroyImage (_graphicXImage);
				_graphicXImage = IntPtr.Zero;
			}
			if (_transpXImage != IntPtr.Zero)
			{
				// Note: The destroy procedure (_XImage.f.destroy_image), that this macro calls,
				// frees both - the image structure and the data pointed to by the image structure.
				X11lib.XDestroyImage (_transpXImage);
				_transpXImage = IntPtr.Zero;
			}
			if (_transpXPixmap != IntPtr.Zero)
			{
				X11lib.XFreePixmap  (_display, _transpXPixmap);
				_transpXPixmap = IntPtr.Zero;
			}
		}
		
		#endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary>Get the bitmap width.</summary>
		public int Width
		{
			get { return _width; }
		}
		
		/// <summary>Get the bitmap height.</summary>
		public int Height
		{
			get { return _height; }
		}
		
		/// <summary>Get the bitmap size.</summary>
		public System.Drawing.Size Size
		{
			get	{	return new System.Drawing.Size (_width, _height);	}
		}
		
		/// <summary>Get the X11 graphic image.</summary>
		public IntPtr GraphicImage
		{
			get { return _graphicXImage; }
		}
		
		/// <summary>Get the X11 Mask image. Can be null.</summary>
		public IntPtr MaskImage
		{
			get { return _transpXImage; }
		}
		
		/// <summary>Get the depth (number of planes) of the graphic.</summary>
		public int GraphicDepth
		{
			get { return _graphicDepth; }
		}

		#endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary>Draw the image in the indicated window, using the indicated graphics context.</summary>
		/// <param name="window">The window to draw the pitmap on.<see cref="System.IntPtr"/></param>
		/// <param name="gc">The crapchics context to use for drawing.<see cref="System.IntPtr"/></param>
		/// <param name="destX">The x coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		/// <param name="destY">The y coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		private void DrawUnfinished (IntPtr window, IntPtr gc, TInt dstX, TInt dstY)
		{
			// This is an alternative drawing approach, but i didn't get it working.
			IntPtr clipPixmap = X11lib.XCreatePixmap (_display, window, (TUint)_width, (TUint)_height, (TUint)_clipDepth);
			IntPtr clipGc     = X11lib.XCreateGC (_display, clipPixmap, (TUlong)0, IntPtr.Zero);
			X11lib.XPutImage    (_display, clipPixmap, clipGc, _transpXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)_width, (TUint)_height);
			
			X11lib.XSetFunction (_display, gc, X11lib.XGCFunction.GXand);
			X11lib.XSetBackground (_display, gc, (TPixel)1);
			X11lib.XSetForeground (_display, gc, (TPixel)0);
			
			X11lib.XCopyPlane (_display, clipPixmap, window, gc, (TInt)0, (TInt)0, (TUint)_width, (TUint)_height, (TInt)dstX, (TInt)dstY, (TUlong)1);
		}
		
		/// <summary>Provide a pixmap, containing the intransparent graphic, that can be used independent from this class.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="window">The target window to create the pixmap for.<see cref="IntPtr"/></param>
		/// <returns>The (server side) pixmap (that must be feed) on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		public IntPtr CreateIndependentGraphicPixmap (IntPtr display, IntPtr window)
		{
			if (_graphicXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			IntPtr pixmap	= X11lib.XCreatePixmap (display, window, (TUint)_width, (TUint)_height, (TUint)_graphicDepth);
			IntPtr pixmapGc	= X11lib.XCreateGC (display, pixmap, (TUlong)0, IntPtr.Zero);
			if (pixmapGc != IntPtr.Zero)
			{
				X11lib.XPutImage (display, pixmap, pixmapGc, _graphicXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)_width, (TUint)_height);
				// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::CreateIndependentGraphicPixmap () Delete graphic image GC.");
				X11lib.XFreeGC   (display, pixmapGc);
				pixmapGc = IntPtr.Zero;
			}	
			else
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CreateIndependentGraphicPixmap() Can not create graphics context for graphic pixmap.");
			}
			// Use XSetWMHints, XtSetValues(), XCopyArea() / XCopyPlane() or XSetStipple() / XSetTile() to apply th pixmap.
			return pixmap;
		}
		
		/// <summary>Provide a pixmap zoomed by the zoom, containing the intransparent graphic, that can be used independent from this class.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="window">The target window to create the pixmap for.<see cref="IntPtr"/></param>
		/// <param name="zoom">The zoom factor, to apply to the pixmap.A <see cref="System.Double"/></param>
		/// <returns>The (server side) pixmap (that must be feed) on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		public IntPtr CreateIndependentGraphicPixmapZoomed (IntPtr display, IntPtr window, double zoom)
		{
			// See here for further ideas:
			// http://www.drdobbs.com/architecture-and-design/fast-bitmap-rotation-and-scaling/184416337
			
			if (_graphicXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			if (zoom <= 0.0)
				return IntPtr.Zero;
				
			IntPtr visual   = X11lib.XDefaultVisual (_display, (TInt)_screenNumber);
			if (visual == IntPtr.Zero)
				throw new OperationCanceledException ("Failed to investigate default visual.");

			int zoomWidth  = (int)(_width  * zoom + 0.49);
			int zoomHeight = (int)(_height * zoom + 0.49);
			
			// Determine the number of bytes per pixel.
			int		colorPixelBytes		= 4;
			if (_graphicDepth > 16)
				colorPixelBytes   = 4;
			else if (_graphicDepth > 8)
				colorPixelBytes   = 2;
			else
				colorPixelBytes   = 1;

			// Allocate bitmap conversion data.
			// -- The bitmap color data. Use a temporary managed byte array for speed.
			byte[]	graphicData				= new byte[zoomHeight * zoomWidth * colorPixelBytes];
			// -- Quick access to current line's color pixel.
			int		graphicPixelIndex	= 0;
			// -- Reduce slow calls to X11lib.XGetPixel (image, x, y).
			X11.TPixel   pixelColorA			= (X11.TPixel)0;
			X11.TPixel   pixelColorB			= (X11.TPixel)0;
			X11.TPixel   pixelColorC			= (X11.TPixel)0;
			X11.TPixel   pixelColorD			= (X11.TPixel)0;
			// -- Switch bilinear color smoothing.
			bool   smooth     = true;
			
			if (colorPixelBytes == 4)
			{
				// ### Convert bitmap on a true color system.
				for (int zoomY = 0; zoomY < zoomHeight; zoomY++)
				{
					for (int zoomX = 0; zoomX < zoomWidth; zoomX++)
					{
						graphicPixelIndex = (zoomY * zoomWidth + zoomX) * colorPixelBytes;
						
						// Calculate backward: Source pixel antialiasing coordinates from target pixel coordinates.
		                double srcXaa = zoomX / zoom;
		                double srcYaa = zoomY / zoom;
						// Calculate source pixel best matching precise coordinates.
						//int srcXpr = (int) (srcXaa + (srcXaa < 0.0 ? 0.0 : 0.49)); // ((int) -1.51D) goes to -2I
		                //int srcYpr = (int) (srcYaa + (srcYaa < 0.0 ? 0.0 : 0.49)); // ((int)  1.51D) goes to +1I
						int srcXpr = (int) srcXaa;
		                int srcYpr = (int) srcYaa;
		 
		                if(srcXpr < _width && srcXpr >= 0 && srcYpr < _height && srcYpr >= 0)
		                {
							if (smooth == false)
							{
								pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr, (X11.TInt)srcYpr);
								
								graphicData[graphicPixelIndex + 0] = (byte)((int)((pixelColorA & (X11.TPixel)(0x000000FF)))      ); // B
								graphicData[graphicPixelIndex + 1] = (byte)((int)((pixelColorA & (X11.TPixel)(0x0000FF00))) >>  8); // G
								graphicData[graphicPixelIndex + 2] = (byte)((int)((pixelColorA & (X11.TPixel)(0x00FF0000))) >> 16); // R
								graphicData[graphicPixelIndex + 3] = (byte)((int)((pixelColorA & (X11.TPixel)(0xFF000000))) >> 24); // A
							}
							else
							{
								// Calculate bilinear color smoothing coefficient.
				                double smoothX    = Math.Abs(srcXaa - srcXpr);
				                double smoothY    = Math.Abs(srcYaa - srcYpr);
								int    beighbourX = (srcXaa < srcXpr && srcXpr > 0 ? -1 : (srcXaa > srcXpr && srcXpr < _width  - 1 ? +1 : 0));
								int    beighbourY = (srcYaa < srcYpr && srcYpr > 0 ? -1 : (srcYaa > srcYpr && srcYpr < _height - 1 ? +1 : 0));

								pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,                (X11.TInt)srcYpr      );
								pixelColorB = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr + beighbourX), (X11.TInt)srcYpr      );
								pixelColorC = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,                (X11.TInt)(srcYpr + beighbourY));
								pixelColorD = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr + beighbourX), (X11.TInt)(srcYpr + beighbourY));
								
								byte bA = (byte)((int)((pixelColorA & (X11.TPixel)(0x000000FF)))      ); // B
								byte gA = (byte)((int)((pixelColorA & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rA = (byte)((int)((pixelColorA & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aA = (byte)((int)((pixelColorA & (X11.TPixel)(0xFF000000))) >> 24); // A
								byte bB = (byte)((int)((pixelColorB & (X11.TPixel)(0x000000FF)))      ); // B
								byte gB = (byte)((int)((pixelColorB & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rB = (byte)((int)((pixelColorB & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aB = (byte)((int)((pixelColorB & (X11.TPixel)(0xFF000000))) >> 24); // A
								byte bC = (byte)((int)((pixelColorC & (X11.TPixel)(0x000000FF)))      ); // B
								byte gC = (byte)((int)((pixelColorC & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rC = (byte)((int)((pixelColorC & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aC = (byte)((int)((pixelColorC & (X11.TPixel)(0xFF000000))) >> 24); // A
								byte bD = (byte)((int)((pixelColorD & (X11.TPixel)(0x000000FF)))      ); // B
								byte gD = (byte)((int)((pixelColorD & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rD = (byte)((int)((pixelColorD & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aD = (byte)((int)((pixelColorD & (X11.TPixel)(0xFF000000))) >> 24); // A
								
								double b  = bA * (1 - smoothX) * (1 - smoothY) + bB * smoothX * (1 - smoothY) + bC * (1 - smoothX) * smoothY + bD * smoothX * smoothY;
			                    double g  = gA * (1 - smoothX) * (1 - smoothY) + gB * smoothX * (1 - smoothY) + gC * (1 - smoothX) * smoothY + gD * smoothX * smoothY;
			                    double r  = rA * (1 - smoothX) * (1 - smoothY) + rB * smoothX * (1 - smoothY) + rC * (1 - smoothX) * smoothY + rD * smoothX * smoothY;
			                    double a  = aA * (1 - smoothX) * (1 - smoothY) + aB * smoothX * (1 - smoothY) + aC * (1 - smoothX) * smoothY + aD * smoothX * smoothY;
								graphicData[graphicPixelIndex + 0] = (byte)Math.Max(0, Math.Min(255, (int)b));
								graphicData[graphicPixelIndex + 1] = (byte)Math.Max(0, Math.Min(255, (int)g));
								graphicData[graphicPixelIndex + 2] = (byte)Math.Max(0, Math.Min(255, (int)r));
								graphicData[graphicPixelIndex + 3] = (byte)Math.Max(0, Math.Min(255, (int)a));
							}
						}
						else
						{
							graphicData[graphicPixelIndex + 0] = (byte)255; // B
							graphicData[graphicPixelIndex + 1] = (byte)255; // G
							graphicData[graphicPixelIndex + 2] = (byte)255; // R
							graphicData[graphicPixelIndex + 3] = (byte)255; // A
						}
					}
				}
			}
			
			// ### Create XImage.
			// The bitmap color data.
			IntPtr	graphicDataHandle = Marshal.AllocHGlobal(graphicData.Length);
			// Allocate not movable memory.
			Marshal.Copy (graphicData, 0, graphicDataHandle, graphicData.Length);
			// Client side XImage storage.
			System.IntPtr graphicXImage = X11lib.XCreateImage (_display, visual, (TUint)_graphicDepth,
			                                                   X11lib.TImageFormat.ZPixmap, (TInt)0,
			                                                   graphicDataHandle, (TUint)zoomWidth, (TUint)zoomHeight,
			                                                   IMAGE_PADDING, ASSUME_CONTIGUOUS);
			
			if (graphicXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			IntPtr pixmap	= X11lib.XCreatePixmap (display, window, (TUint)zoomWidth, (TUint)zoomHeight, (TUint)_graphicDepth);
			IntPtr pixmapGc	= X11lib.XCreateGC (display, pixmap, (TUlong)0, IntPtr.Zero);
			
			// Draw image onto 'pixmap'.
			if (pixmapGc != IntPtr.Zero)
			{
				X11lib.XPutImage (display, pixmap, pixmapGc, graphicXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)zoomWidth, (TUint)zoomHeight);
				// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::CreateIndependentGraphicPixmap () Delete graphic image GC.");
				X11lib.XFreeGC   (display, pixmapGc);
				pixmapGc = IntPtr.Zero;
			}	
			else
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CreateIndependentGraphicPixmap() Can not create graphics context for graphic pixmap.");
			}
			// Use XSetWMHints, XtSetValues(), XCopyArea() / XCopyPlane() or XSetStipple() / XSetTile() to apply th pixmap.
			return pixmap;
		}
		
		/// <summary>Provide a pixmap rotated by the angle, containing the intransparent graphic, that can be used independent from this class.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="window">The target window to create the pixmap for.<see cref="IntPtr"/></param>
		/// <param name="angle">The rotation angle, in radiants counter-clockwise, to apply to the pixmap.A <see cref="System.Double"/></param>
		/// <returns>The (server side) pixmap (that must be feed) on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		public IntPtr CreateIndependentGraphicPixmapRotated (IntPtr display, IntPtr window, double angle)
		{
			// See here for further ideas:
			// http://www.drdobbs.com/architecture-and-design/fast-bitmap-rotation-and-scaling/184416337
			
			if (_graphicXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			IntPtr visual   = X11lib.XDefaultVisual (_display, (TInt)_screenNumber);
			if (visual == IntPtr.Zero)
				throw new OperationCanceledException ("Failed to investigate default visual.");

			// Compute the dimensions of the resulting bitmap.
			double cos = Math.Cos(-angle); // Later we calculate backward: Source pixel antialiasing coordinates from target pixel coordinates.
			double sin = Math.Sin(-angle); // That's why we calculate sin/cos from negative rotation angle.
			
			double rotX0  =  0;				// Left.
			double rotY0  =  0;				// Top.
			double rotX1  = -_height * sin;	// Left.
			double rotY1  =  _height * cos;	// Bottom.
			double rotX2  =  _width  * cos - _height * sin;
			double rotY2  =  _height * cos + _width  * sin;
			double rotX3  =  _width  * cos;	// Right.
			double rotY3  =  _width  * sin;	// Top.
			
			double rotMinX = Math.Min(rotX0, Math.Min(rotX1, Math.Min(rotX2, rotX3)));
			double rotMinY = Math.Min(rotY0, Math.Min(rotY1, Math.Min(rotY2, rotY3)));
			double rotMaxX = Math.Max(rotX0, Math.Max(rotX1, Math.Max(rotX2, rotX3)));
			double rotMaxY = Math.Max(rotX0, Math.Max(rotY1, Math.Max(rotY2, rotY3)));
			
			int rotWidth  = (int)(rotMaxX - rotMinX + 0.49);
			int rotHeight = (int)(rotMaxY - rotMinY + 0.49);
			
			// Determine the number of bytes per pixel.
			int		colorPixelBytes		= 4;
			if (_graphicDepth > 16)
				colorPixelBytes   = 4;
			else if (_graphicDepth > 8)
				colorPixelBytes   = 2;
			else
				colorPixelBytes   = 1;

			// Allocate bitmap conversion data.
			// -- The bitmap color data. Use a temporary managed byte array for speed.
			byte[]	graphicData				= new byte[rotHeight * rotWidth * colorPixelBytes];
			// -- Quick access to current line's color pixel.
			int		graphicPixelIndex	= 0;
			// -- Reduce slow calls to X11lib.XGetPixel (image, x, y).
			X11.TPixel   pixelColorA			= (X11.TPixel)0;
			X11.TPixel   pixelColorB			= (X11.TPixel)0;
			X11.TPixel   pixelColorC			= (X11.TPixel)0;
			X11.TPixel   pixelColorD			= (X11.TPixel)0;
			// -- Switch bilinear color smoothing.
			bool smooth = true;
			
			if (colorPixelBytes == 4)
			{
				// ### Convert bitmap on a true color system.
				for (int rotY = 0; rotY < rotHeight; rotY++)
				{
					for (int rotX = 0; rotX < rotWidth; rotX++)
					{
						graphicPixelIndex = (rotY * rotWidth + rotX) * colorPixelBytes;
						
						// Calculate backward: Source pixel antialiasing coordinates from target pixel coordinates.
		                double srcXaa =  (rotX - rotWidth / 2) * cos + (rotY - rotHeight / 2) * sin + _width  / 2;
		                double srcYaa = -(rotX - rotWidth / 2) * sin + (rotY - rotHeight / 2) * cos + _height / 2;
						// Calculate source pixel best matching precise coordinates.
						int srcXpr = (int) (srcXaa + (srcXaa < 0.0 ? -0.99 : 0.0));
		                int srcYpr = (int) (srcYaa + (srcYaa < 0.0 ? -0.99 : 0.0));
		 
		                if(srcXpr < _width && srcXpr >= 0 && srcYpr < _height && srcYpr >= 0)
		                {
							if (smooth == false)
							{
								pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr, (X11.TInt)srcYpr);
								
								graphicData[graphicPixelIndex + 0] = (byte)((int)((pixelColorA & (X11.TPixel)(0x000000FF)))      ); // B
								graphicData[graphicPixelIndex + 1] = (byte)((int)((pixelColorA & (X11.TPixel)(0x0000FF00))) >>  8); // G
								graphicData[graphicPixelIndex + 2] = (byte)((int)((pixelColorA & (X11.TPixel)(0x00FF0000))) >> 16); // R
								graphicData[graphicPixelIndex + 3] = (byte)((int)((pixelColorA & (X11.TPixel)(0xFF000000))) >> 24); // A
							}
							else
							{
								// Calculate bilinear color smoothing coefficient.
				                double smoothX = Math.Abs (srcXaa - srcXpr);
				                double smoothY = Math.Abs (srcYaa - srcYpr);

								if (srcXpr < _width / 2)
								{
									if (srcYpr < _height / 2)
									{
										pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)srcYpr      );
										pixelColorB = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr + 1), (X11.TInt)srcYpr      );
										pixelColorC = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)(srcYpr + 1));
										pixelColorD = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr + 1), (X11.TInt)(srcYpr + 1));
									}
									else // (srcY >= _height / 2)
									{
										pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)(srcYpr - 1));
										pixelColorB = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr + 1), (X11.TInt)(srcYpr - 1));
										pixelColorC = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)srcYpr      );
										pixelColorD = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr + 1), (X11.TInt)srcYpr      );
									}
								}
								else // (srcX >= _width / 2)
								{
									if (srcYpr < _height / 2)
									{
										pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr - 1), (X11.TInt)srcYpr      );
										pixelColorB = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)srcYpr      );
										pixelColorC = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr - 1), (X11.TInt)(srcYpr + 1));
										pixelColorD = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)(srcYpr + 1));
									}
									else // (srcY >= _height / 2)
									{
										pixelColorA = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr - 1), (X11.TInt)(srcYpr - 1));
										pixelColorB = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)(srcYpr - 1));
										pixelColorC = X11lib.XGetPixel (_graphicXImage, (X11.TInt)(srcXpr - 1), (X11.TInt)srcYpr      );
										pixelColorD = X11lib.XGetPixel (_graphicXImage, (X11.TInt)srcXpr,       (X11.TInt)srcYpr      );
									}
								}
								
								byte bA = (byte)((int)((pixelColorA & (X11.TPixel)(0x000000FF)))      ); // B
								byte gA = (byte)((int)((pixelColorA & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rA = (byte)((int)((pixelColorA & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aA = (byte)((int)((pixelColorA & (X11.TPixel)(0xFF000000))) >> 24); // A
								byte bB = (byte)((int)((pixelColorB & (X11.TPixel)(0x000000FF)))      ); // B
								byte gB = (byte)((int)((pixelColorB & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rB = (byte)((int)((pixelColorB & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aB = (byte)((int)((pixelColorB & (X11.TPixel)(0xFF000000))) >> 24); // A
								byte bC = (byte)((int)((pixelColorC & (X11.TPixel)(0x000000FF)))      ); // B
								byte gC = (byte)((int)((pixelColorC & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rC = (byte)((int)((pixelColorC & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aC = (byte)((int)((pixelColorC & (X11.TPixel)(0xFF000000))) >> 24); // A
								byte bD = (byte)((int)((pixelColorD & (X11.TPixel)(0x000000FF)))      ); // B
								byte gD = (byte)((int)((pixelColorD & (X11.TPixel)(0x0000FF00))) >>  8); // G
								byte rD = (byte)((int)((pixelColorD & (X11.TPixel)(0x00FF0000))) >> 16); // R
								byte aD = (byte)((int)((pixelColorD & (X11.TPixel)(0xFF000000))) >> 24); // A
								
			                    double b  = bA * (1 - smoothX) * (1 - smoothY) + bB * smoothX * (1 - smoothY) + bC * (1 - smoothX) * smoothY + bD * smoothX * smoothY;
			                    double g  = gA * (1 - smoothX) * (1 - smoothY) + gB * smoothX * (1 - smoothY) + gC * (1 - smoothX) * smoothY + gD * smoothX * smoothY;
			                    double r  = rA * (1 - smoothX) * (1 - smoothY) + rB * smoothX * (1 - smoothY) + rC * (1 - smoothX) * smoothY + rD * smoothX * smoothY;
			                    double a  = aA * (1 - smoothX) * (1 - smoothY) + aB * smoothX * (1 - smoothY) + aC * (1 - smoothX) * smoothY + aD * smoothX * smoothY;
								graphicData[graphicPixelIndex + 0] = (byte)Math.Max(0, Math.Min(255, (int)b));
								graphicData[graphicPixelIndex + 1] = (byte)Math.Max(0, Math.Min(255, (int)g));
								graphicData[graphicPixelIndex + 2] = (byte)Math.Max(0, Math.Min(255, (int)r));
								graphicData[graphicPixelIndex + 3] = (byte)Math.Max(0, Math.Min(255, (int)a));
							}
						}
						else
						{
							graphicData[graphicPixelIndex + 0] = (byte)255; // B
							graphicData[graphicPixelIndex + 1] = (byte)255; // G
							graphicData[graphicPixelIndex + 2] = (byte)255; // R
							graphicData[graphicPixelIndex + 3] = (byte)255; // A
						}
					}
				}
			}
			
			// ### Create XImage.
			// The bitmap color data.
			IntPtr	graphicDataHandle = Marshal.AllocHGlobal(graphicData.Length);
			// Allocate not movable memory.
			Marshal.Copy (graphicData, 0, graphicDataHandle, graphicData.Length);
			// Client side XImage storage.
			System.IntPtr graphicXImage = X11lib.XCreateImage (_display, visual, (TUint)_graphicDepth,
			                                                   X11lib.TImageFormat.ZPixmap, (TInt)0,
			                                                   graphicDataHandle, (TUint)rotWidth, (TUint)rotHeight,
			                                                   IMAGE_PADDING, ASSUME_CONTIGUOUS);
			
			if (graphicXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			IntPtr pixmap	= X11lib.XCreatePixmap (display, window, (TUint)rotWidth, (TUint)rotHeight, (TUint)_graphicDepth);
			IntPtr pixmapGc	= X11lib.XCreateGC (display, pixmap, (TUlong)0, IntPtr.Zero);
			
			// Draw image onto 'pixmap'.
			if (pixmapGc != IntPtr.Zero)
			{
				X11lib.XPutImage (display, pixmap, pixmapGc, graphicXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)rotWidth, (TUint)rotHeight);
				// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::CreateIndependentGraphicPixmap () Delete graphic image GC.");
				X11lib.XFreeGC   (display, pixmapGc);
				pixmapGc = IntPtr.Zero;
			}	
			else
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CreateIndependentGraphicPixmap() Can not create graphics context for graphic pixmap.");
			}
			// Use XSetWMHints, XtSetValues(), XCopyArea() / XCopyPlane() or XSetStipple() / XSetTile() to apply th pixmap.
			return pixmap;
		}
		
		/// <summary>Provide a pixmap, containing the transparency mask, that can be used independent from this class.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="window">The target window to create the pixmap for.<see cref="IntPtr"/></param>
		/// <returns>The (server side) pixmap (that must be feed) on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		public IntPtr CreateIndependentMaskPixmap (IntPtr display, IntPtr window)
		{
			if (_transpXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			IntPtr pixmap	= X11lib.XCreatePixmap (display, window, (TUint)_width, (TUint)_height, (TUint)_clipDepth);
			IntPtr pixmapGc	= X11lib.XCreateGC (display, pixmap, (TUlong)0, IntPtr.Zero);
			if (pixmapGc != IntPtr.Zero)
			{
				X11lib.XPutImage (display, pixmap, pixmapGc, _transpXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)_width, (TUint)_height);
				// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::CreateIndependentMaskPixmap () Delete transparency mask image GC.");
				X11lib.XFreeGC   (display, pixmapGc);
				pixmapGc = IntPtr.Zero;
			}	
			else
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CreateIndependentMaskPixmap() Can not create graphics context for mask pixmap.");
			}
			return pixmap;
		}
		
		/// <summary>Provide a bitmap, containing the transparent graphic, that can be used independent from this class.</summary>
		/// <param name="display">The display pointer, that specifies the connection to the X server.<see cref="System.IntPtr"/></param>
		/// <param name="window">The target window to create the pixmap for.<see cref="IntPtr"/></param>
		/// <param name="backgroundColorPixel">The background color behind any transparent pixel.<see cref="TPixel"/></param>
		/// <param name="maskPixmap">The mask pixmap to distinguish transparent from intransparent pixel.<see cref="IntPtr"/></param>
		/// <returns>The (server side) pixmap (that must be feed) on success, or IntPtr.Zero otherwise.<see cref="IntPtr"/></returns>
		public IntPtr CreateIndependentPixmap (IntPtr display, IntPtr window, TPixel backgroundColorPixel, IntPtr maskPixmap)
		{
			if (_graphicXImage == IntPtr.Zero)
				return IntPtr.Zero;
			
			IntPtr pixmap	= X11lib.XCreatePixmap (display, window, (TUint)_width, (TUint)_height, (TUint)_graphicDepth);
			
			// Fill pixmap with background color.
			IntPtr bgGc	= X11lib.XCreateGC (display, window, (TUlong)0, IntPtr.Zero);
			X11lib.XSetForeground (display, bgGc, backgroundColorPixel);
			X11lib.XFillRectangle (display, pixmap, bgGc, 0, 0, _width, _height);
			X11lib.XFreeGC (display, bgGc);
			bgGc = IntPtr.Zero;
			
			// Overlay the image.
			IntPtr pixmapGc	= X11lib.XCreateGC (display, window, (TUlong)0, IntPtr.Zero);
			if (pixmapGc != IntPtr.Zero)
			{
				if (maskPixmap != IntPtr.Zero)
				{
					// Prepare the clipping graphics context.
					IntPtr graphicGc     = X11lib.XCreateGC (display, window, (TUlong)0, IntPtr.Zero);
					if (graphicGc != IntPtr.Zero)
					{
						X11lib.XSetClipMask (display, graphicGc, maskPixmap);
						X11lib.XSetClipOrigin (display, graphicGc, (TInt)0, (TInt)0);
						
						// Draw graphic using the clipping graphics context.
						X11lib.XPutImage (display, pixmap, graphicGc, _graphicXImage, (TInt)0, (TInt)0,
						                  (TInt)0, (TInt)0, (TUint)_width, (TUint)_height);
						
						// Restore previous behaviour and clean up. 
						X11lib.XSetClipMask (display, graphicGc, IntPtr.Zero);
						X11lib.XSetClipOrigin (display, graphicGc, (TInt)0, (TInt)0);
						// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::Draw() Delete clipping image GC.");
						X11lib.XFreeGC (display, graphicGc);
						graphicGc = IntPtr.Zero;
					}
					else
					{
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::Draw() Can not create graphics context for transparency application.");
					}
				}
				else
				{
					X11lib.XPutImage (display, pixmap, pixmapGc, _graphicXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)_width, (TUint)_height);
					// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::CreateIndependentGraphicPixmap() Delete graphic image GC.");
					X11lib.XFreeGC   (display, pixmapGc);
					pixmapGc = IntPtr.Zero;
				}
			}	
			else
			{
				SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::CreateIndependentGraphicPixmap() Can not create graphics context for graphic pixmap.");
			}
			// Use XSetWMHints, XtSetValues(), XCopyArea() / XCopyPlane() or XSetStipple() / XSetTile() to apply th pixmap.
			return pixmap;
		}
		
		/// <summary>Draw the image in the indicated window, using the indicated graphics context.</summary>
		/// <param name="window">The window to draw the pitmap on.<see cref="System.IntPtr"/></param>
		/// <param name="gc">The crapchics context to use for drawing.<see cref="System.IntPtr"/></param>
		/// <param name="destX">The x coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		/// <param name="destY">The y coordinate, which is relative to the origin of the window and is the coordinate of the subimage.<see cref="TInt"/></param>
		public void Draw (IntPtr window, IntPtr gc, TInt destX, TInt destY)
		{
			// Possible optimization: Keep the graphicGc between the exposure events.
			// Problem: Every graphic allocates a graphics context (limited server resource).
			if (_transpXImage != IntPtr.Zero)
			{
				// Prepare the clip mask (for transparency) that must be a XPixmap.
				if (_transpXPixmap == IntPtr.Zero)
				{
					// Server side storage.
					_transpXPixmap = X11lib.XCreatePixmap (_display, window, (TUint)_width, (TUint)_height, (TUint)_clipDepth);
					IntPtr maskGc = X11lib.XCreateGC (_display, _transpXPixmap, (TUlong)0, IntPtr.Zero);
					if (maskGc != IntPtr.Zero)
					{
						X11lib.XPutImage    (_display, _transpXPixmap, maskGc, _transpXImage, (TInt)0, (TInt)0, (TInt)0, (TInt)0, (TUint)_width, (TUint)_height);
						// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::Draw() Delete transparency mask image GC.");
						X11lib.XFreeGC      (_display, maskGc);
						maskGc = IntPtr.Zero;
					}
					else
					{
						SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::Draw() Can not create graphics context for mask pixmap.");
					}
				}
				
				// Prepare the clipping graphics context.
				IntPtr graphicGc     = X11lib.XCreateGC (_display, window, (TUlong)0, IntPtr.Zero);
				if (graphicGc != IntPtr.Zero)
				{
					X11lib.XSetClipMask (_display, graphicGc, _transpXPixmap);
					X11lib.XSetClipOrigin (_display, graphicGc, destX, destY);
					
					// Draw graphic using the clipping graphics context.
					X11lib.XPutImage (_display, window, graphicGc, _graphicXImage, (TInt)0, (TInt)0, (TInt)destX, (TInt)destY, (TUint)_width, (TUint)_height);
					
					// Restore previous behaviour and clean up. 
					X11lib.XSetClipMask (_display, graphicGc, IntPtr.Zero);
					X11lib.XSetClipOrigin (_display, graphicGc, (TInt)0, (TInt)0);
					// SimpleLog.LogLine (TraceEventType.Verbose, CLASS_NAME + "::Draw() Delete clipping image GC.");
					X11lib.XFreeGC (_display, graphicGc);
					graphicGc = IntPtr.Zero;
				}
				else
				{
					SimpleLog.LogLine (TraceEventType.Error, CLASS_NAME + "::Draw() Can not create graphics context for transparency application.");
				}
			}
			else
				X11lib.XPutImage(_display, window, gc, _graphicXImage, 0, 0, destX, destY, (TUint)_width, (TUint)_height);
		}

		#endregion

	}
	
}

