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

using System;
using System.Linq;
using CarterGames.Cart;
using CarterGames.Cart.Management;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Crates.Hierarchy.Editor
{
    /// <summary>
    /// Handles the editing of some gameObjects in the hierarchy to display as headers/separators.
    /// </summary>
    [InitializeOnLoad]
    public static class HierarchyEditor
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static IHierarchyEdit[] cacheEdits;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static IHierarchyEdit[] Edits
        {
            get
            {
                if (!cacheEdits.IsEmptyOrNull()) return cacheEdits;
                cacheEdits = AssemblyHelper.GetClassesOfType<IHierarchyEdit>().OrderBy(t => t.Order).ToArray();
                return cacheEdits;
            }
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        static HierarchyEditor()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= DrawEdits;
            EditorApplication.hierarchyWindowItemOnGUI += DrawEdits;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Draws the header/separator based on the gameObject name & tag.
        /// </summary>
        /// <param name="instanceId">The instance id passed in.</param>
        /// <param name="rect">The rect passed in.</param>
        private static void DrawEdits(int instanceId, Rect rect)
        {
            foreach (var edit in Edits)
            {
                if (!edit.IsEnabled) continue;
                edit.OnHierarchyDraw(instanceId, rect);
            }
        }
        
        
        /// <summary>
        /// Gets the GUIContent for the target object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="targetType">The type the target is.</param>
        /// <returns>GUIContent for the target.</returns>
        public static GUIContent GetObjectContent(Object target, Type targetType)
        {
            var content = EditorGUIUtility.ObjectContent(target, targetType);
            
            if (content.image)
            {
                content.image = IsDefaultObjectIcon(content.image.name) ||
                                IsDefaultPrefabIcon(content.image.name) ? null : content.image;
            }

            return content;
        }


        /// <summary>
        /// Gets if the object is a gameObject.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>The result.</returns>
        public static bool IsDefaultObjectIcon(string name)
        {
            return name == "GameObject Icon" || name == "d_GameObject Icon";
        }


        /// <summary>
        /// Gets if the object is a prefab.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>The result.</returns>
        public static bool IsDefaultPrefabIcon(string name)
        {
            return name == "Prefab Icon" || name == "d_Prefab Icon";
        }
    }
}

#endif