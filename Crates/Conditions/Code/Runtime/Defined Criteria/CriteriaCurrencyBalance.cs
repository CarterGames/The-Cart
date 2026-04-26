#if CARTERGAMES_CART_CRATE_CONDITIONS && CARTERGAMES_CART_CRATE_CURRENCY

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

        protected override bool Valid
        {
            get
            {
                if (!CurrencyManager.TryGetAccount(accountId, out var account)) return false;
                return account.Balance >= amount;
            }
        }

        public override string DisplayName
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
            if (!CurrencyManager.TryGetAccount(accountId, out var account)) return;
            account.Adjusted.Add(OnAccountAdjusted);
            return;

            void OnAccountAdjusted(AccountTransaction transaction)
            {
                stateChanged.Raise();
            }
        }
    }
}

#endif