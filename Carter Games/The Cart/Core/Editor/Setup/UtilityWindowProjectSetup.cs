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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Management.Editor;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	/// <summary>
	/// The Utility window for the project setup, handles GUI & logic.
	/// </summary>
	public sealed class UtilityWindowProjectSetup : UtilityEditorWindow
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		// The default structure to use.
		private static readonly string[] DefaultFolderStructure = new string[]
		{
			"_Assets",
			"_Project",
			"_Scratch",
			"_Project/Art/2D",
			"_Project/Art/3D",
			"_Project/Art/UI",
			"_Project/Audio",
			"_Project/Code/Editor",
			"_Project/Code/Runtime",
			"_Project/Data",
			"_Project/Prefabs/World",
			"_Project/Prefabs/UI", 
			"_Project/Scenes/Menu",
			"_Project/Scenes/Game",
		};

		// The save ids for the setup.
		private const string FolderStructureSaveId = "CarterGames_TheCart_ProjectFolderStructure";
		private const string ScrollViewSaveId = "CarterGames_TheCart_FolderStructure_ScrollPos";
		
		
		// The cache for the default structure.
		private static ProjectSetupTemplate cacheDefaultStructure;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The default structure for the folder generator.
		/// </summary>
		private static ProjectSetupTemplate DefaultStructure
		{
			get
			{
				if (cacheDefaultStructure != null) return cacheDefaultStructure;
				cacheDefaultStructure = new ProjectSetupTemplate
				{
					name = "Default"
				};

				var list = new List<string>();

				foreach (var path in DefaultFolderStructure)
				{
					list.Add(path);
				}
				
				cacheDefaultStructure.paths = list;
				return cacheDefaultStructure;
			}
		}
		

		/// <summary>
		/// The scroll position for the window.
		/// </summary>
		private static Vector2 ScrollPosSave
		{
			get => PerUserSettings.GetValue<Vector2>(ScrollViewSaveId, SettingType.PlayerPref, Vector2.zero);
			set => PerUserSettings.SetValue<Vector2>(ScrollViewSaveId, SettingType.PlayerPref, value);
		}
		
		
		/// <summary>
		/// The save for the folder structure to generate.
		/// </summary>
		private static string FolderStructureSave
		{
			get => PerUserSettings.GetValue<string>(FolderStructureSaveId, SettingType.PlayerPref);
			set => PerUserSettings.SetValue<string>(FolderStructureSaveId, SettingType.PlayerPref, value);
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Menu Item
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The menu item for the window to open from.
		/// </summary>
		[MenuItem("Tools/Carter Games/The Cart/Setup Folder Structure", priority = 20)]
		private static void OpenDisplay()
		{
			if (string.IsNullOrEmpty(FolderStructureSave))
			{
				FolderStructureSave = JsonUtility.ToJson(DefaultStructure);
			}
			
			Open<UtilityWindowProjectSetup>("Project Setup");
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   GUI Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private void OnGUI()
		{
			ScrollPosSave = EditorGUILayout.BeginScrollView(ScrollPosSave);

			EditorGUILayout.Space(5f);

			EditorGUILayout.HelpBox("Define a folder structure to create in the project if they do not exist yet. Press generate to try and create all paths defined.",
				MessageType.Info);

			EditorGUILayout.Space(1.5f);

			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			var data = JToken.Parse(FolderStructureSave);
			var list = JArray.Parse(data["paths"]!.ToString());

			// Draws each path and any new ones.
			if (data["paths"]!.Any())
			{
				EditorGUILayout.BeginVertical();

				EditorGUI.BeginChangeCheck();
				
				for (var i = 0; i < list.Count; i++)
				{
					EditorGUILayout.BeginHorizontal();
				
					EditorGUI.BeginDisabledGroup(true);
					EditorGUILayout.LabelField("Assets/", GUILayout.Width(42.5f));
					EditorGUI.EndDisabledGroup();
					
					list[i] = EditorGUILayout.TextField((string) list[i]);
				
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button("-", GUILayout.Width(22.5f)))
					{
						list.RemoveAt(i);
						break;
					}
					GUI.backgroundColor = Color.white;
				
					EditorGUILayout.EndHorizontal();
				}
				
				if (EditorGUI.EndChangeCheck())
				{
					data["paths"] = list;
					FolderStructureSave = data.ToString();
				}

				EditorGUILayout.EndVertical();
			}

			GeneralUtilEditor.DrawHorizontalGUILine();

			GUI.backgroundColor = Color.green;
			if (GUILayout.Button("+ Add new path"))
			{
				list.Add(string.Empty);
				data["paths"] = list;
				FolderStructureSave = data.ToString();
			}
			GUI.backgroundColor = Color.white;

			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();



			EditorGUILayout.Space(1.5f);

			
			GUI.backgroundColor = Color.cyan;
			if (GUILayout.Button("Generate Folder Structure", GUILayout.Height(25.5f)))
			{
				if (Dialogue.Display("Folder Structure Generator",
					    "Confirm you want to generate the defined structure where possible", "Generate", "Cancel"))
				{
					CreateAllPaths();
				}
			}
			GUI.backgroundColor = Color.white;


			EditorGUILayout.EndScrollView();
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static void CreateAllPaths()
		{
			var sortedPaths = JToken.Parse(FolderStructureSave)["paths"]!.OrderBy(t => t.ToString().Split('/').Length);

			foreach (var path in sortedPaths)
			{
				if (path.ToString().Last() == '/')
				{
					FileEditorUtil.CreateToDirectory("Assets/" + path);
				}
				else
				{
					FileEditorUtil.CreateToDirectory("Assets/" + path + "/");
				}
			}
			
			AssetDatabase.SaveAssets();   
			AssetDatabase.Refresh();
		}
	}
}