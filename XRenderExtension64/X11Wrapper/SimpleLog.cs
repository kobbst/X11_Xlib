// ==================
// The X11 C# wrapper
// ==================

/*
 * Created by Mono Develop 2.4.1.
 * User: PloetzS
 * Date: July 2014
 * --------------------------------
 * Author: Steffen Ploetz
 * eMail:  Steffen.Ploetz@cityweb.de
 * 
 */

// //////////////////////////////////////////////////////////////////////
//
// Copyright (C) 2014 Steffen Ploetz
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
using System.Diagnostics;

namespace X11
{
	/// <summary>Provide a simple unified console logging.</summary>
	public static class SimpleLog
	{
		
		/// <summary>Define which kind of messages are to be logged.</summary>
		public static TraceEventType LogLevel = TraceEventType.Verbose;
		
		
		/// <summary>Log a line to a text output.</summary>
		/// <param name="level">The log level for the line to log. Logs with a level smaller than 'LogLevel' are ignored.<see cref="TraceEventType"/></param>
		/// <param name="format">The message format.<see cref="System.String"/></param>
		/// <param name="parameter">The (optional) message parameters.<see cref="System.Object[]"/></param>
		public static void LogLine (TraceEventType level, string format, params object[] parameter)
		{			
			if       (LogLevel == TraceEventType.Information &&
			          level == TraceEventType.Verbose)
				return;
			else if (LogLevel == TraceEventType.Warning &&
			         (level == TraceEventType.Verbose || level == TraceEventType.Information))
				return;
			else if (LogLevel == TraceEventType.Error &&
			         (level == TraceEventType.Verbose || level == TraceEventType.Information || level == TraceEventType.Warning))
				return;
			else if (LogLevel == TraceEventType.Critical &&
			         (level == TraceEventType.Verbose || level == TraceEventType.Information || level == TraceEventType.Warning || level == TraceEventType.Error))
				return;
			
			Console.WriteLine (SortableTime(DateTime.Now) + Prefix (level) + format, parameter);
		}
		
		/// <summary>Format a sortable time string from indicated date time.</summary>
		/// <param name="time">The date time to format a sortable time string from.<see cref="DateTime"/></param>
		/// <returns>The sortable time string from indicated date time.<see cref="System.String"/></returns>
		public static string SortableTime (DateTime time)
		{
			return String.Format ("{0:yyyy}-{0:MM}-{0:dd} {0:HH}:{0:mm}:{0:ss} ", time); // String.Format ("{0:s} ", now);
		}
		
		/// <summary>Calculate the message prefix from the log level.</summary>
		/// <param name="level">The log level to calculate the message prefix from.<see cref="TraceEventType"/></param>
		/// <returns>The message prefix from the log level.<see cref="System.String"/></returns>
		public static string Prefix (TraceEventType level)
		{
			string prefix = "";
			
			if       (level == TraceEventType.Verbose)
				prefix = "VERBOSE: ";
			else if (level == TraceEventType.Information)
				prefix = "INFORMATION: ";
			else if (level == TraceEventType.Warning)
				prefix = "WARNING: ";
			else if (level == TraceEventType.Error)
				prefix = "ERROR: ";
			else if (level == TraceEventType.Critical)
				prefix = "CRITICAL: ";
			
			else if (level == TraceEventType.Resume)
				prefix = "RESUME: ";
			else if (level == TraceEventType.Start)
				prefix = "START: ";
			else if (level == TraceEventType.Stop)
				prefix = "STOP: ";
			else if (level == TraceEventType.Suspend)
				prefix = "SUSPEND: ";
			else if (level == TraceEventType.Transfer)
				prefix = "TRANSFER: ";
			
			return prefix;
		}
	}
}