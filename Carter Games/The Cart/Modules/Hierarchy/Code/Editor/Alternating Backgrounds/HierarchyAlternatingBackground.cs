#if CARTERGAMES_CART_MODULE_HIERARCHY && UNITY_EDITOR

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

using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public class HierarchyAlternatingBackground : IHierarchyEdit
    {
        private static Color32 AltColor => EditorGUIUtility.isProSkin
            ? new Color32(36, 36, 36, 45)
            : new Color32(174, 174, 174, 45);


        public bool IsEnabled => ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().ObjectRef
            .Fp("alternateLinesConfig").Fpr("isEnabled").boolValue;
        public int Order => -1;
        
        
        public void OnHierarchyDraw(int instanceId, Rect rect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            
            if (gameObject == null) return;
            
            if (Selection.gameObjects.Contains(gameObject)) return;

            if (ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.StartAlternate)
            {
                if (Mathf.RoundToInt(rect.y) % 32 == 0) return;
            }
            else
            {
                if (Mathf.RoundToInt(rect.y) % 32 != 0) return;
            }
            
            var bgRect = AdjustRect(rect, gameObject, ScriptableRef.GetAssetDef<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.FullWidth);
            EditorGUI.DrawRect(bgRect, AltColor);
        }
        
        
        private static Rect AdjustRect(Rect rect, GameObject gameObject, bool fullWidth)
        {
            if (gameObject.transform.parent == null)
            {
                if (!fullWidth) return rect;
                
                rect.x -= 29;
                rect.width += 46;

                return rect;

            }
            else
            {
                if (!fullWidth) return rect;
                
                rect.x -= 29 + (14f * gameObject.transform.GetParentCount());
                rect.width += 46 + (14f * gameObject.transform.GetParentCount());
            }

            return rect;
        }
    }
}

#endif