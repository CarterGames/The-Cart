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
using UnityEngine;

namespace CarterGames.Cart.Random
{
    public sealed class AleaRngProvider : ISeededRngProvider
    {
        private static DataAssetCoreRuntimeSettings Asset => DataAccess.GetAsset<DataAssetCoreRuntimeSettings>();
        private string instanceSeed = string.Empty;
        private static Alea cacheAlea;


        /// <summary>
        /// The seed used to generate all the random values. 
        /// </summary>
        /// <remarks>This is intended to help with debugging as you can replicate the seed & get the same results as a user.</remarks>
        public string Seed
        {
            get => instanceSeed;
            set => instanceSeed = value;
        }


        /// <summary>
        /// The random to call values from.
        /// </summary>
        private Alea Random => CacheRef.GetOrAssign(ref cacheAlea, new Alea(Rng.Seed));


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
            var random = Random.Next();
            return ((random - 0f) / (1f - 0)) * (max - min) + min;
        }
        
        
        public string GenerateSeed()
        {
            return Guid.NewGuid().ToString();
        }
    }
}