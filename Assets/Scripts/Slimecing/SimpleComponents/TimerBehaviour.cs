using System;
using Slimecing.Dependency;
using UnityEngine;
using UnityEngine.Events;

namespace Slimecing.SimpleComponents
{
    public class TimerBehaviour : MonoBehaviour
    {
        [SerializeField] private float durationOfTick = 1f;
        [SerializeField] private float loopFor = 0f;
        [SerializeField] private bool loopInfinite = false;
        [SerializeField] private UnityEvent onTimerEnd = null;

        private Timer _timer;

        private Timer Timer
        {
            get
            {
                if (_timer != null)
                {
                    return _timer;
                }
                //_timer = new Timer(durationOfTick, loopFor, loopInfinite);
                return _timer;
            }
        }
        
        private void Start()
        {
            Timer.OnTimerEnd += WhenTimerEnd;
        }

        private void Update()
        {
            Timer.Tick(Time.deltaTime);
        }

        private void WhenTimerEnd()
        {
            onTimerEnd.Invoke();
        }

    }
}
