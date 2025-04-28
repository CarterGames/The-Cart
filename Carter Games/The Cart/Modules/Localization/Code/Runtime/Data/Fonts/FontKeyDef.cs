using System;
using TMPro;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization
{
    [Serializable]
    public struct FontKeyDef
    {
        [SerializeField] private TMP_FontAsset fontAsset;
        [SerializeField] private Material fontMaterial;


        public TMP_FontAsset Font => fontAsset;
        public Material Material => fontMaterial;
        public bool HasMaterial => fontMaterial != null;


        public static FontKeyDef FromFontDef(LocalizationFontDefinition data)
        {
            return new FontKeyDef()
            {
                fontAsset = data.Font,
                fontMaterial = data.FontMaterial
            };
        }
        
        
        public static FontKeyDef FromElements(TMP_FontAsset fontAsset, Material material)
        {
            return new FontKeyDef()
            {
                fontAsset = fontAsset,
                fontMaterial = material
            };
        }


        public static FontKeyDef FromTMPComponent(TMP_Text component)
        {
            var data = new FontKeyDef()
            {
                fontAsset = component.font,
            };

            if (component.defaultMaterial != component.fontMaterial)
            {
                data.fontMaterial = component.fontMaterial;
            }

            return data;
        }
    }
}