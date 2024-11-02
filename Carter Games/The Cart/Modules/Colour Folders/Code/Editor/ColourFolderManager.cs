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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
{
	/// <summary>
	/// Handles the management of coloured folders in the project.
	/// </summary>
	[InitializeOnLoad]
	public sealed class ColourFolderManager
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public static AssetFolderIcons folderIconsAsset;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		static ColourFolderManager()
		{
			ColorFolderCache.FolderResult.Clear();
			
			EditorApplication.projectWindowItemOnGUI -= ReplaceFolderIcon;
			EditorApplication.projectWindowItemOnGUI += ReplaceFolderIcon;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Folder Drawing Helper Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Updates the folders when called.
		/// </summary>
		/// <param name="guid">The folder guid to edit.</param>
		/// <param name="rect">The rect for the folder.</param>
		private static void ReplaceFolderIcon(string guid, Rect rect)
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			
			if (!AssetDatabase.IsValidFolder(path)) return;
			if (AssetDatabase.IsOpenForEdit(path)) return;

			var isSmall = IsIconSmall(ref rect);
			var texture = GetFolderIcon(path, isSmall);
			
			if (texture == null) return;
			
			DrawFolderIcon(rect, texture);
		}

		
		/// <summary>
		/// Draws the folder icon when called.
		/// </summary>
		/// <param name="rect">The rect to apply to.</param>
		/// <param name="texture">The texture to draw.</param>
		private static void DrawFolderIcon(Rect rect, Texture texture)
		{
			var isTreeView = rect.width > rect.height;
			var isSideView = IsSideView(rect);
			
			
			// Vertical Folder View
			if (isTreeView)
			{
				rect.width = rect.height = 16f;
				
				if (!isSideView)
				{
					rect.x += 0f;
					rect.width += 1f;
				}
			}
			else
			{
				rect.height -= 2f;
				rect.y += 1f;
			}
			
			GUI.DrawTexture(rect, texture, ScaleMode.ScaleAndCrop);
		}


		/// <summary>
		/// Gets is the folder is in a side view.
		/// </summary>
		/// <param name="rect">The rect to check</param>
		/// <returns>If the folder is in a side view.</returns>
		private static bool IsSideView(Rect rect)
		{
#if UNITY_2019_3_OR_NEWER
			return rect.x != 14;
#else
            return rect.x != 13;
#endif
		}
		
		
		/// <summary>
		/// Gets if the folder icon is small.
		/// </summary>
		/// <param name="rect">The rect to check.</param>
		/// <returns>If the folder icon is small.</returns>
		private static bool IsIconSmall(ref Rect rect)
		{
			var isSmall = rect.width > rect.height;

			if (isSmall)
			{
				rect.width = rect.height;
			}
			else
			{
				rect.height = rect.width;
			}

			return isSmall;
		}
		

		/// <summary>
		/// Gets the folder icon, or default if none found.
		/// </summary>
		/// <param name="folderPath">The folder path to get.</param>
		/// <param name="small">Is the icon small?</param>
		/// <returns>The icon to apply.</returns>
		private static Texture2D GetFolderIcon(string folderPath, bool small = true)
		{
			if (folderIconsAsset == null)
			{
				folderIconsAsset =
					(AssetFolderIcons) FileEditorUtil.GetAssetInstance<AssetFolderIcons>(
						"t:AssetFolderIcons");
			}

			if (folderIconsAsset == null) return null;

			if (HasOverrideIconSet(folderPath, out var setId))
			{
				var setData = ColorFolderCache.SetsLookup[setId];
				
				if (setData != null)
				{
					return small ? setData.Small : setData.Large;
				}
			}

			return null;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Folder Override Helper Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

		/// <summary>
		/// Tries to get a folder override definition.
		/// </summary>
		/// <param name="path">The folder path to check.</param>
		/// <param name="recursive">If the check should be recursive.</param>
		/// <param name="returnOnFound">If it should return the first valid folder found, ignoring other possible valid overrides.</param>
		/// <param name="folderEntry">The entry found.</param>
		/// <returns>If it was successful.</returns>
		private static bool TryGetFolder(string path, bool recursive, bool returnOnFound, out string setId)
		{
			if (ColorFolderCache.FolderResult.ContainsKey(path))
			{
				setId = ColorFolderCache.FolderResult[path]?.Id;
				return !string.IsNullOrEmpty(setId);
			}
			
			var data = ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().ObjectRef.Fp("folderOverrides");
			setId = null;
			
			for (var i = 0; i < data.arraySize; i++)
			{
				var entry = data.GetIndex(i);
				
				if (!recursive)
				{
					if (entry.Fpr("folderPath").stringValue != path) continue;
					setId = entry.Fpr("folderSetId").stringValue;
					
					if (setId != null)
					{
						if (entry.Fpr("folderPath").stringValue.Length <= setId.Length) continue;
					}
					
					if (returnOnFound && setId != null)
					{
						ColorFolderCache.AddFolderResult(path, ColorFolderCache.SetsLookup[setId]);
						return true;
					}
				}
				else
				{
					if (entry.Fpr("folderPath").stringValue != path)
					{
						if (!path.Contains(entry.Fpr("folderPath").stringValue)) continue;
						if (!entry.Fpr("isRecursive").boolValue) continue;
						
						if (setId != null)
						{
							if (entry.Fpr("folderPath").stringValue.Length <= setId.Length) continue;
						}
						
						setId = entry.Fpr("folderSetId").stringValue;
						
						if (returnOnFound)
						{
							ColorFolderCache.AddFolderResult(path, ColorFolderCache.SetsLookup[setId]);
							return true;
						}
					}
		
					setId = entry.Fpr("folderSetId").stringValue;
					
					if (returnOnFound)
					{
						ColorFolderCache.AddFolderResult(path, ColorFolderCache.SetsLookup[setId]);
						return true;
					}
				}
			}
			
			ColorFolderCache.AddFolderResult(path, setId != null ? ColorFolderCache.SetsLookup[setId] : null);
			return !string.IsNullOrEmpty(setId);
		}
		
		
		/// <summary>
		/// Gets if a folder path has an icon set override.
		/// </summary>
		/// <param name="folderPath">The path to check.</param>
		/// <param name="setId">The id found if found.</param>
		/// <returns>If it was successful.</returns>
		private static bool HasOverrideIconSet(string folderPath, out string setId)
		{
			if (TryGetFolder(folderPath, true, false, out var entry))
			{
				setId = entry;
				return setId != null;
			}

			setId = null;
			return false;
		}
		

		/// <summary>
		/// Gets if a folder is defined in the overrides.
		/// </summary>
		/// <param name="path">The folder path to check.</param>
		/// <param name="checkRecursive">Should the check be recursive?</param>
		/// <returns>If the folder exists in the overrides.</returns>
		public static bool HasFolder(string path, bool checkRecursive = false)
		{
			return TryGetFolder(path, checkRecursive, true, out _);
		}


		/// <summary>
		/// Resets a folder colour override if it exists.
		/// </summary>
		/// <param name="path">The folder path to reset.</param>
		public static void ResetColor(string path)
		{
			foreach (var assetGuid in Selection.assetGUIDs.ToList())
			{
				var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);

				if (!AssetDatabase.IsValidFolder(assetPath)) continue;
				
				Undo.RecordObject(ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().AssetRef,
					"Folder color reset.");

				ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().AssetRef.FolderOverrides.Remove(
					ScriptableRef
						.GetAssetDef<DataAssetFolderIconOverrides>().AssetRef.FolderOverrides
						.First(t => t.FolderPath == path));
				
				EditorApplication.RepaintProjectWindow();
			}
			
			EditorUtility.SetDirty(ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().AssetRef);
			ScriptableRef.GetAssetDef<DataAssetFolderIconOverrides>().ObjectRef.Update();
			
			ColorFolderCache.FolderResult.Clear();
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}


		public static DataFolderIconSet CurrentlyAppliedSet(string folderPath)
		{
			if (!HasOverrideIconSet(folderPath, out var setId)) return null;
			return ColorFolderCache.SetsLookup[setId];
		}
	}
}

#endif