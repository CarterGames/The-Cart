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

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A class that formats numbers into a formatted position.
    /// </summary>
    public static class PositionFormat
    {
        /// <summary>
        /// Returns the position entered formatted with the correct ending characters as a string...
        /// </summary>
        /// <param name="pos">The position to get</param>
        /// <returns>String of the position with the correct ending characters...</returns>
        public static string ToPosition(this int value)
        {
            var pos = value.ToString();
            
            // Catches the 11,12,13th's as they are different...
            if (pos.Contains("11"))
            {
                return $"{pos}th";
            }
            
            if (pos.Contains("12"))
            {
                return $"{pos}th";
            }
            
            if (pos.Contains("13"))
            {
                return $"{pos}th";
            }

            var toCheck = int.Parse(pos.Substring(pos.Length - 1, 1));

            return toCheck switch
            {
                1 => $"{pos}st",
                2 => $"{pos}nd",
                3 => $"{pos}rd",
                _ => $"{pos}th"
            };
        }
    }
}