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
using UnityEngine;
using System.IO;
using CarterGames.Cart.Logs;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace CarterGames.Cart
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
				
				CartLogger.LogError<CartLogs>($"Tried to set to {value}, but no scene could be located there.", typeof(SceneReference));
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