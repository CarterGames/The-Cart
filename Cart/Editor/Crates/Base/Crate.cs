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
        public string CrateTechnicalName
        {
            get
            {
                if (!IsSubCrate) return $"crate.{CrateAuthor.TrimSpaces().ToLower()}.{CrateName.TrimSpaces().ToLower()}";
                return $"crate.{CrateAuthor.TrimSpaces().ToLower()}.{ParentCrate.CrateName.TrimSpaces().ToLower()}.{CrateName.TrimSpaces().ToLower()}";
            }
        }


        /// <summary>
        /// The scripting define for the crate.
        /// </summary>
        public string CrateDefine
        {
            get
            {
                if (!IsSubCrate) return $"{CrateAuthor.TrimSpaces().ToUpper()}_CART_CRATE_{CrateName.TrimSpaces().ToUpper()}";
                return $"{CrateAuthor.TrimSpaces().ToUpper()}_CART_CRATE_{ParentCrate.CrateName.TrimSpaces().ToUpper()}_{CrateName.TrimSpaces().ToUpper()}";
            }
        }


        /// <summary>
        /// Any crates that are required for the crate to work.
        /// </summary>
        public virtual string[] PreRequisites => Array.Empty<string>();


        /// <summary>
        /// Any crates that are required for the crate to work.
        /// </summary>
        public virtual string[] OptionalPreRequisites => Array.Empty<string>();


        /// <summary>
        /// The URL for the crate's documentation.
        /// </summary>
        public virtual EditorUrl[] CrateLinks => Array.Empty<EditorUrl>();


        /// <summary>
        /// Gets if the crate is a sub crate or not.
        /// </summary>
        public virtual bool IsSubCrate => ParentCrate != null;
        
        
        /// <summary>
        /// Gets the parent crate is applicable.
        /// </summary>
        /// <remarks>Only applies if this crate is a sub crate.</remarks>
        public virtual Crate ParentCrate { get; private set; } = null;
    }
}