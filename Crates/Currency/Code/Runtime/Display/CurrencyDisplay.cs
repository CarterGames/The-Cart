#if CARTERGAMES_CART_CRATE_CURRENCY

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
using CarterGames.Cart.Management;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Crates.Currency
{
    /// <summary>
    /// A display class for a currency.
    /// </summary>
    public class CurrencyDisplay : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] [SelectAccount] private string accountId;
        [SerializeField] [SelectFormatter] private AssemblyClassDef formatter;
        [SerializeField] private DisplayStyleHandler displayStyle;
        [SerializeField] private TMP_Text label;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Confirms if the display is in-sync or not.
        /// </summary>
        public bool InSync => CurrencyManager.GetBalance(accountId).DoubleEquals(LastBalanceShown);


        /// <summary>
        /// The last balance shown.
        /// </summary>
        private double LastBalanceShown { get; set; }


        private Formatter Formatter => formatter.GetTypeInstance<Formatter>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnEnable()
        {
            CurrencyManager.AccountBalanceChanged.Add(UpdateDisplay);
            displayStyle.DisplayedValue.Add(UpdateDisplayManually);
            
            UpdateDisplayManually(CurrencyManager.GetAccount(accountId).Balance);
        }


        private void OnDestroy()
        {
            CurrencyManager.AccountBalanceChanged.Remove(UpdateDisplay);
            displayStyle.DisplayedValue.Remove(UpdateDisplayManually);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Updates the display when called.
        /// </summary>
        /// <param name="account">The account to read.</param>
        private void UpdateDisplay(CurrencyAccount account, AccountTransaction transaction)
        {
            if (!account.Id.Equals(accountId)) return;
            displayStyle.ProcessDisplayEffect(this, transaction);
        }


        /// <summary>
        /// Updates the display to a manual value.
        /// </summary>
        /// <param name="valueToDisplay">The value to display.</param>
        public void UpdateDisplayManually(double valueToDisplay)
        {
            label.SetText(Formatter.Format(valueToDisplay));
        }


        /// <summary>
        /// Forces the display to update if possible.
        /// </summary>
        public void ForceUpdateDisplay()
        {
            if (CurrencyManager.AccountExists(accountId)) return;
            label.SetText(Formatter.Format(CurrencyManager.GetAccount(accountId).Balance));
        }
    }
}

#endif