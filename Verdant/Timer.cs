﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verdant
{
    /// <summary>
    /// Counts milliseconds and triggers a callback when its finished.
    /// </summary>
    public class Timer
    {
        private static List<Timer> timers = new List<Timer>();

        public delegate void TimerCallback(Timer sender);

        // Shows if the Timer is currently running.
        public bool Running { get; private set; } = false;
        // The milliseconds that have elapsed since the Timer has started.
        public float ElapsedTime { get; private set; } = 0;
        // The duration the Timer runs for.
        public float Duration { get; set; } = 0;

        private TimerCallback callback;

        /// <summary>
        /// Initialize a new Timer.
        /// </summary>
        /// <param name="duration">The duration of the Timer (milliseconds).</param>
        /// <param name="startingTime">The time value to start the Timer at.</param>
        public Timer(float duration, TimerCallback callback, float startingTime = 0)
        {
            Duration = duration;

            ElapsedTime = startingTime;
            if (ElapsedTime > duration)
                ElapsedTime = duration;

            this.callback = callback;
        }

        static internal void TickAll(float deltaTime)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i].Tick(deltaTime);
                timers[i].Consume();
            }
        }

        /// <summary>
        /// Increase the recorded time of the Timer. When <c>Timer.Start()</c> is called, the current Scene will tick this timer automatically.
        /// <param name="deltaTime">The Scene's DeltaTime value.</param>
        /// </summary>
        internal void Tick(float deltaTime)
        {
            ElapsedTime += deltaTime;
        }

        /// <summary>
        /// Start the Timer.
        /// </summary>
        public void Start()
        {
            if (Running) return;

            timers.Add(this);
            Running = true;
        }

        /// <summary>
        /// Stop the Timer. Its elapsed time will not be reset.
        /// </summary>
        public void Stop()
        {
            if (!Running) return;

            timers.Remove(this);
            Running = false;
        }

        /// <summary>
        /// If the Timer has reached its duration, reset the time and trigger the callback.
        /// </summary>
        public void Consume()
        {
            if (ElapsedTime >= Duration)
            {
                Stop();
                ElapsedTime = 0;
                callback.Invoke(this);
            }
        }

        /// <summary>
        /// Stop the Timer and reset its current time.
        /// </summary>
        public void Reset()
        {
            Stop();
            ElapsedTime = 0;
        }

        /// <summary>
        /// Reset the Timer's elapsed time and start it.
        /// </summary>
        public void Restart()
        {
            ElapsedTime = 0;
            Start();
        }

    }
}
