#if CARTERGAMES_CART_CRATE_ANALYTICS

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

using System.Collections.Generic;

namespace CarterGames.Cart.Crates.Analytics
{
    public abstract class AnalyticsEvent
    {
        private List<KeyValuePair<string, object>> additionalParameters = new List<KeyValuePair<string, object>>();
        
        public abstract string EventName { get; }
        
        protected abstract List<KeyValuePair<string, object>> MainParameters();
        
        
        public void AddAdditionalParameters(params KeyValuePair<string,object>[] parameters) 
        {
            additionalParameters.AddRange(parameters);
        }

        public List<KeyValuePair<string, object>> CompileParameters()
        {
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.AddRange(MainParameters());
            parameters.AddRange(additionalParameters);
            return parameters;
        }
    }
}

#endif