#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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
using UnityEditor;

namespace CarterGames.Cart.Crates.Currency.Editor
{
    public static class DisplayStyleEditor
    {
        public static void DrawStyleSettings(SerializedProperty targetProperty)
        {
            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.PropertyField(targetProperty.Fpr("displayStyle"));
            
            switch (targetProperty.Fpr("displayStyle").intValue)
            {
                case 1:
                    EditorGUILayout.PropertyField(targetProperty.Fpr("instantDelay"));
                    break;
                case 2:
                    EditorGUILayout.PropertyField(targetProperty.Fpr("trickleDuration"));
                    break;
                case 0:
                default:
                    break;
            }
            
            EditorGUILayout.EndVertical();
        }
    }
}

#endif