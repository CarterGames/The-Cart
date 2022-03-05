// ----------------------------------------------------------------------------
// Evt.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 15/02/2022
// ----------------------------------------------------------------------------

using System;

namespace GameEvents
{
    public class Evt
    {
        private event Action _action = delegate { };

        public void Raise()
        {
            _action.Invoke();
        }

        public void Add(Action listener)
        {
            _action -= listener;
            _action += listener;
        }

        public void Remove(Action listener)
        {
            _action -= listener;
        }
    }
    
    public class Evt<T>
    {
        private event Action<T> _action = delegate { };

        public void Raise(T param)
        {
            _action.Invoke(param);
        }

        public void Add(Action<T> listener)
        {
            _action -= listener;
            _action += listener;
        }

        public void Remove(Action<T> listener)
        {
            _action -= listener;
        }
    }
}