using System;
using System.Collections.Generic;
using CarterGames.Cart.Core.Events;

namespace CarterGames.Cart.Modules.RuntimeTimers
{
	public class RuntimeTimerStopwatch : RuntimeTimer
	{
		public float TimePassed => TimeRemaining;
		public int TimePassedInSeconds => TimeRemainingInSeconds;

		public List<float> LapTimes { get; set; } = new List<float>();

		
		public readonly Evt<RuntimeTimer> LapRegistered = new Evt<RuntimeTimer>();
		public readonly Evt<RuntimeTimer> TimerStopped = new Evt<RuntimeTimer>();

		

		public static RuntimeTimer SetStopwatch(Action<RuntimeTimer> onStopped, bool? useUnscaledTime = false, bool? autoStart = true)
		{
			// var timer = Create(onStopped);
			// timer.TimerDuration = -1;
			// timer.CountDown = false;
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

			// return timer;

			return null;
		}

		protected override void TimerTick(float adjustment)
		{
			throw new NotImplementedException();
		}
	}
}