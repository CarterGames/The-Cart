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
using CarterGames.Cart.Management;
using CarterGames.Cart.Random;

namespace CarterGames.Cart.Editor
{
	public class SearchProviderRandom : SearchProvider<IRngProvider>
	{
		private static SearchProviderRandom Instance;

		protected override string ProviderTitle => "Select Random Provider";
		public override bool HasOptions => AssemblyHelper.GetClassesOfType<IRngProvider>(false)?.Count() > 0;


		public override List<SearchGroup<IRngProvider>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<IRngProvider>>();
			var entries = new List<SearchItem<IRngProvider>>();
			var seededEntries = new List<SearchItem<IRngProvider>>();
			var options = AssemblyHelper.GetClassesOfType<IRngProvider>(false).Where(t => !ToExclude.Contains(t));
			var seededRandomOptions = AssemblyHelper.GetClassesOfType<ISeededRngProvider>(false).Where(t => !ToExclude.Contains(t)).ToList();
			
			foreach (var entry in options)
			{
				if (seededRandomOptions.Any(t => t.GetType() == entry.GetType()))
				{
					seededEntries.Add(SearchItem<IRngProvider>.Set(entry.GetType().Name.Replace("RngProvider", string.Empty), entry));
				}
				else
				{
					entries.Add(SearchItem<IRngProvider>.Set(entry.GetType().Name.Replace("RngProvider", string.Empty), entry));
				}
			}
			
			list.Add(new SearchGroup<IRngProvider>("Seeded Random Providers", seededEntries));
			list.Add(new SearchGroup<IRngProvider>(string.Empty, entries));
			
			return list;
		}
		
		
		public static SearchProviderRandom GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderRandom>();
			}

			return Instance;
		}
	}
}