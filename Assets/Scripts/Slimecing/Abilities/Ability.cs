using Slimecing.Character;
using System;
using UnityEngine;

namespace Slimecing.Abilities
{
    [Serializable]
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string abilityName = "REPLACE ABILITY NAME";
        [SerializeField] private string abilityDescription = "REPLACE ABILITY DESCRIPTION";
        [SerializeField] protected float abilityCooldown = 1f;
        [SerializeField] private AudioClip abilitySound;
        [SerializeField] private string abilityButton = "Void";

        private enum CooldownState
        {
            FirstTimeUse,
            HasAnotherUse,
            OnCooldown
        }
        
        public string AbilityButton => abilityButton;
        public abstract void Use(AbilityUser aUser);
        public virtual void Initialize(AbilityUser aUser) { }
        public virtual void StartAbility(AbilityUser aUser)
        {
            //Debug.Log(User.name + " started " + abilityName);
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

        private CooldownState CanUse(AbilityUser aUser)
        {
            Ability coolinAbility = null;
            CooldownData coolinAbilityObject = null;
            
            foreach (var cooldownData in aUser.AbilitiesOnCooldown)
            {
                if (cooldownData.GetAbility != this) continue;
                coolinAbility = cooldownData.GetAbility;
                coolinAbilityObject = cooldownData;
                break;
            }

            if (ReferenceEquals(coolinAbility, null)) return CooldownState.FirstTimeUse;
            if (coolinAbilityObject.UseAmount > 0)
            {
                coolinAbilityObject.SubUseAmount();
                return CooldownState.HasAnotherUse;
            }
            return CooldownState.OnCooldown;
        }
    }
}
