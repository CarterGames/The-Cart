#if CARTERGAMES_CART_MODULE_COLORFOLDERS

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

using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
{
	public class EditorWindowColorFolderAssignment : EditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static string folderPath;
		private bool recursive = true;
		private DataFolderIconSet iconSet;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static SerializedObject ObjectRef => ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().ObjectRef;
		private bool CanSetColor => !string.IsNullOrEmpty(folderPath) && iconSet != null;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   GUi Control
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public static void OpenAssignToFolder(string path)
		{
			folderPath = path;
			
			if (!HasOpenInstances<EditorWindowColorFolderAssignment>())
			{
				GetWindow<EditorWindowColorFolderAssignment>("Assign Folder Color", true).Show();
			}
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   GUI
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnGUI()
		{
			EditorGUILayout.Space(5f);

			EditorGUI.BeginDisabledGroup(true);
			folderPath = EditorGUILayout.TextField(new GUIContent("Path"), folderPath);
			EditorGUI.EndDisabledGroup();
			
			recursive = EditorGUILayout.Toggle(new GUIContent("Is Recursive"), recursive);

			if (iconSet == null)
			{
				if (GUILayout.Button("Select Style"))
				{
					SearchProviderFolderOptions.GetProvider().SelectionMade.Remove(OnSelectionMade);
					SearchProviderFolderOptions.GetProvider().SelectionMade.Add(OnSelectionMade);

					SearchProviderFolderOptions.GetProvider().Open(ColourFolderManager.CurrentlyAppliedSet(folderPath));
				}
			}
			else
			{
				EditorGUILayout.BeginHorizontal();

				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.TextField(new GUIContent("Style"), iconSet.Id);
				EditorGUI.EndDisabledGroup();
				
				if (GUILayout.Button("Select Style", GUILayout.Width(100)))
				{
					SearchProviderFolderOptions.GetProvider().SelectionMade.Remove(OnSelectionMade);
					SearchProviderFolderOptions.GetProvider().SelectionMade.Add(OnSelectionMade);

					SearchProviderFolderOptions.GetProvider().Open(ColourFolderManager.CurrentlyAppliedSet(folderPath));
				}
				
				EditorGUILayout.EndHorizontal();
			}

			GUI.backgroundColor = CanSetColor ? Color.green : Color.red;
			EditorGUI.BeginDisabledGroup(!CanSetColor);
			
			if (GUILayout.Button("Apply"))
			{
				TryAddOverride();
				
				folderPath = string.Empty;
				recursive = true;
				iconSet = null;
				
				GetWindow<EditorWindowColorFolderAssignment>().Close();
			}
			
			EditorGUI.EndDisabledGroup();
			GUI.backgroundColor = Color.white;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Helper Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnSelectionMade(SearchTreeEntry entry)
		{
			SearchProviderFolderOptions.GetProvider().SelectionMade.Remove(OnSelectionMade);
			iconSet = (DataFolderIconSet) entry.userData;
		}


		private void TryAddOverride()
		{
			if (string.IsNullOrEmpty(folderPath)) return;
			if (iconSet == null) return;
			
			SerializedProperty entry;
			
			for (var i = 0; i < ObjectRef.Fp("folderOverrides").arraySize; i++)
			{
				entry = ObjectRef.Fp("folderOverrides").GetIndex(i);

				if (entry.Fpr("folderPath").stringValue != folderPath) continue;
				
				entry.Fpr("isRecursive").boolValue = recursive;
				entry.Fpr("folderSetId").stringValue = iconSet.Id;
				ObjectRef.ApplyModifiedProperties();
				ObjectRef.Update();
				
				ColorFolderCache.FolderResult.Clear();
				EditorApplication.RepaintProjectWindow();
				
				return;
			}

			ObjectRef.Fp("folderOverrides").InsertIndex(ObjectRef.Fp("folderOverrides").arraySize);
			
			entry = ObjectRef.Fp("folderOverrides").GetIndex(ObjectRef.Fp("folderOverrides").arraySize-1);

			entry.Fpr("folderPath").stringValue = folderPath;
			entry.Fpr("isRecursive").boolValue = recursive;
			entry.Fpr("folderSetId").stringValue = iconSet.Id;
			ObjectRef.ApplyModifiedProperties();
			ObjectRef.Update();
			
			ColorFolderCache.FolderResult.Clear();
			EditorApplication.RepaintProjectWindow();
		}
	}
}

#endif