#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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

using CarterGames.Cart.Data.Editor;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Crates.DataValues.Events;
using UnityEditor;

namespace CarterGames.Cart.Crates.DataValues.Editor
{
	[CustomEditor(typeof(DataValueEventBase), true)]
	public sealed class DataValueEventEditor : InspectorDataAsset
	{
		protected override bool ShowAssetIndexOptions => false;
		protected override bool ShowVariantIdOption => false;
		
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Notes", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();
			serializedObject.Fp("devDescription").stringValue = EditorGUILayout.TextArea(serializedObject.Fp("devDescription").stringValue);
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
			
			
			EditorGUILayout.Space(5f);
			
			EditorGUILayout.BeginVertical("HelpBox");
			EditorGUILayout.Space(1.5f);
			
			EditorGUILayout.LabelField("Data", EditorStyles.boldLabel);
			GeneralUtilEditor.DrawHorizontalGUILine();
			
			EditorGUILayout.PropertyField(serializedObject.Fp("key"));
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
			

			serializedObject.ApplyModifiedProperties();
			serializedObject.Update();
		}
	}
}

#endif