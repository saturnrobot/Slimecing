using System;
using Slimecing.Abilities.AbilityEffects;
using Slimecing.Characters;
using Slimecing.Triggers;
using UnityEngine;

namespace Slimecing.Abilities.UseAbilities
{
    [CreateAssetMenu(fileName = "JumpAbility", menuName = "Abilities/JumpAbility")]
    public class JumpAbility : Ability
    {
        [SerializeField] private float jumpForceInitial;
        [SerializeField] private float jumpForceDouble;
        [SerializeField] private float jumpForceMultiplier;
        [SerializeField] private float simulatedJumpUpMass;
        [SerializeField] private float simulatedJumpFallMass;
        [SerializeField] private int jumpUseAmount;
        
        protected override void PutOnCooldown(AbilityUser aUser)
        {
            aUser.AddAbilityOnCooldown(new CooldownData(this, abilityCooldown, jumpUseAmount));
        }
        public override void Use(AbilityUser aUser)
        {
            var characterMovementController = aUser.gameObject.GetComponent<CharacterMovementController>();
            if (characterMovementController == null) return;
            
            var startJumpEffect = new JumpAbilityEffect(this, aUser, characterMovementController, 
                jumpForceInitial, jumpForceDouble, jumpForceMultiplier, simulatedJumpUpMass, 
                simulatedJumpFallMass, jumpUseAmount);

            if (characterMovementController.IsGrounded())
            {
                startJumpEffect.canJump = true;
                aUser.AddAbilityEffect(startJumpEffect);
                aUser.Audio.PlayOneShot(abilitySound);
            }
            else
            {
                foreach (var effect in aUser.currentAbilityEffects)
                {
                    if (!(effect is JumpAbilityEffect jumpAbilityEffect)) continue;
                    if (jumpAbilityEffect.BeginDouble())
                    {
                        aUser.Audio.PlayOneShot(abilitySound);
                    }
                    return;
                }
            }
        }

        public override void CheckActivation(AbilityUser aUser, TriggerPackage abilityTriggerPackage)
        {
            if (AbilityTrigger == null) return;
            if (!abilityTriggerPackage.user.Equals(aUser.gameObject)) return;
            switch (abilityTriggerPackage.triggerState)
            {
                case TriggerState.Started:
                    StartAbility(aUser);
                    break;
                case TriggerState.Performed:
                    SetCanJump(aUser, false);
                    break;
                case  TriggerState.Canceled:
                    SetCanJump(aUser, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SetCanJump(AbilityUser aUser, bool can)
        {
            foreach (var effect in aUser.currentAbilityEffects)
            {
                switch (effect)
                {
                    case JumpAbilityEffect jumpAbilityEffect:
                        jumpAbilityEffect.canJump = can;
                        return;
                    case SimpleJumpAbilityEffect simpleJumpAbilityEffect:
                        simpleJumpAbilityEffect.canJump = can;
                        return;
                }
            }
        }
    }
}
