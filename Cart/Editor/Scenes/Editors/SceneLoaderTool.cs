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

using System.Linq;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart.Editor
{
	public class SceneLoaderTool : IToolbarElementRight
	{
        private static GenericMenu scenesMenu;
        

        private static void OnEditorDelayCall()
        {
            EditorApplication.delayCall -= OnEditorDelayCall;
            OnSceneChanged(SceneManager.GetActiveScene(), OpenSceneMode.Single);
        }

        
        private static void UpdateSceneOptions()
        {
            scenesMenu = new GenericMenu();

            var foundSceneFiles = AssetDatabase.FindAssets("t:SceneAsset");
            int sceneCount = foundSceneFiles.Length;     
            string[] scenes = new string[sceneCount];

            for( int i = 0; i < sceneCount; i++ )
            {
                scenes[i] = AssetDatabase.GUIDToAssetPath(foundSceneFiles[i]);
            }
            
            var scenesInBuildSettings = EditorBuildSettings.scenes;

            foreach (var sceneEntryPath in scenes)
            {
                var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneEntryPath);

                if (scenesInBuildSettings.Any(t => t.path.Equals(sceneEntryPath)))
                {
                    scenesMenu.AddItem(new GUIContent("Build/" + asset.name), EditorSceneManager.GetActiveScene().path == sceneEntryPath, OnSceneSelected, sceneEntryPath);
                }
                else
                {
                    scenesMenu.AddItem(new GUIContent("Project/" + asset.name), EditorSceneManager.GetActiveScene().path == sceneEntryPath, OnSceneSelected, sceneEntryPath);
                }
            }
        }
        

        private static void OnSceneChanged(Scene activeScene, OpenSceneMode openSceneMode)
        {
            UpdateSceneOptions();
        }
        

        private static void OnMouseDown()
        {
            scenesMenu.ShowAsContext();
        }


        private static void OnSceneSelected(object scenePath)
        {
            if (SceneManager.GetActiveScene().path == (string)scenePath) return;
            
            if (SceneManager.GetActiveScene().isDirty)
            {
                var option = EditorUtility.DisplayDialogComplex("Scene Hop", "You have unsaved changes in the current scene, do you want to save them and hop?, or hop without saving?",
                    "Hop (Save)", "Hop (Don't Save)", "Cancel");

                switch (option)
                {
                    case 0:
                        HopSceneSaveChangesInCurrent((string) scenePath);
                        break;
                    case 1:
                        HopSceneDontSaveChangesInCurrent((string) scenePath);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                HopSceneSaveChangesInCurrent((string) scenePath);
            }
        }


        private static void HopSceneSaveChangesInCurrent(string scenePath)
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene(scenePath);
        }
        
        
        private static void HopSceneDontSaveChangesInCurrent(string scenePath)
        {
            EditorSceneManager.OpenScene(scenePath);
        }
        
        
        public int RightOrder { get; }

        public void Initialize()
        {
            EditorSceneManager.sceneOpened -= OnSceneChanged;
            EditorSceneManager.sceneOpened += OnSceneChanged;
            
            EditorApplication.delayCall -= OnEditorDelayCall;
            EditorApplication.delayCall += OnEditorDelayCall;
        }

        public void OnRightGUI()
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isCompiling || EditorApplication.isPlaying);
            
            var label = " " + SceneManager.GetActiveScene().name.Replace("SCNE_", string.Empty);
            GUILayout.Space(2.5f);
            
            if (EditorGUILayout.DropdownButton(new GUIContent(label, EditorGUIUtility.IconContent("SceneAsset Icon").image), FocusType.Passive, GUILayout.Width(label.GUIWidth() + 37.5f)))
            {
                OnMouseDown();
            }
            
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(2.5f);
            GUI.backgroundColor = Color.white;
        }
    }
}