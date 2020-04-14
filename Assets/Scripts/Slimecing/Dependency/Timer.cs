using System;

namespace Slimecing.Dependency
{
    public class Timer
    {
        public int loopCount
        {
            get; private set;
        }

        public float remainingSeconds
        {
            get; private set;
        }

        public float initialRemainingSeconds
        {
            get; private set;
        }

        // Constructor will set local variables to the give arguments
        public Timer(float duration, int loops)
        {
            loopCount = loops;
            initialRemainingSeconds = remainingSeconds = duration;
        }

        public event Action OnTimerEnd;

        // Handles update that will happen every gametick
        public void Tick(float deltaTime)
        {
            if (remainingSeconds == 0f) return;

            remainingSeconds -= deltaTime;

            CheckForTimerEnd();
        }

        private void CheckForTimerEnd()
        {
            if (remainingSeconds > 0f) return;

            // Will restart timer if the current 'loops' is 1 or greater
            remainingSeconds = loopCount-- > 0?initialRemainingSeconds:0f;
            
            OnTimerEnd?.Invoke();
        }
    }
}