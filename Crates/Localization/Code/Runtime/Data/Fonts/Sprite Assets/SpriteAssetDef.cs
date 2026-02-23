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
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
{
    [Serializable]
    public class SpriteAssetDef
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        [SerializeField] [LanguageSelectable] private Language language;
        [SerializeField] private TMP_SpriteAsset spriteAsset;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */ 
        
        /// <summary>
        /// The language code this font is for.
        /// </summary>
        public string LanguageCode => language.Code;
        
        
        /// <summary>
        /// The font asset to use (TMP is more or less a must these days, don't use legacy).
        /// </summary>
        public TMP_SpriteAsset SpriteAsset => spriteAsset;
    }
}

#endif