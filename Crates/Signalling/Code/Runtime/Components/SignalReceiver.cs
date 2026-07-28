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

using UnityEngine;
using UnityEngine.Events;

namespace CarterGames.Cart.Crates.Signals
{
    /// <summary>
    /// A component for receiving a signal.
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Crates/Signals/SignalReceiver")]
    public class SignalReceiver : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string signalId;
        [SerializeField] private UnityEvent onSignalReceivedEvt;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private SignalListener SignalListener { get; set; }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void Awake()
        {
            SignalListener = new SignalListener(signalId);
            SignalListener.SignaledEvt.Add(OnSignalReceived);
        }

        
        private void OnDestroy()
        {
            if (SignalListener == null) return;
            SignalListener.SignaledEvt.Remove(OnSignalReceived);
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnSignalReceived()
        {
            onSignalReceivedEvt?.Invoke();
        }
    }
}

#endif