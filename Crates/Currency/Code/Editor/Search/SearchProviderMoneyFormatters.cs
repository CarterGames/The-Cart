#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Crates.Currency.Editor
{
    public class SearchProviderMoneyFormatters : SearchProvider<AssemblyClassDef>
    {
        private static SearchProviderMoneyFormatters Instance;
        private List<IMoneyFormatter> moneyFormatterTypes;
        
        
        protected override string ProviderTitle => "Select Money Formatter";

        public override bool HasOptions => AssemblyHelper.GetClassesNamesOfType<IMoneyFormatter>().Any();
        
        
        public override List<SearchGroup<AssemblyClassDef>> GetEntriesToDisplay()
        {
            IMoneyFormatter ignore = null;
            
            if (ToExclude.Count > 0)
            {
                if (ToExclude.First().IsValid)
                {
                    ignore = ToExclude.First().GetDefinedType<IMoneyFormatter>();
                }
            }
            
            var group = new List<SearchGroup<AssemblyClassDef>>();
            var entries = new List<SearchItem<AssemblyClassDef>>();
            var instances = AssemblyHelper.GetClassesOfType<IMoneyFormatter>();
			
            foreach (var entry in instances)
            {
                if (ignore?.GetType() == entry.GetType()) continue;
                entries.Add(SearchItem<AssemblyClassDef>.Set(entry.GetType().Name.Replace("MoneyFormatter", string.Empty).SplitCapitalsWithSpace(), entry.GetType()));
            }
			
            group.Add(new SearchGroup<AssemblyClassDef>(entries));

            return group;
        }
        
        
        public static SearchProviderMoneyFormatters GetProvider()
        {
            if (Instance == null)
            {
                Instance = CreateInstance<SearchProviderMoneyFormatters>();
            }

            return Instance;
        }
    }
}

#endif