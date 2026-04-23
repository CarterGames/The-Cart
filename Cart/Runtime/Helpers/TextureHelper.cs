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
	/// Some helpers for textures.
	/// </summary>
	public static class TextureHelper
	{
		/// <summary>
		/// Creates a single color texture of the requested size and color.
		/// </summary>
		/// <param name="width">The width of the texture.</param>
		/// <param name="height">The height of the texture.</param>
		/// <param name="col">The color to set the texture to.</param>
		/// <returns>The generated texture.</returns>
		public static Texture2D SolidColorTexture2D(int width, int height, Color col)
		{
			var pix = new Color[width * height];

			for (var i = 0; i < pix.Length; i++)
			{
				pix[i] = col;
			}

			var result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();

			return result;
		}
	}
}