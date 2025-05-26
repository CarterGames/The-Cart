#if CARTERGAMES_CART_MODULE_LOCALIZATION && CARTERGAMES_NOTIONDATA

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
using CarterGames.Standalone.NotionData;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CarterGames.Cart.Modules.Localization
{
    /// <summary>
    /// A NotionDatabaseParser for converting Notion Data into SpriteLocalizationData.
    /// The standard parser will not work in this workflow.
    /// </summary>
    [Serializable]
    public sealed class NotionDatabaseProcessorSpriteLocalization : NotionDatabaseProcessor
    {
        /// <summary>
        /// Parses the data when called.
        /// </summary>
        /// <param name="result">The result of the download to use.</param>
        /// <returns>The parsed data to set on the asset.</returns>
        public override List<object> Process<T>(NotionDatabaseQueryResult result)
        {
            var list = new List<object>();
            
#if UNITY_EDITOR
            foreach (var row in result.Rows)
            {
                var entries = new List<LocalizationEntry<Sprite>>();

                foreach (var k in row.DataLookup.Keys.Where(t => t != "id"))
                {
                    var valueData = row.DataLookup[k];
                    var asset = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets($"t:Sprite {valueData.RichText().Value}").First()));
                    
                    entries.Add(new LocalizationEntry<Sprite>(row.DataLookup[k].PropertyName, asset));
                }
                
                list.Add(new LocalizationData<Sprite>(row.DataLookup["id"].RichText().Value, entries));
            }
#endif
            return list;
        }
    }
}

#endif