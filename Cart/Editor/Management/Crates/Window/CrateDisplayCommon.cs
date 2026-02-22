using System;
using CarterGames.Cart;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Window
{
    public static class CrateDisplayCommon
    {
        private static GUIStyle cachedMiniButtonStyle;


        private static GUIStyle MiniButtonStyle =>
            CacheRef.GetOrAssign(ref cachedMiniButtonStyle, new GUIStyle("minibutton"));

        
        private static readonly GUIContent CrateEnabledCopy = new GUIContent("Enabled", EditorGUIUtility.IconContent("Valid@2x").image);
        private static readonly GUIContent CrateInstallCopy = new GUIContent("Enable", EditorGUIUtility.IconContent("Valid").image);
        private static readonly GUIContent CrateEnabled = new GUIContent(EditorGUIUtility.IconContent("Valid").image);
        private static readonly GUIContent CrateDisabledCopy = new GUIContent("Disabled", EditorGUIUtility.IconContent("CrossIcon").image);
        private static readonly GUIContent CrateUninstallCopy = new GUIContent("Disable", EditorGUIUtility.IconContent("CrossIcon").image);
        private static readonly GUIContent CrateDisabled = new GUIContent(EditorGUIUtility.IconContent("CrossIcon").image);
        
        
        
        public static void GetCrateStatusButton(Crate crate, bool showText = true)
        {
            Internal_GetCrateStatusButton(CrateManager.IsEnabled(crate), showText);
        }

        
        public static void GetCrateStatusButton(ExternalCrate crate, bool showText = true)
        {
            Internal_GetCrateStatusButton(CrateManager.IsPackageInstalled(crate), showText);
        }
        
        
        private static void Internal_GetCrateStatusButton(bool isInstalled, bool showText = true)
        { 
            if (isInstalled)
            {
                GUI.backgroundColor = CrateManager.InstallCol;
                
                if (showText)
                {
                    GUILayout.Label(CrateEnabledCopy, MiniButtonStyle, GUILayout.MaxWidth(100), GUILayout.Height(22.5f));
                }
                else
                {
                    GUILayout.Label(CrateEnabled, MiniButtonStyle, GUILayout.MaxWidth(40f), GUILayout.Height(22.5f));
                }
            }
            else
            {
                GUI.backgroundColor = CrateManager.UninstallCol;
                
                if (showText)
                {
                    GUILayout.Label(CrateDisabledCopy, MiniButtonStyle, GUILayout.MaxWidth(100), GUILayout.Height(22.5f));
                }
                else
                {
                    GUILayout.Label(CrateDisabled, MiniButtonStyle, GUILayout.MaxWidth(40f), GUILayout.Height(22.5f));
                }
            }
            
            GUI.backgroundColor = Color.white;
        }

        

        public static void GetInstallButton(Crate crate)
        {
            Internal_GetInstallButton(() => CrateInstaller.Install(crate));
        }


        private static void GetInstallButton(ExternalCrate crate)
        {
            Internal_GetInstallButton(() => CrateManager.AddPackage(crate.PackageInfo));
        }
        
        private static void Internal_GetInstallButton(Action onButtonPressed)
        {
            GUI.backgroundColor = CrateManager.InstallCol;
                
            if (GUILayout.Button(CrateInstallCopy, GUILayout.Height(22.5f)))
            {
                onButtonPressed?.Invoke();
            }

            GUI.backgroundColor = Color.white;
        }
        
        
        
        public static void GetUninstallButton(Crate crate)
        {
            Internal_GetUninstallButton(() => CrateUninstaller.Uninstall(crate));
        }


        private static void GetUninstallButton(ExternalCrate crate)
        {
            Internal_GetUninstallButton(() => CrateManager.RemovePackage(crate.PackageInfo));
        }
        
        
        private static void Internal_GetUninstallButton(Action onButtonPressed)
        {
            GUI.backgroundColor = CrateManager.UninstallCol;
                
            if (GUILayout.Button(CrateUninstallCopy, GUILayout.Height(22.5f)))
            {
                onButtonPressed?.Invoke();
            }

            GUI.backgroundColor = Color.white;
        }
    }
}