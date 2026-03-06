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

using System;

namespace CarterGames.Cart.Events
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