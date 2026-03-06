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

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// Attribute | When applied to an Multi Scene interface, the method will be called in the execution order defined. 
    /// </summary>
    /// <remarks>
    /// The order attribute only works if the method it is on is a Multi Scene Interface Implementation, other methods will be ignored by the system at present.
    /// If the interface implementation has no order it will be set to 0 as it is the default, just like in the scripting execution order system in Unity. 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MultiSceneOrderedAttribute : Attribute
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public int order;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// A default ordered attribute.
        /// </summary>
        public MultiSceneOrderedAttribute()
        {
            order = 0;
        }
        
        
        /// <summary>
        /// A attribute with a defined order.
        /// </summary>
        public MultiSceneOrderedAttribute(int order = 0)
        {
            this.order = order;
        }
    }
}

#endif