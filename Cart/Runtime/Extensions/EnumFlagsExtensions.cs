/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;

namespace CarterGames.Cart
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