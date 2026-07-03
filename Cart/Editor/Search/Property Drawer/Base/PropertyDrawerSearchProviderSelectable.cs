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

using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
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
        
        protected SearchProvider<TSearchType> ProviderCache { get; set; }
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
        protected bool IsValidFieldType => fieldInfo.FieldType.AssemblyQualifiedName.Contains(typeof(TSearchType).Name);


        protected virtual string InitialSelectButtonLabel => GetProvider().ProviderTitle;
        

        private SearchProvider<TSearchType> GetProvider()
        {
            if (ProviderCache != null) return ProviderCache;
            var attribute = fieldInfo.GetCustomAttributes(true).FirstOrDefault(t => t.GetType().BaseType == typeof(SearchAttribute));
            
            if (attribute == null) return null;
            
            var searchType = attribute.GetType().BaseType.GetField("searchProviderType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(attribute);

            ProviderCache = AssemblyHelper.GetClassesOfType<SearchProvider<TSearchType>>().FirstOrDefault(t => t.GetType() == searchType);
            return ProviderCache;
        }
        
        
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
            if (GetProvider().GetValidValues() != null)
            {
                return GetProvider().GetValidValues().Contains(GetCurrentValue(property));
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

            if (GetProvider().HasOptions)
            {
                GUI.backgroundColor = Color.green;
                if (GUI.Button(position, InitialSelectButtonLabel))
                {
                    GetProvider().SelectionMade.Clear();
                    GetProvider().SelectionMade.Add((ste) =>
                    {
                        GetProvider().SelectionMade.Clear();
                        OnSelectionMade(property, (TSearchType) ste.userData);
                    });
                
                    GetProvider().Open(GetCurrentValue(property));
                    GUIUtility.ExitGUI();
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
            pos.width = position.width - 61.5f;
            
            EditorGUI.BeginDisabledGroup(DisableInputWhenSelected);
            EditorGUI.TextField(pos, label, GetCurrentValueString(property));
            EditorGUI.EndDisabledGroup();
            
            if (GetHasValue(property))
            {
                var buttonPos = new Rect(position);
                buttonPos.width = 27.5f;
                buttonPos.x += (position.width - 57.5f);
                
                GUI.backgroundColor = Color.yellow;
                if (GUI.Button(buttonPos, EditorGUIUtility.IconContent("d__Menu@2x")))
                {
                    GetProvider().SelectionMade.Clear();
                    GetProvider().SelectionMade.Add((ste) =>
                    {
                        GetProvider().SelectionMade.Clear();
                        OnSelectionMade(property, (TSearchType) ste.userData);
                    });
                    GetProvider().Open(GetCurrentValue(property));
                    GUIUtility.ExitGUI();
                }
                GUI.backgroundColor = Color.white;
           
            
                var clearPos = new Rect(position);
                clearPos.width = 27.5f;
                clearPos.x = buttonPos.x + buttonPos.width + 2.5f;
            
                GUI.backgroundColor = Color.red;
                if (GUI.Button(clearPos, EditorGUIUtility.IconContent("CrossIcon")))
                {
                    GetProvider().SelectionMade.Clear();
                    ClearValue(property);
                    GUIUtility.ExitGUI();
                }
                GUI.backgroundColor = Color.white;
            }
            else
            {
                var buttonPos = new Rect(position);
                buttonPos.width = 27.5f;
                buttonPos.x += (position.width - 61.5f);
                
                GUI.backgroundColor = Color.yellow;
                if (GUI.Button(buttonPos, EditorGUIUtility.IconContent("d__Menu@2x")))
                {
                    GetProvider().SelectionMade.Clear();
                    GetProvider().SelectionMade.Add((ste) =>
                    {
                        GetProvider().SelectionMade.Clear();
                        OnSelectionMade(property, (TSearchType) ste.userData);
                    });
                    GetProvider().Open(GetCurrentValue(property));
                    GUIUtility.ExitGUI();
                }
                GUI.backgroundColor = Color.white;
            }
        }
    }
}