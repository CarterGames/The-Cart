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

using CarterGames.Cart;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Crates.Localization
{
    /// <summary>
    /// A simple component for a localized UI Image element.
    /// </summary>
    public class LocalizedUIImage : LocalizedComponent<Sprite>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [LocalizationId] private string locId;
        [SerializeField] private Image image;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// A reference to the component, assumed on same object or child if auto-finding.
        /// </summary>
        private Image ImageRef => CacheRef.GetOrAssign(ref image, GetComponentInChildren<Image>);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The loc id to use.
        /// </summary>
        public override string LocId
        {
            get => locId;
            protected set => locId = value;
        }
        

        /// <summary>
        /// Gets the sprite from the loc id.
        /// </summary>
        /// <param name="requestLocId">The id to use.</param>
        /// <returns>The sprite for the loc id.</returns>
        protected override Sprite GetValueFromId(string requestLocId)
        {
            return LocalizationManager.GetSprite(requestLocId);
        }
        

        /// <summary>
        /// Assigns to the sprite to the image.
        /// </summary>
        /// <param name="localizedValue">The sprite to assign.</param>
        protected override void AssignValue(Sprite localizedValue)
        {
            ImageRef.sprite = localizedValue;
        }
    }
}

#endif