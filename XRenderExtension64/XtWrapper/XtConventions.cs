using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;

using X11;

namespace Xt
{

	public enum XtOrientation
	{
		XtorientHorizontal,
		XtorientVertical
	}
	
	public struct Box
	{
	    public short x1, x2, y1, y2;
	}

	public struct Rectangle
	{
	    public short x, y, width, height;
	}

	public struct XRegion
	{
	    public int						size;
	    public int						numRects;
	    public IntPtr					rects;					/* Box[] */
	    public Box						extents;
	}

	public struct XrmResourc
	{
		public TLong					xrm_name;				/* Resource name quark */
		public TLong					xrm_class;				/* Resource class quark */
		public TLong					xrm_type;				/* Resource representation type quark */
		public XCardinal				xrm_size;				/* Size in bytes of representation */
		public TChar					xrm_offset;				/* -offset-1 */
		public TLong					xrm_default_type;		/* Default representation type quark */
		public IntPtr					xrm_default_addr;		/* XtPointer: Default resource address */
	}
	
	public struct XtResource
	{
		public TChar[]					resource_name;			/* Resource name */
		public TChar[]					resource_class;			/* Resource class */
		public TChar[]					resource_type;			/* Representation type desired */
		public XCardinal				resource_size;			/* Size in bytes of representation */
		public XCardinal				resource_offset;		/* Offset from base to put resource value */
		public TChar[]					default_type;			/* representation type of specified default */
		public IntPtr					default_addr;			/* XtPointer: Address of default resource */
	}
	
	// Tested: O.K.
	public struct XtWidgetGeometry
	{
		public XtGeometryMask			request_mode;			/* Flag the fields, that matter. */
		public XPosition				x, y;
		public XDimension				width, height;
		public XDimension				border_width;
		public IntPtr					sibling;				/* Widget: */
		public TInt						stack_mode; 
	}
	
	[Flags]
	public enum XtGeometryMask : uint					/* Size is not unsigned long, as literature says (unsigned long fails for 64 bit)!!! */
	{
		CWX						= (1<<0),
		CWY						= (1<<1),
		CWWidth					= (1<<2),
		CWHeight				= (1<<3),
		CWBorderWidth			= (1<<4),
		CWSibling				= (1<<5),
		CWStackMode				= (1<<6)
	}
	
	public enum XtGeometryResult : int
	{
		XtGeometryYes,									/* The proposed change is acceptable to the child without modifications. */
														/* This means that the proposed changes are exactly what the child would prefer. */
		XtGeometryNo,									/* The child would prefer that no changes were made to its current geometry. */
														/* The parent can respect or ignore this response. */
		XtGeometryAlmost,								/* The child does not agree entirely with the proposed change. At least one field */
														/* of XtWidgetGeometry is different. The parent can respect or ignore this response. */
		XtGeometryDone 
	}

	public enum XtJustify
	{
	    XtJustifyLeft,									/* justify text to left side of button   */
	    XtJustifyCenter,								/* justify text in center of button      */
	    XtJustifyRight									/* justify text to right side of button  */
	}
	
	public enum XtGrabKind
	{
		XtGrabNone,
		XtGrabNonexclusive,
		XtGrabExclusive
	}
	
	public enum XawTextEditType
	{
		XawtextRead,
		XawtextAppend,
		XawtextEdit
	}

	public struct XtCallbackRec
	{
		public XtCallbackProc			callback;				/*	*/
		public IntPtr					closure;				/* XtPointer: */
	}
	
	public struct XtGrabRec
	{
	    public XtGrabRec[]				next;
	    public IntPtr					widget;					/* Widget: */
	    public TUint					exclusive;
	    public TUint					spring_loaded;
	}
	
	// Tested: O.K.
	/// <summary> Action map entry, containing logical action name and action procedure. </summary>
	public struct XtActionsRec
	{
		/// <summary> The logical action name. The C structure name is 'string'. </summary>
		/// <remarks> Define the action name including terminating NULL like: X11Utils.StringToSByteArray ("quitAction\0") </remarks>
		public TChar[]			str;
		
		/// <summary> The pointer to a delegate of prototype XtActionProc. </summary>
		/// <remarks> Can be created like: Marshal.GetFunctionPointerForDelegate(new XtActionProc (QuitAction)) </remarks>
		public IntPtr		 	proc; // XtActionProc
		
		/// <summary> The initializing constructor to simplify creation of an XtActionsRec array. </summary>
		/// <param name="actionName"> The ogical action name. <see cref="TChar[]"/> </param>
		/// <param name="delegatePointer"> The pointer to a delegate of prototype XtActionProc. <see cref="System.IntPtr"/> </param>
		public XtActionsRec		(TChar[] actionName, IntPtr delegatePointer)
		{
			str				= actionName;
			proc			= delegatePointer;
		}
	}

	public struct XtEventRec
	{
	    public XtEventRec[]				next;
	    public EventMask				mask;
	    public XtEventHandler			proc;
	    public IntPtr					closure;				/* XtPointer: */
	    public TUint					selects;
	    public TUint					has_type_specifier;
	    public TUint					async;
	}

	public struct XtTMRec
	{
	    public IntPtr					translations;			/* XtTranslations: private to Translation Manager    */
	    public XtActionProc[]			proc_table;				/* XtBoundActions: procedure bindings for actions    */
	    public IntPtr					current_state;      	/* StatePtr: Translation Manager state ptr     */
	    public TUlong					lastEventTime;
	}

	public struct WidgetRec
	{
	}
	
	public delegate void     			XtProc					();
	public delegate void     			XtWidgetProc			(IntPtr widget);
	public delegate void     			XtWidgetClassProc		(IntPtr widget_class);
	public delegate void     			XtArgsProc				(IntPtr widget, Arg[] args, ref XCardinal num_args);
	
	// Tested: O.K.
	/// <summary> Declare the prototype of a callback procedure. </summary>
	/// <param name="widget"> The widget, that is source of the callback procedure. <see cref="System.IntPtr"/> </param>
	/// <param name="clientData"> Additional callback data from the client. <see cref="System.IntPtr"/> </param>
	/// <param name="callData"> Additional data defined for the call. <see cref="System.IntPtr"/> </param>
	public delegate void     			XtCallbackProc			(IntPtr widget, IntPtr clientData, IntPtr callData);

	public delegate void     			XtInitProc				(IntPtr requestWidget, IntPtr newWidget, Arg[] args, ref XCardinal num_args);
	public delegate void     			XtExposeProc			(IntPtr widget, ref XEvent xevent, XRegion region);
	public delegate void     			XtAlmostProc			(IntPtr oldWidget, IntPtr newWidget, ref XtWidgetGeometry request, ref XtWidgetGeometry reply);
	public delegate TBoolean			XtAcceptFocusProc		(IntPtr widget, IntPtr time_pointer);
	public delegate void     			XtStringProc			(IntPtr widget, TChar[] str);

	public delegate XtGeometryResult	XtGeometryHandler		(IntPtr widget, ref XtWidgetGeometry request, ref XtWidgetGeometry reply);
	
	public delegate void				XtEventHandler			(IntPtr widget, IntPtr clientData, ref XEvent xevent, ref TBoolean continueToDispatchReturn);
	
	// Tested: O.K.
	/// <summary> Declare the prototype of an action procedure. </summary>
	/// <param name="widget"> The widget, that is source of the action procedure. <see cref="System.IntPtr"/> </param>
	/// <param name="xevent"> The event, that is invoked. <see cref="XEvent"/> </param>
	/// <param name="parameters"> Additional parameters (as String[]). <see cref="System.IntPtr"/> </param>
	/// <param name="num_params"> The number of additional parameters. <see cref="XCardinal"/> </param>
	public delegate void				XtActionProc			(IntPtr widget, ref XEvent xevent, IntPtr parameters, ref XCardinal numParams);
	
	public delegate void				XtRealizeProc			(IntPtr widget, ref XtValueMask mask, ref X11lib.XSetWindowAttributes attributes);
	public delegate TBoolean			XtChangeSensitiveFunc	(IntPtr widget);
	public delegate TBoolean			XtSetValuesFunc			(IntPtr old_widget, IntPtr request_widget, IntPtr new_widget, Arg[] args, ref XCardinal num_args);
	public delegate TBoolean			XtArgsFunc				(IntPtr widget, Arg[] args, ref XCardinal num_args);

}