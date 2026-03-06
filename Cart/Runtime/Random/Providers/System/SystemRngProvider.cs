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
using CarterGames.Cart.Data;
using CarterGames.Cart.Management;

namespace CarterGames.Cart.Random
{
    public sealed class SystemRngProvider : ISeededRngProvider
    {
        /// <summary>
        /// The seed used the generate all the random values. 
        /// </summary>
        /// <remarks>This is intended to help with debugging as you can replicate the seed & get the same results as a user.</remarks>
        public static int Seed
        {
            get => DataAccess.GetAsset<DataAssetCoreRuntimeSettings>().RngSystemRngSeed;
            private set => DataAccess.GetAsset<DataAssetCoreRuntimeSettings>().RngSystemRngSeed = value;
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