using CarterGames.Cart.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Crates.Window
{
    public static class PackagedCrateDisplay
    {
        public static void Draw(ExternalCrate crate)
        {
            
        }
        
        
        private static void DrawPackagedCrate(ExternalCrate externalCrate)
        {
            // if (string.IsNullOrEmpty(EditorSettingsCrateWindow.SelectedCrateName)) return;
            // if (packageCrate == null) return;
            //
            // EditorGUILayout.BeginVertical("HelpBox");
            //
            //
            // EditorGUILayout.BeginHorizontal();
            // EditorGUILayout.LabelField(packageCrate.PackageName, EditorStyles.boldLabel);
            // DrawCrateStatusButton(packageCrate);
            //
            // EditorGUILayout.EndHorizontal();
            //
            // GeneralUtilEditor.DrawHorizontalGUILine();
            //
            // EditorGUILayout.LabelField(packageCrate.PackageDescription, labelStyle);
            //
            // GeneralUtilEditor.DrawHorizontalGUILine();
            //
            // EditorGUILayout.BeginHorizontal();
            // EditorGUILayout.LabelField("Author:", GUILayout.MaxWidth(45));
            // EditorGUILayout.LabelField(packageCrate.PackageAuthor);
            // EditorGUILayout.EndHorizontal();
            //
            //
            // // Pre-requirements..
            // if (packageCrate.PreRequisites.Length > 0)
            // {
            //     GeneralUtilEditor.DrawHorizontalGUILine();
            //     
            //     EditorGUILayout.LabelField("Required Crates", EditorStyles.boldLabel);
            //
            //     foreach (var preRequisite in packageCrate.PreRequisites)
            //     {
            //         EditorGUILayout.BeginHorizontal();
            //         
            //         EditorGUILayout.LabelField("- " + preRequisite.PackageName + " " + (CrateManager.CheckPackageInstalled(preRequisite.PackageInfo.packageName) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
            //         
            //         EditorGUILayout.EndHorizontal();
            //     }
            // }
            //
            // // Optional requirements..
            // if (packageCrate.OptionalPreRequisites.Length > 0)
            // {
            //     GeneralUtilEditor.DrawHorizontalGUILine();
            //     
            //     EditorGUILayout.LabelField("Optional Crates", EditorStyles.boldLabel);
            //
            //     EditorGUILayout.BeginVertical();
            //     
            //     foreach (var preRequisite in packageCrate.OptionalPreRequisites)
            //     {
            //         EditorGUILayout.BeginHorizontal();
            //         EditorGUILayout.LabelField("- " + preRequisite.PackageName + " " + (CrateManager.CheckPackageInstalled(preRequisite.PackageInfo.packageName) ? "<color=#71ff50>\u2714</color>" : "<color=#ff9494>\u2718</color>"), labelStyle);
            //         EditorGUILayout.EndHorizontal();
            //     }
            //     
            //     EditorGUILayout.EndVertical();
            // }
            //
            // GUILayout.Space(2.5f);
            //
            // EditorGUILayout.EndVertical();
        }
    }
}