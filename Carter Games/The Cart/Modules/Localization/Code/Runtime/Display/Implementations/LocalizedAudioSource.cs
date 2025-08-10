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

namespace CarterGames.Cart.Modules.Localization
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