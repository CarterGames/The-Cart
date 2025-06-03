#if CARTERGAMES_CART_MODULE_LOCALIZATION

/*
 * Copyright (c) 2025 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.Localization
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