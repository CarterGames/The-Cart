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

namespace CarterGames.Cart.Crates
{
    /// <summary>
    /// The definition for the clamped values' crate.
    /// </summary>
    public sealed class CrateLoopingValues : Crate
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The name of the crate.
        /// </summary>
        public override string CrateName => "Looping Values";
        
        
        /// <summary>
        /// A description of what the crate does.
        /// </summary>
        public override string CrateDescription => "A wrapper system that lets you have values that looping within a range when exceeding it. Useful for menu's etc.";

        
        /// <summary>
        /// The author of the crate.
        /// </summary>
        public override string CrateAuthor => CrateConstants.CarterGamesAuthor;
        

        /// <summary>
        /// The scripting define the crate uses.
        /// </summary>
        public override string CrateDefine => "CARTERGAMES_CART_CRATE_LOOPINGVALUES";
    }
}