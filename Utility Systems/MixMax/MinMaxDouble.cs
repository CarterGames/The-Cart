// ----------------------------------------------------------------------------
// MinMaxDouble.cs
// 
// Description: A double variant of the min/max value system.
// ----------------------------------------------------------------------------

using System;

namespace Erissa.General
{
    [Serializable]
    public class MinMaxDouble : MinMaxBase<double>
    {
        public MinMaxDouble()
        {
            min = 0d;
            max = 1d;
        }
        
        public MinMaxDouble(double min, double max) : base(min, max) { }
        
        public override void IncreaseMinMax(double amount)
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
        public virtual double Random()
        {
            return UnityEngine.Random.Range((float)min, (float)max);
        }
    }
}