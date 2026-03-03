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

namespace CarterGames.Cart.Crates
{
    /// <summary>
    /// The definition for the currency crate.
    /// </summary>
    public sealed class CrateCurrency : Crate
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The name of the crate.
        /// </summary>
        public override string CrateName => "Currency";
        
        
        /// <summary>
        /// A description of what the crate does.
        /// </summary>
        public override string CrateDescription => "A system for storing currency values for players, such as gold, gems etc.";

        
        /// <summary>
        /// The author of the crate
        /// </summary>
        public override string CrateAuthor => CrateConstants.CarterGamesAuthor;


        /// <summary>
        /// The scripting define the crate uses.
        /// </summary>
        public override string CrateDefine => "CARTERGAMES_CART_CRATE_CURRENCY";
    }
}