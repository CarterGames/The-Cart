#if CARTERGAMES_CART_CRATE_PARAMETERS && UNITY_EDITOR

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
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Crates.Parameters.Editor
{
	public class SearchProviderParameterKeys : SearchProvider<string>
	{
		private static SearchProviderParameterKeys Instance;
		private bool cacheHasOptions = false;
		private bool hasSetCache = false;

		private static List<string> IgnoreTypes = new List<string>()
		{
			typeof(ParameterGeneric<>).FullName
		};
		
		protected override string ProviderTitle => "Select Parameter";
		public override bool HasOptions
		{
			get
			{
				if (hasSetCache) return cacheHasOptions;
				cacheHasOptions = AssemblyHelper.CountClassesOfType<Parameter>() > 2;
				hasSetCache = true;
				return true;
			}
		}


		public override List<SearchGroup<string>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<string>>();
			var items = new List<SearchItem<string>>();
			
			foreach (var type in AssemblyHelper.GetClassesNamesOfType<Parameter>())
			{
				if (IgnoreTypes.Contains(type.FullName)) continue;
				items.Add(SearchItem<string>.Set(type.Name, type.FullName));
			}

			list.Add(new SearchGroup<string>(items));
			
			return list;
		}


		public static SearchProviderParameterKeys GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderParameterKeys>();
			}

			return Instance;
		}
	}
}

#endif