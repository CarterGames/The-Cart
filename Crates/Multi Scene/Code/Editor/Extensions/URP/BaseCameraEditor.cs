#if CARTERGAMES_CART_CRATE_MULTISCENE && URP_ENABLED && UNITY_EDITOR

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

using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CarterGames.Cart.Crates.MultiScene.URP.Editor
{
    /// <summary>
    /// An editor override for the base camera inspector to add to extra editor buttons where needed.
    /// </summary>
    [CustomEditor(typeof(MultiSceneBaseCamera))]
    public class BaseCameraEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = !Application.isPlaying;

            if (GUILayout.Button("Add Overlay In Editor"))
            {
                var overlayCamera = MultiSceneRef.GetComponentsFromAllScenes<MultiSceneOverlayCamera>();
                var baseCamera = (MultiSceneBaseCamera)target;
                
                foreach (var cam in overlayCamera)
                {
                    baseCamera.GetComponent<Camera>().GetUniversalAdditionalCameraData().cameraStack.Add(cam.GetCameraInEditor);
                }
            }
        }
    }
}

#endif