#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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

using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency.Editor
{
    public class CurrencyAccountEditorWindow : UtilityEditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string ScrollPosKey = $"CarterGames_TheCart_Crates_Currency_ScrollPos";
        private double accountBalance;

        private string accountName;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Vector2 ScrollPos
        {
            get => (Vector2)PerUserSettings.GetOrCreateValue<Vector2>(ScrollPosKey, SettingType.SessionState, Vector2.zero);
            set => PerUserSettings.SetValue<Vector2>(ScrollPosKey, SettingType.SessionState, value);
        }


        private IAutoMakeDataAssetDefine<DataAssetDefaultAccounts> SettingsDef => AutoMakeDataAssetManager.GetDefine<DataAssetDefaultAccounts>();

        private SerializedProperty DefAccountsProp => SettingsDef.ObjectRef.Fp("defaultAccounts");

        private void OnGUI()
        {
            EditorGUILayout.Space(5f);
            EditorGUILayout.HelpBox("Use this popup to manage the accounts in the project that are default created if they do not exist.", MessageType.Info);
            EditorGUILayout.Space(5f);

            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos);
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Add Default Account", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            DrawAddAccountSection();
            
            EditorGUILayout.EndVertical();
            
            
            EditorGUILayout.Space(10f);
            
            
            EditorGUILayout.BeginVertical("HelpBox");
            
            EditorGUILayout.LabelField("Manage Default Accounts", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            DrawAccountsList();
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }


        public static void OpenCurrencyEditorWindow()
        {
            // ScrollPos = Vector2.zero;
            if (HasOpenInstances<CurrencyAccountEditorWindow>()) return;
            Open<CurrencyAccountEditorWindow>("Manage Accounts");
        }


        private void DrawAddAccountSection()
        {
            EditorGUILayout.BeginVertical();
            
            accountName = EditorGUILayout.TextField(accountName);
            accountBalance = EditorGUILayout.DoubleField(accountBalance);
            
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(accountName) || accountBalance < 0);
            GUI.backgroundColor = string.IsNullOrEmpty(accountName) || accountBalance < 0 ? Color.yellow : Color.green;
            
            if (GUILayout.Button("Add Account"))
            {
                TryAddAccount(accountName, accountBalance);

                accountName = string.Empty;
                accountBalance = 0;
                
                DefAccountsProp.serializedObject.ApplyModifiedProperties();
                DefAccountsProp.serializedObject.Update();
            }
            
            GUI.backgroundColor = Color.white;
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }


        private void TryAddAccount(string key, double balance)
        {
            if (string.IsNullOrEmpty(key))
            {
                CartLogger.Log<CurrencyLogs>($"[Editor/TryAddDefaultAccount]: Cannot add account with a empty id.", typeof(CurrencyAccountEditorWindow));
                return;
            }
            
            for (var i = 0; i < DefAccountsProp.arraySize; i++)
            {
                if (DefAccountsProp.GetIndex(i).Fpr("key").stringValue == key)
                {
                    CartLogger.Log<CurrencyLogs>($"[Editor/TryAddDefaultAccount]: Cannot add account of id {key} as it is already defined.", typeof(CurrencyAccountEditorWindow));
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
            for (var i = 0; i < DefAccountsProp.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(DefAccountsProp.GetIndex(i).Fpr("key"), GUIContent.none);
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(DefAccountsProp.GetIndex(i).Fpr("value"), GUIContent.none);
                if (EditorGUI.EndChangeCheck())
                {
                    DefAccountsProp.serializedObject.ApplyModifiedProperties();
                    DefAccountsProp.serializedObject.Update();
                    CurrencyManager.SaveAccounts();
                }

                GUI.backgroundColor = Color.red;

                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    DefAccountsProp.DeleteIndex(i);
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