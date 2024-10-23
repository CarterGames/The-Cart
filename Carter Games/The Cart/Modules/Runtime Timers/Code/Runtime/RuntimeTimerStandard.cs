using System;

namespace CarterGames.Cart.Modules.RuntimeTimers
{
	public class RuntimeTimerStandard : RuntimeTimer
	{
		public new float TimeRemaining => base.TimeRemaining;
		public new int TimeRemainingInSeconds => base.TimeRemainingInSeconds;
		public new float TimerDuration => base.TimerDuration;
		public new float TimeRemainingFraction => TimeRemaining / TimerDuration;
		
		
		
		/// <summary>
		/// Sets a new runtime timer with the entered values.
		/// </summary>
		/// <param name="duration">The duration for the timer.</param>
		/// <param name="onComplete">The action to fire when the timer completed.</param>
		/// <param name="useUnscaledTime">Should the timer be in unscaled time or not?</param>
		/// <param name="autoStart">Should the timer auto start on this method call or wait for the user to manually start it?</param>
		/// <returns>The timer created.</returns>
		public static RuntimeTimer SetCountdown(float duration, Action onComplete, bool? useUnscaledTime = false, bool? autoStart = true)
		{
			// var timer = Create(onComplete);
			// timer.TimerDuration = duration;
			// timer.CountDown = true;
   //          
			// if (useUnscaledTime.HasValue)
			// {
			// 	timer.UseUnscaledTime = useUnscaledTime.Value;
			// }
			//
			// if (!autoStart.HasValue) return timer;
   //          
			// if (autoStart.Value)
			// {
			// 	timer.StartTimer();
			// }
			//
			// return timer;

			return null;
		}


		protected override void TimerTick(float adjustment)
		{
			base.TimeRemaining -= adjustment;
					
            

			TimerCompleted.Raise();
			timerRoutine = null;
			RuntimeTimerManager.UnRegister(this);
		}
	}
}