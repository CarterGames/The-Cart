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

using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace CarterGames.Cart.Management.Editor
{
    public class InstallMethodChecker : IAssetEditorInitialize
    {
        private static ListRequest PackageManifestRequest { get; set; }

        
        private static bool HasChecked
        {
            get => SessionState.GetBool("VersionValidation_InstallMethod_Checked", false);
            set => SessionState.SetBool("VersionValidation_InstallMethod_Checked", value);
        }
        
        
        public static bool IsPackageInstalled
        {
            get => SessionState.GetBool("VersionValidation_InstallMethod_IsPackageManager", false);
            set => SessionState.SetBool("VersionValidation_InstallMethod_IsPackageManager", value);
        }


        public int InitializeOrder { get; }
        
        
        public void OnEditorInitialized()
        {
            if (HasChecked) return;
            
            PackageManifestRequest = Client.List();
            
            EditorApplication.update -= OnPackageManagerGetResolved;
            EditorApplication.update += OnPackageManagerGetResolved;
        }
        
        
        private static void OnPackageManagerGetResolved()
        {
            if (!PackageManifestRequest.IsCompleted) return;
            
            EditorApplication.update -= OnPackageManagerGetResolved;

            if (PackageManifestRequest.Status != StatusCode.Success) return;

            HasChecked = true;

            foreach (var result in PackageManifestRequest.Result)
            {
                if (result.packageId.Equals("games.carter.thecart")) continue;
                IsPackageInstalled = true;
            }
        }
    }
}