#if CARTERGAMES_CART_CRATE_GAMETICKER && UNITY_EDITOR

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