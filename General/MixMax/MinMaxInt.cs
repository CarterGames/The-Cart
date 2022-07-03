// ----------------------------------------------------------------------------
// MinMaxInt.cs
// 
// Description: A int variant of the min/max value system.
// ----------------------------------------------------------------------------

using System;

namespace Scarlet.General
{
    [Serializable]
    public class MinMaxInt : MinMaxBase<int>
    {
        public MinMaxInt()
        {
            min = 0;
            max = 1;
        }
        
        public MinMaxInt(int min, int max) : base(min, max) { }
        
        public override void IncreaseMinMax(int amount)
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
        public virtual int Random()
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
    }
}