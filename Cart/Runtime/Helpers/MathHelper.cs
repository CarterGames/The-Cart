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
    /// A helper class for common math problems.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Gets the speed from a distance & time.
        /// </summary>
        /// <param name="distance">The distance to check.</param>
        /// <param name="time">The time to check</param>
        /// <returns>The speed.</returns>
        public static float GetSpeed(float distance, float time)
        {
            return distance / time;
        }
        
        
        /// <summary>
        /// Gets the distance from a speed & time.
        /// </summary>
        /// <param name="speed">The speed to check.</param>
        /// <param name="time">The time to check</param>
        /// <returns>The distance.</returns>
        public static float GetDistance(float speed, float time)
        {
            return speed * time;
        }
        
        
        /// <summary>
        /// Gets the time from a distance & speed.
        /// </summary>
        /// <param name="distance">The distance to check.</param>
        /// <param name="speed">The speed to check</param>
        /// <returns>The time.</returns>
        public static float GetTime(float distance, float speed)
        {
            return distance / speed;
        }
        
        
        /// <summary>
        /// Lerp functionally but with double. Clamped between 0-1.
        /// </summary>
        /// <param name="a">Value A</param>
        /// <param name="b">Value B</param>
        /// <param name="t">Time</param>
        /// <returns>The progress in the lerp.</returns>
        public static double Lerp(double a, double b, float t)
        {
            return a + (b - a) * Mathf.Clamp01(t);
        }
        
        
        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="percentageScale">The scale to read from.</param>
        /// <param name="value">The percentage to get from the scale.</param>
        /// <returns>The adjusted value.</returns>
        public static int Percentage(int percentageScale, int value)
        {
            return (value / percentageScale) * 100;
        }
        
        
        /// <summary>
        /// Gets the value adjusted by a percentage as a decimal.
        /// </summary>
        /// <param name="percentageScale">The scale to read from.</param>
        /// <param name="value">The percentage to get from the scale.</param>
        /// <returns>The adjusted value.</returns>
        public static int PercentageDecimal(int percentageScale, int value)
        {
            return value / percentageScale;
        }
        
        
        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="percentage">The percentage to get.</param>
        /// <returns>The adjusted value.</returns>
        public static float Percentage(this float value, float percentage)
        {
            return (value / 100) * percentage;
        }
        
        
        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="percentage">The percentage to get.</param>
        /// <returns>The adjusted value.</returns>
        public static float PercentageDecimal(this float value, float percentage)
        {
            return percentage * value;
        }
        

        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="percentage">The percentage to get.</param>
        /// <returns>The adjusted value.</returns>
        public static double Percentage(this double value, double percentage)
        {
            return (value / 100) * percentage;
        }
        
        
        /// <summary>
        /// Gets the value adjusted by a percentage.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="percentage">The percentage to get.</param>
        /// <returns>The adjusted value.</returns>
        public static double PercentageDecimal(this double value, double percentage)
        {
            return percentage * value;
        }
    }
}