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

using UnityEngine;

namespace CarterGames.Cart
{
    /// <summary>
    /// An extension class for normal ints.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Inverts the value...
        /// </summary>
        /// <param name="original">The value to edit...</param>
        /// <returns>The inverted value</returns>
        public static int Invert(this int original)
        {
            return original * -1;
        }
        
        
        /// <summary>
        /// Inverts the value between 0-1...
        /// </summary>
        /// <param name="original">The value to edit...</param>
        /// <returns>The inverted value</returns>
        public static int Invert01(this int original)
        {
            return (int) Mathf.Clamp01(1f - original);
        }


        /// <summary>
        /// Converts an int to a bool anchored around 0.
        /// </summary>
        /// <param name="original">The int to use.</param>
        /// <returns>1+ = True, 0 or lower False.</returns>
        public static bool ParseAsBool(this int original)
        {
            return original > 0;
        }
        
        
        /// <summary>
        /// Normalises the int between a min & max.
        /// </summary>
        /// <param name="value">The value to edit.</param>
        /// <param name="max">The max value</param>
        /// <param name="min">The min value, is 0 by default.</param>
        /// <returns>The normalised int.</returns>
        public static int Normalise(this int value, int max, int min = 0)
        {
            return (value - min) / (max - min);
        }
        
        
        /// <summary>
        /// Normalises the int between a min & max.
        /// </summary>
        /// <param name="value">The value to edit.</param>
        /// <param name="intRange">The minmax range to use</param>
        /// <returns>The normalised int.</returns>
        public static int Normalise(this int value, IntRange intRange)
        {
            return (value - intRange.min) / (intRange.max - intRange.min);
        }


        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="percentage">The percentage to get.</param>
        /// <returns>The adjusted value.</returns>
        public static int Percentage(this int value, int percentage)
        {
            return (value / 100) * percentage;
        }
        
        
        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="percentage">The percentage to get.</param>
        /// <returns>The adjusted value.</returns>
        public static int Percentage01(this int value, int percentage)
        {
            return percentage * value;
        }
    }
}