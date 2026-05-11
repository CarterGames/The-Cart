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
using SystemRng = System.Random;
using CarterGames.Cart.Data;
using CarterGames.Cart.Management;
using UnityEngine;

namespace CarterGames.Cart.Random
{
    public sealed class SystemRngProvider : ISeededRngProvider
    {
        private static DataAssetCoreRuntimeSettings Asset => DataAccess.GetAsset<DataAssetCoreRuntimeSettings>();
        private int instanceSeed = int.MinValue;
        private static SystemRng cacheSysRandom;

        
        /// <summary>
        /// The seed used to generate all the random values. 
        /// </summary>
        /// <remarks>This is intended to help with debugging as you can replicate the seed & get the same results as a user.</remarks>
        public string Seed
        {
            get => instanceSeed == int.MinValue ? string.Empty : instanceSeed.ToString();
            set => instanceSeed = int.Parse(value);
        }


        /// <summary>
        /// The random to call values from.
        /// </summary>
        private SystemRng Random => CacheRef.GetOrAssign(ref cacheSysRandom, new SystemRng(int.Parse(Rng.Seed)));


        public bool Bool => Convert.ToBoolean(Random.Next(1));
        
        
        public int Int(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
        

        public float Float(float min, float max)
        {
            return (float) (Random.NextDouble() * (max - min)) + min;
        }
        

        public double Double(double min, double max)
        {
            return (Random.NextDouble() * (max - min)) + min;
        }
        
        
        public string GenerateSeed()
        {
            return Guid.NewGuid().GetHashCode().ToString();
        }
    }
}