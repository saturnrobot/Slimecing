using Slimecing.Abilities.AbilityEffects;
using UnityEngine;
using Slimecing.Character;

namespace Slimecing.Abilities
{
    [CreateAssetMenu(fileName = "DashAbility", menuName = "Abilities/DashAbility")]
    public class DashAbility : Ability
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashLength;
        [SerializeField] private int dashUseAmount;

        public float DashForce { get { return dashForce; } }
        public float DashLength { get { return dashLength; } }

        protected override void PutOnCooldown(AbilityUser aUser)
        {
            aUser.AddAbilityOnCooldown(new CooldownData(this, abilityCooldown, dashUseAmount));
        }

        public override void Use(AbilityUser aUser)
        {
            //Debug.Log(aUser.name + " used " + abilityName);
            aUser.AddTimedAbilityEffect(new DashAbilityEffect(dashLength, this, aUser.gameObject));
        }
    }
}