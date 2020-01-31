using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using X11;

namespace Xt
{
	public static class XtNames
	{
		public static TChar[] XtNaccelerators = X11.X11Utils.StringToSByteArray ("accelerators\0");
		public static TChar[] XtNallowHoriz = X11.X11Utils.StringToSByteArray ("allowHoriz\0");
		public static TChar[] XtNallowVert = X11.X11Utils.StringToSByteArray ("allowVert\0");
		public static TChar[] XtNancestorSensitive = X11.X11Utils.StringToSByteArray ("ancestorSensitive\0");
		public static TChar[] XtNbackground = X11.X11Utils.StringToSByteArray ("background\0");
		public static TChar[] XtNbackgroundPixmap = X11.X11Utils.StringToSByteArray ("backgroundPixmap\0");
		public static TChar[] XtNbitmap = X11.X11Utils.StringToSByteArray ("bitmap\0");
		public static TChar[] XtNborderColor = X11.X11Utils.StringToSByteArray ("borderColor\0");
		public static TChar[] XtNborder = X11.X11Utils.StringToSByteArray ("borderColor\0");
		public static TChar[] XtNborderPixmap = X11.X11Utils.StringToSByteArray ("borderPixmap\0");
		public static TChar[] XtNborderWidth = X11.X11Utils.StringToSByteArray ("borderWidth\0");
		public static TChar[] XtNbottom = X11.X11Utils.StringToSByteArray ("bottom\0");
		public static TChar[] XtNcallback = X11.X11Utils.StringToSByteArray ("callback\0");
		public static TChar[] XtNchildren = X11.X11Utils.StringToSByteArray ("children\0");
		public static TChar[] XtNcolormap = X11.X11Utils.StringToSByteArray ("colormap\0");
		public static TChar[] XtNdefaultDistance = X11.X11Utils.StringToSByteArray ("defaultDistance\0");
		public static TChar[] XtNdepth = X11.X11Utils.StringToSByteArray ("depth\0");
		public static TChar[] XtNdestroyCallback = X11.X11Utils.StringToSByteArray ("destroyCallback\0");
		public static TChar[] XtNeditType = X11.X11Utils.StringToSByteArray ("editType\0");
		public static TChar[] XtNfile = X11.X11Utils.StringToSByteArray ("file\0");
		public static TChar[] XtNfont = X11.X11Utils.StringToSByteArray ("font\0");
		public static TChar[] XtNforceBars = X11.X11Utils.StringToSByteArray ("forceBars\0");
		public static TChar[] XtNforeground = X11.X11Utils.StringToSByteArray ("foreground\0");
		public static TChar[] XtNfunction = X11.X11Utils.StringToSByteArray ("function\0");
		public static TChar[] XtNheight = X11.X11Utils.StringToSByteArray ("height\0");
		public static TChar[] XtNhighlight = X11.X11Utils.StringToSByteArray ("highlight\0");
		public static TChar[] XtNhSpace = X11.X11Utils.StringToSByteArray ("hSpace\0");
		public static TChar[] XtNicon = X11.X11Utils.StringToSByteArray ("icon\0");
		public static TChar[] XtNindex = X11.X11Utils.StringToSByteArray ("index\0");
		public static TChar[] XtNinitialResourcesPersistent = X11.X11Utils.StringToSByteArray ("initialResourcesPersistent\0");
		public static TChar[] XtNinnerHeight = X11.X11Utils.StringToSByteArray ("innerHeight\0");
		public static TChar[] XtNinnerWidth = X11.X11Utils.StringToSByteArray ("innerWidth\0");
		public static TChar[] XtNinnerWindow = X11.X11Utils.StringToSByteArray ("innerWindow\0");
		public static TChar[] XtNinsertPosition = X11.X11Utils.StringToSByteArray ("insertPosition\0");
		public static TChar[] XtNinternalHeight = X11.X11Utils.StringToSByteArray ("internalHeight\0");
		public static TChar[] XtNinternalWidth = X11.X11Utils.StringToSByteArray ("internalWidth\0");
		public static TChar[] XtNjumpProc = X11.X11Utils.StringToSByteArray ("jumpProc\0");
		public static TChar[] XtNjustify = X11.X11Utils.StringToSByteArray ("justify\0");
		public static TChar[] XtNknobHeight = X11.X11Utils.StringToSByteArray ("knobHeight\0");
		public static TChar[] XtNknobIndent = X11.X11Utils.StringToSByteArray ("knobIndent\0");
		public static TChar[] XtNknobPixel = X11.X11Utils.StringToSByteArray ("knobPixel\0");
		public static TChar[] XtNknobWidth = X11.X11Utils.StringToSByteArray ("knobWidth\0");
		public static TChar[] XtNlabel = X11.X11Utils.StringToSByteArray ("label\0");
		public static TChar[] XtNleftBitmap = X11.X11Utils.StringToSByteArray ("leftBitmap\0");
		public static TChar[] XtNlength = X11.X11Utils.StringToSByteArray ("length\0");
		public static TChar[] XtNlowerRight = X11.X11Utils.StringToSByteArray ("lowerRight\0");
		public static TChar[] XtNmappedWhenManaged = X11.X11Utils.StringToSByteArray ("mappedWhenManaged\0");
		public static TChar[] XtNmenuEntry = X11.X11Utils.StringToSByteArray ("menuEntry\0");
		public static TChar[] XtNmenuName = X11.X11Utils.StringToSByteArray ("menuName\0");
		public static TChar[] XtNname = X11.X11Utils.StringToSByteArray ("name\0");
		public static TChar[] XtNnotify = X11.X11Utils.StringToSByteArray ("notify\0");
		public static TChar[] XtNnumChildren = X11.X11Utils.StringToSByteArray ("numChildren\0");
		public static TChar[] XtNorientation = X11.X11Utils.StringToSByteArray ("orientation\0");
		public static TChar[] XtNparameter = X11.X11Utils.StringToSByteArray ("parameter\0");
		public static TChar[] XtNpixmap = X11.X11Utils.StringToSByteArray ("pixmap\0");
		public static TChar[] XtNpopupCallback = X11.X11Utils.StringToSByteArray ("popupCallback\0");
		public static TChar[] XtNpopdownCallback = X11.X11Utils.StringToSByteArray ("popdownCallback\0");
		public static TChar[] XtNresize = X11.X11Utils.StringToSByteArray ("resize\0");
		public static TChar[] XtNreverseVideo = X11.X11Utils.StringToSByteArray ("reverseVideo\0");
		public static TChar[] XtNscreen = X11.X11Utils.StringToSByteArray ("screen\0");
		public static TChar[] XtNscrollProc = X11.X11Utils.StringToSByteArray ("scrollProc\0");
		public static TChar[] XtNscrollDCursor = X11.X11Utils.StringToSByteArray ("scrollDCursor\0");
		public static TChar[] XtNscrollHCursor = X11.X11Utils.StringToSByteArray ("scrollHCursor\0");
		public static TChar[] XtNscrollLCursor = X11.X11Utils.StringToSByteArray ("scrollLCursor\0");
		public static TChar[] XtNscrollRCursor = X11.X11Utils.StringToSByteArray ("scrollRCursor\0");
		public static TChar[] XtNscrollUCursor = X11.X11Utils.StringToSByteArray ("scrollUCursor\0");
		public static TChar[] XtNscrollVCursor = X11.X11Utils.StringToSByteArray ("scrollVCursor\0");
		public static TChar[] XtNselection = X11.X11Utils.StringToSByteArray ("selection\0");
		public static TChar[] XtNselectionArray = X11.X11Utils.StringToSByteArray ("selectionArray\0");
		public static TChar[] XtNsensitive = X11.X11Utils.StringToSByteArray ("sensitive\0");
		public static TChar[] XtNshown = X11.X11Utils.StringToSByteArray ("shown\0");
		public static TChar[] XtNspace = X11.X11Utils.StringToSByteArray ("space\0");
		public static TChar[] XtNstring = X11.X11Utils.StringToSByteArray ("string\0");
		public static TChar[] XtNtextOptions = X11.X11Utils.StringToSByteArray ("textOptions\0");
		public static TChar[] XtNtextSink = X11.X11Utils.StringToSByteArray ("textSink\0");
		public static TChar[] XtNtextSource = X11.X11Utils.StringToSByteArray ("textSource\0");
		public static TChar[] XtNthickness = X11.X11Utils.StringToSByteArray ("thickness\0");
		public static TChar[] XtNthumb = X11.X11Utils.StringToSByteArray ("thumb\0");
		public static TChar[] XtNthumbProc = X11.X11Utils.StringToSByteArray ("thumbProc\0");
		public static TChar[] XtNtitle = X11.X11Utils.StringToSByteArray ("title\0");
		public static TChar[] XtNtop = X11.X11Utils.StringToSByteArray ("top\0");
		public static TChar[] XtNtranslations = X11.X11Utils.StringToSByteArray ("translations\0");
		public static TChar[] XtNunrealizeCallback = X11.X11Utils.StringToSByteArray ("unrealizeCallback\0");
		public static TChar[] XtNupdate = X11.X11Utils.StringToSByteArray ("update\0");
		public static TChar[] XtNuseBottom = X11.X11Utils.StringToSByteArray ("useBottom\0");
		public static TChar[] XtNuseRight = X11.X11Utils.StringToSByteArray ("useRight\0");
		public static TChar[] XtNvalue = X11.X11Utils.StringToSByteArray ("value\0");
		public static TChar[] XtNvSpace = X11.X11Utils.StringToSByteArray ("vSpace\0");
		public static TChar[] XtNwidth = X11.X11Utils.StringToSByteArray ("width\0");
		public static TChar[] XtNwindow = X11.X11Utils.StringToSByteArray ("window\0");
		public static TChar[] XtNx = X11.X11Utils.StringToSByteArray ("x\0");
		public static TChar[] XtNy = X11.X11Utils.StringToSByteArray ("y\0");
		
		public static TChar[] XtNleft = X11.X11Utils.StringToSByteArray ("left\0");
		public static TChar[] XtNright = X11.X11Utils.StringToSByteArray ("right\0");
		public static TChar[] XtNfromHoriz = X11.X11Utils.StringToSByteArray ("fromHoriz\0");
		public static TChar[] XtNfromVert = X11.X11Utils.StringToSByteArray ("fromVert\0");
		public static TChar[] XtNhorizDistance = X11.X11Utils.StringToSByteArray ("horizDistance\0");
		public static TChar[] XtNvertDistance = X11.X11Utils.StringToSByteArray ("vertDistance\0");
		public static TChar[] XtNresizable = X11.X11Utils.StringToSByteArray ("resizable\0");

	}
	
	/// <summary> A smart wrapper class for a native string. </summary>
	public class XtString : IDisposable
	{
		// Hold the native string.
		private IntPtr	_data = IntPtr.Zero;
		
		// Determine whether to auto dispose the native string.
		private bool	_autoDisposeData = true;
		
		 // Track whether Dispose has been called.
        private bool	_disposed = false;
		
		/// <summary> HIDDEN default constructor. </summary>
		private XtString (){}
		
		/// <summary> Initializing constructor for auto dispose smart string. </summary>
		/// <param name="text"> The text of the smart string. <see cref="System.String"/> </param>
		public XtString (string text)
		{
			_data = Xtlib.TransformToCharArray (text);
		}
		
		/// <summary> Initializing constructor for smart string. </summary>
		/// <param name="text"> The text of the smart string. <see cref="System.String"/> </param>
		/// <param name="autoDisposeData"> Determine whether to auto dispose the native string. <see cref="System.Boolean"/> </param>
		public XtString (string text, bool autoDisposeData)
		{
			_data = Xtlib.TransformToCharArray (text);
			_autoDisposeData = autoDisposeData;
		}
		
		/// <summary> Get the native string adress. </summary>
		public IntPtr Data
		{
			get {	return _data;	}
		}
		
		/// <summary> Implement IDisposable. </summary>
        /// <remarks> Do not make this method virtual. A derived class should not be able to override this method. </remarks>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
		
		/// <summary> Execute disposing for user calls and GC calls. </summary>
		/// <param name="disposing"> Determine whether called by user (true) or GC (false). <see cref="System.Boolean"/> </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(_disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    ;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
				if (_autoDisposeData == true)
					Xtlib.TransformFreeCharArray (_data);

                // Note disposing has been done.
                _disposed = true;

            }
        }
		
		
        /// <summary> Finalize. </summary>
        /// <remarks> This destructor will run only if the Dispose method does not get called. It gives the class
        /// the opportunity to finalize. Do not provide destructors in types derived from this class. </remarks>
		~XtString()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose(false);
        }
	}
	
	/// <summary> A smart wrapper class for a native string array. </summary>
	public class XtStringArray : IDisposable
	{
		// Hold the native string array.
		private IntPtr	_data = IntPtr.Zero;
		
		// Determine whether to auto dispose the native string array.
		private bool	_autoDisposeData = true;
		
		 // Track whether Dispose has been called.
        private bool	_disposed = false;
		
		/// <summary> HIDDEN default constructor. </summary>
		private XtStringArray (){}
		
		/// <summary> Initializing constructor for auto dispose smart string array. </summary>
		/// <param name="textToSplit"> The text to split into a smart string array. <see cref="System.String"/> </param>
		/// <param name="delimiter"> The delimiter to use, to split the text into an array. <see cref="System.Char"/> </param>
		public XtStringArray (string textToSplit, char delimiter)
		{
			_data = Xtlib.TransformToArrayOfCharArray (textToSplit, (TChar)delimiter);
		}
		
		/// <summary> Initializing constructor for smart string array. </summary>
		/// <param name="textToSplit"> The text to split into a smart string array. <see cref="System.String"/> </param>
		/// <param name="delimiter"> The delimiter to use, to split the text into an array. <see cref="System.Char"/> </param>
		/// <param name="autoDisposeData"> Determine whether to auto dispose the native string array. <see cref="System.Boolean"/> </param>
		public XtStringArray (string textToSplit, char delimiter, bool autoDisposeData)
		{
			_data = Xtlib.TransformToArrayOfCharArray (textToSplit, (TChar)delimiter);
			_autoDisposeData = autoDisposeData;
		}
		
		/// <summary> Get the native string array adress. </summary>
		public IntPtr Data
		{
			get {	return _data;	}
		}
		
		/// <summary> Implement IDisposable. </summary>
        /// <remarks> Do not make this method virtual. A derived class should not be able to override this method. </remarks>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
		
		/// <summary> Execute disposing for user calls and GC calls. </summary>
		/// <param name="disposing"> Determine whether called by user (true) or GC (false). <see cref="System.Boolean"/> </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(_disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    ;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
				if (_autoDisposeData == true)
					Xtlib.TransformFreeArrayOfCharArray (_data);

                // Note disposing has been done.
                _disposed = true;

            }
        }
		
		
        /// <summary> Finalize. </summary>
        /// <remarks> This destructor will run only if the Dispose method does not get called. It gives the class
        /// the opportunity to finalize. Do not provide destructors in types derived from this class. </remarks>
		~XtStringArray()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose(false);
        }
	}
	
	/// <summary> A smart wrapper class for a native byte array. </summary>
	public class XtByteArray : IDisposable
	{
		// Hold the native byte array.
		private IntPtr	_data = IntPtr.Zero;
		
		private uint	_dataSize = 0;
		
		// Determine whether to auto dispose the native byte array.
		private bool	_autoDisposeData = true;
		
		 // Track whether Dispose has been called.
        private bool	_disposed = false;
		
		/// <summary> HIDDEN default constructor. </summary>
		private XtByteArray (){}
		
		/// <summary> Initializing constructor for auto dispose smart byte array. </summary>
		/// <param name="textToSplit"> The text to split into a smart byte array. <see cref="System.String"/> </param>
		/// <remarks> The delimiter is space ' '. </remarks>
		public XtByteArray (string textToSplit)
		{
			_data = Xtlib.TransformToArrayOfByte (textToSplit, ref _dataSize);
		}
		
		/// <summary> Initializing constructor for smart byte array. </summary>
		/// <param name="textToSplit"> The text to split into a smart byte array. <see cref="System.String"/> </param>
		/// <param name="autoDisposeData"> Determine whether to auto dispose the native byte array. <see cref="System.Boolean"/> </param>
		/// <remarks> The delimiter is space ' '. </remarks>
		public XtByteArray (string textToSplit, bool autoDisposeData)
		{
			_data = Xtlib.TransformToArrayOfByte (textToSplit, ref _dataSize);
			_autoDisposeData = autoDisposeData;
		}
		
		/// <summary> Get the native byte array adress. </summary>
		public IntPtr Data
		{
			get {	return _data;	}
		}
		
		/// <summary> Implement IDisposable. </summary>
        /// <remarks> Do not make this method virtual. A derived class should not be able to override this method. </remarks>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
		
		/// <summary> Execute disposing for user calls and GC calls. </summary>
		/// <param name="disposing"> Determine whether called by user (true) or GC (false). <see cref="System.Boolean"/> </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(_disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    ;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
				if (_autoDisposeData == true)
					Xtlib.TransformFreeArrayOfInt (_data);

                // Note disposing has been done.
                _disposed = true;

            }
        }
		
		
        /// <summary> Finalize. </summary>
        /// <remarks> This destructor will run only if the Dispose method does not get called. It gives the class
        /// the opportunity to finalize. Do not provide destructors in types derived from this class. </remarks>
		~XtByteArray()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose(false);
        }
	}
	
	/// <summary> A smart wrapper class for a native integer array. </summary>
	public class XtIntegerArray : IDisposable
	{
		// Hold the native integer array.
		private IntPtr	_data = IntPtr.Zero;
		
		private uint	_dataSize = 0;
		
		// Determine whether to auto dispose the native integer array.
		private bool	_autoDisposeData = true;
		
		 // Track whether Dispose has been called.
        private bool	_disposed = false;
		
		/// <summary> HIDDEN default constructor. </summary>
		private XtIntegerArray (){}
		
		/// <summary> Initializing constructor for auto dispose smart integer array. </summary>
		/// <param name="textToSplit"> The text to split into a smart integer array. <see cref="System.String"/> </param>
		/// <remarks> The delimiter is space ' '. </remarks>
		public XtIntegerArray (string textToSplit)
		{
			_data = Xtlib.TransformToArrayOfInt (textToSplit, ref _dataSize);
		}
		
		/// <summary> Initializing constructor for smart integer array. </summary>
		/// <param name="textToSplit"> The text to split into a smart integer array. <see cref="System.String"/> </param>
		/// <param name="autoDisposeData"> Determine whether to auto dispose the native integer array. <see cref="System.Boolean"/> </param>
		/// <remarks> The delimiter is space ' '. </remarks>
		public XtIntegerArray (string textToSplit, bool autoDisposeData)
		{
			_data = Xtlib.TransformToArrayOfInt (textToSplit, ref _dataSize);
			_autoDisposeData = autoDisposeData;
		}
		
		/// <summary> Get the native integer array adress. </summary>
		public IntPtr Data
		{
			get {	return _data;	}
		}
		
		/// <summary> Implement IDisposable. </summary>
        /// <remarks> Do not make this method virtual. A derived class should not be able to override this method. </remarks>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
		
		/// <summary> Execute disposing for user calls and GC calls. </summary>
		/// <param name="disposing"> Determine whether called by user (true) or GC (false). <see cref="System.Boolean"/> </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(_disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    ;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
				if (_autoDisposeData == true)
					Xtlib.TransformFreeArrayOfInt (_data);

                // Note disposing has been done.
                _disposed = true;

            }
        }
		
		
        /// <summary> Finalize. </summary>
        /// <remarks> This destructor will run only if the Dispose method does not get called. It gives the class
        /// the opportunity to finalize. Do not provide destructors in types derived from this class. </remarks>
		~XtIntegerArray()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose(false);
        }
	}
	
	public static class XtValues
	{
		public static IntPtr XtDefaultBackground = Marshal.StringToHGlobalAnsi ("XtDefaultBackground");
		public static IntPtr XtDefaultFont = Marshal.StringToHGlobalAnsi ("XtDefaultFont");
		public static IntPtr XtDefaultFontSet = Marshal.StringToHGlobalAnsi ("XtDefaultFontSet");
		public static IntPtr XtDefaultForeground = Marshal.StringToHGlobalAnsi ("XtDefaultForeground");
	}
	
	/// <summary> Collect fallback ressource strings and provide them Xt ready. </summary>
	public class XtFallbackRessources : IDisposable
	{
		/// <summary> The accessible list of ressource strings. The list can be manipulated at any time. </summary>
		private List<string>   		_stringList = new List<string> ();
		
		/// <summary> The hidden list of marshalled resource strings. This is a unmanaged ressource that must be disposed. </summary>
		private IntPtr[]			_marshalledFallbackResources = _zero;
		
		/// <summary> The hidden zero pointer, idicating _marshalledFallbackResources is not initialized. </summary>
		private static IntPtr[]		_zero = new IntPtr[0];

		/// <summary> Add the specified string resource. </summary>
		/// <param name='resource'> The string resource to add. </param>
		public void Add (string resource)
		{
			if (!string.IsNullOrEmpty (resource))
			_stringList.Add (resource);
		}

		/// <summary> Provide the Xt ready list of ressource strings. </summary>
		/// <returns> The t ready list of ressource strings. <see cref="IntPtr[]"/> </returns>
		public IntPtr[]      Marshal ()
		{
			// Free old unmanaged resources.
			Dispose ();

			// Allocate an unmanaged ressource for every ressource string.
			_marshalledFallbackResources = new IntPtr[_stringList.Count + 1];
			for (int count = 0; count < _stringList.Count; count++)
			{
				_marshalledFallbackResources[count] = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi (_stringList[count]);
			}
			_marshalledFallbackResources[_stringList.Count] = IntPtr.Zero;
			
			return _marshalledFallbackResources;
		}
		
		/// <summary> Free unmanaged ressources. </summary>
		public void Dispose ()
		{
			if (_marshalledFallbackResources != _zero)
			{
				for (int count = 0; count < _marshalledFallbackResources.Length; count++)
				{
					System.Runtime.InteropServices.Marshal.FreeHGlobal (_marshalledFallbackResources[count]);
					_marshalledFallbackResources[count] = IntPtr.Zero;
				}
				_marshalledFallbackResources = _zero;
			}
		}
	}
	
    [StructLayout(LayoutKind.Explicit)]
    public struct Arg
	{
		/// <summary> Make code able to check, if correct offset is set. </summary>
		public const int ElementOffset = 8;
		
		/// <summary> The argument name. </summary>
        [FieldOffset(0)]
		public TChar[] name;

		/// <summary> The argument value. </summary>
		/// <remarks> Field offset dependends on OS pointer size. </remarks>
		[FieldOffset(ElementOffset)]
	 	public XtArgVal intValue32;
		[FieldOffset(ElementOffset)]
	 	public float    floatValue32;
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aValue"> The argument value. <see cref="XtArgVal"/> </param>
		public Arg (TChar[] aName, XtArgVal aValue)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			this.intValue32 = aValue;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aValue"> The argument value. <see cref="float"/> </param>
		public Arg (TChar[] aName, float aValue)
		{
			this.name = aName;
			this.intValue32 = 0;		// Just to quiet down th stupid interpreter.
			
			this.floatValue32 = aValue;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aPointer"> The argument value. <see cref="System.IntPtr"/> </param>
		public Arg (TChar[] aName, IntPtr aPointer)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			this.intValue32 = (XtArgVal)(System.Int32)aPointer;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aArray"> The argument value. <see cref="TChar[]"/> </param>
		//[Obsolete("Use XtString (..., true) instead!")]
		public Arg (TChar[] aName, TChar[] aArray)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			System.IntPtr aPointer = Marshal.AllocHGlobal (aArray.Length * sizeof(TChar));
			byte []       aBuffer  = new byte [aArray.Length];
			for (int count = 0; count < aArray.Length; count++)
				aBuffer[count] = (byte) aArray[count];
			Marshal.Copy (aBuffer, 0, aPointer, aBuffer.Length);
			this.intValue32 = (XtArgVal)(System.Int32)aPointer;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aNativeStringWrapper"> The argument value. <see cref="XtString"/> </param>
		public Arg (TChar[] aName, XtString aNativeStringWrapper)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			this.intValue32 = (XtArgVal)(System.Int32)aNativeStringWrapper.Data;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aString"> The argument value. <see cref="TBoolean[]"/> </param>
		public Arg (TChar[] aName, TBoolean[] aArray)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			System.IntPtr aPointer = Marshal.AllocHGlobal (aArray.Length * sizeof(byte));
			byte []        aBuffer  = new byte [aArray.Length];
			for (int count = 0; count < aArray.Length; count++)
				aBuffer[count] = (byte)aArray[count];
			Marshal.Copy (aBuffer, 0, aPointer, aBuffer.Length);
			this.intValue32 = (XtArgVal)(System.Int32)aPointer;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aString"> The argument value. <see cref="TUint[]"/> </param>
		public Arg (TChar[] aName, TUint[] aArray)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			System.IntPtr aPointer = Marshal.AllocHGlobal (aArray.Length * sizeof(TUint));
			int []        aBuffer  = new int [aArray.Length];
			for (int count = 0; count < aArray.Length; count++)
				aBuffer[count] = (int) aArray[count];
			Marshal.Copy (aBuffer, 0, aPointer, aBuffer.Length);
			this.intValue32 = (XtArgVal)(System.Int32)aPointer;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aString"> The argument value. <see cref="System.String"/> </param>
		public Arg (TChar[] aName, string aString)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			if (aString != null && aString.Length != 0)
			{
				System.IntPtr aPointer = Marshal.AllocHGlobal (aString.Length);
				byte []       aBuffer  = new byte [aString.Length];
				for (int count = 0; count < aString.Length; count++)
					aBuffer[count] = (byte) aString[count];
				Marshal.Copy (aBuffer, 0, aPointer, aBuffer.Length);
				this.intValue32 = (XtArgVal)(System.Int32)aPointer;
			}
			else
				this.intValue32 = 0;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aNativeDataPointerArray"> The argument value. <see cref="System.IntPtr[]"/> </param>
		public Arg (TChar[] aName, IntPtr[] aNativeDataPointerArray)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			System.IntPtr	pointerArray	= Marshal.AllocHGlobal (aNativeDataPointerArray.Length * System.IntPtr.Size);
			Marshal.Copy (aNativeDataPointerArray, 0, pointerArray, aNativeDataPointerArray.Length);
			this.intValue32 = (XtArgVal)(System.Int32)pointerArray;
		}
		
		// Tested: O.K.
		/// <summary> Initializing constructor. </summary>
		/// <param name="aName"> The argument name. <see cref="TChar[]"/> </param>
		/// <param name="aTextArray"> The argument value. <see cref="System.String[]"/> </param>
		public Arg (TChar[] aName, string[] aTextArray)
		{
			this.name = aName;
			this.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			if (aTextArray == null || aTextArray.Length == 0)
			{
				this.intValue32 = 0;
				return;
			}
			
			IntPtr[] nativeDataPointerArray = new IntPtr[aTextArray.Length];
			for (int index = 0; index < aTextArray.Length; index++)
			{
				if (aTextArray[index] != null && aTextArray[index].Length != 0)
				{
					nativeDataPointerArray[index] = Marshal.AllocHGlobal (aTextArray[index].Length + 1);
					byte []       aBuffer  = new byte [aTextArray[index].Length + 1];
					for (int count = 0; count < aTextArray[index].Length; count++)
						aBuffer[count] = (byte) aTextArray[index][count];
					aBuffer[aBuffer.Length - 1] = (byte) 0;
					Marshal.Copy (aBuffer, 0, nativeDataPointerArray[index], aBuffer.Length);
				}
				else
				{
					nativeDataPointerArray[index] = IntPtr.Zero;
				}
			}

			System.IntPtr	pointerArray	= Marshal.AllocHGlobal (nativeDataPointerArray.Length * System.IntPtr.Size);
			Marshal.Copy (nativeDataPointerArray, 0, pointerArray, nativeDataPointerArray.Length);
			this.intValue32 = (XtArgVal)(System.Int32)pointerArray;
		}
		
		// Tested: O.K.
		public static Arg[] Zero = new Arg[0];
		
		public static void XtSetArg (Arg arg, TChar[] name, TInt value)
		{
			arg.name = name;
			arg.floatValue32 = 0.0F;	// Just to quiet down th stupid interpreter.
			
			arg.intValue32 = (XtArgVal)value;
		}
	}
	
    [StructLayout(LayoutKind.Explicit)]
    public struct Arg_IntPtr
	{
		/// <summary> The argument name. </summary>
        [FieldOffset(0)]
		public TChar[] name;

		/// <summary> The argument value. </summary>
		[FieldOffset(4)]
	 	public IntPtr value;

		public Arg_IntPtr (TChar[] aName, IntPtr aPointer)
		{
			this.name = aName;
			this.value = aPointer;
		}
    }

	public enum XtEdgeType
	{
		XtChainTop,
		XtChainBottom,
		XtChainLeft,
		XtChainRight,
		XtRubber
	}
	
	public class Xtlib
	{
		public Xtlib ()
		{
		}
		
		const string libXt = "libXt.so.6";
		
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtInitialize (string shellName, string applicationClass, IntPtr options, int numOptions, ref int srgc, IntPtr argv);
		
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtSetLanguageProc (IntPtr appContext, IntPtr languageProc, IntPtr clientData);
		
		// Tested: O.K.
		/// <summary> Initialize the X Toolkit internals, create an application context, open and initialize a display,
		/// and create  the  initial application shell instance. </summary>
		/// <param name="appContext"> [OUT] Returns the newly created application context on success, or NULL otherwise. <see cref="IntPtr"/> </param>
		/// <param name="appClassName"> Specifies the class name of the application. <see cref="TChar[]"/> </param>
		/// <param name="options"> Specifies  an	array  of XrmOptionDescRec which describe how to parse the command line. <see cref="IntPtr"/> </param>
		/// <param name="numOptions"> Specifies the number of elements in options. <see cref="TUint"/> </param>
		/// <param name="argc"> [IN] Specifies the address of the number	of command line arguments. This argument is of type
		/// int* in Release 5 and of type Cardinal* in Release 4. [OUT] Returns  the  number of command line arguments remaining
		/// after the command line is parsed by XtDisplayInitialize().<see cref="TInt"/> </param>
		/// <param name="argv"> [IN] Specifies the array of command line arguments.
		/// [OUT] Returns  the  command	line as modified by XtDisplayInitialize(). <see cref="IntPtr"/> </param>
		/// <param name="fallbackResources"> Specifies a NULL-terminated array of resource specification strings to be used if
		/// the application class resource file cannot be opened or read, or NULL if no fallback resources are desired. <see cref="IntPtr[]"/> </param>
		/// <param name="args"> Specifies an argument list to override any other resource specifications for the shell widget to create. <see cref="IntPtr"/> </param>
		/// <param name="numArgs"> Specifies the number of elements in args. <see cref="XCardinal"/> </param>
		/// <returns> A toplevel shell widget of class applicationShellWidgetClass. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtAppInitialize (ref IntPtr appContext, TChar[] appClassName, IntPtr options, TUint numOptions, ref TInt argc, IntPtr argv, IntPtr[] fallbackResources, IntPtr args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Initialize the X Toolkit internals, create an application context, open and initialize a display,
		/// and create  the  initial application shell instance. </summary>
		/// <param name="appContext"> [OUT] Returns the newly created application context on success, or NULL otherwise. <see cref="IntPtr"/> </param>
		/// <param name="appClassName"> Specifies the class name of the application. <see cref="String"/> </param>
		/// <param name="options"> Specifies  an	array  of XrmOptionDescRec which describe how to parse the command line. <see cref="IntPtr"/> </param>
		/// <param name="numOptions"> Specifies the number of elements in options. <see cref="TUint"/> </param>
		/// <param name="argc"> [IN] Specifies the address of the number	of command line arguments. This argument is of type
		/// int* in Release 5 and of type Cardinal* in Release 4. [OUT] Returns  the  number of command line arguments remaining
		/// after the command line is parsed by XtDisplayInitialize().<see cref="TInt"/> </param>
		/// <param name="argv"> [IN] Specifies the array of command line arguments.
		/// [OUT] Returns  the  command	line as modified by XtDisplayInitialize(). <see cref="IntPtr"/> </param>
		/// <param name="fallbackResources"> Specifies a NULL-terminated array of resource specification strings to be used if
		/// the application class resource file cannot be opened or read, or NULL if no fallback resources are desired. <see cref="IntPtr[]"/> </param>
		/// <param name="args"> Specifies an argument list to override any other resource specifications for the shell widget to create. <see cref="IntPtr"/> </param>
		/// <param name="numArgs"> Specifies the number of elements in args. <see cref="XCardinal"/> </param>
		/// <returns> A toplevel shell widget of class applicationShellWidgetClass. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtAppInitialize (ref IntPtr appContext, [MarshalAs(UnmanagedType.LPStr)] string appClassName, IntPtr options, TUint numOptions, ref TInt argc, IntPtr argv, IntPtr[] fallbackResources, IntPtr args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Create a popup shell widget (on any sublass of shell). </summary>
		/// <param name="shellResourceName"> Specifies the name for the shell to create, that can be used to assign or obtain resources to or from. <see cref="TChar[]"/> </param>
		/// <param name="widgetClass"> The class pointer of the shell to create, must be of shellWidgetClass or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="parentWidget"> The parent widget, must be of class coreWidgetClass or or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="args"> Specifies an argument list to override any other resource specifications for the widget to create. <see cref="IntPtr"/> </param>
		/// <param name="numArgs"> Specifies the number of elements in args. <see cref="XCardinal"/> </param>
		/// <returns> A created widget of indicated class. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtCreatePopupShell (TChar[] shellResourceName, IntPtr widgetClass, IntPtr parentWidget, Arg[] args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Create a popup shell widget (on any sublass of shell). </summary>
		/// <param name="shellResourceName"> Specifies the name for the shell to create, that can be used to assign or obtain resources to or from. <see cref="System.String"/> </param>
		/// <param name="widgetClass"> The class pointer of the shell to create, must be of shellWidgetClass or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="parentWidget"> The parent widget, must be of class coreWidgetClass or or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="args"> Specifies an argument list to override any other resource specifications for the widget to create. <see cref="IntPtr"/> </param>
		/// <param name="numArgs"> Specifies the number of elements in args. <see cref="XCardinal"/> </param>
		/// <returns> A created widget of indicated class. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtCreatePopupShell ([MarshalAs(UnmanagedType.LPStr)] string shellResourceName, IntPtr widgetClass, IntPtr parentWidget, Arg[] args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Create a manage widget (calling XtCreateWidget() and XtManageChild()). </summary>
		/// <param name="widgetResourceName"> Specifies the name for the widget to create, that can be used to assign or obtain resources to or from. <see cref="TChar[]"/> </param>
		/// <param name="widgetClass"> The class pointer of the widget to create, must be of rectObectClass or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="parentWidget"> The parent widget, must be of class compositeWidgetClass or or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="args"> Specifies an argument list to override any other resource specifications for the widget to create. <see cref="IntPtr"/> </param>
		/// <param name="numArgs"> Specifies the number of elements in args. <see cref="XCardinal"/> </param>
		/// <returns> A created widget of indicated class. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtCreateManagedWidget (TChar[] widgetResourceName, IntPtr widgetClass, IntPtr parentWidget, Arg[] args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Create a manage widget (calling XtCreateWidget() and XtManageChild()). </summary>
		/// <param name="widgetResourceName"> Specifies the name for the widget to create, that can be used to assign or obtain resources to or from. <see cref="TChar[]"/> </param>
		/// <param name="widgetClass"> The class pointer of the widget to create, must be of rectObectClass or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="parentWidget"> The parent widget, must be of class compositeWidgetClass or or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="args"> Specifies an argument list to override any other resource specifications for the widget to create. <see cref="IntPtr"/> </param>
		/// <param name="numArgs"> Specifies the number of elements in args. <see cref="XCardinal"/> </param>
		/// <returns> A created widget of indicated class. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtCreateManagedWidget ([MarshalAs(UnmanagedType.LPStr)] string widgetResourceName, IntPtr widgetClass, IntPtr parentWidget, Arg[] args, XCardinal numArgs);

		/// <summary> Create a manage widget (calling XtCreateWidget() and XtManageChild()). </summary>
		/// <param name="widgetResourceName"> Specifies the name for the widget to create, that can be used to assign or obtain resources to or from. <see cref="TChar[]"/> </param>
		/// <param name="widgetClass"> The class pointer of the widget to create, must be of rectObectClass or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="parentWidget"> The parent widget, must be of class compositeWidgetClass or or a subclass. <see cref="IntPtr"/> </param>
		/// <param name="args"> Specifies an alternating list of resource names (XtN*) and argument values to override any other resource specifications for the widget to create. <see cref="IntPtr"/> </param>
		/// <returns> A created widget of indicated class. <see cref="IntPtr"/> </returns>
		/// <remarks> The params "args" must end with a NULL pointer. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtVaCreateManagedWidget (TChar[] widgetResourceName, IntPtr widgetClass, IntPtr parentWidget, params IntPtr[] args);
		
		/// <summary> Destroy indicated widget and all of its normal and popup descendants. It frees all resources associated with that widget
		/// and its descendants, and calls the Xlib function XDestroyWindow() to destroy the windows (if any) of the affected objects. </summary>
		/// <param name="xtWidget"> The widget to destroy. A <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtDestroyWidget (IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Query widget resource values. </summary>
		/// <param name="xtWidget"> Specifies the object whose resource values are to be returned.
		/// May be of class Object or any subclass thereof. <see cref="IntPtr"/> </param>
		/// <param name="args"> Specifies the argument list of name/address pairs that contain the resource
		/// names and the addresses into which the resource values are to be stored. <see cref="Arg[]"/> </param>
		/// <param name="numArgs"> Specifies the number of arguments in the argument list. <see cref="XCardinal"/> </param>
		/// <remarks> Each element in args is an Arg structure which contains the resource name in the name field,
		/// and	a pointer to the location at which the resource is to be stored in the value field.	It is the caller's
		/// responsibility to ensure that the value field points to a value of the correct type. If the value field
		/// points to allocated memory, the caller is also responsible for freeing that memory. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtGetValues (IntPtr xtWidget, Arg[] args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Convenience method to query a single widget resource value of type Dimension. </summary>
		/// <param name="xtWidget"> Specifies the object whose resource value is to be returned.
		/// May be of class Object or any subclass thereof. <see cref="IntPtr"/> </param>
		/// <param name="resourceName"> The name of the resource to query. <see cref="TChar[]"/> </param>
		/// <returns> The requested resource value. <see cref="XDimension"/> </returns>
		public static XDimension XtGetValueOfDimension (IntPtr xtWidget, TChar[] resourceName)
		{
			if (xtWidget == IntPtr.Zero)
				throw new ArgumentNullException ("xtWidget");
			if (resourceName == null || resourceName.Length == 0)
				throw new ArgumentNullException ("valueName");
			
			IntPtr		hResult			= Marshal.AllocHGlobal (sizeof(XDimension));
			Arg[]		widgetArgs		= { new Arg(resourceName, hResult) };

			Xtlib.XtGetValues (xtWidget, widgetArgs, (XCardinal)1);
			XDimension	result			= (XDimension)Marshal.ReadInt16 (hResult);
			Marshal.FreeHGlobal (hResult);

			return result;
		}
		
		// Tested: O.K.
		/// <summary> Convenience method to query a single widget resource value of type Int. </summary>
		/// <param name="xtWidget"> Specifies the object whose resource value is to be returned.
		/// May be of class Object or any subclass thereof. <see cref="IntPtr"/> </param>
		/// <param name="resourceName"> The name of the resource to query. <see cref="TChar[]"/> </param>
		/// <returns> The requested resource value. <see cref="TInt"/> </returns>
		public static TInt XtGetValueOfInt (IntPtr xtWidget, TChar[] resourceName)
		{
			if (xtWidget == IntPtr.Zero)
				throw new ArgumentNullException ("xtWidget");
			if (resourceName == null || resourceName.Length == 0)
				throw new ArgumentNullException ("valueName");
			
			IntPtr		hResult			= Marshal.AllocHGlobal (sizeof(TInt));
			Arg[]		widgetArgs		= { new Arg(resourceName, hResult) };

			Xtlib.XtGetValues (xtWidget, widgetArgs, (XCardinal)1);
			TInt		result			= (TInt)Marshal.ReadInt16 (hResult);
			Marshal.FreeHGlobal (hResult);

			return result;
		}
		
		// Tested: O.K.
		/// <summary> Convenience method to query a single widget resource value of type Pixel. </summary>
		/// <param name="xtWidget"> Specifies the object whose resource value is to be returned.
		/// May be of class Object or any subclass thereof. <see cref="IntPtr"/> </param>
		/// <param name="resourceName"> The name of the resource to query. <see cref="TChar[]"/> </param>
		/// <returns> The requested resource value. <see cref="TPixel"/> </returns>
		public static TPixel XtGetValueOfPixel (IntPtr xtWidget, TChar[] resourceName)
		{
			if (xtWidget == IntPtr.Zero)
				throw new ArgumentNullException ("xtWidget");
			if (resourceName == null || resourceName.Length == 0)
				throw new ArgumentNullException ("valueName");
			
			IntPtr		hResult			= Marshal.AllocHGlobal (sizeof(TPixel));
			Arg[]		widgetArgs		= { new Arg(resourceName, hResult) };

			Xtlib.XtGetValues (xtWidget, widgetArgs, (XCardinal)1);
			TPixel		result			= 0;
			if (sizeof (TPixel) == sizeof (uint))
				result = (TPixel)Marshal.ReadInt32 (hResult);
			else
				result = (TPixel)Marshal.ReadInt64 (hResult);
			Marshal.FreeHGlobal (hResult);

			return result;
		}
		
		// Tested: O.K.
		/// <summary> Convenience method to query a single widget resource value of type Pointer. </summary>
		/// <param name="xtWidget"> Specifies the object whose resource value is to be returned.
		/// May be of class Object or any subclass thereof. <see cref="IntPtr"/> </param>
		/// <param name="resourceName"> The name of the resource to query. <see cref="TChar[]"/> </param>
		/// <returns> The requested resource value. <see cref="IntPtr"/> </returns>
		public static IntPtr XtGetValueOfPointer (IntPtr xtWidget, TChar[] resourceName)
		{
			if (xtWidget == IntPtr.Zero)
				throw new ArgumentNullException ("xtWidget");
			if (resourceName == null || resourceName.Length == 0)
				throw new ArgumentNullException ("valueName");
			
			IntPtr		hResult			= Marshal.AllocHGlobal (IntPtr.Size);
			Arg[]		widgetArgs		= { new Arg(resourceName, hResult) };

			Xtlib.XtGetValues (xtWidget, widgetArgs, (XCardinal)1);
			IntPtr		result			= Marshal.ReadIntPtr (hResult);
			Marshal.FreeHGlobal (hResult);

			return result;
		}
		
		[DllImport ("libXt.so.6")]
		public extern static void XtSetArg (ref IntPtr arg, [MarshalAs(UnmanagedType.LPStr)] string name, IntPtr val);
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtSetValues (IntPtr xtWidget, Arg[] args, XCardinal numArgs);
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtSetValues (IntPtr xtWidget, Arg_IntPtr[] args, XCardinal numArgs);
		
		// Tested: O.K.
		/// <summary> Add a callback procedure to a named callback list. </summary>
		/// <param name="xtWidget"> The widget to add a callback procedure for. <see cref="System.IntPtr"/> </param>
		/// <param name="callbackName"> The resource name of the callback list to which the procedure is to be added. <see cref="TChar[]"/> </param>
		/// <param name="callback"> The callback procedure to be added. <see cref="IntPtr"/> </param>
		/// <param name="clientData"> The data to be passed to callback when it is invoked, or NULL. <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtAddCallback (IntPtr xtWidget, TChar[] callbackName, IntPtr callback, IntPtr clientData);
		
		// Tested: O.K.
		/// <summary> Removes all the callback procedures from the specified widget's callback list. </summary>
		/// <param name="xtWidget"> The widget to remove all callbacks from the specified widget's callback list. <see cref="System.IntPtr"/> </param>
		/// <param name="callbackName"> The resource name of the callback list which has to be cleared. <see cref="TChar[]"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtRemoveAllCallbacks (IntPtr xtWidget, TChar[] callbackName);
			
		// Tested: O.K.
		/// <summary> Return the display pointer for the specified widget. </summary>
		/// <param name="xtWidget"> The widget to determine the display pointer for. <see cref="System.IntPtr"/> </param>
		/// <returns> The display pointer for the specified widget. </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtDisplay (IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Return the window pointer for the specified widget. </summary>
		/// <param name="xtWidget"> The widget to determine the window pointer for. <see cref="IntPtr"/> </param>
		/// <returns> The display window for the specified widget. </returns>
		/// <remarks> Note that the window is obtained from the Core window field, which may be NULL if the widget has not yet been realized. </remarks>
		/// <remarks> Alternatively use X11lib.XDefaultRootWindow (Xtlib.XtDisplay (xtWidget)). </remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtWindow (IntPtr xtWidget);
		
		/// <summary> Return the screen pointer for the specified widget. </summary>
		/// <param name="xtWidget"> The widget to determine the screen pointer for. <see cref="System.IntPtr"/> </param>
		/// <returns> The screen for the specified widget. </returns>
		/// <remarks> Note that the window is obtained from the Core window field, which may be NULL if the widget has not yet been realized. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtScreen (IntPtr xtWidget);
		
		/// <summary> Return the default screen number referenced by the XOpenDisplay() function. </summary>
		/// <param name="xtDisplay"> The display, specifying the connection to the X server. <see cref="System.IntPtr"/> </param>
		/// <returns> <see cref="TInt"/> The default screen number. </returns>
		/// <remarks> This macro or function should be used to retrieve the screen number in applications that will use only a single screen. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static TInt XDefaultScreen (IntPtr xtDisplay);

		// Tested: O.K.
		/// <summary> Realize a widget instance. </summary>
		/// <param name="xtWidget"> The widget to realize. <see cref="IntPtr"/> </param>
		/// <remarks> XtRealizeWidget() creates windows for the specified widget and all of its  descendants.
		/// If the specified widget is already realized, XtRealizeWidget () simply returns.  When a widget is first created, no X window is created along with it.
		/// Realizing a widget is the term for creating this window, and no widget can appear on the screen until it is realized.</remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtRealizeWidget (IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Start continuously process events. </summary>
		/// <param name="appContext"> The application context that identifies the application. <see cref="System.IntPtr"/> </param>
		/// <remarks> Enter an infinite loop which calls XtAppNextEvent() to wait for an events on all displays in
		/// app_context and XtDispatchEvent() to dispatch that event to the appropriate code.</remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtAppMainLoop (IntPtr appContext);
		
		// Tested: O.K.
		/// <summary> Return the indicated storage and allow it to be reused. If ptr is NULL, XtFree returns immediately. </summary>
		/// <param name="pointer"> The storage to return. <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtFree (IntPtr pointer); 
		
		// Tested: O.K.
		[DllImport ("libc.so.6")]
		public extern static void exit (int returnCode);
		
		/// <summary> Translate a resource name to a widget. </summary>
		/// <param name="xtReferenceWidget"> Specifies the widget from which the search is to start. <see cref="IntPtr"/> </param>
		/// <param name="widgetRelativeResourceName"> Specifies the resource name of the widget to search for
		/// with respect to the specified xtReferenceWidget. <see cref="TChar[]"/> </param>
		/// <returns> The specified widget on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		/// <remarks> The XtNameToWidget function looks for a widget whose name is the (only or) first component in the specified
		/// widgetRelativeResourceName and that is a pop-up child of xtReferenceWidget (or a normal child if xtReferenceWidget is
		/// a subclass of compositeWidgetClass). It then uses that widget as the new xtReferenceWidget and repeats the search after
		/// deleting the first component from the specified names.
		/// Note that the widgetRelativeResourceName argument contains the name of a widget with respect to the specified
		/// xtReferenceWidget and can contain more than one widget name (separated by periods) for widgets that are not direct
		/// children of the specified xtReferenceWidget. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtNameToWidget (IntPtr xtReferenceWidget, TChar[] widgetRelativeResourceName);
		
		/// <summary> Translate a resource name to a widget. </summary>
		/// <param name="xtReferenceWidget"> Specifies the widget from which the search is to start. <see cref="IntPtr"/> </param>
		/// <param name="widgetRelativeResourceName"> Specifies the resource name of the widget to search for
		/// with respect to the specified xtReferenceWidget. <see cref="TChar[]"/> </param>
		/// <returns> The specified widget on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		/// <remarks> The XtNameToWidget function looks for a widget whose name is the (only or) first component in the specified
		/// widgetRelativeResourceName and that is a pop-up child of xtReferenceWidget (or a normal child if xtReferenceWidget is
		/// a subclass of compositeWidgetClass). It then uses that widget as the new xtReferenceWidget and repeats the search after
		/// deleting the first component from the specified names.
		/// Note that the widgetRelativeResourceName argument contains the name of a widget with respect to the specified
		/// xtReferenceWidget and can contain more than one widget name (separated by periods) for widgets that are not direct
		/// children of the specified xtReferenceWidget. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtNameToWidget (IntPtr xtReferenceWidget, [MarshalAs(UnmanagedType.LPStr)] string widgetRelativeResourceName);

		// Tested: O.K.
		/// <summary> Get the parent of the specified widget. </summary>
		/// <param name="xtWidget"> The widget to get the parent for. <see cref="IntPtr"/> </param>
		/// <returns> The parent widget	of the indicated widget. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtParent (IntPtr xtWidget);
		
		/// <summary> Get the class of the specified widget. </summary>
		/// <param name="xtWidget"> The widget to get the class for. <see cref="IntPtr"/> </param>
		/// <returns> The class widget of the indicated widget. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtClass (IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Test if the class of the specified widget is equal to or is a subclass
		/// of the specified class. The widget's class can be any number of subclasses down
		/// the chain and need not be an immediate subclass of the specified class. </summary>
		/// <param name="xtWidget"> The widget to test the class for. <see cref="IntPtr"/> </param>
		/// <param name="xtWidgetClass"> The widget class to test the widget for. <see cref="IntPtr"/> </param>
		/// <returns> True on success, or false otherwise. <see cref="TBoolean"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static TBoolean XtIsSubclass (IntPtr xtWidget, IntPtr xtWidgetClass);
		
		// Tested: O.K.
		/// <summary> Count the widget's children. </summary>
		/// <param name="xtWidget"> The widget to count the children for. <see cref="IntPtr"/> </param>
		/// <returns> The number of children. <see cref="TInt"/> </returns>
		public static TInt XtCountChildren (IntPtr xtWidget)
		{
			return XtGetValueOfInt (xtWidget, XtNames.XtNnumChildren);
		}
		
		// Tested: O.K.
		/// <summary> Get the indicated child widget. </summary>
		/// <param name="xtWidget"> The widget to get the indicated child from. <see cref="IntPtr"/> </param>
		/// <param name="index"> The index of the requested child widget. <see cref="TInt"/> </param>
		/// <returns> The child widget on success, or IntPtr.Zero otherwise. <see cref="IntPtr"/> </returns>
		public static IntPtr XtGetChild (IntPtr xtWidget, TInt index)
		{
			if (index < 0)
				return IntPtr.Zero;
			
			IntPtr		hCount			= Marshal.AllocHGlobal (sizeof(TInt));
			IntPtr		hChildren		= Marshal.AllocHGlobal (IntPtr.Size);
			Arg[]		widgetArgs		= { new Arg(XtNames.XtNnumChildren, hCount),
											new Arg(XtNames.XtNchildren,    hChildren) };
			Xtlib.XtGetValues (xtWidget, widgetArgs, (XCardinal)2);

			TInt		count			= (TInt)Marshal.ReadInt16 (hCount);
			IntPtr		childWidgets	= Marshal.ReadIntPtr (hChildren);
			Marshal.FreeHGlobal (hCount);
			Marshal.FreeHGlobal (hChildren);
			
			if (index >= count)
				return IntPtr.Zero;
			
			return Marshal.ReadIntPtr (childWidgets, ((int)index) * IntPtr.Size);
		}
		
		// Tested: O.K.
		/// <summary> Compile a translation table into its internal representation. </summary>
		/// <param name="table"> The translation table to compile. <see cref="System.String"/> </param>
		/// <returns> The compiled form of table. <see cref="IntPtr"/> </returns>
		/// <remarks> This compiled form can then be set as the value of a widget's XtNtranslations resource, or merged with a widget's existing translation table with
		/// XtAugmentTranslations () or XtOverrideTranslations (). To create an empty translation table, call XtParseTranslationTable () and passing an empty string.
		/// It is also possible to set a translation table with the XtVaTypedArg feature of XtVaCreateWidget () and XtVaSetValues (). This allows to
		/// specify  the translation table in string form, and have the appropriate resource converter automatically invoked to compile it.</remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtParseTranslationTable ([MarshalAs(UnmanagedType.LPStr)] string table);
		
		// Tested: O.K.
		/// <summary> Merge new translations, overriding a widget's existing ones. </summary>
		/// <param name="xtWidget"> The widget into which the new translations are to be merged. Must be of class Core or any subclass thereof. <see cref="System.IntPtr"/> </param>
		/// <param name="translationTable"> The compiled translation table to merge in. Typically compiled with XtParseTranslationTable (). <see cref="System.IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtOverrideTranslations (IntPtr xtWidget, IntPtr translationTable);
		
		/// <summary> Remove all existing translations from a widget. </summary>
		/// <param name="xtWidget"> Specifies the widget from which the translations are to be removed. <see cref="System.IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtUninstallTranslations (IntPtr xtWidget);
		
		/// <summary> Compiles the accelerator table into the opaque internal representation. </summary>
		/// <param name="table"> The accelerator table to compile. <see cref="System.String"/> </param>
		/// <returns> The compiled form of table. <see cref="IntPtr"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtParseAcceleratorTable ([MarshalAs(UnmanagedType.LPStr)] string table);
		
		// Tested: O.K.
		/// <summary> Install a widget's accelerators on another widget.</summary>
		/// <param name="xtWidgetDestination" > Specifies the widget in which events specified in the  accelerator  table will be detected.
		/// Must be of class Core or any subclass thereof. (E.g. a shell widget) <see cref="IntPtr"/> </param>
		/// <param name="xtWidgetSource"> Specifies the widget whose actions will be invoked when events occur in destination. Must be of
		/// class Core or any subclass thereof. (E.g. a push button widget with '<KeyPress>q: notify()') <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtInstallAccelerators (IntPtr xtWidgetDestination, IntPtr xtWidgetSource);
		
		// Tested: O.K.
		/// <summary> Install all accelerators from a widget and its descendants onto a destination widget.</summary>
		/// <param name="xtWidgetDestination" > Specifies the widget in which events specified in the  accelerator  table will be detected.
		/// Must be of class Core or any subclass thereof. (E.g. a shell widget) <see cref="IntPtr"/> </param>
		/// <param name="xtWidgetSource"> Specifies the widget whose actions will be invoked when events occur in destination. Must be of
		/// class Core or any subclass thereof. (E.g. a push button widget with '<KeyPress>q: notify()') <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtInstallAllAccelerators (IntPtr xtWidgetDestination, IntPtr xtWidgetSource);
			
		// Tested: O.K.
		/// <summary> Register	an action table with the Translation Manager for the application context. </summary>
		/// <param name="appContext"> The application context that identifies the application. <see cref="System.IntPtr"/> </param>
		/// <param name="actionList">  The action table to register. <see cref="XtActionsRec[]"/> </param>
		/// <param name="numActions"> The number of actions to register. <see cref="XCardinal"/> </param>
		/// <remarks> If more than one action is registered with the same name, the most recently registered action is used.
		/// If duplicate actions exist in an action table, the first is used.
		/// XtAppAddActions () registers actions globally to an application context. This is in contrast to actions
		/// placed in the Core widget class part actions field which are defined locally to a widget class only. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtAppAddActions (IntPtr appContext, XtActionsRec[] actionList, XCardinal numActions);
		
		// Tested: O.K.
		/// <summary> Obtain widget's name . </summary>
		/// <param name="widget"> The widget to obtain the name for. <see cref="System.IntPtr"/> </param>
		/// <returns> The requested name on success, or System.IntPtr.Zero otherwise. <see cref="System.IntPtr"/> </returns>
		/// <remarks> The name string is owned by the widget and must not be freed nor schold be manipulated. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static IntPtr XtName (IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Obtain widget's name . </summary>
		/// <param name="widget"> The widget to obtain the name for. <see cref="System.IntPtr"/> </param>
		/// <returns> The requested name on success, or System.IntPtr.Zero otherwise. <see cref="System.String"/> </returns>
		/// <remarks> The name string is owned by the widget and must not be freed nor schold be manipulated. </remarks>
		public static string XtNameAsString (IntPtr xtWidget)
		{
			if (xtWidget == IntPtr.Zero)
				return String.Empty;
			
			IntPtr hWidgetName = Xtlib.XtName (xtWidget);
			if (hWidgetName == IntPtr.Zero)
				return String.Empty;
			
			return Marshal.PtrToStringAuto(hWidgetName);
		}
		
		// Tested: O.K.
		/// <summary> Translate widget coordinates to root window coordinates. </summary>
		/// <param name="widget"> The widget, who's coordinates are to translate. <see cref="System.IntPtr"/> </param>
		/// <param name="x"> Additional x offset. <see cref="XPosition"/> </param>
		/// <param name="y"> Additional y offset. <see cref="XPosition"/> </param>
		/// <param name="rootX"> [OUT] The widget coordinate relative to the root window. <see cref="XPosition"/> </param>
		/// <param name="rootY"> [OUT] The widget coordinate relative to the root window. <see cref="XPosition"/> </param>
		/// <remarks> While XtTranslateCoords is similar to the Xlib XTranslateCoordinates tunction, it does not generate a
		/// server request because all the required information already is in the widget's data structures. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtTranslateCoords (IntPtr widget, XPosition x, XPosition y, ref XPosition rootX, ref XPosition rootY);
		
		// Tested: O.K.
		/// <summary> Map a popup shell. </summary>
		/// <param name="xtPopupShell"> The popup shell to map, returned by XtCreatePopupShell(). <see cref="System.IntPtr"/> </param>
		/// <param name="grabKind">
		/// A <see cref="XtGrabKind"/> Sests the dialog box nonmodal, tartly modal or modal. </param>
		/// <remarks> Calls the functions registered on the shell's XtNpopupCallback list and pops up the shell widget (and its managed child). </remarks>
		/// <remarks> If grabKind is XtGrabNone, the resulting popup is "modeless", and does not  lock  out  input  events to the rest of the application.
		/// If it is XtGrabNonexclusive, then the resulting popup is "modal" and locks out input to the main application window, but not to other modal
		/// popups that are currently popped up. If it is XtGrabExclusive, then the resulting popup is modal and locks out input to the main application
		/// window and all previous popup windows. For more details on  XtGrabNonexclusive and XtGrabExclusive, see XtAddGrab(). </remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtPopup (IntPtr xtPopupShell, XtGrabKind grabKind);
		
		// Tested: O.K.
		/// <summary> Unmap a popup shell. </summary>
		/// <param name="xtPopupShell"> The popup shell to unmap, returned by XtCreatePopupShell(). <see cref="System.IntPtr"/> </param>
		/// <remarks> XtPopdown() Pops down a popup shell and calls the functions registered on the shell's XtNpopdownCallback list. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtPopdown (IntPtr xtPopupShell);
		
		// Tested: O.K.
		/// <summary> Determine whether a widget has been realized. </summary>
		/// <param name="xtWidget"> The object whose state is to be tested; may be of class Object or any subclass thereof. <see cref="System.IntPtr"/> </param>
		/// <returns> True if object (or its nearest widget ancestor) is realized, false otherwise. <see cref="TBoolean"/> </returns>

		[DllImport ("libXt.so.6")]
		public extern static TBoolean XtIsRealized (IntPtr xtWidget);
		/// <summary> Query a child widget's preferred geometry. </summary>
		/// <param name="xtWidget"> The widget to query the geometry for. <see cref="IntPtr"/> </param>
		/// <param name="intended"> The changes the parent plans to make to the child's geometry, or NULL. Use the alternative declaration to pass NULL. <see cref="XtWidgetGeometry"/> </param>
		/// <param name="preferred"> The child widget's preferred geometry. <see cref="XtWidgetGeometry"/> </param>
		/// <returns> A response to the request:  XtGeometryYes,  XtGeometryNo,  or  XtGeometryAlmost. <see cref="XtGeometryResult"/> </returns>
		/// <remarks> XtQueryGeometry() invokes a widget's query_geometry() method to determine its preferred (or at least its current)	geometry.
		/// The intended structure specifies the geometry values that the parent plans to set. The geometry_return structure returns the child's
		/// preferred geometry. Each argument has a flags field in which bits are set to indicate which of the geometry fields the respective
		/// widgets have set. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static XtGeometryResult XtQueryGeometry (IntPtr xtWidget, ref XtWidgetGeometry intended, ref XtWidgetGeometry preferred);
		
		// Tested: O.K.
		/// <summary> Query a child widget's preferred geometry. </summary>
		/// <param name="xtWidget"> The widget to query the geometry for. <see cref="IntPtr"/> </param>
		/// <param name="intended"> The changes the parent plans to make to the child's geometry, or NULL. Use the alternative declaration to pass a XtWidgetGeometry. <see cref="System.IntPtr"/> </param>
		/// <param name="preferred"> The child widget's preferred geometry. <see cref="XtWidgetGeometry"/> </param>
		/// <returns> A response to the request:  XtGeometryYes,  XtGeometryNo,  or  XtGeometryAlmost. <see cref="XtGeometryResult"/> </returns>
		/// <remarks> XtQueryGeometry() invokes a widget's query_geometry() method to determine its preferred (or at least its current)	geometry.
		/// The intended structure specifies the geometry values that the parent plans to set. The geometry_return structure returns the child's
		/// preferred geometry. Each argument has a flags field in which bits are set to indicate which of the geometry fields the respective
		/// widgets have set. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static XtGeometryResult XtQueryGeometry (IntPtr xtWidget, IntPtr intended, ref XtWidgetGeometry preferred);
		
		// Tested: O.K.
		/// <summary> Resize a child widget. </summary>
		/// <param name="xtWidget"> The widget to resize. <see cref="IntPtr"/> </param>
		/// <param name="width"> The new width. <see cref="XDimension"/> </param>
		/// <param name="height"> The new height. <see cref="XDimension"/> </param>
		/// <param name="borderWidth"> The new border width. <see cref="XDimension"/> </param>
		/// <remarks> XtResizeWidget() changes the width, height, and border width of the widget as specified. It stores the new values into the
		/// widget record, and if the widget is realized, calls XConfigureWindow() to change the size of the widget's window. Whether or not the
		/// widget is realized, XtResizeWidget() calls the widget's resize() method to notify it of	the size changes. XtResizeWidget() should only
		/// be used by a parent widget to change the size of its children. </remarks>
		[DllImport ("libXt.so.6")]
		public extern static void XtResizeWidget(IntPtr xtWidget, XDimension width, XDimension height, XDimension borderWidth);
		
		// Tested: O.K.
		/// <summary> Set a widget's sensitivity state. </summary>
		/// <param name="xtWidget"> The widget to set sensitivity. <see cref="IntPtr"/> </param>
		/// <param name="senitivity"> Specifies a boolean value that indicates whether the widget should
		/// receive keyboard and pointer events. <see cref="TBoolean"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtSetSensitive (IntPtr xtWidget, TBoolean senitivity);
		
		/// <summary> Get a widget's sensitivity state. </summary>
		/// <param name="xtWidget"> The widget to set sensitivity. <see cref="IntPtr"/> </param>
		/// <returns> The boolean value that indicates whether the widget can receive keyboard and pointer events. <see cref="TBoolean"/> </returns>
		[DllImport ("libXt.so.6")]
		public extern static TBoolean XtIsSensitive (IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Manage indicated widget. </summary>
		/// <param name="xtWidget"> The widget to manage. <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtManageChild(IntPtr xtWidget);
		
		// Tested: O.K.
		/// <summary> Unmanage indicated widget. </summary>
		/// <param name="xtWidget"> The widget to unmanage. <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static void XtUnmanageChild(IntPtr xtWidget);
				
		/// <summary> Check indicated widget for managed. </summary>
		/// <param name="xtWidget"> The widget to check. <see cref="IntPtr"/> </param>
		[DllImport ("libXt.so.6")]
		public extern static TBoolean XtIsManaged(IntPtr xtWidget);
		
		
		
		// Tested: O.K.
		/// <summary> Transform a managed text to a native string. </summary>
		/// <param name="text"> The managed text to transform. <see cref="System.String"/> </param>
		/// <returns> The pointer to a native (unmanaged) string. <see cref="IntPtr"/> </returns>
		[DllImport ("libXawNative.so")]
		public extern static IntPtr TransformToCharArray ([MarshalAs(UnmanagedType.LPStr)] string text);

		// Tested: O.K.
		/// <summary> Free the memory of a native (unmanaged) sting. </summary>
		/// <param name="text"> The pointer to the native (unmanaged) sting.<see cref="IntPtr"/> </param>
		[DllImport ("libXawNative.so")]
		public extern static void TransformFreeCharArray (IntPtr text);
		
		// Tested: O.K.
		/// <summary> Transform a managed text to a native string array using indicated delimiter to split. </summary>
		/// <param name="text"> The managed text to transform. <see cref="System.String"/> </param>
		/// <param name="delimiter"> The delimiter to use for split. <see cref="TChar"/> </param>
		/// <returns> The pointer to a native (unmanaged) string array, where the last array entry is NULL. <see cref="IntPtr"/> </returns>
		[DllImport ("libXawNative.so")]
		public extern static IntPtr TransformToArrayOfCharArray ([MarshalAs(UnmanagedType.LPStr)] string text, TChar delimiter);
		
		// Tested: O.K.
		/// <summary> Free the memory of a native (unmanaged) sting array. </summary>
		/// <param name="array"> The pointer to the native (unmanaged) sting array.<see cref="IntPtr"/> </param>
		[DllImport ("libXawNative.so")]
		public extern static void TransformFreeArrayOfCharArray (IntPtr array);
		
		// Tested: O.K.
		/// <summary> Transform a managed text to a native byte array using space ' ' as delimiter to split. </summary>
		/// <param name="text"> The managed text to transform. <see cref="System.String"/> </param>
		/// <param name="size"> The number of byte values in the array / the array length. <see cref="uint"/> </param>
		/// <returns> The pointer to a native (unmanaged) byte array, where the last array entry is NULL. <see cref="IntPtr"/> </returns>
		[DllImport ("libXawNative.so")]
		public extern static IntPtr TransformToArrayOfByte ([MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.SysUInt)] ref uint size);
		
		// Tested: O.K.
		/// <summary> Free the memory of a native (unmanaged) byte array. </summary>
		/// <param name="array"> The pointer to the native (unmanaged) byte array.<see cref="IntPtr"/> </param>
		[DllImport ("libXawNative.so")]
		public extern static void TransformFreeArrayOfByte (IntPtr array);
		
		// Tested: O.K.
		/// <summary> Transform a managed text to a native integer array using space ' ' as delimiter to split. </summary>
		/// <param name="text"> The managed text to transform. <see cref="System.String"/> </param>
		/// <param name="size"> The number of integer values in the array / the array length. <see cref="uint"/> </param>
		/// <returns> The pointer to a native (unmanaged) integer array, where the last array entry is NULL. <see cref="IntPtr"/> </returns>
		[DllImport ("libXawNative.so")]
		public extern static IntPtr TransformToArrayOfInt ([MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.SysUInt)] ref uint size);
		
		// Tested: O.K.
		/// <summary> Free the memory of a native (unmanaged) integer array. </summary>
		/// <param name="array"> The pointer to the native (unmanaged) integer array.<see cref="IntPtr"/> </param>
		[DllImport ("libXawNative.so")]
		public extern static void TransformFreeArrayOfInt (IntPtr array);
		
		
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawAsciiTextWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawBoxWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawCanvasWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static void ClearCanvas (IntPtr canvasWidget);
		
		[DllImport ("libXawNative.so")]
		public extern static void ExposeCanvas (IntPtr canvasWidget);
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr CanvasPixmap (IntPtr canvasWidget);
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawCommandWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawDialogWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawFormWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawGripWidgetClass ();		
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawLabelWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawMenuButtonWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawOverrideShellWidgetClass();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawPanedWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawScrollbarWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawSimpleWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawSimpleMenuWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawSmeWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawSmeBSBWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawTextWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawTransientShellWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawTreeWidgetClass ();
		
		[DllImport ("libXawNative.so")]
		public extern static IntPtr XawViewportWidgetClass ();

	}
}

