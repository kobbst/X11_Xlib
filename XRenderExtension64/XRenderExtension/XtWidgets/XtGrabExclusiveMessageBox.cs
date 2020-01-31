using System;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
	/// <summary> Sample implementation of a typical X Toolkit Intrinsic exclusively grabbing message box dialog based on a form widget. </summary>
	public class XtGrabExclusiveMessageBox : XtDialog
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public new const string		CLASS_NAME				= "XtGrabExclusiveMessageBox";
		
		/// <summary> The shell widgets resource name. </summary>
		public const string			DIALOG_RESOURCE_NAME	= "MessageBoxForm";
		
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
						
		public XtGrabExclusiveMessageBox (XtApplicationShell toplevelShell, string message, string caption, DialogIcon icontype)
			: base (toplevelShell, caption)
		{
			if (toplevelShell == null)
				throw new ArgumentNullException ("toplevelShell");
			
			Arg[] formArgs		= {	new Arg(XtNames.XtNbackground, (XtArgVal)toplevelShell.AllocColorFromDefaultColormap (X11ColorNames.Gray80).P) };
			_dialogForm			= Xtlib.XtCreateManagedWidget(DIALOG_RESOURCE_NAME,
									Xtlib.XawFormWidgetClass(), _shell,
									formArgs, (XCardinal)1);
			
			// The dialog widget needs the final size of all child widgets during layout calculation to prevent child widget overlapping. Or in other words:
			// Child widget overlapping will happen, if XtNicon is set after dialog widget's layout calculation, because XtMakeResizeRequest () can't be called.
			
			// Currently the dialog widget isn't realized and therefor it hasn't a window. That's why we use the toplevel shell's window instead.
			IntPtr logo	= IntPtr.Zero;
			if (!(Xtlib.XtIsRealized (toplevelShell.Shell) == (TBoolean)0) && icontype != XtDialog.DialogIcon.None)
			{
				IntPtr	display = Xtlib.XtDisplay (toplevelShell.Shell);
				IntPtr	window  = Xtlib.XtWindow  (toplevelShell.Shell);
				if (icontype == DialogIcon.Information)
				{
					logo			= X11lib.XCreateBitmapFromData (display, window, XtResources.BIG_INFORMATION_ICON_BITS,
			      													XtResources.BIG_ICON_WIDTH, XtResources.BIG_ICON_HEIGHT);
				}
				else if (icontype == DialogIcon.Question)
				{
					logo			= X11lib.XCreateBitmapFromData (display, window, XtResources.BIG_QUESTION_ICON_BITS,
			      													XtResources.BIG_ICON_WIDTH, XtResources.BIG_ICON_HEIGHT);
				}
				else if (icontype == DialogIcon.Exclamation)
				{
					logo			= X11lib.XCreateBitmapFromData (display, window, XtResources.BIG_EXCLAMATION_ICON_BITS,
			      													XtResources.BIG_ICON_WIDTH, XtResources.BIG_ICON_HEIGHT);
				}
				else // if (icontype == DialogIcon.Stop)
				{
					logo			= X11lib.XCreateBitmapFromData (display, window, XtResources.BIG_STOP_ICON_BITS,
			      													XtResources.BIG_ICON_WIDTH, XtResources.BIG_ICON_HEIGHT);
				}
			}			
			Arg[] msgArgs		= {	new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray (message + "\0")),
									new Arg(XtNames.XtNbackground, (XtArgVal)toplevelShell.AllocColorFromDefaultColormap (X11ColorNames.Gray80).P),
									new Arg(XtNames.XtNborderWidth, (XtArgVal)0),
				 					new Arg(XtNames.XtNleftBitmap, (XtArgVal)logo) };
			_messageLabel		= Xtlib.XtCreateManagedWidget(_msgLblName,
		                    		Xtlib.XawLabelWidgetClass(), _dialogForm,
		                    		msgArgs, (XCardinal)4);

			Arg[] okArgs		= {	new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray ("Yes\0")),
				 					new Arg(XtNames.XtNfromVert, _messageLabel) };
			_okCommand			= Xtlib.XtCreateManagedWidget(_okCmdName,
									Xtlib.XawCommandWidgetClass(), _dialogForm,
									okArgs, (XCardinal)2);
            Xtlib.XtAddCallback (_okCommand, XtNames.XtNcallback, CallBackMarshaler.Add (_okCommand, this.DialogOk), IntPtr.Zero);
			
			Arg[] cancelArgs	= {	new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray ("No\0")),
				 					new Arg(XtNames.XtNfromVert, _messageLabel),
				 					new Arg(XtNames.XtNfromHoriz, _okCommand) };
			_cancelCommand		= Xtlib.XtCreateManagedWidget(_cancelCmdName,
									Xtlib.XawCommandWidgetClass(), _dialogForm,
									cancelArgs, (XCardinal)3);
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

			// Memory and resources, that are assigned to underlaying Athena widget's
			// instance structure are destroyed by XtDestroyWidget ().
			// This typically inclused the GCs, pixmaps and local copies of strings.
			// They are typically assigned during XtCreateWidget () or XtSetValues ().
			if (_shell != IntPtr.Zero)
			{
				Xtlib.XtDestroyWidget (_shell);
				_shell = IntPtr.Zero;
			}

			this.DisposeByParent ();
		}

		/// <summary> Dispose by parent. </summary>
		public override void DisposeByParent ()
		{
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
			Console.WriteLine (CLASS_NAME + "::DeleteWindowAction() For message box.");
			this.Close ();
		}
		
		#endregion

	}
}

