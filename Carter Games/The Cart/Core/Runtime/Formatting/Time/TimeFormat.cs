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

using System;

namespace CarterGames.Cart.Core
{
    public static class TimeFormat
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Formatter<ITimeFormatter> formatter;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Formatter<ITimeFormatter> Formatter
        {
            get
            {
                if (formatter != null) return formatter;
                formatter = new Formatter<ITimeFormatter>();
                return formatter;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the int as a time formatter.
        /// </summary>
        /// <param name="secondsLeft">The seconds to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this int secondsLeft) where T : ITimeFormatter
        {
            return Formatter.Get<T>().Format(TimeSpan.FromSeconds(secondsLeft));
        }


        /// <summary>
        /// Formats the double as a time formatter.
        /// </summary>
        /// <param name="secondsLeft">The seconds to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this double secondsLeft) where T : ITimeFormatter
        {
            return Formatter.Get<T>().Format(TimeSpan.FromSeconds(secondsLeft));
        }


        /// <summary>
        /// Formats the float as a time formatter.
        /// </summary>
        /// <param name="secondsLeft">The seconds to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this float secondsLeft) where T : ITimeFormatter
        {
            return Formatter.Get<T>().Format(TimeSpan.FromSeconds(secondsLeft));
        }
        
        
        /// <summary>
        /// Formats the timespan as a time formatter.
        /// </summary>
        /// <param name="timeSpan">The timespan to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this TimeSpan timeSpan) where T : ITimeFormatter
        {
            return Formatter.Get<T>().Format(timeSpan);
        }
    }
}