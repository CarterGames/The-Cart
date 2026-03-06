#if CARTERGAMES_CART_CRATE_PANELS && UNITY_EDITOR

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
using CarterGames.Cart.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Crates.Panels.Editor
{
    [CustomEditor(typeof(Panel), true)]
    public class PanelEditor : UnityEditor.Editor
    {
        protected virtual bool HasExtra { get; }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            GeneralUtilEditor.DrawMonoScriptSection((Panel) target);
            
            DrawHelpSection();
            EditorGUILayout.Space(2.5f);
            DrawUtilityButtons();
            EditorGUILayout.Space(2.5f);
            DrawMeta();
            EditorGUILayout.Space(2.5f);
            DrawReferences();
            EditorGUILayout.Space(2.5f);
            DrawTransitions();
            
            if (HasExtra)
            {
                EditorGUILayout.Space(2.5f);
                DrawExtraContent();
            }
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }


        private void DrawHelpSection()
        {
            if (serializedObject.Fp("transitions").arraySize > 0) return;
            
            EditorGUILayout.Space(2.5f);
            
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            if (serializedObject.Fp("transitions").arraySize <= 0)
            {
                EditorGUILayout.HelpBox("Panel has no transitions assigned.", MessageType.Warning);
            }
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void DrawUtilityButtons()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUI.BeginDisabledGroup(!((Panel) target).IsValid());
            
            if (GUILayout.Button("Toggle Panel"))
            {
                TogglePanelState();
            }
            
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void DrawMeta()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            
            EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("panelId"));
            EditorGUILayout.PropertyField(serializedObject.Fp("panelObject"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void DrawReferences()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);
            
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

            if (serializedObject.Fp("canvas").objectReferenceValue == null || serializedObject.Fp("graphicRaycaster").objectReferenceValue == null)
            {
                GUI.backgroundColor = Color.yellow;

                if (GUILayout.Button("Try Get References", GUILayout.Width(140)))
                {
                    serializedObject.Fp("canvas").objectReferenceValue ??=
                        ((Panel)target).GetComponentInChildren<Canvas>();
                    serializedObject.Fp("graphicRaycaster").objectReferenceValue ??=
                        ((Panel)target).GetComponentInChildren<GraphicRaycaster>();

                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }

                GUI.backgroundColor = Color.white;
            }
            
            EditorGUILayout.EndHorizontal();
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUILayout.PropertyField(serializedObject.Fp("canvas"));
            EditorGUILayout.PropertyField(serializedObject.Fp("graphicRaycaster"));
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        private void DrawTransitions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space(1f);

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("Transitions", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Find Transitions", GUILayout.Width(125)))
            {
                serializedObject.Fp("transitions").ClearArray();

                var components = ((Panel) target).transform.root.GetComponentsInChildren<PanelTransition>(true);

                for (var i = 0; i < components.Length; i++)
                {
                    serializedObject.Fp("transitions").InsertIndex(serializedObject.Fp("transitions").arraySize);
                    serializedObject.Fp("transitions").GetIndex(serializedObject.Fp("transitions").arraySize - 1).objectReferenceValue = components[i];
                }

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            
            EditorGUILayout.EndHorizontal();
            
            GeneralUtilEditor.DrawHorizontalGUILine();
            
            EditorGUI.indentLevel++;

            for (var i = 0; i < serializedObject.Fp("transitions").arraySize; i++)
            {
                var entry = serializedObject.Fp("transitions").GetIndex(i);

                if (entry.objectReferenceValue == null) continue;
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(new GUIContent(entry.objectReferenceValue.GetType().Name.Replace("PanelTransition", string.Empty)), entry.objectReferenceValue, typeof(PanelTransition), true);

                GUI.backgroundColor = Color.red;
                
                if (GUILayout.Button(" - ", GUILayout.Width(27.5f)))
                {
                    serializedObject.Fp("transitions").DeleteIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    continue;
                }
                
                GUI.backgroundColor = Color.white;
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space(1.5f);
            EditorGUILayout.EndVertical();
        }


        protected virtual void DrawExtraContent()
        {
            
        }


        private void TogglePanelState(bool? state = null)
        {
            var canvasRef = ReflectionHelper.GetField(typeof(Panel), "canvas", false).GetValue(target);
            var canvasEnabled = ReflectionHelper.GetProperty(canvasRef.GetType(), "enabled").GetValue(canvasRef);
            var targetState = state.HasValue ? state.Value : !((bool)canvasEnabled);
            
            ReflectionHelper.GetProperty(canvasRef.GetType(), "enabled").SetValue(canvasRef, targetState);
                
            var graphicRaycaster = ReflectionHelper.GetField(typeof(Panel), "graphicRaycaster", false).GetValue(target);

            if (graphicRaycaster != null)
            {
                var gREnabled = ReflectionHelper.GetProperty(graphicRaycaster.GetType(), "enabled").GetValue(graphicRaycaster);
                ReflectionHelper.GetProperty(graphicRaycaster.GetType(), "enabled").SetValue(graphicRaycaster, targetState);
            }
        }
    }
}

#endif