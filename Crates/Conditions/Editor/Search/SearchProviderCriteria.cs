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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Crates.Conditions
{
	public class SearchProviderCriteria : SearchProvider<Type>
	{
		private static SearchProviderCriteria Instance;

		protected override string ProviderTitle => "Select Criteria";
		public override bool HasOptions => AssemblyHelper.CountClassesOfType<Criteria>() > 0;


		public override List<SearchGroup<Type>> GetEntriesToDisplay()
		{
			var group = new List<SearchGroup<Type>>();
			var entries = new List<SearchItem<Type>>();
			var instances = AssemblyHelper.GetClassesNamesOfType<Criteria>();

			foreach (var entry in instances)
			{
				entries.Add(SearchItem<Type>.Set(entry.Name.Replace("Criteria", string.Empty).SplitCapitalsWithSpace(),
					entry));
			}

			group.Add(new SearchGroup<Type>(entries));

			return group;
		}
		
		
		public static SearchProviderCriteria GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderCriteria>();
			}

			return Instance;
		}
	}
}

#endif