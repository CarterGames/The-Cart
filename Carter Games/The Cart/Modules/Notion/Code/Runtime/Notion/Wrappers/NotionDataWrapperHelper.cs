#if CARTERGAMES_CART_MODULE_NOTIONDATA

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

using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Modules.NotionData
{
    public static class NotionDataWrapperHelper
    {
        public static void AssignAsObject<T>(string id, ref T value) where T : Object
        {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(id))
            {
                var asset = UnityEditor.AssetDatabase.FindAssets(id);
                
                if (asset.Length > 0)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(asset[0]);
                    value = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                    return;
                }

                CartLogger.LogWarning<LogCategoryModules>($"Unable to find a reference with the name {id}");
            }
            else
            {
                CartLogger.LogWarning<LogCategoryModules>("Unable to assign a reference, the id was empty.");
            }
#endif
        }
    }
}

#endif