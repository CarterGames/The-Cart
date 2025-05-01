#if CARTERGAMES_CART_MODULE_CONDITIONS && UNITY_EDITOR

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
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using System.IO;
using CarterGames.Cart.Core.Editor;
using CarterGames.Cart.Core.Logs;
using CarterGames.Cart.Core.Management.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
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
			"Assets/Plugins/Carter Games/The Cart/Data/Modules/Conditions/Contants/";

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
				ClassCreateHelper.ApplyHeader(fileWriter, "CARTERGAMES_CART_MODULE_CONDITIONS",
					"CarterGames.Cart.Modules.Conditions", "ConditionIds");
			
				foreach (var condition in conditions)
				{
					if (string.IsNullOrEmpty(condition.Id) || string.IsNullOrEmpty(condition.VariantId))
					{
						CartLogger.LogWarning<LogCategoryConditions>("Unable to add id to constant as its key or variant id was empty.");
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
			File.WriteAllText(ConstantClassPath + "CarterGames.Cart.Modules.ConditionsReference.asmref", "{\n    \"reference\": \"GUID:3bc014fde37452a449efc85866cb18c3\"\n}");
			
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