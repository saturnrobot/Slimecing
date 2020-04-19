using Slimecing.Dependency;
using UnityEngine;
using UnityEngine.Serialization;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "TimerTrigger", menuName = "Triggers/SimpleTriggers/TimerTrigger")]
    public class TimerTrigger : Trigger
    {
        [SerializeField] private float timerDuration = 0.1f;
        [SerializeField] private int timerLoops = 0;
        
        private Timer _timer;

        public Timer timer
        {
            get => _timer;
            set => _timer = value;
        }

        public float duration
        {
            get => timerDuration;
            set => timerDuration = value;
        }

        public int loops
        {
            get => timerLoops;
            set => timerLoops = value;
        }
        
        public override void EnableTrigger(GameObject target)
        {
            _timer = new Timer(duration, loops);
            _timer.OnTimerEnd += ctx => TriggerPerformed(target);
        }

        private void TriggerPerformed(GameObject target)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Performed, target));
        }
    }
}
