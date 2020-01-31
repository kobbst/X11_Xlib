using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xt
{
	/// <summary> Central register of callback procedures. A widget's callback should be *** STATIC *** to avoid problems accessing instance members. </summary>
	/// <remarks> Managed code to native code marshaled callback procedures lose the instance relation. Neither "this" nor instance members are accesible. </remarks>
	public static class CallBackMarshaler
	{

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string		CLASS_NAME					= "CallBackMarshaller";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary> The list of registered callbacks. </summary>
		private static Dictionary<IntPtr, XtCallbackProc> _list = new Dictionary<IntPtr, XtCallbackProc> ();
		
		/// <summary> The managed code to native code marshalable generic callback procedure. </summary>
		private static IntPtr _callbackPtr = Marshal.GetFunctionPointerForDelegate(new XtCallbackProc (CallBackMarshaler.MarshalCallback));

        #endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary> Add a widget specific callback registration. </summary>
		/// <param name="widget"> The widget to register the callback for. <see cref="System.IntPtr"/> </param>
		/// <param name="callback"> The callback to execute. <see cref="XtCallbackProc"/> </param>
		/// <returns> The managed code to native code marshalable generic callback procedure. <see cref="System.IntPtr"/> </returns>
		public static IntPtr Add (IntPtr widget, XtCallbackProc callback)
		{
			if (widget == IntPtr.Zero)
			{
				Console.WriteLine (CLASS_NAME + "::Add () WARNING: Widget is zero.");
			}
			if (callback == null)
			{
				Console.WriteLine (CLASS_NAME + "::Add () WARNING: Callback is zero.");
			}

			if (_list.ContainsKey (widget))
		    {
				// Debug code.
				Console.WriteLine (CLASS_NAME + "::Add() WARNING: Callback already registered for widget (" + Xtlib.XtNameAsString (widget) + "). Perform refresh.");
				_list[widget] = callback;
			}
			else
				_list.Add (widget, callback);

			return _callbackPtr;
		}
		
		/// <summary> Remove a widget specific callback registration. </summary>
		/// <param name="widget"> The widget to unregister the callback for. <see cref="System.IntPtr"/> </param>
		public static void Remove (IntPtr widget)
		{
			if (widget == IntPtr.Zero)
			{
				Console.WriteLine (CLASS_NAME + "::Remove () WARNING: Widget pointer is zero.");
			}
			if (_list.ContainsKey (widget))
			{
				_list.Remove (widget);
			}
			else
			{
				Console.WriteLine (CLASS_NAME + "::Reassign () WARNING: newWidget pointer was not registered.");
			}
		}
		
		/// <summary> Reassign a callback to a new widget. </summary>
		/// <param name="oldWidget"> The widget a calback is currently assigned to. <see cref="IntPtr"/> </param>
		/// <param name="newWidget"> The widget a callback should be reassigned to. <see cref="IntPtr"/> </param>
		public static void Reassign (IntPtr oldWidget, IntPtr newWidget)
		{
			// Complete function code is an extension for Motif.
			// Motif menu bars and menus assign "XmNsimpleCallback" at a moment, the widget ID is not known to the caller.
			if (_list.ContainsKey (oldWidget))
			{
				XtCallbackProc callback = _list[oldWidget];
				_list.Remove (oldWidget);
				_list.Add (newWidget, callback);
			}
			else
			{
				Console.WriteLine (CLASS_NAME + "::Reassign () WARNING: newWidget pointer was not registered.");
			}
		}
		/// <summary> The generic callback procedure, that marshals to the widget specific callback procedure. </summary>
		/// <param name="widget"> The widget, that initiated the callback procedure. <see cref="System.IntPtr"/> </param>
		/// <param name="clientData"> Additional callback data from the client. <see cref="System.IntPtr"/> </param>
		/// <param name="callData"> Additional data defined for the call. <see cref="System.IntPtr"/> </param>
		/// <remarks> The prototype must match the XtCallbackProc delegate. </remarks>
		public static void MarshalCallback (IntPtr widget, IntPtr clientData, IntPtr callData)
		{
			// Debug code.
			// IntPtr hName = Xtlib.XtName (widget);
			// string sName = Marshal.PtrToStringAuto(hName);
			// Console.WriteLine (CLASS_NAME + "::MarshalCallback () INFORMATION: Widget name is: " + sName);

			if (_list.ContainsKey (widget))
			{
				_list[widget] (widget, clientData, callData);
				return;
			}
			
			// Subsequent code is an extension for Motif.
			// Motif menu bars and menus assign "XmNsimpleCallback" to their child widgets.
			IntPtr parentWidget = Xtlib.XtParent (widget);
			if (parentWidget != IntPtr.Zero && _list.ContainsKey (parentWidget))
			{
				_list[parentWidget] (widget, clientData, callData);
				return;
			}
			else
			{
				// Debug code.
				Console.WriteLine (CLASS_NAME + "::MarshalCallback () WARNING: Widget (" + Xtlib.XtNameAsString (widget) + ") pointer and widget's parent pointer are not registered.");
			}
		}
		
        #endregion

	}
}

