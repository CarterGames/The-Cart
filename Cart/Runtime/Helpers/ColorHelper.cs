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
    public static class ColorHelper
    {
        /// <summary>
        /// Converts a html color code to a color. Uses the built in <see cref="ColorUtility.TryParseHtmlString"/> method.
        /// </summary>
        /// <param name="input">The string to convert. Needs the # at the start to work.</param>
        /// <returns>The color from the string, Default is pink if it didn't work.</returns>
        public static Color HtmlStringToColor(string input)
        {
            return ColorUtility.TryParseHtmlString(input, out var col) 
                ? col 
                : Color.magenta;
        }
        
        
        /// <summary>
        /// Converts a color to a html color string. Uses the built in <see cref="ColorUtility.ToHtmlStringRGBA"/> method.
        /// </summary>
        /// <param name="input">The color to convert.</param>
        /// <returns>The string from the color.</returns>
        public static string ColorToHtmlString(Color input)
        {
            return ColorUtility.ToHtmlStringRGBA(input);
        }
    }
}