#if CARTERGAMES_CART_CRATE_SIGNALLING

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
using System.Linq;
using CarterGames.Cart.Logs;

namespace CarterGames.Cart.Crates.Signals
{
    /// <summary>
    /// Handles signalling signals for other systems to listen to.
    /// </summary>
    public static class Signaller
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private static readonly Dictionary<string, List<SignalListener>> ListenersLookup =
            new Dictionary<string, List<SignalListener>>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Registers a listener to the setup.
        /// </summary>
        /// <param name="listener">The listener to add.</param>
        public static void RegisterListener(SignalListener listener)
        {
            if (ListenersLookup.TryGetValue(listener.SignalId, out var signalsList))
            {
                signalsList.Add(listener);
            }
            else
            {
                ListenersLookup.Add(listener.SignalId, new List<SignalListener>()
                {
                    listener
                });
            }
        }


        /// <summary>
        /// Removes a listener from the system.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public static void RemoveListener(SignalListener listener)
        {
            if (!ListenersLookup.ContainsKey(listener.SignalId)) return;
            ListenersLookup[listener.SignalId].Remove(listener);
        }
        
        
        /// <summary>
        /// Sends the signal when called.
        /// </summary>
        /// <param name="signalId">The id to send.</param>
        public static void Send(string signalId)
        {
            if (ListenersLookup.TryGetValue(signalId, out var listeners))
            {
                foreach (var listener in listeners.OrderBy(t => t.Priority))
                {
                    listener.SignaledEvt.Raise();
                }

                CartLogger.Log<SignalLogs>($"[Signal {signalId}]: Sent.");
                return;
            }
            
            CartLogger.Log<SignalLogs>($"[Signal]: Blocked {signalId} as it has no listeners.");
        }


        /// <summary>
        /// Tries to send the signal when called.
        /// </summary>
        /// <param name="signalId">The id to send.</param>
        /// <returns>If is sent or not (Bool).</returns>
        public static bool TrySend(string signalId)
        {
            if (!ListenersLookup.ContainsKey(signalId)) return false;
            Send(signalId);
            return true;
        }
    }
}

#endif