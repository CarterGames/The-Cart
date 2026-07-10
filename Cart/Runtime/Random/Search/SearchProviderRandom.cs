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

namespace CarterGames.Cart.Random
{
	public class SearchProviderRandom : SearchProviderClassDef
	{
		private static IEnumerable<IRngProvider> cacheProviders;
		private static IEnumerable<IRngProvider> cacheSeededProviders;

		public override string ProviderTitle => "Select Random Provider";
		public override bool HasOptions => GetEntriesToDisplay().Any();


		protected override List<SearchGroup<AssemblyClassDef>> GetEntriesToDisplay()
		{
			if (cacheProviders == null)
			{
				cacheProviders = AssemblyHelper.GetClassesOfType<IRngProvider>().Where(t => !ToExclude.Contains(t.GetType()));
				cacheSeededProviders = AssemblyHelper.GetClassesOfType<ISeededRngProvider>().Where(t => !ToExclude.Contains(t.GetType())).ToList();
			}
			
			var list = new List<SearchGroup<AssemblyClassDef>>();
			var entries = new List<SearchItem<AssemblyClassDef>>();
			var seededEntries = new List<SearchItem<AssemblyClassDef>>();
			
			foreach (var entry in cacheProviders)
			{
				if (cacheSeededProviders.Any(t => t.GetType() == entry.GetType()))
				{
					seededEntries.Add(SearchItem<AssemblyClassDef>.Set(entry.GetType().Name.Replace("RngProvider", string.Empty), entry.GetType()));
				}
				else
				{
					entries.Add(SearchItem<AssemblyClassDef>.Set(entry.GetType().Name.Replace("RngProvider", string.Empty), entry.GetType()));
				}
			}
			
			list.Add(new SearchGroup<AssemblyClassDef>("Seeded Random Providers", seededEntries));
			list.Add(new SearchGroup<AssemblyClassDef>(string.Empty, entries));
			
			return list;
		}
	}
}