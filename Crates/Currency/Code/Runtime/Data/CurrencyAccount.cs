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
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;

namespace CarterGames.Cart.Crates.Currency
{
    /// <summary>
    /// A class defining an account in the currency system.
    /// </summary>
    public sealed class CurrencyAccount
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private string id;
        private readonly object padlock = new object();
        private double balance;
        private double startingBalance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// The Id of the account.
        /// </summary>
        public string Id => id;
        
        
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

        
        /// <summary>
        /// The starting balance of the account.
        /// </summary>
        public double StartingBalance => startingBalance;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the account is adjusted in any way.
        /// </summary>
        public readonly Evt<AccountTransaction> Adjusted = new Evt<AccountTransaction>();


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

        private CurrencyAccount() { }
        
        
        /// <summary>
        /// Makes a new account with the initial balance entered.
        /// </summary>
        /// <param name="initialBalance">The balance to start on.</param>
        public static CurrencyAccount NewAccount(string id, double initialBalance = 0)
        {
            return new CurrencyAccount()
            {
                id = id,
                startingBalance = initialBalance.Round(),
                balance = initialBalance.Round()
            };
        }
        
        
        /// <summary>
        /// Makes a new account with the initial balance entered.
        /// </summary>
        /// <param name="currentBalance">The current balance of the account.</param>
        /// <param name="initialBalance">The balance to start on.</param>
        public static CurrencyAccount AccountWithBalance(string id, double currentBalance, double initialBalance = 0)
        {
            return new CurrencyAccount()
            {
                id = id,
                startingBalance = initialBalance.Round(),
                balance = currentBalance.Round()
            };
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
                CartLogger.Log<LogCategoryCurrency>("Cannot debit less than or equal to 0.", typeof(CurrencyAccount));
                return;
            }

            lock (padlock)
            {
                if (balance < amount.Round()) return;

                var starting = balance;
                
                balance -= amount.Round();
                balance.Round();
                
                Debited.Raise();
                Adjusted.Raise(AccountTransaction.Debited(starting, starting - amount));
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
                CartLogger.Log<LogCategoryCurrency>("Cannot credit less than or equal to 0.", typeof(CurrencyAccount));
                return;
            }

            lock (padlock)
            {
                var starting = balance;
                
                balance += amount.Round();
                balance.Round();
                
                Credited.Raise();
                Adjusted.Raise(AccountTransaction.Credited(starting, starting + amount));
            }
        }


        /// <summary>
        /// Clears the account to its default balance or a specified value.
        /// </summary>
        public void Clear(bool clearToInitial = true)
        {
            lock (padlock)
            {
                var starting = balance;
                balance = 0d;
                Adjusted.Raise(AccountTransaction.Reset(starting, clearToInitial ? startingBalance : 0d));
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