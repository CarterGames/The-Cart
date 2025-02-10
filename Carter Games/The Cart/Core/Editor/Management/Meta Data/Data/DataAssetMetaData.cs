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

using System;
using CarterGames.Cart.Core.Data;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.MetaData.Editor
{
    [Serializable]
    public class DataAssetMetaData : DataAsset
    {
        [SerializeField] private SerializableDictionary<string, TextAsset> data;


        private SerializableDictionary<string, TextAsset> GetAssets()
        {
            var lookup = new SerializableDictionary<string, TextAsset>();
            
            foreach (var defPath in InterfaceHelper.GetAllInterfacesInstancesOfType<IMetaDefinition>())
            {
                var assets = AssetDatabase.FindAssets("t:textasset", new[] { defPath.MetaPath });
                
                if (assets.Length <= 0) continue;

                foreach (var asset in assets)
                {
                    var path = AssetDatabase.GUIDToAssetPath(asset);
                    
                    if (!path.Contains(".json")) continue;
                    
                    var actualFile = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
                    
                    lookup.Add(actualFile.name, (TextAsset) actualFile);
                }
            }

            return lookup;
        }


        public MetaData GetData(string id)
        {
            return GetDataUpdateIfNotPresent(id);
        }
        
        
        public bool TryGetData(string id, out MetaData asset)
        {
            asset = GetData(id);
            return !asset.IsMissingOrNull();
        }


        private MetaData GetDataUpdateIfNotPresent(string id)
        {
            TextAsset value;
            
            if (!data.IsEmptyOrNull())
            {
                if (data.TryGetValue(id, out value))
                {
                    return JsonUtility.FromJson<MetaData>(value.text);
                }

                data = GetAssets();

                if (data.TryGetValue(id, out value))
                {
                    return JsonUtility.FromJson<MetaData>(value.text);
                }
            }

            data = GetAssets();

            if (data.TryGetValue(id, out value))
            {
                return JsonUtility.FromJson<MetaData>(value.text);
            }

            return null;
        }
    }
}