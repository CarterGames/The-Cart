#if CARTERGAMES_CART_MODULE_DEVENVIRONMENTS

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

using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.DevEnvironments.Editor
{
	[InitializeOnLoad]
	public class EnvironmentToggleButton : IToolbarElement
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static readonly GenericMenu optionsMenu;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IToolbarElement
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public int LeftOrder { get; }
		public void OnLeftGUI() { }

		
		public int RightOrder => -1;
		public void OnRightGUI()
		{
			EditorGUI.BeginDisabledGroup(EditorApplication.isCompiling || EditorApplication.isPlaying);

			var label = EnvironmentDetection.CurrentEnvironment.ToString();
			GUILayout.Space(2.5f);

			Color buttonCol;
			
#if DEV_ENVIRONMENT_DEVELOPMENT
			buttonCol = Color.red;
#elif DEV_ENVIRONMENT_TEST
			buttonCol = Color.yellow;
#elif DEV_ENVIRONMENT_PRESS
			buttonCol = Color.cyan;
#else
			buttonCol = Color.green;
#endif

			GUI.backgroundColor = buttonCol;
			
			if (EditorGUILayout.DropdownButton(new GUIContent($" {label}", EditorGUIUtility.IconContent("d_Package Manager").image), FocusType.Passive, GUILayout.Width(label.GUIWidth() + 42.5f)))
			{
				optionsMenu.ShowAsContext();
			}
			
			GUI.backgroundColor = Color.white;
            
			EditorGUI.EndDisabledGroup();
			GUILayout.Space(2.5f);
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructor
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		static EnvironmentToggleButton()
		{
			optionsMenu = new GenericMenu();
			
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Production.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Production, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Production));
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Development.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Development, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Development));
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Test.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Test, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Test));
#if CARTERGAMES_CART_MODULE_PRESS
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Press.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Press, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Press));
#endif
		}
	}
}

#endif