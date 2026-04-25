#if CARTERGAMES_CART_CRATE_ANALYTICS && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.Analytics.Search
{
    public class SearchProviderAnalyticsProvider : SearchProvider<AssemblyClassDef>
    {
        private static SearchProviderAnalyticsProvider Instance;

        protected override string ProviderTitle => "Select Analytics Provider";
        public override bool HasOptions => AssemblyHelper.GetClassesOfType<AnalyticsProvider>()?.Count() > 0;
        private static IEnumerable<AnalyticsProvider> ProviderTypes { get; set; }


        public override List<SearchGroup<AssemblyClassDef>> GetEntriesToDisplay()
        {
            var list = new List<SearchGroup<AssemblyClassDef>>();
            var searchOptions = new List<SearchItem<AssemblyClassDef>>();
			
            if (ProviderTypes == null)
            {
                ProviderTypes = AssemblyHelper.GetClassesOfType<AnalyticsProvider>().ToArray();
            }
			
            foreach (var entry in ProviderTypes)
            {
                if (ToExclude.Any(t => t.IsDefineType(entry.GetType()))) continue;
                searchOptions.Add(SearchItem<AssemblyClassDef>.Set(entry.GetType().Name.Replace("AnalyticsProvider", string.Empty), entry.GetType()));
            }
            
            list.Add(new SearchGroup<AssemblyClassDef>(string.Empty, searchOptions));
			
            return list;
        }
		
		
        public static SearchProviderAnalyticsProvider GetProvider()
        {
            if (Instance == null)
            {
                Instance = CreateInstance<SearchProviderAnalyticsProvider>();
            }

            return Instance;
        }
    }
}

#endif