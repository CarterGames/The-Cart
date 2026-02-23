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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Cart.Crates.Currency
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
            PreReq.DisallowIfNull(button);
            
            button.onClick.RemoveListener(InternalOnButtonPressed);
            button.onClick.AddListener(InternalOnButtonPressed);
            
            button.onClick.RemoveListener(OnButtonPressed);
            button.onClick.AddListener(OnButtonPressed);

            if (!setAmount) return;
            PriceForPurchase = amount;
            ActionType = transactionType;
            UpdateDisplay();
            
            CurrencyManager.AccountsLoaded.Add(UpdateDisplay);
            CurrencyManager.GetAccount(accountId).Adjusted.Add(OnAccountAdjusted);
            
            if (IsSetup) return;
            IsSetup = true;
        }


        private void OnDestroy()
        {
            CurrencyManager.AccountsLoaded.Remove(UpdateDisplay);
            CurrencyManager.GetAccount(accountId).Adjusted.Remove(OnAccountAdjusted);
            
            if (button == null) return;
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
            ActionType = currencyTransactionType;
            
            CurrencyManager.AccountsLoaded.Add(UpdateDisplay);
            CurrencyManager.GetAccount(accountId).Adjusted.Add(OnAccountAdjusted);
            
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
        /// Updates the display when called based on evt's
        /// </summary>
        private void OnAccountAdjusted(AccountTransaction transaction)
        {
            UpdateDisplay();
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