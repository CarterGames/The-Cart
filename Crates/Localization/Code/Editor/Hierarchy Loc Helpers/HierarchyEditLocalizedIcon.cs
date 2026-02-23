#if CARTERGAMES_CART_CRATE_LOCALIZATION && CARTERGAMES_CART_CRATE_HIERARCHY && UNITY_EDITOR

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

using CarterGames.Cart.Crates.Hierarchy.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
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