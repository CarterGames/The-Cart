#if CARTERGAMES_CART_MODULE_CURRENCY

/*
 * Copyright (c) 2024 Carter Games
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
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency
{
    /// <summary>
    /// A display class for a currency.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Modules/Currency/Currency Display Component")]
    public class CurrencyDisplay : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private string accountId;
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

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnEnable()
        {
            CurrencyManager.AccountBalanceChanged.Add(UpdateDisplay);
            UpdateDisplay(CurrencyManager.GetAccount(accountId));
        }


        private void OnDestroy()
        {
            CurrencyManager.AccountBalanceChanged.Remove(UpdateDisplay);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Updates the display when called.
        /// </summary>
        /// <param name="account">The account to read.</param>
        private void UpdateDisplay(CurrencyAccount account)
        {
            label.SetText(account.Balance.Format<MoneyFormatterGeneric>());
        }


        /// <summary>
        /// Updates the display to a manual value.
        /// </summary>
        /// <param name="valueToDisplay">The value to display.</param>
        public void UpdateDisplayManually(double valueToDisplay)
        {
            label.SetText(valueToDisplay.Format<MoneyFormatterGeneric>());
        }


        /// <summary>
        /// Forces the display to update if possible.
        /// </summary>
        public void ForceUpdateDisplay()
        {
            if (CurrencyManager.AccountExists(accountId)) return;
            label.SetText(CurrencyManager.GetAccount(accountId).Balance.Format<MoneyFormatterGeneric>());
        }
    }
}

#endif