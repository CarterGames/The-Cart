#if CARTERGAMES_CART_CRATE_MULTISCENE && URP_ENABLED

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

using System.Linq;
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene.URP
{
    /// <summary>
    /// The overlay camera script for handling camera stacking in multi scene setups.
    /// </summary>
    [ExecuteInEditMode, RequireComponent(typeof(Camera))]
    public class MultiSceneOverlayCamera : MonoBehaviour, IMultiSceneAwake
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string camID;
        
        private Camera cameraCache;
        private bool setupRun;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the camera in the editor...
        /// </summary>
        public Camera GetCameraInEditor => GetComponent<Camera>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void Awake()
        {
            cameraCache = GetComponent<Camera>();
        }
        
        private void Start()
        {
            if (setupRun) return;
            SetCamera();
        }
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Multi Scene Listeners
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        public void OnMultiSceneAwake()
        {
            SetCamera();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Sets the camera to the base camera of the same ID when all scenes have loaded xD
        /// </summary>
        private void SetCamera()
        {
            var cameras = MultiSceneRef.GetComponentsFromAllScenes<MultiSceneBaseCamera>();

            if (cameras.Count <= 0) return;
            
            foreach (var cam in cameras.Where(t => t.CameraId.Equals(camID)))
            {
                cam.AddCamera(cameraCache);
            }

            setupRun = true;
        }
    }
}

#endif