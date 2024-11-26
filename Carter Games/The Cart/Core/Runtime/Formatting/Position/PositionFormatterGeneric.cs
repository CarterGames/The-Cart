/*
 * Copyright (c) 2024 Carter Games
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

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A position formatter that formats to a generic 1st, 2nd, 3rd setup.
    /// </summary>
    public sealed class PositionFormatterGeneric : IPositionFormatter
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IPositionFormatter Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the entry.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The formatted string.</returns>
        public string Format(int value)
        {
            var pos = value.ToString("N0");
            var lastDigit = int.Parse(pos.ToCharArray().Last().ToString());

            if (pos.Length > 2)
            {
                pos = pos.Substring(pos.Length - 2, 2);
            }

            return pos switch
            {
                // Catches the 11,12,13th's as they are different.
                "11" => $"{value}th",
                "12" => $"{value}th",
                "13" => $"{value}th",
                
                // Otherwise normal formatting.
                _ => lastDigit switch
                {
                    1 => $"{value}st",
                    2 => $"{value}nd",
                    3 => $"{value}rd",
                    _ => $"{value}th"
                }
            };
        }
    }
}