#if CARTERGAMES_CART_MODULE_LOCALIZATION

/*
 * Copyright (c) 2024 Carter Games
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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    public class SearchProviderLocalizationIds : SearchProvider<string>
    {
        private static SearchProviderLocalizationIds Instance;
        
        protected override string ProviderTitle => "Select Localization Id";
        
        
        public override List<SearchGroup<string>> GetEntriesToDisplay()
        {
            var list = new List<SearchGroup<string>>();
            var items = new List<SearchItem<string>>();
			
            foreach (var asset in DataAccess.GetAssets<DataAssetLocalizedText>())
            {
                items.Clear();
                
                foreach (var data in asset.Data)
                {
                    items.Add(SearchItem<string>.Set(data.LocId, data.LocId));
                }
                
                list.Add(new SearchGroup<string>(asset.VariantId, items));
            }

            // list.Add(new SearchGroup<string>(items));
			
            return list;
        }
        
        
        public static SearchProviderLocalizationIds GetProvider()
        {
            if (Instance == null)
            {
                Instance = CreateInstance<SearchProviderLocalizationIds>();
            }

            return Instance;
        }
    }
}

#endif