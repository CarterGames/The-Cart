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

using System;
using CarterGames.Cart.Data;
using UnityEditor;

namespace CarterGames.Cart.Editor
{
    public abstract class AutoMakeDataAssetDefineBase<T> : IAutoMakeDataAssetDefine<T> where T : DataAsset
    {
        private T cache;
        private SerializedObject objCache;

        public Type AssetType => typeof(T);
        
        public virtual AutoMakePathType PathType => AutoMakePathType.Data;

        public abstract string DataAssetFileName { get; }
        
        public virtual string DataAssetFilter => $"t:{AssetType.FullName} name={DataAssetFileName}";

        public virtual string DataAssetPath => DataAssetFileName;
        
        public string CompleteDataAssetPath => $"{GetBasePath()}/{DataAssetPath}";
        
        public T AssetRef => AutoMakeDataAssetManager.GetOrCreateAsset(this, ref cache);
        
        public SerializedObject ObjectRef => AutoMakeDataAssetManager.GetOrCreateAssetObject(this, ref objCache);

        public virtual void TryCreate()
        {
            AutoMakeDataAssetManager.GetOrCreateAsset(this, ref cache);
        }
        
        public virtual void OnCreated() { }

        private string GetBasePath()
        {
            return PathType switch
            {
                AutoMakePathType.Resources => AutoMakeDataAssetManager.FullPathResources,
                AutoMakePathType.Data => AutoMakeDataAssetManager.FullPathData,
                _ => string.Empty
            };
        }
    }
}