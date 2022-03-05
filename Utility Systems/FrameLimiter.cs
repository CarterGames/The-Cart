using UnityEngine;

namespace JTools
{
    public static class FrameLimiter
    {
        /// <summary>
        /// Checks the frame count and only returns ture for every x frame
        /// </summary>
        /// <param name="x">How often the code should run, lower is more often, higher is less often</param>
        /// <returns>Bool</returns>
        /// <remarks>
        /// Ideal for use in Update() or similar where stuff has to be in update to work but shouldn't run every fame
        /// to increase the performance of the game.  
        /// </remarks>
        public static bool LimitEveryXFrames(int x)
        {
            return Time.frameCount % x == 0;
        }
    }
}