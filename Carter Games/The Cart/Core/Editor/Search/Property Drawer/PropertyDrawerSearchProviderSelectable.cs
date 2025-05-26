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

using System.Collections.Generic;
using UnityEditor;
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
        
        protected abstract TProviderType Provider { get; }
        protected abstract string InitialSelectButtonLabel { get; }
        protected virtual bool DisableInputWhenSelected { get; } = true;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Abstract Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected abstract bool IsValid(SerializedProperty property);
        protected abstract bool GetHasValue(SerializedProperty property);
        protected abstract TSearchType GetCurrentValue(SerializedProperty property);
        protected abstract string GetCurrentValueString(SerializedProperty property);
        protected abstract void OnSelectionMade(SerializedProperty property, TSearchType selectedEntry);
        protected abstract void ClearValue(SerializedProperty property);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   GUI Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected bool IsValidFieldType => fieldInfo.FieldType == typeof(TSearchType);
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (IsValidFieldType)
            {
                if (IsValid(property))
                {
                    // Draw field with edit button...
                    DrawEditView(position, property, label);
                }
                else if (!IsCurrentValueValid(property))
                {
                    DrawInitialView(position, property, label);
                }
                else
                {
                    // Draw initial value select button...
                    DrawInitialView(position, property, label);
                }
            }
            else
            {
                DrawInvalidView(position, property, label, "Invalid field type for attribute.");
            }

            EditorGUI.EndProperty();
        }
        
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 1.25f;
        }
        
        
        protected bool IsCurrentValueValid(SerializedProperty property)
        {
            if (Provider.GetValidValues() != null)
            {
                return Provider.GetValidValues().Contains(GetCurrentValue(property));
            }

            return true;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void DrawInvalidView(Rect position, SerializedProperty property, GUIContent label, string message)
        {
            GUI.color = Color.yellow;
            EditorGUI.LabelField(position, label);
                
            position.width -= EditorGUIUtility.labelWidth;
            position.x += EditorGUIUtility.labelWidth;
            
            EditorGUI.LabelField(position, message);
            GUI.color = Color.white;
        }
        
        
        private void DrawInitialView(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, label);
                
            position.width -= EditorGUIUtility.labelWidth;
            position.x += EditorGUIUtility.labelWidth;
                
            // Draw select button only...

            if (Provider.HasOptions)
            {
                GUI.backgroundColor = Color.green;
                if (GUI.Button(position, InitialSelectButtonLabel))
                {
                    Provider.SelectionMade.Clear();
                    Provider.SelectionMade.Add((ste) =>
                    {
                        Provider.SelectionMade.Clear();
                        OnSelectionMade(property, (TSearchType) ste.userData);
                    });
                
                    Provider.Open(GetCurrentValue(property));
                }
                GUI.backgroundColor = Color.white;
            }
            else
            {
                GUI.backgroundColor = Color.gray;
                EditorGUI.BeginDisabledGroup(true);
                if (GUI.Button(position, "No options available")) { }
                EditorGUI.EndDisabledGroup();
                GUI.backgroundColor = Color.white;
            }
        }


        private void DrawEditView(Rect position, SerializedProperty property, GUIContent label)
        {
            var pos = new Rect(position);
            pos.width = position.width - 57.5f;
            
            EditorGUI.BeginDisabledGroup(DisableInputWhenSelected);
            EditorGUI.TextField(pos, label, GetCurrentValueString(property));
            EditorGUI.EndDisabledGroup();
            
            if (GetHasValue(property))
            {
                var buttonPos = new Rect(position);
                buttonPos.width = 35f;
                buttonPos.x += (position.width - 55f);
                
                GUI.backgroundColor = Color.yellow;
                if (GUI.Button(buttonPos, "Edit"))
                {
                    Provider.SelectionMade.Clear();
                    Provider.SelectionMade.Add((ste) =>
                    {
                        Provider.SelectionMade.Clear();
                        OnSelectionMade(property, (TSearchType) ste.userData);
                    });
                    Provider.Open(GetCurrentValue(property));
                }
                GUI.backgroundColor = Color.white;
           
            
                var clearPos = new Rect(position);
                clearPos.width = 17.5f;
                clearPos.x = buttonPos.x + buttonPos.width + 2.5f;
            
                GUI.backgroundColor = Color.red;
                if (GUI.Button(clearPos, GeneralUtilEditor.CrossIcon))
                {
                    Provider.SelectionMade.Clear();
                    ClearValue(property);
                }
                GUI.backgroundColor = Color.white;
            }
            else
            {
                var buttonPos = new Rect(position);
                buttonPos.width = 35f;
                buttonPos.x += (position.width - 55f);
                
                GUI.backgroundColor = Color.yellow;
                if (GUI.Button(buttonPos, "Edit"))
                {
                    Provider.SelectionMade.Clear();
                    Provider.SelectionMade.Add((ste) =>
                    {
                        Provider.SelectionMade.Clear();
                        OnSelectionMade(property, (TSearchType) ste.userData);
                    });
                    Provider.Open(GetCurrentValue(property));
                }
                GUI.backgroundColor = Color.white;
            }
        }
    }
}