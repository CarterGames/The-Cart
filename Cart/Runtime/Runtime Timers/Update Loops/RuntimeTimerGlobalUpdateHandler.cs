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

using CarterGames.Cart.Events;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace CarterGames.Cart
{
    /// <summary>
    /// Handles the management of runtime timers when the game is active. Only gets used if a runtime timerOld is called into existence.
    /// </summary>
    public static class RuntimeTimerGlobalUpdateHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static PlayerLoopSystem timerLoopSystem;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public static readonly Evt UpdateTickedEvt = new Evt();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            // Setup up an update loop listener to call an evt, giving update to timers not in momo classes.
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            ref var updateSystem = ref playerLoop.GetSubSystem<Update>();
            updateSystem.AddCallback(UpdateTickedEvt.Raise);
            PlayerLoop.SetPlayerLoop(playerLoop);
        }
    }
}