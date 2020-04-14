<<<<<<< HEAD
﻿using Slimecing.Character;
using System;
=======
﻿using System;
using Slimecing.Characters;
using Slimecing.Triggers;
>>>>>>> Added triggers and lots of backend
using UnityEngine;

namespace Slimecing.Abilities
{
    [Serializable]
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string abilityName = "REPLACE ABILITY NAME";
        [SerializeField] private string abilityDescription = "REPLACE ABILITY DESCRIPTION";
        [SerializeField] protected float abilityCooldown = 1f;
<<<<<<< HEAD
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
=======
        [SerializeField] protected AudioClip abilitySound;
        [SerializeField] private Trigger abilityTrigger = null;
        public virtual void CheckActivation(AbilityUser aUser, InputTriggerPackage abilityTriggerPackage)
        {
            if (!abilityTriggerPackage.user.Equals(aUser.gameObject)) return;
            if (AbilityTrigger == null) return;
            if (abilityTriggerPackage.triggerState != TriggerState.Performed) return;
            AbilityTrigger.currentTriggerState = TriggerState.Canceled;
            StartAbility(aUser);
        }

        private TriggerInput GetInputAbilityActionType()
        {
            if (abilityTrigger == null) return null;
            return abilityTrigger as TriggerInput;
        }
        
        public Trigger AbilityTrigger { get => abilityTrigger; set => abilityTrigger = value; }

        public abstract void Use(AbilityUser aUser);

        public virtual void Initialize(AbilityUser aUser)
        {
            GetInputAbilityActionType()?.ConfigureInput(aUser.gameObject);
            if (abilitySound == null) abilitySound = AudioClip.Create("void", 1, 1, 1000, false);
            
            abilityTrigger.TriggerStateChange += ctx => CheckActivation(aUser, ctx);
        }

        public void StartAbility(AbilityUser aUser)
        {
>>>>>>> Added triggers and lots of backend
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

<<<<<<< HEAD
        private CooldownState CanUse(AbilityUser aUser)
=======
        protected CooldownState CanUse(AbilityUser aUser)
>>>>>>> Added triggers and lots of backend
        {
            Ability coolinAbility = null;
            CooldownData coolinAbilityObject = null;
            
<<<<<<< HEAD
            foreach (var cooldownData in aUser.AbilitiesOnCooldown)
=======
            foreach (var cooldownData in aUser.abilitiesOnCooldown)
>>>>>>> Added triggers and lots of backend
            {
                if (cooldownData.GetAbility != this) continue;
                coolinAbility = cooldownData.GetAbility;
                coolinAbilityObject = cooldownData;
                break;
            }

            if (ReferenceEquals(coolinAbility, null)) return CooldownState.FirstTimeUse;
<<<<<<< HEAD
            if (coolinAbilityObject.UseAmount > 0)
            {
                coolinAbilityObject.SubUseAmount();
                return CooldownState.HasAnotherUse;
            }
            return CooldownState.OnCooldown;
=======
            if (coolinAbilityObject.UseAmount <= 0) return CooldownState.OnCooldown;
            coolinAbilityObject.SubUseAmount();
            return CooldownState.HasAnotherUse;
        }

        private void EndAbility()
        {
            AbilityTrigger.currentTriggerState = TriggerState.Canceled;
>>>>>>> Added triggers and lots of backend
        }
    }
}
