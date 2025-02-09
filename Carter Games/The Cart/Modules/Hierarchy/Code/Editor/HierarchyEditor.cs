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

using System;
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Management;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
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