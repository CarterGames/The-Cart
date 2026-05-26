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

using System;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
	/// <summary>
	/// Inherit from to make a custom inspector.
	/// </summary>
	public abstract class CustomInspector : UnityEditor.Editor
	{
		/// <summary>
		/// Stores the rect for the width to assign to.
		/// </summary>
		private Rect widthRect;


		/// <summary>
		/// Gets the raw width without padding.
		/// </summary>
		protected float ScreenWidth => widthRect.width;
		
		
		/// <summary>
		/// Gets the width of the inspector with some padding so its ready for use.
		/// </summary>
		protected float ScreenWidthPadded => widthRect.width - 12f;
		
		
		/// <summary>
		/// Gets the properties to not draw in the inspector.
		/// </summary>
		protected abstract string[] HideProperties { get; }
		
		
		/// <summary>
		/// Override to change the inspector GUI entirely.
		/// </summary>
		public override void OnInspectorGUI()
		{
			GetWidth();
			
			EditorGUI.BeginChangeCheck();
			
			GUILayout.Space(2.5f);
			
			DrawScriptField();
			
			DrawInspectorGUI();

			if (EditorGUI.EndChangeCheck())
			{
				try
				{
					serializedObject.ApplyModifiedProperties();
					serializedObject.Update();
				}
#pragma warning disable 0168
				catch (Exception e)
#pragma warning restore
				{
					// Console.WriteLine(e);
					// throw;
				}
			}
		}


		/// <summary>
		/// Calculates the width for the custom inspectors to use.
		/// </summary>
		private void GetWidth()
		{
			EditorGUILayout.LabelField(string.Empty, GUILayout.MaxHeight(0));
			
			if (Event.current.type == EventType.Repaint)
			{
				widthRect = GUILayoutUtility.GetLastRect();
			}
		}
		

		/// <summary>
		/// Implement to add your own GUI to the base GUI of the custom editor.
		/// </summary>
		protected abstract void DrawInspectorGUI();


		/// <summary>
		/// Draws the script field for the type the inspector is for.
		/// </summary>
		/// <param name="styleName"></param>
		private void DrawScriptField(string styleName = "HelpBox")
		{
			EditorGUILayout.BeginVertical(styleName);
			EditorGUILayout.Space(1.5f);

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(serializedObject.Fp("m_Script"));
			EditorGUI.EndDisabledGroup();
			
			EditorGUILayout.Space(1.5f);
			EditorGUILayout.EndVertical();
		}


		/// <summary>
		/// Draws the base inspector when called.
		/// </summary>
		protected void DrawBaseInspectorGUI()
		{
			DrawPropertiesExcluding(serializedObject, HideProperties);
		}
	}
}