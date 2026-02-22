#if CARTERGAMES_CART_CRATE_MULTISCENE && URP_ENABLED

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