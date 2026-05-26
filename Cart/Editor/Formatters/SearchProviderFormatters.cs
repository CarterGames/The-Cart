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
using UnityEngine;

namespace CarterGames.Cart.Editor
{
	public class SearchProviderFormatters : SearchProvider<AssemblyClassDef>
	{
		private static SearchProviderFormatters Instance;

		protected override string ProviderTitle => "Select Formatter";
		public override bool HasOptions => AssemblyHelper.GetClassesOfType<Formatter>()?.Count() > 0;
		private IGrouping<string, Formatter>[] Formatters { get; set; }


		public override List<SearchGroup<AssemblyClassDef>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<AssemblyClassDef>>();
			
			if (Formatters == null)
			{
				Formatters = AssemblyHelper.GetClassesOfType<Formatter>()
					.GroupBy(t => t.Category)
					.ToArray();
			}
			
			foreach (var group in Formatters)
			{
				var groupEntries = new List<SearchItem<AssemblyClassDef>>();
				
				foreach (var entry in group)
				{
					if (ToExclude.Any(t => t.IsDefineType(entry.GetType()))) continue;
					groupEntries.Add(SearchItem<AssemblyClassDef>.Set(entry.GetType().Name, entry.GetType()));
				}
				
				if (groupEntries.Count <= 0) continue;
				list.Add(new SearchGroup<AssemblyClassDef>(group.Key, groupEntries));
			}
			
			return list;
		}
		
		
		public static SearchProviderFormatters GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderFormatters>();
			}

			return Instance;
		}
	}
}