using UnityEngine;

namespace CarterGames.Cart.Core
{
	public static class TextureExtensions
	{
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