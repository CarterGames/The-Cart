#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.Linq;

#if CARTERGAMES_NOTIONDATA
using CarterGames.Shared.NotionData;
#endif

using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    public class SearchProviderLocalizationIds : SearchProvider<string>
    {
        private static SearchProviderLocalizationIds Instance;
        
        protected override string ProviderTitle => "Select Localization Id";

        public override bool HasOptions
        {
            get
            {
                if (DataAccess.GetAssets<DataAssetLocalizedText>()?.Count > 0) return true;
                if (DataAccess.GetAssets<DataAssetLocalizedSprite>()?.Count > 0) return true;
                if (DataAccess.GetAssets<DataAssetLocalizedAudio>()?.Count > 0) return true;
                
#if CARTERGAMES_NOTIONDATA
                if (NotionDataAccessor.GetAssets<NotionDataAssetLocalizedText>()?.Count > 0) return true;
                if (NotionDataAccessor.GetAssets<NotionDataAssetLocalizedSprite>()?.Count > 0) return true;
                if (NotionDataAccessor.GetAssets<NotionDataAssetLocalizedAudio>()?.Count > 0) return true;
#endif

                return false;
            }
        }


        public override List<SearchGroup<string>> GetEntriesToDisplay()
        {
            var list = new List<SearchGroup<string>>();

            if (DataAccess.GetAssets<DataAssetLocalizedText>()?.Count > 0)
            {
                foreach (var asset in DataAccess.GetAssets<DataAssetLocalizedText>())
                {
                    list.Add(GetGroupFromData(asset.VariantId, asset.Data.Select(t => t.LocId)));
                }
            }
            
            if (DataAccess.GetAssets<DataAssetLocalizedSprite>()?.Count > 0)
            {
                foreach (var asset in DataAccess.GetAssets<DataAssetLocalizedSprite>())
                {
                    list.Add(GetGroupFromData(asset.VariantId, asset.Data.Select(t => t.LocId)));
                }
            }
            
            if (DataAccess.GetAssets<DataAssetLocalizedAudio>()?.Count > 0)
            {
                foreach (var asset in DataAccess.GetAssets<DataAssetLocalizedAudio>())
                {
                    list.Add(GetGroupFromData(asset.VariantId, asset.Data.Select(t => t.LocId)));
                }
            }
            
#if CARTERGAMES_NOTIONDATA
            if (NotionDataAccessor.GetAssets<NotionDataAssetLocalizedText>()?.Count > 0)
            {
                foreach (var asset in NotionDataAccessor.GetAssets<NotionDataAssetLocalizedText>())
                {
                    list.Add(GetGroupFromData(asset.VariantId, asset.Data.Select(t => t.LocId)));
                }
            }
            
            if (NotionDataAccessor.GetAssets<NotionDataAssetLocalizedSprite>()?.Count > 0)
            {
                foreach (var asset in NotionDataAccessor.GetAssets<NotionDataAssetLocalizedSprite>())
                {
                    list.Add(GetGroupFromData(asset.VariantId, asset.Data.Select(t => t.LocId)));
                }
            }
            
            if (NotionDataAccessor.GetAssets<NotionDataAssetLocalizedAudio>()?.Count > 0)
            {
                foreach (var asset in NotionDataAccessor.GetAssets<NotionDataAssetLocalizedAudio>())
                {
                    list.Add(GetGroupFromData(asset.VariantId, asset.Data.Select(t => t.LocId)));
                }
            }
#endif
            
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
        

        private SearchGroup<string> GetGroupFromData(string id, IEnumerable<string> values)
        {
            var items = new List<SearchItem<string>>();
            
            foreach (var value in values)
            {
                items.Add(SearchItem<string>.Set(value, value));
            }
                
            return new SearchGroup<string>(id, items);
        }
    }
}

#endif