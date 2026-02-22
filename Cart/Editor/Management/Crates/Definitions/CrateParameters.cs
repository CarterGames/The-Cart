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

using System;

namespace CarterGames.Cart.Crates
{
	/// <summary>
	/// The definition for the parameters crate
	/// </summary>
	public class CrateParameters : Crate
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
		/// <summary>
		/// The name of the crate.
		/// </summary>
		public override string CrateName => "Parameters";


		/// <summary>
		/// A description of what the crate does.
		/// </summary>
		public override string CrateDescription =>
			"A class based setup to define generic parameters that can be used for a variety of applications without needing a rigid data structure.";
        
		
		/// <summary>
		/// The author of the crate.
		/// </summary>
		public override string CrateAuthor => "Carter Games";
        
        
		/// <summary>
		/// The scripting define the crate uses.
		/// </summary>
		public override string CrateDefine => "CARTERGAMES_CART_CRATE_PARAMETERS";
	}
}