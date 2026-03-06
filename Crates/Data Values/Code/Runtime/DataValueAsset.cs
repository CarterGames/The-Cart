#if CARTERGAMES_CART_CRATE_DATAVALUES

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

using System;
using CarterGames.Cart.Data;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues
{
	/// <summary>
	/// A base class for all data values to inherit from.
	/// </summary>
	[Serializable]
    public abstract class DataValueAsset : DataAsset, IDataValueReset
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Properties
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
	    /// Defines the key for the asset.
	    /// </summary>
	    public abstract string Key { get; }


	    /// <summary>
	    /// Defines the states that the asset can reset from.
	    /// </summary>
        public virtual DataValueResetState ValidStates { get; }

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Methods
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
        /// Resets the asset when called.
        /// </summary>
        public abstract void ResetAsset();
    }
}

#endif