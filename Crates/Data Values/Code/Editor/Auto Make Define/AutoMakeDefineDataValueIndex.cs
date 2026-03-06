#if CARTERGAMES_CART_CRATE_DATAVALUES && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.DataValues.Editor.Definitions
{
	public sealed class AutoMakeDefineDataValueIndex : AutoMakeDataAssetDefineBase<DataValueIndex>
	{
		public override string DataAssetFileName => "[Cart] [Data Values] Data Value Index.asset";

		public override string DataAssetPath => $"Crates/{DataAssetFileName}";


	}
}

#endif