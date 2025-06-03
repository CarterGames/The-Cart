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

using System;

namespace CarterGames.Cart.Core.Events
{
    /// <summary>
    /// A helper class to aid in running logic after a system has initialized, or a trigger has been reached without having to write extra logic.
    /// </summary>
    public static class EvtListener
    {
        /// <summary>
        /// Runs the entered action when the evt is raised or the entered bool is true. Whichever is first.
        /// </summary>
        /// <param name="check">The bool to check.</param>
        /// <param name="evt">The evt to listen for if the bool is false when checked.</param>
        /// <param name="toRun">The action to run.</param>
        public static void RunWhenEither(bool check, Evt evt, Action toRun)
        {
            if (check)
            {
                toRun?.Invoke();
                return;
            }

            evt.Add(OnEvtRaised);
            return;

            void OnEvtRaised()
            {
                evt.Remove(OnEvtRaised);
                toRun?.Invoke();
            }
        }
        
        
        /// <summary>
        /// Raises the entered evt when the check evt is raised or the entered bool is true. Whichever is first.
        /// </summary>
        /// <param name="check">The bool to check.</param>
        /// <param name="evt">The evt to listen for if the bool is false when checked.</param>
        /// <param name="toRunEvt">The evt to raise.</param>
        public static void RunWhenEither(bool check, Evt evt, Evt toRunEvt)
        {
            if (check)
            {
                toRunEvt.Raise();
                return;
            }

            evt.Add(OnEvtRaised);
            return;

            void OnEvtRaised()
            {
                evt.Remove(OnEvtRaised);
                toRunEvt.Raise();
            }
        }
        
        
        /// <summary>
        /// Runs the entered action when the action is invoked or the entered bool is true. Whichever is first.
        /// </summary>
        /// <param name="check">The bool to check.</param>
        /// <param name="checkAction">The action to listen for if the bool is false when checked.</param>
        /// <param name="toRun">The action to run.</param>
        public static void RunWhenEither(bool check, Action checkAction, Action toRun)
        {
            if (check)
            {
                toRun?.Invoke();
                return;
            }

            checkAction -= OnActionRaised;
            checkAction += OnActionRaised;
            return;

            void OnActionRaised()
            {
                checkAction -= OnActionRaised;
                toRun?.Invoke();
            }
        }
        
        
        /// <summary>
        /// Raises the entered evt when the check action is invoked or the entered bool is true. Whichever is first.
        /// </summary>
        /// <param name="check">The bool to check.</param>
        /// <param name="checkAction">The action to listen for if the bool is false when checked.</param>
        /// <param name="toRunEvt">The evt to raise.</param>
        public static void RunWhenEither(bool check, Action checkAction, Evt toRunEvt)
        {
            if (check)
            {
                toRunEvt.Raise();
                return;
            }

            checkAction -= OnActionRaised;
            checkAction += OnActionRaised;
            return;

            void OnActionRaised()
            {
                checkAction -= OnActionRaised;
                toRunEvt.Raise();
            }
        }
        
        
        /// <summary>
        /// Runs the entered action when the evt is raised or the entered bool is true. Whichever is first.
        /// </summary>
        /// <param name="check">The bool to check.</param>
        /// <param name="evt">The evt to listen for if the bool is false when checked.</param>
        /// <param name="toRun">The action to run.</param>
        public static void ListenIfFalse(this Evt evt, bool check, Action toRun) => RunWhenEither(check, evt, toRun);
        
        
        /// <summary>
        /// Runs the entered action when the evt is raised or the entered bool is true. Whichever is first.
        /// </summary>
        /// <param name="check">The bool to check.</param>
        /// <param name="evt">The evt to listen for if the bool is false when checked.</param>
        /// <param name="toRunEvt">The evt to raise.</param>
        public static void ListenIfFalse(this Evt evt, bool check, Evt toRunEvt) => RunWhenEither(check, evt, toRunEvt);
    }
}