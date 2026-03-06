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

namespace CarterGames.Cart.Crates.Currency
{
    /// <summary>
    /// Defines a transaction for an account.
    /// </summary>
    public struct AccountTransaction
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The type of the transaction.
        /// </summary>
        public CurrencyTransactionType TransactionType { get; private set; }
        
        
        /// <summary>
        /// The starting value.
        /// </summary>
        public double StartingValue { get; private set; }
        
        
        /// <summary>
        /// The new value.
        /// </summary>
        public double NewValue { get; private set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// A transition to add currency.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>AccountTransaction</returns>
        public static AccountTransaction Credited(double startingValue, double newValue)
        {
            return new AccountTransaction()
            {
                TransactionType = CurrencyTransactionType.Credit,
                StartingValue = startingValue,
                NewValue = newValue
            };
        }
        

        /// <summary>
        /// A transition to remove currency.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>AccountTransaction</returns>
        public static AccountTransaction Debited(double startingValue, double newValue)
        {
            return new AccountTransaction()
            {
                TransactionType = CurrencyTransactionType.Debit,
                StartingValue = startingValue,
                NewValue = newValue
            };
        }
        
        
        /// <summary>
        /// A transition to reset an account.
        /// </summary>
        /// <param name="startingValue">The starting value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>AccountTransaction</returns>
        public static AccountTransaction Reset(double startingValue, double newValue)
        {
            return new AccountTransaction()
            {
                TransactionType = CurrencyTransactionType.Clear,
                StartingValue = startingValue,
                NewValue = newValue
            };
        }
    }
}

#endif