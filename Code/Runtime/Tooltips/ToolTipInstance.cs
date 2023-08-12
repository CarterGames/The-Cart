/*
 * Copyright (c) 2018-Present Carter Games
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
using System.Collections;
using CarterGames.Common.Events;
using UnityEngine;


namespace CarterGames.Common.Tooltips
{
    /// <summary>
    /// Handles a tooltip in the game world.
    /// </summary>
    public class ToolTipInstance : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private bool followMouse;
        [SerializeField] private TooltipPosition position;

        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private RectTransform parentRect;
        [SerializeField] private RectTransform rectTransform;
        
        private Coroutine updateRoutine;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the id of the tooltip.
        /// </summary>
        public string Id { get; private set; } = Guid.NewGuid().ToString();


        /// <summary>
        /// Gets if the tooltip is active or not.
        /// </summary>
        public bool IsActive { get; private set; }


        /// <summary>
        /// Gets the mouse position, based on the input system in use.
        /// </summary>
        private static Vector2 MousePos
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return Mouse.current.position.ReadValue<Vector2>();
#elif ENABLE_LEGACY_INPUT_MANAGER
                return Input.mousePosition;
#endif
            }
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Raises when the tooltip is toggled on/off.
        /// </summary>
        public readonly Evt<ToolTipInstance> Toggled = new Evt<ToolTipInstance>();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void OnEnable()
        {
            TooltipManager.RegisterTooltip(this);
            Enable();
        }


        private void OnDisable()
        {
            Disable();
        }


        private void OnDestroy()
        {
            TooltipManager.UnregisterTooltip(this);
        }


        private void OnValidate()
        {
            if (Id.Length > 0) return;
            Id = Guid.NewGuid().ToString();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Enables the tooltip when called.
        /// </summary>
        public void Enable()
        {
            IsActive = true;
            StartUpdate();
            Toggled.Raise(this);
        }


        /// <summary>
        /// Disables the tooltip when called.
        /// </summary>
        public void Disable()
        {
            StopUpdate();
            IsActive = false;
            Toggled.Raise(this);
        }


        /// <summary>
        /// Starts the updating for the tooltip position.
        /// </summary>
        private void StartUpdate()
        {
            if (updateRoutine != null)
            {
                StopCoroutine(updateRoutine);
                updateRoutine = null;
            }
            
            updateRoutine = StartCoroutine(Co_UpdateLoop());
        }


        /// <summary>
        /// Stops the updating for the tooltip position.
        /// </summary>
        private void StopUpdate()
        {
            if (updateRoutine == null) return;
            StopCoroutine(updateRoutine);
            updateRoutine = null;
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Coroutines
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Updates the tooltip position on a loop when called.
        /// </summary>
        private IEnumerator Co_UpdateLoop()
        {
            if (!IsActive)
            {
                updateRoutine = null;
                yield break;
            }

            if (followMouse)
            {
                var pos = MousePos / canvas.scaleFactor;

                switch (position)
                {
                    case TooltipPosition.BottomRight:
                        pos += new Vector2(-(parentRect.rect.width / 2), (parentRect.rect.height / 2));
                        break;
                    case TooltipPosition.TopRight:
                        pos += new Vector2(-(parentRect.rect.width / 2), -(parentRect.rect.height / 2));
                        break;
                    case TooltipPosition.BottomLeft:
                        pos += new Vector2((parentRect.rect.width / 2), (parentRect.rect.height / 2));
                        break;
                    case TooltipPosition.TopLeft:
                        pos += new Vector2((parentRect.rect.width / 2), -(parentRect.rect.height / 2));
                        break;
                    case TooltipPosition.Center:
                        break;
                }

                pos.x = Mathf.Clamp(pos.x, parentRect.rect.width / 2,
                    canvasRect.rect.width - (parentRect.rect.width / 2));
                pos.y = Mathf.Clamp(pos.y, parentRect.rect.height / 2,
                    canvasRect.rect.height - (parentRect.rect.height / 2));

                rectTransform.anchoredPosition = pos;


                yield return null;
                updateRoutine = StartCoroutine(Co_UpdateLoop());
            }
            else
            {
                rectTransform.anchoredPosition = MousePos / canvas.scaleFactor;
            }
        }
    }
}