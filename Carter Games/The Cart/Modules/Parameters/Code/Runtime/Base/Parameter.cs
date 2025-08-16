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

using CarterGames.Cart.Core.Events;

namespace CarterGames.Cart.Modules.Parameters
{
	/// <summary>
	/// The base class for a parameter in the parameters setup.
	/// </summary>
	public abstract class Parameter
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// The key for the parameter.
		/// </summary>
		public virtual string Key => GetType().FullName;
		
		
		/// <summary>
		/// Gets if the value is populated.
		/// </summary>
		public abstract bool HasValue { get; }
		
		
		/// <summary>
		/// Gets the value in object form.
		/// </summary>
		public abstract object ValueObject { get; }
		
		
		/// <summary>
		/// Gets the value in string form.
		/// </summary>
		public string ValueString => HasValue 
			? ValueObject.ToString() 
			: string.Empty;
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Events
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Raises when the parameter value is updated.
		/// </summary>
		public readonly Evt UpdatedEvt = new Evt();

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Initializes the parameter when called.
		/// </summary>
		public void Initialize() => OnInitialize();
		
		
		/// <summary>
		/// Disposes of the parameter when called.
		/// </summary>
		public void Dispose() => OnDisposedOf();
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods (Overridable)
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Override to add logic on initializing the parameter.
		/// </summary>
		protected virtual void OnInitialize() {}
		
		
		/// <summary>
		/// Override to add logic on disposing the parameter.
		/// </summary>
		protected virtual void OnDisposedOf() {}
	}
}

#endif