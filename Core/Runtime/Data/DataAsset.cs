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
using UnityEngine;

namespace CarterGames.Cart.Core.Data
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