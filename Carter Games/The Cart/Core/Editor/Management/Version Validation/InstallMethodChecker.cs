using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace CarterGames.Cart.Core.Management.Editor
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