#if CARTERGAMES_CART_CRATE_MULTISCENE && UNITY_EDITOR

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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Cart.Crates.MultiScene.Editor
{
    /// <summary>
    /// Handles the editor window for all the scene groups in the project...
    /// </summary>
    public sealed class SceneGroupLoader : EditorWindow
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private SceneGroup[] allSceneGroups;
        private Vector2 scrollPos;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Menu Items
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */         

        /// <summary>
        /// Shows the scene group loader or focuses on it if it is already open.
        /// </summary>
        [MenuItem("Tools/Carter Games/The Cart/[Multi Scene] Scene Group Loader", priority = 1500)]
        private static void ShowWindow()
        {
            var window = GetWindow<SceneGroupLoader>();
            window.titleContent = new GUIContent("Scene Group Loader");
            
            window.Show();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        private void OnEnable()
        {
            MultiSceneEditorEvents.Settings.OnSettingChanged.Add(UpdateData);
            MultiSceneEditorEvents.Settings.OnGroupCategoriesChanged.Add(UpdateData);

            MultiSceneEditorEvents.SceneGroups.OnSceneGroupCreated.Add(UpdateData);
            MultiSceneEditorEvents.SceneGroups.OnSceneGroupCategoryChanged.Add(UpdateData);
            
            GetAllGroups();
            UpdateData();
        }


        private void OnDisable()
        {
            MultiSceneEditorEvents.Settings.OnSettingChanged.Remove(UpdateData);
            MultiSceneEditorEvents.Settings.OnGroupCategoriesChanged.Remove(UpdateData);

            MultiSceneEditorEvents.SceneGroups.OnSceneGroupCreated.Remove(UpdateData);
            MultiSceneEditorEvents.SceneGroups.OnSceneGroupCategoryChanged.Remove(UpdateData);
        }


        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                GUILayout.Space(5f);
                EditorGUILayout.HelpBox("You cannot use this window at runtime, please exit play mode to use this window.", MessageType.Warning);
                GUI.enabled = false;
            }
            else
            {
                GUI.enabled = true;
            }
            
            
            if (allSceneGroups == null)
            {
                GetAllGroups();
            }

            if (allSceneGroups == null) return;
            if (allSceneGroups.Length <= 0)
            {
                EditorGUILayout.HelpBox("There are no scene groups in the project currently. You will need to create a scene group for something to show here", MessageType.Info);
            }
            
            EditorGUILayout.HelpBox("Click the buttons below to load a specific scene group in the editor.", MessageType.Info);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            DrawGroupsAndButtons(GUI.enabled);
            EditorGUILayout.EndScrollView();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Updates the options for the scene group loader.
        /// </summary>
        private void UpdateData()
        {
            GetAllGroups();
            Repaint();
        }


        /// <summary>
        /// Draws all the buttons for the scene groups in the categories required.
        /// </summary>
        private void DrawGroupsAndButtons(bool isEnabled)
        {
            EditorGUILayout.Space(4f);

            foreach (var group in allSceneGroups)
            {
                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(group, typeof(SceneGroup), false);
                
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(!group.ContainsScene(string.Empty) && group.IsValid && isEnabled);
                
                if (GUILayout.Button("Load Scene Group", GUILayout.Width(150)))
                {
                    LoadSceneGroupInEditor(group);
                }
                
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.Space(1.5f);
        }

        
        /// <summary>
        /// Gets all the scene groups in the project.
        /// </summary>
        private void GetAllGroups()
        {
            allSceneGroups = AssetDatabaseHelper.GetAllInstancesInProject<SceneGroup>().ToArray();
        }
        
        
        /// <summary>
        /// Loads a scene group in the editor when called.
        /// </summary>
        /// <param name="group">The group to load.</param>
        private static void LoadSceneGroupInEditor(SceneGroup group)
        {
            var sceneList = new List<string>();

            for (var i = 0; i < group.scenes.Count; i++)
                sceneList.Add(group.scenes[i].SceneName);

            var paths = GetScenePaths();
            
            if (sceneList.Count <= 0) return;

            for (var i = 0; i < sceneList.Count; i++)
            {
                var scene = sceneList[i];
                var path = paths.FirstOrDefault(t => t.Contains(scene));

                if (i.Equals(0))
                {
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                }
                else
                {
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                }
            }

            AutoMakeDataAssetManager.GetDefine<MultiSceneSettings>().ObjectRef.Fp("lastGroup").objectReferenceValue = group;
            AutoMakeDataAssetManager.GetDefine<MultiSceneSettings>().ObjectRef.ApplyModifiedProperties();
            AutoMakeDataAssetManager.GetDefine<MultiSceneSettings>().ObjectRef.Update();
            
            MultiSceneEditorEvents.SceneGroups.OnSceneGroupLoadedInEditor.Raise();
        }
        
        
        /// <summary>
        /// Gets the paths for all scenes in the project build settings for use.
        /// </summary>
        /// <returns>A list of the paths of the scenes in the build settings.</returns>
        private static List<string> GetScenePaths()
        {
            var sceneNumber = SceneManager.sceneCountInBuildSettings;
            var arrayOfNames = new string[sceneNumber];
            
            for (var i = 0; i < sceneNumber; i++)
            {
                arrayOfNames[i] = SceneUtility.GetScenePathByBuildIndex(i);
            }

            return arrayOfNames.ToList();
        }
    }
}

#endif