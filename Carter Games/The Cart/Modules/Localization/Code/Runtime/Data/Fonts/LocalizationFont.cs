using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    [Serializable]
    public struct LocalizationFont
    {
        [SerializeField] private string displayName;
        [SerializeField] private LocalizationFontDefinition[] data;

        
        public bool HasDefaultSetup => data.Any(t => t.Language.Code == LocalizationManager.CurrentLanguage.Code);
		
        public LocalizationFontDefinition DefaultLanguageData =>
            data.FirstOrDefault(t => t.Language.Code == Language.Default.Code);
        
        public LocalizationFontDefinition[] Data => data;
    }
}