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

using System.Linq;
using System.Text.RegularExpressions;

namespace CarterGames.Cart
{
    /// <summary>
    /// An extensions class for strings.
    /// </summary>
    public static class StringExtensions
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string Space = " ";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Extension Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Replaces any " " with "" instead.
        /// </summary>
        /// <param name="entry">The string to modify.</param>
        /// <returns>The edited string.</returns>
        public static string TrimSpaces(this string entry)
        {
            return entry.Replace(Space, string.Empty);
        }


        /// <summary>
        /// Inserts a space after every capital letter in the string.
        /// </summary>
        /// <param name="entry">The string to modify.</param>
        /// <returns>The edited string.</returns>
        public static string SplitCapitalsWithSpace(this string entry)
        {
            return SplitCamelCase(entry);
        }

        
        private static string SplitCamelCase(this string input, string delimeter = Space)
        {
            return input.Any(char.IsUpper) ? string.Join(delimeter, Regex.Split(input, "(?<!^)(?=[A-Z])")) : input;
        }


        /// <summary>
        /// Splits the string into characters and gets the last element.
        /// </summary>
        /// <param name="input">The string to read.</param>
        /// <param name="character">The last character of the string.</param>
        /// <returns>The last character of the string.</returns>
        public static string SplitAndGetLastElement(this string input, char character)
        {
            return input.Split(character).Last();
        }
    }
}