using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
	
	/// <summary> Sample implementation of a typical X Toolkit Intrinsic application window. </summary>
	public class XtWindow : XtApplicationShell
	{
		[DllImport("libXRenderTestLib", EntryPoint = "linearGradient")]
		extern public static IntPtr linearGradientPict(IntPtr x11display, IntPtr x11window, IntPtr x11visual,
		                                  TInt iWidth, TInt iHeight, TInt gradientType,
		                                  IntPtr gradient,
		                                  X11.TInt[] stops, // X11.TInt[] stops,
		                                  IntPtr colors, //  XRenderLib.XRenderColor[] colors,
		                                  X11.TInt nstops);

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public new const string		CLASS_NAME = "XtWindow";

		/// <summary> Width of the output label. </summary>
		public const int			INITIAL_WIDTH = 420;

		/// <summary> Width of the output label. </summary>
		public const int			OUTPUT_HEIGHT = 380;

		/// <summary> The size if a lines bunch. </summary>
		public const int			LINES_BUNCH_SIZE = 3;

		/// <summary> The size if a circle bunch. </summary>
		public const int			CIRCLE_BUNCH_SIZE = 3;

		/// <summary> The size if a rectangle bunch. </summary>
		public const int			RECT_BUNCH_SIZE = 3;
		
		/// <summary>The available modes to draw the demo output.</summary>
		private const int			DrawModeInitial		= 0;
		private const int			DrawModeBackground	= 1;
		private const int			DrawModeFgLinear	= 2;
		private const int			DrawModeFgConical	= 3;
		private const int			DrawModeFgRadial	= 4;
		
        #endregion
		
		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary>The root manager widget.</summary
		IntPtr					_rootForm			= IntPtr.Zero;
			
		/// <summary>The drawing canvas.</summary
		IntPtr					_output				= IntPtr.Zero;
					
		/// <summary>The status icon widget.</summary>
		IntPtr					_statusIcon			= IntPtr.Zero;
		
		/// <summary>The status label widget.</summary>
		IntPtr					_statusLabel		= IntPtr.Zero;
		
		/// <summary>Flag, indicating whether initial configuration is complete.</summary>
		bool					_initialConfigured	= false;
		
		/// <summary>The current mode to draw the demo output.</summary>
		private static int		_drawMode			= DrawModeInitial;

		// ==========================================================================================
		// Resource names.
		// ==========================================================================================
		// Main application widgets.
		string rootFormName				= "RootForm";
		string menuBoxName				= "MenuBox";
		string canvasName				= "Canvas";
		string statFormName				= "StatForm";
		string preX11r4CmdName			= "PreX11R4Command";
		string x11r4CmdName				= "X11R4MenuCommand";
		string closeCmdName				= "CloseAppMenuCommand";
		string statusicoName			= "StatusIcon";
		string statuslblName			= "StatusLabel";
		// File menu widgets.
		string preX11r4MenuName			= "PreX11R4MenuShell";
		string preX11r4BoxName			= "PreX11R4MenuBox";
		string preX11r4MenuEntry1Name	= "PreX11R4MenuEntry1";
		string preX11r4MenuEntry2Name	= "PreX11R4MenuEntry2";
		string preX11r4MenuEntry3Name	= "PreX11R4MenuEntry3";
		// X11R4 menu widgets.
		string x11r4menuName			= "X11R4Menu";
		string x11r4entry1Name			= "X11R4MenuEntry1";
		string x11r4entry2Name			= "X11R4MenuEntry2";
		string x11r4entry3Name			= "X11R4MenuEntry3";
		string x11r4entry4Name			= "X11R4MenuEntry4";
			
        #endregion
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction

		/// <summary> Initializing constructor. </summary>
		/// <param name="appResourceName"> The ressource name of the application. <see cref="System.String"/> </param>
		/// <param name="shellResourceName"> The ressource name of the underlaying shell. <see cref="System.String"/> </param>
		/// <param name="fallbackResources"> The fallback ressources to apply. <see cref="Xt.XtFallbackRessources"/> </param>
		public XtWindow (string appResourceName, string shellResourceName, Xt.XtFallbackRessources fallbackResources)
			: base (appResourceName, shellResourceName, fallbackResources)
		{
			base.IconPath = "attention.bmp";
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
			base.DisposeByParent ();
		}

        #endregion
		
        // ###############################################################################
        // ### E N T R Y   P O I N T
        // ###############################################################################

        #region Entry point
		
		public static void Main (string[] args)
		{
			
			// ==========================================================================================
			// Fallback ressources.
			// ==========================================================================================
			Xt.XtFallbackRessources fallbackResources = new Xt.XtFallbackRessources ();
			// Main application widgets.
			fallbackResources.Add ("*RootForm.background:                #F0F0F0");
			fallbackResources.Add ("*MenuBox.orientation:                horizontal");
			fallbackResources.Add ("*MenuBox.background:                 #F0F0F0");
			fallbackResources.Add ("*MenuBox.top:                        ChainTop");		// Resize behaviour.
			fallbackResources.Add ("*MenuBox.bottom:                     ChainTop");		// Resize behaviour.
			fallbackResources.Add ("*MenuBox.left:                       ChainLeft");		// Resize behaviour.
			fallbackResources.Add ("*MenuBox.right:                      ChainRight");		// Resize behaviour.
			fallbackResources.Add ("*PreX11R4Command.label:              Background Pictures");
			fallbackResources.Add ("*PreX11R4Command.translations:       <EnterWindow>: highlight() \\n <LeaveWindow>: reset() \\n <BtnDown>: set() XtMenuPopup(PreX11R4MenuShell) reset()");
			fallbackResources.Add ("*X11R4MenuCommand.label:             Foreground gradients");
			fallbackResources.Add ("*CloseAppMenuCommand*label:          Close app.");
			//fallbackResources.Add ("*CloseAppMenuCommand.translations:   <EnterWindow>: set() \\n <LeaveWindow>: unset()");
			//fallbackResources.Add ("*CloseAppMenuCommand.fromHoriz:      MsgBoxMenuCommand");
			fallbackResources.Add ("*Canvas.fromVert:                    MenuBox");			// Initial placement.
			fallbackResources.Add ("*Canvas.left:                        ChainLeft");		// Resize behaviour.
			fallbackResources.Add ("*Canvas.right:                       ChainRight");		// Resize behaviour.
			fallbackResources.Add ("*Canvas.top:                         ChainTop");		// Resize behaviour.
			fallbackResources.Add ("*Canvas.bottom:                      ChainBottom");		// Resize behaviour.
			fallbackResources.Add ("*Canvas.translations:                <Expose>: draw()");
			fallbackResources.Add ("*StatForm.fromVert:                  Canvas");			// Initial placement.
			fallbackResources.Add ("*StatForm.left:                      ChainLeft");		// Resize behaviour.
			fallbackResources.Add ("*StatForm.right:                     ChainRight");		// Resize behaviour.
			fallbackResources.Add ("*StatForm.top:                       ChainBottom");		// Resize behaviour.
			fallbackResources.Add ("*StatForm.bottom:                    ChainBottom");		// Resize behaviour.
			fallbackResources.Add ("*StatusLabel.label:                  Application up and running.");
			fallbackResources.Add ("*StatusIcon.borderWidth:             0");
			fallbackResources.Add ("*StatusIcon.background:              #DDDDDD");
			fallbackResources.Add ("*StatusIcon.left:                    ChainLeft");		// Resize behaviour.
			fallbackResources.Add ("*StatusIcon.right:                   ChainLeft");		// Resize behaviour.
			fallbackResources.Add ("*StatusLabel.borderWidth:            0");
			fallbackResources.Add ("*StatusLabel.background:             #DDDDDD");
			fallbackResources.Add ("*StatusLabel.fromHoriz:              StatusIcon");		// Initial placement.
			fallbackResources.Add ("*StatusLabel.left:                   ChainLeft");		// Resize behaviour.
			fallbackResources.Add ("*StatusLabel.right:                  ChainRight");		// Resize behaviour.
			// File menu widgets.
			fallbackResources.Add ("*PreX11R4MenuBox.background:         #D0D0D0");
			fallbackResources.Add ("*PreX11R4MenuEntry1.label:           Semi-transparent rectangles overlapping");
			fallbackResources.Add ("*PreX11R4MenuEntry1.background:      #D0D0D0");
			fallbackResources.Add ("*PreX11R4MenuEntry1.borderWidth:     0");
			fallbackResources.Add ("*PreX11R4MenuEntry1.translations:    <EnterWindow>:highlight()\\n<LeaveWindow>:unhighlight()\\n<BtnDown>:set() notify() unset() XtMenuPopdown(PreX11R4MenuShell)");
			fallbackResources.Add ("*PreX11R4MenuEntry2.label:           Semi-transparent rectangles stacked");
			fallbackResources.Add ("*PreX11R4MenuEntry2.background:      #D0D0D0");
			fallbackResources.Add ("*PreX11R4MenuEntry2.borderWidth:     0");
			fallbackResources.Add ("*PreX11R4MenuEntry2.translations:    <EnterWindow>:highlight()\\n<LeaveWindow>:unhighlight()\\n<BtnDown>:set() notify() unset() XtMenuPopdown(PreX11R4MenuShell)");
			fallbackResources.Add ("*PreX11R4MenuEntry3.label:           No background picture");
			fallbackResources.Add ("*PreX11R4MenuEntry3.background:      #D0D0D0");
			fallbackResources.Add ("*PreX11R4MenuEntry3.borderWidth:     0");
			fallbackResources.Add ("*PreX11R4MenuEntry3.translations:    <EnterWindow>:highlight()\\n<LeaveWindow>:unhighlight()\\n<BtnDown>:set() notify() unset() XtMenuPopdown(PreX11R4MenuShell)");
			fallbackResources.Add ("*X11R4MenuEntry1.label:              Linear vertical color gradient");
			fallbackResources.Add ("*X11R4MenuEntry2.label:              Conical color gradient");
			fallbackResources.Add ("*X11R4MenuEntry3.label:              Radial color gradient");
			fallbackResources.Add ("*X11R4MenuEntry4.label:              No gradient");
			
			// ************************************************************************************************************
			// The "notity()" transaltion will only call the XtNcallback registered callbacks, if commandWidget is "set()".
			
			// ==========================================================================================
			// Application initialization.
			// ==========================================================================================
			XtWindow xtWindow = null;
			
			try
			{
				xtWindow = new XtWindow ("XtApplication", "ApplicationShell", fallbackResources);
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
				Xtlib.exit(1);
			}
			finally
			{
				fallbackResources.Dispose();
				fallbackResources = null;
			}
			
			xtWindow.Run (args);
		}

		#endregion

        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		public void Run (string[] args)
		{
			// ==========================================================================================
			// Widget pointer.
			// ==========================================================================================
			// Main application widgets.
			// IntPtr	toplevelShell		= IntPtr.Zero;	// moved to class attributes.
			IntPtr	menuBox					= IntPtr.Zero;
			IntPtr	statForm				= IntPtr.Zero;
			IntPtr	preX11r4Command			= IntPtr.Zero;
			IntPtr	x11r4Command			= IntPtr.Zero;
			IntPtr	closeCommand			= IntPtr.Zero;
			// File menu widgets.
			IntPtr  preX11r4MenuShell		= IntPtr.Zero;
			IntPtr  preX11r4MenuBox			= IntPtr.Zero;
			IntPtr  preX11r4MenuEntryOne	= IntPtr.Zero;
			IntPtr  preX11r4MenuEntryTwo	= IntPtr.Zero;
			IntPtr  preX11r4MenuEntryThree	= IntPtr.Zero;
			// X11R4 menu widgets.
			IntPtr  x11r4menuShell			= IntPtr.Zero;
			IntPtr  x11r4entryOne			= IntPtr.Zero;
			IntPtr  x11r4entryTwo			= IntPtr.Zero;
			IntPtr  x11r4entryThree			= IntPtr.Zero;
			IntPtr  x11r4entryFour			= IntPtr.Zero;
			// Button box widgets.

			if (Arg.ElementOffset != IntPtr.Size)
			{
				Console.WriteLine ("Adopt element offset for 'Arg' structure member, compile and run again!");
				return;
			}

			// ==========================================================================================
			// Widget tree creation.
			// ==========================================================================================
			try
			{
				// *******************************************
				// Root composite: Use a Form widget, it supports rubber-band resizing
				// and fromVert / fromHorz constraints.
				// * To prevent typical Xaw resizing problems of Form widget children
				// * (they don't expand to the old size after compress), use top, bottom,
				// * left and right constraints with Chain* values for all direct children!
				_rootForm				= Xtlib.XtCreateManagedWidget(rootFormName,
											Xtlib.XawFormWidgetClass(), _shell,
											Arg.Zero, 0);
				
				// *******************************************
				// Menu bar: Use a Box widget, this is common.
				Arg[] menuBoxArgs		= {	new Arg(XtNames.XtNwidth,  (XtArgVal)INITIAL_WIDTH ),
											new Arg(XtNames.XtNhSpace, (XtArgVal)12) };
				menuBox					= Xtlib.XtCreateManagedWidget(menuBoxName,
											Xtlib.XawBoxWidgetClass(), _rootForm,
				                            menuBoxArgs, (XCardinal)2);

				// *******************************************
				// Main form: Use a Form widget, it supports rubber-band resizing and
				// fromVert / fromHors constraints.
				Arg[] mainFormArgs		= { new Arg(XtNames.XtNwidth,  (XtArgVal)INITIAL_WIDTH ),
											new Arg(XtNames.XtNheight, (XtArgVal)OUTPUT_HEIGHT) };
				_output					= Xtlib.XtCreateManagedWidget(canvasName,
				                	    	Xtlib.XawLabelWidgetClass(), _rootForm,
				                    		mainFormArgs, (XCardinal)2);
			
				// *******************************************
				// Register an action proc for 'Expore' event to be able to draw the foreground.
				// - Register (instance method) action procedure (with XtActionProc signature) to runtime
				// - action marshaller and let it map the signal to the (global static) action procedure.
				IntPtr drawWindowActionPtr	= ActionMarshaler.Add (_output, X11.XEventName.Expose, this.draw);
				
				// - Create an actions record, to provide the application's context
				// - with a "action-name" to "action-procedure" translation.
				XtActionsRec[]	actionProcs		= new XtActionsRec[] {
					new XtActionsRec (X11Utils.StringToSByteArray ("draw\0"), drawWindowActionPtr) };
				
				// - Register the actions record to the application's context.
				Xtlib.XtAppAddActions (XtApplicationShell.Instance.AppContext, actionProcs, (XCardinal)1);
				
				// - To function properly, a translation must be defined in resources for the output widget.
				//   E. g.: "*Canvas.translations:                <Expose>: draw()"
				
				// *******************************************
				// Status bar:
				Arg[] statFormArgs		= { new Arg(XtNames.XtNresizable,  (XtArgVal)1 ) };
				statForm				= Xtlib.XtCreateManagedWidget(statFormName,
				                	    	Xtlib.XawFormWidgetClass(), _rootForm,
				                    		statFormArgs, (XCardinal)1);

				// *******************************************
				// DEBUG: Start
				IntPtr hBroderWidth		= Marshal.AllocHGlobal (2);
				IntPtr hBackColor		= Marshal.AllocHGlobal (4);
				Arg[] statRetArgs		= {	new Arg(XtNames.XtNborderWidth, hBroderWidth),
											new Arg(XtNames.XtNbackground,  hBackColor)    };
				Xtlib.XtGetValues (statForm, statRetArgs, (XCardinal)2);
				ushort  brdrWidth		= (ushort)Marshal.ReadInt16 (hBroderWidth);
				uint    bkColor			= (uint)Marshal.ReadInt32 (hBackColor);
				Marshal.FreeHGlobal (hBackColor);
				Marshal.FreeHGlobal (hBroderWidth);
				Console.WriteLine ("Border width: " + brdrWidth);
				Console.WriteLine ("Back color: " + bkColor);
				// DEBUG: End
				
				// *******************************************
				// Fill status bar.
				// - Status icon.
				Arg[] statIconArgs		= {	new Arg(XtNames.XtNwidth,  (XtArgVal)24),
											new Arg(XtNames.XtNheight, (XtArgVal)17),
											new Arg(XtNames.XtNlabel, X11.X11Utils.StringToSByteArray ("\0")) };
				_statusIcon				= Xtlib.XtCreateManagedWidget(statusicoName,
				                    		Xtlib.XawLabelWidgetClass(), statForm,
				                    		statIconArgs, (XCardinal)3);
				// - Status label.
				Arg[] statLblArgs		= {	new Arg(XtNames.XtNwidth,  (XtArgVal)(INITIAL_WIDTH - 12 - 24)) };
				_statusLabel			= Xtlib.XtCreateManagedWidget(statuslblName,
				                    		Xtlib.XawLabelWidgetClass(), statForm,
				                    		statLblArgs, (XCardinal)1);
				
				// *******************************************
				// Fill menu bar.
				// - Prior X11r4 --> First menu command button.
		        preX11r4Command			= Xtlib.XtCreateManagedWidget(preX11r4CmdName,
				                    		Xtlib.XawCommandWidgetClass(), menuBox,
				                    		Arg.Zero, 0);
				
				// - X11R4 --> Second menu button. Directly connect the X11R4 menu shell via 'menuName' argument.
				IntPtr menuName         = Marshal.StringToHGlobalAnsi (x11r4menuName);
				Arg[] x11r4cmdArgs      = { new Arg(XtNames.XtNmenuName, (XtArgVal)menuName) };
				x11r4Command			= Xtlib.XtCreateManagedWidget(x11r4CmdName,
											Xtlib.XawMenuButtonWidgetClass(), menuBox,
											x11r4cmdArgs, (XCardinal)1);
				
				// - Close command button: Use a commond widget and connect a callback to the button up event.
		        closeCommand			= Xtlib.XtCreateManagedWidget(closeCmdName,
											Xtlib.XawCommandWidgetClass(), menuBox,
											Arg.Zero, 0);
				// This approach works, but the "this" context gets lost.
				// XtCallbackProc closeHandler = new XtCallbackProc (ApplicationClose);
				// IntPtr closePtr = Marshal.GetFunctionPointerForDelegate(closeHandler);
                // Xtlib.XtAddCallback (closeCommand, XtNames.XtNcallback, closePtr, IntPtr.Zero);
	            Xtlib.XtAddCallback (closeCommand, XtNames.XtNcallback, CallBackMarshaler.Add (closeCommand, this.ApplicationCloseClick), IntPtr.Zero);

				// - Prior X11r4 --> First menu's shell: Use an override shell to prevent window decoration
				// (title bar, close button, ...) and connect a callback to the popup event.
				// The shell must be placed by an explicit implemented callback during popup.
				preX11r4MenuShell		= Xtlib.XtCreatePopupShell(preX11r4MenuName,
											Xtlib.XawOverrideShellWidgetClass(), preX11r4Command,
											Arg.Zero, 0);
				XtCallbackProc placePriorX11r4MMenuHandler = new XtCallbackProc (PlacePriorX11r4Menu);
				IntPtr placePriorX11r4MMenuPtr = Marshal.GetFunctionPointerForDelegate(placePriorX11r4MMenuHandler);
				Xtlib.XtAddCallback (preX11r4MenuShell, XtNames.XtNpopupCallback, placePriorX11r4MMenuPtr, IntPtr.Zero);
				
				// - Prior X11r4 --> First menu's shell content: Use a box widget to organize the menu options and use label widgets (smallest overhead) for menu options.
				preX11r4MenuBox			= Xtlib.XtCreateManagedWidget(preX11r4BoxName,
											Xtlib.XawBoxWidgetClass(), preX11r4MenuShell,
											Arg.Zero, 0);
				preX11r4MenuEntryOne	= Xtlib.XtCreateManagedWidget(preX11r4MenuEntry1Name,
											Xtlib.XawCommandWidgetClass(), preX11r4MenuBox,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (preX11r4MenuEntryOne, XtNames.XtNcallback, CallBackMarshaler.Add (preX11r4MenuEntryOne, this.CommandBasedMenuEntryClick), IntPtr.Zero);
				preX11r4MenuEntryTwo	= Xtlib.XtCreateManagedWidget(preX11r4MenuEntry2Name,
											Xtlib.XawCommandWidgetClass(), preX11r4MenuBox,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (preX11r4MenuEntryTwo, XtNames.XtNcallback, CallBackMarshaler.Add (preX11r4MenuEntryTwo, this.CommandBasedMenuEntryClick), IntPtr.Zero);
				preX11r4MenuEntryThree	= Xtlib.XtCreateManagedWidget(preX11r4MenuEntry3Name,
											Xtlib.XawCommandWidgetClass(), preX11r4MenuBox,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (preX11r4MenuEntryThree, XtNames.XtNcallback, CallBackMarshaler.Add (preX11r4MenuEntryThree, this.CommandBasedMenuEntryClick), IntPtr.Zero);
				
				// X11r4 --> Second menu's shell: Use a X11R4 simple menu widget class for menu shell and X11R4 SmeBSB widgets for menu options.
				// The X11r4 menu shell is a spring-loaded popup shell. Such a shell pops up in response to a button press event and
				// will pop down when the button is released - even if it occures in another window.
				x11r4menuShell			= Xtlib.XtCreatePopupShell(x11r4menuName,
											Xtlib.XawSimpleMenuWidgetClass(), x11r4Command,
											Arg.Zero, 0);
				// 11r4 menu shell content: The registration to a spring-loaded popup shell is all, that's needed.
				x11r4entryOne 			= Xtlib.XtCreateManagedWidget(x11r4entry1Name,
											Xtlib.XawSmeBSBWidgetClass(), x11r4menuShell,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (x11r4entryOne, XtNames.XtNcallback, CallBackMarshaler.Add (x11r4entryOne, this.SmeBasedMenuEntryClick), IntPtr.Zero);
				x11r4entryTwo 			= Xtlib.XtCreateManagedWidget(x11r4entry2Name,
											Xtlib.XawSmeBSBWidgetClass(), x11r4menuShell,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (x11r4entryTwo, XtNames.XtNcallback, CallBackMarshaler.Add (x11r4entryTwo, this.SmeBasedMenuEntryClick), IntPtr.Zero);
				x11r4entryThree			= Xtlib.XtCreateManagedWidget(x11r4entry3Name,
											Xtlib.XawSmeBSBWidgetClass(), x11r4menuShell,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (x11r4entryThree, XtNames.XtNcallback, CallBackMarshaler.Add (x11r4entryThree, this.SmeBasedMenuEntryClick), IntPtr.Zero);
				x11r4entryFour 			= Xtlib.XtCreateManagedWidget(x11r4entry4Name,
											Xtlib.XawSmeBSBWidgetClass(), x11r4menuShell,
											Arg.Zero, 0);
	            Xtlib.XtAddCallback (x11r4entryFour, XtNames.XtNcallback, CallBackMarshaler.Add (x11r4entryFour, this.SmeBasedMenuEntryClick), IntPtr.Zero);
				
				this.ConfigureNotify += HandleConfigureNotify;
				this.SetStatusLabel += HandleSetStatusLabel;
				
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
				Xtlib.exit(1);
			}
			
			// ==========================================================================================
			// Application start.
			// ==========================================================================================
			base.Run ();
		}
		
		/// <summary> The prototype of shell specific close. </summary>
		/// <remarks> Application shels may implement the application exit, all other shells may implement popdown. </remarks>
		public override void Close ()
		{
			Console.WriteLine (CLASS_NAME + "::Close ()");
			
			XtGrabExclusiveMessageBox dlg = new XtGrabExclusiveMessageBox (this, "Do you really want to\nquit the application?",
			                                                               "Quit request", XtDialog.DialogIcon.Question);
			dlg.Name = "QueryForApplicationClose";
			dlg.DialogEnd += HandleDlgDialogEnd_QueryForApplicationClose;
			dlg.Run (_appContext);
		}
		
		#endregion

        // ###############################################################################
        // ### . N E T   M E S S A G E   H A N D L E R
        // ###############################################################################

		#region .NET message handler
		
		/// <summary>The callback for Expose event on output widget. Must match the XtActionProc signature.</summary>
		/// <param name="widget">The widget, that received the event.<see cref="IntPtr"/></param>
		/// <param name="xevent">The event.<see cref="XEvent"/></param>
		/// <param name="parameters">Additional parameters, that can be used application specific.<see cref="IntPtr"/></param>
		/// <param name="numParams">Number of additional parameters, that can be used application specific.<see cref="XCardinal"/></param>
		public void draw (IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal numParams)
		{
			if (widget == IntPtr.Zero)
				return;
			
			if (_drawMode == DrawModeInitial)
				return;
			if (_drawMode == DrawModeBackground)
				return;
			
			IntPtr display = Xtlib.XtDisplay (widget); // Xtlib.XtDisplay (_output);
			if (display == IntPtr.Zero)
			{
				Console.WriteLine ("ERROR: draw() - Unable to investigate the display.");
				return;
			}
			
			IntPtr window  = Xtlib.XtWindow(widget);   // Xtlib.XtWindow(_output);
			if (window == IntPtr.Zero)
			{
				Console.WriteLine ("ERROR: draw() - Unable to investigate the window.");
				return;
			}
			
			IntPtr visual  = X11lib.XDefaultVisual (display, 0);
			if (visual == IntPtr.Zero)
			{
				Console.WriteLine ("ERROR: draw() - Unable to investigate the visual.");
				return;
			}
			
			TInt firstEvent;
			TInt firstError;
			if (XRenderLib.XRenderQueryExtension (display, out firstEvent, out firstError) != true)
			{
				Console.WriteLine ("ERROR: draw() - Call to XRenderQueryExtension() failed.");
				return;
			}
			
			TInt majorVersion = 0;
			TInt minorVersion = 0;
			if (XRenderLib.XRenderQueryVersion (display, out majorVersion, out minorVersion) <= 0)
			{
				Console.WriteLine ("ERROR: MenuEntryClick() - Call to XRenderQueryVersion() failed.");
				return;
			}
			if ((int)majorVersion > 0 || ((int)majorVersion == 0 || (int)minorVersion >= 7))
			{	;
				// O.K. All functions, this sample is based on, are supported.
				// - XRenderFillRectangle/XRenderFillRectangles
				// - XRenderComposite
				// - XRenderCreateLinearGradient/XRenderCreateConicalGradient/XRenderCreateRadialGradient
				// - ...
			}
			else
			{
				Console.WriteLine ("ERROR: draw() - Outdated version of XRender extension. Test might failed.");
			}
			
			int	width  = (int)Xtlib.XtGetValueOfDimension (_output, XtNames.XtNwidth);
			int	height = (int)Xtlib.XtGetValueOfDimension (_output, XtNames.XtNheight);
			int pixmapWidth  = Math.Max (1, width ); // Math.Max (1, ((int)(width  / 8)) * 8);
			int pixmapHeight = Math.Max (1, height); // Math.Max (1, ((int)(height / 8)) * 8);
			
			XRenderLib.XRenderPictFormat xRenderPictFormat = XRenderLib.XRenderFindVisualFormat   (display, visual);
			if (xRenderPictFormat.id != IntPtr.Zero)
			{
 				XRenderLib.XRenderPictureAttributes pictureAttributes = new XRenderLib.XRenderPictureAttributes();

			 	/* this is my target- or destination-drawable derived from the window */ 
			 	IntPtr destPict = XRenderLib.XRenderCreatePicture (display, window, ref xRenderPictFormat,
												 					XRenderCreatePictureValueMask.CPNone, ref pictureAttributes);
				
				if (destPict != IntPtr.Zero)
					drawGradientForeground (display, window, visual, (TInt)pixmapWidth, (TInt)pixmapHeight,
					                        destPict);
			}
		}
		
		void drawGradientForeground (IntPtr display, IntPtr window, IntPtr visual, TInt pixmapWidth, TInt pixmapHeight,
					                 IntPtr destPict)
		{
			X11.TInt[]						aColorStops     = new X11.TInt[4]; /* XFixed */
			XRenderLib.XRenderColor[]		aColorList      = new XRenderLib.XRenderColor[4]; 
			XRenderLib.XLinearGradient		linearGradient  = new XRenderLib.XLinearGradient (); 
			XRenderLib.XConicalGradient		conicalGradient = new XRenderLib.XConicalGradient (); 
			XRenderLib.XRadialGradient		radialGradient  = new XRenderLib.XRadialGradient (); 
 
		 	/* offsets for color-stops have to be stated in normalized form, 
		 	** which means within the range of [0.0, 1.0f] */ 
		 	aColorStops[0] = XRenderLib.XDoubleToFixed (0.0f); 
		 	aColorStops[1] = XRenderLib.XDoubleToFixed (0.33f); 
		 	aColorStops[2] = XRenderLib.XDoubleToFixed (0.66f); 
		 	aColorStops[3] = XRenderLib.XDoubleToFixed (1.0f); 

 		 	/* there's nothing much to say about a XRenderColor, 
		 	** each R/G/B/A-component is an unsigned int (16 bit) */ 
		 	aColorList[0].red   = (X11.TUshort)0xffff; 
		 	aColorList[0].green = (X11.TUshort)0x0000; 
		 	aColorList[0].blue =  (X11.TUshort)0x0000; 
		 	aColorList[0].alpha = (X11.TUshort)0xffff; 
		 	aColorList[1].red =   (X11.TUshort)0x0000; 
		 	aColorList[1].green = (X11.TUshort)0xffff; 
		 	aColorList[1].blue =  (X11.TUshort)0x0000; 
		 	aColorList[1].alpha = (X11.TUshort)0xffff; 
		 	aColorList[2].red =   (X11.TUshort)0x0000; 
		 	aColorList[2].green = (X11.TUshort)0x0000; 
		 	aColorList[2].blue =  (X11.TUshort)0xffff; 
		 	aColorList[2].alpha = (X11.TUshort)0xffff; 
		 	aColorList[3].red =   (X11.TUshort)0xffff; 
		 	aColorList[3].green = (X11.TUshort)0x0000; 
		 	aColorList[3].blue =  (X11.TUshort)0x0000; 
		 	aColorList[3].alpha = (X11.TUshort)0xffff; 

			/* coordinates for the start- and end-point of the linear gradient are 
		 	** in  window-space, they are not normalized like in cairo... here using 
		 	** a 10th of the width so it gradient will repeat 10 times sideways if 
		 	** the repeat-attribute is used (see further below) */ 
		 	linearGradient.p1.x = XRenderLib.XDoubleToFixed (0.0f); 
		 	linearGradient.p1.y = XRenderLib.XDoubleToFixed (0.0f); 
		 	linearGradient.p2.x = XRenderLib.XDoubleToFixed (0.0f); 
		 	linearGradient.p2.y = XRenderLib.XDoubleToFixed ((double)pixmapHeight); 

		 	/* here an example for a conical gradient, the angle is in degrees */ 
		 	conicalGradient.center.x = XRenderLib.XDoubleToFixed ((double) pixmapWidth / 2); 
		 	conicalGradient.center.y = XRenderLib.XDoubleToFixed ((double) pixmapHeight / 2); 
		 	conicalGradient.angle    = XRenderLib.XDoubleToFixed (180.0f);

		 	/* and an example for a radial gradient, coordinates are in window-space again */ 
		 	radialGradient.inner.x      = XRenderLib.XDoubleToFixed ((double) pixmapWidth / 2 - 100); 
		 	radialGradient.inner.y      = XRenderLib.XDoubleToFixed ((double) pixmapHeight / 2 - 100); 
		 	radialGradient.inner.radius = XRenderLib.XDoubleToFixed (45.0f); 
		 	radialGradient.outer.x      = XRenderLib.XDoubleToFixed ((double) pixmapWidth / 2); 
		 	radialGradient.outer.y      = XRenderLib.XDoubleToFixed ((double) pixmapHeight / 2); 
		 	radialGradient.outer.radius = XRenderLib.XDoubleToFixed (200.0f); 
			
			IntPtr gradientPict = IntPtr.Zero;
			if (_drawMode == DrawModeFgConical)
 				gradientPict = XRenderLib.XRenderCreateConicalGradient (display, ref conicalGradient, aColorStops, aColorList, (X11.TInt)4);
			else if (_drawMode == DrawModeFgRadial)
 				gradientPict = XRenderLib.XRenderCreateRadialGradient  (display, ref radialGradient,  aColorStops, aColorList, (X11.TInt)4);
			else
 				gradientPict = XRenderLib.XRenderCreateLinearGradient  (display, ref linearGradient,  aColorStops, aColorList, (X11.TInt)4);

		 	XRenderLib.XRenderComposite (display, X11.XRenderPictureOp.PictOpSrc,
							 			  gradientPict, 
							 			  IntPtr.Zero, 
							 			  destPict, 
							 			  (X11.TInt)0, (X11.TInt)0, 			(X11.TInt)0, (X11.TInt)0, 
							 			  (X11.TInt)0, (X11.TInt)0,				(X11.TUint)pixmapWidth, (X11.TUint)pixmapHeight); 

			XRenderLib.XRenderFreePicture (display, destPict);
			XRenderLib.XRenderFreePicture (display, gradientPict);
		}
		
		/// <summary> Handle the ConfigureNotify event. </summary>
		/// <param name="source"> The widget, the DialogEnd event is assigned to. <see cref="XtWmShell"/> </param>
		/// <param name="widget"> The widget, that is source of the event. <see cref="System.IntPtr"/> </param>
		/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
		/// <param name="parameters"> Additional parameters (as String[]). Not used. <see cref="System.IntPtr"/> </param>
		/// <param name="numParams"> The number of additional parameters. Not used. <see cref="XCardinal"/> </param>
		void HandleConfigureNotify (XtWmShell source, IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal numParams)
		{
			if (!(Xtlib.XtIsRealized (_shell) == (TBoolean)0))
		    {
				if (_initialConfigured == false)
				{
					if (!(Xtlib.XtIsRealized (_statusIcon) == (TBoolean)0))
					{
			
						IntPtr	display = Xtlib.XtDisplay (_shell);
						IntPtr	window  = Xtlib.XtWindow  (_shell);
						IntPtr smallExclamationIcon	= X11lib.XCreateBitmapFromData (display, window,
						                                  	XtResources.SMALL_EXCLAMATION_ICON_BITS,
				      										XtResources.SMALL_ICON_WIDTH,
						                                    XtResources.SMALL_ICON_HEIGHT);
						Arg[] statusIconArgs	= {	new Arg(XtNames.XtNleftBitmap, (XtArgVal)smallExclamationIcon) };
						Xtlib.XtSetValues (_statusIcon, statusIconArgs, (XCardinal)1);
					}
					_initialConfigured = true;
				}
			}
			
		}

		/// <summary> Implement the HandleDialogEnd event handler. </summary>
		/// <param name="label"> The label to set to the status bar. <see cref="System.String"/> </param>
		void HandleDialogEnd (XtDialog source)
		{
			OnSetStatusLabel ("Dialog ended, result: " + source.Result.ToString());
		}
		
		/// <summary> Implement the SetStatusLabel event handler. </summary>
		/// <param name="label"> The label to set to the status bar. <see cref="System.String"/> </param>
		private void HandleSetStatusLabel (string label)
		{
			if (!(Xtlib.XtIsRealized (_statusLabel) == (TBoolean)0))
			{
				Arg[] statusLabelArgs	= {	new Arg(XtNames.XtNlabel, label + "\0") };
				Xtlib.XtSetValues (_statusLabel, statusLabelArgs, (XCardinal)1);

				XtWidgetGeometry preferred = new XtWidgetGeometry();
				XtGeometryResult result = Xtlib.XtQueryGeometry (_statusLabel, IntPtr.Zero, ref preferred);
				Xtlib.XtResizeWidget (_statusLabel, preferred.width, preferred.height, preferred.border_width);
			}
		}
		
		#endregion
		
        // ###############################################################################
        // ### X T   C A L L B A C K   H A N D L E R
        // ###############################################################################

		#region Xt callback handler
		
		/// <summary> Set the position of a menu shell relative to it's caller widget. </summary>
		/// <param name="widget"> The menu shell widget. <see cref="System.IntPtr"/> </param>
		/// <param name="clientData"> Additional data from the client. <see cref="System.IntPtr"/> </param>
		/// <param name="callData"> Additional data defined for the call. <see cref="System.IntPtr"/> </param>
		public static void PlacePriorX11r4Menu (IntPtr widget, IntPtr clientData, IntPtr callData)
		{
			try
			{
				if (widget == IntPtr.Zero)
					return;
				
				int			menuX		= 0;
				int			menuY		= 0;
				IntPtr		parent		= Xtlib.XtParent (widget);
				
				if (parent != IntPtr.Zero)
				{
					XPosition rootX      = 0;
					XPosition rootY      = 0;
					
					// Determin screen coordinated of file menu command widget.
					Xtlib.XtTranslateCoords (parent, (XPosition)0, (XPosition)0, ref rootX, ref rootY);
					menuX += (int)rootX;
					menuY += (int)rootY;
					
					// Determine file menu command widget's height.
					
					XDimension	height = Xtlib.XtGetValueOfDimension (parent, XtNames.XtNheight);
					menuY += (int)height;
				}
				
				// Set menu shell position.
				Arg[]		shellSetArgs	= { new Arg(XtNames.XtNx, (XtArgVal)menuX) , new Arg(XtNames.XtNy, (XtArgVal)menuY) };
				Xtlib.XtSetValues (widget, shellSetArgs, (XCardinal)2);
			}
			catch (Exception e)
			{
				Console.WriteLine (e.StackTrace);
			}
		}
		
		/// <summary> The application close callback procedure. </summary>
		/// <param name="widget"> The widget, that initiated the callback procedure. <see cref="System.IntPtr"/> </param>
		/// <param name="clientData"> Additional callback data from the client. <see cref="System.IntPtr"/> </param>
		/// <param name="callData"> Additional data defined for the call. <see cref="System.IntPtr"/> </param>
		public void ApplicationCloseClick (IntPtr widget, IntPtr clientData, IntPtr callData)
		{
			Close ();
		}

		/// <summary>The fileentry one callback procedure.</summary>
		/// <param name="widget">The widget, that initiated the callback procedure.<see cref="System.IntPtr"/></param>
		/// <param name="clientData">Additional callback data from the client.<see cref="System.IntPtr"/></param>
		/// <param name="callData">Additional data defined for the call.<see cref="System.IntPtr"/></param>
		public void CommandBasedMenuEntryClick (IntPtr widget, IntPtr clientData, IntPtr callData)
		{
			_drawMode = DrawModeBackground;
			
			string currentMenuEntryName = Xtlib.XtNameAsString (widget);
			OnSetStatusLabel ("'" + currentMenuEntryName + "' selected.");
			
			IntPtr display = Xtlib.XtDisplay (_output);
			if (display == IntPtr.Zero)
			{
				Console.WriteLine ("ERROR: MenuEntryClick() - Unable to investigate the display.");
				return;
			}
			
			// Investigate installed X11 extensions.
			string listExtensions = String.Empty;
			IntPtr pListExtensionNames = X11lib.XListExtensions (display, out listExtensions);
			if (pListExtensionNames != IntPtr.Zero)
			{
				if (!string.IsNullOrEmpty (listExtensions))
				{
					Arg[] outputLabelArgs	= {	new Arg(XtNames.XtNlabel, listExtensions + "\0") };
					Xtlib.XtSetValues (_output, outputLabelArgs, (XCardinal)1);
				}				
			
				X11lib.XFreeExtensionList (pListExtensionNames);
			}

			TInt majorOpcode;
			TInt firstEvent;
			TInt firstError;
			if (X11lib.XQueryExtension (display, "RENDER", out majorOpcode, out firstEvent, out firstError) != true)
			{
				Console.WriteLine ("ERROR: MenuEntryClick() - Call to XQueryExtension() for 'RENDER' failed.");
				return;
			}
			
			if (XRenderLib.XRenderQueryExtension (display, out firstEvent, out firstError) != true)
			{
				Console.WriteLine ("ERROR: MenuEntryClick() - Call to XRenderQueryExtension() failed.");
				return;
			}
			
			TInt majorVersion = 0;
			TInt minorVersion = 0;
			if (XRenderLib.XRenderQueryVersion (display, out majorVersion, out minorVersion) <= 0)
			{
				Console.WriteLine ("ERROR: MenuEntryClick() - Call to XRenderQueryVersion() failed.");
				return;
			}
			if ((int)majorVersion > 0 || ((int)majorVersion == 0 || (int)minorVersion >= 7))
			{	;
				// O.K. All functions, this sample is based on, are supported.
				// - XRenderFillRectangle/XRenderFillRectangles
				// - XRenderComposite
				// - XRenderCreateLinearGradient/XRenderCreateConicalGradient/XRenderCreateRadialGradient
				// - ...
			}
			else
			{
				Console.WriteLine ("ERROR: draw() - Outdated version of XRender extension. Test might failed.");
			}
			
			int	width  = (int)Xtlib.XtGetValueOfDimension (_output, XtNames.XtNwidth);
			int	height = (int)Xtlib.XtGetValueOfDimension (_output, XtNames.XtNheight);
			int pixmapWidth  = Math.Max (1, width ); // Math.Max (1, ((int)(width  / 8)) * 8);
			int pixmapHeight = Math.Max (1, height); // Math.Max (1, ((int)(height / 8)) * 8);
			
			if (currentMenuEntryName == preX11r4MenuEntry3Name)	
			{
				// Assign background pixmap.
				X11lib.XSetWindowBackgroundPixmap (display, Xtlib.XtWindow(_output), IntPtr.Zero);
				X11lib.XSetWindowBackground (display, Xtlib.XtWindow(_output), X11lib.XWhitePixelOfScreen (X11lib.XScreenOfDisplay (display, (TInt)0)));
				// Apply background pixmap - invalidate the background.
				// X11lib.XClearWindow (display, Xtlib.XtWindow(_output)); // Doesn't provide invocation of Expose event!
				X11lib.XClearArea (display, Xtlib.XtWindow(_output), (TInt)0, (TInt)0, (TUint)width, (TUint)height, true);
				// Ensure processing.
				X11lib.XFlush (display);
				return;
			}
			
			IntPtr pixmapBackground  = X11lib.XCreatePixmap (display, Xtlib.XtWindow(_output),
			                                                 (TUint)pixmapWidth, (TUint)pixmapHeight, (TUint)24);
			if (pixmapBackground == IntPtr.Zero)
			{
				Console.WriteLine ("ERROR: MenuEntryClick() - Call to XCreatePixmap() failed.");
				return;
			}
			
			// ******************************************************************************************************
			// Attention: Fill memory (behind pixmap) with zero to ensure a defined start value for raster operations.
			IntPtr pGC = X11lib.XCreateGC (display, pixmapBackground, (TUlong)0, IntPtr.Zero);
			X11lib.XSetForeground (display, pGC, (TPixel)(0)); // Assume HIGH or TRUE color ==> black pixel.
			X11lib.XFillRectangle (display, pixmapBackground, pGC, 0, 0, pixmapWidth, pixmapHeight);
			X11lib.XFreeGC (display, pGC);		
			// ******************************************************************************************************
			
			XRenderLib.XRenderPictFormat xRenderPictFormat = XRenderLib.XRenderFindVisualFormat (display, X11lib.XDefaultVisual (display, 0));
			if (xRenderPictFormat.id != IntPtr.Zero)
			{
				// XRenderLib.XRenderPictFormat rpf = (XRenderLib.XRenderPictFormat)Marshal.PtrToStructure (pXRenderPictFormat, typeof(XRenderLib.XRenderPictFormat));

				XRenderLib.XRenderPictureAttributes pictureAttributes = new XRenderLib.XRenderPictureAttributes();
				pictureAttributes.poly_edge = (TInt)X11.XRenderPolyEdge.PolyEdgeSmooth;
				pictureAttributes.poly_mode = (TInt)X11.XRenderPolyEdge.PolyModeImprecise;
				
				IntPtr picture =  XRenderLib.XRenderCreatePicture (display, pixmapBackground, ref xRenderPictFormat,
								                                   XRenderCreatePictureValueMask.CPPolyEdge | XRenderCreatePictureValueMask.CPPolyMode,
								                                   ref pictureAttributes);
				if (picture != IntPtr.Zero)
				{
					if (currentMenuEntryName == preX11r4MenuEntry1Name)
					{
						TUshort alphaStep = (TUshort)0x00FF;
						XRenderLib.XRenderColor colorR = new XRenderLib.XRenderColor ((TUshort)(255 * (long)alphaStep), // premultilied red
						                                                              (TUshort)(  0 * (long)alphaStep), // premultilied green
						                                                              (TUshort)(  0 * (long)alphaStep), // premultilied blue
						                                                              alphaStep);
						XRenderLib.XRenderColor colorG = new XRenderLib.XRenderColor ((TUshort)(  0 * (long)alphaStep), // premultilied red
						                                                              (TUshort)(255 * (long)alphaStep), // premultilied green
						                                                              (TUshort)(  0 * (long)alphaStep), // premultilied blue
						                                                              alphaStep);
						XRenderLib.XRenderColor colorB = new XRenderLib.XRenderColor ((TUshort)(  0 * (long)alphaStep), // premultilied red
						                                                              (TUshort)(  0 * (long)alphaStep), // premultilied green
						                                                              (TUshort)(255 * (long)alphaStep), // premultilied blue
						                                                              alphaStep);
						
						XRenderLib.XRenderFillRectangle (display, XRenderPictureOp.PictOpOver, picture, ref colorR,
														 (TInt)(0), (TInt)(0), (TUint)(pixmapWidth), (TUint)(pixmapHeight / 3 + 30));
						XRenderLib.XRenderFillRectangle (display, XRenderPictureOp.PictOpOver, picture, ref colorG,
														 (TInt)(0), (TInt)(pixmapHeight / 3 - 10), (TUint)(pixmapWidth / 2 + 20), (TUint)(pixmapHeight * 2 / 3 + 10));
						XRenderLib.XRenderFillRectangle (display, XRenderPictureOp.PictOpOver, picture, ref colorB,
														 (TInt)(pixmapWidth / 2 - 20), (TInt)(pixmapHeight / 3 - 10), (TUint)(pixmapWidth / 2 + 20), (TUint)(pixmapHeight * 2 / 3 + 10));
					}
					else
					{
						TUshort alphaStep = (TUshort)0x008f;
						XRenderLib.XRenderColor color = new XRenderLib.XRenderColor ((TUshort)(172 * (long)alphaStep), // premultilied red
						                                                             (TUshort)(  8 * (long)alphaStep), // premultilied green
						                                                             (TUshort)(  4 * (long)alphaStep), // premultilied blue
						                                                             alphaStep);
	
						int numSteps = 80;
						for(int t = 0; t < numSteps; t++)
						{
							TInt  x = (TInt)  Math.Max (0L,                 (long)(pixmapWidth  * 0.5 * t / numSteps));
							TInt  y = (TInt)  Math.Max (0L,                 (long)(pixmapHeight * 0.5 * t / numSteps));
							TUint w = (TUint) Math.Min ((long)pixmapWidth,  (long)(pixmapWidth  - pixmapWidth  * 1.0 * t / numSteps));
							TUint h = (TUint) Math.Min ((long)pixmapHeight, (long)(pixmapHeight - pixmapHeight * 1.0 * t / numSteps));
							if ((int)w > 1 && (int)h > 1)
							XRenderLib.XRenderFillRectangle (display, XRenderPictureOp.PictOpOver, picture, ref color,
															 x, y, w, h);
							
							if ((long)color.red > (long)0x00ff && t % 1 == 0)
								color.red = (TUshort)((long)color.red - (32 * (long)alphaStep));
							if ((long)color.blue < (long)0xEfff && t % 2 == 0)
								color.blue = (TUshort)((long)color.blue + (4 * (long)alphaStep));
						}
					}
					// Assign background pixmap.
					X11lib.XSetWindowBackgroundPixmap (display, Xtlib.XtWindow(_output), pixmapBackground);
					// Apply background pixmap - invalidate the background.
					// X11lib.XClearWindow (display, Xtlib.XtWindow(_output)); // Doesn't provide invocation of Expose event!
					X11lib.XClearArea (display, Xtlib.XtWindow(_output), (TInt)0, (TInt)0, (TUint)pixmapWidth, (TUint)pixmapHeight, true);
					// Ensure processing.
					X11lib.XFlush (display);
				}
				// Finished drawing operations - picture is obsolete.
				XRenderLib.XRenderFreePicture(display, picture);
			}
			// Finished background pixmap handling - pixmap is obsolete. 
			X11lib.XFreePixmap (display, pixmapBackground);
		}
		
		/// <summary>The fileentry one callback procedure.</summary>
		/// <param name="widget">The widget, that initiated the callback procedure.<see cref="System.IntPtr"/></param>
		/// <param name="clientData">Additional callback data from the client.<see cref="System.IntPtr"/></param>
		/// <param name="callData">Additional data defined for the call.<see cref="System.IntPtr"/></param>
		public void SmeBasedMenuEntryClick (IntPtr widget, IntPtr clientData, IntPtr callData)
		{
			string currentMenuEntryName = Xtlib.XtNameAsString (widget);
			OnSetStatusLabel ("'" + currentMenuEntryName + "' selected.");
			
			if (currentMenuEntryName == x11r4entry1Name)
				_drawMode = DrawModeFgLinear;
			else if (currentMenuEntryName == x11r4entry2Name)
				_drawMode = DrawModeFgConical;
			else if (currentMenuEntryName == x11r4entry3Name)
				_drawMode = DrawModeFgRadial;
			else
				_drawMode = DrawModeInitial;

			// X11lib.XClearWindow (Xtlib.XtDisplay (_output), Xtlib.XtWindow(_output)); // Doesn't provide invocation of Expose event!
			X11lib.XClearArea (Xtlib.XtDisplay (_output), Xtlib.XtWindow(_output), (TInt)0, (TInt)0, (TUint)0, (TUint)0, true);
		}
		
		#endregion

	}
}