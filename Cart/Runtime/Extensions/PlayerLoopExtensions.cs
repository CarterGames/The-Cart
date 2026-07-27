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
using UnityEngine.LowLevel;

// Initially not my code, though I understand the concept, this was just a nice setup to add.
// OG Credit: https://gist.github.com/popcron/bf295e193ed5c3b965cd3c805f3ce378
// Adjustments made to match conventions for Carter Games as well as the flow I'd prefer.

namespace CarterGames.Cart
{
    public static class PlayerLoopExtensions
    {
        public static int AddCallback(this ref PlayerLoopSystem system, Action function, Type systemType = null)
        {
            var index = system.subSystemList.Length;
            var subsystemList = new PlayerLoopSystem[index + 1];
            
            Array.Copy(system.subSystemList, subsystemList, system.subSystemList.Length);
            
            ref var newCallbackSystem = ref subsystemList[index];
            
            newCallbackSystem.updateDelegate = new PlayerLoopSystem.UpdateFunction(function);
            newCallbackSystem.type = systemType;
            system.subSystemList = subsystemList;
            return index;
        }
        

        public static void RemoveCallback(this ref PlayerLoopSystem system, int index)
        {
            if (index < 0 || index >= system.subSystemList.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var subsystemList = new PlayerLoopSystem[system.subSystemList.Length - 1];
            
            if (index > 0)
            {
                Array.Copy(system.subSystemList, 0, subsystemList, 0, index);
            }

            if (index < system.subSystemList.Length - 1)
            {
                Array.Copy(system.subSystemList, index + 1, subsystemList, index,
                    system.subSystemList.Length - index - 1);
            }

            system.subSystemList = subsystemList;
        }
        

        public static ref PlayerLoopSystem GetSubSystem<T>(this ref PlayerLoopSystem playerLoopSystem)
        {
            var systemType = typeof(T);
            var subSystems = playerLoopSystem.subSystemList;
            
            for (var i = 0; i < subSystems.Length; i++)
            {
                ref var subSystem = ref subSystems[i];
                
                if (subSystem.type == systemType)
                {
                    return ref subSystem;
                }
            }

            throw new InvalidOperationException($"Subsystem of type {typeof(T)} not found");
        }

        
        public static int IndexOf(this ref PlayerLoopSystem system, Type systemType)
        {
            for (var i = 0; i < system.subSystemList.Length; i++)
            {
                if (system.subSystemList[i].type == systemType)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}