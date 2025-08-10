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
using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;

namespace CarterGames.Cart.Core.Logs.Editor.Assets
{
    public class ScriptableAssetDefLogCategories : IScriptableAssetDef<DataAssetLogCategories>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static DataAssetLogCategories cache;
        private static SerializedObject objCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   IScriptableAssetDef Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public Type AssetType => typeof(DataAssetLogCategories);
        public string DataAssetFileName => "[Cart] Logging Asset.asset";
        public string DataAssetFilter => $"t:{typeof(DataAssetLogCategories).FullName}";
        public string DataAssetPath => $"{ScriptableRef.FullPathData}/{DataAssetFileName}";


        public DataAssetLogCategories AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
        public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);


        public void TryCreate()
        {
            ScriptableRef.GetOrCreateAsset(this, ref cache);
        }

        public void OnCreated()
        {
            var categories = AssemblyHelper
                .GetClassesOfType<LogCategory>(false)
                .OrderBy(t => t.GetType().FullName);

            var internalCategories = AssemblyHelper.GetClassesOfType<LogCategory>().OrderBy(t => t.GetType().Name).Select(t => t.GetType().FullName).ToList();

            var lookup = new List<string>();
            var currentSetup = SerializableDictionary<string, bool>.FromDictionary(AssetRef.Categories);
            
            ObjectRef.Fp("cartCategories").ClearArray();
            ObjectRef.Fp("categories").Fpr("list").ClearArray();
            
            foreach (var category in categories)
            {
                if (lookup.Contains(category.GetType().FullName)) continue;
                lookup.Add(category.GetType().FullName);

                if (internalCategories.Contains(category.GetType().FullName))
                {
                    ObjectRef.Fp("cartCategories").InsertAtEnd();
                    ObjectRef.Fp("cartCategories").GetLastIndex().stringValue = category.GetType().FullName;
                }
                
                ObjectRef.Fp("categories").Fpr("list").InsertAtEnd();
                ObjectRef.Fp("categories").Fpr("list").GetLastIndex().Fpr("key").stringValue = category.GetType().FullName;
                ObjectRef.Fp("categories").Fpr("list").GetLastIndex().Fpr("value").boolValue =
                    currentSetup.ContainsKey(category.GetType().FullName)
                        ? currentSetup[category.GetType().FullName]
                        : false;
            }

            ObjectRef.ApplyModifiedProperties();
            ObjectRef.Update();
        }
    }
}