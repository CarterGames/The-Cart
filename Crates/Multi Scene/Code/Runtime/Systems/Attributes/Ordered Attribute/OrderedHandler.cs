#if CARTERGAMES_CART_CRATE_MULTISCENE

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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CarterGames.Cart.Crates.MultiScene
{
    /// <summary>
    /// A class to handle sorting ordered listeners.
    /// </summary>
    public static class OrderedHandler
    {
        /// <summary>
        /// Gets the listeners in the order they are defined via the attributes. 
        /// </summary>
        /// <param name="listeners">The listeners to process</param>
        /// <param name="methodName">The method name to look out for</param>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>A list of ordered listeners based on their defined orders.</returns>
        /// <remarks>
        /// The order attribute only works if the method it is on is a Multi Scene Interface Implementation, other methods will be ignored by the system at present.
        /// If the interface implementation has no order it will be set to 0 as it is the default, just like in the scripting execution order system in Unity. 
        /// </remarks>
        public static List<OrderedListenerData<T>> OrderListeners<T>(List<T> listeners, string methodName)
        {
            var _data = new List<OrderedListenerData<T>>();
            
            foreach (var _listener in listeners)
            {
                var _method = _listener.GetType().GetMethod(methodName);
                if (_method == null) continue;
                var _hasOrder = _method.GetCustomAttributes(typeof(MultiSceneOrderedAttribute), true).Length > 0;
        
                if (!_hasOrder)
                {
                    _data.Add(new OrderedListenerData<T>(0, _listener));
                    continue;
                }
                
                _data.Add(new OrderedListenerData<T>(_method.GetCustomAttribute<MultiSceneOrderedAttribute>().order, _listener));
            }

            return _data.OrderBy(t => t.Order).ToList();
        }
    }
}

#endif