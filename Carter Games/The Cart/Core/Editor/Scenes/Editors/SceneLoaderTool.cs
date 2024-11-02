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

using System.Linq;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart.Core.Editor
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