#if CARTERGAMES_CART_MODULE_COLORFOLDERS && UNITY_EDITOR

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
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
{
	public class EditorWindowColorFolderAssignment : EditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static string TargetFolderPath
		{
			get => SessionState.GetString("CarterGames_Cart_Modules_ColorFolders_TargetFolderPath", string.Empty);
			set => SessionState.SetString("CarterGames_Cart_Modules_ColorFolders_TargetFolderPath", value);
		}
		
		private static string IconSetKey
		{
			get => SessionState.GetString("CarterGames_Cart_Modules_ColorFolders_IconSetKey", string.Empty);
			set => SessionState.SetString("CarterGames_Cart_Modules_ColorFolders_IconSetKey", value);
		}
		
		private static bool Recursive
		{
			get => SessionState.GetBool("CarterGames_Cart_Modules_ColorFolders_Recursive", true);
			set => SessionState.SetBool("CarterGames_Cart_Modules_ColorFolders_Recursive", value);
		}
		
		
		private static SerializedObject ObjectRef => ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().ObjectRef;
		
		
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
				if (index % 5 == 0)
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

				if (index % 5 == 4)
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
			window.minSize = new Vector2(435, 250);
			window.maxSize = new Vector2(435, 250);
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