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

using System;
using CarterGames.Cart.Core.Logs;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    /// <summary>
    /// The base class for localizing an element of the project.
    /// </summary>
    /// <typeparam name="T">The localized type.</typeparam>
    public abstract class LocalizedComponent<T> : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] protected bool updateOnStart = true;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The loc id for the component to use.
        /// </summary>
        public abstract string LocId { get; protected set; }
        
        /// <summary>
        /// Gets if the loc id is assigned or not.
        /// </summary>
        private bool IsLocIdAssigned => string.IsNullOrEmpty(LocId);
        
        /// <summary>
        /// The manually assigned value by the user if assigned.
        /// </summary>
        protected T ManuallyAssignedValue { get; set; }
        
        /// <summary>
        /// Gets if the setup should use the manually assigned value.
        /// </summary>
        protected bool IsAssignedManually { get; set; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            LocalizationManager.LanguageChanged.Add(Localize);
        }


        private void Start()
        {
            if (!updateOnStart) return;
            AssignLocalization();
        }


        private void OnDestroy()
        {
            LocalizationManager.LanguageChanged.Remove(Localize);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Assigns the localization to the componenet when called based on the current data setup it has to work with.
        /// </summary>
        private void AssignLocalization()
        {
            if (IsAssignedManually)
            {
                AssignValue(ManuallyAssignedValue);
                return;
            }

            try
            {
                if (IsLocIdAssigned)
                {
                    CartLogger.LogWarning<LogCategoryModuleLocalization>("Loc Id not assigned, cannot localize", GetType());
                    return;
                }
                
                AssignValue(GetValueFromId(LocId));
            }
            catch (Exception e)
            {
                CartLogger.LogError<LogCategoryModuleLocalization>($"Failed to localize component with the exception {e}", GetType());
            }
        }


        /// <summary>
        /// Implement to get the localized value from the localization setup.
        /// </summary>
        /// <param name="locId">The id to look for.</param>
        /// <returns>The value found.</returns>
        protected abstract T GetValueFromId(string locId);
        
        
        /// <summary>
        /// Implement to assign the localized value to the target component.
        /// </summary>
        /// <param name="localizedValue">The localized value to assign.</param>
        protected abstract void AssignValue(T localizedValue);

        
        /// <summary>
        /// Call to update the localization on the component with the current loc id assigned.
        /// </summary>
        public void Localize()
        {
            ClearManuallySetValue();
            AssignLocalization();
        }
        
       
        /// <summary>
        /// Call to update the localization on the component with the loc id entered.
        /// </summary>
        /// <param name="locId">The loc id to use.</param>
        public void Localize(string locId)
        {
            LocId = locId;
            
            ClearManuallySetValue();
            AssignLocalization();
        }
        
        
        /// <summary>
        /// Localize the component manually to the entered value.
        /// </summary>
        /// <param name="value">The value to display.</param>
        public virtual void LocalizeManually(T value)
        {
            ManuallyAssignedValue = value;
            IsAssignedManually = true;
            
            AssignLocalization();
        }

        
        /// <summary>
        /// Clears the current manually assigned value.
        /// </summary>
        protected virtual void ClearManuallySetValue()
        {
            if (!IsAssignedManually) return;
            ManuallyAssignedValue = default;
            IsAssignedManually = false;
        }
    }
}

#endif