#if CARTERGAMES_CART_CRATE_CONDITIONS && CARTERGAMES_CART_CRATE_LOCALIZATION

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
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.Localization;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
    /// <summary>
    /// A criteria for checking the current language selected for the user.
    /// </summary>
    [Serializable]
    public sealed class CriteriaCurrentLocalizationLanguage : Criteria
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField, LanguageSelectable] private Language language;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected override bool Valid => LocalizationManager.CurrentLanguage.Code == language.Code;

        public override string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(language.Code)) return base.DisplayName;
                return $"Current language {IsString} {language.Code}";
            }
        }
        
        public override string SearchProviderGroup => "Localization";

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInitialize(Evt stateChanged)
        {
            LocalizationManager.LanguageChanged.Add(stateChanged.Raise);
        }
    }
}

#endif