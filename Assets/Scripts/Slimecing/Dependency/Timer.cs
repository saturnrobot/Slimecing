using System;

namespace Slimecing.Dependency
{
    public class Timer
    {
        public float RemainingSec { get; private set; }
        public float TrueDuration { get; private set; }
        public float RemainingLoop { get; private set; }
        public bool LoopInf { get; private set; }
        
        public event Action OnTimerEnd;
        
        public Timer(float duration)
        {
            RemainingSec = duration;
        }
        
        public Timer(float duration, float loopfor, bool li)
        {
            RemainingSec = duration;
            TrueDuration = duration;
            RemainingLoop = loopfor;
            LoopInf = li;
        }
        

        public void Tick(float deltaTime)
        {
            if (RemainingSec == 0f)
            {
                return;
                
            }

            RemainingSec -= deltaTime;
            
            CheckForEnd();
        }

        private void CheckForEnd()
        {
            if (RemainingSec > 0f)
            {
                return;
            }
            
            RemainingSec = 0f;

            if (RemainingLoop > 0)
            {
                RemainingSec = TrueDuration;
            }
            else if (LoopInf)
            {
                RemainingSec = TrueDuration;
            }

            OnTimerEnd?.Invoke();
        }
    }
}