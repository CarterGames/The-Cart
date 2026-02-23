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
using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates
{
    /// <summary>
    /// An class to define a crate
    /// </summary>
    public abstract class Crate
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The name of the crate.
        /// </summary>
        public abstract string CrateName { get; } 
        
        
        /// <summary>
        /// A description of what the crate does.
        /// </summary>
        public abstract string CrateDescription { get; }
        
        
        /// <summary>
        /// The author of the crate.
        /// </summary>
        public abstract string CrateAuthor { get; }


        /// <summary>
        /// The technical name for the crate itself.
        /// </summary>
        public string CrateTechnicalName => $"crate.{CrateAuthor.TrimSpaces().ToLower()}.{CrateName.TrimSpaces().ToLower()}";
        
        
        /// <summary>
        /// The scripting define for the crate.
        /// </summary>
        public abstract string CrateDefine { get; }


        /// <summary>
        /// Any crates that are required for the crate to work.
        /// </summary>
        public virtual Crate[] PreRequisites => Array.Empty<Crate>();
        
        
        /// <summary>
        /// Any crates that are required for the crate to work.
        /// </summary>
        public virtual Crate[] OptionalPreRequisites => Array.Empty<Crate>();


        /// <summary>
        /// The URL for the crate's documentation.
        /// </summary>
        public virtual EditorUrl[] CrateLinks => Array.Empty<EditorUrl>();
    }
}