using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Xt
{
	/// <summary> Central register of callback procedures. A widget's callback should be *** STATIC *** to avoid problems accessing instance members. </summary>
	/// <remarks> Managed code to native code marshaled callback procedures lose the instance relation. Neither "this" nor instance members are accesible. </remarks>
	public static class ActionMarshaler
	{

        // ###############################################################################
        // ### I N N E R   C L A S S E S
        // ###############################################################################

        #region Inner classes

        /// <summary> The implementation of a 'widget + event-type' key for a dictionary. </summary>
        private class ActionKey : IEquatable<ActionKey>
		{
			/// <summary> The widget this key is associated to. </summary>
			public IntPtr Widget;
			
			/// <summary> The event type this key is associated to. </summary>
			public X11.XEventName EventType;
			
			/// <summary> The initializing constructor. </summary>
			/// <param name="widget"> The widget this key is associated to. <see cref="System.IntPtr"/> </param>
			/// <param name="eventType"> The event type this key is associated to. <see cref="X11.XEventName"/> </param>
			public ActionKey (IntPtr widget, X11.XEventName eventType)
			{
				Widget = widget;
				EventType = eventType;
			}
			
			/// <summary> The IEquatableEquals () implementation. </summary>
			/// <param name="other"> The action key to compare to. <see cref="ActionKey"/> </param>
			/// <returns> True in case of equality, or false otherwise. <see cref="System.Boolean"/> </returns>
			public bool Equals (ActionKey other)
			{
				if (other == null)
					return false;

				if (!other.Widget.Equals(Widget))
					return false;

				if (other.EventType != EventType)
					return false;
				
				return true;
			}
		}
		
		/// <summary> Implementation of a comparer for action keys. </summary>
		private class ActionKeyComparer : IEqualityComparer<ActionKey>
		{
			/// <summary> The IEqualityComparer.Equals () implementation. </summary>
			/// <param name="x"> One action keys to compare. <see cref="ActionKey"/> </param>
			/// <param name="y"> The other action keys to compare. <see cref="ActionKey"/> </param>
			/// <returns> True on equality, or false otherwise. <see cref="System.Boolean"/> </returns>
			public bool Equals(ActionKey x, ActionKey y)
			{
				if (x == null && y == null)
					return true;
				if (x == null)
					return false;
				return x.Equals (y);
			}
			
			/// <summary> The IEqualityComparer.GetHashCode () implementation. </summary>
			/// <param name="obj"> The action keys to calcualte a hash code for. <see cref="ActionKey"/> </param>
			/// <returns> The hash code for indicatd action key. <see cref="System.Int32"/> </returns>
			public int GetHashCode (ActionKey obj)
			{
				return obj.Widget.ToInt32 () + (int)obj.EventType;
			}
		}
		
        #endregion

        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string		CLASS_NAME					= "ActionMarshaler";
		
        #endregion

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes
		
		/// <summary> The list of registered callbacks. </summary>
		private static Dictionary<ActionKey, XtActionProc> _list = new Dictionary<ActionKey, XtActionProc> (new ActionKeyComparer ());
		
		/// <summary> The managed code to native code marshalable generic action procedure. </summary>
		private static IntPtr _actionPtr = Marshal.GetFunctionPointerForDelegate(new XtActionProc (ActionMarshaler.MarshalAction));
		
        #endregion
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

		#region Properties
		
		/// <summary> Get the function pointer of the generic action procedure. </summary>
		public static IntPtr ActionProc
		{	get	{	return _actionPtr;	}
		}
		
        #endregion
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################

		#region Methods
		
		/// <summary> Add a widget specific action registration. </summary>
		/// <param name="widget"> The widget to register the action for. <see cref="System.IntPtr"/> </param>
		/// <param name="eventType"> The event type to register the action for. <see cref="X11.XEventName"/> </param>
		/// <param name="action"> The action to execute. <see cref="XtActionProc"/> </param>
		/// <returns> The managed code to native code marshalable generic action procedure. <see cref="System.IntPtr"/> </returns>
		public static IntPtr Add (IntPtr widget, X11.XEventName eventType, XtActionProc action)
		{
			ActionKey actionKey = new ActionKey (widget, eventType);
			
			if (_list.ContainsKey (actionKey))
		    {
				Console.WriteLine (CLASS_NAME + "::Add() WARNING: ActionProc already registered for widget. Perform refresh.");
				_list[actionKey] = action;
			}
			else
				_list.Add (actionKey, action);

			return _actionPtr;
		}
		
		/// <summary> Remove a widget specific action registration. </summary>
		/// <param name="widget"> The widget to unregister the action for. <see cref="IntPtr"/> </param>
		/// <param name="eventType"> The event type to unregister the action for. <see cref="X11.XEventName"/> </param>
		public static void Remove (IntPtr widget,  X11.XEventName eventType)
		{
			ActionKey actionKey = new ActionKey (widget, eventType);
			
			_list.Remove (actionKey);
		}
		
		/// <summary> The generic callback procedure, that marshals to the widget specific callback procedure. </summary>
		/// <param name="widget"> The widget, that is source of the event. <see cref="System.IntPtr"/> </param>
		/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
		/// <param name="parameters"> Additional parameters (as String[]). <see cref="System.IntPtr"/> </param>
		/// <param name="num_params"> The number of additional parameters. <see cref="XCardinal"/> </param>
		/// <remarks> The prototype must match the XtActionProc delegate. </remarks>
		public static void MarshalAction (IntPtr widget, ref X11.XEvent xevent, IntPtr parameters, ref X11.XCardinal num_params)
		{
			ActionKey actionKey = new ActionKey (widget, xevent.type);
			
			if (_list.ContainsKey (actionKey))
			{
				_list[actionKey] (widget, ref xevent, parameters, ref num_params);
			}
			else
			{
				// Debug code.
				Console.WriteLine (CLASS_NAME + "::MarshalAction () WARNING: Widget (" + Xtlib.XtNameAsString (widget) + ") pointer is not registered.");
			}
		}
		
        #endregion

	}
}

