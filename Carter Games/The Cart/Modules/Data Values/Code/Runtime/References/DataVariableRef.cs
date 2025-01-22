#if CARTERGAMES_CART_MODULE_DATAVALUES

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
using UnityEngine;

namespace CarterGames.Cart.Modules.DataValues
{
    /// <summary>
    /// Use to reference a data value with the option to use a constant variant of the value in place of the variable. 
    /// </summary>
    /// <typeparam name="TValueType">The value type.</typeparam>
    /// <typeparam name="TDataValueType">The data value type.</typeparam>
    [Serializable]
    public class DataVariableRef<TValueType, TDataValueType> where TDataValueType : DataVariable<TValueType>
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private bool useConstant = false;
        [SerializeField] private TValueType constantValue;
        [SerializeField] private TDataValueType variable;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Makes a new reference.
        /// </summary>
        public DataVariableRef() { }


        /// <summary>
        /// Makes a new reference with the entered value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public DataVariableRef(TValueType value)
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
        public virtual TValueType Value
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
        public static implicit operator TValueType(DataVariableRef<TValueType, TDataValueType> variableRef)
        {
            return variableRef.Value;
        }
    }
}

#endif