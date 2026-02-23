#if CARTERGAMES_CART_CRATE_HIERARCHY && UNITY_EDITOR

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

using CarterGames.Cart;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Hierarchy.Editor
{
    public class HierarchyAlternatingBackground : IHierarchyEdit
    {
        private static Color32 AltColor => EditorGUIUtility.isProSkin
            ? new Color32(36, 36, 36, 45)
            : new Color32(174, 174, 174, 45);


        public bool IsEnabled => AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().ObjectRef
            .Fp("alternateLinesConfig").Fpr("isEnabled").boolValue;
        public int Order => -1;
        
        
        public void OnHierarchyDraw(int instanceId, Rect rect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            
            if (gameObject == null) return;
            
            if (Selection.gameObjects.Contains(gameObject)) return;

            if (AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.StartAlternate)
            {
                if (Mathf.RoundToInt(rect.y) % 32 == 0) return;
            }
            else
            {
                if (Mathf.RoundToInt(rect.y) % 32 != 0) return;
            }
            
            var bgRect = AdjustRect(rect, gameObject, AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.FullWidth);
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