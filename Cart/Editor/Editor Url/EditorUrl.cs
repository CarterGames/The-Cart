using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Editor
{
    public class EditorUrl
    {
        public string DisplayName { get; private set; }
        public string Target { get; private set; }
        private EditorUrlType UrlType { get; set; }
        public Action TargetAction { get; private set; }

        

        public static EditorUrl LocalFile(string displayName, string fileName)
        {
            return new EditorUrl()
            {
                DisplayName = displayName,
                UrlType = EditorUrlType.LocalFile,
                Target = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets($"{fileName}").FirstOrDefault()),
            };
        }
        
        
        public static EditorUrl CustomLocalAction(string displayName, Action action)
        {
            return new EditorUrl()
            {
                DisplayName = displayName,
                UrlType = EditorUrlType.LocalCustomAction,
                TargetAction = action
            };
        }
        
        
        public static EditorUrl Webpage(string displayName, string url)
        {
            return new EditorUrl()
            {
                DisplayName = displayName,
                UrlType = EditorUrlType.Webpage,
                Target = url,
            };
        }


        public void OpenLink()
        {
            Debug.Log(Target);
            
            switch (UrlType)
            {
                case EditorUrlType.Webpage:
                    Application.OpenURL(Target);
                    break;
                case EditorUrlType.LocalFile:
                    Application.OpenURL(Target);
                    break;
                case EditorUrlType.LocalCustomAction:
                    TargetAction?.Invoke();
                    break;
                case EditorUrlType.Unassigned:
                default:
                    break;
            }
        }
    }
}