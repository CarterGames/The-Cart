#if CARTERGAMES_CART_MODULE_CURRENCY && UNITY_EDITOR

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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Core.Save;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency.Editor
{
    public class SettingsProviderCurrency : ISettingsProvider
    {
        private static readonly string FoldoutKey = $"{PerUserSettings.UniqueId}_Modules_Currency_Foldout";


        private static bool IsExpanded
        {
            get => PerUserSettings.GetValue<bool>(FoldoutKey, SettingType.EditorPref);
            set => PerUserSettings.SetValue<bool>(FoldoutKey, SettingType.EditorPref, value);
        }


        public void OnInspectorSettingsGUI()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Currency", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(new GUIContent("Accounts Stored:"), CurrencyManager.AllAccountIds.Count.ToString());
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndVertical();
        }


        public void OnProjectSettingsGUI()
        {
            IsExpanded = EditorGUILayout.Foldout(IsExpanded, "Currency");
            
            if (!IsExpanded) return;
            
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(1.5f);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(new GUIContent("Accounts Stored:"), CurrencyManager.AllAccountIds.Count.ToString());
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("Manage Default Accounts", GUILayout.Width(175)))
            {
                CurrencyAccountEditorWindow.OpenCurrencyEditorWindow();
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(2.5f);
            
            GUI.backgroundColor = Color.red;
            
            if (GUILayout.Button("Reset Accounts"))
            {
                Dialogue.Display("Reset Accounts", "Are you sure you want to reset all saved accounts", "Reset",
                    "Cancel",
                    () =>
                    {
                        CartSaveHandler.Set("CartSave_Modules_Currency_Accounts", string.Empty);
                    });
            }

            GUI.backgroundColor = Color.white;
            
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
    }
}

#endif