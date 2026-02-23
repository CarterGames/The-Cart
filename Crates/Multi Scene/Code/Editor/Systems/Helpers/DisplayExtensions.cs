#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

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

using System.Collections.Generic;
using System.Linq;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// An extension class to get help formatting of the group categories & scene groups into something legible...
    /// </summary>
    public static class DisplayExtensions
    {
        /// <summary>
        /// Converts the scene group names into a usable array of options to select from...
        /// </summary>
        /// <param name="input">The strings to edit</param>
        /// <returns>The edited list as an array...</returns>
        public static string[] ToDisplayOptions(this List<string> input)
        {
            var array = new string[input.Count];

            for (var i = 0; i < input.Count; i++)
            {
                array[i] = (input[i].Equals(string.Empty)
                    ? "Unassigned (Blank)"
                    : input[i]);
            }

            return array;
        }
        
        
        /// <summary>
        /// Converts the scene group names into a usable array of options to select from...
        /// </summary>
        /// <param name="input">The strings to edit</param>
        /// <returns>The edited list as an array...</returns>
        public static string[] ToDisplayOptions<T>(this Dictionary<string, T> input)
        {
            var array = new string[input.Count];
            var keys = input.Keys.ToArray();

            for (var i = 0; i < input.Count; i++)
            {
                array[i] = (keys[i].Equals(string.Empty)
                    ? "Unassigned (Blank)"
                    : keys[i]);
            }

            return array;
        }
    }
}

#endif