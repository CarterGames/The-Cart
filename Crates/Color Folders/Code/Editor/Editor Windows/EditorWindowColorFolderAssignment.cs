#if CARTERGAMES_CART_CRATE_COLORFOLDERS && UNITY_EDITOR

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
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Crates.ColourFolders.Editor
{
	public class EditorWindowColorFolderAssignment : EditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static string TargetFolderPath
		{
			get => SessionState.GetString("CarterGames_Cart_Crates_ColorFolders_TargetFolderPath", string.Empty);
			set => SessionState.SetString("CarterGames_Cart_Crates_ColorFolders_TargetFolderPath", value);
		}
		
		private static string IconSetKey
		{
			get => SessionState.GetString("CarterGames_Cart_Crates_ColorFolders_IconSetKey", string.Empty);
			set => SessionState.SetString("CarterGames_Cart_Crates_ColorFolders_IconSetKey", value);
		}
		
		private static bool Recursive
		{
			get => SessionState.GetBool("CarterGames_Cart_Crates_ColorFolders_Recursive", true);
			set => SessionState.SetBool("CarterGames_Cart_Crates_ColorFolders_Recursive", value);
		}
		
		
		private static SerializedObject ObjectRef => AutoMakeDataAssetManager.GetDefine<DataAssetFolderIconOverrides>().ObjectRef;
		
		
		private bool CanSetColor => !string.IsNullOrEmpty(TargetFolderPath) && !string.IsNullOrEmpty(IconSetKey);
		

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   GUI
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private void OnGUI()
		{
			EditorGUILayout.Space(5f);

			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUI.BeginDisabledGroup(true);
			TargetFolderPath = EditorGUILayout.TextField(new GUIContent("Path"), TargetFolderPath);
			EditorGUI.EndDisabledGroup();
			
			Recursive = EditorGUILayout.Toggle(new GUIContent("Is Recursive"), Recursive);

			EditorGUILayout.EndVertical();
			
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			var index = 0;

			EditorGUILayout.BeginVertical();
			
			foreach (var kvp in ColorFolderCache.SetsLookup)
			{
				if (index % 6 == 0)
				{
					EditorGUILayout.BeginHorizontal();
				}

				if (!string.IsNullOrEmpty(IconSetKey))
				{
					GUI.backgroundColor = IconSetKey == kvp.Key ? Color.green : Color.grey;
				}
				else
				{
					GUI.backgroundColor = Color.grey;
				}
				
				if (GUILayout.Button(new GUIContent(kvp.Value.Large)))
				{
					IconSetKey = IconSetKey == kvp.Key ? string.Empty : kvp.Key;
				}
				
				if (!string.IsNullOrEmpty(IconSetKey))
				{
					GUI.backgroundColor = Color.white;
				}

				if (index % 6 == 5)
				{
					EditorGUILayout.EndHorizontal();
				}

				index++;
			}
			
			GUI.backgroundColor = Color.white;
			
			if (index == 0)
			{
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.EndVertical();

			GeneralUtilEditor.DrawHorizontalGUILine();
			
			GUI.backgroundColor = CanSetColor ? Color.green : Color.red;
			EditorGUI.BeginDisabledGroup(!CanSetColor);
			
			if (GUILayout.Button("Apply", GUILayout.Height(27f)))
			{
				TryAddOverride();
				
				TargetFolderPath = string.Empty;
				Recursive = true;
				IconSetKey = string.Empty;
				
				GetWindow<EditorWindowColorFolderAssignment>().Close();
			}
			
			EditorGUI.EndDisabledGroup();
			GUI.backgroundColor = Color.white;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   GUi Control
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		public static void OpenAssignToFolder(string path)
		{
			TargetFolderPath = path;
			var window = GetWindow(typeof(EditorWindowColorFolderAssignment), true, "Assign Folder Color");
			window.minSize = new Vector2(500, 250);
			window.maxSize = new Vector2(500, 250);
			window.Show();
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Helper Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void TryAddOverride()
		{
			if (string.IsNullOrEmpty(TargetFolderPath)) return;
			if (string.IsNullOrEmpty(IconSetKey)) return;
			
			SerializedProperty entry;
			
			for (var i = 0; i < ObjectRef.Fp("folderOverrides").arraySize; i++)
			{
				entry = ObjectRef.Fp("folderOverrides").GetIndex(i);

				if (entry.Fpr("folderPath").stringValue != TargetFolderPath) continue;
				
				entry.Fpr("isRecursive").boolValue = Recursive;
				entry.Fpr("folderSetId").stringValue = IconSetKey;
				ObjectRef.ApplyModifiedProperties();
				ObjectRef.Update();
				
				ColorFolderCache.FolderResult.Clear();
				EditorApplication.RepaintProjectWindow();
				
				return;
			}

			ObjectRef.Fp("folderOverrides").InsertIndex(ObjectRef.Fp("folderOverrides").arraySize);
			
			entry = ObjectRef.Fp("folderOverrides").GetIndex(ObjectRef.Fp("folderOverrides").arraySize-1);

			entry.Fpr("folderPath").stringValue = TargetFolderPath;
			entry.Fpr("isRecursive").boolValue = Recursive;
			entry.Fpr("folderSetId").stringValue = IconSetKey;
			ObjectRef.ApplyModifiedProperties();
			ObjectRef.Update();
			
			ColorFolderCache.FolderResult.Clear();
			EditorApplication.RepaintProjectWindow();
		}
	}
}

#endif