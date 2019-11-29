using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public abstract class AbilityEffect
    {
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
        }
        
        public abstract void Activate();
        public virtual void DoUpdate() {}
        public abstract void End();
    }
}