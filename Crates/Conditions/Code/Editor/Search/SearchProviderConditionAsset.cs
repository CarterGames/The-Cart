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
	public class SearchProviderConditionAsset : SearchProvider<Condition>
	{
		private static SearchProviderConditionAsset Instance;
		private Condition[] conditionsCache;
		

		protected override string ProviderTitle => "Select Condition";
		public override bool HasOptions => Conditions.Any();

		
		private Condition[] Conditions => CacheRef.GetOrAssign(ref conditionsCache,
			AssetDatabaseHelper.GetAllInstancesInProject<Condition>());


		public override List<SearchGroup<Condition>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<Condition>>();
			var items = new List<SearchItem<Condition>>();
			
			foreach (var asset in Conditions)
			{
				if (ToExclude.Contains(asset)) continue;
				items.Add(SearchItem<Condition>.Set(asset.Id, asset));
			}

			list.Add(new SearchGroup<Condition>(items));
			
			return list;
		}


		public static SearchProviderConditionAsset GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderConditionAsset>();
			}

			return Instance;
		}
	}
}

#endif