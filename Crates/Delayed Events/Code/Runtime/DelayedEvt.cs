#if CARTERGAMES_CART_CRATE_DELAYEDEVENTS

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

namespace CarterGames.Cart.Crates.DelayedEvents
{
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   No Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt : EvtBase
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(RaiseAction);
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   1 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T> : EvtBase<T>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The params to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T param, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   2 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2> : EvtBase<T1,T2>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   3 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2,T3> : EvtBase<T1,T2,T3>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, T3 param3, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2, param3));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   4 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2,T3,T4> : EvtBase<T1,T2,T3,T4>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, T3 param3, T4 param4, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2, param3, param4));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   5 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2,T3,T4,T5> : EvtBase<T1,T2,T3,T4,T5>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, T3 param3, T4 param4, T5 param5, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2, param3, param4, param5));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   6 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2,T3,T4,T5,T6> : EvtBase<T1,T2,T3,T4,T5,T6>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="param6">The 6th param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2, param3, param4, param5, param6));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   7 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2,T3,T4,T5,T6,T7> : EvtBase<T1,T2,T3,T4,T5,T6,T7>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="param6">The 6th param to pass through when raising.</param>
        /// <param name="param7">The 7th param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2, param3, param4, param5, param6, param7));
            Timer.Start();
        }
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   8 Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt<T1,T2,T3,T4,T5,T6,T7,T8> : EvtBase<T1,T2,T3,T4,T5,T6,T7,T8>
    {
        private RuntimeTimer Timer { get; set; }
        
        
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="param6">The 6th param to pass through when raising.</param>
        /// <param name="param7">The 7th param to pass through when raising.</param>
        /// <param name="param8">The 8th param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, bool unscaledTime = true)
        {
            Timer = new CountdownRuntimeTimer(seconds, TimerUpdateSource.Global, unscaledTime);
            Timer.CompletedEvt.Add(() => RaiseAction(param, param2, param3, param4, param5, param6, param7, param8));
            Timer.Start();
        }
    }
}

#endif