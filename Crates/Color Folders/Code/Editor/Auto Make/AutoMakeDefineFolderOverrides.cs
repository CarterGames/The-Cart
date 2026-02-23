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

using System.Collections.Generic;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Reflection;

namespace CarterGames.Cart.Crates.ColourFolders.Editor
{
	/// <summary>
	/// Handles the creation of the DataAssetFolderIconOverrides asset.
	/// </summary>
	public sealed class AutoMakeDefineFolderOverrides : AutoMakeDataAssetDefineBase<DataAssetFolderIconOverrides>
	{
		public override string DataAssetFileName => "[Cart] [Colour Folders] Folder Icon Overrides.asset";
		
		public override string DataAssetPath => $"Crates/{DataAssetFileName}";


		/// <summary>
		/// Runs when the asset is created.
		/// </summary>
		public override void OnCreated()
		{
			var defaultFolders = new List<DataFolderIconOverride>()
			{
				// Defaults from project structure used from library.
				new DataFolderIconOverride("Assets/_Project/Art", "Yellow"),
				new DataFolderIconOverride("Assets/_Project/Audio", "Orange"),
				new DataFolderIconOverride("Assets/_Project/Data", "Green"),
				new DataFolderIconOverride("Assets/_Project/Scenes", "Blue"),
				new DataFolderIconOverride("Assets/_Project/Code", "Red"),
				new DataFolderIconOverride("Assets/_Project/Prefabs", "Cyan"),
			};
			
			ReflectionHelper.SetField("folderOverrides", AssetRef, defaultFolders, false);
			
			ObjectRef.Fp("excludeFromAssetIndex").boolValue = true;
			ObjectRef.ApplyModifiedProperties();
			ObjectRef.Update();
		}
	}
}

#endif