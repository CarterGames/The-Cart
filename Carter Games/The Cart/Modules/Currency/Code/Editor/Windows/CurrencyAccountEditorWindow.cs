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
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency.Editor
{
    public class CurrencyAccountEditorWindow : UtilityEditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static readonly string ScrollPosKey = $"CarterGames_TheCart_Modules_Currency_ScrollPos";
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


        private IScriptableAssetDef<DataAssetDefaultAccounts> SettingsDef => ScriptableRef.GetAssetDef<DataAssetDefaultAccounts>();

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
                }

                GUI.backgroundColor = Color.red;

                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    DefAccountsProp.DeleteIndex(i);
                    DefAccountsProp.serializedObject.ApplyModifiedProperties();
                    DefAccountsProp.serializedObject.Update();
                    return;
                }

                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}

#endif