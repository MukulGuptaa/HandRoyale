using System;
using System.Collections;
using UnityEngine;

namespace Util
{
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

        public void StartTimer(int duration)
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _remainingTime = duration;
            OnTimerUpdated?.Invoke(_remainingTime);
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

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
