#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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

using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management.Editor;
using CarterGames.Cart.Save;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency.Editor
{
    public class SettingsProviderCurrency : ICrateSettingsProvider
    {
        private string accountName;
        private double accountBalance;
 
        
        public string MenuName => "Currency";
        private IScriptableAssetDef<DataAssetDefaultAccounts> SettingsDef => ScriptableRef.GetAssetDef<DataAssetDefaultAccounts>();

        private SerializedProperty DefAccountsProp => SettingsDef.ObjectRef.Fp("defaultAccounts");
        

        public void OnProjectSettingsGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(1.5f);

            EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(new GUIContent("Accounts Stored"), CurrencyManager.AllAccountIds.Count.ToString());
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(15f);
            
            EditorGUILayout.LabelField("Add Default Account", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.EndHorizontal();
            
            DrawAddAccountSection();
            
            GUILayout.Space(15f);
            
            EditorGUILayout.LabelField("Manage Default Accounts", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.EndHorizontal();
            
            DrawAccountsList();
            
            GUILayout.Space(15f);
            
            GUI.backgroundColor = Color.red;
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            if (GUILayout.Button("Reset Accounts"))
            {
                Dialogue.Display("Reset Accounts", "Are you sure you want to reset all saved accounts", "Reset",
                    "Cancel",
                    () =>
                    {
                        CartSaveHandler.Set("CartSave_Crates_Currency_Accounts", string.Empty);
                    });
            }
            EditorGUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }
        
        
        private void DrawAddAccountSection()
        {
            accountName = EditorGUILayout.TextField(new GUIContent("Account Name"), accountName);
            accountBalance = EditorGUILayout.DoubleField(new GUIContent("Starting Balance"), accountBalance);
            
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(accountName) || accountBalance < 0);
            GUI.backgroundColor = string.IsNullOrEmpty(accountName) || accountBalance < 0 ? Color.yellow : Color.green;
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(17.5f);
            
            if (GUILayout.Button("Add Account"))
            {
                TryAddAccount(accountName, accountBalance);

                accountName = string.Empty;
                accountBalance = 0;
                
                DefAccountsProp.serializedObject.ApplyModifiedProperties();
                DefAccountsProp.serializedObject.Update();
            }
            
            EditorGUILayout.EndHorizontal();
            
            GUI.backgroundColor = Color.white;
            EditorGUI.EndDisabledGroup();
        }
        
        
        private void TryAddAccount(string key, double balance)
        {
            if (string.IsNullOrEmpty(key))
            {
                CartLogger.Log<LogCategoryCurrency>($"[Editor/TryAddDefaultAccount]: Cannot add account with a empty id.", typeof(CurrencyAccountEditorWindow));
                return;
            }
            
            for (var i = 0; i < DefAccountsProp.arraySize; i++)
            {
                if (DefAccountsProp.GetIndex(i).Fpr("key").stringValue == key)
                {
                    CartLogger.Log<LogCategoryCurrency>($"[Editor/TryAddDefaultAccount]: Cannot add account of id {key} as it is already defined.", typeof(CurrencyAccountEditorWindow));
                    return;
                }
            }
            
            DefAccountsProp.InsertIndex(DefAccountsProp.arraySize);
            DefAccountsProp.GetIndex(DefAccountsProp.arraySize - 1).Fpr("key").stringValue = key;
            DefAccountsProp.GetIndex(DefAccountsProp.arraySize - 1).Fpr("value").doubleValue = balance;
                
            DefAccountsProp.serializedObject.ApplyModifiedProperties();
            DefAccountsProp.serializedObject.Update();
            
            CurrencyManager.SaveAccounts();
            
            GUI.FocusControl(null);
        }


        private void DrawAccountsList()
        {
            if (DefAccountsProp.arraySize <= 0)
            {
                EditorGUILayout.HelpBox("No default accounts defined. Add one to see it here", MessageType.Info);
                return;
            }
            
            for (var i = 0; i < DefAccountsProp.arraySize + 1; i++)
            {
                if (i == 0)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Account Name");
                    EditorGUILayout.LabelField("Starting Balance");
                    EditorGUILayout.LabelField("", GUILayout.Width(25));
                    EditorGUILayout.EndHorizontal();
                    continue;
                }

                var index = i - 1;
                
                EditorGUILayout.BeginHorizontal();
                
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(DefAccountsProp.GetIndex(index).Fpr("key"), GUIContent.none);
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(DefAccountsProp.GetIndex(index).Fpr("value"), GUIContent.none);
                if (EditorGUI.EndChangeCheck())
                {
                    DefAccountsProp.serializedObject.ApplyModifiedProperties();
                    DefAccountsProp.serializedObject.Update();
                    CurrencyManager.SaveAccounts();
                }

                GUI.backgroundColor = Color.red;

                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    DefAccountsProp.DeleteIndex(index);
                    DefAccountsProp.serializedObject.ApplyModifiedProperties();
                    DefAccountsProp.serializedObject.Update();
                    CurrencyManager.SaveAccounts();
                    return;
                }

                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}

#endif