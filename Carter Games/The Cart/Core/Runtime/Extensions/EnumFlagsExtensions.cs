/*
 * Copyright (c) 2025 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

namespace CarterGames.Cart.Core
{
	/// <summary>
	/// A extension class for enum flags specifically.
	/// </summary>
	public static class EnumFlagsExtensions
	{
		/// <summary>
		/// Gets the total number of flags selected
		/// </summary>
		/// <param name="value">The value to read.</param>
		/// <typeparam name="T">The type to read as.</typeparam>
		/// <returns>The total options in the flags enunm.</returns>
		public static int EnumFlagsCount<T>(this T value)
		{
			if (value.ToString() == "-1")
			{
				return Enum.GetValues(typeof(T)).Length;
			}
			
			return value.ToString().Split(',').Length;
		}


		/// <summary>
		/// Converts the entered value into its enum flagged type as an array for use.
		/// </summary>
		/// <param name="value">The value to read.</param>
		/// <typeparam name="T">The type to read as.</typeparam>
		/// <returns>An enum array of the flagged items.</returns>
		public static T[] EnumFlagsToArray<T>(this T value)
		{
			if (value.ToString() == "-1")
			{
				return (T[]) Enum.GetValues(typeof(T));
			}
			
			var flagsAsStrings = value.ToString().Split(',');
			var parsed = new T[flagsAsStrings.Length];

			for (var i = 0; i < parsed.Length; i++)
			{
				parsed[i] = (T) Enum.Parse(typeof(T), flagsAsStrings[i]);
			}

			return parsed;
		}
	}
}