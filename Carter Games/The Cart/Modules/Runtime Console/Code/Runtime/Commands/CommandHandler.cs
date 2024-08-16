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

using System;
using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core;
using CarterGames.Cart.Core.Management;

namespace CarterGames.Cart.Modules.RuntimeConsole
{
    /// <summary>
    /// Handles storing a cache of all the commands the user can use.
    /// </summary>
    public static class CommandHandler
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Dictionary<Type, IRuntimeConsoleCommand> commandLookupCache;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private static Dictionary<Type, IRuntimeConsoleCommand> CommandLookup =>
            CacheRef.GetOrAssign(ref commandLookupCache, GetCommandsByType);

        private static Dictionary<Type, IRuntimeConsoleCommand> GetCommandsByType()
        {
            var lookup = new Dictionary<Type, IRuntimeConsoleCommand>();
            var commands = AssemblyHelper.GetClassesOfType<IRuntimeConsoleCommand>();

            foreach (var command in commands)
            {
                lookup.Add(command.GetType(), command);
            }

            return lookup;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Creates a new formatter instance if one doesn't exist at the time of requesting.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The formatter to use.</returns>
        private static IRuntimeConsoleCommand Create<T>() where T : IRuntimeConsoleCommand
        {
            var formatter = Activator.CreateInstance<T>();
            CommandLookup.Add(typeof(T), formatter);
            return formatter;
        }


        /// <summary>
        /// Gets or make the formatter requested for use.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>The formatter to use.</returns>
        public static void Get<T>() where T : IRuntimeConsoleCommand
        {
            var command = CommandLookup.ContainsKey(typeof(T))
                ? CommandLookup[typeof(T)]
                : Create<T>();
        }


        /// <summary>
        /// Gets a command by its string key.
        /// </summary>
        /// <param name="commandLine">The line to check.</param>
        /// <returns>The command if it exists.</returns>
        public static IRuntimeConsoleCommand Get(string commandLine)
        {
            return CommandLookup.FirstOrDefault(t => t.Value.Key.Equals(commandLine)).Value;
        }
    }
}

#endif