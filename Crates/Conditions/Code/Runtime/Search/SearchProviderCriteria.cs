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

namespace CarterGames.Cart.Crates.Conditions
{
	public class SearchProviderCriteria : SearchProviderClassDef
	{
		public override string ProviderTitle => "Select Criteria";
		public override bool HasOptions => AssemblyHelper.CountClassesOfType<Criteria>() > 0;


		protected override List<SearchGroup<AssemblyClassDef>> GetEntriesToDisplay()
		{
			var group = new List<SearchGroup<AssemblyClassDef>>();
			var entries = new List<SearchItem<AssemblyClassDef>>();
			var instances = AssemblyHelper.GetClassNamesOfType<Criteria>();

			foreach (var entry in instances)
			{
				entries.Add(SearchItem<AssemblyClassDef>.Set(entry.Name.Replace("Criteria", string.Empty).SplitCapitalsWithSpace(),
					entry));
			}

			group.Add(new SearchGroup<AssemblyClassDef>(entries));

			return group;
		}
	}
}

#endif