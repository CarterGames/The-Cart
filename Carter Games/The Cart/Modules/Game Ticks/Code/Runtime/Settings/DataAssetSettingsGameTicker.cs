#if CARTERGAMES_CART_MODULE_GAMETICKER

/*
 * Copyright (c) 2025 Carter Games
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
using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.GameTicks
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