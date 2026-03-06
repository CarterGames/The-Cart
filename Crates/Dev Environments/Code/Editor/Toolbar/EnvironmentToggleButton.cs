#if CARTERGAMES_CART_CRATE_DEVENVIRONMENTS && UNITY_EDITOR

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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.DevEnvironments.Editor
{
	public class EnvironmentToggleButton : IToolbarElementRight
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static GenericMenu optionsMenu;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IToolbarElement
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		public int RightOrder => -1;


		public void Initialize()
		{
			optionsMenu = new GenericMenu();
			
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Release.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Release, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Release));
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Development.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Development, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Development));
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Test.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Test, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Test));
#if CARTERGAMES_CART_CRATE_PRESS
			optionsMenu.AddItem(new GUIContent(DevelopmentEnvironments.Press.ToString()), EnvironmentDetection.CurrentEnvironment == DevelopmentEnvironments.Press, () => ProjectEnvironmentHandler.SetEnvironment(DevelopmentEnvironments.Press));
#endif
		}

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
	}
}

#endif