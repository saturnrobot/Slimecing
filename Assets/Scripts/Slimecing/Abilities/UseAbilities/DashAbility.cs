using Slimecing.Abilities.AbilityEffects;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities
{
    [CreateAssetMenu(fileName = "DashAbility", menuName = "Abilities/DashAbility")]
    public class DashAbility : Ability
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashLength;
        [SerializeField] private int dashUseAmount;
        
        protected override void PutOnCooldown(AbilityUser aUser)
        {
            aUser.AddAbilityOnCooldown(new CooldownData(this, abilityCooldown, dashUseAmount));
        }

        public override void Use(AbilityUser aUser)
        {
            var characterMovementController = aUser.gameObject.GetComponent<CharacterMovementController>();
            if (characterMovementController == null) return;
            aUser.AddTimedAbilityEffect(new DashAbilityEffect(dashLength, this, aUser, characterMovementController,
                dashForce));
            aUser.Audio.PlayOneShot(abilitySound);
        }
    }
}