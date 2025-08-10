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

using UnityEngine;

namespace CarterGames.Cart.Core
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
    }
}