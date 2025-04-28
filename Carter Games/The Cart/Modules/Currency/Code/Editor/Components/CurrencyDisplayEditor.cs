#if CARTERGAMES_CART_MODULE_CURRENCY && UNITY_EDITOR

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

namespace CarterGames.Cart.Modules.Currency.Editor
{
	[CustomEditor(typeof(CurrencyDisplay), true)]
	public class CurrencyDisplayEditor : CustomInspector
	{
		protected override string[] HideProperties { get; }
		

		protected override void DrawInspectorGUI()
		{
			EditorGUILayout.BeginVertical("HelpBox");
			
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.PropertyField(serializedObject.Fp("accountId"));
			EditorGUILayout.PropertyField(serializedObject.Fp("formatter"));
			DisplayStyleEditor.DrawStyleSettings(serializedObject.Fp("displayStyle"));

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(serializedObject.Fp("label"));

			if (serializedObject.Fp("label").objectReferenceValue == null)
			{
				GUI.backgroundColor = Color.yellow;

				if (GUILayout.Button("Try Get Reference", GUILayout.Width(140)))
				{
					serializedObject.Fp("label").objectReferenceValue ??=
						((CurrencyDisplay) target).GetComponentInChildren<Canvas>();

					serializedObject.ApplyModifiedProperties();
					serializedObject.Update();
				}

				GUI.backgroundColor = Color.white;
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space(1f);
			
			EditorGUILayout.EndVertical();
		}
	}
}

#endif