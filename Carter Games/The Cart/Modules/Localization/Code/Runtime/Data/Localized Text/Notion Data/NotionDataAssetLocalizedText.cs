#if CARTERGAMES_CART_MODULE_LOCALIZATION && CARTERGAMES_NOTIONDATA

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
using CarterGames.NotionData;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
	/// <summary>
	/// A Notion Data Asset for localized text.
	/// </summary>
	[CreateAssetMenu(fileName = "Notion Data Asset Localized Text",
		menuName = "Carter Games/The Cart/Modules/Localization/Notion/Notion Data Asset Localized Text")]
	[Serializable]
	public sealed class NotionDataAssetLocalizedText : NotionDataAsset<LocalizationData<string>>
	{
		private void Awake()
		{
			// Auto-assigns the text processor to the asset when created.
			processor = typeof(NotionDatabaseProcessorTextLocalization);
		}
	}
}

#endif