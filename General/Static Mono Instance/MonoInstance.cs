using Scarlet.ModularComponents;
using UnityEngine;

namespace Scarlet.General
{
    public class MonoInstance : MonoBehaviour
    {
        private static Instance<MonoInstance> _instance;


        /// <summary>
        /// The instance of a monoBehaviour to access.
        /// </summary>
        public static Instance<MonoInstance> Instance
        {
            get
            {
                if (_instance != null) return _instance;
                var obj = Instantiate(new GameObject("Mono Instance (Scarlet Library)"));
                obj.AddComponent<MonoInstance>();
                _instance = new Instance<MonoInstance>(obj.GetComponent<MonoInstance>(), true);
                return _instance;
            }
        }
    }
}