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
    /// Formats numbers into time.
    /// </summary>
    public static class TimeFormat
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Formatter<ITimeFormatter> formatter;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Formatter<ITimeFormatter> Formatter
        {
            get
            {
                if (formatter != null) return formatter;
                formatter = new Formatter<ITimeFormatter>();
                return formatter;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
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