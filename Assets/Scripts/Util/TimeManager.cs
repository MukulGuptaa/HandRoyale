using System;
using System.Collections;
using UnityEngine;

namespace Util
{
    /// <summary>
    /// Manages a countdown timer using a singleton pattern.
    /// Provides events for timer updates and completion.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        public static Action<int> OnTimerUpdated;
        public static Action OnTimerFinished;

        private int _remainingTime;
        private Coroutine _timerCoroutine;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Starts a countdown timer for the specified duration.
        /// If a timer is already running, it stops the previous one before starting a new timer.
        /// </summary>
        /// <param name="duration">Duration of the timer in seconds.</param>
        public void StartTimer(int duration)
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _remainingTime = duration;
            OnTimerUpdated?.Invoke(_remainingTime);
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        /// <summary>
        /// Coroutine that updates the timer every second and triggers events.
        /// </summary>
        private IEnumerator TimerCoroutine()
        {
            while (_remainingTime > 0)
            {
                OnTimerUpdated?.Invoke(_remainingTime);
                yield return new WaitForSeconds(1f);
                _remainingTime--;
            }
            OnTimerUpdated?.Invoke(0);
            OnTimerFinished?.Invoke();
        }

        /// <summary>
        /// Stops the currently running timer, if any.
        /// </summary>
        public void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }
        }
    }
}
