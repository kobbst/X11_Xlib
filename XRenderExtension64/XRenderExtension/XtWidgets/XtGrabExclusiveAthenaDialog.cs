using System;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
	/// <summary> Sample implementation of a typical X Toolkit Intrinsic exclusively grabbing dialog box based on dialog widget. </summary>
	public class XtGrabExclusiveAthenaDialog : XtDialog
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public new const string		CLASS_NAME				= "XtGrabExclusiveAthenaDialog";
		
		/// <summary> The shell widgets resource name. </summary>
		public const string			DIALOG_RESOURCE_NAME	= "AthenaDialogForm";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary> The initializing constructor. </summary>
		/// <param name="toplevelShell"> The toplevel shell that owns the underlaying transient popup shell of this dialog. <see cref="XtApplicationShell"/> </param>
		/// <param name="toplevelShell"> The shell caption. <see cref="System.String"/> </param>
		public XtGrabExclusiveAthenaDialog (XtApplicationShell toplevelShell, string caption)
			: base (toplevelShell, caption)
		{
			if (toplevelShell == null)
				throw new ArgumentNullException ("toplevelShell");
			
			// The dialog widget needs the final size of all child widgets during layout calculation to prevent child widget overlapping. Or in other words:
			// Child widget overlapping will happen, if XtNicon is set after dialog widget's layout calculation, because XtMakeResizeRequest () can't be called.
			
			// Currently the dialog widget isn't realized and therefor it hasn't a window. That's why we use the toplevel shell's window instead.
			IntPtr logo	= IntPtr.Zero;
			if (!(Xtlib.XtIsRealized (toplevelShell.Shell) == (TBoolean)0))
			{
				IntPtr	display = Xtlib.XtDisplay (toplevelShell.Shell);
				IntPtr	window  = Xtlib.XtWindow  (toplevelShell.Shell);
				logo			= X11lib.XCreateBitmapFromData (display, window, XtResources.BIG_EXCLAMATION_ICON_BITS,
		      													XtResources.BIG_ICON_WIDTH, XtResources.BIG_ICON_HEIGHT);
			}			
			Arg[] formArgs		= { new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray ("Enter data string:\0")),
									new Arg(XtNames.XtNvalue, X11.X11Utils.StringToSByteArray ("\0")),
									new Arg(XtNames.XtNicon, (XtArgVal)logo) };
			_dialogForm			= Xtlib.XtCreateManagedWidget(DIALOG_RESOURCE_NAME,
									Xtlib.XawDialogWidgetClass(), _shell,
									formArgs, (XCardinal)3);

			Arg[] okArgs		= {	new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray ("OK\0")) };
			_okCommand			= Xtlib.XtCreateManagedWidget(_okCmdName,
									Xtlib.XawCommandWidgetClass(), _dialogForm,
									okArgs, (XCardinal)1);
            Xtlib.XtAddCallback (_okCommand, XtNames.XtNcallback, CallBackMarshaler.Add (_okCommand, this.DialogOk), IntPtr.Zero);
			
			Arg[] cancelArgs	= {	new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray ("Cancel\0")) };
			_cancelCommand		= Xtlib.XtCreateManagedWidget(_cancelCmdName,
									Xtlib.XawCommandWidgetClass(), _dialogForm,
									cancelArgs, (XCardinal)1);
            Xtlib.XtAddCallback (_cancelCommand, XtNames.XtNcallback, CallBackMarshaler.Add (_cancelCommand, this.DialogCancel), IntPtr.Zero);
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
			CallBackMarshaler.Remove (_cancelCommand);
			CallBackMarshaler.Remove (_okCommand);
			UnregisterDeleteWindowAction ();

			base.DisposeByParent ();
		}

		#endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary> Run the shell. </summary>
		/// <param name="appContext"> The application context that identifies the application. <see cref="System.IntPtr"/> </param>
		/// <returns> Zero on success, or negative nonzero otherwise. <see cref="System.Int32"/> </returns>
		public int Run (IntPtr appContext)
		{
			if (appContext == IntPtr.Zero)
			{
				Console.WriteLine (CLASS_NAME + "::Run () ERROR: Application context pointer is zero.");
			    return -1;
			}
			
			try
			{
				bool firstRun = false;
				
				if (!(Xtlib.XtIsRealized (_shell) != 0))
				{
					firstRun = true;
				}
				
				Xtlib.XtPopup (_shell, XtGrabKind.XtGrabExclusive);
					
				if (firstRun && XtApplicationShell.Instance != null)
				{
					SetShellIcon (XtApplicationShell.Instance.IconPath);
				}
					
				// Register a "delete window action" to application context, translate the "delete window action", add/overwrite
				// the shell widget's translation table and set windows manager protocol hook for the shell widget.
				// This must be done *** AFTER *** XtRealizeWidget ().
				RegisterDeleteWindowAction (_toplevelShell.AppContext, this.DeleteWindowAction);
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
			}
			return 0;
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
			Console.WriteLine (CLASS_NAME + "::DeleteWindowAction() For Atthena dialog.");
			this.Close ();
		}
		
		#endregion

	}
}

