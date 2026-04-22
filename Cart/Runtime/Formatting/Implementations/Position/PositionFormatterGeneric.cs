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

namespace CarterGames.Cart
{
    /// <summary>
    /// A position formatter that formats to a generic 1st, 2nd, 3rd setup.
    /// </summary>
    public sealed class PositionFormatterGeneric : Formatter
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override string Category => "Position";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Formats the entry.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The formatted string.</returns>
        public override string Format(double value)
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