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

using System.Linq;
using System.Text.RegularExpressions;

namespace CarterGames.Cart.Core
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


        public static string SplitCapitalsWithSpace(this string value)
        {
            return SplitCamelCase(value);
        }

        
        private static string SplitCamelCase(this string input, string delimeter = Space)
        {
            return input.Any(char.IsUpper) ? string.Join(delimeter, Regex.Split(input, "(?<!^)(?=[A-Z])")) : input;
        }
    }
}