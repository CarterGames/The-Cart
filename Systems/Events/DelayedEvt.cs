// ----------------------------------------------------------------------------
// DelayedEvt.cs
// 
// Description: A selection of classes to handle most use cases you'll need
//              for events in your games when you need the event to run
//              after a delay.
// ----------------------------------------------------------------------------

using System;
using System.Threading;

namespace Scarlet.EventsSystem
{
    public class DelayedEvt
    {
        private event Action Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke();
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
        
    }
    
    
    public class DelayedEvt<T>
    {
        private event Action<T> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T param, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2>
    {
        private event Action<T1,T2> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2,T3>
    {
        private event Action<T1,T2,T3> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, T3 param3, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2, param3);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2,T3> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2,T3> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2,T3,T4>
    {
        private event Action<T1,T2,T3,T4> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2, param3, param4);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2,T3,T4> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2,T3,T4> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2,T3,T4,T5>
    {
        private event Action<T1,T2,T3,T4,T5> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2, param3, param4, param5);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2,T3,T4,T5> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2,T3,T4,T5> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2,T3,T4,T5,T6>
    {
        private event Action<T1,T2,T3,T4,T5,T6> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2, param3, param4, param5, param6);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2,T3,T4,T5,T6> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2,T3,T4,T5,T6> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2,T3,T4,T5,T6,T7>
    {
        private event Action<T1,T2,T3,T4,T5,T6,T7> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2, param3, param4, param5, param6, param7);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2,T3,T4,T5,T6,T7> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2,T3,T4,T5,T6,T7> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
    
    
    public class DelayedEvt<T1,T2,T3,T4,T5,T6,T7,T8>
    {
        private event Action<T1,T2,T3,T4,T5,T6,T7,T8> Action = delegate { };

        /// <summary>
        /// Raises the event to all listeners.
        /// </summary>
        public void Raise(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, int milliseconds = 0)
        {
            Timer timer = null;

            timer = new Timer((obj) =>
                {
                    Action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
                    timer?.Dispose();
                },
                null, milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Adds the action/method to the event listeners.
        /// </summary>
        public void Add(Action<T1,T2,T3,T4,T5,T6,T7,T8> listener)
        {
            Action -= listener;
            Action += listener;
        }

        /// <summary>
        /// Removes the action/method to the event listeners.
        /// </summary>
        public void Remove(Action<T1,T2,T3,T4,T5,T6,T7,T8> listener) => Action -= listener;
        
        /// <summary>
        /// Clears all listeners from the event.
        /// </summary>
        public void Clear() => Action = null;
    }
}