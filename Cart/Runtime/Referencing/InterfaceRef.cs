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
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarterGames.Cart
{
    [Serializable]
    public class InterfaceRef<T> : ISerializationCallbackReceiver where T : class
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private Object target;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public T Value => target as T;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Operators
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public static implicit operator bool(InterfaceRef<T> ir) => ir.target != null;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public void OnValidate()
        {
            if (target is T) return;
            if (!(target is GameObject go)) return;
            
            target = null;
                    
            foreach (var comp in go.GetComponents<Component>())
            {
                if (!(comp is T variable)) continue;
                
                target = comp;
                break;
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   ISerializationCallbackReceiver Implementation
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();
        void ISerializationCallbackReceiver.OnAfterDeserialize() { }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public void SetValue(T value)
        {
            target = value as Object;
        }
    }
}