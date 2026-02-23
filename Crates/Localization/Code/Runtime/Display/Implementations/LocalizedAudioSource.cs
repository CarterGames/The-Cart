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

namespace CarterGames.Cart.Crates.Localization
{
    /// <summary>
    /// A simple component for a localized AudioSource element.
    /// </summary>
    public class LocalizedAudioSource : LocalizedComponent<AudioClip>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [LocalizationId] private string locId;
        [SerializeField] private AudioSource source;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// A reference to the component, assumed on same object or child if auto-finding.
        /// </summary>
        private AudioSource AudioRef => CacheRef.GetOrAssign(ref source, GetComponentInChildren<AudioSource>);

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
        /// Gets the audio from the loc id.
        /// </summary>
        /// <param name="requestLocId">The id to use.</param>
        /// <returns>The audio for the loc id.</returns>
        protected override AudioClip GetValueFromId(string requestLocId)
        {
            return LocalizationManager.GetAudio(requestLocId);
        }
        

        /// <summary>
        /// Assigns to the audio to the source.
        /// </summary>
        /// <param name="localizedValue">The clip to assign.</param>
        protected override void AssignValue(AudioClip localizedValue)
        {
            AudioRef.clip = localizedValue;
        }
    }
}

#endif