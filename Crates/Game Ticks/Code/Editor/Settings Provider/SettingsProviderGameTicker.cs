#if CARTERGAMES_CART_CRATE_GAMETICKER && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.GameTicks.Editor
{
    /// <summary>
    /// Handles the settings GUI for the game ticker crate.
    /// </summary>
    public sealed class SettingsProviderGameTicker : ISettingsProvider
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public string MenuName => "Game Ticks";
        
        private IAutoMakeDataAssetDefine<DataAssetSettingsGameTicker> SettingsDef => AutoMakeDataAssetManager.GetDefine<DataAssetSettingsGameTicker>();

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
            
            // Draw the provider enum field on the GUI...
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickUseGlobalTicker"), new GUIContent("Use global ticker?", "Defines if the global ticker should be used or not."));
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("globalSyncState"), new GUIContent("Global sync state", "Defines the sync state for the global ticker."));
            
            if (SettingsDef.ObjectRef.Fp("globalSyncState").intValue == 0)
            {
                EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickTicksPerSecond"),
                    new GUIContent("Global ticks per second", "Defines the tick rate of the global ticker."));
            }
            
            EditorGUILayout.PropertyField(SettingsDef.ObjectRef.Fp("gameTickUseUnscaledTime"), new GUIContent("Use unscaled time", "Defines if the global ticker uses unscaled time."));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif