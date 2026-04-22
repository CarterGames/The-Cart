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

namespace CarterGames.Cart
{
    /// <summary>
    /// Just like the TryGetComponent, but with options to get from parents and children.
    /// </summary>
    public static class TryGetExtensions
    {
        /// <summary>
        /// Tries to get the component requested on the GameObject entered.
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponents<T>(this Component target, out T[] component)
        {
            var check = target.GetComponents<T>();
            component = check;
            return check is { } && check.Length > 0;
        }
        
        
        /// <summary>
        /// Tries to get the component requested on the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponents(this Component target, Type type, out Component[] component)
        {
            var check = target.GetComponents(type);
            component = check;
            return check is { } && check.Length > 0;
        } 
        
        
        /// <summary>
        /// Tries to get the component requested on the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponents<T>(this GameObject target, out T[] component)
        {
            var check = target.GetComponents<T>();
            component = check;
            return check is { } && check.Length > 0;
        }


        /// <summary>
        /// Tries to get the component requested on the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponents(this GameObject target, Type type, out Component[] component)
        {
            var check = target.GetComponents(type);
            component = check;
            return check is { } && check.Length > 0;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent<T>(this Component target, out T component)
        {
            var check = target.GetComponentInParent<T>();
            component = check;
            return check != null;
        }


        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent(this Component target, Type type, out Component component)
        {
            var check = target.GetComponentInParent(type);
            component = check;
            return check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent<T>(this GameObject target, out T component)
        {
            var check = target.GetComponentInParent<T>();
            component = check;
            return check != null;
        }


        /// <summary>
        /// Tries to get the component requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInParent(this GameObject target, Type type, out Component component)
        {
            var check = target.GetComponentInParent(type);
            component = check;
            return check != null;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent<T>(this Component target, out T[] component)
        {
            var check = target.GetComponentsInParent<T>();
            component = check;
            return check is { } && check.Length > 0;
        } 
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent(this Component target, Type type, out Component[] component)
        {
            var check = target.GetComponentsInParent(type);
            component = check;
            return check is { } && check.Length > 0;
        } 
        
        
        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent<T>(this GameObject target, out T[] component)
        {
            var check = target.GetComponentsInParent<T>();
            component = check;
            return check is { } && check.Length > 0;
        }


        /// <summary>
        /// Tries to get the components requested in the parent(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInParent(this GameObject target, Type type, out Component[] component)
        {
            var check = target.GetComponentsInParent(type);
            component = check;
            return check is { } && check.Length > 0;
        }


        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren<T>(this Component target, out T component)
        {
            var check = target.GetComponentInChildren<T>();
            component = check;
            return check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren(this Component target, Type type, out Component component)
        {
            var check = target.GetComponentInChildren(type);
            component = check;
            return check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The component found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren<T>(this GameObject target, out T component)
        {
            var check = target.GetComponentInChildren<T>();
            component = check;
            return check != null;
        }
        
        
        /// <summary>
        /// Tries to get the component requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The component found</param>
        /// <returns>Bool with the component</returns>
        public static bool TryGetComponentInChildren(this GameObject target, Type type, out Component component)
        {
            var check = target.GetComponentInChildren(type);
            component = check;
            return check != null;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren<T>(this Component target, out T[] component)
        {
            var check = target.GetComponentsInChildren<T>();
            component = check;
            return check is { } && check.Length > 0;
        }

        
        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren(this Component target, Type type, out Component[] component)
        {
            var check = target.GetComponentsInChildren(type);
            component = check;
            return check is { } && check.Length > 0;
        }
        
        
        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="component">The components found</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren<T>(this GameObject target, out T[] component)
        {
            var check = target.GetComponentsInChildren<T>();
            component = check;
            return check is { } && check.Length > 0;
        }

        /// <summary>
        /// Tries to get the components requested in the children(s) of the GameObject entered...
        /// </summary>
        /// <param name="target">The GameObject to target</param>
        /// <param name="type">The type to get</param>
        /// <param name="component">The components found</param>
        /// <returns>Bool with the components list</returns>
        public static bool TryGetComponentsInChildren(this GameObject target, Type type, out Component[] component)
        {
            var check = target.GetComponentsInChildren(type);
            component = check;
            return check is { } && check.Length > 0;
        }
    }
}