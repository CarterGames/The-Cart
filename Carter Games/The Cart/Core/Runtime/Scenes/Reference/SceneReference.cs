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
using UnityEngine;
using System.IO;
using CarterGames.Cart.Core.Logs;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace CarterGames.Cart.Core
{
	[Serializable]
	public class SceneReference : ISerializationCallbackReceiver
	{
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Fields
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
#if UNITY_EDITOR
		[SerializeField] private SceneAsset sceneAssetRef;
		
#pragma warning disable 0414
		[SerializeField] private bool isDirty;
#pragma warning restore 0414
#endif
		
		[SerializeField] private string scenePath = string.Empty;

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Properties
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public string ScenePath
		{
			get
			{
#if UNITY_EDITOR
				AutoUpdateReference();
#endif
				return scenePath;
			}
			set
			{
				scenePath = value;
#if UNITY_EDITOR
				if (string.IsNullOrEmpty(scenePath))
				{
					sceneAssetRef = null;
					return;
				}

				sceneAssetRef = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

				if (sceneAssetRef != null) return;
				
				CartLogger.LogError<LogCategoryCore>($"Tried to set to {value}, but no scene could be located there.", typeof(SceneReference));
#endif
			}
		}


		public string SceneName => Path.GetFileNameWithoutExtension(ScenePath);

		public bool IsEmpty => string.IsNullOrEmpty(ScenePath);
		
		public SceneReference Clone() => new SceneReference(this);
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Constructors
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public SceneReference() { }

		
		public SceneReference(string scenePath)
		{
			ScenePath = scenePath;
		}

		
		public SceneReference(SceneReference other)
		{
			scenePath = other.scenePath;

#if UNITY_EDITOR
			sceneAssetRef = other.sceneAssetRef;
			isDirty = other.isDirty;

			AutoUpdateReference();
#endif
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   ToString Override
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public override string ToString()
		{
			return scenePath;
		}
		
		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   ISerializationCallbackReceiver Implementation
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
		public void OnBeforeSerialize()
		{
#if UNITY_EDITOR
			AutoUpdateReference();
#endif
		}
		
		
		public void OnAfterDeserialize()
		{
#if UNITY_EDITOR
			EditorApplication.update += OnAfterDeserializeHandler;
#endif
		}

		/* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
		|   Methods
		───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
		
#if UNITY_EDITOR
		private void OnAfterDeserializeHandler()
		{
			EditorApplication.update -= OnAfterDeserializeHandler;
			AutoUpdateReference();
		}
		
		
		private void AutoUpdateReference()
		{
			if (sceneAssetRef == null)
			{
				if (string.IsNullOrEmpty(scenePath)) return;

				var foundAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
				
				if (!foundAsset) return;
				
				sceneAssetRef = foundAsset;
				isDirty = true;

				if (Application.isPlaying) return;

				EditorSceneManager.MarkAllScenesDirty();
			}
			else
			{
				var foundPath = AssetDatabase.GetAssetPath(sceneAssetRef);
				
				if (string.IsNullOrEmpty(foundPath)) return;

				if (foundPath == scenePath) return;
				
				scenePath = foundPath;
				isDirty = true;

				if (Application.isPlaying) return;
				
				EditorSceneManager.MarkAllScenesDirty();
			}
		}
#endif
	}
}