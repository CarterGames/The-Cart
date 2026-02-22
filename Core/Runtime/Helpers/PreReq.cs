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

using System;

namespace CarterGames.Cart.Core
{
    /// <summary>
    /// A class to handle common checks that can act as blockers if returning false.
    /// </summary>
    public static class PreReq
    {
        /// <summary>
        /// Stops logic if a reference is null.
        /// </summary>
        /// <param name="reference">The reference to check</param>
        /// <param name="message">The message to show in the error if null.</param>
        /// <typeparam name="T">The type of the reference.</typeparam>
        public static void DisallowIfNull<T>(T reference, string message = "")
        {
            if (reference is UnityEngine.Object obj && ((obj ? obj : null) == null))
            {
                throw new ArgumentNullException(message);
            }

            if (reference is null)
            {
                throw new ArgumentNullException(message);
            }
        }
        

        /// <summary>
        /// Stops logic if the value is false.
        /// </summary>
        /// <param name="check">The bool to check</param>
        /// <param name="message">The message to show in the error if null.</param>
        public static void DisallowIfFalse(bool check, string message = "")
        {
            if (check) return; 
            throw new ArgumentNullException(message);
        }
    }
}