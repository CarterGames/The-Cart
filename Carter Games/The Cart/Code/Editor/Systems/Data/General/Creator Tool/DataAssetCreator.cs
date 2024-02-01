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

using System.IO;
using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Data.Notion
{
    /// <summary>
    /// Creates data asset for the user.
    /// </summary>
    public sealed class DataAssetCreator : EditorWindow
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
        [MenuItem("Tools/Carter Games/The Cart/Data/Asset Creator")]
        private static void ShowWindow()
        {
            var window = GetWindow<DataAssetCreator>();
            window.titleContent = new GUIContent("Data Asset Creator");
            window.Show();
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
            
            var script = AssetDatabase.FindAssets($"t:Script {nameof(DataAssetCreator)}")[0];
            var pathToTextFile = AssetDatabase.GUIDToAssetPath(script);
            pathToTextFile = pathToTextFile.Replace("DataAssetCreator.cs", "DataAssetTemplate.txt");
            
            TextAsset template = AssetDatabase.LoadAssetAtPath<TextAsset>(pathToTextFile);
            template = new TextAsset(template.text);
            var replace = template.text.Replace("%DataAssetName%", "DataAsset" + dataAssetName);
            replace = replace.Replace("%DataTypeName%", "Data" + dataAssetName);

            File.WriteAllText(filePath, replace);
            EditorUtility.SetDirty(AssetDatabase.LoadAssetAtPath<TextAsset>(pathToTextFile));
        }
    }
}