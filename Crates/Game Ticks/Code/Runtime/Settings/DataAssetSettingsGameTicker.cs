#if CARTERGAMES_CART_CRATE_GAMETICKS

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

namespace CarterGames.Cart.Crates.GameTicks
{ 
	[Serializable]
	public sealed class DataAssetSettingsGameTicker : DataAsset
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Fields
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    [SerializeField] private int gameTickTicksPerSecond = 5;
	    [SerializeField] private bool gameTickUseUnscaledTime;
	    [SerializeField] private bool gameTickUseGlobalTicker = true;
	    [SerializeField] private GameTickSyncState globalSyncState = GameTickSyncState.Custom;

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Properties
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
        /// The number of ticks per second.
        /// </summary>
        public int GameTickTicksPerSecond => gameTickTicksPerSecond;


	    /// <summary>
        /// Should the tick system be in unscaled time?
        /// </summary>
        public bool GameTickUseUnscaledTime => gameTickUseUnscaledTime;


	    /// <summary>
        /// Should the global ticker be initialized & used at runtime?
        /// </summary>
        public bool GameTickUseGlobalTicker => gameTickUseGlobalTicker;


	    /// <summary>
        /// The sync state for the global game ticker.
        /// </summary>
        public GameTickSyncState GameTickGlobalSyncState => globalSyncState;
    }
}

#endif