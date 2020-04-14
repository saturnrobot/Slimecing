using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public abstract class AbilityEffect
    {
        public abstract bool DoesUpdate();

        private AbilityUser aUser;
        private Ability ability;
        
        public AbilityEffect(Ability ability, AbilityUser aUser)
        {
            this.ability = ability;
            this.aUser = aUser;
        }
        
        public abstract void Activate();
        public virtual void DoUpdate() {}
        public abstract void End();
    }
}