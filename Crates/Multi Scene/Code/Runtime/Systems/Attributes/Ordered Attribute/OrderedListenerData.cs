#if CARTERGAMES_CART_CRATE_MULTISCENE

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
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// A data class for holding ordered listeners for the multi scene manager to use
    /// </summary>
    /// <typeparam name="T">The interface type to use</typeparam>
    [Serializable]
    public sealed class OrderedListenerData<T>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private int order;
        [SerializeField] private T listener;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The order to use for this listener
        /// </summary>
        public int Order => order;
        
        /// <summary>
        /// The listener for to use
        /// </summary>
        public T Listener => listener;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Constructor | Implements the order & listener automatically
        /// </summary>
        /// <param name="order">The order to set</param>
        /// <param name="listener">The listener to set</param>
        public OrderedListenerData(int order, T listener)
        {
            this.listener = listener;
            this.order = order;
        }
    }
}

#endif