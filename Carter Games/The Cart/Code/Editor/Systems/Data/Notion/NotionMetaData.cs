using UnityEngine;

namespace CarterGames.Cart.Data.Notion
{
    public static class NotionMetaData
    {
        public static readonly GUIContent DefaultApiKey =
            new GUIContent(
                "API key",
                "The default API key to use for notion data asset downloads unless overriden on the asset.");
        
        
        public static readonly GUIContent ApiKey =
            new GUIContent(
                "API key",
                "The API key to use with the download.");
        
        
        public static readonly GUIContent DatabaseLink =
            new GUIContent(
                "Link to database",
                "The link to the page with the database on that you want to download.");
        
        
        public static readonly GUIContent UseOverrideApiKey =
            new GUIContent(
                "Use unique API key",
                "Should the asset use a different API key from the default one assigned?");
    }
}