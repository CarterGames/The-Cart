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
    public abstract class LocalizedComponent<T> : MonoBehaviour
    {
        [SerializeField] protected bool updateOnStart;
        
        public abstract string LocId { get; protected set; }
        protected T ManuallyAssignedValue { get; set; }
        protected bool IsAssignedManually { get; set; }
        private bool IsLocIdAssigned => string.IsNullOrEmpty(LocId);
        
        
        private void OnEnable()
        {
            LocalizationManager.LanguageChanged.Add(OnLanguageChanged);
        }


        private void Start()
        {
            if (!updateOnStart) return;
            AssignLocalization();
        }


        private void OnDestroy()
        {
            LocalizationManager.LanguageChanged.Remove(OnLanguageChanged);
        }


        private void OnLanguageChanged()
        {
            Localize();
        }


        protected void AssignLocalization()
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


        protected abstract T GetValueFromId(string locId);
        protected abstract void AssignValue(T localizedValue);

        
        public void Localize()
        {
            ClearManuallySetValue();
            AssignLocalization();
        }
        
        
        public void Localize(string locId)
        {
            LocId = locId;
            
            ClearManuallySetValue();
            AssignLocalization();
        }
        
        
        public virtual void LocalizeManually(T value)
        {
            ManuallyAssignedValue = value;
            IsAssignedManually = true;
            
            AssignLocalization();
        }

        
        protected virtual void ClearManuallySetValue()
        {
            if (!IsAssignedManually) return;
            ManuallyAssignedValue = default;
            IsAssignedManually = false;
        }
    }
}

#endif