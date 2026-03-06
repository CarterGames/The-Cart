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

namespace CarterGames.Cart.Random
{
    public sealed class UnityRngProvider : IRngProvider
    {
        public bool Bool => UnityEngine.Random.Range(0, 2).Equals(1);
        
        
        public int Int(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
        

        public float Float(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        

        public double Double(double min, double max)
        {
            return UnityEngine.Random.Range((float) min, (float) max);
        }
    }
}