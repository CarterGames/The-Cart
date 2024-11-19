#if CARTERGAMES_CART_MODULE_HIERARCHY && UNITY_EDITOR

/*
 * Copyright (c) 2024 Carter Games
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
using CarterGames.Cart.Core;
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
        |   Constructor
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        static HierarchyEditor()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= DrawEdits;
            EditorApplication.hierarchyWindowItemOnGUI += DrawEdits;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Creates a new empty gameObject setup to be a header with no name.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Header", false, 1)] 
        private static void CreateHeader()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.HeaderPrefix);
            gameObject.tag = "EditorOnly";
            Selection.activeObject = gameObject;
        }


        /// <summary>
        /// Creates a new empty gameObject setup to be a separator.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Separator", false, 2)] 
        private static void CreateSeparator()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.SeparatorPrefix);
            gameObject.tag = "EditorOnly";
            Selection.activeObject = gameObject;
        }


        /// <summary>
        /// Creates a new empty gameObject setup to be a header with no name.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Customizable Header", false, 3)] 
        private static void CreateCustomizableHeader()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.HeaderPrefix);
            gameObject.tag = "EditorOnly";
            gameObject.AddComponent<HierarchyHeaderSettings>();
            Selection.activeObject = gameObject;
        }


        /// <summary>
        /// Creates a new empty gameObject setup to be a header with no name.
        /// </summary>
        [MenuItem("GameObject/Hierarchy/Create Customizable Separator", false, 4)] 
        private static void CreateCustomizableSeparator()
        {
            var gameObject = new GameObject(EditorSettingsHierarchy.SeparatorPrefix);
            gameObject.tag = "EditorOnly";
            gameObject.AddComponent<HierarchySeparatorSettings>();
            Selection.activeObject = gameObject;
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
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

            if (gameObject == null) return;
            if (!gameObject.CompareTag("EditorOnly")) return;

            switch (gameObject.name)
            {
                case var x when x.Contains(EditorSettingsHierarchy.HeaderPrefix):
                    DrawHeaderItemLabel(rect, gameObject, gameObject.name.Replace(EditorSettingsHierarchy.HeaderPrefix, string.Empty));
                    break;
                case var x when x.Contains(EditorSettingsHierarchy.SeparatorPrefix):
                    DrawSeparatorItemLabel(rect, gameObject);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Draws a header when called.
        /// </summary>
        /// <param name="rect">The rect to draw on.</param>
        /// <param name="gameObject">The gameObject to edit.</param>
        /// <param name="label">The label to display on the gameObject.</param>
        private static void DrawHeaderItemLabel(Rect rect, GameObject gameObject, string label)
        {
            var style = new GUIStyle();
            
            var hasSettings = gameObject.TryGetComponent<HierarchyHeaderSettings>(out var settings);
            style.normal.background = CreateColorTexture(hasSettings ? settings.BackgroundColor : EditorSettingsHierarchy.HeaderBackgroundColor);

            var textStyle = hasSettings ? settings.BoldLabel ? new GUIStyle(EditorStyles.boldLabel) : new GUIStyle() : new GUIStyle(EditorStyles.boldLabel);

            AlignText(textStyle, hasSettings ? settings.TextAlign : EditorSettingsHierarchy.TextAlign);
                
            textStyle.normal.textColor = hasSettings ? settings.LabelColor : EditorSettingsHierarchy.TextColor;

            // Adjusts the rect to fill the hierarchy blank space between the normal label field space.
            rect = AdjustRect(rect, gameObject, hasSettings ? settings.FullWidth : EditorSettingsHierarchy.FullWidth);

            if (Event.current.type == EventType.Repaint)
            {
                style.Draw(rect, false, false, false, false);
            }
            
            var iconContent = GetObjectContent(gameObject, typeof(GameObject));
            var itemContent = new GUIContent(hasSettings ? settings.Label : label, iconContent.image);
            
            EditorGUI.LabelField(rect, itemContent, textStyle);
        }


        /// <summary>
        /// Draws a separator when called.
        /// </summary>
        /// <param name="rect">The rect to draw on.</param>
        /// <param name="gameObject">The gameObject to edit.</param>
        private static void DrawSeparatorItemLabel(Rect rect, GameObject gameObject)
        {
            var style = new GUIStyle();
            var hasSettings = gameObject.TryGetComponent<HierarchySeparatorSettings>(out var settings);
            
            style.normal.background = CreateColorTexture(hasSettings 
                ? settings.BackgroundColor 
                : EditorGUIUtility.isProSkin
                    ? new Color32(46, 46, 46, 255)
                    : new Color32(184, 184, 184, 255));

            // Adjusts the rect to fill the hierarchy blank space between the normal label field space.
            rect = AdjustRect(rect, gameObject, hasSettings ? settings.FullWidth : EditorSettingsHierarchy.FullWidth);
            
            if (Event.current.type == EventType.Repaint)
            {
                style.Draw(rect, false, false, false, false);
            }
            
            var iconContent = GetObjectContent(gameObject, typeof(GameObject));
            var itemContent = new GUIContent(string.Empty, iconContent.image);

            EditorGUI.LabelField(rect, itemContent);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Utility Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Adjusts the rect based on the settings & parent depth.
        /// </summary>
        /// <param name="rect">The rect to edit.</param>
        /// <param name="gameObject">The target object.</param>
        private static Rect AdjustRect(Rect rect, GameObject gameObject, bool fullWidth)
        {
            if (gameObject.transform.parent == null)
            {
                rect.x -= 28;
                rect.width += 44;
            }
            else
            {
                if (fullWidth)
                {
                    rect.x -= 28 + (13.5f * gameObject.transform.GetParentCount());
                    rect.width += 44 + (13.5f * gameObject.transform.GetParentCount());
                }
                else
                {
                    rect.x -= 28 - (13 * gameObject.transform.GetParentCount());
                    rect.width += 44 - (13 * gameObject.transform.GetParentCount()); 
                }
            }

            return rect;
        }


        /// <summary>
        /// Aligns the text of a header when called.
        /// </summary>
        /// <param name="style">The style to edit.</param>
        /// <param name="alignment">The alignment to set to.</param>
        private static void AlignText(GUIStyle style, HierarchyTitleTextAlign alignment)
        {
            style.alignment = alignment switch
            {
                HierarchyTitleTextAlign.Center => TextAnchor.UpperCenter,
                HierarchyTitleTextAlign.Left => TextAnchor.UpperLeft,
                HierarchyTitleTextAlign.Right => TextAnchor.UpperRight,
                _ => TextAnchor.UpperCenter
            };
            
            switch (alignment)
            {
                case HierarchyTitleTextAlign.Left:
                    style.padding.left = 5;
                    break;
                case HierarchyTitleTextAlign.Right:
                    style.padding.right = 5;
                    break;
                case HierarchyTitleTextAlign.Center:
                default:
                    break;
            }
        }


        /// <summary>
        /// Creates a colour square to use as a texture without drawing loads of pixels.
        /// </summary>
        /// <param name="color">The color to make a texture of.</param>
        /// <returns>The resulting 1x1 texture.</returns>
        private static Texture2D CreateColorTexture(Color color)
        {
            var texture = new Texture2D(1, 1);

            texture.SetPixel(0, 0, color);
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.Apply();

            return texture;
        }


        /// <summary>
        /// Gets the GUIContent for the target object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="targetType">The type the target is.</param>
        /// <returns>GUIContent for the target.</returns>
        private static GUIContent GetObjectContent(Object target, Type targetType)
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
        private static bool IsDefaultObjectIcon(string name)
        {
            return name == "GameObject Icon" || name == "d_GameObject Icon";
        }


        /// <summary>
        /// Gets if the object is a prefab.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>The result.</returns>
        private static bool IsDefaultPrefabIcon(string name)
        {
            return name == "Prefab Icon" || name == "d_Prefab Icon";
        }
    }
}

#endif