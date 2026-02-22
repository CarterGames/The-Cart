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

using CarterGames.Cart.Editor;

namespace CarterGames.Cart.Crates
{
	/// <summary>
	/// The definition for the color folders' crate.
	/// </summary>
	public sealed class CrateColorFolders : Crate
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
		/// <summary>
		/// The name of the crate.
		/// </summary>
		public override string CrateName => "Color Folders";

		
		/// <summary>
		/// A description of what the crate does.
		/// </summary>
		public override string CrateDescription =>
			"An editor improvement to lets you colorize folders, you do sacrifice folder context to apply these colors.";

		
		/// <summary>
		/// The author of the crate.
		/// </summary>
		public override string CrateAuthor => "Carter Games";
		
		
		/// <summary>
		/// The scripting define the crate uses.
		/// </summary>
		public override string CrateDefine => "CARTERGAMES_CART_CRATE_COLORFOLDERS";


		public override EditorUrl[] CrateLinks => new EditorUrl[1]
		{
			EditorUrl.LocalFile("Documentation", "Docs_Module_ColorFolders_Usage.pdf")
		};
	}
}