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

namespace CarterGames.Cart.Data.Editor
{
	/// <summary>
	/// Defines the data asset index in the library.
	/// </summary>
	public sealed class AutoMakeDefineAssetIndex : AutoMakeDataAssetDefineBase<DataAssetIndex>
	{
		public override AutoMakePathType PathType => AutoMakePathType.Resources;
		

		/// <summary>
		/// The name for the file.
		/// </summary>
		public override string DataAssetFileName => "[Cart] Data Asset Index.asset";

		
		/// <summary>
		/// Runs when the asset is created.
		/// </summary>
		public override void OnCreated()
		{
			DataAssetIndexHandler.UpdateIndex();
		}
	}
}