#if CARTERGAMES_CART_MODULE_CURRENCY

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

namespace CarterGames.Cart.Modules.Currency
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