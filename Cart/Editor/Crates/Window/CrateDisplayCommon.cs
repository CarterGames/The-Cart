using System;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Window
{
    public static class CrateDisplayCommon
    {
        private static GUIStyle cachedMiniButtonStyle;


        private static GUIStyle MiniButtonStyle =>
            CacheRef.GetOrAssign(ref cachedMiniButtonStyle, new GUIStyle("minibutton"));

        
        private static readonly GUIContent CrateEnableCopy = new GUIContent("Enable", EditorGUIUtility.IconContent("Valid@2x").image);
        private static readonly GUIContent CrateEnabledCopy = new GUIContent("Enabled", EditorGUIUtility.IconContent("Valid@2x").image);
        private static readonly GUIContent CrateInstallCopy = new GUIContent("Enable (Import Package)", EditorGUIUtility.IconContent("Valid").image);
        private static readonly GUIContent CrateEnabled = new GUIContent(EditorGUIUtility.IconContent("Valid").image);
        private static readonly GUIContent CrateDisabledCopy = new GUIContent("Disabled", EditorGUIUtility.IconContent("CrossIcon").image);
        private static readonly GUIContent CrateDisableCopy = new GUIContent("Disable", EditorGUIUtility.IconContent("CrossIcon").image);
        private static readonly GUIContent CrateUninstallCopy = new GUIContent("Disable (Remove Package)", EditorGUIUtility.IconContent("CrossIcon").image);
        private static readonly GUIContent CrateDisabled = new GUIContent(EditorGUIUtility.IconContent("CrossIcon").image);
        
        
        
        public static void GetCrateStatusButton(Crate crate, bool showText = true)
        {
            Internal_GetCrateStatusButton(CrateManager.IsEnabled(crate), showText);
        }

        
        public static void GetCrateStatusButton(ExternalCrate crate, bool showText = true)
        {
            Internal_GetCrateStatusButton(ExternalCrateManager.IsPackageInstalled(crate.PackageInfo), showText);
        }
        
        
        private static void Internal_GetCrateStatusButton(bool isInstalled, bool showText = true)
        { 
            if (isInstalled)
            {
                GUI.backgroundColor = Color.green;
                
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
                GUI.backgroundColor = Color.red;
                
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
            Internal_GetInstallButton(CrateEnableCopy, () => CrateManager.EnableDefines(crate));
        }


        public static void GetExternalInstallButton(ExternalCrate crate)
        {
            Internal_GetInstallButton(CrateInstallCopy, () =>
            {
                CrateDelayedCallbackActions.MarkToEnable(crate);
                
                try
                {
                    ExternalCrateManager.AddPackage(crate.PackageInfo);
                }
                catch (Exception e)
                {
                    CrateDelayedCallbackActions.RemoveLastAdded();
                    Console.WriteLine(e);
                    throw;
                }
            });
        }
        
        private static void Internal_GetInstallButton(GUIContent content, Action onButtonPressed)
        {
            GUI.backgroundColor = Color.green;
                
            if (GUILayout.Button(content, GUILayout.Height(22.5f)))
            {
                onButtonPressed?.Invoke();
            }

            GUI.backgroundColor = Color.white;
        }
        
        
        
        public static void GetUninstallButton(Crate crate)
        {
            Internal_GetUninstallButton(CrateDisableCopy, () => CrateManager.DisableDefines(crate));
        }


        public static void GetExternalUninstallButton(ExternalCrate crate)
        {
            Internal_GetUninstallButton(CrateUninstallCopy,() =>
            {
                CrateDelayedCallbackActions.MarkToDisable(crate);
                
                try
                {
                    ExternalCrateManager.RemovePackage(crate.PackageInfo);
                }
                catch (Exception e)
                {
                    CrateDelayedCallbackActions.RemoveLastAdded();
                    Console.WriteLine(e);
                    throw;
                }
            });
        }
        
        
        private static void Internal_GetUninstallButton(GUIContent content, Action onButtonPressed)
        {
            GUI.backgroundColor = Color.red;
                
            if (GUILayout.Button(content, GUILayout.Height(22.5f)))
            {
                onButtonPressed?.Invoke();
            }

            GUI.backgroundColor = Color.white;
        }
    }
}