#if CARTERGAMES_CART_CRATE_LOCALIZATION && UNITY_EDITOR

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
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates.Localization.Editor
{
	public sealed class SearchProviderLanguages : SearchProvider<Language>
	{
		private static SearchProviderLanguages Instance;

		protected override string ProviderTitle => "Select Language";
		public override bool HasOptions => DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.Count > 0;


		public override List<SearchGroup<Language>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<Language>>();
			var items = new List<SearchItem<Language>>();
			
			foreach (var language in DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.OrderBy(t => t.DisplayName))
			{
				items.Add(SearchItem<Language>.Set(language.DisplayName, language));
			}

			list.Add(new SearchGroup<Language>(items));
			
			return list;
		}


		public static SearchProviderLanguages GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderLanguages>();
			}

			return Instance;
		}
	}
}

#endif