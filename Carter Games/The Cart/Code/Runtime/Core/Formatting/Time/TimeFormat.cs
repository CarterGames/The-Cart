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
using System.Collections.Generic;

namespace CarterGames.Cart.Core
{
    public static class TimeFormat
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Dictionary<Type, ITimeFormatter> formatterLookupCache;
        private static Dictionary<Type, ITimeFormatter> FormatterLookup { get; } = CacheRef.GetOrAssign(ref formatterLookupCache, GetFormatters);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Dictionary<Type, ITimeFormatter> GetFormatters()
        {
            return new Dictionary<Type, ITimeFormatter>()
            {
                { typeof(TimeFormatterStopWatchSimple), new TimeFormatterStopWatchSimple() },
                { typeof(TimeFormatterDayHourMinSecSimple), new TimeFormatterDayHourMinSecSimple() },
                { typeof(TimeFormatterDayHourMinSecDetailed), new TimeFormatterDayHourMinSecDetailed() },
            };
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a new time formatter instance if one doesn't exist at the time of requesting.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The formatter to use.</returns>
        private static ITimeFormatter Create<T>() where T : ITimeFormatter
        {
            var formatter = Activator.CreateInstance<T>();
            FormatterLookup.Add(typeof(T), formatter);
            return formatter;
        }


        /// <summary>
        /// Gets or make the time formatter requested for use.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The formatter to use.</returns>
        private static ITimeFormatter Get<T>() where T : ITimeFormatter
        {
            return FormatterLookup.ContainsKey(typeof(T)) 
                ? FormatterLookup[typeof(T)] 
                : Create<T>();
        }


        /// <summary>
        /// Formats the int as a time formatter.
        /// </summary>
        /// <param name="secondsLeft">The seconds to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string FormatAsTime<T>(this int secondsLeft) where T : ITimeFormatter
        {
            return Get<T>().Format(TimeSpan.FromSeconds(secondsLeft));
        }
        
        
        /// <summary>
        /// Formats the double as a time formatter.
        /// </summary>
        /// <param name="secondsLeft">The seconds to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string FormatAsTime<T>(this double secondsLeft) where T : ITimeFormatter
        {
            return Get<T>().Format(TimeSpan.FromSeconds(secondsLeft));
        }
        
        
        /// <summary>
        /// Formats the float as a time formatter.
        /// </summary>
        /// <param name="secondsLeft">The seconds to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string FormatAsTime<T>(this float secondsLeft) where T : ITimeFormatter
        {
            return Get<T>().Format(TimeSpan.FromSeconds(secondsLeft));
        }
        
        
        /// <summary>
        /// Formats the timespan as a time formatter.
        /// </summary>
        /// <param name="timeSpan">The timespan to convert.</param>
        /// <typeparam name="T">The time formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string FormatAsTime<T>(this TimeSpan timeSpan) where T : ITimeFormatter
        {
            return Get<T>().Format(timeSpan);
        }
    }
}