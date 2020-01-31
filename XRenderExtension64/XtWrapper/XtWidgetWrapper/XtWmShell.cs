using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
				
	/// <summary> Prototype the ConfigureNotify event. </summary>
	/// <param name="source"> The widget, the DialogEnd event is assigned to. <see cref="XtWmShell"/> </param>
	/// <param name="widget"> The widget, that is source of the event. <see cref="System.IntPtr"/> </param>
	/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
	/// <param name="parameters"> Additional parameters (as String[]). Not used. <see cref="System.IntPtr"/> </param>
	/// <param name="numParams"> The number of additional parameters. Not used. <see cref="XCardinal"/> </param>
	public delegate void ConfigureNotifyDelegate (XtWmShell source, IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal numParams);

	/// <summary> Base calss for shell widgets with ICCCM-compliant window manager interaction. </summary>
	public class XtWmShell : XtShell
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public new const string	CLASS_NAME					= "XtWmShell";
		
		/// <summary> The lookup name of the delete window action (initiated by the window manager) to register an action proc for. </summary>
		public const string		DELETE_WINDOW_ACTION_NAME	= "deleteWindowAction";
		
		/// <summary> The lookup name of the configure notify action (initiated by the window manager) to register an action proc for. </summary>
		public const string		COFIGURE_NOTIFY_ACTION_NAME	= "configureNotifyAction";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
        #endregion

		// ###############################################################################
        // ### E V E N T S
        // ###############################################################################

		#region Events
		
		/// <summary> Register ConfigureNotify event handler. </summary>
		public event ConfigureNotifyDelegate ConfigureNotify;
		
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary> The default constructor. </summary>
		public XtWmShell ()
		{
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

			// Dispose base ressources.
			base.DisposeByParent ();
		}
		
        #endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
        #endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary> Process ConfigureNotify event. </summary>
		/// <param name="widget"> The widget, that is source of the event. <see cref="System.IntPtr"/> </param>
		/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
		/// <param name="parameters"> Additional parameters (as String[]). Not used. <see cref="System.IntPtr"/> </param>
		/// <param name="numParams"> The number of additional parameters. Not used. <see cref="XCardinal"/> </param>
		internal virtual void OnConfigureNotify(IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal numParams)
		{
			ConfigureNotifyDelegate configureNotify = ConfigureNotify;
			if (configureNotify != null)
				configureNotify (this, widget, ref xevent, parameters, ref numParams);
		}
		
		/// <summary> Register a  "configure notify action" to application context, translate the "configure notify action", add/overwrite
		/// the shell widget's translation table and set windows manager protocol hook for the shell widget. </summary>
		/// <param name="appContext">  The application's context to register the action to.  <see cref="System.IntPtr"/> </param>
		/// <param name="configureNotifyAction"> The action to register to the application's context. <see cref="XtActionProc"/> </param>
		/// <remarks> This method can be called *** BEFORE *** XtRealizeWidget (). </remarks>
		public void RegisterConfigureNotifyAction (IntPtr appContext, XtActionProc configureNotifyAction)
		{
			try
			{
				// Register (instance method) action procedure to runtime action marshaller and let it map the signal to the (global static) action procedure.
				IntPtr configureNotifyActionPtr	= ActionMarshaler.Add (_shell, X11.XEventName.ConfigureNotify, configureNotifyAction);
				
				// Create an actions record, to provide the application's context with a "action-name" to "action-procedure" translation.
				XtActionsRec[]	actionProcs		= new XtActionsRec[] {
					new XtActionsRec (X11Utils.StringToSByteArray (XtWmShell.COFIGURE_NOTIFY_ACTION_NAME + "\0"), configureNotifyActionPtr) };
				
				// Register the actions record to the application's context.
				Xtlib.XtAppAddActions (appContext, actionProcs, (XCardinal)1);
			
				// Create a compiled translation table, to provide the widget with a "message" to "action-name" translation.
				IntPtr			translationTable	= Xtlib.XtParseTranslationTable("<Configure>: " + XtWmShell.COFIGURE_NOTIFY_ACTION_NAME + "()");
				
				// Merge new translations to the widget, overriding existing ones.
				Xtlib.XtOverrideTranslations (_shell, translationTable);
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
			}
		}
		
		/// <summary> Unregister a delete window action. </summary>
		public void UnregisterConfigureNotifyAction ()
		{
			// Unregister (instance method) action procedure from runtime action marshaller.
			ActionMarshaler.Remove (_shell, X11.XEventName.ConfigureNotify);
		}
		
		/// <summary> Register a "delete window action" to application context, translate the "delete
		/// window action", add/overwrite the shell widget's translation table and set windows manager
		/// protocol hook for the shell widget. </summary>
		/// <param name="appContext">  The application's context to register the action to.
		/// <see cref="System.IntPtr"/> </param>
		/// <param name="deleteWindowAction"> The action to register to the application's context.
		/// <see cref="XtActionProc"/> </param>
		/// <remarks> This must be done *** AFTER *** XtRealizeWidget (). </remarks>
		public void RegisterDeleteWindowAction (IntPtr appContext, XtActionProc deleteWindowAction)
		{
			try
			{
				// Register (instance method) action procedure to runtime action marshaller
				// and let it map the signal to the (global static) action procedure.
				IntPtr deleteWindowActionPtr	= ActionMarshaler.Add (_shell,
				                                  	X11.XEventName.ClientMessage, deleteWindowAction);
				
				// Create an actions record, to provide the application's context
				// with a "action-name" to "action-procedure" translation.
				XtActionsRec[]	actionProcs		= new XtActionsRec[] {
					new XtActionsRec (X11Utils.StringToSByteArray (XtWmShell.DELETE_WINDOW_ACTION_NAME + "\0"),
					                  deleteWindowActionPtr) };
				
				// Register the actions record to the application's context.
				Xtlib.XtAppAddActions (appContext, actionProcs, (XCardinal)1);
				
				// Create a compiled translation table, to provide the widget with
				// a "message" to "action-name" translation.
				IntPtr			translationTable	= Xtlib.XtParseTranslationTable("<Message>WM_PROTOCOLS: " +
				                                      	XtWmShell.DELETE_WINDOW_ACTION_NAME + "()");
				
				// Merge new translations to the widget, overriding existing ones.
				Xtlib.XtOverrideTranslations (_shell, translationTable);
			
				/// The delete message from the windows manager. Closing an app via window
				/// title functionality doesn't generate a window message - it only generates a
				/// window manager message, thot must be routed to the window (message loop).
				IntPtr	wmDeleteMessage = IntPtr.Zero;
				
				// Hook the closing event from windows manager.
				// Must be done *** AFTER *** XtRealizeWidget () to determine display and window!
				wmDeleteMessage = X11lib.XInternAtom (Xtlib.XtDisplay(_shell), "WM_DELETE_WINDOW", false);
				if (X11lib.XSetWMProtocols (Xtlib.XtDisplay(_shell), Xtlib.XtWindow(_shell),
				                            ref wmDeleteMessage, (X11.TInt)1) == 0)
				{
					Console.WriteLine (CLASS_NAME + "::RegisterDeleteWindowAction () " +
					                   "WARNING: Failed to register 'WM_DELETE_WINDOW' event.");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
			}
		}
		
		/// <summary> Unregister a delete window action. </summary>
		public void UnregisterDeleteWindowAction ()
		{
			// Unregister (instance method) action procedure from runtime action marshaller.
			ActionMarshaler.Remove (_shell, X11.XEventName.ClientMessage);
		}

		#endregion
		
		#region Actions
		
		/// <summary> Define the "configure notify action", invoked by the windows manager on window layout, resize and move. </summary>
		/// <param name="widget"> The widget, that is source of the event. Typically the the shell's toplevel widget. <see cref="System.IntPtr"/> </param>
		/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
		/// <param name="parameters"> Additional parameters (as String[]). Not used. <see cref="System.IntPtr"/> </param>
		/// <param name="numParams"> The number of additional parameters. Not used. <see cref="XCardinal"/> </param>
		/// <remarks> The prototype must match the XtActionProc delegate. </remarks>
		public virtual void ConfigureNotifyAction (IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal numParams)
		{
			// Console.WriteLine (CLASS_NAME + "::ConfigureNotifyAction ()");
			OnConfigureNotify (widget, ref xevent, parameters, ref numParams);
		}
		
		#endregion

	}
}

