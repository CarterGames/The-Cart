using CarterGames.Cart.Core.Data;
using UnityEngine;

namespace CarterGames.Cart.Modules.Hierarchy.Editor
{
    public sealed class DataAssetHierarchySettings : DataAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private HierarchyHeaderSeparatorConfig headerSeparatorConfig = new HierarchyHeaderSeparatorConfig();
        [SerializeField] private HierarchyAlternateLinesConfig alternateLinesConfig = new HierarchyAlternateLinesConfig();
        [SerializeField] private HierarchyNotesConfig notesConfig = new HierarchyNotesConfig();
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public HierarchyHeaderSeparatorConfig HeaderSeparatorConfig => headerSeparatorConfig;
        public HierarchyAlternateLinesConfig AlternateLinesConfig => alternateLinesConfig;
        public HierarchyNotesConfig NotesConfig => notesConfig;
    }
}