using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public abstract class TimedAbilityEffect
    {
        protected float duration;
        protected GameObject obj;
        protected Ability ability;

        public bool IsDone => duration <= 0;

        public TimedAbilityEffect(float duration, Ability ability, GameObject obj)
        {
            this.duration = duration;
            this.ability = ability;
            this.obj = obj;
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
