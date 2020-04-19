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
        [SerializeField] private string abilityDescription = "REPLACE ABILITY DESCRIPTION";
        [SerializeField] protected float abilityCooldown = 1f;
        [SerializeField] protected AudioClip abilitySound;
        [SerializeField] private Trigger abilityTrigger = null;
        public virtual void CheckActivation(AbilityUser aUser, TriggerPackage abilityTriggerPackage)
        {
            if (!abilityTriggerPackage.user.Equals(aUser.gameObject)) return;
            if (AbilityTrigger == null) return;
            if (abilityTriggerPackage.triggerState != TriggerState.Performed) return;
            AbilityTrigger.currentTriggerState = TriggerState.Canceled;
            StartAbility(aUser);
        }
        
        public Trigger AbilityTrigger { get => abilityTrigger; set => abilityTrigger = value; }

        public abstract void Use(AbilityUser aUser);

        public virtual void Initialize(AbilityUser aUser)
        {
            abilityTrigger.EnableTrigger(aUser.gameObject);
            if (abilitySound == null) abilitySound = AudioClip.Create("void", 1, 1, 1000, false);
            
            abilityTrigger.TriggerStateChange += ctx => CheckActivation(aUser, ctx);
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
            AbilityTrigger.currentTriggerState = TriggerState.Canceled;
        }
    }
}
