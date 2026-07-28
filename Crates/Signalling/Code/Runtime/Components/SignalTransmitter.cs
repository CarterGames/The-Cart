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

namespace CarterGames.Cart.Crates.Signals
{
    /// <summary>
    /// A component for sending a signal
    /// </summary>
    [AddComponentMenu("Carter Games/The Cart/Crates/Signals/SignalTransmitter")]
    public class SignalTransmitter : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string signalId;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Sends the signal when called.
        /// </summary>
        public void SendSignal()
        {
            Signaller.Send(signalId);
        }
    }
}

#endif