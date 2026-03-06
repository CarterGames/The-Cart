#if CARTERGAMES_CART_CRATE_LOCALIZATION && CARTERGAMES_NOTIONDATA

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
using CarterGames.NotionData;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
	/// <summary>
	/// A Notion Data Asset for localized sprite.
	/// </summary>
	[CreateAssetMenu(fileName = "Notion Data Asset Localized Sprite",
		menuName = "Carter Games/The Cart/Crates/Localization/Notion/Notion Data Asset Localized Sprite")]
	[Serializable]
	public sealed class NotionDataAssetLocalizedSprite : NotionDataAsset<LocalizationData<Sprite>>
	{
		private void Awake()
		{
			// Auto-assigns the sprite processor to the asset when created.
			processor = typeof(NotionDatabaseProcessorSpriteLocalization);
		}
	}
}

#endif