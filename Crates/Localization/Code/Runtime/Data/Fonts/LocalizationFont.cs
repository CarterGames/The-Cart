#if CARTERGAMES_CART_CRATE_LOCALIZATION

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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
    /// <summary>
    /// Defines a font that localization can use.
    /// </summary>
    [Serializable]
    public struct LocalizationFont
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string displayName;
        [SerializeField] private LocalizationFontDefinition[] data;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets if the default setup has been assigned.
        /// </summary>
        public bool HasDefaultSetup => data.Any(t => t.Language.Code == LocalizationManager.CurrentLanguage.Code);
		
        
        /// <summary>
        /// Gets the default setup.
        /// </summary>
        public LocalizationFontDefinition DefaultLanguageData =>
            data.FirstOrDefault(t => t.Language.Code == Language.Default.Code);
        
        
        /// <summary>
        /// Gets the data to read from.
        /// </summary>
        public IReadOnlyCollection<LocalizationFontDefinition> Data => data;
    }
}

#endif