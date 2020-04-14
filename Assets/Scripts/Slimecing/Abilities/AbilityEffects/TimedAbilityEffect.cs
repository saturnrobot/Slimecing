<<<<<<< HEAD
﻿using UnityEngine;
=======
﻿using Slimecing.Characters;
using UnityEngine;
>>>>>>> Added triggers and lots of backend

namespace Slimecing.Abilities.AbilityEffects
{
    public abstract class TimedAbilityEffect
    {
        protected float duration;
<<<<<<< HEAD
        protected GameObject obj;
=======
        protected AbilityUser aUser;
>>>>>>> Added triggers and lots of backend
        protected Ability ability;

        public bool IsDone => duration <= 0;

<<<<<<< HEAD
        public TimedAbilityEffect(float duration, Ability ability, GameObject obj)
        {
            this.duration = duration;
            this.ability = ability;
            this.obj = obj;
=======
        public TimedAbilityEffect(float duration, Ability ability, AbilityUser aUser)
        {
            this.duration = duration;
            this.ability = ability;
            this.aUser = aUser;
>>>>>>> Added triggers and lots of backend
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
