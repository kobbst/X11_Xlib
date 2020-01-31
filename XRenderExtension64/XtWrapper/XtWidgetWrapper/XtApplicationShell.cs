using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
	
	/// <summary> Prototype the SetStatusLabel event. </summary>
	/// <param name="label"> The label to set to the status bar. <see cref="System.String"/> </param>
	public delegate void SetStatusLabelDelegate (string label);

	/// <summary> Base class for application shells. </summary>
	public class XtApplicationShell : XtWmShell
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public new const string		CLASS_NAME = "XtApplicationShell";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Static attributes
		
		/// <summary> The only application shell instance. </summary>
		private static XtApplicationShell _instance = null;
		
        #endregion

		#region Attributes
		
		/// <summary> The application's context. </summary>
		protected IntPtr			_appContext			= IntPtr.Zero;
		
		/// <summary> The application resource name. </summary>
		private string				_appResourceName	= "XtApplication";
		
		/// <summary> The list of associated shells, that interact with the window manager. </summary>
		protected List<XtWmShell>	_associatedShells	= new List<XtWmShell> ();
		
		/// <summary> Path to the application icon. </summary>
		public string				IconPath			= "";

        #endregion

		// ###############################################################################
        // ### E V E N T S
        // ###############################################################################

		#region Events
		
		/// <summary> Register the SetStatusLabel event handler. </summary>
		public event SetStatusLabelDelegate SetStatusLabel;
		
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary> Default constructor. </summary>
		[ObsoleteAttribute("Use parametrized constructor instead!")]
		private XtApplicationShell ()
		{	;	}
		
		/// <summary> Initializing constructor. </summary>
		/// <param name="appResourceName"> The ressource name of the application. <see cref="System.String"/> </param>
		/// <param name="shellResourceName"> The ressource name of the root shell. <see cref="System.String"/> </param>
		/// <param name="fallbackResources"> The fallback ressources to apply. <see cref="Xt.XtFallbackRessources"/> </param>
		public XtApplicationShell (string appResourceName, string shellResourceName, Xt.XtFallbackRessources fallbackResources)
		{
			if (_instance != null)
			{
				Console.WriteLine (CLASS_NAME + "::ctr () ERROR: Already an instance created.");
			}

			_appResourceName = appResourceName;
			
			IntPtr			argv = (string.IsNullOrEmpty (shellResourceName) ? IntPtr.Zero : (new XtStringArray (shellResourceName, '|')).Data);
			TInt 			argc = (TInt)(argv != IntPtr.Zero ? 1 : 0);
			_shell	= Xtlib.XtAppInitialize (ref _appContext, _appResourceName, IntPtr.Zero, 0, ref argc,
			                                 argv, fallbackResources.Marshal (), IntPtr.Zero, 0);
			_instance = this;
		}
		
		#endregion
		
        // ###############################################################################
        // ### D E S T R U C T I O N
        // ###############################################################################

        #region Destruction
		
		/// <summary> IDisposable implementation. </summary>
		public override void Dispose ()
		{
			Console.WriteLine (CLASS_NAME + "::Dispose ()");
			
			this.DisposeByParent ();
		}

		/// <summary> Dispose by parent. </summary>
		public override void DisposeByParent ()
		{	
			// Dispose new ressources.
			UnregisterConfigureNotifyAction ();
			UnregisterDeleteWindowAction ();

			// Dispose base ressources.
			base.DisposeByParent ();
		}
		
        #endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################
		
		#region Static properties
		
		/// <summary> Get the only application shell instance. </summary>
		public static XtApplicationShell Instance
		{	get	{ return _instance;	}	}
		
        #endregion
		
		#region Properties

		/// <summary> Get the shell's resource name. </summary>
		public string SHELL_RESOURCE_NAME
		{	get	{	return _appResourceName;	}	}

		/// <summary> Get the application content. </summary>
		public IntPtr AppContext
		{	get	{	return _appContext;	}	}		
		
		/// <summary> Get the list of associated shells, that interact with the window manager. </summary>
		public List<XtWmShell> AssociatedShells
		{	get	{	return _associatedShells;	}	}
		
        #endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary> Alloc a color from the default colormap of the display's default screen. </summary>
		/// <param name="colorname"> The color to alloc. <see cref="System.String"/> </param>
		/// <returns> The color pixel on success, or the white pixel otherwise. <see cref="X11.TColor"/> </returns>
		public TColor AllocColorFromDefaultColormap (string colorname)
		{
			IntPtr display = Xtlib.XtDisplay (_shell);
			if (display == IntPtr.Zero)
				return TColor.FallbackWhite;
			
			TInt   scrnID  = X11lib.XDefaultScreen (display);
			
			return X11lib.XAllocClosestNamedColor (display, X11lib.XDefaultColormap (display, scrnID), colorname);
		}
		
		/// <summary> Process the SetStatusLabel event handler. </summary>
		/// <param name="label"> The label to set to the status bar. <see cref="System.String"/> </param>
		public void OnSetStatusLabel (string label)
		{
			SetStatusLabelDelegate setStatusLabel = SetStatusLabel;
			if (setStatusLabel != null)
				setStatusLabel (label);
		}
		
		/// <summary> Run the shell. </summary>
		public void Run ()
		{
			if (_appContext == IntPtr.Zero)
			{
				Console.WriteLine (CLASS_NAME + "::Run () ERROR: Application context pointer is zero.");
			    return;
			}
			if (_shell == IntPtr.Zero)
			{
				Console.WriteLine (CLASS_NAME + "::Run () ERROR: Application shell pointer is zero.");
			    return;
			}

			try
			{
				// Register a  "configure notify action" to application context, translate the "configure notify action", add/overwrite
				// the shell widget's translation table and set windows manager protocol hook for the shell widget.
				// This must be can *** BEFORE *** XtRealizeWidget ().
				RegisterConfigureNotifyAction (_appContext, this.ConfigureNotifyAction);
				
				Xtlib.XtRealizeWidget (_shell);
				
				SetShellIcon (IconPath);
				
				// Register a "delete window action" to application context, translate the "delete window action", add/overwrite
				// the shell widget's translation table and set windows manager protocol hook for the shell widget.
				// This must be done *** AFTER *** XtRealizeWidget ().
				RegisterDeleteWindowAction (_appContext, this.DeleteWindowAction);

				Xtlib.XtAppMainLoop (_appContext);
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
			}
		}
		
		/// <summary> The prototype of shell specific close. </summary>
		/// <remarks> Application shels may implement the application exit, all other shells may implement popdown. </remarks>
		public override void Close ()
		{
			Console.WriteLine (CLASS_NAME + "::Close ()");
		}
		
		#endregion

		#region .NET message handler

		/// <summary> Implement the HandleDialogEnd event handler. </summary>
		/// <param name="label"> The label to set to the status bar. <see cref="System.String"/> </param>
		protected void HandleDlgDialogEnd_QueryForApplicationClose (XtDialog source)
		{
			if (source.Result == XtDialog.DialogResult.Ok)
			{
				Xtlib.exit(0);
			}
		}

		#endregion
		
		#region Actions
		
		/// <summary> Define the "delete window action", invoked by the windows manager on window's decoration close [X] button. </summary>
		/// <param name="widget"> The widget, that is source of the event. Typically the the shell's toplevel widget. <see cref="System.IntPtr"/> </param>
		/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
		/// <param name="parameters"> Additional parameters (as String[]). Not used for WM_DELETE_WINDOW. <see cref="System.IntPtr"/> </param>
		/// <param name="num_params"> The number of additional parameters. 0 for WM_DELETE_WINDOW. <see cref="XCardinal"/> </param>
		/// <remarks> The prototype must match the XtActionProc delegate. </remarks>
		public void DeleteWindowAction (IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal num_params)
		{
			Console.WriteLine (CLASS_NAME + "::DeleteWindowAction()");
			this.Close ();
		}
		
		#endregion

	}
}