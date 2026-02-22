#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

/*
 * Copyright (c) 2025 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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

        public override bool HasOptions => AssemblyHelper.GetClassesNamesOfType<IMoneyFormatter>(false).Any();
        
        
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
            var instances = AssemblyHelper.GetClassesOfType<IMoneyFormatter>(false);
			
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