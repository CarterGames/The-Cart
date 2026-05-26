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

using CarterGames.Cart.Events;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public class UtilityEditorWindowGenericText : UtilityEditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
        public static string CurrentValue { get; private set; }
        private static string Description { get; set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the value is changed at any time.
        /// </summary>
        public static readonly Evt<string> ValueChangedCtx = new Evt<string>();
        
        
        public static readonly Evt<string> ValueConfirmedCtx = new Evt<string>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Open Window Method
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
        public static void OpenAndAssignInfo(string windowTitle, string description, string defaultValue = "")
        {
            Description = description;

            if (!string.IsNullOrEmpty(defaultValue))
            {
                CurrentValue = defaultValue;
            }
            
            Open<UtilityEditorWindowGenericText>(windowTitle);
        }
		
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   GUI Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
        private void OnGUI()
        {
            GUILayout.Space(7.5f);
            EditorGUILayout.HelpBox(Description, MessageType.Info);
            GUILayout.Space(1.5f);
            
            EditorGUI.BeginChangeCheck();
            CurrentValue = EditorGUILayout.TextField(CurrentValue);
            if (EditorGUI.EndChangeCheck())
            {
                ValueChangedCtx.Raise(CurrentValue);
            }
        }
    }
}