#if CARTERGAMES_CART_MODULE_LOCALIZATION && CARTERGAMES_CART_MODULE_HIERARCHY && UNITY_EDITOR

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

using CarterGames.Cart.Modules.Hierarchy.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    public class HierarchyEditLocalizedIcon : IHierarchyEdit
    {
        public bool IsEnabled => true;

        public int Order => 0;
        
        
        public void OnHierarchyDraw(int instanceId, Rect rect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

            if (gameObject == null) return;
            
            rect.width -= 17.5f;
            rect.x += rect.width;
            
            if (gameObject.TryGetComponent(out LocalizedTextMeshPro component))
            {
                EditorGUI.LabelField(rect, GetContentForComponent(component));
            }
            
            if (gameObject.TryGetComponent(out LocalizedAudioSource audioComponent))
            {
                EditorGUI.LabelField(rect, GetContentForComponent(audioComponent));
            }
            
            if (gameObject.TryGetComponent(out LocalizedSpriteRenderer spriteComponent))
            {
                EditorGUI.LabelField(rect, GetContentForComponent(spriteComponent));
            }
            
            if (gameObject.TryGetComponent(out LocalizedUIImage imageComponent))
            {
                EditorGUI.LabelField(rect, GetContentForComponent(imageComponent));
            }
        }


        private GUIContent GetContentForComponent(LocalizedTextMeshPro component)
        {
            if (string.IsNullOrEmpty(component.LocId))
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabChangesDeleted Icon").image,
                    "Text localized but doesn't have a pre-defined loc id assigned.");
            }
            else
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabEdit Icon").image,
                    $"Text localized with loc id: {component.LocId}.");
            }
        }
        
        
        private GUIContent GetContentForComponent(LocalizedAudioSource component)
        {
            if (string.IsNullOrEmpty(component.LocId))
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabChangesDeleted Icon").image,
                    "Audio localized but doesn't have a pre-defined loc id assigned.");
            }
            else
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabEdit Icon").image,
                    $"Audio localized with loc id: {component.LocId}.");
            }
        }
        
        
        private GUIContent GetContentForComponent(LocalizedSpriteRenderer component)
        {
            if (string.IsNullOrEmpty(component.LocId))
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabChangesDeleted Icon").image,
                    "Sprite localized but doesn't have a pre-defined loc id assigned.");
            }
            else
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabEdit Icon").image,
                    $"Sprite localized with loc id: {component.LocId}.");
            }
        }
        
        
        private GUIContent GetContentForComponent(LocalizedUIImage component)
        {
            if (string.IsNullOrEmpty(component.LocId))
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabChangesDeleted Icon").image,
                    "Image localized but doesn't have a pre-defined loc id assigned.");
            }
            else
            {
                return new GUIContent(EditorGUIUtility.IconContent("CollabEdit Icon").image,
                    $"Image localized with loc id: {component.LocId}.");
            }
        }
    }
}

#endif