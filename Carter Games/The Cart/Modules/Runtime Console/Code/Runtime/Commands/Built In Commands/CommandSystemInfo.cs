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

using System.Text;
using CarterGames.Cart.Core;
using UnityEngine;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
	/// <summary>
	/// A command to get some system info of the current device the game is running on.
	/// </summary>
	public sealed class CommandSystemInfo : IRuntimeConsoleCommand
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   IRuntimeConsoleCommand Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public bool AllowInProduction => true;
		
		public string Key => "sysinfo";
		
		
		public string OnExecutedMessage()
		{
			var info = new StringBuilder();

			info.Append("Printing user system info...");
			info.AppendLine();
			info.AppendLine();
			info.Append("<color=#eee600>Name:</color> ");
			info.Append(SystemInfo.deviceName);
			info.AppendLine();
			info.Append("<color=#eee600>OS:</color> ");
			info.Append(SystemInfo.deviceType);
			info.Append(" | ");
			info.Append(SystemInfo.operatingSystem);
			info.AppendLine();
			info.Append("<color=#eee600>CPU:</color> ");
			info.Append(SystemInfo.processorType);
			info.Append(" | ");
			info.Append($"{((float) SystemInfo.processorFrequency / 1000):N2}GHz");
			info.Append(" | ");
			info.Append($"{SystemInfo.processorCount} threads");
			info.AppendLine();
			info.Append("<color=#eee600>RAM:</color> ");
			info.Append($"{SystemInfo.systemMemorySize.RoundToPower(2) / 1000:N0}GB");
			info.AppendLine();
			info.Append("<color=#eee600>GPU:</color> ");
			info.Append(SystemInfo.graphicsDeviceName);
			info.Append(" | ");
			info.Append($"{SystemInfo.graphicsMemorySize.RoundToPower(2) / 1000:N0}GB V-RAM");
			
			return info.ToString();
		}

		
		public void OnCommandExecuted() { }
	}
}

#endif