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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
    /// <summary>
    /// A base class for property drawers that can be used to select 
    /// </summary>
    /// <typeparam name="TProviderType">The provider implementation type.</typeparam>
    /// <typeparam name="TSearchType">The search result type.</typeparam>
    public abstract class PropertyDrawerSearchProviderSelectable<TProviderType, TSearchType> : PropertyDrawer where TProviderType : SearchProvider<TSearchType>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Abstract Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected abstract bool HasValue { get; }
        protected abstract TSearchType CurrentValue { get; }
        protected abstract TProviderType Provider { get; }
        protected abstract SerializedProperty EditDisplayProperty { get; }
        protected abstract string InitialSelectButtonLabel { get; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Abstract Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected abstract bool IsValid(SerializedProperty property);
        protected abstract void OnSelectionMade(TSearchType selectedEntry);
        protected abstract void ClearValue();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected SerializedProperty TargetProperty { get; private set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   GUI Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TargetProperty = property;
            
            EditorGUI.BeginProperty(position, label, property);
            
            if (IsValid(property))
            {
                // Draw field with edit button...
                DrawEditView(position, label);
            }
            else
            {
                // Draw initial value select button...
                DrawInitialView(position, label);
            }
            
            EditorGUI.EndProperty();
        }
        
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 1.25f;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void DrawInitialView(Rect position, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
                
            EditorGUI.LabelField(position, label);
            EditorGUI.EndDisabledGroup();
                
            position.width -= EditorGUIUtility.labelWidth;
            position.x += EditorGUIUtility.labelWidth;
                
            // Draw select button only...
            GUI.backgroundColor = Color.green;
            if (GUI.Button(position, InitialSelectButtonLabel))
            {
                Provider.SelectionMade.Add(OnSearchSelectionMade);
                Provider.Open(CurrentValue);
            }
            GUI.backgroundColor = Color.white;
        }


        private void DrawEditView(Rect position, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
                
            var pos = new Rect(position);
            pos.width = (pos.width / 20) * 17;
                
            EditorGUI.PropertyField(pos, EditDisplayProperty, label);
            EditorGUI.EndDisabledGroup();
            
            if (HasValue)
            {
                var buttonPos = new Rect(position);
                buttonPos.width = (position.width / 20 * 2) - 2.5f;
                buttonPos.x += ((position.width / 20) * 17) + 2.5f;
                
                GUI.backgroundColor = Color.yellow;
                if (GUI.Button(buttonPos, "Edit"))
                {
                    Provider.SelectionMade.Add(OnSearchSelectionMade);
                    Provider.Open(CurrentValue);
                }
                GUI.backgroundColor = Color.white;
           
            
                var clearPos = new Rect(position);
                clearPos.width = (position.width / 20) - 2.5f;
                clearPos.x = buttonPos.x + buttonPos.width + 2.5f;
            
                GUI.backgroundColor = Color.red;
                if (GUI.Button(clearPos, "X"))
                {
                    ClearValue();
                }
                GUI.backgroundColor = Color.white;
            }
            else
            {
                var buttonPos = new Rect(position);
                buttonPos.width = (position.width / 20 * 2) - 2.5f;
                buttonPos.x += ((position.width / 20) * 18) + 2.5f;
                
                GUI.backgroundColor = Color.yellow;
                if (GUI.Button(buttonPos, "Edit"))
                {
                    Provider.SelectionMade.Add(OnSearchSelectionMade);
                    Provider.Open(CurrentValue);
                }
                GUI.backgroundColor = Color.white;
            }
        }

        
        private void OnSearchSelectionMade(SearchTreeEntry entry)
        {
            Provider.SelectionMade.Remove(OnSearchSelectionMade);
            
            if (TargetProperty != null)
            {
                OnSelectionMade((TSearchType) entry.userData);
            }
        }
    }
}