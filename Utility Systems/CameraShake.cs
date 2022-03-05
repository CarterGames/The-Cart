using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace JTools
{
    public class CameraShake : MonoBehaviour
    {
        // Defines how long the camera should shake for.
        [Tooltip("The duration for the camera shake effect.")]
        [SerializeField] private float shakeDuration;
        
        // Defines whether or not the camera should shake.
        [Tooltip("Should the camera be shaken.")]
        [SerializeField] private bool shouldCameraShake;
        
        
        private bool is2D;                      // Should the camera shake it 2d or 3d space?
        private float shakeAmount;              // How much the camera should shake, is set to the force defined by the user in the Shake Camera Method.
        private bool isCameraPositionSaved;     // Used to check if the camera starting position is saved before it is shaken.
        private bool isCoRunning;               // Used to check if the coroutine is running or not.
        private Vector3 cameraPosition;         // Used to save the camera position before the shake effect is started, so it can be returned to its starting position.
        private GameObject cam;                 // The camera to shake.
        
        
        public bool IsShaking => isCoRunning;
        
        
        private void OnDisable()
        {
            // Stops any coroutines from running that still are...
            StopAllCoroutines();
        }


        private void Start()
        {
            cam = this.gameObject;
        }



        private void Update()
        {
            // Returns if the camera shake is not enabled...
            if (!shouldCameraShake) return;
            
            // Shakes the camera in 2d or 3d space...
            if (!is2D)
                cam.transform.localPosition += new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z) * shakeAmount;
            else
                cam.transform.localPosition += new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, 0) * shakeAmount;

            // Runs the shake coroutine if it is not running already...
            if (!isCoRunning)
                StartCoroutine(StopCameraShake());
        }

        
        /// <summary>
        /// Coroutine | Stops the camera shake effect after the set duration is completed.
        /// </summary>
        private IEnumerator StopCameraShake()
        {
            isCoRunning = true;
            yield return new WaitForSeconds(shakeDuration);
            shouldCameraShake = false;
            isCoRunning = false;
            transform.localPosition = cameraPosition;
            isCameraPositionSaved = false;
        }


        public void StopShake()
        {
            shouldCameraShake = false;
            isCoRunning = false;
            transform.localPosition = cameraPosition;
            isCameraPositionSaved = false; 
        }

        
        /// <summary>
        /// Runs the method will shake the camera with the defined values.
        /// </summary>
        /// <param name="shakeForce">the amount of force to apply. Default = 0.1f</param>
        /// <param name="shakeLenght">the duration for the effect. Default = 0.25f</param>
        public void ShakeCamera(bool should2D = false, float shakeForce = .1f, float shakeLenght = .25f)
        {
            // Only edited if inputted, otherwise default values are entered
            is2D = should2D;
            shakeAmount = shakeForce;
            shakeDuration = shakeLenght;

            if (!isCameraPositionSaved)
            {
                cameraPosition = transform.localPosition;
                isCameraPositionSaved = true;
            }

            if (!isCoRunning)
                shouldCameraShake = true;
        }
    }
}
