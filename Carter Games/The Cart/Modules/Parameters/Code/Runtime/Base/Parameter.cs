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
	public abstract class Parameter
	{
		public virtual string Key => GetType().FullName;
		
		public abstract bool HasValue { get; }
		
		public abstract object ValueObject { get; }
		
		
		public string ValueString => HasValue 
			? ValueObject.ToString() 
			: string.Empty;
		
		
		public readonly Evt UpdatedEvt = new Evt();


		public void Initialize() => OnInitialize();
		protected virtual void OnInitialize() {}
		
		
		public void Dispose() => OnDisposedOf();
		protected virtual void OnDisposedOf() {}
	}
}

#endif