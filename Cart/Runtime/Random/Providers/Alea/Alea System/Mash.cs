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
    public sealed class Mash
    {
        double n = 0xefc8249d;

        public double RunMash(string x)
        {
            var data = x;
            
            for (var i = 0; i < data.Length; i++)
            {
                n += data[i];
                
                var h = 0.02519603282416938 * n;
                n = (uint) h >> 0;
                h -= n;
                h *= n;
                n = (uint) h >> 0;
                h -= n;
                n += h * 0x100000000;
            }

            return ((uint) n >> 0) * 2.3283064365386963e-10;
        }
    }
}