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

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CarterGames.Cart.Crates.MultiScene.URP
{
    /// <summary>
    /// The base camera script for handling camera stacking in multi scene setups.
    /// </summary>
    [ExecuteInEditMode, RequireComponent(typeof(Camera))]
    public class MultiSceneBaseCamera : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private string camID;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the id of the base camera for comparing.
        /// </summary>
        public string CameraId => camID;
        
        /// <summary>
        /// Gets the camera on this base camera.
        /// </summary>
        public Camera BaseCamera { get; private set; }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void Awake()
        {
            BaseCamera = GetComponent<Camera>();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Adds a camera to this base camera stack.
        /// </summary>
        /// <param name="overlayCam">The overlay camera to add to the stack.</param>
        public void AddCamera(Camera overlayCam)
        {
            var cameraData = BaseCamera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(overlayCam);
        }
    }
}

#endif