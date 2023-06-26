using Scarlet.Events;
using Scarlet.Utility;
using UnityEngine;

namespace Scarlet.Optimisation
{
    /// <summary>
    /// Handles a tick system with the ticks defined locally.
    /// </summary>
    public class GameTicker : MonoBehaviour
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private int ticksPerSecond;
        
        private float tickTimer;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// The amount of time between each tick.
        /// </summary>
        private float MaxTimeBetweenTicks => 1f / ticksPerSecond;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Events
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Raises when the a tick is reached.
        /// </summary>
        public readonly Evt Ticked = new Evt();

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Unity Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private void Update()
        {
            tickTimer += UtilRuntime.Settings.GameTickUseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            if (tickTimer < MaxTimeBetweenTicks) return;
            
            tickTimer -= MaxTimeBetweenTicks;
            Ticked.Raise();
        }

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Sets the ticks per second to the value provided.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public void SetTicksPerSecond(int value)
        {
            ticksPerSecond = value;
        }
    }
}