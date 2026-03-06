#if CARTERGAMES_CART_CRATE_PANELS

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
using UnityEngine.UI;

namespace CarterGames.Cart.Crates.Panels
{
    /// <summary>
    /// Handles the close logic for a panel using a UI button.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class PanelCloseButton : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private Panel panel;
        [SerializeField] private Button button;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        private void OnEnable()
        {
            TryGetReferences();
            
            button.onClick.RemoveListener(OnButtonPressed);
            button.onClick.AddListener(OnButtonPressed);
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        protected virtual void OnButtonPressed()
        {
            panel.ClosePanel();
        }


        private void TryGetReferences()
        {
            panel ??= GetComponentInParent<Panel>();
            button ??= GetComponentInChildren<Button>();
        }
    }
}

#endif