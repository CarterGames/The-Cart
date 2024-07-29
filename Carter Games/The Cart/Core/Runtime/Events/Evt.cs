/*
 * Copyright (c) 2024 Carter Games
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

namespace CarterGames.Cart.Core.Events
{
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   No Parameters Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt : EvtBase
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise() => RaiseAction();
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   1 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T> : EvtBase<T>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The params to pass through when raising.</param>
        public void Raise(T param) => RaiseAction(param);
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   2 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2> : EvtBase<T1,T2>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2) => RaiseAction(param, param2);
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   3 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2,T3> : EvtBase<T1,T2,T3>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2, T3 param3) => RaiseAction(param, param2, param3);
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   4 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2,T3,T4> : EvtBase<T1,T2,T3,T4>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2, T3 param3, T4 param4) => RaiseAction(param, param2, param3, param4);
    }

    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   5 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2,T3,T4,T5> : EvtBase<T1,T2,T3,T4,T5>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2, T3 param3, T4 param4, T5 param5) 
            => RaiseAction(param, param2, param3, param4, param5);
    }

    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   6 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2,T3,T4,T5,T6> : EvtBase<T1,T2,T3,T4,T5,T6>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="param6">The 6th param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) 
            => RaiseAction(param, param2, param3, param4, param5, param6);
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   7 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2,T3,T4,T5,T6,T7> : EvtBase<T1,T2,T3,T4,T5,T6,T7>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="param6">The 6th param to pass through when raising.</param>
        /// <param name="param7">The 7th param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) 
            => RaiseAction(param, param2, param3, param4, param5, param6, param7);
    }
    
    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────
    |   8 Parameter Evt
    ───────────────────────────────────────────────────────────────────────────────────────────────────────────────── */
    
    /// <summary>
    /// A custom event class that helps avoid over subscription and more.
    /// </summary>
    public sealed class Evt<T1,T2,T3,T4,T5,T6,T7,T8> : EvtBase<T1,T2,T3,T4,T5,T6,T7,T8>
    {
        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        /// <param name="param">The 1st param to pass through when raising.</param>
        /// <param name="param2">The 2nd param to pass through when raising.</param>
        /// <param name="param3">The 3rd param to pass through when raising.</param>
        /// <param name="param4">The 4th param to pass through when raising.</param>
        /// <param name="param5">The 5th param to pass through when raising.</param>
        /// <param name="param6">The 6th param to pass through when raising.</param>
        /// <param name="param7">The 7th param to pass through when raising.</param>
        /// <param name="param8">The 8th param to pass through when raising.</param>
        public void Raise(T1 param, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8) 
            => RaiseAction(param, param2, param3, param4, param5, param6, param7, param8);
    }
}