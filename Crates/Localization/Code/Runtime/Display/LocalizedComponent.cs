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
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart.Crates.Localization
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
                    CartLogger.LogWarning<LogCategoryLocalization>("Loc Id not assigned, cannot localize", GetType());
                    return;
                }
                
                AssignValue(GetValueFromId(LocId));
            }
            catch (Exception e)
            {
                CartLogger.LogError<LogCategoryLocalization>($"Failed to localize component with the exception {e}", GetType());
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