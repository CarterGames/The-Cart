#if CARTERGAMES_CART_CRATE_CURRENCY && UNITY_EDITOR

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

namespace CarterGames.Cart.Crates.Currency.Editor
{
    public class AutoMakeDefineDefaultAccounts : AutoMakeDataAssetDefineBase<DataAssetDefaultAccounts>
    {
	    public override string DataAssetFileName => "[Cart] [Currency] Default Accounts Data Asset.asset";
	    
	    
	    public override string DataAssetPath => $"Crates/{DataAssetFileName}";

	    
	    public override void OnCreated()
	    {
		    ReflectionHelper.SetField("defaultAccounts", AssetRef,
			    new List<SerializableKeyValuePair<string, double>>(), false, false);
	    }
    }
}

#endif