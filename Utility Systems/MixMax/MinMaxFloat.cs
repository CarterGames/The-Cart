// ----------------------------------------------------------------------------
// MinMaxFloat.cs
// 
// Description: A float variant of the min/max value system.
// ----------------------------------------------------------------------------

using System;

namespace Erissa.General
{
    [Serializable]
    public class MinMaxFloat : MinMaxBase<float>
    {
        public MinMaxFloat()
        {
            min = 0f;
            max = 1f;
        }
        
        public MinMaxFloat(float min, float max) : base(min, max) { }
        
        public override void IncreaseMinMax(float amount)
        {
            base.IncreaseMinMax(amount);

            min += amount;
            max += amount;
        }

        public override void ResetMinMax()
        {
            min = 0;
            max = 0;
        }
        
        /// <summary>
        /// Returns a random value in the min/max range...
        /// </summary>
        public virtual float Random()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}