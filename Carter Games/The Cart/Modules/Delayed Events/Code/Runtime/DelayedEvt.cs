#if CARTERGAMES_CART_MODULE_DELAYEDEVENTS

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

using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Events;

namespace CarterGames.Cart.Modules.DelayedEvents
{
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   No Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class DelayedEvt : EvtBase
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, bool unscaledTime = true)
        {
            RuntimeTimer.Set(seconds, RaiseAction, unscaledTime);
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
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The params to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T param, bool unscaledTime = true)
        {
            RuntimeTimer.Set(seconds, () => RaiseAction(param), unscaledTime);
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
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="seconds">The time to delay the evt by.</param>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="unscaledTime">Should the event be tied to unscaled time? Def: True</param>
        public void Raise(float seconds, T1 param, T2 param2, bool unscaledTime = true)
        {
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2), unscaledTime);
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
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2, param3), unscaledTime);
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
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2, param3, param4), unscaledTime);
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
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2, param3, param4, param5), unscaledTime);
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
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2, param3, param4, param5, param6), unscaledTime);
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
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2, param3, param4, param5, param6, param7), unscaledTime);
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
            RuntimeTimer.Set(seconds, () => RaiseAction(param, param2, param3, param4, param5, param6, param7, param8), unscaledTime);
        }
    }
}

#endif