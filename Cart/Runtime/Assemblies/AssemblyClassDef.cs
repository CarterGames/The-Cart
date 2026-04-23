/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

using System;
using System.Linq;
using CarterGames.Cart.Logs;
using UnityEngine;

namespace CarterGames.Cart
{
	/// <summary>
	/// A class for storing info about a class so it can be referenced from its assembly and type names.
	/// </summary>
	[Serializable]
	public sealed class AssemblyClassDef
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		[SerializeField] private string assembly;
		[SerializeField] private string type;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets if the class is valid or not.
		/// </summary>
		public bool IsValid => !string.IsNullOrEmpty(assembly) && !string.IsNullOrEmpty(type);

		
		/// <summary>
		/// The assembly string stored.
		/// </summary>
		public string StoredAssembly => assembly;
		
		
		/// <summary>
		/// The type string stored.
		/// </summary>
		public string StoredType => type;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Creates a new definition when called.
		/// </summary>
		/// <param name="storedAssembly">The assembly to reference.</param>
		/// <param name="type">The type to reference.</param>
		public AssemblyClassDef(string storedAssembly, string type)
		{
			this.assembly = storedAssembly;
			this.type = type;
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Converts System.Type to a AssemblyClassDef instance.
		/// </summary>
		/// <param name="type">The type to convert.</param>
		/// <returns>The created AssemblyClassDef from the type.</returns>
		public static implicit operator AssemblyClassDef(Type type)
		{
			return new AssemblyClassDef(type.Assembly.FullName, type.FullName);
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Gets the type stored in this AssemblyClassDef.
		/// </summary>
		/// <typeparam name="T">The type to make.</typeparam>
		/// <returns>The made type or the types default on failure.</returns>
		public T GetDefinedType<T>()
		{
			if (!IsValid)
			{
				CartLogger.LogError<LogCategoryCart>(
					"[GetDefinedType]: Data not valid to generate the defined type", typeof(AssemblyClassDef));
				return default;
			}
			
			try
			{
				return AssemblyHelper.GetClassesOfType<T>().FirstOrDefault(t =>
					t.GetType().Assembly.FullName == StoredAssembly && t.GetType().FullName == StoredType);
			}
#pragma warning disable 0168
			catch (Exception e)
			{
				CartLogger.LogError<LogCategoryCart>(
					"[GetDefinedType]: Failed to generate type from stored data. If you have refactored the class selected, please reselect it to update the record.", typeof(AssemblyClassDef));

				return default;
			}
#pragma warning restore
		}


		/// <summary>
		/// Gets if a type is the same as this assembly class define.
		/// </summary>
		/// <param name="type">The type to compare</param>
		/// <returns>bool</returns>
		public bool IsDefineType(Type type)
		{
			return StoredAssembly == type.Assembly.FullName && StoredType == type.FullName;
		}

		
		/// <summary>
		/// Gets if the type entered is a base class of the stored value.
		/// </summary>
		/// <param name="type">The type to compare</param>
		/// <returns>Bool</returns>
		public bool InheritsFrom(Type type)
		{
			return Type.GetType(StoredAssembly + StoredType)!.IsAssignableFrom(type);
		}
	}
}