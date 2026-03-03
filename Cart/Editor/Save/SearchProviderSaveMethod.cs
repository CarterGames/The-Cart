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
using CarterGames.Cart.Save;

namespace CarterGames.Cart.Editor
{
	public class SearchProviderSaveMethod : SearchProvider<ISaveMethod>
	{
		private static SearchProviderSaveMethod Instance;

		protected override string ProviderTitle => "Select Save Method";
		public override bool HasOptions => AssemblyHelper.GetClassesOfType<ISaveMethod>(false)?.Count() > 0;
		
		
		public override List<SearchGroup<ISaveMethod>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<ISaveMethod>>();
			var entries = new List<SearchItem<ISaveMethod>>();
			var options = AssemblyHelper.GetClassesOfType<ISaveMethod>(false).Where(t => !ToExclude.Contains(t));

			foreach (var entry in options)
			{
				entries.Add(SearchItem<ISaveMethod>.Set(entry.GetType().Name, entry));
			}
			
			list.Add(new SearchGroup<ISaveMethod>(string.Empty, entries));
			
			return list;
		}
		
		
		public static SearchProviderSaveMethod GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderSaveMethod>();
			}

			return Instance;
		}
	}
}