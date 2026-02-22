#if CARTERGAMES_CART_CRATE_CONDITIONS && CARTERGAMES_CART_CRATE_CURRENCY

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using CarterGames.Cart;
using CarterGames.Cart.Events;
using CarterGames.Cart.Crates.Currency;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
    /// <summary>
    /// A criteria for checking the balance of an account in the currency crate.
    /// </summary>
    [Serializable]
    public sealed class CriteriaCurrencyBalance : Criteria
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [SelectAccount] private string accountId;
        [SerializeField] private NumericalComparisonType comparisonType;
        [SerializeField] private int amount;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected override bool Valid => CurrencyManager.GetAccount(accountId).Balance >= amount;

        protected override string DisplayName
        {
            get
            {
                switch (comparisonType)
                {
                    case NumericalComparisonType.LessThan:
                        return $"{accountId} balance {IsString} <= {amount}"; 
                    case NumericalComparisonType.LessThanOrEqual:
                        return $"{accountId} balance {IsString} < {amount}"; 
                    case NumericalComparisonType.Equals:
                        return $"{accountId} balance {IsString} exactly {amount}"; 
                    case NumericalComparisonType.GreaterThanOrEqual:
                        return $"{accountId} balance {IsString} >= {amount}"; 
                    case NumericalComparisonType.GreaterThan:
                        return $"{accountId} balance {IsString} > {amount}"; 
                    default:
                    case NumericalComparisonType.Unassigned:
                        return base.DisplayName;
                }
            }
        }

        public override string SearchProviderGroup => "Currency";
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public override void OnInitialize(Evt stateChanged)
        {
            CurrencyManager.GetAccount(accountId).Adjusted.Add(OnAccountAdjusted);
            return;

            void OnAccountAdjusted(AccountTransaction transaction)
            {
                stateChanged.Raise();
            }
        }
    }
}

#endif