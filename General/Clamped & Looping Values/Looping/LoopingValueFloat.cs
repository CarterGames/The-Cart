// ----------------------------------------------------------------------------
// LoopingValueFloat.cs
// 
// Description: A helper class to automatically loop a value between a range.
//              Useful for menus & UI elements.
// ----------------------------------------------------------------------------

namespace Scarlet.General
{
    public class LoopingValueFloat : ValueSystemBase<float>
    {
        /// <summary>
        /// Converts the value to its read value implicitly for use.
        /// </summary>
        /// <param name="loopingValueFloat">The value to convert</param>
        /// <returns>Int</returns>
        public static implicit operator float(LoopingValueFloat loopingValueFloat)
        {
            return loopingValueFloat.Value;
        }


        /// <summary>
        /// Constructor | Creates a new looping value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        public LoopingValueFloat(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = minValue;
        }
        
        
        /// <summary>
        /// Constructor | Creates a new looping value.
        /// </summary>
        /// <param name="minValue">The min/lower value</param>
        /// <param name="maxValue">The max/upper value</param>
        /// <param name="startingValue">The starting value</param>
        public LoopingValueFloat(float minValue, float maxValue, float startingValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            currentValue = startingValue;
        }

        
        /// <summary>
        /// Increments the value by the amount entered.
        /// </summary>
        /// <param name="amount">The amount to increment by</param>
        public void IncrementValue(float amount)
        {
            currentValue = ValueChecks(amount);
        }


        protected override float ValueChecks(float adjustment)
        {
            currentValue += adjustment;

            var p = maxValue - minValue + adjustment;
            var mod = (currentValue - minValue) % p;
            
            if (mod < 0)
                mod += p;
            
            return minValue + mod;
        }
    }
}