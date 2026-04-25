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

using CarterGames.Cart.Data;
using CarterGames.Cart.Events;
using CarterGames.Cart.Logs;

namespace CarterGames.Cart.Crates.Analytics
{
    public static class AnalyticsManager
    {
        public static bool IsInitialized { get; private set; }
        
        private static AnalyticsProvider Provider => Settings.GetProvider;
        private static AnalyticsSettingsDataAsset Settings => DataAccess.GetAsset<AnalyticsSettingsDataAsset>();

        public static readonly Evt InitializedEvt = new Evt();


        private static void Initialize()
        {
            if (IsInitialized) return;
            
            Provider.Initialize(OnProviderInitialized);
            return;

            void OnProviderInitialized()
            {
                IsInitialized = true;
                InitializedEvt.Raise();
            }
        }
        
        
        public static void SendEvent(AnalyticsEvent analyticsEvent)
        {
            if (!IsInitialized)
            {
                CartLogger.Log<AnalyticsLogs>(
                    $"Cannot send event {analyticsEvent.EventName} as the analytics system is not initialized",
                    typeof(AnalyticsManager));

                return;
            }
            
            Provider.SendEvent(analyticsEvent);
        }
    }
}

#endif