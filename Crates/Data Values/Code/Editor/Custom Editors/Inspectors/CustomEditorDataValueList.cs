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

using CarterGames.Cart.Data.Editor;
using UnityEditor;

namespace CarterGames.Cart.Crates.DataValues.Editor
{
	[CustomEditor(typeof(DataValueList<>), true)]
	public sealed class DataValueListEditor : InspectorDataAsset
	{
		protected override bool ShowAssetIndexOptions => false;
		protected override bool ShowVariantIdOption => false;
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			CustomEditorDataValueDefault.DrawInspector(serializedObject);
		}
	}
}

#endif