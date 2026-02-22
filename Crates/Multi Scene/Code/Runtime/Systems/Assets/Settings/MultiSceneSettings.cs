#if CARTERGAMES_CART_CRATE_MULTISCENE

using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Data;
using UnityEngine;

namespace CarterGames.Cart.Crates.MultiScene
{
    public class MultiSceneSettings : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private SceneGroupEditorLoadMode sceneGroupLoadMode;
        [SerializeField] private SceneGroup startGroup;
        [SerializeField] private SceneGroup lastGroupLoaded;

#pragma warning disable 0414
        [SerializeField] private bool showToolbar = true;
#pragma warning restore
        
        [SerializeField] private int listenerFrequency = 5;
        [SerializeField] private bool useUnloadResources;

        [SerializeField, HideInInspector] private bool showSceneGroupOptions;
        [SerializeField, HideInInspector] private bool showGeneralOptions;
        [SerializeField, HideInInspector] private bool showGroupCategoryOptions;
        [SerializeField, HideInInspector] private bool showDefaultGroupsInSetAsset;
        [SerializeField, HideInInspector] private bool showUserGroupsInSetAsset;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The scene group to load first.
        /// </summary>
        public SceneGroup StartGroup => startGroup;
        
        /// <summary>
        /// How many of the listeners of IMultiSceneAwake/Enable/Start invoke per frame.
        /// </summary>
        public int ListenerFrequency => listenerFrequency;
        
        /// <summary>
        /// Defines if the Resources.UnloadUnusedAssets is used when changing scene groups.
        /// </summary>
        public bool UseUnloadResources => useUnloadResources;
        
        
        /// <summary>
        /// The load mode to use when loading scene groups.
        /// </summary>
        public SceneGroupEditorLoadMode LoadMode => sceneGroupLoadMode;

        
        /// <summary>
        /// The last scene group loaded if saved.
        /// </summary>
        public SceneGroup LastGroup
        {
            get => lastGroupLoaded;
            set => lastGroupLoaded = value;
        }
    }
}

#endif