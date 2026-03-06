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

namespace CarterGames.Cart.Random
{
    public static class AleaHandler
    {
        public static AleaState InitializeState(string seed)
        {
            Mash mash = new Mash();


            var s0 = mash.RunMash(" ");
            var s1 = mash.RunMash(" ");
            var s2 = mash.RunMash(" ");
            var c = 1;


            s0 -= mash.RunMash(seed);

            if (s0 < 0)
            {
                s0 += 1;
            }

            s1 -= mash.RunMash(seed);

            if (s1 < 0)
            {
                s1 += 1;
            }

            s2 -= mash.RunMash(seed);

            if (s2 < 0)
            {
                s2 += 1;
            }


            return new AleaState()
            {
                c = c,
                s0 = s0,
                s1 = s1,
                s2 = s2,
            };
        }



        public static KeyValuePair<double, AleaState> AdvanceState(AleaState state)
        {
            var c = state.c;
            var s0 = state.s0;
            var s1 = state.s1;
            var s2 = state.s2;
            
            var t = 2091639 * s0 + c * 2.3283064365386963e-10;
            
            s0 = s1;
            s1 = s2;
            
            var value = (s2 = t - (c = (int) t | 0));

            return new KeyValuePair<double, AleaState>(value, new AleaState()
            {
                c = c,
                s0 = s0,
                s1 = s1,
                s2 = s2,
            });
        }
    }
}