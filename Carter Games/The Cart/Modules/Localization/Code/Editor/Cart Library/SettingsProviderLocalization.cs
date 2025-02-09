#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

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

using System.Linq;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization.Editor
{
	public sealed class SettingsProviderLocalization : ISettingsProvider
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static readonly string ExpandedId  = $"{PerUserSettings.UniqueId}_CarterGames_TheCart_Module_Localization_IsExpanded";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Should the data notion section be shown?
		/// </summary>
		public static bool IsExpanded
		{
			get => (bool)PerUserSettings.GetOrCreateValue<bool>(ExpandedId, SettingType.EditorPref);
			set => PerUserSettings.SetValue<bool>(ExpandedId, SettingType.EditorPref, value);
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   ISettingsProvider Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		public void OnInspectorSettingsGUI()
		{
			EditorGUILayout.BeginVertical("HelpBox");
            
			EditorGUILayout.LabelField("Localization", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();
            
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.TextField(new GUIContent("Language"), LocalizationManager.CurrentLanguage.DisplayName);
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.EndVertical();
		}


		public void OnProjectSettingsGUI()
		{
			IsExpanded = EditorGUILayout.Foldout(IsExpanded, "Localization");


			if (!IsExpanded) return;


			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.Space(1.5f);
			EditorGUI.indentLevel++;
			
			EditorGUILayout.BeginHorizontal();
			
			GUILayout.Space(15f);
			
			if (GUILayout.Button("Manage Languages"))
			{
				LanguageEditorPopup.ShowLanguagesEditorWindow();
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();
			
			if (string.IsNullOrEmpty(LocalizationManager.CurrentLanguage.DisplayName))
			{
				GUILayout.Space(15f);
				
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Select Language"))
				{
					var current = DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.FirstOrDefault(t =>
						t.DisplayName.Equals(LocalizationManager.CurrentLanguage.DisplayName));

					SearchProviderLanguages.GetProvider().SelectionMade.Add(OnSelectionMade);
					SearchProviderLanguages.GetProvider().Open(current);
				}
				GUI.backgroundColor = Color.white;
			}
			else
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.TextField(new GUIContent("Language"), LocalizationManager.CurrentLanguage.DisplayName);
				EditorGUI.EndDisabledGroup();
				
				GUI.backgroundColor = Color.yellow;
				if (GUILayout.Button("Edit", GUILayout.Width(55f)))
				{
					var current = DataAccess.GetAsset<DataAssetDefinedLanguages>().Languages.FirstOrDefault(t =>
						t.DisplayName.Equals(LocalizationManager.CurrentLanguage.DisplayName));

					SearchProviderLanguages.GetProvider().SelectionMade.Add(OnSelectionMade);
					SearchProviderLanguages.GetProvider().Open(current);
				}
				GUI.backgroundColor = Color.white;
				
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUI.indentLevel--;
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}


		private void OnSelectionMade(SearchTreeEntry entry)
		{
			SearchProviderLanguages.GetProvider().SelectionMade.Remove(OnSelectionMade);
			LocalizationManager.SetLanguage(((Language) entry.userData));
		}
	}
}

#endif