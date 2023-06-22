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

using System;
using Scarlet.Management;

namespace Scarlet.Random
{
    public class SystemRngProvider : ISeededRngProvider
    {
        /// <summary>
        /// The seed used the generate all the random values. 
        /// </summary>
        /// <remarks>This is intended to help with debugging as you can replicate the seed & get the same results as a user.</remarks>
        public static int Seed
        {
            get => ScarletLibraryAssetAccessor.GetAsset<ScarletLibraryRuntimeSettings>().SystemRngSeed;
            set => ScarletLibraryAssetAccessor.GetAsset<ScarletLibraryRuntimeSettings>().SystemRngSeed = value;
        }


        /// <summary>
        /// The random to call values from.
        /// </summary>
        private static readonly System.Random R = new System.Random(Seed);
        
        
        
        public bool Bool => Convert.ToBoolean(R.Next(1));
        
        
        public int Int(int min, int max)
        {
            return R.Next(min, max + 1);
        }
        

        public float Float(float min, float max)
        {
            return (float) (R.NextDouble() * (max - min)) + min;
        }
        

        public double Double(double min, double max)
        {
            return (R.NextDouble() * (max - min)) + min;
        }
        
        
        public void GenerateSeed()
        {
            Seed = Guid.NewGuid().GetHashCode();
        }
    }
}