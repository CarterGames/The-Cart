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

namespace CarterGames.Cart.Core
{
	/// <summary>
	/// Handles signalling mono behaviour events out for systems to use without a mono base class.
	/// </summary>
	public static class MonoEvents
	{
		/// <summary>
		/// Raises on Mono Awake.
		/// </summary>
		public static readonly Evt Awake = new Evt();
		
		
		/// <summary>
		/// Raises on Mono Start.
		/// </summary>
		public static readonly Evt Start = new Evt();
		
		
		/// <summary>
		/// Raises on Mono OnDestroy (of the mono instance object in DND).
		/// </summary>
		public static readonly Evt OnDestroy = new Evt();
		
		
		/// <summary>
		/// Raises on Application Quit.
		/// </summary>
		public static readonly Evt ApplicationQuit = new Evt();
	}
}