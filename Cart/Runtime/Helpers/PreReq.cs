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