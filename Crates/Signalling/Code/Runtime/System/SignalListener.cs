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

using CarterGames.Cart.Events;

namespace CarterGames.Cart.Crates.Signals
{
    public sealed class SignalListener
    {
        public string SignalId { get; private set; }
        public int Priority { get; private set; }
        

        public readonly Evt SignaledEvt;
        
        
        public SignalListener(string signalId, int priority = 0)
        {
            SignalId = signalId;
            Priority = priority;
            
            SignaledEvt = new Evt();
            
            Signaller.RegisterListener(this);
        }

        
        ~SignalListener()
        {
            Signaller.RemoveListener(this);
        }
    }
}

#endif