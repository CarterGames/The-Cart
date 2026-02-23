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
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Logs.Editor.Assets
{
    public class AutoMakeDefineLogCategories : AutoMakeDataAssetDefineBase<DataAssetLogCategories>
    {
        public override string DataAssetFileName => "[Cart] Logging Asset.asset";

        
        public override void OnCreated()
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