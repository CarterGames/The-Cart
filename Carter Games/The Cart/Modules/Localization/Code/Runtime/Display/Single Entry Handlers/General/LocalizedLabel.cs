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
using CarterGames.Cart.Core;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    [Serializable]
    public class LocalizedLabel : LocalizedElement
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private DataAssetLocalizationFont font;
        [SerializeField] private TMP_Text label;
        [SerializeField] private bool usesSpriteAsset;
        [SerializeField] private DataAssetLocalizationTmpSpriteAsset spriteAsset;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public GameObject LabelObject => label.gameObject;
        public TMP_Text Label => label;
        private bool IsValid => font != null && label != null;
        private string LastCopySet { get; set; }
        protected object[] FormattingParameters { get; set; } = null;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void SetupFont()
        {
            font.ApplyToComponent(label);
        }


        public override void Update(string locId)
        {
            Update(locId, null);
        }

        
        public void Update(string locId, params object[] formatParams)
        {
            FormattingParameters = formatParams;
            SetLocId(locId);
            PreReq.DisallowIfFalse(IsValid);
            UpdateLocalization(LocalizationManager.GetText(LocId));
        }
        
        
        public void UpdateParamsOnly(params object[] formatParams)
        {
            FormattingParameters = formatParams;
            PreReq.DisallowIfFalse(IsValid);
            UpdateLocalization(LastCopySet, false);
        }
        
        
        public void ForceSetCopy(string copy, params object[] formatParams)
        {
            FormattingParameters = formatParams;
            UpdateLocalization(copy, false);
        }
        

        private void UpdateLocalization(string copy, bool requiresLocId = true)
        {
            TrySubscribeToLanguageChange();
            
            PreReq.DisallowIfFalse(IsValid);
            PreReq.DisallowIfFalse(requiresLocId && !IsLocIdValid);
            
            font.ApplyToComponent(label);
            spriteAsset?.ApplyToComponent(label);

            LastCopySet = copy;
            
            if (FormattingParameters != null)
            {
                label.SetText(FormattingParameters.Length <= 0 
                    ? copy 
                    : string.Format(copy, FormattingParameters));
            }
            else
            {
                label.SetText(copy);
            }
        }
        
        
        protected override void RefreshLocalization()
        {
            if (IsLocIdValid)
            {
                Update(LocId, FormattingParameters);
            }
            else
            {
                UpdateParamsOnly(FormattingParameters);
            }
        }
    }
}

#endif