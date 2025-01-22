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
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;

namespace CarterGames.Cart.Modules.Currency
{
    /// <summary>
    /// A class defining an account in the currency system.
    /// </summary>
    public sealed class CurrencyAccount
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private readonly object padlock = new object();
        private double balance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The balance of the account.
        /// </summary>
        public double Balance
        {
            get
            {
                lock (padlock)
                {
                    return balance;
                }
            }
        }


        /// <summary>
        /// The balance of the account formatted with the generic formatter.
        /// </summary>
        public string BalanceFormatted => Balance.Format<MoneyFormatterGeneric>();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the account is adjusted in any way.
        /// </summary>
        public readonly Evt Adjusted = new Evt();


        /// <summary>
        /// Raises when the account is credited to.
        /// </summary>
        public readonly Evt Credited = new Evt();
        
        /// <summary>
        /// Raises when the account is debited to.
        /// </summary>
        public readonly Evt Debited = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Makes a new account with the initial balance entered.
        /// </summary>
        /// <param name="initialBalance">The balance to start on.</param>
        public CurrencyAccount(double initialBalance = 0)
        {
            balance = initialBalance;
            balance.Round();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Debit (remove) from the account.
        /// </summary>
        /// <param name="amount">The amount to remove.</param>
        public void Debit(double amount)
        {
            if (amount < 0)
            {
                CartLogger.Log<LogCategoryModules>("Cannot debit less than or equal to 0.", typeof(CurrencyAccount));
                return;
            }

            lock (padlock)
            {
                if (balance < amount.Round()) return;

                balance -= amount.Round();
                balance.Round();
                
                Debited.Raise();
                Adjusted.Raise();
            }
        }


        /// <summary>
        /// Credit (add) to the account.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                CartLogger.Log<LogCategoryModules>("Cannot credit less than or equal to 0.", typeof(CurrencyAccount));
                return;
            }

            lock (padlock)
            {
                balance += amount.Round();
                balance.Round();
                
                Credited.Raise();
                Adjusted.Raise();
            }
        }


        /// <summary>
        /// Processes the change either way.
        /// </summary>
        /// <param name="amount">The amount to adjust by.</param>
        /// <param name="transactionType">The method to adjust by.</param>
        public void Process(double amount, CurrencyTransactionType transactionType)
        {
            switch (transactionType)
            {
                case CurrencyTransactionType.Credit:
                    Credit(amount);
                    break;
                case CurrencyTransactionType.Debit:
                    Debit(amount);
                    break;
                case CurrencyTransactionType.Clear:
                default:
                    break;
            }
        }
    }
}

#endif