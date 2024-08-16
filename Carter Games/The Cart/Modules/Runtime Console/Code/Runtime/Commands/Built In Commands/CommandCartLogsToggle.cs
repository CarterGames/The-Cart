#if CARTERGAMES_CART_MODULE_RUNTIMECONSOLE

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

using CarterGames.Cart.Core.Data;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// A command to enable cart logs in the console.
	/// </summary>
	public sealed class CommandCartLogsEnable : IRuntimeConsoleCommand
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IRuntimeConsoleCommand Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public bool AllowInProduction => true;
		
		
		public string Key => "cartlogs enable";
		
		
		public string OnExecutedMessage()
		{
			return "Cart logs messages enabled";
		}
		

		public void OnCommandExecuted()
		{
			DataAccess.GetAsset<DataAssetRuntimeConsole>().ShowCartLogs = true;
		}
	}
	
	
	/// <summary>
	/// A command to disable cart logs in the console.
	/// </summary>
	public sealed class CommandCartLogsDisable : IRuntimeConsoleCommand
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IRuntimeConsoleCommand Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public bool AllowInProduction => true;
		
		
		public string Key => "cartlogs disable";
		
		
		public string OnExecutedMessage()
		{
			return "Cart logs messages disabled";
		}
		

		public void OnCommandExecuted()
		{
			DataAccess.GetAsset<DataAssetRuntimeConsole>().ShowCartLogs = false;
		}
	}
}

#endif