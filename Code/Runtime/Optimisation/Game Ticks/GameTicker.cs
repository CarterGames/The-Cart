using CarterGames.Common.Events;
using CarterGames.Common.Utility;
using UnityEngine;

namespace CarterGames.Common.Optimisation
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

        private bool hasTimescaleOverride;
        private bool timescaleOverride;
        
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
            var change = 0f;

            if (hasTimescaleOverride)
            {
                change = timescaleOverride ? Time.unscaledDeltaTime : Time.deltaTime;
            }
            else
            {
                change = UtilRuntime.Settings.GameTickUseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            }

            tickTimer += change;

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


        /// <summary>
        /// Sets the timescale type from unscaled or scaled regardless of the global setting.
        /// </summary>
        /// <param name="useUnscaled">The scale to use.</param>
        public void OverrideTimeScaleType(bool useUnscaled)
        {
            hasTimescaleOverride = true;
            timescaleOverride = useUnscaled;
        }
    }
}