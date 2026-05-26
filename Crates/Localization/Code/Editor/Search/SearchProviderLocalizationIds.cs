#if CARTERGAMES_CART_CRATE_LOCALIZATION && UNITY_EDITOR

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

#if CARTERGAMES_NOTIONDATA
using CarterGames.Shared.NotionData;
#endif

using CarterGames.Cart.Data;
using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates.Localization.Editor
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