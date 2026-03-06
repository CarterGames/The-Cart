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

using System.IO;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Data.Editor
{
    /// <summary>
    /// Creates data asset for the user.
    /// </summary>
    public sealed class UtilityWindowDataAssetCreator : UtilityEditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private string dataAssetName;
        private bool makeDataAssetWhenGenerated;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Makes a menu item for the creator to open.
        /// </summary>
        [MenuItem("Tools/Carter Games/The Cart/[Data] Data Asset Creator", priority = 121)]
        private static void ShowWindow()
        {
            Open<UtilityWindowDataAssetCreator>("Data Asset Creator");
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Draws the GUI.
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Space(4f);

            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);

            EditorGUILayout.LabelField("Data Asset Name", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            dataAssetName = EditorGUILayout.TextField(dataAssetName);

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();

            GUILayout.Space(4f);

            EditorGUILayout.BeginVertical("HelpBox");
            GUILayout.Space(1.5f);

            EditorGUILayout.LabelField("Will create:", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            EditorGUILayout.LabelField(new GUIContent("DataAsset: "),
                new GUIContent("DataAsset" + dataAssetName + ".cs"));

            GUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();

            GUILayout.Space(4f);

            if (GUILayout.Button("Create"))
            {
                CreateAssetFile(out string path);
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                EditorUtility.RequestScriptReload();

                if (Dialogue.Display("Data Asset Creator",
                        "Would you like to open the newly created files for editing?", "Yes", "No"))
                {
                    AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<TextAsset>(path));
                }
            }
        }


        /// <summary>
        /// Creates the asset file for the data class the user is making.
        /// </summary>
        /// <param name="filePath">The path to create at.</param>
        private void CreateAssetFile(out string filePath)
        {
            filePath = EditorUtility.SaveFilePanelInProject("Save New Data Asset Class",
                $"DataAsset{dataAssetName}", "cs", "");
            
            var script = AssetDatabase.FindAssets($"t:Script {nameof(UtilityWindowDataAssetCreator)}")[0];
            var pathToTextFile = AssetDatabase.GUIDToAssetPath(script);
            pathToTextFile = pathToTextFile.Replace("UtilityWindowDataAssetCreator.cs", "DataAssetTemplate.txt");
            
            TextAsset template = AssetDatabase.LoadAssetAtPath<TextAsset>(pathToTextFile);
            template = new TextAsset(template.text);
            var replace = template.text.Replace("%DataAssetName%", "DataAsset" + dataAssetName);
            replace = replace.Replace("%DataTypeName%", "Data" + dataAssetName);

            File.WriteAllText(filePath, replace);
            EditorUtility.SetDirty(AssetDatabase.LoadAssetAtPath<TextAsset>(pathToTextFile));
        }
    }
}