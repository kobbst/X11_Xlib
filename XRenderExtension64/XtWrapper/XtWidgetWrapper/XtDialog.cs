using System;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
				
	/// <summary> Prototype the DialogEnd event. </summary>
	/// <param name="source"> The widget, the DialogEnd event is assigned to. <see cref="XtWmShell"/> </param>
	public delegate void DialogEndDelegate (XtDialog source);

	/// <summary> Base class (can not be instanciated) for exclusively grabbing dialogs. </summary>
	public abstract class XtDialog : XtWmShell
	{
		
        // ###############################################################################
        // ### I N N E R   C L A S S E S
        // ###############################################################################

		#region Inner classes
		
		/// <summary> The result of the fully modal dialog call. </summary>
		public enum DialogResult
		{
			/// <summary> dialog ended with OK. </summary>
			Ok,
			
			/// <summary> Dialog ended with Cancel or was closed by the window's decoration close [X] button. </summary>
			Cancel
		}
		
		/// <summary> The icon to show within the dialog. </summary>
		public enum DialogIcon
		{
			/// <summary> No icon to show. </summary>
			None,
			
			/// <summary> Show the information icon. </summary>
			Information,
			
			/// <summary> Show the question icon. </summary>
			Question,
			
			/// <summary> Show the exclamation icon. </summary>
			Exclamation,
			
			/// <summary> Show the stop icon. </summary>
			Stop
		}
		
		#endregion

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public new const string			CLASS_NAME				= "XtGrabExclusiveDialog";
		
		/// <summary> The shell widgets resource name. </summary>
		public const string				SHELL_RESOURCE_NAME		= "GrabExclusiveDialog";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
				
		/// <summary> The toplevel shell that owns the underlaying transient popup shell of this dialog. </summary>
		protected XtApplicationShell	_toplevelShell	= null;
				
		/// <summary> The dialog form widget. </summary>
		protected IntPtr				_dialogForm		= IntPtr.Zero;
		
		/// <summary> The ressource name of the message label. </summary>
		protected string				_msgLblName		= "DialogMessageLabel";
		
		/// <summary> The ressource name of the OK command button. </summary>
		protected string				_okCmdName		= "DialogOkCommand";
		
		/// <summary> The ressource name of the Cancel command button. </summary>
		protected string				_cancelCmdName	= "DialogCancelCommand";
		
		/// <summary> The message label widget. </summary>
		protected IntPtr				_messageLabel	= IntPtr.Zero;
		
		/// <summary> The OK command button widget. </summary>
		protected IntPtr				_okCommand		= IntPtr.Zero;
		
		/// <summary> The Cancel command button widget. </summary>
		protected IntPtr				_cancelCommand	= IntPtr.Zero;
		
		/// <summary> The result after the dialog end. </summary>
		protected DialogResult			_result			= DialogResult.Cancel;
		
		/// <summary> The custom dialog name. Can be used to identify a dialog instance. </summary>
		public string					Name			= "";

        #endregion

		// ###############################################################################
        // ### E V E N T S
        // ###############################################################################

		#region Events
		
		/// <summary> Register DialogEnd event handler. </summary>
		public event DialogEndDelegate DialogEnd;
		
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary> The initializing constructor. </summary>
		/// <param name="toplevelShell"> The toplevel shell that owns the underlaying transient popup shell of this dialog. <see cref="XtApplicationShell"/> </param>
		/// <param name="toplevelShell"> The shell caption. <see cref="System.String"/> </param>
		public XtDialog (XtApplicationShell toplevelShell, string caption)
		{
			if (toplevelShell == null)
				throw new ArgumentNullException ("toplevelShell");
			
			_toplevelShell			= toplevelShell;
			
			if (!string.IsNullOrEmpty (caption))
			{
				Arg[] shellArgs		= {	new Arg(XtNames.XtNtitle, X11.X11Utils.StringToSByteArray (caption + "\0")) };
				_shell				= Xtlib.XtCreatePopupShell (SHELL_RESOURCE_NAME,
										Xtlib.XawTransientShellWidgetClass(), toplevelShell.Shell,
										shellArgs, (XCardinal)1);
			}
			else
			{
				_shell				= Xtlib.XtCreatePopupShell (SHELL_RESOURCE_NAME,
										Xtlib.XawTransientShellWidgetClass(), toplevelShell.Shell,
										Arg.Zero, 0);
			}
			
			_toplevelShell.AssociatedShells.Add (this);
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
			_toplevelShell.AssociatedShells.Remove (this);
		
			base.DisposeByParent ();
		}
		
        #endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary> Get the dialog form widget. </summary>
		public IntPtr DialogForm
		{	get	{	return _dialogForm;	}	}

		/// <summary> Get the result after the dialog end. </summary>
		public DialogResult Result
		{	get	{	return _result;	}	}

		#endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary> Process DialogEnd event. </summary>
		internal virtual void OnDialogEnd()
		{
			DialogEndDelegate dialogEnd = DialogEnd;
			if (dialogEnd != null)
				dialogEnd (this);
		}
		
		/// <summary> The dialog specific close. </summary>
		public override void Close ()
		{
			Xtlib.XtPopdown (_shell);
			_result = XtDialog.DialogResult.Cancel;
			OnDialogEnd ();
			Dispose ();
		}
		
        #endregion

		#region Callback handler
		
		/// <summary> The message box ok callback procedure. </summary>
		/// <param name="widget"> The widget, that initiated the callback procedure. <see cref="System.IntPtr"/> </param>
		/// <param name="clientData"> Additional callback data from the client. <see cref="System.IntPtr"/> </param>
		/// <param name="callData"> Additional data defined for the call. <see cref="System.IntPtr"/> </param>
		public virtual void DialogOk ([In]IntPtr widget, [In]IntPtr client_data, [In]IntPtr call_data)
		{
			Xtlib.XtPopdown (_shell);
			_result = XtDialog.DialogResult.Ok;
			OnDialogEnd ();
			Dispose ();
		}
		
		/// <summary> The message box cancel callback procedure. </summary>
		/// <param name="widget"> The widget, that initiated the callback procedure. <see cref="System.IntPtr"/> </param>
		/// <param name="clientData"> Additional callback data from the client. <see cref="System.IntPtr"/> </param>
		/// <param name="callData"> Additional data defined for the call. <see cref="System.IntPtr"/> </param>
		public virtual void DialogCancel ([In]IntPtr widget, [In]IntPtr client_data, [In]IntPtr call_data)
		{
			Xtlib.XtPopdown (_shell);
			_result = XtDialog.DialogResult.Cancel;
			OnDialogEnd ();
			Dispose ();
		}
		
        #endregion

	}
}

