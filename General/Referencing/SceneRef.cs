// ----------------------------------------------------------------------------
// SceneRef.cs
// 
// Description: A helper class for getting references to other scripts.
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Adriana
{
    public static class SceneRef
    {
        /// <summary>
        /// Gets any and all of the type requested from the active scene...
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>List of any instances of the type found in the scene</returns>
        public static List<T> GetComponentsFromScene<T>()
        {
            var _objects = new List<GameObject>();
            var _scene = SceneManager.GetActiveScene();
            var _validObjectsFromScene = new List<T>();
            
            _scene.GetRootGameObjects(_objects);
            
            foreach (var _go in _objects)
                _validObjectsFromScene.AddRange(_go.GetComponentsInChildren<T>(true));

            return _validObjectsFromScene;
        }


        /// <summary>
        /// Gets the first of any and all of the type requested from the active scene...
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns>First instance of the type found in the active scene</returns>
        public static T GetComponentFromScene<T>()
        {
            var _allOfType = GetComponentsFromScene<T>();

            return _allOfType.Count > 0 
                ? _allOfType[0] 
                : default;
        }
    }
}