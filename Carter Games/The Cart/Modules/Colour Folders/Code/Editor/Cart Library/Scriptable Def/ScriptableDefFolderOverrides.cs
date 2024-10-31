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

using System;
using CarterGames.Cart.Core.Management.Editor;
using CarterGames.Cart.Modules.Settings;
using UnityEditor;

namespace CarterGames.Cart.Modules.ColourFolders.Editor
{
	/// <summary>
	/// Handles the creation of the DataAssetFolderIconOverrides asset.
	/// </summary>
	public sealed class ScriptableDefFolderOverrides : IScriptableAssetDef<DataAssetFolderIconOverrides>
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static DataAssetFolderIconOverrides cache;
		private static SerializedObject objCache;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IScriptableAssetDef Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public Type AssetType => typeof(DataAssetFolderIconOverrides);
		public string DataAssetFileName => "[Cart] [Colour Folders] Folder Icon Overrides.asset";
		public string DataAssetFilter => $"t:{typeof(DataAssetFolderIconOverrides).FullName}";
		public string DataAssetPath => $"{ScriptableRef.FullPathData}/Modules/{DataAssetFileName}";
		
		
		public DataAssetFolderIconOverrides AssetRef => ScriptableRef.GetOrCreateAsset(this, ref cache);
		public SerializedObject ObjectRef => ScriptableRef.GetOrCreateAssetObject(this, ref objCache);
		
		
		public void TryCreate()
		{
			ScriptableRef.GetOrCreateAsset(this, ref cache);
		}
	}
}

#endif