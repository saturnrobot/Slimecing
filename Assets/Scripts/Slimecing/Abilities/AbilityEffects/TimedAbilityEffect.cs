using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public abstract class TimedAbilityEffect
    {
        protected float duration;
        protected AbilityUser aUser;
        protected Ability ability;

        public bool IsDone => duration <= 0;
        
        public TimedAbilityEffect(float duration, Ability ability, AbilityUser aUser)
        {
            this.duration = duration;
            this.ability = ability;
            this.aUser = aUser;
        }

        public void Tick(float delta)
        {
            duration -= delta;

            if (duration <= 0)
            {
                End();
            }
                
        }

        public abstract void Activate();
        public abstract void End();
    }
}
