#if CARTERGAMES_CART_MODULE_DATAVALUES

/*
 * Copyright (c) 2024 Carter Games
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

using System;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues.Editor.Window
{
	/// <summary>
	/// Handles the creator tool for data values.
	/// </summary>
	public sealed class DataValuesCreatorTool : EditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private DataValuesSearchProvider search;
		private SearchTreeEntry selectedEntry;

		private const string KeyId = "dataValueCreator_newKey";
		private const string TypeId = "dataValueCreator_newType";
		private const string LocationId = "dataValueCreator_newLocation";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static string Key
		{
			get => (string) PerUserSettings.GetOrCreateValue<string>(KeyId, SettingType.SessionState);
			set => PerUserSettings.SetValue<string>(KeyId, SettingType.SessionState, value);
		}
		
		
		private static string TypeName
		{
			get => (string) PerUserSettings.GetOrCreateValue<string>(TypeId, SettingType.SessionState);
			set => PerUserSettings.SetValue<string>(TypeId, SettingType.SessionState, value);
		}
		
		
		private static string Location
		{
			get => (string) PerUserSettings.GetOrCreateValue<string>(LocationId, SettingType.SessionState);
			set => PerUserSettings.SetValue<string>(LocationId, SettingType.SessionState, value);
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Menu Items
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
		/// <summary>
		/// Makes a menu item for the creator to open.
		/// </summary>
		[MenuItem("Tools/Carter Games/The Cart/Modules/Data Values/Data Value Asset Creator", priority = 300)]
		private static void ShowWindow()
		{
			var window = GetWindow<DataValuesCreatorTool>();
			window.titleContent = new GUIContent("Data Value Creator");
			window.Show();
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnGUI()
		{
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
			
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			DrawKeyField();
			DrawTypeSelect();
			DrawSelectLocation();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);

			DrawCreateSection();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}

		
		private void DrawKeyField()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.LabelField("Key:", GUILayout.Width(45f));
			
			var value = EditorGUILayout.TextField((string) PerUserSettings.GetOrCreateValue<string>(KeyId, SettingType.SessionState));
			
			if (EditorGUI.EndChangeCheck())
			{
				PerUserSettings.SetValue<string>(KeyId, SettingType.SessionState, value);
			}
			
			EditorGUILayout.EndHorizontal();
		}


		private void DrawTypeSelect()
		{
			if (search == null)
			{
				search = CreateInstance<DataValuesSearchProvider>();
			}

			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField("Type:", GUILayout.Width(45f));
			
			if (selectedEntry != null)
			{
				if (selectedEntry.userData != null)
				{
					EditorGUILayout.LabelField(((Type) selectedEntry.userData).Name);

					if (GUILayout.Button("Change Type", GUILayout.Width(100)))
					{
						DataValuesSearchProvider.ToExclude.Clear();

						DataValuesSearchProvider.OnSearchTreeSelectionMade.Clear();

						if (selectedEntry != null)
						{
							DataValuesSearchProvider.OnSearchTreeSelectionMade.Remove(OnSelectionMade);
							DataValuesSearchProvider.OnSearchTreeSelectionMade.Add(OnSelectionMade);
						}

						SearchWindow.Open(
							new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), search);
					}
				}
				else
				{
					if (GUILayout.Button("Select Type"))
					{
						DataValuesSearchProvider.ToExclude.Clear();
                            
						DataValuesSearchProvider.OnSearchTreeSelectionMade.Clear();
						
						DataValuesSearchProvider.OnSearchTreeSelectionMade.Remove(OnSelectionMade);
						DataValuesSearchProvider.OnSearchTreeSelectionMade.Add(OnSelectionMade);
				
						SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), search);
					}
				}
			}
			else
			{
				if (GUILayout.Button("Select Type"))
				{
					DataValuesSearchProvider.ToExclude.Clear();
                            
					DataValuesSearchProvider.OnSearchTreeSelectionMade.Clear();
					
					DataValuesSearchProvider.OnSearchTreeSelectionMade.Remove(OnSelectionMade);
					DataValuesSearchProvider.OnSearchTreeSelectionMade.Add(OnSelectionMade);
				
					SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), search);
				}
			}
			
			EditorGUILayout.EndHorizontal();
			

		}


		private void DrawSelectLocation()
		{
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField("Path:", GUILayout.Width(45f));
			
			
			if (!string.IsNullOrEmpty((string) PerUserSettings.GetOrCreateValue<string>(LocationId, SettingType.SessionState)))
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.TextField(
					(string) PerUserSettings.GetOrCreateValue<string>(LocationId, SettingType.SessionState));
				
				if (GUILayout.Button("Change Path", GUILayout.Width(100)))
				{
					PerUserSettings.SetValue<string>(LocationId, SettingType.SessionState,
						EditorUtility.SaveFilePanelInProject("Save New Data Value Asset", $"New Data Value Asset",
							"asset", ""));
				}
				
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				if (GUILayout.Button("Select Path"))
				{
					PerUserSettings.SetValue<string>(LocationId, SettingType.SessionState,
						EditorUtility.SaveFilePanelInProject("Save New Data Value Asset", $"New Data Value Asset",
							"asset", ""));
				}
			}
			
			EditorGUILayout.EndHorizontal();
		}


		private void DrawCreateSection()
		{
			EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(Key) || 
			                             string.IsNullOrEmpty(TypeName) || 
			                             string.IsNullOrEmpty(Location));
			
			if (GUILayout.Button("Create"))
			{
				CreateAsset(Key, Type.GetType(TypeName));

				Key = string.Empty;
				TypeName = string.Empty;
				Location = string.Empty;
			}
			
			EditorGUI.EndDisabledGroup();
		}


		private void OnSelectionMade(SearchTreeEntry entry)
		{
			selectedEntry = entry;
			TypeName = ((Type)entry.userData).FullName;
		}
		

		private void CreateAsset(string key, Type type)
		{
			AssetDatabase.CreateAsset(CreateInstance(type), Location);
			
			var instance = (DataValueAsset) AssetDatabase.LoadAssetAtPath(Location, type);
			var instanceSo = new SerializedObject(instance);
			
			instanceSo.Fp("key").stringValue = key;

			instanceSo.ApplyModifiedProperties();
			instanceSo.Update();
			
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}

#endif