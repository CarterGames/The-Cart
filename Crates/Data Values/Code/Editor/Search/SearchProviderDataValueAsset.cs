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
	public class SearchProviderDataValueAsset : SearchProvider<DataValueAsset>
	{
		private static SearchProviderDataValueAsset Instance;

		protected override string ProviderTitle => "Select Data Value";
		public override bool HasOptions => AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>().Any();


		public override List<SearchGroup<DataValueAsset>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<DataValueAsset>>();
			var items = new List<SearchItem<DataValueAsset>>();
			
			foreach (var asset in AssetDatabaseHelper.GetAllInstancesInProject<DataValueAsset>())
			{
				if (ToExclude.Contains(asset)) continue;
				items.Add(SearchItem<DataValueAsset>.Set(asset.Key, asset));
			}

			list.Add(new SearchGroup<DataValueAsset>(items));
			
			return list;
		}


		public static SearchProviderDataValueAsset GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderDataValueAsset>();
			}

			return Instance;
		}
	}
}

#endif