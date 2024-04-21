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

using System;
using System.Collections.Generic;

namespace CarterGames.Cart.Core.Events
{
    public class EvtBase
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected readonly Dictionary<string, Action> anonymous = new Dictionary<string, Action>();
        protected event Action Action = delegate { };

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        protected void RaiseAction()
        {
            Action?.Invoke();
        }

        
        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        /// <param name="listener">The listener to add.</param>
        protected void AddAction(Action listener)
        {
            Action -= listener;
            Action += listener;
        }
        

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        /// <param name="id">The id to refer to this listener.</param>
        /// <param name="listener">The listener to add.</param>
        protected void AddAnonymousAction(string id, Action listener)
        {
            if (anonymous.TryGetValue(id, out var anon))
            {
                AddAction(anon);
                return;
            }
            
            anonymous.Add(id, listener);
            AddAction(anonymous[id]);
        }
        

        /// <summary>
        /// Removes the action/method from the event listeners.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        protected void RemoveAction(Action listener)
        {
            Action -= listener;
        }

        
        /// <summary>
        /// Removes the action/method from the event listeners.
        /// </summary>
        /// <param name="id">The id of the listener to remove.</param>
        protected void RemoveAnonymousAction(string id)
        {
            if (!anonymous.ContainsKey(id)) return;
            RemoveAction(anonymous[id]);
            anonymous.Remove(id);
        }
        
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() 
        {
            anonymous.Clear();
            Action = null;
        }
    }
}