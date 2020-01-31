// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: May 2015
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2015 Steffen Ploetz
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// This copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// //////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace X11
{
	public interface IPathFigureCollection : IList<X11.IPathFigure>, ICollection<X11.IPathFigure>, IEnumerable<X11.IPathFigure>, IEnumerable
	{
	}
	
	public interface IPathFigure
	{
		/// <summary>Get or set the start point coordinates.</summary>	
		System.Windows.Point Start {	get;	set;	}
		
		/// <summary>Get or set the collection of path segments, assigned to this figure.</summary>
		IPathSegmentsCollection Segments {	get;	set;	}
	}
	
	/// <summary>Represents a subsection of a geometry, a single connected series of two-dimensional geometric segments.</summary>
    public class X11PathFigure : IPathFigure
	{
		
        // ###############################################################################
        // ### C O N S T A N T S
        // ###############################################################################

        #region Constants

        /// <summary> The class name constant. </summary>
        public const string	CLASS_NAME = "X11PathFigure";

		#endregion Constants

		// ###############################################################################
        // ### A T T R I B U T E S
        // ###############################################################################

		#region Attributes		
		
		/// <summary>The start point coordinates.</summary>	
		public System.Windows.Point _start						= new System.Windows.Point (0, 0);
		
		/// <summary>The collection of path segments, defining this path figure.</summary>
		protected IPathSegmentsCollection _segmentCollection = null;
		
        #endregion Attributes
		
        // ###############################################################################
        // ### C O N S T R U C T I O N   A N D   I N I T I A L I Z A T I O N
        // ###############################################################################

        #region Construction
		
		/// <summary>The default constructor.</summary>
		public X11PathFigure ()
		{	_segmentCollection = new X11PathSegmentCollection ();	}
		
		/// <summary>The initializing constructor.</summary>
		public X11PathFigure (IEnumerable<IPathSegment> segments)
		{	_segmentCollection = new X11PathSegmentCollection (segments);
		}
		
        #endregion Construction
		
        // ###############################################################################
        // ### P R O P E R T I E S
        // ###############################################################################

        #region Properties
		
		/// <summary>Get or set the start point coordinates.</summary>	
		public System.Windows.Point Start
		{	get	{	return _start;	}
			set	{	_start = value;	}
		}
		
		/// <summary>Get or set the collection of path segments, assigned to this figure.</summary>
		public IPathSegmentsCollection Segments
		{	get	{	return _segmentCollection;	}
			set	{	_segmentCollection = value;	}
		}

        #endregion Properties
		
        // ###############################################################################
        // ### M E T H O D S
        // ###############################################################################
		
		#region Methods
		
		#endregion Methods

	}
}

