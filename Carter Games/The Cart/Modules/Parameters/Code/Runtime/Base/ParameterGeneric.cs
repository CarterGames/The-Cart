#if CARTERGAMES_CART_MODULE_PARAMETERS

/*
 * Copyright (c) 2025 Carter Games
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

namespace CarterGames.Cart.Modules.Parameters
{
	/// <summary>
	/// A generic wrapper class for parameters.
	/// </summary>
	/// <typeparam name="T">The type for the parameter value.</typeparam>
	public abstract class ParameterGeneric<T> : Parameter
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Property Overrides
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets the value in object form.
		/// </summary>
		public override object ValueObject => Value;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Property Abstract
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The value of the parameter.
		/// </summary>
		public abstract T Value { get; }
	}
}

#endif