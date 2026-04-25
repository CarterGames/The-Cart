#if CARTERGAMES_CART_CRATE_ANALYTICS

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

using System.Collections.Generic;

#if CARTERGAMES_CART_CRATE_CURRENCY
using CarterGames.Cart.Crates.Currency;
#endif

namespace CarterGames.Cart.Crates.Analytics
{
    public class AnalyticsEventCurrencyEarn : AnalyticsEvent
    {
        public override string EventName => "currency_earn";
        
        
        private string CurrencyName { get; set; }
        private double CurrencyValue { get; set; }
        private double TotalCurrency { get; set; }
        private string[] VcCategories { get; set; }


#if CARTERGAMES_CART_CRATE_CURRENCY
        public AnalyticsEventCurrencyEarn(CurrencyAccount account, AccountTransaction transaction, params string[] virtualCurrencyCategories)
        {
            CurrencyName = account.Id;
            CurrencyValue = transaction.NewValue - transaction.StartingValue;
            TotalCurrency = transaction.NewValue;
            VcCategories = virtualCurrencyCategories;
        }
#endif
        
        public AnalyticsEventCurrencyEarn(string name, double amount, double total, params string[] virtualCurrencyCategories)
        {
            CurrencyName = name;
            CurrencyValue = amount;
            TotalCurrency = total;
            VcCategories = virtualCurrencyCategories;
        }
        
        
        protected override List<KeyValuePair<string, object>> MainParameters()
        {
            var values = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("currency", CurrencyName),
                new KeyValuePair<string, object>("value", CurrencyValue),
                new KeyValuePair<string, object>("net", TotalCurrency)
            };

            var index = 0;
            
            foreach (var category in VcCategories)
            {
                values.Add(new KeyValuePair<string, object>($"vc_category_{index}", category));
                index++;
            }

            return values;
        }
    }
}

#endif