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

#if CARTERGAMES_CART_CRATE_NOTIONDATA && UNITY_EDITOR

using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.NotionData.Editor
{
    public class NotionDataCrateSettingsProvider : ISettingsProvider
    {
        public string MenuName => "Notion Data";
        
        
        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.HelpBox("Notion Data's settings are in their own tab. Use the button below to open them here.", MessageType.Info);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            
            if (GUILayout.Button("Open Settings"))
            {
                SettingsService.OpenProjectSettings("Carter Games/Assets/Notion Data");
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif