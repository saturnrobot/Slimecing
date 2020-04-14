<<<<<<< HEAD
﻿using UnityEngine;
=======
﻿using Slimecing.Characters;
using UnityEngine;
>>>>>>> Added triggers and lots of backend

namespace Slimecing.Abilities.AbilityEffects
{
    public abstract class AbilityEffect
    {
<<<<<<< HEAD
        protected bool doesUpdate;

        private GameObject obj;
        private Ability ability;
        
        public bool DoesUpdate => doesUpdate;
        
        public AbilityEffect(Ability ability, GameObject obj)
        {
            this.ability = ability;
            this.obj = obj;
        }
        
        public void UpdateEffect()
        {
            DoUpdate();
=======
        public abstract bool DoesUpdate();

        private AbilityUser aUser;
        private Ability ability;
        
        public AbilityEffect(Ability ability, AbilityUser aUser)
        {
            this.ability = ability;
            this.aUser = aUser;
>>>>>>> Added triggers and lots of backend
        }
        
        public abstract void Activate();
        public virtual void DoUpdate() {}
        public abstract void End();
    }
}