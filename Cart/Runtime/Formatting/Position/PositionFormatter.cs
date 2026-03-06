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

namespace CarterGames.Cart
{
    /// <summary>
    /// Formats numbers into positions.
    /// </summary>
    public static class PositionFormatter
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Formatter<IPositionFormatter> formatter;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static Formatter<IPositionFormatter> Formatter
        {
            get
            {
                if (formatter != null) return formatter;
                formatter = new Formatter<IPositionFormatter>();
                return formatter;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the int as money.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The money formatter type to use.</typeparam>
        /// <returns>The formatted string</returns>
        public static string Format<T>(this int value) where T : IPositionFormatter
        {
            return Formatter.Get<T>().Format(value);
        }
    }
}