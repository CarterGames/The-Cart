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

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Events;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Save;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace CarterGames.Cart.Modules.Currency
{
    /// <summary>
    /// The main manager class for the currency system.
    /// </summary>
    public static class CurrencyManager
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private const string AccountsSaveKey = "CartSave_Modules_Currency_Accounts";
        private static readonly Dictionary<string, CurrencyAccount> Accounts = new Dictionary<string, CurrencyAccount>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when all accounts are loaded into the system.
        /// </summary>
        public static readonly Evt AccountsLoaded = new Evt();


        /// <summary>
        /// Raises when an account is opened.
        /// </summary>
        public static readonly Evt AccountOpened = new Evt();


        /// <summary>
        /// Raises when an accounts balance is altered.
        /// </summary>
        public static readonly Evt<CurrencyAccount, AccountTransaction> AccountBalanceChanged = new Evt<CurrencyAccount, AccountTransaction>();


        /// <summary>
        /// Raises when an account is closed.
        /// </summary>
        public static readonly Evt AccountClosed = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if the accounts have been loaded from the save or not.
        /// </summary>
        public static bool HasLoadedAccounts { get; private set; }


        /// <summary>
        /// Gets all the account id's the system has stored.
        /// </summary>
        public static List<string> AllAccountIds
        {
            get
            {
                var keys = new List<string>();
                var data = CartSaveHandler.Get<string>(AccountsSaveKey);
            
                if (!string.IsNullOrEmpty(data))
                {
                    var jsonData = JArray.Parse(data);

                    if (jsonData.Count > 0)
                    {
                        foreach (var account in jsonData)
                        {
                            keys.Add(account["key"]!.ToString());
                        }
                    }
                }
                
                if (DataAccess.GetAsset<DataAssetDefaultAccounts>())
                {
                    foreach (var defAccount in DataAccess.GetAsset<DataAssetDefaultAccounts>().DefaultAccounts)
                    {
                        if (keys.Contains(defAccount.Key)) continue;
                        keys.Add(defAccount.Key);
                    }
                }

                return keys;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Initialization
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Initializes the system automatically.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeAccounts()
        {
            Application.focusChanged -= OnFocusChange;
            Application.focusChanged += OnFocusChange;
            
            LoadAccounts();

            void OnFocusChange(bool isFocused)
            {
                if (isFocused) return;
                SaveAccounts();
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if an account exists.
        /// </summary>
        /// <param name="accountId">The id to check for.</param>
        /// <returns>If the account exists.</returns>
        public static bool AccountExists(string accountId)
        {
            if (!HasLoadedAccounts)
            {
                CartLogger.Log<LogCategoryCurrency>("Accounts not loaded, returning false", typeof(CurrencyManager));
                return false;
            }
            
            return Accounts.ContainsKey(accountId);
        }


        /// <summary>
        /// Tries to get the balance of an account.
        /// </summary>
        /// <param name="accountId">The id to add.</param>
        /// <param name="balance">The balance of the account.</param>
        /// <returns>If it was successful or not.</returns>
        public static bool TryGetBalance(string accountId, out double balance)
        {
            balance = GetBalance(accountId);
            return balance > -1;
        }


        /// <summary>
        /// Gets the balance of an account.
        /// </summary>
        /// <param name="accountId">The id to add.</param>
        /// <returns>The balance of the account.</returns>
        public static double GetBalance(string accountId)
        {
            if (!Accounts.TryGetValue(accountId, out var account)) return -1;
            return account.Balance;
        }


        /// <summary>
        /// Tries to get an account of the required id.
        /// </summary>
        /// <param name="accountId">The id to add.</param>
        /// <param name="account">The account found.</param>
        /// <returns>If it was successful or not.</returns>
        public static bool TryGetAccount(string accountId, out CurrencyAccount account)
        {
            account = GetAccount(accountId);
            return account != null;
        }


        /// <summary>
        /// Gets an account.
        /// </summary>
        /// <param name="accountId">The id to add.</param>
        /// <returns>The account found.</returns>
        public static CurrencyAccount GetAccount(string accountId)
        {
            if (!Accounts.TryGetValue(accountId, out var account)) return null;
            return account;
        }


        /// <summary>
        /// Adds an account to the system.
        /// </summary>
        /// <param name="accountId">The id to add.</param>
        /// <param name="startingBalance">The starting balance for the account. Def = 0.</param>
        public static void AddAccount(string accountId, double startingBalance = 0d)
        {
            if (Accounts.ContainsKey(accountId)) return;
            Accounts.Add(accountId, CurrencyAccount.NewAccount(accountId, startingBalance));

            Accounts[accountId].Adjusted.Add(OnAccountAdjusted);
            AccountOpened.Raise();
            return;
            
            
            void OnAccountAdjusted(AccountTransaction transaction)
            {
                AccountBalanceChanged.Raise(Accounts[accountId], transaction);
            }
        }


        /// <summary>
        /// Closes an account when called.
        /// </summary>
        /// <param name="accountId">The id to close.</param>
        public static void CloseAccount(string accountId)
        {
            if (!Accounts.ContainsKey(accountId)) return;
            Accounts.Remove(accountId);
            AccountClosed.Raise();
        }


        /// <summary>
        /// Loads the accounts when called.
        /// </summary>
        private static void LoadAccounts()
        {
            var data = CartSaveHandler.Get<string>(AccountsSaveKey);
            Accounts.Clear();

            if (!string.IsNullOrEmpty(data))
            {
                var jsonData = JToken.Parse(data); // Array (account in json)

                foreach (var account in jsonData)
                {
                    if (account["starting"] != null)
                    {
                        Accounts.Add(account["key"].ToString(),
                            CurrencyAccount.AccountWithBalance(account["key"].ToString(),
                                account["value"].Value<double>().Round(), account["starting"].Value<double>().Round()));
                    }
                    else
                    {
                        Accounts.Add(account["key"].ToString(),
                            CurrencyAccount.AccountWithBalance(account["key"].ToString(),
                                account["value"].Value<double>().Round()));
                    }
                }
            }

            foreach (var defAccount in DataAccess.GetAsset<DataAssetDefaultAccounts>().DefaultAccounts)
            {
                if (Accounts.ContainsKey(defAccount.Key)) continue;
                Accounts.Add(defAccount.Key, CurrencyAccount.NewAccount(defAccount.Key, defAccount.Value.Round()));
            }

            foreach (var account in Accounts)
            {
                account.Value.Adjusted.Add(OnAccountAdjusted);
                
                continue;
                void OnAccountAdjusted(AccountTransaction transaction)
                {
                    AccountBalanceChanged.Raise(account.Value, transaction);
                }
            }
            
            HasLoadedAccounts = true;
            AccountsLoaded.Raise();
        }


        /// <summary>
        /// Saves the accounts when called.
        /// </summary>
        public static void SaveAccounts()
        {
            var list = new Dictionary<string, AccountSaveStructure>();
            
            foreach (var account in Accounts)
            {
                if (string.IsNullOrEmpty(account.Key)) continue;
                
                if (list.ContainsKey(account.Key))
                {
                    list[account.Key].balance = account.Value.Balance.Round();
                }
                else
                {
                    var obj = new AccountSaveStructure
                    {
                        key = account.Key,
                        balance = account.Value.Balance.Round(),
                        starting = account.Value.StartingBalance.Round(),
                    };

                    list.Add(account.Key, obj);
                }
            }
            
            CartSaveHandler.Set(AccountsSaveKey, JObject.FromObject(list));
        }
    }
}

#endif