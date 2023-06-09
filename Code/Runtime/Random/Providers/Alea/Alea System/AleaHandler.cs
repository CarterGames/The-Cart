/*
 * Copyright (c) 2018-Present Carter Games
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

using System.Collections.Generic;

namespace Scarlet.Random.AleaPRNG
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