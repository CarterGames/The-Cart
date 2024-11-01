#if CARTERGAMES_CART_MODULE_COLORFOLDERS

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
using System.Reflection;
using UnityEditor;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
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