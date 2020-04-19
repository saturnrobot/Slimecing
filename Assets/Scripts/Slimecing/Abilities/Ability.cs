using System;
using Slimecing.Characters;
using Slimecing.Triggers;
using UnityEngine;

namespace Slimecing.Abilities
{
    [Serializable]
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string abilityName = "REPLACE ABILITY NAME";
        [SerializeField] protected float abilityCooldown = 1f;
        [SerializeField] protected AudioClip abilitySound;
        [SerializeField] private Trigger abilityTrigger;

        public Trigger currentAbilityTrigger { get; set; }

        public virtual void CheckActivation(AbilityUser aUser, TriggerState state)
        {
            if (currentAbilityTrigger == null) return;
            if (state != TriggerState.Performed) return;
            currentAbilityTrigger.currentTriggerState = TriggerState.Canceled;
            StartAbility(aUser);
        }
        
        public abstract void Use(AbilityUser aUser);

        public virtual void Initialize(AbilityUser aUser)
        {
            currentAbilityTrigger = Instantiate(abilityTrigger);
            currentAbilityTrigger.EnableTrigger(aUser.gameObject);
            if (abilitySound == null) abilitySound = AudioClip.Create("void", 1, 1, 1000, false);
            
            currentAbilityTrigger.TriggerStateChange += ctx => CheckActivation(aUser, ctx);
        }

        public void StartAbility(AbilityUser aUser)
        {
            switch (CanUse(aUser))
            {
                case CooldownState.FirstTimeUse:
                    Use(aUser);
                    PutOnCooldown(aUser);
                    break;
                case CooldownState.HasAnotherUse:
                    Use(aUser);
                    break;
                case CooldownState.OnCooldown:
                    break;
            }
        }

        protected virtual void PutOnCooldown(AbilityUser aUser)
        {
            aUser.AddAbilityOnCooldown(new CooldownData(this, abilityCooldown));
        }
        protected CooldownState CanUse(AbilityUser aUser)
        {
            Ability coolinAbility = null;
            CooldownData coolinAbilityObject = null;
            foreach (var cooldownData in aUser.abilitiesOnCooldown)
            {
                if (cooldownData.GetAbility != this) continue;
                coolinAbility = cooldownData.GetAbility;
                coolinAbilityObject = cooldownData;
                break;
            }

            if (ReferenceEquals(coolinAbility, null)) return CooldownState.FirstTimeUse;
            if (coolinAbilityObject.UseAmount <= 0) return CooldownState.OnCooldown;
            coolinAbilityObject.SubUseAmount();
            return CooldownState.HasAnotherUse;
        }

        private void EndAbility()
        {
            currentAbilityTrigger.currentTriggerState = TriggerState.Canceled;
        }
    }
}
