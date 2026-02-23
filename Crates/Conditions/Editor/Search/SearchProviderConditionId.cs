#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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
using CarterGames.Cart;
using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates.Conditions.Editor.Search
{
	public class SearchProviderConditionId : SearchProvider<string>
	{
		private static SearchProviderConditionId Instance;
		private Condition[] conditionsCache;

		protected override string ProviderTitle => "Select Condition Id";
		public override bool HasOptions => Conditions.Any();
		
		private Condition[] Conditions => CacheRef.GetOrAssign(ref conditionsCache,
			AssetDatabaseHelper.GetAllInstancesInProject<Condition>());


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var items = new List<SearchItem<string>>();
			
			foreach (var asset in Conditions)
			{
				if (ToExclude.Contains(asset.Id)) continue;
				items.Add(SearchItem<string>.Set(asset.Id, asset.VariantId));
			}

			list.Add(new SearchGroup<string>(items));
			
			return list;
		}


		public static SearchProviderConditionId GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderConditionId>();
			}

			return Instance;
		}
	}
}

#endif