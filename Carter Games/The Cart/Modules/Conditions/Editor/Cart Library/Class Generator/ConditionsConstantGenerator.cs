#if CARTERGAMES_CART_MODULE_CONDITIONS

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
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Conditions
{
	public static class ConditionsConstantGenerator
	{
		private static TextWriter fileWriter;


		public static void Generate(IEnumerable<Condition> conditions)
		{
			var classInstance = AssetDatabase.FindAssets($"t:Script {nameof(ConditionIds)}");

			if (classInstance == null) return;
			var classPath = AssetDatabase.GUIDToAssetPath(classInstance[0]);

			if (string.IsNullOrEmpty(classPath)) return;

			Debug.Log(classPath);

			using (fileWriter = new StreamWriter(classPath))
			{
				ClassCreateHelper.ApplyHeader(fileWriter, "CARTERGAMES_CART_MODULE_CONDITIONS",
					"CarterGames.Cart.Modules.Conditions", nameof(ConditionIds));
			
				foreach (var condition in conditions)
				{
					if (string.IsNullOrEmpty(condition.Id) || string.IsNullOrEmpty(condition.VariantId))
					{
						CartLogger.LogWarning<LogCategoryModules>("[Conditions]: Unable to add id to constant as its key or variant id was empty.");
						continue;
					}

					var key = condition.Id.Replace(" ", "").Trim();
					var value = condition.VariantId.Replace(" ", "").Trim();
					
					ClassCreateHelper.ApplyLine(fileWriter, key, value);
				}
			
				ClassCreateHelper.ApplyFooter(fileWriter);
			}
		}
	}
}

#endif