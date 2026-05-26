#if CARTERGAMES_CART_CRATE_DATAVALUES

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
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Cart.Crates.DataValues
{
	/// <summary>
	/// Use to reference a data value with the option to use a constant variant of the value in place of the variable. 
	/// </summary>
	/// <typeparam name="TValueType">The value type.</typeparam>
	/// <typeparam name="TDataValueType">The data value type.</typeparam>
	[Serializable]
    public class DataListRef<TValueType, TDataValueType> where TDataValueType : DataValueList<TValueType>
    {
	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Fields
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    [SerializeField] private bool useConstant = false;
	    [SerializeField] private List<TValueType> constantValue;
	    [SerializeField] private TDataValueType variable;

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Constructors
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
	    /// Makes a new reference.
	    /// </summary>
	    public DataListRef() { }


	    /// <summary>
	    /// Makes a new reference with the entered value.
	    /// </summary>
	    /// <param name="value">The value to set to.</param>
	    public DataListRef(List<TValueType> value)
	    {
		    useConstant = true;
		    constantValue = value;
	    }

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Properties
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
	    /// The variable assigned to the value.
	    /// </summary>
	    public TDataValueType Variable => variable;


	    /// <summary>
	    /// The value the reference is currently set to.
	    /// </summary>
	    public virtual List<TValueType> Value
	    {
		    get
		    {
			    return useConstant
				    ? constantValue
				    : Variable.Value;
		    }
		    set
		    {
			    if (useConstant)
			    {
				    constantValue = value;
			    }
			    else
			    {
				    Variable.SetValue(value);
			    }
		    }
	    }

	    /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
	    |   Operators
	    ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

	    /// <summary>
	    /// Converts the variable to the reference type.
	    /// </summary>
	    /// <param name="variableRef">The variable to read.</param>
	    /// <returns>The converted value.</returns>
	    public static implicit operator List<TValueType>(DataListRef<TValueType, TDataValueType> variableRef)
	    {
		    return variableRef.Value;
	    }
    }
}

#endif