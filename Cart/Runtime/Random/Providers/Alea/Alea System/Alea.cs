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