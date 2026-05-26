#if CARTERGAMES_CART_CRATE_CONDITIONS && UNITY_EDITOR

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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarterGames.Cart.Editor;
using CarterGames.Cart.Logs;
using CarterGames.Cart.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Conditions
{
	/// <summary>
	/// Handles generating the constants class for conditions.
	/// </summary>
	public static class ConditionsConstantGenerator
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		private static TextWriter fileWriter;

		private const string ConstantClassPath =
			"Assets/Plugins/Carter Games/The Cart/Data/Crates/Conditions/Constants/";

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		/// <summary>
		/// Generates the constants class when called.
		/// </summary>
		/// <param name="conditions">The conditions to pass in as constants.</param>
		public static void Generate(IEnumerable<Condition> conditions)
		{
			var classInstance = AssetDatabase.FindAssets($"t:Script ConditionIds");

			if (classInstance == null) return;

			if (classInstance.Length <= 0)
			{
				FileEditorUtil.CreateToDirectory(ConstantClassPath);
				CreateAssetFile();
				return;
			}
			
			var classPath = AssetDatabase.GUIDToAssetPath(classInstance[0]);

			if (string.IsNullOrEmpty(classPath)) return;

			using (fileWriter = new StreamWriter(classPath))
			{
				ClassCreateHelper.ApplyHeader(fileWriter, "CARTERGAMES_CART_CRATE_CONDITIONS",
					"CarterGames.Cart.Crates.Conditions", "ConditionIds");
				
				
				foreach (var condition in conditions)
				{
					if (string.IsNullOrEmpty(condition.Id) || string.IsNullOrEmpty(condition.VariantId))
					{
						CartLogger.LogWarning<ConditionsLogs>("Unable to add id to constant as its key or variant id was empty.");
						continue;
					}

					var key = condition.Id.Replace(" ", "").Trim();
					var value = condition.VariantId.Replace(" ", "").Trim();
					
					ClassCreateHelper.ApplyLine(fileWriter, key, value);
				}
			
				ClassCreateHelper.ApplyFooter(fileWriter);
				
				EditorUtility.SetDirty(AssetDatabase.LoadAssetAtPath<TextAsset>(ConstantClassPath + "ConditionIds.cs"));
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
		
		
		/// <summary>
		/// Creates the asset file for the data class the user is making.
		/// </summary>
		private static void CreateAssetFile()
		{
			var script = AssetDatabase.FindAssets($"t:Script {nameof(ConditionManager)}")[0];
			var pathToTextFile = AssetDatabase.GUIDToAssetPath(script);
			pathToTextFile = pathToTextFile.Replace("ConditionManager.cs", "Constants/ConditionIdsClassTemplate.txt");
            
			TextAsset template = AssetDatabase.LoadAssetAtPath<TextAsset>(pathToTextFile);
			template = new TextAsset(template.text);

			File.WriteAllText(ConstantClassPath + "ConditionIds.cs", template.text);
			File.WriteAllText(ConstantClassPath + "CarterGames.Cart.Crates.ConditionsReference.asmref", "{\n    \"reference\": \"GUID:92e9c72da1b1cfb25ab8b0cbe24f956e\"\n}");
			
			EditorApplication.delayCall -= InternalGenerateConstantClassRecall;
			EditorApplication.delayCall += InternalGenerateConstantClassRecall;
			
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		
		/// <summary>
		/// Re-calls to generate the class if after the editor delay call is passed should it be needed.
		/// </summary>
		private static void InternalGenerateConstantClassRecall()
		{
			EditorApplication.delayCall -= InternalGenerateConstantClassRecall;
			Generate(AssetDatabaseHelper.GetAllInstancesInProject<Condition>());
		}
	}
}

#endif