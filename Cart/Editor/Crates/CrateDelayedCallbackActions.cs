using CarterGames.Cart.Editor;
using CarterGames.Cart.Management.Editor;

namespace CarterGames.Cart.Crates
{
    public class CrateDelayedCallbackActions : IAssetEditorReload
    {
        private const string EnableOperation = "crate:enable:";
        private const string DisableOperation = "crate:disable:";
        
        
        public static void MarkToEnable(Crate crate)
        {
            var array = AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("postCompileActions");
            array.InsertAtEnd();
            array.GetLastIndex().stringValue = $"{EnableOperation}{crate.CrateTechnicalName}";
            array.serializedObject.ApplyModifiedProperties();
            array.serializedObject.Update();
        }
        
        
        public static void MarkToDisable(Crate crate)
        {
            var array = AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("postCompileActions");
            array.InsertAtEnd();
            array.GetLastIndex().stringValue = $"{DisableOperation}{crate.CrateTechnicalName}";
            array.serializedObject.ApplyModifiedProperties();
            array.serializedObject.Update();
        }
        
        
        public void OnEditorReloaded()
        {
            if (!HasOperations()) return;
            PerformNextOperation(GetNextOperation());
        }


        private static bool HasOperations()
        {
            return AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("postCompileActions").arraySize > 0;
        }


        private static string GetNextOperation()
        {
            return AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("postCompileActions").GetIndex(0)
                .stringValue;
        }


        private static void PerformNextOperation(string operationString)
        {
            RemoveNextOperationFromAsset();
            
            switch (operationString)
            {
                case var _ when operationString.Contains(EnableOperation):
                    CrateManager.EnableDefines(CrateManager.GetCrateByTechnicalName(operationString.Replace(EnableOperation, string.Empty)));
                    break;
                case var _ when operationString.Contains(DisableOperation):
                    CrateManager.DisableDefines(CrateManager.GetCrateByTechnicalName(operationString.Replace(DisableOperation, string.Empty)));
                    break;
                default:
                    break;
            }
        }


        private static void RemoveNextOperationFromAsset()
        {
            AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Fp("postCompileActions")
                .DeleteAndRemoveIndex(0);
            
            AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.ApplyModifiedProperties();
            AutoMakeDataAssetManager.GetDefine<DataAssetCsc>().ObjectRef.Update();
        }

        
        public static void RemoveLastAdded()
        {
            RemoveNextOperationFromAsset();
        }
    }
}