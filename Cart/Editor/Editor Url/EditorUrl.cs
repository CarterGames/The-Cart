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