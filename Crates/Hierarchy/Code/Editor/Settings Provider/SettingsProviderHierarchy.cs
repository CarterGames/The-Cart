#if CARTERGAMES_CART_CRATE_HIERARCHYDECORATORS && UNITY_EDITOR

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

using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Hierarchy.Editor
{
    /// <summary>
    /// Handles the settings drawing for the hierarchy block system.
    /// </summary>
    public sealed class SettingsProviderHierarchy : ISettingsProvider
    {
        public string MenuName => "Hierarchy Decorators";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISettingsProvider Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the settings provider version of the settings.
        /// </summary>
        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);
            
            AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().AssetRef.HeaderSeparatorConfig.DrawConfig(AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().ObjectRef.Fp("headerSeparatorConfig"));
            EditorGUILayout.Space(15f);
            AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().AssetRef.AlternateLinesConfig.DrawConfig(AutoMakeDataAssetManager.GetDefine<DataAssetHierarchySettings>().ObjectRef.Fp("alternateLinesConfig"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif