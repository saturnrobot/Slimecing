<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> Added triggers and lots of backend

namespace Slimecing.Dependency
{
    public class Timer
    {
<<<<<<< HEAD
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
=======

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
>>>>>>> Added triggers and lots of backend

            OnTimerEnd?.Invoke();
        }
    }
}