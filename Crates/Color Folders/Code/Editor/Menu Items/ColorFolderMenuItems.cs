#if CARTERGAMES_CART_CRATE_COLORFOLDERS && UNITY_EDITOR

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
using System.Reflection;
using UnityEditor;

namespace CarterGames.Cart.Crates.ColourFolders.Editor
{
	public static class ColorFolderMenuItems
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Menu Items
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Opens the folder color editor for the select folder.
		/// </summary>
		[MenuItem("Assets/Color Folders/Set Folder Color", priority = 1)] 
		private static void OpenColorAssign()
		{
			if (!TryGetActiveFolderPath(out var path)) return;
			EditorWindowColorFolderAssignment.OpenAssignToFolder(path);
		}


		/// <summary>
		/// Validates the OpenColorAssign() menu item for use.
		/// </summary>
		/// <returns>If the option can be used or not.</returns>
		[MenuItem("Assets/Color Folders/Set Folder Color", true)]
		private static bool OpenColorAssignValidation()
		{
			return TryGetActiveFolderPath(out _);
		}


		/// <summary>
		/// Resets the folder color of a folder when used.
		/// </summary>
		[MenuItem("Assets/Color Folders/Reset Folder Color", priority = 2)]
		private static void ResetFolderColor()
		{
			Selection.assetGUIDs.ToList().ForEach(
				assetGuid =>
				{
					var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
					if (AssetDatabase.IsValidFolder(assetPath))
					{
						ColourFolderManager.ResetColor(assetPath);
					}
				}
			);
		}


		/// <summary>
		/// Validates the ResetFolderColor() menu item for use.
		/// </summary>
		/// <returns>If the option can be used or not.</returns>
		[MenuItem("Assets/Color Folders/Reset Folder Color", true)]
		private static bool ResetFolderColorValidation()
		{
			if (!TryGetActiveFolderPath(out var path)) return false;
			return ColourFolderManager.HasFolder(path);
		}


		/// <summary>
		/// Forces the folder selected to update when called.
		/// </summary>
		[MenuItem("Assets/Color Folders/Force Update", priority = 4)]
		private static void ForceUpdate()
		{
			ColorFolderCache.FolderResult.Clear();
			EditorApplication.RepaintProjectWindow();
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Helper Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		private static bool TryGetActiveFolderPath( out string path )
		{
			var tryGetActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("TryGetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic );

			var args = new object[] { null };
			var found = (bool)tryGetActiveFolderPath.Invoke( null, args );
			path = (string)args[0];

			return found;
		}
	}
}

#endif