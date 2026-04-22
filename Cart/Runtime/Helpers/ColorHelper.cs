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
        /// Converts a html color code to a color. Uses "ColorUtility.TryParseHtmlString" without the 
        /// </summary>
        /// <param name="input">The string to convert. Needs the # at the start to work.</param>
        /// <returns>The color from the string, Default is pink if it didn't work.</returns>
        public static Color HtmlStringToColor(string input)
        {
            return ColorUtility.TryParseHtmlString(input, out var col) 
                ? col 
                : Color.magenta;
        }
    }
}