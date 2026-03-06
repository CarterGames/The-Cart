#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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
using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates.DataValues.Editor.Search
{
	public class SearchProviderDataValueKeys : SearchProvider<string>
	{
		private static SearchProviderDataValueKeys Instance;

		protected override string ProviderTitle => "Select Data Value Key";
		public override bool HasOptions => AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>().Any();


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var items = new List<SearchItem<string>>();
			
			foreach (var asset in AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>())
			{
				if (ToExclude.Contains(asset.Key)) continue;
				items.Add(SearchItem<string>.Set(asset.Key, asset.Key));
			}

			list.Add(new SearchGroup<string>(items));
			
			return list;
		}


		public static SearchProviderDataValueKeys GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderDataValueKeys>();
			}

			return Instance;
		}
	}
}

#endif