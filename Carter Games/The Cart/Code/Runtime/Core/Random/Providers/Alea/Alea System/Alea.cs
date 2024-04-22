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

namespace CarterGames.Cart.Core.Random
{
    public sealed class Alea
    {
        private string seed;
        private AleaState state;
        
        
        public Alea(string seed)
        {
            this.seed = seed;
            state = AleaHandler.InitializeState(seed);
        }


        public AleaState ExportState()
        {
            return state;
        }


        public Alea ImportState(AleaState state)
        {
            this.state = state;
            return this;
        }


        public double Next()
        {
            var data = AleaHandler.AdvanceState(state);
            state = data.Value;
            return data.Key;
        }


        public double Random()
        {
            return Next();
        }


        public Alea Clone()
        {
            return new Alea(seed).ImportState(ExportState());
        }
    }
}