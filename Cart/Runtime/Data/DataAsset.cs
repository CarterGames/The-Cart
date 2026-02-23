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
using UnityEngine;

namespace CarterGames.Cart.Data
{
    /// <summary>
    /// Inherit from to define a data asset that the data system will detect and allow access at runtime.
    /// </summary>
    [Serializable]
    public abstract class DataAsset : ScriptableObject
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Defines a id for this data asset.
        /// </summary>
        [SerializeField] private string variantId = Guid.NewGuid().ToString();


        /// <summary>
        /// Defines if the asset is not added to the asset index.
        /// </summary>
        [SerializeField] protected bool excludeFromAssetIndex;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Override to define a custom variant id for the data asset.
        /// </summary>
        public virtual string VariantId => variantId;


        /// <summary>
        /// Gets if the asset is ignored from being added to the asset index system.
        /// </summary>
        public bool ExcludeFromAssetIndex => excludeFromAssetIndex;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the asset when called.
        /// </summary>
        public void Initialize()
        {
            OnInitialize();
        }
        
        
        /// <summary>
        /// Override to implement logic when the asset is initialized.
        /// </summary>
        protected virtual void OnInitialize() {}
    }
}