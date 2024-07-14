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

using System;
using CarterGames.Cart.Core.Data;
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Core.Random
{
    public sealed class AleaRngProvider : ISeededRngProvider
    {
        /// <summary>
        /// The seed used the generate all the random values. 
        /// </summary>
        /// <remarks>This is intended to help with debugging as you can replicate the seed & get the same results as a user.</remarks>
        public static string Seed
        {
            get => DataAccess.GetAsset<DataAssetCartGlobalRuntimeSettings>().RngAleaRngSeed;
            set => DataAccess.GetAsset<DataAssetCartGlobalRuntimeSettings>().RngAleaRngSeed = value;
        }


        /// <summary>
        /// The random to call values from.
        /// </summary>
        private static readonly Alea R = new Alea(Seed);
        
        
        
        public bool Bool => Convert.ToBoolean(Int(0,1));
        
        
        public int Int(int min, int max)
        {
            return (int) Double(min, max);
        }

        
        public float Float(float min, float max)
        {
            return (float) Double(min, max);
        }
        

        public double Double(double min, double max)
        {
            var random = R.Next();
            return ((random - 0f) / (1f - 0)) * (max - min) + min;
        }
        
        
        public void GenerateSeed()
        {
            Seed = Guid.NewGuid().ToString();
        }
    }
}