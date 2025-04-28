#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    [CustomEditor(typeof(LocalizedSpriteRenderer))]
    public class LocalizedSpriteRendererEditor : CustomInspector
    {
        protected override string[] HideProperties { get; }
		

        protected override void DrawInspectorGUI()
        {
            EditorGUILayout.Space(2.5f);
            DrawSetupOptions();
        }


        private void DrawSetupOptions()
        {
            EditorGUILayout.BeginVertical("HelpBox");
			
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.Fp("locId"));
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.Fp("spriteRenderer"));
			
            if (serializedObject.Fp("spriteRenderer").objectReferenceValue == null)
            {
                GUI.backgroundColor = Color.yellow;
				
                if (GUILayout.Button("Try Assign", GUILayout.Width("Try Assign".GUIWidth())))
                {
                    serializedObject.Fp("spriteRenderer").objectReferenceValue =
                        ((LocalizedSpriteRenderer) target).GetComponent<SpriteRenderer>();
                }
				
                GUI.backgroundColor = Color.white;
            }
			
            EditorGUILayout.EndHorizontal();
			
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
			
            EditorGUILayout.EndVertical();
        }
    }
}

#endif