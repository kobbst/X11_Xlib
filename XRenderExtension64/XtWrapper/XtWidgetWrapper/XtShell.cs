using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
	/// <summary> Fundametal calss for shell widgets with window manager interaction. </summary>
	public class XtShell : IDisposable
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "XtShell";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary> The widget pointer of the underlaying shell. </summary>
		protected IntPtr _shell = IntPtr.Zero;

		/// <summary> The server-side application icon pixmap. </summary>
		protected IntPtr _appIconPixMap	= IntPtr.Zero;
		
		/// <summary> The server-side application mask pixmap. </summary>
		protected IntPtr _appMaskPixMap	= IntPtr.Zero;

        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary> The default constructor. </summary>
		
		public XtShell ()
		{
		}
		
        #endregion
		
        // ###############################################################################
        // ### D E S T R U C T I O N
        // ###############################################################################

        #region Destruction
		
		/// <summary> IDisposable implementation. </summary>
		public virtual void Dispose ()
		{
			Console.WriteLine (CLASS_NAME + "::Dispose ()");
			
			this.DisposeByParent ();
		}

		/// <summary> Dispose by parent. </summary>
		public virtual void DisposeByParent ()
		{	
			// Memory and resources, that are assigned to underlaying Athena widget's
			// instance structure (and thereby owned there) are destroyed by XtDestroyWidget ().
			// This typically inclused the GCs, pixmaps and local copies of strings.
			// They are typically assigned during XtCreateWidget () or XtSetValues ().
			if (_shell != IntPtr.Zero)
			{
				IntPtr display = Xtlib.XtDisplay (_shell);

				if (_appIconPixMap != IntPtr.Zero)
					X11lib.XFreePixmap (display, _appIconPixMap);
			
				if (_appMaskPixMap != IntPtr.Zero)
					X11lib.XFreePixmap (display, _appMaskPixMap);

				Xtlib.XtDestroyWidget (_shell);
				_shell = IntPtr.Zero;
			}
		}
		
        #endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
				
		/// <summary> Get the widget pointer of the underlaying shell. </summary>
		public IntPtr Shell
		{	get	{	return _shell;	}	}		
		
        #endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods

		/// <summary> Load the icon from indicated path and set it as shell icon. </summary>
		/// <returns> <c>true</c>, if icon was set, <c>false</c> otherwise. </returns>
		/// <param name='iconPath'> The icon path. </param>
		public bool SetShellIcon (string iconPath)
		{
			bool result = false;
			
			if (_shell == IntPtr.Zero)
			{
				Console.WriteLine (CLASS_NAME + "::SetShellIcon() ERROR: Member attribute '_shell' null.");
				return result;
			}
			if (string.IsNullOrEmpty (iconPath))
			{
				Console.WriteLine (CLASS_NAME + "::SetShellIcon() ERROR: Paramerter 'iconPath' null or empty.");
				return result;
			}
			
			IntPtr display = Xtlib.XtDisplay (_shell);
			IntPtr window  = Xtlib.XtWindow  (_shell);
			TInt   screenNumber  = Xtlib.XDefaultScreen (display);
			using (X11Graphic appIcon			= new X11Graphic (display, (int)screenNumber, IntPtr.Zero, X11lib.XDefaultDepth (display, screenNumber), iconPath))
			{
				_appIconPixMap	= appIcon.CreateIndependentGraphicPixmap (display, window);
				_appMaskPixMap	= appIcon.CreateIndependentMaskPixmap    (display, window);
				if (_appIconPixMap != IntPtr.Zero && _appMaskPixMap != IntPtr.Zero)
				{
					X11lib.XWMHints wmHints = new X11lib.XWMHints();
					IntPtr          wmAddr  = X11lib.XAllocWMHints (ref wmHints);
					
					wmHints.flags		= X11lib.XWMHintMask.IconPixmapHint   |
										  X11lib.XWMHintMask.IconPositionHint |
										  X11lib.XWMHintMask.IconMaskHint;
				    wmHints.icon_pixmap = _appIconPixMap;
					wmHints.icon_mask   = _appMaskPixMap;
				    wmHints.icon_x      = 0;
				    wmHints.icon_y      = 0;
					
					X11lib.XSetWMHints (display, window, ref wmHints);
					X11lib.XFree (wmAddr);
					
					result = true;
				}
				else
					Console.WriteLine (CLASS_NAME + "::SetShellIcon () ERROR: Can not create application icon.");
			}

			return result;
		}
		
		/// <summary> The prototype of shell specific close. </summary>
		/// <remarks> Application shels may implement the application exit, all other shells may implement popdown. </remarks>
		public virtual void Close ()
		{	;	}
		
        #endregion

	}
}

