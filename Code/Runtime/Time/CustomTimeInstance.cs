using System;
using UnityEngine;

namespace CarterGames.Common.CustomTime
{
    [Serializable]
    public class CustomTimeInstance
    {
        // TODO - Have an editor setup to set this up in settings so not done runtime if not in use.
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Field
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        private float localTimeScale = 1f;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets the Time.deltaTime scales with this instance.
        /// </summary>
        public float DeltaTime => Time.deltaTime * localTimeScale;
        
        
        /// <summary>
        /// Gets the Time.fixedDeltaTime scales with this instance.
        /// </summary>
        public float FixedDeltaTime => Time.fixedDeltaTime * localTimeScale;
        
        
        /// <summary>
        /// Gets the Time.time scales with this instance.
        /// </summary>
        public float StandardTime => Time.time * localTimeScale;
        
        
        /// <summary>
        /// Gets is the time is paused.
        /// </summary>
        public bool IsPaused => Mathf.Approximately(localTimeScale, 0);

        
        /// <summary>
        /// The timescale for the instance.
        /// </summary>
        public float TimeScale => Time.timeScale * localTimeScale;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Pauses the time when called.
        /// </summary>
        public void PauseTime()
        {
            localTimeScale = Mathf.Epsilon;
        }

        
        /// <summary>
        /// Resumes the time when called.
        /// </summary>
        public void ResumeTime()
        {
            localTimeScale = 1f;
        }
    }
}