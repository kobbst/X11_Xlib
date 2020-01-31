using System;

namespace System
{
	public static class StringExtensions
	{
		/// <summary>Test whether 'text', starting at 'startIndex', is equal to 'predicate'.</summary>
		/// <param name="text">The string to test.<see cref="System.String"/></param>
		/// <param name="startIndex">The index to start the test.<see cref="System.Int32"/></param>
		/// <param name="predicate">The string to compare to.<see cref="System.String"/></param>
		/// <returns>True if substring is equal to predicate, or false otherwise.<see cref="System.Boolean"/></returns>
		public static bool SubstringEqual(this String text, int startIndex, string predicate)
		{
			if (string.IsNullOrEmpty (text))
				return false;
			if (string.IsNullOrEmpty (predicate))
				return false;
			
			if (text.Length - startIndex < predicate.Length)
				return false;
			
			for (int index = 0; index < predicate.Length; index++)
				if (text[startIndex + index] != predicate[index])
					return false;
			
			return true;
		}

		/// <summary>Test whether 'text', starting at 'startIndex', matches the 'predicate'.</summary>
		/// <param name="text">The string to test.<see cref="System.String"/></param>
		/// <param name="startIndex">The index to start the test.<see cref="System.Int32"/></param>
		/// <param name="predicate">The mask string to compare to. Accepts '?' as single-char joker and '*' as multi-char joker.<see cref="System.String"/></param>
		/// <returns>The matching string if substring matches predicate, or string.Empty otherwise.<see cref="System.String"/></returns>
		public static string SubstringMatch(this String text, int startIndex, string predicate)
		{
			if (string.IsNullOrEmpty (text))
				return string.Empty;
			if (string.IsNullOrEmpty (predicate))
				return string.Empty;
			
			if (text.Length - startIndex < predicate.Length)
				return string.Empty;
			
			string result = string.Empty;
			
			for (int index = 0; index < predicate.Length; index++)
			{
				// [1] Either current char matches the predicate's current mask char.
				if (text[startIndex + index] == predicate[index] ||
				    predicate[index] == '*' /* '*' = Multi-char joker! */ ||
				    predicate[index] == '?' /* '?' = Single-char joker! */)
					result += text[startIndex + index];
				// [2] Or the predicate's current mask char is a multi-char joker.
				else if (index > 0 && predicate[index - 1] == '*' /* Multi-char joker! */)
				{
					// [A] Either multi-char joker represents a sub-string length of 0 characters.
					if (text[startIndex + index - 1] == predicate[index])
						startIndex--;
					// [B] Or the multi-char joker represents a sub-string length of >= 1 character(s).
					else
					{
						do
						{
							// [a] Either current char matches the predicate's current mask char.
							if (text[startIndex + index] == predicate[index])
							{
								result += text[startIndex + index];
								break;
							}
							// [b] Or the current char isn't the last char and matches the predicate's last joker.
							if (startIndex + index < text.Length - 1)
							{
								result += text[startIndex + index];
								startIndex++;
							}
							// [c] Or the current char don't match at all.
							else
								return string.Empty;
						} while (true);
					}
				}
				// [3] Or the current char don't match at all.
				else
					return string.Empty;
			}
			return result;
		}
		
	}
}

