using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JTools
{
    /// <summary>
    /// Class | DeSpawns the object it is attached to.
    /// </summary>
    public class Despawner : MonoBehaviour
    {
        /// <summary>
        /// Enum | The choices for the de-spawner to use.
        /// </summary>
        public enum DeSpawnerChoices
        {
            Disable,
            DisableCanvas,
            DisableInteractable,
            DisableAnimator,
            Destroy
        };

        /// <summary>
        /// Float | defines how long the object has before it is removed.
        /// </summary>
        [Header("De-spawn Delay")]
        [Tooltip("Set this to define how long the object will wait before de-spawning. Default Value = 1")]
        [SerializeField] private float deSpawnTime = 1f;

        /// <summary>
        /// De-spawnerChoice | the option to run when the timer runs out.
        /// </summary>
        [Header("De-spawn Choice")]
        [Tooltip("Pick an option for what happens when the de-spawn timer runs out.")]
        [SerializeField] private DeSpawnerChoices choices;

        

        private void OnEnable()
        {
            if (deSpawnTime <= 0) return;
            StartCoroutine(DeSpawnCo());
        }

        
        private void OnDisable()
        {
            deSpawnTime = 0f;
            StopAllCoroutines();
        }

        
        public void SetDeSpawner(double time, DeSpawnerChoices type)
        {
            deSpawnTime = (float) time;
            choices = type;
            StartCoroutine(DeSpawnCo());
        }
        
        
        /// <summary>
        /// Coroutine | De-spawns the object this is attached to as and when it is enabled.
        /// </summary>
        private IEnumerator DeSpawnCo()
        {
            // waits for the defined time.
            yield return new WaitForSeconds(deSpawnTime);

            // removes the object based on the user choice.
            switch (choices)
            {
                case DeSpawnerChoices.Disable:
                    gameObject.SetActive(false);
                    break;
                case DeSpawnerChoices.Destroy:
                    Destroy(gameObject);
                    break;
                case DeSpawnerChoices.DisableInteractable:
                    TryGetComponent(out Button _button);
                    if (_button == null) break;
                    _button.interactable = false;
                    break;
                case DeSpawnerChoices.DisableAnimator:
                    TryGetComponent(out Animator _anim);
                    if (_anim == null) break;
                    _anim.enabled = false;
                    break;
                case DeSpawnerChoices.DisableCanvas:
                    TryGetComponent(out Canvas _canvas);
                    if (_canvas == null) break;
                    _canvas.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}