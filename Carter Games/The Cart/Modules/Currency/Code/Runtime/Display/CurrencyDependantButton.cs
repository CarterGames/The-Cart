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
using CarterGames.Cart.Modules.Currency.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Modules.Currency
{
    /// <summary>
    /// Implement to make a currency dependant button.
    /// </summary>
    public abstract class CurrencyDependantButton : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField, SelectAccount] private string accountId;
        [SerializeField] private bool setAmount;
        [SerializeField] private double amount;
        [SerializeField] private CurrencyTransactionType transactionType = CurrencyTransactionType.Debit;

        [SerializeField] protected Button button;
        [SerializeField] protected Image buttonGraphic;
        [SerializeField] protected TMP_Text buttonLabel;

        [SerializeField] private Color affordableLabelColor = Color.white;
        [SerializeField] private Color unaffordableLabelColor = Color.grey;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Defines if the component has been setup.
        /// </summary>
        protected bool IsSetup { get; set; }


        /// <summary>
        /// The amount to adjust by.
        /// </summary>
        protected double PriceForPurchase { get; set; }


        /// <summary>
        /// The action type to apply when processing the amount.
        /// </summary>
        protected CurrencyTransactionType ActionType { get; set; }


        /// <summary>
        /// Gets if the user can afford to use the button.
        /// </summary>
        protected bool CanAfford => IsSetup && (ActionType == CurrencyTransactionType.Debit && CurrencyManager.GetBalance(accountId) >= PriceForPurchase);

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnEnable()
        {
            button.onClick.RemoveListener(InternalOnButtonPressed);
            button.onClick.AddListener(InternalOnButtonPressed);
            
            button.onClick.RemoveListener(OnButtonPressed);
            button.onClick.AddListener(OnButtonPressed);

            if (!setAmount) return;
            PriceForPurchase = amount;
            ActionType = transactionType;
            UpdateDisplay();
            
            if (IsSetup) return;
            IsSetup = true;
        }


        private void OnDestroy()
        {
            button.onClick.RemoveListener(InternalOnButtonPressed);
            button.onClick.RemoveListener(OnButtonPressed);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Sets up the button for use.
        /// </summary>
        /// <param name="value">The value to show.</param>
        /// <param name="currencyTransactionType">The transaction type to use (Def: Debit)</param>
        public void SetAmount(double value, CurrencyTransactionType currencyTransactionType = CurrencyTransactionType.Debit)
        {
            if (setAmount) return;
            
            PriceForPurchase = value;
            UpdateDisplay();

            if (IsSetup) return;
            IsSetup = true;
        }


        /// <summary>
        /// Updates the display when called.
        /// </summary>
        protected void UpdateDisplay()
        {
            button.interactable = CanAfford;
            buttonLabel.SetText(PriceForPurchase.Format<MoneyFormatterGeneric>());
        }


        /// <summary>
        /// Internal method to the class, Runs on button press to update the account.
        /// </summary>
        private void InternalOnButtonPressed()
        {
            if (!CurrencyManager.TryGetAccount(accountId, out var account)) return;
            account.Process(PriceForPurchase, ActionType);
        }


        /// <summary>
        /// Implement to run logic on the button pressed.
        /// </summary>
        protected abstract void OnButtonPressed();
    }
}

#endif