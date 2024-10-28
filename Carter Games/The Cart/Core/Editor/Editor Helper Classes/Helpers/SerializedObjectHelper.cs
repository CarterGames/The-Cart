using UnityEditor;

namespace CarterGames.Cart.Core.Management.Editor
{
    /// <summary>
    /// A helper class to aid with editor scripting where the API is really wordy etc.
    /// </summary>
    public static class SerializedObjectHelper
    {
        /// <summary>
        /// Gets the total number of properties on the target Object, this includes the script itself as 1.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        /// <param name="enterChildren">Should children be included.</param>
        /// <returns>The total amount on the object.</returns>
        public static int TotalProperties(this SerializedObject targetObject, bool enterChildren = true)
        {
            var total = 0;
            var iterator = targetObject.GetIterator();

            while (iterator.NextVisible(enterChildren))
            {
                total++;
            }

            return total;
        }
    }
}